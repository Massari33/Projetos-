using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using RFID;

namespace WindowsApplication1 {



    public partial class Form1 : Form
    {



        private void chbxPost_CheckedChanged(object sender, EventArgs e)
        {
            // Alterna postagens automáticas: inicia ou para o timer de post
            // quando a checkbox é alterada. Valida o intervalo selecionado.
            if (chbxPost.Checked)
            {
                if (TimerPost.SelectedItem != null)
                {
                    int segundos = Convert.ToInt32(TimerPost.SelectedItem);

                    postTimer.Interval = segundos * 1000; // milissegundos
                    postTimer.Start();
                }
                else
                {
                    MessageBox.Show("Selecione o tempo para o Post automático.");
                    chbxPost.Checked = false;
                }
            }
            else
            {
                postTimer.Stop();
            }
        }


        private System.Windows.Forms.Timer postTimer = new System.Windows.Forms.Timer();

        // Parâmetros de dispositivo (endereços usados nas chamadas de Get/Set)
        // 01: Transport
        // 02: WorkMode
        // 03: DeviceAddr
        // 04: FilterTime
        // 05: RFPower
        // 06: BeepEnable
        // 07: UartBaudRate
        private const byte PARAM_TRANSPORT = 0x01;
        private const byte PARAM_WORKMODE = 0x02;
        private const byte PARAM_RFPOWER = 0x05;
        private const byte PARAM_BEEP = 0x06;

        // Helpers para mapear índice do combobox de frequência <-> bytes
        private static byte[] GetFreqBytesFromIndex(int index)
        {
            switch (index)
            {
                case 0: return new byte[] { 0x31, 0x80 }; // US/CA
                case 1: return new byte[] { 0x4E, 0x00 }; // Europe
                case 2: return new byte[] { 0x2C, 0xA3 }; // China/HK/TH
                case 3: return new byte[] { 0x29, 0x9D }; // Korea/Japan
                case 4: return new byte[] { 0x2E, 0x9F }; // Australia
                case 5: return new byte[] { 0x2C, 0x81 }; // Singapore
                case 6: return new byte[] { 0x31, 0xA7 }; // Taiwan
                case 7: return new byte[] { 0x31, 0x99 }; // Brazil
                case 8: return new byte[] { 0x1C, 0x99 }; // Israel
                case 9: return new byte[] { 0x24, 0x9D }; // South Africa
                case 10: return new byte[] { 0x28, 0xA1 }; // Malaysia
                default: return new byte[] { 0x4E, 0x00 };
            }
        }

        private static int GetFreqIndexFromBytes(byte[] b)
        {
            if (b == null || b.Length < 2) return -1;
            if (b[0] == 0x31 && b[1] == 0x80) return 0;
            if (b[0] == 0x4E && b[1] == 0x00) return 1;
            if (b[0] == 0x2C && b[1] == 0xA3) return 2;
            if (b[0] == 0x29 && b[1] == 0x9D) return 3;
            if (b[0] == 0x2E && b[1] == 0x9F) return 4;
            if (b[0] == 0x2C && b[1] == 0x81) return 5;
            if (b[0] == 0x31 && b[1] == 0xA7) return 6;
            if (b[0] == 0x31 && b[1] == 0x99) return 7;
            if (b[0] == 0x1C && b[1] == 0x99) return 8;
            if (b[0] == 0x24 && b[1] == 0x9D) return 9;
            if (b[0] == 0x28 && b[1] == 0xA1) return 10;
            return -1;
        }


        private IEnumerable<RfidPostModel> SelecionarPrimeirasLeiturasPorEpc(DataGridView grid)
        {
            // Retorna apenas a primeira leitura para cada EPC único (insensível a maiúsculas/minúsculas)
            var vistos = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            foreach (DataGridViewRow row in grid.Rows)
            {
                if (row.IsNewRow) continue;

                var epc = row.Cells["EPC"]?.Value?.ToString()?.Trim();
                if (string.IsNullOrWhiteSpace(epc)) continue;

                // EPC repetido
                if (!vistos.Add(epc))
                    continue;

                var antena = row.Cells["Ant"]?.Value?.ToString()?.Trim();

                int leituras = 0;
                var celLeituras = row.Cells["Leituras"]?.Value;
                if (celLeituras != null && int.TryParse(celLeituras.ToString(), out var l))
                    leituras = l;

                yield return new RfidPostModel
                {
                    reading_reader_ip = txtIP.Text,               
                    reading_reader_mac = txtMac.Text,             
                    reading_antenna = 1,
                    reading_rssi = 0,
                    reading_epc_hex = epc,
                    reading_movement_type = 1,                  
                    reading_company_id = int.Parse(txtCompanyID.Text),
                    reading_created_at = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                };
            }
        }

        private async Task<bool> EnviarPostEmLoteAsync(List<RfidPostModel> dados, bool modoAutomatico = false)
        {
            // Envia um POST em lote com as leituras fornecidas como um array JSON.
            // Se modoAutomatico for true, suprime diálogos da UI e registra silenciosamente.
            var url = txtPostUrl.Text != null ? txtPostUrl.Text.Trim() : null;
            if (string.IsNullOrWhiteSpace(url))
                throw new ArgumentException("URL do endpoint não informada.");

            // Monte o JSON como ARRAY puro: [ { ... }, { ... } ]
            // Se seu backend exige um "wrapper" (ex.: { "leituras": [ ... ] }),
            // veja a opção mais abaixo.
            string json = JsonConvert.SerializeObject(dados, Formatting.Indented);

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Clear();

                var token = txtToken.Text != null ? txtToken.Text.Trim() : null;
                if (!string.IsNullOrWhiteSpace(token))
                {
                    client.DefaultRequestHeaders.Authorization =
                        new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                }

                // (Opcional) Se quiser declarar que aceita JSON de resposta:
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json")
                );
                using (var content = new StringContent(json, Encoding.UTF8, "application/json"))
                using (var response = await client.PostAsync(url, content))
                {
                    string resposta = await response.Content.ReadAsStringAsync();

                    bool sucesso = TratarResposta(response, resposta, modoAutomatico);


                    return sucesso; // já encerra aqui
                }


            }
        }




        private bool TratarResposta(HttpResponseMessage response, string resposta, bool modoAutomatico)
        {
            // Trata códigos de resposta HTTP e exibe mensagens ou registra no log
            // dependendo se estamos em modo automático.
            if (response.IsSuccessStatusCode)
            {
                if (!modoAutomatico)
                {
                    MessageBox.Show(
                        $"✅ Sucesso!\n\nStatus: {(int)response.StatusCode}\n\n{resposta}",
                        "Sucesso",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                }
                else
                {
                    // log silencioso
                    txtLog.AppendText($"Resposta: [{(int)response.StatusCode}]  Enviado com sucesso\r\n");
                }

                return true;
            }
            else if ((int)response.StatusCode >= 400 &&
                     (int)response.StatusCode < 500)
            {
                if (!modoAutomatico)
                {
                    MessageBox.Show(
                        $"\n\nStatus: {(int)response.StatusCode}{resposta} ⚠ Erro na autorização\n\n",
                        "Erro 4xx",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                }
                else
                {
                    txtLog.AppendText($"Resposta: [{(int)response.StatusCode}] Não autorizado, verifque credenciais e cadastro\r\n");
                }

                return false;
            }
            else if ((int)response.StatusCode >= 500)
            {
                if (!modoAutomatico)
                {
                    MessageBox.Show(
                        $"\n\nStatus: {(int)response.StatusCode}{resposta} Erro do Servidor\n\n",
                        "Erro 5xx",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                }
                else
                {
                    txtLog.AppendText($"Resposta: [{(int)response.StatusCode}] Erro Servidor\r\n");
                }

                return false;
            }

            return false;
        }

        private async Task<bool> EnviarPostAsync(RfidPostModel dados, bool modoAutomatico)
        {
            // Envia uma leitura única como JSON para o endpoint configurado.
            // Retorna true quando o servidor confirmou sucesso.
            try
            {
                string url = txtPostUrl.Text.Trim();

                using (HttpClient client = new HttpClient())
                {
                    string json = JsonConvert.SerializeObject(dados);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    HttpResponseMessage response =
                        await client.PostAsync(url, content);

                    string resposta =
                        await response.Content.ReadAsStringAsync();

                    bool sucesso = TratarResposta(response, resposta, modoAutomatico);

                    return sucesso; // 🔥 RETORNA aqui
                }
            }
            catch (HttpRequestException ex)
            {
                if (!modoAutomatico)
                    MessageBox.Show("Erro de conexão: " + ex.Message);

                return false;
            }
            catch (Exception ex)
            {
                if (!modoAutomatico)
                    MessageBox.Show("Erro inesperado: " + ex.Message);

                return false;
            }
        }





        DataTable Leitura = new DataTable();
        private DataTable leituraPost = new DataTable();

        public Form1()
        {
            InitializeComponent();
            byte[] arrBuffer = new byte[256];
        }


        public class RfidPostModel
        {
            public string reading_reader_ip { get; set; }
            public string reading_reader_mac { get; set; }
            public int reading_antenna { get; set; }
            public int reading_rssi { get; set; }
            public string reading_epc_hex { get; set; }
            public int reading_movement_type { get; set; }
            public int reading_company_id { get; set; }
            public string reading_created_at { get; set; }

        }

       

        delegate void SetTextCallback(string text);
        private void SetText(string text)
        {
            // Adiciona texto ao log (`txtLog`) de forma segura entre threads.
            if (this.txtLog.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetText);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.txtLog.Text += text;
                this.txtLog.SelectionStart = this.txtLog.Text.Length;
                this.txtLog.ScrollToCaret();
            }
        }







        private void Form1_Load(object sender, EventArgs e) {

            TimerPost.SelectedItem = "3";

            postTimer.Tick += PostTimer_Tick;

            // Criação das colunas do DataTable
            //leituraPost.Columns.Add("Tipo");
            leituraPost.Columns.Add("Ant");
            leituraPost.Columns.Add("EPC");
            leituraPost.Columns.Add("Leituras", typeof(int));
           

            // Vincula ao grid
            dgLeituras.DataSource = leituraPost;

            // Configuração visual do grid
            dgLeituras.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgLeituras.RowHeadersVisible = false;
            dgLeituras.AllowUserToAddRows = false;
            dgLeituras.AllowUserToResizeRows = false;
            dgLeituras.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgLeituras.MultiSelect = false;
            dgLeituras.ColumnHeadersVisible = false; // começa escondido


            Block();
            rbCom.Checked = true;
            cbRegiao.SelectedIndex = 1;

            tabelaLeitura();
        }

        private void tabelaLeitura() {
            // Prepara o esquema do DataTable `Leitura` usado para leituras ao vivo.
            Leitura.Columns.Add("Ant");
            Leitura.Columns.Add("EPC");
            Leitura.Columns.Add("Leituras");
        }

        private void Block() {
            // Desabilita controles que não devem estar disponíveis enquanto desconectado.
            rbCom.Enabled = true;
            rbHid.Enabled = true;
            btReload.Enabled = true;
            btConectar.Enabled = true;
            btDesconectar.Enabled = false;
            cbModo.Enabled = false;
            btModo.Enabled = false;
            btGpioOn.Enabled = false;
            btBeepOn.Enabled = false;
            cbPotencia.Enabled = false;
            btSetPotencia.Enabled = false;
            btGetPotencia.Enabled = false;
            btLimpar.Enabled = false;
            //btIniciar.Enabled = false;
            cbRegiao.Enabled = false;
            txtStart.Enabled = false;
            txtLenght.Enabled = false;
            txtAcessPass.Enabled = false;
            txtRetorno.Enabled = false;
            txtWriteData.Enabled = false;
            ckIncremental.Enabled = false;
            ckAuto.Enabled = false;
            btLer.Enabled = false;
            btGravar.Enabled = false;
            //dgLeitura.Enabled = false;
            //btLimpaDg.Enabled = false;
            cbFreq.Enabled = false;
            btSetFreq.Enabled = false;
            btGetFreq.Enabled = false;
            cbInterface.Enabled = false;
            btInterface.Enabled = false;
            cbPorta.Enabled = true;
        }

        private void Unblock() {
            // Habilita controles disponíveis quando conectado ao leitor.
            rbCom.Enabled = true;
            rbHid.Enabled = true;
            btConectar.Enabled = false;
            btReload.Enabled = false;
            btDesconectar.Enabled = true;
            cbModo.Enabled = true;
            btModo.Enabled = true;
            btGpioOn.Enabled = true;
            btBeepOn.Enabled = true;
            cbPotencia.Enabled = true;
            btSetPotencia.Enabled = true;
            btGetPotencia.Enabled = true;
            btLimpar.Enabled = true;
            //btIniciar.Enabled = true;
            cbRegiao.Enabled = true;
            txtStart.Enabled = true;
            txtLenght.Enabled = true;
            txtAcessPass.Enabled = true;
            txtRetorno.Enabled = true;
            txtWriteData.Enabled = true;
            ckIncremental.Enabled = true;
            ckAuto.Enabled = true;
            btLer.Enabled = true;
            btGravar.Enabled = true;
            //dgLeitura.Enabled = true;
            //btLimpaDg.Enabled = true;
            cbFreq.Enabled = true;
            btSetFreq.Enabled = true;
            btGetFreq.Enabled = true;
            cbInterface.Enabled = true;
            btInterface.Enabled = true;
            cbPorta.Enabled = false;
        }

        private void btConectar_Click(object sender, EventArgs e) {
            // Conecta ao dispositivo via COM ou HID, dependendo da seleção.
            if (rbCom.Checked) {
                if (cbPorta.Items.Count == 0) {
                    this.SetText("Não há portas disponíveis\r\n");
                }
                else {
                    byte[] arrBuffer = new byte[64];

                    String strPort = cbPorta.Text;
                    if (RFID.CFComApi.CFCom_OpenDevice(strPort, 115200)) {
                        this.SetText("Conectado\r\n");
                        if (RFID.CFComApi.CFCom_GetDeviceSystemInfo(0xFF, arrBuffer) == false) {
                            this.SetText("Erro de Conexão\r\n");
                            //RFID.CFComApi.CFCom_CloseDevice();
                            //return;
                        }
                    }
                    else {
                        this.SetText("Falha ao Conectar\r\n");
                        return;
                    }

                    string str = "", str1 = "";
                    str = String.Format("SoftVer:{0:D}.{0:D}\r\n", arrBuffer[0] >> 4, arrBuffer[0] & 0x0F);
                    this.SetText(str);
                    str = String.Format("HardVer:{0:D}.{0:D}\r\n", arrBuffer[1] >> 4, arrBuffer[1] & 0x0F);
                    str = "SN:";
                    string sn = "";

                    for (int i = 0; i < 7; i++)
                    {
                        str1 = String.Format("{0:X2}", arrBuffer[2 + i]);
                        sn += str1;     // monta só o número
                        str += str1;    // continua mostrando no log
                    }

                    str += "\r\n";
                    this.SetText(str);

                    // 🔥 Aqui preenche o txtMac automaticamente
                    txtMac.Text = sn;

                    Unblock();
                    CarregaBeep();
                    CarregaPotencia();
                    CarregaModo();
                    CarregaFrequencia();
                    rbHid.Enabled = false;
                    gbInt.Enabled = false;

                }
            }
            else if (rbHid.Checked) {
                byte[] arrBuffer = new byte[64];

                if (RFID.CFHidApi.CFHid_OpenDevice((UInt16)cbPorta.SelectedIndex)) {
                    this.SetText((UInt16)cbPorta.SelectedIndex + "\r\n");
                    this.SetText("Conectado\r\n");
                    if (RFID.CFHidApi.CFHid_GetDeviceSystemInfo(0xFF, arrBuffer) == false) //Falha ao obter informações do sistema
                    {
                        this.SetText("Erro de Conexão\r\n");
                        //RFID.CFHidApi.CFHid_CloseDevice();
                        //return;
                    }
                }
                else {
                    this.SetText("Falha ao Conectar\r\n");
                    return;
                }

                string str = "", str1 = "";
                str = String.Format("SoftVer:{0:D}.{0:D}\r\n", arrBuffer[0] >> 4, arrBuffer[0] & 0x0F);
                this.SetText(str);
                str = String.Format("HardVer:{0:D}.{0:D}\r\n", arrBuffer[1] >> 4, arrBuffer[1] & 0x0F);
                this.SetText(str);
                str = "SN:";
                string sn = "";

                for (int i = 0; i < 7; i++)
                {
                    str1 = String.Format("{0:X2}", arrBuffer[2 + i]);
                    sn += str1;     // monta só o número
                    str += str1;    // continua mostrando no log
                }

                str += "\r\n";
                this.SetText(str);

                // 🔥 Aqui preenche o txtMac automaticamente
                txtMac.Text = sn;
                Unblock();
                CarregaBeep();
                CarregaPotencia();
                CarregaModo();
                CarregaFrequencia();
                CarregaInterface();
                rbCom.Enabled = false;
                txtLog.Focus();
                gbInt.Enabled = true;

            }

        }

        private void btDesconectar_Click(object sender, EventArgs e) {
            // Fecha a conexão com o dispositivo e atualiza o estado da UI.
            if (rbCom.Checked) {
                RFID.CFComApi.CFCom_CloseDevice();
                Block();
                this.SetText("Fechar\r\n");
                rbHid.Enabled = true;
            }
            else if (rbHid.Checked) {
                RFID.CFHidApi.CFHid_CloseDevice();
                Block();
                this.SetText("Fechar\r\n");
                rbCom.Enabled = true;
            }


        }

        private void btModo_Click(object sender, EventArgs e) {
            // Define o modo de trabalho do leitor (escravo/autônomo) no dispositivo.
            if (rbCom.Checked) {
                RFID.CFComApi.CFCom_ClearTagBuf();
                byte bParamAddr = 0;
                byte bValue = 0;

                /*  01: Transport
                    02: WorkMode
                    03: DeviceAddr
                    04: FilterTime
                    05: RFPower
                    06: BeepEnable
                    07: UartBaudRate*/
                bParamAddr = 0x02;
                //bValue = 26;   //RF = 26

                if (cbModo.SelectedIndex == 0) {
                    bValue = (byte)Convert.ToInt16(0);
                }
                else if (cbModo.SelectedIndex == 1) {
                    bValue = (byte)Convert.ToInt16(1);
                }

                if (RFID.CFComApi.CFCom_SetDeviceOneParam(0xFF, bParamAddr, bValue) == false) {
                    this.SetText("Falha ao Definir " + cbModo.Text + "\r\n");
                    return;
                }
                this.SetText(cbModo.Text + " Definido com sucesso\r\n");
            }
            else if (rbHid.Checked) {
                RFID.CFHidApi.CFHid_ClearTagBuf();
                byte bParamAddr = 0;
                byte bValue = 0;

                /*  01: Transport
                    02: WorkMode
                    03: DeviceAddr
                    04: FilterTime
                    05: RFPower
                    06: BeepEnable
                    07: UartBaudRate*/
                bParamAddr = 0x02;
                //bValue = 26;   //RF = 26

                if (cbModo.SelectedIndex == 0) {
                    bValue = (byte)Convert.ToInt16(0);
                }
                else if (cbModo.SelectedIndex == 1) {
                    bValue = (byte)Convert.ToInt16(1);
                }

                if (RFID.CFHidApi.CFHid_SetDeviceOneParam(0xFF, bParamAddr, bValue) == false) {
                    this.SetText("Falha ao Definir " + cbModo.Text + "\r\n");
                    return;
                }
                this.SetText(cbModo.Text + " Definido com sucesso\r\n");
                txtLog.Focus();
            }

        }

        private void rbCom_CheckedChanged(object sender, EventArgs e) {
            // Atualiza a lista de portas COM disponíveis quando a opção COM é selecionada.
            if (rbCom.Checked) {
                cbPorta.Items.Clear();
                string[] ports = SerialPort.GetPortNames();
                foreach (var s in ports) {
                    cbPorta.Items.Add(s);
                    Console.WriteLine("Porta: " + s);
                }

                if (cbPorta.Items.Count > 0) {
                    cbPorta.SelectedIndex = 0;
                }

            }
        }

        private void rbHid_CheckedChanged(object sender, EventArgs e) {
            // Atualiza a lista de dispositivos HID disponíveis quando HID é selecionado.
            if (rbHid.Checked) {
                string strSN = "";
                byte[] arrBuffer = new byte[256];
                int iHidNumber = 0;
                UInt16 iIndex = 0;
                cbPorta.Items.Clear();
                iHidNumber = RFID.CFHidApi.CFHid_GetUsbCount();
                for (iIndex = 0; iIndex < iHidNumber; iIndex++) {
                    RFID.CFHidApi.CFHid_GetUsbInfo(iIndex, arrBuffer);
                    strSN = System.Text.Encoding.Default.GetString(arrBuffer);
                    cbPorta.Items.Add(strSN);
                }
                if (iHidNumber > 0)
                    cbPorta.SelectedIndex = 0;
            }
        }

        private void btReload_Click(object sender, EventArgs e) {
            // Recarrega a lista de portas/dispositivos disponíveis.
            if (rbCom.Checked) {
                cbPorta.Items.Clear();
                string[] ports = SerialPort.GetPortNames();
                foreach (var s in ports) {
                    cbPorta.Items.Add(s);
                    Console.WriteLine("Porta: " + s);
                }
                if (cbPorta.Items.Count > 0) {
                    cbPorta.SelectedIndex = 0;
                }
            }
            else if (rbHid.Checked) {
                string strSN = "";
                byte[] arrBuffer = new byte[256];
                int iHidNumber = 0;
                UInt16 iIndex = 0;
                cbPorta.Items.Clear();
                iHidNumber = RFID.CFHidApi.CFHid_GetUsbCount();
                for (iIndex = 0; iIndex < iHidNumber; iIndex++) {
                    RFID.CFHidApi.CFHid_GetUsbInfo(iIndex, arrBuffer);
                    strSN = System.Text.Encoding.Default.GetString(arrBuffer);
                    cbPorta.Items.Add(strSN);
                }
                if (iHidNumber > 0)
                    cbPorta.SelectedIndex = 0;
            }
        }

        private void btSetPotencia_Click(object sender, EventArgs e) {
            // Define o parâmetro de potência RF no dispositivo.
            byte bParamAddr = 0;
            byte bValue = 0;

            /*  01: Transport
                02: WorkMode
                03: DeviceAddr
                04: FilterTime
                05: RFPower
                06: BeepEnable
                07: UartBaudRate*/
            bParamAddr = 0x05;
            //bValue = 26;   //RF = 26

            bValue = (byte)Convert.ToInt16(cbPotencia.SelectedItem);
            if (rbCom.Checked) {
                if (RFID.CFComApi.CFCom_SetDeviceOneParam(0xFF, bParamAddr, bValue) == false) {
                    this.SetText("Falha ao definir potência\r\n");
                    return;
                }
                this.SetText("Sucesso ao definir potência\r\n");
            }
            else if (rbHid.Checked) {
                if (RFID.CFHidApi.CFHid_SetDeviceOneParam(0xFF, bParamAddr, bValue) == false) {
                    this.SetText("Falha ao definir potência\r\n");
                    return;
                }
                this.SetText("Sucesso ao definir potência\r\n");
                txtLog.Focus();
            }
        }

        private void CarregaInterface() {
            // Lê o parâmetro de interface (transporte) do dispositivo e atualiza a UI.
            byte bParamAddr = 0;
            byte[] bValue = new byte[2];

            /*  01: Transport
                02: WorkMode
                03: DeviceAddr
                04: FilterTime
                05: RFPower
                06: BeepEnable
                07: UartBaudRate*/
            bParamAddr = 0x01;

            if (rbCom.Checked) {

            }
            else if (rbHid.Checked) {
                if (RFID.CFHidApi.CFHid_ReadDeviceOneParam(0xFF, bParamAddr, bValue) == false) {
                    this.SetText("Falha ao Obter Interface\r\n");
                    return;
                }
                txtLog.Focus();
            }


            cbInterface.SelectedIndex = bValue[0];


            this.SetText("Sucesso ao Obter Interface\r\n");
        }

        private void CarregaBeep() {
            // Lê o estado atual do beep (habilitado/desabilitado) do dispositivo.
            byte bParamAddr = 0;
            byte[] bValue = new byte[2];

            /*  01: Transport
                02: WorkMode
                03: DeviceAddr
                04: FilterTime
                05: RFPower
                06: BeepEnable
                07: UartBaudRate*/
            bParamAddr = 0x06;

            if (rbCom.Checked) {
                if (RFID.CFComApi.CFCom_ReadDeviceOneParam(0xFF, bParamAddr, bValue) == false) {
                    this.SetText("Falha ao Obter Beep\r\n");
                    return;
                }
            }
            else if (rbHid.Checked) {
                if (RFID.CFHidApi.CFHid_ReadDeviceOneParam(0xFF, bParamAddr, bValue) == false) {
                    this.SetText("Falha ao Obter Beep\r\n");
                    return;
                }
                txtLog.Focus();
            }

            if (bValue[0] == 0) {
                btBeepOn.Text = "Desligado";
            }
            else if (bValue[0] == 1) {
                btBeepOn.Text = "Ligado";
            }

            this.SetText("Sucesso ao Obter Beep\r\n");
        }

        private void CarregaPotencia() {
            // Lê o parâmetro de potência RF do dispositivo e atualiza controles da UI.
            byte bParamAddr = 0;
            byte[] bValue = new byte[2];

            /*  01: Transport
                02: WorkMode
                03: DeviceAddr
                04: FilterTime
                05: RFPower
                06: BeepEnable
                07: UartBaudRate*/
            bParamAddr = 0x05;

            if (rbCom.Checked) {
                if (RFID.CFComApi.CFCom_ReadDeviceOneParam(0xFF, bParamAddr, bValue) == false) {
                    this.SetText("Falha ao carregar potência\r\n");
                    return;
                }
            }
            else if (rbHid.Checked) {
                if (RFID.CFHidApi.CFHid_ReadDeviceOneParam(0xFF, bParamAddr, bValue) == false) {
                    this.SetText("Falha ao carregar potência\r\n");
                    return;
                }
                txtLog.Focus();
            }

            string str1 = "";
            str1 = bValue[0].ToString("d2");
            str1 = "RF:" + str1 + "\r\n";

            cbPotencia.SelectedIndex = bValue[0];
            this.SetText(str1);
        }
        private void CarregaModo() {
            // Lê o modo de trabalho atual (escravo/autônomo) do dispositivo.
            byte bParamAddr = 0;
            byte[] bValue = new byte[2];

            /*  01: Transport
                02: WorkMode
                03: DeviceAddr
                04: FilterTime
                05: RFPower
                06: BeepEnable
                07: UartBaudRate*/
            bParamAddr = 0x02;

            if (rbCom.Checked) {
                if (RFID.CFComApi.CFCom_ReadDeviceOneParam(0xFF, bParamAddr, bValue) == false) {
                    this.SetText("Falha ao carregar modo\r\n");
                    return;
                }
            }
            else if (rbHid.Checked) {
                if (RFID.CFHidApi.CFHid_ReadDeviceOneParam(0xFF, bParamAddr, bValue) == false) {
                    this.SetText("Falha ao carregar modo\r\n");
                    return;
                }
                txtLog.Focus();
            }

            cbModo.SelectedIndex = bValue[0];

            if (cbModo.SelectedIndex == 0) {
                this.SetText("Modo Escravo\r\n");
            }
            else if (cbModo.SelectedIndex == 1) {
                this.SetText("Modo Autônomo\r\n");
            }

        }

        private void CarregaFrequencia() {
            // Lê a configuração de frequência do dispositivo e ajusta o índice do combobox.
            byte[] bFreq = new byte[2];

            if (rbCom.Checked) {
                if (RFID.CFComApi.CFCom_ReadFreq(0xFF, bFreq) == false) {
                    this.SetText("Falha ao Obter Frequência\r\n");
                    return;
                }
            }
            else if (rbHid.Checked) {
                if (RFID.CFHidApi.CFHid_ReadFreq(0xFF, bFreq) == false) {
                    this.SetText("Falha ao Obter Frequência\r\n");
                    return;
                }
                txtLog.Focus();
            }
            int idx = GetFreqIndexFromBytes(bFreq);
            if (idx >= 0)
                cbFreq.SelectedIndex = idx;

            this.SetText("Sucesso ao Obter Frequência\r\n");
        }


        private void btGetPotencia_Click(object sender, EventArgs e) {
            // Consulta a potência RF e exibe o resultado na UI.
            byte bParamAddr = 0;
            byte[] bValue = new byte[2];

            /*  01: Transport
                02: WorkMode
                03: DeviceAddr
                04: FilterTime
                05: RFPower
                06: BeepEnable
                07: UartBaudRate*/
            bParamAddr = 0x05;

            if (rbCom.Checked) {
                if (RFID.CFComApi.CFCom_ReadDeviceOneParam(0xFF, bParamAddr, bValue) == false) {
                    this.SetText("Falha ao obter potência\r\n");
                    return;
                }
            }
            else if (rbHid.Checked) {
                if (RFID.CFHidApi.CFHid_ReadDeviceOneParam(0xFF, bParamAddr, bValue) == false) {
                    this.SetText("Falha ao obter potência\r\n");
                    return;
                }
                txtLog.Focus();
            }

            string str1 = "";
            str1 = bValue[0].ToString("d2");
            str1 = "RF:" + str1 + "\r\n";

            cbPotencia.SelectedIndex = bValue[0];
            this.SetText(str1);
        }

        private void btGpioOn_Click(object sender, EventArgs e) {
            // Alterna o relé GPIO do dispositivo (ligar/desligar) e atualiza o texto do botão.
            if (btGpioOn.Text == "Desligado") {

                if (rbCom.Checked) {
                    if (RFID.CFComApi.CFCom_RelayOn(0xFF) == false) {
                        this.SetText("Falha ao ligar GPIO\r\n");
                        return;
                    }
                }
                else if (rbHid.Checked) {
                    if (RFID.CFHidApi.CFHid_RelayOn(0xFF) == false) {
                        this.SetText("Falha ao ligar GPIO\r\n");
                        return;
                    }
                    txtLog.Focus();
                }

                btGpioOn.Text = "Ligado";
                this.SetText("Sucesso ao ligar GPIO\r\n");
            }
            else if (btGpioOn.Text == "Ligado") {

                if (rbCom.Checked) {
                    if (RFID.CFComApi.CFCom_RelayOff(0xFF) == false) {
                        this.SetText("Falha ao desligar GPIO\r\n");
                        return;
                    }
                }
                else if (rbHid.Checked) {
                    if (RFID.CFHidApi.CFHid_RelayOff(0xFF) == false) {
                        this.SetText("Falha ao desligar GPIO\r\n");
                        return;
                    }
                    txtLog.Focus();
                }

                btGpioOn.Text = "Desligado";
                this.SetText("Sucesso ao desligar GPIO\r\n");
            }
        }



        private void btBeepOn_Click(object sender, EventArgs e) {
            // Alterna o beep do dispositivo (ligar/desligar) e persiste a configuração no dispositivo.
            if (rbCom.Checked) {
                RFID.CFComApi.CFCom_ClearTagBuf();
            }
            else if (rbHid.Checked) {
                RFID.CFHidApi.CFHid_ClearTagBuf();
            }
            byte bParamAddr = 0;
            byte bValue = 0;

            /*  01: Transport
                02: WorkMode
                03: DeviceAddr
                04: FilterTime
                05: RFPower
                06: BeepEnable
                07: UartBaudRate*/
            bParamAddr = 0x06;
            //bValue = 26;   //RF = 26

            if (btBeepOn.Text == "Desligado") {
                bValue = (byte)Convert.ToInt16(1);
                if (rbCom.Checked) {
                    if (RFID.CFComApi.CFCom_SetDeviceOneParam(0xFF, bParamAddr, bValue) == false) {
                        this.SetText("Falha ao Ligar Beep\r\n");
                        return;
                    }
                }
                else if (rbHid.Checked) {
                    if (RFID.CFHidApi.CFHid_SetDeviceOneParam(0xFF, bParamAddr, bValue) == false) {
                        this.SetText("Falha ao Ligar Beep\r\n");
                        return;
                    }
                    txtLog.Focus();
                }
                btBeepOn.Text = "Ligado";
                this.SetText("Sucesso ao Ligar Beep\r\n");
            }
            else if (btBeepOn.Text == "Ligado") {
                bValue = (byte)Convert.ToInt16(0);

                if (rbCom.Checked) {
                    if (RFID.CFComApi.CFCom_SetDeviceOneParam(0xFF, bParamAddr, bValue) == false) {
                        this.SetText("Falha ao Ligar Beep\r\n");
                        return;
                    }
                }
                else if (rbHid.Checked) {
                    if (RFID.CFHidApi.CFHid_SetDeviceOneParam(0xFF, bParamAddr, bValue) == false) {
                        this.SetText("Falha ao Ligar Beep\r\n");
                        return;
                    }
                    txtLog.Focus();
                }
                btBeepOn.Text = "Desligado";
                this.SetText("Sucesso ao Desligar Beep\r\n");
            }
        }

        private void btSetFreq_Click(object sender, EventArgs e) {
            // Aplica a região de frequência selecionada ao dispositivo.
            byte[] bFreq = GetFreqBytesFromIndex(cbFreq.SelectedIndex);

            if (rbCom.Checked)
            {
                if (RFID.CFComApi.CFCom_SetFreq(0xFF, bFreq) == false)
                {
                    this.SetText("Falha ao Definir Frequência\r\n");
                    return;
                }
            }
            else if (rbHid.Checked)
            {
                if (RFID.CFHidApi.CFHid_SetFreq(0xFF, bFreq) == false)
                {
                    this.SetText("Falha ao Definir Frequência\r\n");
                    return;
                }
                txtLog.Focus();
            }

            this.SetText("Sucesso ao Definir Frequência\r\n");

        }



        private void btLimpar_Click(object sender, EventArgs e) {
            // Limpa o textbox principal de log.
            txtLog.Text = "";
        }

        private void btGetFreq_Click(object sender, EventArgs e) {
            // Lê e exibe a frequência atual do dispositivo.
            byte[] bFreq = new byte[2];

            if (rbCom.Checked) {
                if (RFID.CFComApi.CFCom_ReadFreq(0xFF, bFreq) == false) {
                    this.SetText("Falha ao Obter Frequência\r\n");
                    return;
                }
            }
            else if (rbHid.Checked) {
                if (RFID.CFHidApi.CFHid_ReadFreq(0xFF, bFreq) == false) {
                    this.SetText("Falha ao Obter Frequência\r\n");
                    return;
                }
                txtLog.Focus();
            }
            int idx = GetFreqIndexFromBytes(bFreq);
            if (idx >= 0)
                cbFreq.SelectedIndex = idx;

            this.SetText("Sucesso ao Obter Frequência\r\n");
        }

        private void btIniciar_Click(object sender, EventArgs e) {
            // Inicia/para o timer de inventário contínuo (leituras) e valida o modo.

            if (rbHid.Checked) {
                txtLog.Focus();
                RFID.CFHidApi.CFHid_ClearTagBuf();

               
               

                /*  01: Transport
                    02: WorkMode
                    03: DeviceAddr
                    04: FilterTime
                    05: RFPower
                    06: BeepEnable
                    07: UartBaudRate*/

                //bValue = 26;   //RF = 26
                CarregaInterface();
                CarregaModo();
                if (cbInterface.SelectedIndex == 1) {
                    MessageBox.Show("Mude para Interface USB!\r\n");
                    this.SetText("Falha\r\n");
                    return;
                }

                if (cbModo.SelectedIndex == 1) {
                    MessageBox.Show("Mude para Modo Escravo!\r\n");
                    this.SetText("Falha\r\n");
                    return;
                }



                timerLeitura.Enabled = !timerLeitura.Enabled;

                RFID.CFHidApi.CFHid_ClearTagBuf();

                /* MessageBox.Show("Não permitido em Modo HID\r\nVerifique o log na parte inferior para verificar leituras HID\r\n");
                 this.SetText("Modo HID");
                 btIniciar.Text = "Iniciar";
                 //btLimpaDg.Enabled = true;
                 groupBox9.Enabled = true;
                 timerLeitura.Enabled = false;
                 txtLog.Focus();*/
            }
            else if (rbCom.Checked) {
                RFID.CFComApi.CFCom_ClearTagBuf();

                
                

                /*  01: Transport
                    02: WorkMode
                    03: DeviceAddr
                    04: FilterTime
                    05: RFPower
                    06: BeepEnable
                    07: UartBaudRate*/
               
                //bValue = 26;   //RF = 26

                CarregaModo();

                if (cbModo.SelectedIndex == 1) {
                    MessageBox.Show("Mude para Modo Escravo!\r\n");
                    this.SetText("Falha\r\n");
                    return;
                }



                timerLeitura.Enabled = !timerLeitura.Enabled;

                RFID.CFComApi.CFCom_ClearTagBuf();
            }
        }

        private void btLer_Click(object sender, EventArgs e) {
            // Lê banco de memória de uma tag e exibe os dados hex retornados.
            byte[] arrBuffer = new byte[4096];
            byte Mem = 0x02;
            byte WordPtr = 0x00;
            byte ReadEPClen = 0x06;
            ushort iNum = 0;
            ushort iTotalLen = 0;
            byte[] Password = new byte[4];

           

            Mem = Convert.ToByte(cbRegiao.SelectedIndex);

            string sStartAdd = txtStart.Text;
            string sLen = txtLenght.Text;
            string sPass = txtAcessPass.Text;

            WordPtr = byte.Parse(sStartAdd);
            ReadEPClen = byte.Parse(sLen);


            Password[0] = byte.Parse(sPass.Substring(0, 1));
            Password[1] = byte.Parse(sPass.Substring(1, 1));
            Password[2] = byte.Parse(sPass.Substring(2, 1));
            Password[3] = byte.Parse(sPass.Substring(3, 1));

            txtRetorno.Text = "";

            Console.WriteLine("MEM: " + Mem + "\nWORDPTR: " + WordPtr + "\nEPCLEN: " + ReadEPClen + "\nPassWord0: " + Password[0] + "\nPassWord1: " + Password[1] + "\nPassWord2: " + Password[2] + "\nPassWord3: " + Password[3]);

            if (rbCom.Checked) {
                if (RFID.CFComApi.CFCom_ReadCardG2(0xFF, Password, Mem, WordPtr, ReadEPClen, arrBuffer) == false) {
                    this.SetText("Falha na leitura\r\n");
                    return;
                }
                this.SetText("Sucesso na leitura\r\n");

                if (Mem == 0x00) {
                    iTotalLen = 4;
                }
                else if (Mem == 0x01) {
                    iTotalLen = 12;
                }
                else if (Mem == 0x02) {
                    iTotalLen = 12;
                }
                else if (Mem == 0x03) {
                    iTotalLen = 64;
                }

                int iTagLength = 0;
                int iTagNumber = 0;
                iTagLength = iTotalLen;
                iTagNumber = iNum;
                
                int iLength = 0;
                byte bPackLength = 0;
                int i = 0;


                bPackLength = arrBuffer[iLength];
                string str2 = "";
                string str1 = "";
                str1 = arrBuffer[1 + iLength + 0].ToString("X2");
                str2 = str2 + "Type:" + str1 + " ";  //Tag Type

                str1 = arrBuffer[1 + iLength + 1].ToString("X2");
                str2 = str2 + "Ant:" + str1 + " Tag:";  //Ant

                string str3 = "";
                for (i = 0; i < iTotalLen; i++) {
                    str1 = arrBuffer[iLength + i].ToString("X2");
                    str3 = str3 + str1;
                }
                str2 = str2 + str3;
                str1 = arrBuffer[1 + iLength + i].ToString("X2");
                str2 = str2 + "RSSI:" + str1 + "\r\n";  //RSSI
                iLength = iLength + bPackLength + 1;
                this.SetText(str3 + "\r\n");
                txtRetorno.Text = str3.Trim();

            }
            else if (rbHid.Checked) {
                CarregaModo();
                if (cbModo.SelectedIndex == 1) {

                    RFID.CFHidApi.CFHid_ClearTagBuf();

                    byte bParamAddr = 0;
                    byte bValue = 0;

                    /*  01: Transport
                        02: WorkMode
                        03: DeviceAddr
                        04: FilterTime
                        05: RFPower
                        06: BeepEnable
                        07: UartBaudRate*/
                    bParamAddr = 0x02;
                    //bValue = 26;   //RF = 26

                    bValue = (byte)Convert.ToInt16(0);

                    if (RFID.CFHidApi.CFHid_SetDeviceOneParam(0xFF, bParamAddr, bValue) == false) {
                        this.SetText("Falha ao modificar modo\r\n");
                        MessageBox.Show("Para esta leitura mude o dispositivo HID para Modo Escravo");
                        return;
                    }

                    if (RFID.CFHidApi.CFHid_ReadCardG2(0xFF, Password, Mem, WordPtr, ReadEPClen, arrBuffer) == false) {
                        this.SetText("Falha na leitura\r\n");
                        return;
                    }
                    this.SetText("Sucesso na leitura\r\n");

                    if (Mem == 0x00) {
                        iTotalLen = 4;
                    }
                    else if (Mem == 0x01) {
                        iTotalLen = 12;
                    }
                    else if (Mem == 0x02) {
                        iTotalLen = 12;
                    }
                    else if (Mem == 0x03) {
                        iTotalLen = 64;
                    }

                    int iTagLength = 0;
                    int iTagNumber = 0;
                    iTagLength = iTotalLen;
                    iTagNumber = iNum;
                    int iLength = 0;
                    byte bPackLength = 0;
                    int i = 0;


                    bPackLength = arrBuffer[iLength];
                    string str2 = "";
                    string str1 = "";
                    str1 = arrBuffer[1 + iLength + 0].ToString("X2");
                    str2 = str2 + "Type:" + str1 + " ";  //Tag Type

                    str1 = arrBuffer[1 + iLength + 1].ToString("X2");
                    str2 = str2 + "Ant:" + str1 + " Tag:";  //Ant

                    string str3 = "";
                    for (i = 0; i < iTotalLen; i++) {
                        str1 = arrBuffer[iLength + i].ToString("X2");
                        str3 = str3 + str1;
                    }
                    str2 = str2 + str3;
                    str1 = arrBuffer[1 + iLength + i].ToString("X2");
                    str2 = str2 + "RSSI:" + str1 + "\r\n";  //RSSI
                    iLength = iLength + bPackLength + 1;
                    this.SetText(str3 + "\r\n");
                    txtRetorno.Text = str3.Trim();


                    bValue = (byte)Convert.ToInt16(1);

                    if (RFID.CFHidApi.CFHid_SetDeviceOneParam(0xFF, bParamAddr, bValue) == false) {
                        this.SetText("Falha ao modificar modo\r\n");
                        return;
                    }

                }
                else if (cbModo.SelectedIndex == 0) {
                    if (RFID.CFHidApi.CFHid_ReadCardG2(0xFF, Password, Mem, WordPtr, ReadEPClen, arrBuffer) == false) {
                        this.SetText("Falha na leitura\r\n");
                        return;
                    }
                    this.SetText("Sucesso na leitura\r\n");

                    if (Mem == 0x00) {
                        iTotalLen = 4;
                    }
                    else if (Mem == 0x01) {
                        iTotalLen = 12;
                    }
                    else if (Mem == 0x02) {
                        iTotalLen = 12;
                    }
                    else if (Mem == 0x03) {
                        iTotalLen = 64;
                    }

                    int iTagLength = 0;
                    int iTagNumber = 0;
                    iTagLength = iTotalLen;
                    iTagNumber = iNum;
                    int iLength = 0;
                    byte bPackLength = 0;
                    int i = 0;


                    bPackLength = arrBuffer[iLength];
                    string str2 = "";
                    string str1 = "";
                    str1 = arrBuffer[1 + iLength + 0].ToString("X2");
                    str2 = str2 + "Type:" + str1 + " ";  //Tag Type

                    str1 = arrBuffer[1 + iLength + 1].ToString("X2");
                    str2 = str2 + "Ant:" + str1 + " Tag:";  //Ant

                    string str3 = "";
                    for (i = 0; i < iTotalLen; i++) {
                        str1 = arrBuffer[iLength + i].ToString("X2");
                        str3 = str3 + str1;
                    }
                    str2 = str2 + str3;
                    str1 = arrBuffer[1 + iLength + i].ToString("X2");
                    str2 = str2 + "RSSI:" + str1 + "\r\n";  //RSSI
                    iLength = iLength + bPackLength + 1;
                    this.SetText(str3 + "\r\n");
                    txtRetorno.Text = str3.Trim();
                }
            }
        }

        private void btGravar_Click(object sender, EventArgs e) {
            // Grava dados em uma tag (operação única) ou habilita gravação automática.
            byte[] arrBuffer = new byte[4096];
            byte Mem = 0x01;
            byte WordPtr = 0x02;
            byte WriteLen = 0x06;
            byte[] Password = new byte[4];

            Mem = Convert.ToByte(cbRegiao.SelectedIndex);

            string sStartAdd = txtStart.Text;
            string sLen = txtLenght.Text;
            string sPass = txtAcessPass.Text;

            WordPtr = byte.Parse(sStartAdd);
            WriteLen = byte.Parse(sLen);


            Password[0] = byte.Parse(sPass.Substring(0, 1));
            Password[1] = byte.Parse(sPass.Substring(1, 1));
            Password[2] = byte.Parse(sPass.Substring(2, 1));
            Password[3] = byte.Parse(sPass.Substring(3, 1));

            arrBuffer = StringToByteArray(txtWriteData.Text);

            if (string.IsNullOrWhiteSpace(txtWriteData.Text)) {
                this.SetText("Falha na Gravação\r\n");
                MessageBox.Show("Escreva o dado a ser gravado!");
            }
            else if (Mem == 0x00 && txtWriteData.Text.Length < 4) {
                MessageBox.Show("Banco Reservado - Necessário 4 dígitos!");
            }
            else if (Mem == 0x01 && txtWriteData.Text.Length < 24) {
                MessageBox.Show("EPC - Necessário 24 dígitos!");
            }
            else if (Mem == 0x02) {
                MessageBox.Show("Banco TID não pode ser gravado!");
            }
            else {
                if (ckAuto.Checked) {
                    if (btGravar.Text == "Gravar") {
                        timerGravar.Enabled = true;
                        txtWriteData.Enabled = false;
                        txtAcessPass.Enabled = false;
                        txtLenght.Enabled = false;
                        txtStart.Enabled = false;
                        btLer.Enabled = false;
                        //btIniciar.Enabled = false;
                    }
                    else if (btGravar.Text == "Parar") {
                        timerGravar.Enabled = false;
                        txtWriteData.Enabled = true;
                        txtAcessPass.Enabled = true;
                        txtLenght.Enabled = true;
                        txtStart.Enabled = true;
                        btLer.Enabled = true;
                        //btIniciar.Enabled = true;
                        btGravar.Text = "Gravar";
                    }
                }
                else {
                    if (rbCom.Checked) {
                        if (RFID.CFComApi.CFCom_WriteCardG2(0xFF, Password, Mem, WordPtr, WriteLen, arrBuffer) == false) {
                            this.SetText("Falha na Gravação\r\n");
                            return;
                        }
                    }
                    else if (rbHid.Checked) {
                        CarregaModo();
                        if (cbModo.SelectedIndex == 1) {
                            MessageBox.Show("Para esta leitura mude o dispositivo HID para Modo Escravo");
                        }
                        else if (cbModo.SelectedIndex == 0) {
                            if (RFID.CFHidApi.CFHid_WriteCardG2(0xFF, Password, Mem, WordPtr, WriteLen, arrBuffer) == false) {
                                this.SetText("Falha na Gravação\r\n");
                                return;
                            }
                        }
                    }


                    this.SetText("Sucesso na Gravação\r\n");

                    if (ckIncremental.Checked) {
                        string sData = txtWriteData.Text;
                        int iValue = 0;
                        try {
                            iValue = int.Parse(sData.Substring(20));
                            iValue++;
                        }
                        catch (Exception ) {
                            iValue = 1;
                        }

                        string sValue = iValue.ToString();

                        if (sValue.Length == 1) {
                            sValue = "000" + iValue.ToString();
                        }
                        else if (sValue.Length == 2) {
                            sValue = "00" + iValue.ToString();
                        }
                        else if (sValue.Length == 3) {
                            sValue = "0" + iValue.ToString();
                        }
                        else if (sValue.Length == 4) {
                            sValue = iValue.ToString();
                        }

                        txtWriteData.Text = sData.Substring(0, 20) + sValue;
                    }

                }
            }

        }

        private void ckAuto_CheckedChanged(object sender, EventArgs e) {

        }

        private void timerGravar_Tick(object sender, EventArgs e) {
            // Manipulador para gravações automáticas repetidas quando `ckAuto` está habilitado.

            byte[] arrBuffer = new byte[4096];
            byte Mem = 0x02;
            byte WordPtr = 0x00;
            byte WriteLen = 0x06;
            byte[] Password = new byte[4];

            Mem = Convert.ToByte(cbRegiao.SelectedIndex);

            string sStartAdd = txtStart.Text;
            string sLen = txtLenght.Text;
            string sPass = txtAcessPass.Text;

            WordPtr = byte.Parse(sStartAdd);
            WriteLen = byte.Parse(sLen);


            Password[0] = byte.Parse(sPass.Substring(0, 1));
            Password[1] = byte.Parse(sPass.Substring(1, 1));
            Password[2] = byte.Parse(sPass.Substring(2, 1));
            Password[3] = byte.Parse(sPass.Substring(3, 1));

            arrBuffer = StringToByteArray(txtWriteData.Text);

            btGravar.Text = "Parar";
            if (rbCom.Checked) {
                if (RFID.CFComApi.CFCom_WriteCardG2(0xFF, Password, Mem, WordPtr, WriteLen, arrBuffer) == false) {
                    this.SetText("Falha na Gravação\r\n");
                    return;
                }
            }
            else if (rbHid.Checked) {
                CarregaModo();
                if (cbModo.SelectedIndex == 1) {
                    MessageBox.Show("Para esta leitura mude o dispositivo HID para Modo Escravo");
                }
                else if (cbModo.SelectedIndex == 0) {
                    if (RFID.CFHidApi.CFHid_WriteCardG2(0xFF, Password, Mem, WordPtr, WriteLen, arrBuffer) == false) {
                        this.SetText("Falha na Gravação\r\n");
                        return;
                    }
                }
            }


            this.SetText("Sucesso na Gravação\r\n");

            if (ckIncremental.Checked) {
                string sData = txtWriteData.Text;
                int iValue = 0;
                try {
                    iValue = int.Parse(sData.Substring(20));
                    iValue++;
                }
                catch (Exception ) {
                    iValue = 1;
                }

                string sValue = iValue.ToString();

                if (sValue.Length == 1) {
                    sValue = "000" + iValue.ToString();
                }
                else if (sValue.Length == 2) {
                    sValue = "00" + iValue.ToString();
                }
                else if (sValue.Length == 3) {
                    sValue = "0" + iValue.ToString();
                }
                else if (sValue.Length == 4) {
                    sValue = iValue.ToString();
                }

                txtWriteData.Text = sData.Substring(0, 20) + sValue;
            }
        }

        public static byte[] StringToByteArray(string hex) {
            // Converte uma string hex (sem separadores) para um array de bytes.
            return Enumerable.Range(0, hex.Length)
                             .Where(x => x % 2 == 0)
                             .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                             .ToArray();
        }

        private void timerLeitura_Tick(object sender, EventArgs e) {

            byte[] arrBuffer = new byte[4096];
            ushort iNum = 0;
            ushort iTotalLen = 0;

            if (rbCom.Checked) {
                //this.SetText("Modo Comandos\r\n");
                if (RFID.CFComApi.CFCom_InventoryG2(0xFF, arrBuffer, out iTotalLen, out iNum) == false) {
                    //this.SetText("Nenhuma tag encontrada\r\n");
                    return;
                }
            }
            else if (rbHid.Checked) {
                if (RFID.CFHidApi.CFHid_InventoryG2(0xFF, arrBuffer, out iTotalLen, out iNum) == false) {
                    //this.SetText("Nenhuma tag encontrada\r\n");
                    return;
                }
            }

            int iTagLength = 0;
            int iTagNumber = 0;
            iTagLength = iTotalLen;
            iTagNumber = iNum;
            if (iTagNumber == 0) return;
            int iIndex = 0;
            int iLength = 0;
            byte bPackLength = 0;
            int i = 0;
            string tipo = "";
            string ant = "";
            int leituras = 1;
            string tag = "";
            string rssi = "";


            for (iIndex = 0; iIndex < iTagNumber; iIndex++) {
                bPackLength = arrBuffer[iLength];
                string str2 = "";
                string str1 = "";
                str1 = arrBuffer[1 + iLength + 0].ToString("X2");
                //str2 = str2 + "Tipo:" + str1 + " ";  //Tag Type

                Console.WriteLine(arrBuffer[1 + iLength + 1].ToString("X2"));

                tipo = str1;

                str1 = arrBuffer[1 + iLength + 1].ToString("X2");
                str2 = str2 + "Ant:" + str1;  //Ant

                ant = str1;

                string str3 = "";
                for (i = 2; i < bPackLength - 1; i++) {
                    str1 = arrBuffer[1 + iLength + i].ToString("X2");
                    str3 = str3 + str1;
                }

                tag = str3;




                str2 = str2 + str3;
                str1 = arrBuffer[1 + iLength + i].ToString("X2");
                str2 = "RSSI:" + str1;  //RSSI
                iLength = iLength + bPackLength + 1;

                rssi = str2;




                for (int j = 0; j < Leitura.Rows.Count; j++) {
                    if (Leitura.Rows[j]["EPC"].ToString() == tag) {
                        leituras = Convert.ToInt32(Leitura.Rows[j]["Leituras"]) + 1;
                        Leitura.Rows.RemoveAt(j);

                    }
                }

                Leitura.Rows.Add( ant, tag, leituras);
                Leitura.AcceptChanges();

                Leitura.DefaultView.Sort = "Leituras DESC";

                dgLeituras.DataSource = Leitura;

            }
        }

        private void btLimpaDg_Click(object sender, EventArgs e) {
            // Clear the in-memory table of live readings.
            Leitura.Rows.Clear();
        }

        private void btInterface_Click(object sender, EventArgs e) {
            // Set the device transport/interface parameter (USB/other).
            RFID.CFHidApi.CFHid_ClearTagBuf();
            byte bParamAddr = 0;
            byte bValue = 0;

            /*  01: Transport
                02: WorkMode
                03: DeviceAddr
                04: FilterTime
                05: RFPower
                06: BeepEnable
                07: UartBaudRate*/
            bParamAddr = 0x01;
            //bValue = 26;   //RF = 26

            if (cbInterface.SelectedIndex == 0) {
                bValue = (byte)Convert.ToInt16(0);
            }
            else if (cbInterface.SelectedIndex == 1) {
                bValue = (byte)Convert.ToInt16(1);
            }

            if (RFID.CFHidApi.CFHid_SetDeviceOneParam(0xFF, bParamAddr, bValue) == false) {
                this.SetText("Falha ao Definir Interface\r\n");
                return;
            }
            this.SetText("Interface Definido com sucesso\r\n");
            txtLog.Focus();
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void iniciar_PostClick(object sender, EventArgs e)
        {
            // (Unused placeholder) Start/stop logic for posting reads.

            if (rbHid.Checked)
            {
                txtLog.Focus();
                RFID.CFHidApi.CFHid_ClearTagBuf();



                /*  01: Transport
                    02: WorkMode
                    03: DeviceAddr
                    04: FilterTime
                    05: RFPower
                    06: BeepEnable
                    07: UartBaudRate*/

                //bValue = 26;   //RF = 26
                CarregaInterface();
                CarregaModo();
                if (cbInterface.SelectedIndex == 1)
                {
                    MessageBox.Show("Mude para Interface USB!\r\n");
                    this.SetText("Falha\r\n");
                    return;
                }

                if (cbModo.SelectedIndex == 1)
                {
                    MessageBox.Show("Mude para Modo Escravo!\r\n");
                    this.SetText("Falha\r\n");
                    return;
                }

                if (btnIniciarLeituraPost.Text == "Iniciar")
                {
                    dgLeituras.DataSource = null;
                    dgLeituras.Rows.Clear();
                    Leitura.Rows.Clear();
                    btnIniciarLeituraPost.Text = "Parar";
                    groupBox9.Enabled = false;
                    //btLimpaDg.Enabled = false;
                }
                else if (btnIniciarLeituraPost.Text == "Parar")
                {
                    btnIniciarLeituraPost.Text = "Iniciar";
                    //btLimpaDg.Enabled = true;
                    groupBox9.Enabled = true;
                }

                timerLeitura.Enabled = !timerLeitura.Enabled;

                RFID.CFHidApi.CFHid_ClearTagBuf();

                /* MessageBox.Show("Não permitido em Modo HID\r\nVerifique o log na parte inferior para verificar leituras HID\r\n");
                 this.SetText("Modo HID");
                 btIniciar.Text = "Iniciar";
                 //btLimpaDg.Enabled = true;
                 groupBox9.Enabled = true;
                 timerLeitura.Enabled = false;
                 txtLog.Focus();*/
            }
            else if (rbCom.Checked)
            {
                RFID.CFComApi.CFCom_ClearTagBuf();

 

                /*  01: Transport
                    02: WorkMode
                    03: DeviceAddr
                    04: FilterTime
                    05: RFPower
                    06: BeepEnable
                    07: UartBaudRate*/
              
                //bValue = 26;   //RF = 26

                CarregaModo();

                if (cbModo.SelectedIndex == 1)
                {
                    MessageBox.Show("Mude para Modo Escravo!\r\n");
                    this.SetText("Falha\r\n");
                    return;
                }

                if (btnIniciarLeituraPost.Text == "Iniciar")
                {
                    dgLeituras.DataSource = null;
                    dgLeituras.Rows.Clear();
                    Leitura.Rows.Clear();
                    btnIniciarLeituraPost.Text = "Parar";
                    groupBox9.Enabled = false;
                    //btLimpaDg.Enabled = false;
                }
                else if (btnIniciarLeituraPost.Text == "Parar")
                {
                    btnIniciarLeituraPost.Text = "Iniciar";
                    //btLimpaDg.Enabled = true;
                    groupBox9.Enabled = true;
                }

                timerLeitura.Enabled = !timerLeitura.Enabled;

                RFID.CFComApi.CFCom_ClearTagBuf();
            }
        }

        private void cbRegiao_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void iniciar_post_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void label9_Click_1(object sender, EventArgs e)
        {

        }

        private void btnIniciarLeituraPost_Click(object sender, EventArgs e)
        {
            // Start/stop reading loop used specifically when posting readings.
            dgLeituras.ColumnHeadersVisible = true;
            if (rbHid.Checked)
            {
                txtLog.Focus();
                RFID.CFHidApi.CFHid_ClearTagBuf();

 
                /*  01: Transport
                    02: WorkMode
                    03: DeviceAddr
                    04: FilterTime
                    05: RFPower
                    06: BeepEnable
                    07: UartBaudRate*/

                //bValue = 26;   //RF = 26
                CarregaInterface();
                CarregaModo();
                if (cbInterface.SelectedIndex == 1)
                {
                    MessageBox.Show("Mude para Interface USB!\r\n");
                    this.SetText("Falha\r\n");
                    return;
                }

                if (cbModo.SelectedIndex == 1)
                {
                    MessageBox.Show("Mude para Modo Escravo!\r\n");
                    this.SetText("Falha\r\n");
                    return;
                }

                if (btnIniciarLeituraPost.Text == "Iniciar")
                {
                    dgLeituras.DataSource = null;
                    dgLeituras.Rows.Clear();
                    Leitura.Rows.Clear();
                    btnIniciarLeituraPost.Text = "Parar";
                    groupBox9.Enabled = false;
                    //btLimpaDg.Enabled = false;
                }
                else if (btnIniciarLeituraPost.Text == "Parar")
                {
                    btnIniciarLeituraPost.Text = "Iniciar";
                    //btLimpaDg.Enabled = true;
                    groupBox9.Enabled = true;
                }

                timerLeitura.Enabled = !timerLeitura.Enabled;

                RFID.CFHidApi.CFHid_ClearTagBuf();

                /* MessageBox.Show("Não permitido em Modo HID\r\nVerifique o log na parte inferior para verificar leituras HID\r\n");
                 this.SetText("Modo HID");
                 btIniciar.Text = "Iniciar";
                 //btLimpaDg.Enabled = true;
                 groupBox9.Enabled = true;
                 timerLeitura.Enabled = false;
                 txtLog.Focus();*/
            }
            else if (rbCom.Checked)
            {
                RFID.CFComApi.CFCom_ClearTagBuf();


                /*  01: Transport
                    02: WorkMode
                    03: DeviceAddr
                    04: FilterTime
                    05: RFPower
                    06: BeepEnable
                    07: UartBaudRate*/

                //bValue = 26;   //RF = 26

                CarregaModo();

                if (cbModo.SelectedIndex == 1)
                {
                    MessageBox.Show("Mude para Modo Escravo!\r\n");
                    this.SetText("Falha\r\n");
                    return;
                }

                if (btnIniciarLeituraPost.Text == "Iniciar")
                {
                    dgLeituras.DataSource = null;
                    dgLeituras.Rows.Clear();
                    Leitura.Rows.Clear();
                    btnIniciarLeituraPost.Text = "Parar";
                    groupBox9.Enabled = false;
                    //btLimpaDg.Enabled = false;
                }
                else if (btnIniciarLeituraPost.Text == "Parar")
                {
                    btnIniciarLeituraPost.Text = "Iniciar";
                    //btLimpaDg.Enabled = true;
                    groupBox9.Enabled = true;
                }

                timerLeitura.Enabled = !timerLeitura.Enabled;

                RFID.CFComApi.CFCom_ClearTagBuf();
            }

        }



        private async Task ExecutarPostAsync(bool modoAutomatico = false)
        {
            // Build payload from current readings, send batch POST and
            // clear buffers and UI on success. In automatic mode, minimize UI.
            var payload = SelecionarPrimeirasLeiturasPorEpc(dgLeituras).ToList();

            if (payload.Count == 0)
            {
                if (!modoAutomatico)
                    MessageBox.Show("Nenhum item para enviar.");

                return;
            }

            //  ENVIA PRIMEIRO
            bool enviado = await EnviarPostEmLoteAsync(payload, modoAutomatico);

            if (!enviado)
            {
                return;
            }

            //  LIMPA GRID
            dgLeituras.DataSource = null;
            dgLeituras.Rows.Clear();
            Leitura.Rows.Clear();

            //  LIMPA BUFFER DO LEITOR
            if (rbCom.Checked)
                RFID.CFComApi.CFCom_ClearTagBuf();

            if (rbHid.Checked)
                RFID.CFHidApi.CFHid_ClearTagBuf();

            if (!modoAutomatico)
                MessageBox.Show($"{payload.Count} item(ns) enviados.");
            

        }




        private async void btnPostar_Click(object sender, EventArgs e)
        {
            // Manual trigger to post current readings to configured endpoint.
            await ExecutarPostAsync(false);
        }

        private async void PostTimer_Tick(object sender, EventArgs e)
        {
            // Timer tick for automatic posting: stop timer while sending
            // and restart only if checkbox remains enabled.
            postTimer.Stop();

            try
            {
                await ExecutarPostAsync(true); // automático
            }
            finally
            {
                if (chbxPost.Checked)
                    postTimer.Start();
            }
        }




        private void dgLeituras_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void txtPostUrl_TextChanged(object sender, EventArgs e)
        {

        }

        private void CompanyId_Click(object sender, EventArgs e)
        {

        }

        private void txtMac_TextChanged(object sender, EventArgs e)
        {
            // Remove ":" e "-" se existirem
            string raw = txtMac.Text.Replace(":", "").Replace("-", "").ToUpper();

            // Mantém apenas hexadecimal
            raw = System.Text.RegularExpressions.Regex.Replace(raw, "[^0-9A-F]", "");

            // Limita a 12 caracteres (MAC = 12 hex)
            if (raw.Length > 12)
                raw = raw.Substring(0, 12);

            // Formata colocando ":" a cada 2 caracteres
            var formatted = new System.Text.StringBuilder();

            for (int i = 0; i < raw.Length; i++)
            {
                if (i > 0 && i % 2 == 0)
                    formatted.Append(":");

                formatted.Append(raw[i]);
            }

            txtMac.TextChanged -= txtMac_TextChanged;
            txtMac.Text = formatted.ToString();
            // move cursor para o fim após formatar
            txtMac.SelectionStart = txtMac.Text.Length;
            txtMac.TextChanged += txtMac_TextChanged;
        }

        private void txtIP_TextChanged(object sender, EventArgs e)
        {

        }

        private void tab1_DrawItem(object sender, DrawItemEventArgs e)
        {
            TabPage page = tab1.TabPages[e.Index];
            Rectangle area = e.Bounds;

            Color fundo = ColorTranslator.FromHtml("#B9D1EA");
            Color texto = ColorTranslator.FromHtml("#202020");

            using (Brush brush = new SolidBrush(fundo))
            {
                e.Graphics.FillRectangle(brush, area);
            }

            TextRenderer.DrawText(
                e.Graphics,
                page.Text,
                this.Font,
                area,
                texto,
                TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter
            );
        }

        private void btnExportarJson_Click(object sender, EventArgs e)
        {
            // Export current unique readings to a JSON file selected by user.
            try
            {
                var dados = SelecionarPrimeirasLeiturasPorEpc(dgLeituras).ToList();

                if (dados.Count == 0)
                {
                    MessageBox.Show("Nenhum item para exportar.");
                    return;
                }

                using (SaveFileDialog sfd = new SaveFileDialog())
                {
                    sfd.Filter = "Arquivo JSON (*.json)|*.json";
                    sfd.FileName = $"Leituras {DateTime.Now:dd-mm-yyyy/HH:mm}.json";

                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        string json = JsonConvert.SerializeObject(dados, Formatting.Indented);

                        File.WriteAllText(sfd.FileName, json, Encoding.UTF8);

                        MessageBox.Show("Arquivo exportado com sucesso!");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao exportar: " + ex.Message);
            }
        }


        private void ExportarParaCsv(List<RfidPostModel> dados, string caminho)
        {
            // Create a CSV file from the readings and write it with UTF8 BOM.
            var sb = new StringBuilder();

            // Cabeçalho
            sb.AppendLine("reading_reader_ip,reading_reader_mac,reading_antenna,reading_rssi,reading_epc_hex,reading_movement_type,reading_company_id");

            foreach (var item in dados)
            {
                sb.AppendLine(string.Join(",",
                    EscaparCsv(item.reading_reader_ip),
                    EscaparCsv(item.reading_reader_mac),
                    item.reading_antenna,
                    item.reading_rssi,
                    EscaparCsv(item.reading_epc_hex),
                    item.reading_movement_type,
                    item.reading_company_id
                ));
            }

            // UTF8 com BOM → Excel abre corretamente
            File.WriteAllText(caminho, sb.ToString(), new UTF8Encoding(true));
        }

        private string EscaparCsv(string valor)
        {
            if (string.IsNullOrEmpty(valor))
                return "";

            if (valor.Contains(",") || valor.Contains("\""))
                return $"\"{valor.Replace("\"", "\"\"")}\"";

            return valor;
        }

        private void btnExportarCSV_Click(object sender, EventArgs e)
        {
            // Export current unique readings to CSV using a SaveFileDialog.
            try
            {
                var dados = SelecionarPrimeirasLeiturasPorEpc(dgLeituras).ToList();

                if (dados.Count == 0)
                {
                    MessageBox.Show("Nenhum item para exportar.");
                    return;
                }

                using (SaveFileDialog sfd = new SaveFileDialog())
                {
                    sfd.Filter = "Arquivo CSV (*.csv)|*.csv";
                    sfd.FileName = $"Leituras {DateTime.Now:dd-mm-yyyy/HH:mm}.csv";

                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        ExportarParaCsv(dados, sfd.FileName);
                        MessageBox.Show("Arquivo exportado com sucesso!");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao exportar CSV: " + ex.Message);
            }
        }

        private void LimparLeituras()
        {
            // Clear UI and reader buffers of current readings.
            dgLeituras.DataSource = null;
            dgLeituras.Rows.Clear();
            Leitura.Rows.Clear();

            if (rbCom.Checked)
                RFID.CFComApi.CFCom_ClearTagBuf();

            if (rbHid.Checked)
                RFID.CFHidApi.CFHid_ClearTagBuf();
        }


        private void btnLimparGrid_Click(object sender, EventArgs e)
        {
            if (dgLeituras.Rows.Count == 0)
                return;


                LimparLeituras();
            
        }

        private void TimerPost_SelectedIndexChanged(object sender, EventArgs e)
        {
            TimerPost.SelectedItem = 1;
        }
    }
}
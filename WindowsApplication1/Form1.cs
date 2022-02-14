using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using RFID;
using System.Runtime.InteropServices;
using System.IO.Ports;
using System.Linq;
using ViaondaRFID_MID10S_DEMO;

namespace WindowsApplication1 {
    public partial class Form1 : Form {
        DataTable Leitura = new DataTable();

        public Form1() {
            InitializeComponent();
            byte[] arrBuffer = new byte[256];
        }



        delegate void SetTextCallback(string text);
        private void SetText(string text) {
            if (this.txtLog.InvokeRequired) {
                SetTextCallback d = new SetTextCallback(SetText);
                this.Invoke(d, new object[] { text });
            }
            else {
                this.txtLog.Text = this.txtLog.Text + text;
                this.txtLog.SelectionStart = this.txtLog.Text.Length;
                this.txtLog.ScrollToCaret();
            }
        }






        private void Form1_Load(object sender, EventArgs e) {
            Block();
            rbCom.Checked = true;
            cbRegiao.SelectedIndex = 0;
            tabelaLeitura();
        }

        private void tabelaLeitura() {
            Leitura.Columns.Add("Tipo");
            Leitura.Columns.Add("Ant");
            Leitura.Columns.Add("EPC");
            Leitura.Columns.Add("Leituras");
            Leitura.Columns.Add("RSSI");
        }

        private void Block() {
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
            btIniciar.Enabled = false;
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
            dgLeitura.Enabled = false;
            btLimpaDg.Enabled = false;
            cbFreq.Enabled = false;
            btSetFreq.Enabled = false;
            btGetFreq.Enabled = false;
            cbInterface.Enabled = false;
            btInterface.Enabled = false;
            cbPorta.Enabled = true;
        }

        private void Unblock() {
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
            btIniciar.Enabled = true;
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
            dgLeitura.Enabled = true;
            btLimpaDg.Enabled = true;
            cbFreq.Enabled = true;
            btSetFreq.Enabled = true;
            btGetFreq.Enabled = true;
            cbInterface.Enabled = true;
            btInterface.Enabled = true;
            cbPorta.Enabled = false;
        }

        private void btConectar_Click(object sender, EventArgs e) {
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
                    this.SetText(str);
                    str = "SN:";
                    for (int i = 0; i < 7; i++) {
                        str1 = String.Format("{0:X2}", arrBuffer[2 + i]);
                        str = str + str1;
                    }
                    str = str + "\r\n";
                    this.SetText(str);
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
                for (int i = 0; i < 7; i++) {
                    str1 = String.Format("{0:X2}", arrBuffer[2 + i]);
                    str = str + str1;
                }
                str = str + "\r\n";
                this.SetText(str);
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


            if (bFreq[0] == 0x31 && bFreq[1] == 0x80) {
                cbFreq.SelectedIndex = 0;
            }
            else if (bFreq[0] == 0x4E && bFreq[1] == 0x00) {
                cbFreq.SelectedIndex = 1;
            }
            else if (bFreq[0] == 0x2C && bFreq[1] == 0xA3) {
                cbFreq.SelectedIndex = 2;
            }
            else if (bFreq[0] == 0x29 && bFreq[1] == 0x9D) {
                cbFreq.SelectedIndex = 3;
            }
            else if (bFreq[0] == 0x2E && bFreq[1] == 0x9F) {
                cbFreq.SelectedIndex = 4;
            }
            else if (bFreq[0] == 0x2C && bFreq[1] == 0x81) {
                cbFreq.SelectedIndex = 5;
            }
            else if (bFreq[0] == 0x31 && bFreq[1] == 0xA7) {
                cbFreq.SelectedIndex = 6;
            }
            else if (bFreq[0] == 0x31 && bFreq[1] == 0x99) {
                cbFreq.SelectedIndex = 7;
            }
            else if (bFreq[0] == 0x1C && bFreq[1] == 0x99) {
                cbFreq.SelectedIndex = 8;
            }
            else if (bFreq[0] == 0x24 && bFreq[1] == 0x9D) {
                cbFreq.SelectedIndex = 9;
            }
            else if (bFreq[0] == 0x28 && bFreq[1] == 0xA1) {
                cbFreq.SelectedIndex = 10;
            }

            this.SetText("Sucesso ao Obter Frequência\r\n");
        }


        private void btGetPotencia_Click(object sender, EventArgs e) {
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
            byte[] bFreq = new byte[2];

            switch (cbFreq.SelectedIndex) {
                case 0:
                    bFreq[0] = 0x31;
                    bFreq[1] = 0x80;
                    break;
                case 1:
                    bFreq[0] = 0x4E;
                    bFreq[1] = 0x00;
                    break;
                case 2:
                    bFreq[0] = 0x2C;
                    bFreq[1] = 0xA3;
                    break;
                case 3:
                    bFreq[0] = 0x29;
                    bFreq[1] = 0x9D;
                    break;
                case 4:
                    bFreq[0] = 0x2E;
                    bFreq[1] = 0x9F;
                    break;
                case 5:
                    bFreq[0] = 0x2C;
                    bFreq[1] = 0x81;
                    break;
                case 6:
                    bFreq[0] = 0x31;
                    bFreq[1] = 0xA7;
                    break;
                case 7:
                    bFreq[0] = 0x31;
                    bFreq[1] = 0x99;
                    break;
                case 8:
                    bFreq[0] = 0x1C;
                    bFreq[1] = 0x99;
                    break;
                case 9:
                    bFreq[0] = 0x24;
                    bFreq[1] = 0x9D;
                    break;
                case 10:
                    bFreq[0] = 0x28;
                    bFreq[1] = 0xA1;
                    break;

            }

            if (rbCom.Checked) {
                if (RFID.CFComApi.CFCom_SetFreq(0xFF, bFreq) == false) {
                    this.SetText("Falha ao Definir Frequência\r\n");
                    return;
                }
            }
            else if (rbHid.Checked) {
                if (RFID.CFHidApi.CFHid_SetFreq(0xFF, bFreq) == false) {
                    this.SetText("Falha ao Definir Frequência\r\n");
                    return;
                }
                txtLog.Focus();
            }
            this.SetText("Sucesso ao Definir Frequência\r\n");

        }



        private void btLimpar_Click(object sender, EventArgs e) {
            txtLog.Text = "";
        }

        private void btGetFreq_Click(object sender, EventArgs e) {
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

            if (bFreq[0] == 0x31 && bFreq[1] == 0x80) {
                cbFreq.SelectedIndex = 0;
            }
            else if (bFreq[0] == 0x4E && bFreq[1] == 0x00) {
                cbFreq.SelectedIndex = 1;
            }
            else if (bFreq[0] == 0x2C && bFreq[1] == 0xA3) {
                cbFreq.SelectedIndex = 2;
            }
            else if (bFreq[0] == 0x29 && bFreq[1] == 0x9D) {
                cbFreq.SelectedIndex = 3;
            }
            else if (bFreq[0] == 0x2E && bFreq[1] == 0x9F) {
                cbFreq.SelectedIndex = 4;
            }
            else if (bFreq[0] == 0x2C && bFreq[1] == 0x81) {
                cbFreq.SelectedIndex = 5;
            }
            else if (bFreq[0] == 0x31 && bFreq[1] == 0xA7) {
                cbFreq.SelectedIndex = 6;
            }
            else if (bFreq[0] == 0x31 && bFreq[1] == 0x99) {
                cbFreq.SelectedIndex = 7;
            }
            else if (bFreq[0] == 0x1C && bFreq[1] == 0x99) {
                cbFreq.SelectedIndex = 8;
            }
            else if (bFreq[0] == 0x24 && bFreq[1] == 0x9D) {
                cbFreq.SelectedIndex = 9;
            }
            else if (bFreq[0] == 0x28 && bFreq[1] == 0xA1) {
                cbFreq.SelectedIndex = 10;
            }

            this.SetText("Sucesso ao Obter Frequência\r\n");
        }

        private void btIniciar_Click(object sender, EventArgs e) {

            if (rbHid.Checked) {
                txtLog.Focus();
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

                if (btIniciar.Text == "Iniciar") {
                    dgLeitura.DataSource = null;
                    dgLeitura.Rows.Clear();
                    Leitura.Rows.Clear();
                    btIniciar.Text = "Parar";
                    groupBox9.Enabled = false;
                    btLimpaDg.Enabled = false;
                }
                else if (btIniciar.Text == "Parar") {
                    btIniciar.Text = "Iniciar";
                    btLimpaDg.Enabled = true;
                    groupBox9.Enabled = true;
                }

                timerLeitura.Enabled = !timerLeitura.Enabled;

                RFID.CFHidApi.CFHid_ClearTagBuf();

                /* MessageBox.Show("Não permitido em Modo HID\r\nVerifique o log na parte inferior para verificar leituras HID\r\n");
                 this.SetText("Modo HID");
                 btIniciar.Text = "Iniciar";
                 btLimpaDg.Enabled = true;
                 groupBox9.Enabled = true;
                 timerLeitura.Enabled = false;
                 txtLog.Focus();*/
            }
            else if (rbCom.Checked) {
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

                CarregaModo();

                if (cbModo.SelectedIndex == 1) {
                    MessageBox.Show("Mude para Modo Escravo!\r\n");
                    this.SetText("Falha\r\n");
                    return;
                }

                if (btIniciar.Text == "Iniciar") {
                    dgLeitura.DataSource = null;
                    dgLeitura.Rows.Clear();
                    Leitura.Rows.Clear();
                    btIniciar.Text = "Parar";
                    groupBox9.Enabled = false;
                    btLimpaDg.Enabled = false;
                }
                else if (btIniciar.Text == "Parar") {
                    btIniciar.Text = "Iniciar";
                    btLimpaDg.Enabled = true;
                    groupBox9.Enabled = true;
                }

                timerLeitura.Enabled = !timerLeitura.Enabled;

                RFID.CFComApi.CFCom_ClearTagBuf();
            }
        }

        private void btLer_Click(object sender, EventArgs e) {
            byte[] arrBuffer = new byte[4096];
            byte Mem = 0x02;
            byte WordPtr = 0x00;
            byte ReadEPClen = 0x06;
            ushort iNum = 0;
            ushort iTotalLen = 0;
            byte[] Password = new byte[4];

            string retorno = "";

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
                int iIndex = 0;
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
                    int iIndex = 0;
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
                    int iIndex = 0;
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
                        btIniciar.Enabled = false;
                    }
                    else if (btGravar.Text == "Parar") {
                        timerGravar.Enabled = false;
                        txtWriteData.Enabled = true;
                        txtAcessPass.Enabled = true;
                        txtLenght.Enabled = true;
                        txtStart.Enabled = true;
                        btLer.Enabled = true;
                        btIniciar.Enabled = true;
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
                        catch (Exception ex) {
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
                catch (Exception ex) {
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
                str2 = str2 + "Tipo:" + str1 + " ";  //Tag Type

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

                Leitura.Rows.Add(tipo, ant, tag, leituras, rssi);
                Leitura.AcceptChanges();

                Leitura.DefaultView.Sort = "Leituras DESC";

                dgLeitura.DataSource = Leitura;

            }
        }

        private void btLimpaDg_Click(object sender, EventArgs e) {
            dgLeitura.DataSource = null;
            dgLeitura.Rows.Clear();
            Leitura.Rows.Clear();
        }

        private void btInterface_Click(object sender, EventArgs e) {
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
    }
}
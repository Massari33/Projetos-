namespace WindowsApplication1
{
    partial class Form1
    {
        /// <summary>
        /// 
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows

        /// <summary>
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.btConectar = new System.Windows.Forms.Button();
            this.cbPorta = new System.Windows.Forms.ComboBox();
            this.btDesconectar = new System.Windows.Forms.Button();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btReload = new System.Windows.Forms.Button();
            this.rbHid = new System.Windows.Forms.RadioButton();
            this.rbCom = new System.Windows.Forms.RadioButton();
            this.btGetPotencia = new System.Windows.Forms.Button();
            this.btSetPotencia = new System.Windows.Forms.Button();
            this.btGpioOn = new System.Windows.Forms.Button();
            this.cbPotencia = new System.Windows.Forms.ComboBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.timerLeitura = new System.Windows.Forms.Timer(this.components);
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tbCon = new System.Windows.Forms.TabPage();
            this.groupBox10 = new System.Windows.Forms.GroupBox();
            this.btGetFreq = new System.Windows.Forms.Button();
            this.cbFreq = new System.Windows.Forms.ComboBox();
            this.btSetFreq = new System.Windows.Forms.Button();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.btBeepOn = new System.Windows.Forms.Button();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.btModo = new System.Windows.Forms.Button();
            this.cbModo = new System.Windows.Forms.ComboBox();
            this.tbRead = new System.Windows.Forms.TabPage();
            this.btLimpaDg = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox9 = new System.Windows.Forms.GroupBox();
            this.ckAuto = new System.Windows.Forms.CheckBox();
            this.ckIncremental = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.btLer = new System.Windows.Forms.Button();
            this.txtRetorno = new System.Windows.Forms.TextBox();
            this.btGravar = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.txtWriteData = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtAcessPass = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtLenght = new System.Windows.Forms.TextBox();
            this.txtStart = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cbRegiao = new System.Windows.Forms.ComboBox();
            this.btIniciar = new System.Windows.Forms.Button();
            this.dgLeitura = new System.Windows.Forms.DataGridView();
            this.btLimpar = new System.Windows.Forms.Button();
            this.timerGravar = new System.Windows.Forms.Timer(this.components);
            this.gbInt = new System.Windows.Forms.GroupBox();
            this.cbInterface = new System.Windows.Forms.ComboBox();
            this.btInterface = new System.Windows.Forms.Button();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tbCon.SuspendLayout();
            this.groupBox10.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.tbRead.SuspendLayout();
            this.groupBox9.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgLeitura)).BeginInit();
            this.gbInt.SuspendLayout();
            this.SuspendLayout();
            // 
            // btConectar
            // 
            this.btConectar.Location = new System.Drawing.Point(184, 19);
            this.btConectar.Name = "btConectar";
            this.btConectar.Size = new System.Drawing.Size(78, 56);
            this.btConectar.TabIndex = 0;
            this.btConectar.Text = "Conectar";
            this.btConectar.UseVisualStyleBackColor = true;
            this.btConectar.Click += new System.EventHandler(this.btConectar_Click);
            // 
            // cbPorta
            // 
            this.cbPorta.AutoCompleteCustomSource.AddRange(new string[] {
            "COM1",
            "COM2"});
            this.cbPorta.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPorta.FormattingEnabled = true;
            this.cbPorta.Location = new System.Drawing.Point(9, 55);
            this.cbPorta.Name = "cbPorta";
            this.cbPorta.Size = new System.Drawing.Size(155, 21);
            this.cbPorta.TabIndex = 1;
            this.cbPorta.Tag = "";
            // 
            // btDesconectar
            // 
            this.btDesconectar.Location = new System.Drawing.Point(268, 19);
            this.btDesconectar.Name = "btDesconectar";
            this.btDesconectar.Size = new System.Drawing.Size(76, 56);
            this.btDesconectar.TabIndex = 2;
            this.btDesconectar.Text = "Desconectar";
            this.btDesconectar.UseVisualStyleBackColor = true;
            this.btDesconectar.Click += new System.EventHandler(this.btDesconectar_Click);
            // 
            // txtLog
            // 
            this.txtLog.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.txtLog.Location = new System.Drawing.Point(0, 448);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtLog.Size = new System.Drawing.Size(458, 83);
            this.txtLog.TabIndex = 5;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btReload);
            this.groupBox3.Controls.Add(this.rbHid);
            this.groupBox3.Controls.Add(this.rbCom);
            this.groupBox3.Controls.Add(this.btConectar);
            this.groupBox3.Controls.Add(this.btDesconectar);
            this.groupBox3.Controls.Add(this.cbPorta);
            this.groupBox3.Location = new System.Drawing.Point(6, 6);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(434, 91);
            this.groupBox3.TabIndex = 14;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Conexão";
            // 
            // btReload
            // 
            this.btReload.Location = new System.Drawing.Point(350, 19);
            this.btReload.Name = "btReload";
            this.btReload.Size = new System.Drawing.Size(78, 56);
            this.btReload.TabIndex = 8;
            this.btReload.Text = "Atualizar Portas";
            this.btReload.UseVisualStyleBackColor = true;
            this.btReload.Click += new System.EventHandler(this.btReload_Click);
            // 
            // rbHid
            // 
            this.rbHid.AutoSize = true;
            this.rbHid.Location = new System.Drawing.Point(120, 20);
            this.rbHid.Name = "rbHid";
            this.rbHid.Size = new System.Drawing.Size(44, 17);
            this.rbHid.TabIndex = 7;
            this.rbHid.TabStop = true;
            this.rbHid.Text = "HID";
            this.rbHid.UseVisualStyleBackColor = true;
            this.rbHid.CheckedChanged += new System.EventHandler(this.rbHid_CheckedChanged);
            // 
            // rbCom
            // 
            this.rbCom.AutoSize = true;
            this.rbCom.Location = new System.Drawing.Point(9, 20);
            this.rbCom.Name = "rbCom";
            this.rbCom.Size = new System.Drawing.Size(49, 17);
            this.rbCom.TabIndex = 6;
            this.rbCom.TabStop = true;
            this.rbCom.Text = "COM";
            this.rbCom.UseVisualStyleBackColor = true;
            this.rbCom.CheckedChanged += new System.EventHandler(this.rbCom_CheckedChanged);
            // 
            // btGetPotencia
            // 
            this.btGetPotencia.Location = new System.Drawing.Point(174, 17);
            this.btGetPotencia.Name = "btGetPotencia";
            this.btGetPotencia.Size = new System.Drawing.Size(76, 56);
            this.btGetPotencia.TabIndex = 24;
            this.btGetPotencia.Text = "Obter";
            this.btGetPotencia.UseVisualStyleBackColor = true;
            this.btGetPotencia.Click += new System.EventHandler(this.btGetPotencia_Click);
            // 
            // btSetPotencia
            // 
            this.btSetPotencia.Location = new System.Drawing.Point(90, 17);
            this.btSetPotencia.Name = "btSetPotencia";
            this.btSetPotencia.Size = new System.Drawing.Size(76, 56);
            this.btSetPotencia.TabIndex = 25;
            this.btSetPotencia.Text = "Definir";
            this.btSetPotencia.UseVisualStyleBackColor = true;
            this.btSetPotencia.Click += new System.EventHandler(this.btSetPotencia_Click);
            // 
            // btGpioOn
            // 
            this.btGpioOn.Location = new System.Drawing.Point(6, 24);
            this.btGpioOn.Name = "btGpioOn";
            this.btGpioOn.Size = new System.Drawing.Size(157, 52);
            this.btGpioOn.TabIndex = 26;
            this.btGpioOn.Text = "Desligado";
            this.btGpioOn.UseVisualStyleBackColor = true;
            this.btGpioOn.Click += new System.EventHandler(this.btGpioOn_Click);
            // 
            // cbPotencia
            // 
            this.cbPotencia.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPotencia.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.cbPotencia.FormattingEnabled = true;
            this.cbPotencia.Items.AddRange(new object[] {
            "0",
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17"});
            this.cbPotencia.Location = new System.Drawing.Point(6, 36);
            this.cbPotencia.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cbPotencia.Name = "cbPotencia";
            this.cbPotencia.Size = new System.Drawing.Size(78, 21);
            this.cbPotencia.TabIndex = 28;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.cbPotencia);
            this.groupBox4.Controls.Add(this.btSetPotencia);
            this.groupBox4.Controls.Add(this.btGetPotencia);
            this.groupBox4.Location = new System.Drawing.Point(6, 200);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(256, 83);
            this.groupBox4.TabIndex = 29;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Potência";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.btGpioOn);
            this.groupBox5.Location = new System.Drawing.Point(268, 103);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(172, 94);
            this.groupBox5.TabIndex = 30;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Controle do Relê (GPIO)";
            // 
            // timerLeitura
            // 
            this.timerLeitura.Interval = 1;
            this.timerLeitura.Tick += new System.EventHandler(this.timerLeitura_Tick);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tbCon);
            this.tabControl1.Controls.Add(this.tbRead);
            this.tabControl1.Location = new System.Drawing.Point(3, 3);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(454, 403);
            this.tabControl1.TabIndex = 32;
            // 
            // tbCon
            // 
            this.tbCon.Controls.Add(this.gbInt);
            this.tbCon.Controls.Add(this.groupBox10);
            this.tbCon.Controls.Add(this.groupBox8);
            this.tbCon.Controls.Add(this.groupBox7);
            this.tbCon.Controls.Add(this.groupBox3);
            this.tbCon.Controls.Add(this.groupBox4);
            this.tbCon.Controls.Add(this.groupBox5);
            this.tbCon.Location = new System.Drawing.Point(4, 22);
            this.tbCon.Name = "tbCon";
            this.tbCon.Padding = new System.Windows.Forms.Padding(3);
            this.tbCon.Size = new System.Drawing.Size(446, 377);
            this.tbCon.TabIndex = 0;
            this.tbCon.Text = "Conexão/Parâmetros";
            this.tbCon.UseVisualStyleBackColor = true;
            // 
            // groupBox10
            // 
            this.groupBox10.Controls.Add(this.btGetFreq);
            this.groupBox10.Controls.Add(this.cbFreq);
            this.groupBox10.Controls.Add(this.btSetFreq);
            this.groupBox10.Location = new System.Drawing.Point(6, 288);
            this.groupBox10.Name = "groupBox10";
            this.groupBox10.Size = new System.Drawing.Size(256, 83);
            this.groupBox10.TabIndex = 30;
            this.groupBox10.TabStop = false;
            this.groupBox10.Text = "Frequência";
            // 
            // btGetFreq
            // 
            this.btGetFreq.Location = new System.Drawing.Point(172, 48);
            this.btGetFreq.Name = "btGetFreq";
            this.btGetFreq.Size = new System.Drawing.Size(76, 25);
            this.btGetFreq.TabIndex = 29;
            this.btGetFreq.Text = "Obter";
            this.btGetFreq.UseVisualStyleBackColor = true;
            this.btGetFreq.Click += new System.EventHandler(this.btGetFreq_Click);
            // 
            // cbFreq
            // 
            this.cbFreq.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFreq.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.cbFreq.FormattingEnabled = true;
            this.cbFreq.Items.AddRange(new object[] {
            "EUA/Canadá/México",
            "Europa/Nova Zelândia/Índia",
            "China/Hong Kong/Tailândia",
            "Coréia/Japão",
            "Australia",
            "Singapura",
            "Taiwan",
            "Brazil",
            "Israel",
            "África do Sul",
            "Malásia"});
            this.cbFreq.Location = new System.Drawing.Point(6, 20);
            this.cbFreq.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cbFreq.Name = "cbFreq";
            this.cbFreq.Size = new System.Drawing.Size(242, 21);
            this.cbFreq.TabIndex = 28;
            // 
            // btSetFreq
            // 
            this.btSetFreq.Location = new System.Drawing.Point(6, 48);
            this.btSetFreq.Name = "btSetFreq";
            this.btSetFreq.Size = new System.Drawing.Size(76, 25);
            this.btSetFreq.TabIndex = 25;
            this.btSetFreq.Text = "Definir";
            this.btSetFreq.UseVisualStyleBackColor = true;
            this.btSetFreq.Click += new System.EventHandler(this.btSetFreq_Click);
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.btBeepOn);
            this.groupBox8.Location = new System.Drawing.Point(267, 200);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(173, 83);
            this.groupBox8.TabIndex = 32;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "Beep";
            // 
            // btBeepOn
            // 
            this.btBeepOn.Location = new System.Drawing.Point(6, 19);
            this.btBeepOn.Name = "btBeepOn";
            this.btBeepOn.Size = new System.Drawing.Size(158, 53);
            this.btBeepOn.TabIndex = 28;
            this.btBeepOn.Text = "Ligado";
            this.btBeepOn.UseVisualStyleBackColor = true;
            this.btBeepOn.Click += new System.EventHandler(this.btBeepOn_Click);
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.btModo);
            this.groupBox7.Controls.Add(this.cbModo);
            this.groupBox7.Location = new System.Drawing.Point(6, 103);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(256, 94);
            this.groupBox7.TabIndex = 31;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Modo de trabalho";
            // 
            // btModo
            // 
            this.btModo.Location = new System.Drawing.Point(174, 19);
            this.btModo.Name = "btModo";
            this.btModo.Size = new System.Drawing.Size(76, 57);
            this.btModo.TabIndex = 1;
            this.btModo.Text = "Definir";
            this.btModo.UseVisualStyleBackColor = true;
            this.btModo.Click += new System.EventHandler(this.btModo_Click);
            // 
            // cbModo
            // 
            this.cbModo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbModo.FormattingEnabled = true;
            this.cbModo.Items.AddRange(new object[] {
            "Modo Escravo",
            "Modo Autonômo"});
            this.cbModo.Location = new System.Drawing.Point(9, 38);
            this.cbModo.Name = "cbModo";
            this.cbModo.Size = new System.Drawing.Size(135, 21);
            this.cbModo.TabIndex = 0;
            // 
            // tbRead
            // 
            this.tbRead.Controls.Add(this.btLimpaDg);
            this.tbRead.Controls.Add(this.label8);
            this.tbRead.Controls.Add(this.label7);
            this.tbRead.Controls.Add(this.groupBox9);
            this.tbRead.Controls.Add(this.btIniciar);
            this.tbRead.Controls.Add(this.dgLeitura);
            this.tbRead.Location = new System.Drawing.Point(4, 22);
            this.tbRead.Name = "tbRead";
            this.tbRead.Padding = new System.Windows.Forms.Padding(3);
            this.tbRead.Size = new System.Drawing.Size(446, 377);
            this.tbRead.TabIndex = 1;
            this.tbRead.Text = "Leitura/Gravação";
            this.tbRead.UseVisualStyleBackColor = true;
            // 
            // btLimpaDg
            // 
            this.btLimpaDg.Location = new System.Drawing.Point(364, 10);
            this.btLimpaDg.Name = "btLimpaDg";
            this.btLimpaDg.Size = new System.Drawing.Size(75, 25);
            this.btLimpaDg.TabIndex = 7;
            this.btLimpaDg.Text = "Limpar";
            this.btLimpaDg.UseVisualStyleBackColor = true;
            this.btLimpaDg.Click += new System.EventHandler(this.btLimpaDg_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(200, 10);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(18, 20);
            this.label8.TabIndex = 4;
            this.label8.Text = "0";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(118, 15);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(76, 13);
            this.label7.TabIndex = 3;
            this.label7.Text = "Total de Tags:";
            // 
            // groupBox9
            // 
            this.groupBox9.Controls.Add(this.ckAuto);
            this.groupBox9.Controls.Add(this.ckIncremental);
            this.groupBox9.Controls.Add(this.label6);
            this.groupBox9.Controls.Add(this.btLer);
            this.groupBox9.Controls.Add(this.txtRetorno);
            this.groupBox9.Controls.Add(this.btGravar);
            this.groupBox9.Controls.Add(this.label5);
            this.groupBox9.Controls.Add(this.txtWriteData);
            this.groupBox9.Controls.Add(this.label4);
            this.groupBox9.Controls.Add(this.txtAcessPass);
            this.groupBox9.Controls.Add(this.label3);
            this.groupBox9.Controls.Add(this.txtLenght);
            this.groupBox9.Controls.Add(this.txtStart);
            this.groupBox9.Controls.Add(this.label2);
            this.groupBox9.Controls.Add(this.label1);
            this.groupBox9.Controls.Add(this.cbRegiao);
            this.groupBox9.Location = new System.Drawing.Point(6, 175);
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.Size = new System.Drawing.Size(433, 196);
            this.groupBox9.TabIndex = 2;
            this.groupBox9.TabStop = false;
            this.groupBox9.Text = "Ler/Gravar";
            // 
            // ckAuto
            // 
            this.ckAuto.AutoSize = true;
            this.ckAuto.Location = new System.Drawing.Point(219, 168);
            this.ckAuto.Name = "ckAuto";
            this.ckAuto.Size = new System.Drawing.Size(98, 17);
            this.ckAuto.TabIndex = 15;
            this.ckAuto.Text = "Auto Gravação";
            this.ckAuto.UseVisualStyleBackColor = true;
            this.ckAuto.CheckedChanged += new System.EventHandler(this.ckAuto_CheckedChanged);
            // 
            // ckIncremental
            // 
            this.ckIncremental.AutoSize = true;
            this.ckIncremental.Location = new System.Drawing.Point(115, 169);
            this.ckIncremental.Name = "ckIncremental";
            this.ckIncremental.Size = new System.Drawing.Size(81, 17);
            this.ckIncremental.TabIndex = 14;
            this.ckIncremental.Text = "Incremental";
            this.ckIncremental.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(36, 107);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(73, 13);
            this.label6.TabIndex = 13;
            this.label6.Text = "Retorno(Hex):";
            // 
            // btLer
            // 
            this.btLer.Location = new System.Drawing.Point(323, 77);
            this.btLer.Name = "btLer";
            this.btLer.Size = new System.Drawing.Size(75, 48);
            this.btLer.TabIndex = 12;
            this.btLer.Text = "Ler";
            this.btLer.UseVisualStyleBackColor = true;
            this.btLer.Click += new System.EventHandler(this.btLer_Click);
            // 
            // txtRetorno
            // 
            this.txtRetorno.Location = new System.Drawing.Point(115, 104);
            this.txtRetorno.Name = "txtRetorno";
            this.txtRetorno.Size = new System.Drawing.Size(202, 20);
            this.txtRetorno.TabIndex = 11;
            // 
            // btGravar
            // 
            this.btGravar.Location = new System.Drawing.Point(323, 140);
            this.btGravar.Name = "btGravar";
            this.btGravar.Size = new System.Drawing.Size(75, 50);
            this.btGravar.TabIndex = 10;
            this.btGravar.Text = "Gravar";
            this.btGravar.UseVisualStyleBackColor = true;
            this.btGravar.Click += new System.EventHandler(this.btGravar_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(48, 145);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(61, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Dado(Hex):";
            // 
            // txtWriteData
            // 
            this.txtWriteData.Location = new System.Drawing.Point(115, 142);
            this.txtWriteData.Name = "txtWriteData";
            this.txtWriteData.Size = new System.Drawing.Size(202, 20);
            this.txtWriteData.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(5, 77);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(104, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Senha Acesso(Hex):";
            // 
            // txtAcessPass
            // 
            this.txtAcessPass.Location = new System.Drawing.Point(115, 74);
            this.txtAcessPass.MaxLength = 4;
            this.txtAcessPass.Name = "txtAcessPass";
            this.txtAcessPass.Size = new System.Drawing.Size(100, 20);
            this.txtAcessPass.TabIndex = 6;
            this.txtAcessPass.Text = "0000";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(232, 48);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(96, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Comprimento(Hex):";
            // 
            // txtLenght
            // 
            this.txtLenght.Location = new System.Drawing.Point(334, 45);
            this.txtLenght.MaxLength = 2;
            this.txtLenght.Name = "txtLenght";
            this.txtLenght.Size = new System.Drawing.Size(64, 20);
            this.txtLenght.TabIndex = 4;
            this.txtLenght.Text = "06";
            // 
            // txtStart
            // 
            this.txtStart.Location = new System.Drawing.Point(115, 45);
            this.txtStart.MaxLength = 2;
            this.txtStart.Name = "txtStart";
            this.txtStart.Size = new System.Drawing.Size(100, 20);
            this.txtStart.TabIndex = 3;
            this.txtStart.Text = "02";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(22, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(87, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "End. Inicial(Hex):";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(65, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Região:";
            // 
            // cbRegiao
            // 
            this.cbRegiao.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbRegiao.FormattingEnabled = true;
            this.cbRegiao.Items.AddRange(new object[] {
            "Reservado",
            "EPC",
            "TID",
            "Usuário"});
            this.cbRegiao.Location = new System.Drawing.Point(115, 18);
            this.cbRegiao.Name = "cbRegiao";
            this.cbRegiao.Size = new System.Drawing.Size(145, 21);
            this.cbRegiao.TabIndex = 0;
            // 
            // btIniciar
            // 
            this.btIniciar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btIniciar.Location = new System.Drawing.Point(6, 6);
            this.btIniciar.Name = "btIniciar";
            this.btIniciar.Size = new System.Drawing.Size(103, 32);
            this.btIniciar.TabIndex = 1;
            this.btIniciar.Text = "Iniciar";
            this.btIniciar.UseVisualStyleBackColor = true;
            this.btIniciar.Click += new System.EventHandler(this.btIniciar_Click);
            // 
            // dgLeitura
            // 
            this.dgLeitura.AllowUserToAddRows = false;
            this.dgLeitura.AllowUserToDeleteRows = false;
            this.dgLeitura.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgLeitura.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgLeitura.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgLeitura.Location = new System.Drawing.Point(6, 41);
            this.dgLeitura.MultiSelect = false;
            this.dgLeitura.Name = "dgLeitura";
            this.dgLeitura.ReadOnly = true;
            this.dgLeitura.RowHeadersVisible = false;
            this.dgLeitura.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgLeitura.Size = new System.Drawing.Size(433, 127);
            this.dgLeitura.TabIndex = 0;
            // 
            // btLimpar
            // 
            this.btLimpar.Location = new System.Drawing.Point(7, 413);
            this.btLimpar.Name = "btLimpar";
            this.btLimpar.Size = new System.Drawing.Size(75, 23);
            this.btLimpar.TabIndex = 33;
            this.btLimpar.Text = "Limpar";
            this.btLimpar.UseVisualStyleBackColor = true;
            this.btLimpar.Click += new System.EventHandler(this.btLimpar_Click);
            // 
            // timerGravar
            // 
            this.timerGravar.Interval = 3000;
            this.timerGravar.Tick += new System.EventHandler(this.timerGravar_Tick);
            // 
            // gbInt
            // 
            this.gbInt.Controls.Add(this.btInterface);
            this.gbInt.Controls.Add(this.cbInterface);
            this.gbInt.Location = new System.Drawing.Point(269, 290);
            this.gbInt.Name = "gbInt";
            this.gbInt.Size = new System.Drawing.Size(171, 81);
            this.gbInt.TabIndex = 33;
            this.gbInt.TabStop = false;
            this.gbInt.Text = "Interface (Apenas HID)";
            // 
            // cbInterface
            // 
            this.cbInterface.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbInterface.FormattingEnabled = true;
            this.cbInterface.Items.AddRange(new object[] {
            "USB",
            "Teclado"});
            this.cbInterface.Location = new System.Drawing.Point(7, 19);
            this.cbInterface.Name = "cbInterface";
            this.cbInterface.Size = new System.Drawing.Size(155, 21);
            this.cbInterface.TabIndex = 0;
            // 
            // btInterface
            // 
            this.btInterface.Location = new System.Drawing.Point(86, 46);
            this.btInterface.Name = "btInterface";
            this.btInterface.Size = new System.Drawing.Size(76, 25);
            this.btInterface.TabIndex = 30;
            this.btInterface.Text = "Definir";
            this.btInterface.UseVisualStyleBackColor = true;
            this.btInterface.Click += new System.EventHandler(this.btInterface_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(458, 531);
            this.Controls.Add(this.btLimpar);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.txtLog);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Viaonda M-ID10S";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tbCon.ResumeLayout(false);
            this.groupBox10.ResumeLayout(false);
            this.groupBox8.ResumeLayout(false);
            this.groupBox7.ResumeLayout(false);
            this.tbRead.ResumeLayout(false);
            this.tbRead.PerformLayout();
            this.groupBox9.ResumeLayout(false);
            this.groupBox9.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgLeitura)).EndInit();
            this.gbInt.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btConectar;
        private System.Windows.Forms.ComboBox cbPorta;
        private System.Windows.Forms.Button btDesconectar;
        private System.Windows.Forms.TextBox txtLog;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btGetPotencia;
        private System.Windows.Forms.Button btSetPotencia;
        private System.Windows.Forms.Button btGpioOn;
        private System.Windows.Forms.ComboBox cbPotencia;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Timer timerLeitura;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tbCon;
        private System.Windows.Forms.TabPage tbRead;
        private System.Windows.Forms.RadioButton rbHid;
        private System.Windows.Forms.RadioButton rbCom;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.ComboBox cbModo;
        private System.Windows.Forms.Button btModo;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.Button btBeepOn;
        private System.Windows.Forms.Button btLimpar;
        private System.Windows.Forms.DataGridView dgLeitura;
        private System.Windows.Forms.Button btIniciar;
        private System.Windows.Forms.GroupBox groupBox9;
        private System.Windows.Forms.ComboBox cbRegiao;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtStart;
        private System.Windows.Forms.TextBox txtLenght;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtAcessPass;
        private System.Windows.Forms.TextBox txtWriteData;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btGravar;
        private System.Windows.Forms.Button btLer;
        private System.Windows.Forms.TextBox txtRetorno;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox ckIncremental;
        private System.Windows.Forms.CheckBox ckAuto;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btReload;
        private System.Windows.Forms.Button btLimpaDg;
        private System.Windows.Forms.GroupBox groupBox10;
        private System.Windows.Forms.ComboBox cbFreq;
        private System.Windows.Forms.Button btSetFreq;
        private System.Windows.Forms.Button btGetFreq;
        private System.Windows.Forms.Timer timerGravar;
        private System.Windows.Forms.GroupBox gbInt;
        private System.Windows.Forms.ComboBox cbInterface;
        private System.Windows.Forms.Button btInterface;
    }
}


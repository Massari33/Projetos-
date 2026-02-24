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
            this.txtLog = new System.Windows.Forms.TextBox();
            this.timerLeitura = new System.Windows.Forms.Timer(this.components);
            this.btLimpar = new System.Windows.Forms.Button();
            this.timerGravar = new System.Windows.Forms.Timer(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tbRead = new System.Windows.Forms.TabPage();
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
            this.tbCon = new System.Windows.Forms.TabPage();
            this.gbInt = new System.Windows.Forms.GroupBox();
            this.btInterface = new System.Windows.Forms.Button();
            this.cbInterface = new System.Windows.Forms.ComboBox();
            this.groupBox10 = new System.Windows.Forms.GroupBox();
            this.btGetFreq = new System.Windows.Forms.Button();
            this.cbFreq = new System.Windows.Forms.ComboBox();
            this.btSetFreq = new System.Windows.Forms.Button();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.btBeepOn = new System.Windows.Forms.Button();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.btModo = new System.Windows.Forms.Button();
            this.cbModo = new System.Windows.Forms.ComboBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btReload = new System.Windows.Forms.Button();
            this.rbHid = new System.Windows.Forms.RadioButton();
            this.rbCom = new System.Windows.Forms.RadioButton();
            this.btConectar = new System.Windows.Forms.Button();
            this.btDesconectar = new System.Windows.Forms.Button();
            this.cbPorta = new System.Windows.Forms.ComboBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.cbPotencia = new System.Windows.Forms.ComboBox();
            this.btSetPotencia = new System.Windows.Forms.Button();
            this.btGetPotencia = new System.Windows.Forms.Button();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.btGpioOn = new System.Windows.Forms.Button();
            this.tab1 = new System.Windows.Forms.TabControl();
            this.Post = new System.Windows.Forms.TabPage();
            this.gbLeitura = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.btnPostar = new System.Windows.Forms.Button();
            this.TimerPost = new System.Windows.Forms.ComboBox();
            this.chbxPost = new System.Windows.Forms.CheckBox();
            this.dgLeituras = new System.Windows.Forms.DataGridView();
            this.gpPost = new System.Windows.Forms.GroupBox();
            this.txtCompanyID = new System.Windows.Forms.TextBox();
            this.CompanyID = new System.Windows.Forms.Label();
            this.txtIP = new System.Windows.Forms.TextBox();
            this.txtMac = new System.Windows.Forms.TextBox();
            this.IP = new System.Windows.Forms.Label();
            this.Mac = new System.Windows.Forms.Label();
            this.txtToken = new System.Windows.Forms.TextBox();
            this.Token = new System.Windows.Forms.Label();
            this.txtPostUrl = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnLimparGrid = new System.Windows.Forms.Button();
            this.btnExportarCSV = new System.Windows.Forms.Button();
            this.btnExportarJson = new System.Windows.Forms.Button();
            this.btnIniciarLeituraPost = new System.Windows.Forms.Button();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.tbRead.SuspendLayout();
            this.groupBox9.SuspendLayout();
            this.tbCon.SuspendLayout();
            this.gbInt.SuspendLayout();
            this.groupBox10.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.tab1.SuspendLayout();
            this.Post.SuspendLayout();
            this.gbLeitura.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgLeituras)).BeginInit();
            this.gpPost.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtLog
            // 
            this.txtLog.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.txtLog.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtLog.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.txtLog.ForeColor = System.Drawing.SystemColors.Desktop;
            this.txtLog.Location = new System.Drawing.Point(0, 422);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.ReadOnly = true;
            this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtLog.Size = new System.Drawing.Size(498, 109);
            this.txtLog.TabIndex = 5;
            // 
            // timerLeitura
            // 
            this.timerLeitura.Interval = 1;
            this.timerLeitura.Tick += new System.EventHandler(this.timerLeitura_Tick);
            // 
            // btLimpar
            // 
            this.btLimpar.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.btLimpar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btLimpar.Location = new System.Drawing.Point(382, 422);
            this.btLimpar.Name = "btLimpar";
            this.btLimpar.Size = new System.Drawing.Size(99, 22);
            this.btLimpar.TabIndex = 33;
            this.btLimpar.Text = "🗑️";
            this.btLimpar.UseVisualStyleBackColor = false;
            this.btLimpar.Click += new System.EventHandler(this.btLimpar_Click);
            // 
            // timerGravar
            // 
            this.timerGravar.Interval = 3000;
            this.timerGravar.Tick += new System.EventHandler(this.timerGravar_Tick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // tbRead
            // 
            this.tbRead.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.tbRead.Controls.Add(this.groupBox9);
            this.tbRead.Location = new System.Drawing.Point(4, 22);
            this.tbRead.Name = "tbRead";
            this.tbRead.Padding = new System.Windows.Forms.Padding(3);
            this.tbRead.Size = new System.Drawing.Size(487, 387);
            this.tbRead.TabIndex = 1;
            this.tbRead.Text = "Leitura/Gravação";
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
            this.groupBox9.Location = new System.Drawing.Point(6, 3);
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.Size = new System.Drawing.Size(475, 199);
            this.groupBox9.TabIndex = 2;
            this.groupBox9.TabStop = false;
            this.groupBox9.Text = "Ler/Gravar";
            // 
            // ckAuto
            // 
            this.ckAuto.AutoSize = true;
            this.ckAuto.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.ckAuto.Location = new System.Drawing.Point(219, 168);
            this.ckAuto.Name = "ckAuto";
            this.ckAuto.Size = new System.Drawing.Size(104, 18);
            this.ckAuto.TabIndex = 15;
            this.ckAuto.Text = "Auto Gravação";
            this.ckAuto.UseVisualStyleBackColor = true;
            this.ckAuto.CheckedChanged += new System.EventHandler(this.ckAuto_CheckedChanged);
            // 
            // ckIncremental
            // 
            this.ckIncremental.AutoSize = true;
            this.ckIncremental.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.ckIncremental.Location = new System.Drawing.Point(115, 169);
            this.ckIncremental.Name = "ckIncremental";
            this.ckIncremental.Size = new System.Drawing.Size(87, 18);
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
            this.btLer.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.btLer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btLer.Location = new System.Drawing.Point(323, 104);
            this.btLer.Name = "btLer";
            this.btLer.Size = new System.Drawing.Size(75, 21);
            this.btLer.TabIndex = 12;
            this.btLer.Text = "Ler";
            this.btLer.UseVisualStyleBackColor = false;
            this.btLer.Click += new System.EventHandler(this.btLer_Click);
            // 
            // txtRetorno
            // 
            this.txtRetorno.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.txtRetorno.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtRetorno.Location = new System.Drawing.Point(115, 104);
            this.txtRetorno.Name = "txtRetorno";
            this.txtRetorno.Size = new System.Drawing.Size(202, 20);
            this.txtRetorno.TabIndex = 11;
            // 
            // btGravar
            // 
            this.btGravar.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.btGravar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btGravar.Location = new System.Drawing.Point(323, 140);
            this.btGravar.Name = "btGravar";
            this.btGravar.Size = new System.Drawing.Size(75, 22);
            this.btGravar.TabIndex = 10;
            this.btGravar.Text = "Gravar";
            this.btGravar.UseVisualStyleBackColor = false;
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
            this.txtWriteData.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.txtWriteData.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
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
            this.txtAcessPass.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.txtAcessPass.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAcessPass.Location = new System.Drawing.Point(115, 74);
            this.txtAcessPass.MaxLength = 4;
            this.txtAcessPass.Name = "txtAcessPass";
            this.txtAcessPass.Size = new System.Drawing.Size(100, 20);
            this.txtAcessPass.TabIndex = 6;
            this.txtAcessPass.Text = "00000000";
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
            this.txtLenght.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.txtLenght.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtLenght.Location = new System.Drawing.Point(334, 45);
            this.txtLenght.MaxLength = 2;
            this.txtLenght.Name = "txtLenght";
            this.txtLenght.Size = new System.Drawing.Size(64, 20);
            this.txtLenght.TabIndex = 4;
            this.txtLenght.Text = "06";
            // 
            // txtStart
            // 
            this.txtStart.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.txtStart.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
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
            this.cbRegiao.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.cbRegiao.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbRegiao.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
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
            this.cbRegiao.SelectedIndexChanged += new System.EventHandler(this.cbRegiao_SelectedIndexChanged);
            // 
            // tbCon
            // 
            this.tbCon.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
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
            this.tbCon.Size = new System.Drawing.Size(487, 387);
            this.tbCon.TabIndex = 0;
            this.tbCon.Text = "Conexão/Parâmetros";
            // 
            // gbInt
            // 
            this.gbInt.Controls.Add(this.btInterface);
            this.gbInt.Controls.Add(this.cbInterface);
            this.gbInt.Location = new System.Drawing.Point(269, 288);
            this.gbInt.Name = "gbInt";
            this.gbInt.Size = new System.Drawing.Size(212, 83);
            this.gbInt.TabIndex = 33;
            this.gbInt.TabStop = false;
            this.gbInt.Text = "Interface (Apenas HID)";
            // 
            // btInterface
            // 
            this.btInterface.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.btInterface.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btInterface.Location = new System.Drawing.Point(130, 46);
            this.btInterface.Name = "btInterface";
            this.btInterface.Size = new System.Drawing.Size(76, 25);
            this.btInterface.TabIndex = 30;
            this.btInterface.Text = "Definir";
            this.btInterface.UseVisualStyleBackColor = false;
            this.btInterface.Click += new System.EventHandler(this.btInterface_Click);
            // 
            // cbInterface
            // 
            this.cbInterface.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.cbInterface.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbInterface.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbInterface.FormattingEnabled = true;
            this.cbInterface.Items.AddRange(new object[] {
            "USB",
            "Teclado"});
            this.cbInterface.Location = new System.Drawing.Point(7, 19);
            this.cbInterface.Name = "cbInterface";
            this.cbInterface.Size = new System.Drawing.Size(199, 21);
            this.cbInterface.TabIndex = 0;
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
            this.btGetFreq.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.btGetFreq.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btGetFreq.Location = new System.Drawing.Point(172, 48);
            this.btGetFreq.Name = "btGetFreq";
            this.btGetFreq.Size = new System.Drawing.Size(76, 25);
            this.btGetFreq.TabIndex = 29;
            this.btGetFreq.Text = "Obter";
            this.btGetFreq.UseVisualStyleBackColor = false;
            this.btGetFreq.Click += new System.EventHandler(this.btGetFreq_Click);
            // 
            // cbFreq
            // 
            this.cbFreq.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.cbFreq.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFreq.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
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
            this.btSetFreq.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.btSetFreq.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btSetFreq.Location = new System.Drawing.Point(6, 48);
            this.btSetFreq.Name = "btSetFreq";
            this.btSetFreq.Size = new System.Drawing.Size(76, 25);
            this.btSetFreq.TabIndex = 25;
            this.btSetFreq.Text = "Definir";
            this.btSetFreq.UseVisualStyleBackColor = false;
            this.btSetFreq.Click += new System.EventHandler(this.btSetFreq_Click);
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.btBeepOn);
            this.groupBox8.Location = new System.Drawing.Point(267, 200);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(214, 83);
            this.groupBox8.TabIndex = 32;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "Beep";
            // 
            // btBeepOn
            // 
            this.btBeepOn.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.btBeepOn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btBeepOn.Location = new System.Drawing.Point(33, 26);
            this.btBeepOn.Name = "btBeepOn";
            this.btBeepOn.Size = new System.Drawing.Size(158, 38);
            this.btBeepOn.TabIndex = 28;
            this.btBeepOn.Text = "Ligado";
            this.btBeepOn.UseVisualStyleBackColor = false;
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
            this.btModo.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.btModo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btModo.Location = new System.Drawing.Point(160, 30);
            this.btModo.Name = "btModo";
            this.btModo.Size = new System.Drawing.Size(76, 35);
            this.btModo.TabIndex = 1;
            this.btModo.Text = "Definir";
            this.btModo.UseVisualStyleBackColor = false;
            this.btModo.Click += new System.EventHandler(this.btModo_Click);
            // 
            // cbModo
            // 
            this.cbModo.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.cbModo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbModo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbModo.FormattingEnabled = true;
            this.cbModo.Items.AddRange(new object[] {
            "Modo Escravo",
            "Modo Autonômo"});
            this.cbModo.Location = new System.Drawing.Point(9, 38);
            this.cbModo.Name = "cbModo";
            this.cbModo.Size = new System.Drawing.Size(135, 21);
            this.cbModo.TabIndex = 0;
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
            this.groupBox3.Size = new System.Drawing.Size(475, 91);
            this.groupBox3.TabIndex = 14;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Conexão";
            // 
            // btReload
            // 
            this.btReload.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.btReload.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btReload.Location = new System.Drawing.Point(379, 32);
            this.btReload.Name = "btReload";
            this.btReload.Size = new System.Drawing.Size(90, 32);
            this.btReload.TabIndex = 8;
            this.btReload.Text = "Atualizar Portas";
            this.btReload.UseVisualStyleBackColor = false;
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
            // btConectar
            // 
            this.btConectar.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.btConectar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btConectar.Location = new System.Drawing.Point(174, 32);
            this.btConectar.Name = "btConectar";
            this.btConectar.Size = new System.Drawing.Size(87, 32);
            this.btConectar.TabIndex = 0;
            this.btConectar.Text = "Conectar";
            this.btConectar.UseVisualStyleBackColor = false;
            this.btConectar.Click += new System.EventHandler(this.btConectar_Click);
            // 
            // btDesconectar
            // 
            this.btDesconectar.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.btDesconectar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btDesconectar.Location = new System.Drawing.Point(275, 32);
            this.btDesconectar.Name = "btDesconectar";
            this.btDesconectar.Size = new System.Drawing.Size(91, 32);
            this.btDesconectar.TabIndex = 2;
            this.btDesconectar.Text = "Desconectar";
            this.btDesconectar.UseVisualStyleBackColor = false;
            this.btDesconectar.Click += new System.EventHandler(this.btDesconectar_Click);
            // 
            // cbPorta
            // 
            this.cbPorta.AutoCompleteCustomSource.AddRange(new string[] {
            "COM1",
            "COM2"});
            this.cbPorta.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.cbPorta.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPorta.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbPorta.FormattingEnabled = true;
            this.cbPorta.Location = new System.Drawing.Point(9, 55);
            this.cbPorta.Name = "cbPorta";
            this.cbPorta.Size = new System.Drawing.Size(155, 21);
            this.cbPorta.TabIndex = 1;
            this.cbPorta.Tag = "";
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
            // cbPotencia
            // 
            this.cbPotencia.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.cbPotencia.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPotencia.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
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
            // btSetPotencia
            // 
            this.btSetPotencia.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.btSetPotencia.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btSetPotencia.Location = new System.Drawing.Point(88, 27);
            this.btSetPotencia.Name = "btSetPotencia";
            this.btSetPotencia.Size = new System.Drawing.Size(76, 37);
            this.btSetPotencia.TabIndex = 25;
            this.btSetPotencia.Text = "Definir";
            this.btSetPotencia.UseVisualStyleBackColor = false;
            this.btSetPotencia.Click += new System.EventHandler(this.btSetPotencia_Click);
            // 
            // btGetPotencia
            // 
            this.btGetPotencia.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.btGetPotencia.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btGetPotencia.Location = new System.Drawing.Point(174, 27);
            this.btGetPotencia.Name = "btGetPotencia";
            this.btGetPotencia.Size = new System.Drawing.Size(76, 37);
            this.btGetPotencia.TabIndex = 24;
            this.btGetPotencia.Text = "Obter";
            this.btGetPotencia.UseVisualStyleBackColor = false;
            this.btGetPotencia.Click += new System.EventHandler(this.btGetPotencia_Click);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.btGpioOn);
            this.groupBox5.Location = new System.Drawing.Point(268, 103);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(213, 94);
            this.groupBox5.TabIndex = 30;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Controle do Relê (GPIO)";
            // 
            // btGpioOn
            // 
            this.btGpioOn.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.btGpioOn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btGpioOn.Location = new System.Drawing.Point(32, 30);
            this.btGpioOn.Name = "btGpioOn";
            this.btGpioOn.Size = new System.Drawing.Size(157, 35);
            this.btGpioOn.TabIndex = 26;
            this.btGpioOn.Text = "Desligado";
            this.btGpioOn.UseVisualStyleBackColor = false;
            this.btGpioOn.Click += new System.EventHandler(this.btGpioOn_Click);
            // 
            // tab1
            // 
            this.tab1.Controls.Add(this.tbCon);
            this.tab1.Controls.Add(this.tbRead);
            this.tab1.Controls.Add(this.Post);
            this.tab1.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            this.tab1.Location = new System.Drawing.Point(3, 3);
            this.tab1.Name = "tab1";
            this.tab1.SelectedIndex = 0;
            this.tab1.Size = new System.Drawing.Size(495, 413);
            this.tab1.TabIndex = 32;
            this.tab1.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.tab1_DrawItem);
            // 
            // Post
            // 
            this.Post.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.Post.Controls.Add(this.gbLeitura);
            this.Post.Controls.Add(this.dgLeituras);
            this.Post.Controls.Add(this.gpPost);
            this.Post.Controls.Add(this.groupBox1);
            this.Post.Location = new System.Drawing.Point(4, 22);
            this.Post.Name = "Post";
            this.Post.Padding = new System.Windows.Forms.Padding(3);
            this.Post.Size = new System.Drawing.Size(487, 387);
            this.Post.TabIndex = 2;
            this.Post.Text = "Post";
            // 
            // gbLeitura
            // 
            this.gbLeitura.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbLeitura.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.gbLeitura.Controls.Add(this.label7);
            this.gbLeitura.Controls.Add(this.btnPostar);
            this.gbLeitura.Controls.Add(this.TimerPost);
            this.gbLeitura.Controls.Add(this.chbxPost);
            this.gbLeitura.Location = new System.Drawing.Point(3, 109);
            this.gbLeitura.Name = "gbLeitura";
            this.gbLeitura.Size = new System.Drawing.Size(481, 49);
            this.gbLeitura.TabIndex = 1;
            this.gbLeitura.TabStop = false;
            this.gbLeitura.Text = "Enviar";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(405, 9);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(47, 13);
            this.label7.TabIndex = 4;
            this.label7.Text = "Timer (s)";
            // 
            // btnPostar
            // 
            this.btnPostar.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.btnPostar.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnPostar.Location = new System.Drawing.Point(9, 17);
            this.btnPostar.Name = "btnPostar";
            this.btnPostar.Size = new System.Drawing.Size(75, 23);
            this.btnPostar.TabIndex = 1;
            this.btnPostar.Text = "Enviar";
            this.btnPostar.UseVisualStyleBackColor = false;
            this.btnPostar.Click += new System.EventHandler(this.btnPostar_Click);
            // 
            // TimerPost
            // 
            this.TimerPost.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.TimerPost.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.TimerPost.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.TimerPost.FormattingEnabled = true;
            this.TimerPost.Items.AddRange(new object[] {
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
            "17",
            "18",
            "19",
            "20"});
            this.TimerPost.Location = new System.Drawing.Point(392, 25);
            this.TimerPost.Name = "TimerPost";
            this.TimerPost.Size = new System.Drawing.Size(77, 21);
            this.TimerPost.TabIndex = 3;
            this.TimerPost.SelectedIndexChanged += new System.EventHandler(this.TimerPost_SelectedIndexChanged);
            // 
            // chbxPost
            // 
            this.chbxPost.AutoSize = true;
            this.chbxPost.Location = new System.Drawing.Point(283, 23);
            this.chbxPost.Name = "chbxPost";
            this.chbxPost.Size = new System.Drawing.Size(103, 17);
            this.chbxPost.TabIndex = 2;
            this.chbxPost.Text = "Post Automático";
            this.chbxPost.UseVisualStyleBackColor = true;
            this.chbxPost.CheckedChanged += new System.EventHandler(this.chbxPost_CheckedChanged);
            // 
            // dgLeituras
            // 
            this.dgLeituras.AllowUserToAddRows = false;
            this.dgLeituras.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgLeituras.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgLeituras.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgLeituras.Cursor = System.Windows.Forms.Cursors.Default;
            this.dgLeituras.Location = new System.Drawing.Point(3, 209);
            this.dgLeituras.Name = "dgLeituras";
            this.dgLeituras.ReadOnly = true;
            this.dgLeituras.RowHeadersVisible = false;
            this.dgLeituras.Size = new System.Drawing.Size(481, 176);
            this.dgLeituras.TabIndex = 2;
            this.dgLeituras.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgLeituras_CellContentClick);
            // 
            // gpPost
            // 
            this.gpPost.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.gpPost.Controls.Add(this.txtCompanyID);
            this.gpPost.Controls.Add(this.CompanyID);
            this.gpPost.Controls.Add(this.txtIP);
            this.gpPost.Controls.Add(this.txtMac);
            this.gpPost.Controls.Add(this.IP);
            this.gpPost.Controls.Add(this.Mac);
            this.gpPost.Controls.Add(this.txtToken);
            this.gpPost.Controls.Add(this.Token);
            this.gpPost.Controls.Add(this.txtPostUrl);
            this.gpPost.Controls.Add(this.label9);
            this.gpPost.Dock = System.Windows.Forms.DockStyle.Top;
            this.gpPost.Location = new System.Drawing.Point(3, 3);
            this.gpPost.Name = "gpPost";
            this.gpPost.Size = new System.Drawing.Size(481, 100);
            this.gpPost.TabIndex = 0;
            this.gpPost.TabStop = false;
            this.gpPost.Text = "Configuração Post";
            this.gpPost.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // txtCompanyID
            // 
            this.txtCompanyID.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.txtCompanyID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCompanyID.Location = new System.Drawing.Point(404, 33);
            this.txtCompanyID.Name = "txtCompanyID";
            this.txtCompanyID.Size = new System.Drawing.Size(65, 20);
            this.txtCompanyID.TabIndex = 9;
            this.txtCompanyID.Text = "0";
            // 
            // CompanyID
            // 
            this.CompanyID.AutoSize = true;
            this.CompanyID.Location = new System.Drawing.Point(405, 16);
            this.CompanyID.Name = "CompanyID";
            this.CompanyID.Size = new System.Drawing.Size(68, 13);
            this.CompanyID.TabIndex = 8;
            this.CompanyID.Text = "Company ID:";
            // 
            // txtIP
            // 
            this.txtIP.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.txtIP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtIP.Location = new System.Drawing.Point(295, 77);
            this.txtIP.Name = "txtIP";
            this.txtIP.Size = new System.Drawing.Size(100, 20);
            this.txtIP.TabIndex = 7;
            this.txtIP.Text = "192.168.0.10";
            this.txtIP.TextChanged += new System.EventHandler(this.txtIP_TextChanged);
            // 
            // txtMac
            // 
            this.txtMac.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.txtMac.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtMac.Location = new System.Drawing.Point(295, 33);
            this.txtMac.Name = "txtMac";
            this.txtMac.Size = new System.Drawing.Size(100, 20);
            this.txtMac.TabIndex = 6;
            this.txtMac.TextChanged += new System.EventHandler(this.txtMac_TextChanged);
            // 
            // IP
            // 
            this.IP.AutoSize = true;
            this.IP.Location = new System.Drawing.Point(296, 61);
            this.IP.Name = "IP";
            this.IP.Size = new System.Drawing.Size(17, 13);
            this.IP.TabIndex = 5;
            this.IP.Text = "IP";
            this.IP.Click += new System.EventHandler(this.CompanyId_Click);
            // 
            // Mac
            // 
            this.Mac.AutoSize = true;
            this.Mac.Location = new System.Drawing.Point(296, 16);
            this.Mac.Name = "Mac";
            this.Mac.Size = new System.Drawing.Size(28, 13);
            this.Mac.TabIndex = 4;
            this.Mac.Text = "Mac";
            this.Mac.Click += new System.EventHandler(this.label10_Click);
            // 
            // txtToken
            // 
            this.txtToken.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.txtToken.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtToken.Location = new System.Drawing.Point(9, 78);
            this.txtToken.Name = "txtToken";
            this.txtToken.Size = new System.Drawing.Size(280, 20);
            this.txtToken.TabIndex = 3;
            // 
            // Token
            // 
            this.Token.AutoSize = true;
            this.Token.Location = new System.Drawing.Point(9, 61);
            this.Token.Name = "Token";
            this.Token.Size = new System.Drawing.Size(38, 13);
            this.Token.TabIndex = 2;
            this.Token.Text = "Token";
            // 
            // txtPostUrl
            // 
            this.txtPostUrl.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.txtPostUrl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPostUrl.Location = new System.Drawing.Point(9, 33);
            this.txtPostUrl.Name = "txtPostUrl";
            this.txtPostUrl.Size = new System.Drawing.Size(280, 20);
            this.txtPostUrl.TabIndex = 1;
            this.txtPostUrl.Text = "http://demo.viaondarfid.com.br/viaonda/getTagList.php";
            this.txtPostUrl.TextChanged += new System.EventHandler(this.txtPostUrl_TextChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 16);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(53, 13);
            this.label9.TabIndex = 0;
            this.label9.Text = "URL Post";
            this.label9.Click += new System.EventHandler(this.label9_Click_1);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnLimparGrid);
            this.groupBox1.Controls.Add(this.btnExportarCSV);
            this.groupBox1.Controls.Add(this.btnExportarJson);
            this.groupBox1.Controls.Add(this.btnIniciarLeituraPost);
            this.groupBox1.Location = new System.Drawing.Point(0, 157);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(491, 46);
            this.groupBox1.TabIndex = 37;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Leitura / Exportar";
            // 
            // btnLimparGrid
            // 
            this.btnLimparGrid.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.btnLimparGrid.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLimparGrid.Location = new System.Drawing.Point(399, 16);
            this.btnLimparGrid.Name = "btnLimparGrid";
            this.btnLimparGrid.Size = new System.Drawing.Size(73, 23);
            this.btnLimparGrid.TabIndex = 36;
            this.btnLimparGrid.Text = "Limpar";
            this.btnLimparGrid.UseVisualStyleBackColor = false;
            this.btnLimparGrid.Click += new System.EventHandler(this.btnLimparGrid_Click);
            // 
            // btnExportarCSV
            // 
            this.btnExportarCSV.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.btnExportarCSV.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExportarCSV.Location = new System.Drawing.Point(307, 16);
            this.btnExportarCSV.Name = "btnExportarCSV";
            this.btnExportarCSV.Size = new System.Drawing.Size(86, 23);
            this.btnExportarCSV.TabIndex = 35;
            this.btnExportarCSV.Text = "Exportar CSV";
            this.btnExportarCSV.UseVisualStyleBackColor = false;
            this.btnExportarCSV.Click += new System.EventHandler(this.btnExportarCSV_Click);
            // 
            // btnExportarJson
            // 
            this.btnExportarJson.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.btnExportarJson.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExportarJson.Location = new System.Drawing.Point(215, 16);
            this.btnExportarJson.Name = "btnExportarJson";
            this.btnExportarJson.Size = new System.Drawing.Size(86, 23);
            this.btnExportarJson.TabIndex = 34;
            this.btnExportarJson.Text = "Exportar Json";
            this.btnExportarJson.UseVisualStyleBackColor = false;
            this.btnExportarJson.Click += new System.EventHandler(this.btnExportarJson_Click);
            // 
            // btnIniciarLeituraPost
            // 
            this.btnIniciarLeituraPost.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.btnIniciarLeituraPost.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnIniciarLeituraPost.Location = new System.Drawing.Point(12, 16);
            this.btnIniciarLeituraPost.Name = "btnIniciarLeituraPost";
            this.btnIniciarLeituraPost.Size = new System.Drawing.Size(75, 23);
            this.btnIniciarLeituraPost.TabIndex = 0;
            this.btnIniciarLeituraPost.Text = "Iniciar";
            this.btnIniciarLeituraPost.UseVisualStyleBackColor = false;
            this.btnIniciarLeituraPost.Click += new System.EventHandler(this.btnIniciarLeituraPost_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.ClientSize = new System.Drawing.Size(498, 531);
            this.Controls.Add(this.btLimpar);
            this.Controls.Add(this.txtLog);
            this.Controls.Add(this.tab1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "M-ID10S";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tbRead.ResumeLayout(false);
            this.groupBox9.ResumeLayout(false);
            this.groupBox9.PerformLayout();
            this.tbCon.ResumeLayout(false);
            this.gbInt.ResumeLayout(false);
            this.groupBox10.ResumeLayout(false);
            this.groupBox8.ResumeLayout(false);
            this.groupBox7.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.tab1.ResumeLayout(false);
            this.Post.ResumeLayout(false);
            this.gbLeitura.ResumeLayout(false);
            this.gbLeitura.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgLeituras)).EndInit();
            this.gpPost.ResumeLayout(false);
            this.gpPost.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox txtLog;
        private System.Windows.Forms.Timer timerLeitura;
        private System.Windows.Forms.Button btLimpar;
        private System.Windows.Forms.Timer timerGravar;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.TabPage tbRead;
        private System.Windows.Forms.GroupBox groupBox9;
        private System.Windows.Forms.CheckBox ckAuto;
        private System.Windows.Forms.CheckBox ckIncremental;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btLer;
        private System.Windows.Forms.TextBox txtRetorno;
        private System.Windows.Forms.Button btGravar;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtWriteData;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtAcessPass;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtLenght;
        private System.Windows.Forms.TextBox txtStart;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbRegiao;
        private System.Windows.Forms.TabPage tbCon;
        private System.Windows.Forms.GroupBox gbInt;
        private System.Windows.Forms.Button btInterface;
        private System.Windows.Forms.ComboBox cbInterface;
        private System.Windows.Forms.GroupBox groupBox10;
        private System.Windows.Forms.Button btGetFreq;
        private System.Windows.Forms.ComboBox cbFreq;
        private System.Windows.Forms.Button btSetFreq;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.Button btBeepOn;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.Button btModo;
        private System.Windows.Forms.ComboBox cbModo;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btReload;
        private System.Windows.Forms.RadioButton rbHid;
        private System.Windows.Forms.RadioButton rbCom;
        private System.Windows.Forms.Button btConectar;
        private System.Windows.Forms.Button btDesconectar;
        private System.Windows.Forms.ComboBox cbPorta;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.ComboBox cbPotencia;
        private System.Windows.Forms.Button btSetPotencia;
        private System.Windows.Forms.Button btGetPotencia;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Button btGpioOn;
        private System.Windows.Forms.TabControl tab1;
        private System.Windows.Forms.TabPage Post;
        private System.Windows.Forms.GroupBox gpPost;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label Token;
        private System.Windows.Forms.TextBox txtPostUrl;
        private System.Windows.Forms.GroupBox gbLeitura;
        private System.Windows.Forms.TextBox txtToken;
        private System.Windows.Forms.Button btnIniciarLeituraPost;
        private System.Windows.Forms.DataGridView dgLeituras;
        private System.Windows.Forms.Button btnPostar;
        private System.Windows.Forms.Label IP;
        private System.Windows.Forms.Label Mac;
        private System.Windows.Forms.TextBox txtIP;
        private System.Windows.Forms.TextBox txtMac;
        private System.Windows.Forms.CheckBox chbxPost;
        private System.Windows.Forms.ComboBox TimerPost;
        private System.Windows.Forms.CheckBox chkDarkMode;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtCompanyID;
        private System.Windows.Forms.Label CompanyID;
        private System.Windows.Forms.Button btnExportarJson;
        private System.Windows.Forms.Button btnExportarCSV;
        private System.Windows.Forms.Button btnLimparGrid;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}


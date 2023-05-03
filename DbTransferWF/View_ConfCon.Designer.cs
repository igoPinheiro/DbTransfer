namespace DbTransferWF
{
    partial class View_ConfCon
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.gvDados = new System.Windows.Forms.DataGridView();
            this.descricaoDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.serverDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.databaseDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.userDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.passwordDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.portDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.isProducaoDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.sGBDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bsGrid = new System.Windows.Forms.BindingSource(this.components);
            this.txtDes = new System.Windows.Forms.TextBox();
            this.BDFormbs = new System.Windows.Forms.BindingSource(this.components);
            this.txtSvd = new System.Windows.Forms.TextBox();
            this.txtNomBD = new System.Windows.Forms.TextBox();
            this.txtUsr = new System.Windows.Forms.TextBox();
            this.txtPwd = new System.Windows.Forms.TextBox();
            this.cmbSBDB = new System.Windows.Forms.ComboBox();
            this.ckbPrd = new System.Windows.Forms.CheckBox();
            this.btnSav = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtPrt = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.btnDel = new System.Windows.Forms.Button();
            this.btnEdt = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.lblAct = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.gvDados)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BDFormbs)).BeginInit();
            this.SuspendLayout();
            // 
            // gvDados
            // 
            this.gvDados.AllowUserToAddRows = false;
            this.gvDados.AllowUserToDeleteRows = false;
            this.gvDados.AutoGenerateColumns = false;
            this.gvDados.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvDados.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.descricaoDataGridViewTextBoxColumn,
            this.serverDataGridViewTextBoxColumn,
            this.databaseDataGridViewTextBoxColumn,
            this.userDataGridViewTextBoxColumn,
            this.passwordDataGridViewTextBoxColumn,
            this.portDataGridViewTextBoxColumn,
            this.isProducaoDataGridViewCheckBoxColumn,
            this.sGBDDataGridViewTextBoxColumn});
            this.gvDados.DataSource = this.bsGrid;
            this.gvDados.Location = new System.Drawing.Point(12, 34);
            this.gvDados.Name = "gvDados";
            this.gvDados.ReadOnly = true;
            this.gvDados.RowTemplate.Height = 25;
            this.gvDados.Size = new System.Drawing.Size(867, 163);
            this.gvDados.TabIndex = 0;
            this.gvDados.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvDados_CellContentClick);
            // 
            // descricaoDataGridViewTextBoxColumn
            // 
            this.descricaoDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.descricaoDataGridViewTextBoxColumn.DataPropertyName = "Descricao";
            this.descricaoDataGridViewTextBoxColumn.HeaderText = "Descricao";
            this.descricaoDataGridViewTextBoxColumn.Name = "descricaoDataGridViewTextBoxColumn";
            this.descricaoDataGridViewTextBoxColumn.ReadOnly = true;
            this.descricaoDataGridViewTextBoxColumn.Width = 83;
            // 
            // serverDataGridViewTextBoxColumn
            // 
            this.serverDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.serverDataGridViewTextBoxColumn.DataPropertyName = "Server";
            this.serverDataGridViewTextBoxColumn.HeaderText = "Server";
            this.serverDataGridViewTextBoxColumn.Name = "serverDataGridViewTextBoxColumn";
            this.serverDataGridViewTextBoxColumn.ReadOnly = true;
            this.serverDataGridViewTextBoxColumn.Width = 64;
            // 
            // databaseDataGridViewTextBoxColumn
            // 
            this.databaseDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.databaseDataGridViewTextBoxColumn.DataPropertyName = "Database";
            this.databaseDataGridViewTextBoxColumn.HeaderText = "Database";
            this.databaseDataGridViewTextBoxColumn.Name = "databaseDataGridViewTextBoxColumn";
            this.databaseDataGridViewTextBoxColumn.ReadOnly = true;
            this.databaseDataGridViewTextBoxColumn.Width = 80;
            // 
            // userDataGridViewTextBoxColumn
            // 
            this.userDataGridViewTextBoxColumn.DataPropertyName = "User";
            this.userDataGridViewTextBoxColumn.HeaderText = "User";
            this.userDataGridViewTextBoxColumn.Name = "userDataGridViewTextBoxColumn";
            this.userDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // passwordDataGridViewTextBoxColumn
            // 
            this.passwordDataGridViewTextBoxColumn.DataPropertyName = "Password";
            this.passwordDataGridViewTextBoxColumn.HeaderText = "Password";
            this.passwordDataGridViewTextBoxColumn.Name = "passwordDataGridViewTextBoxColumn";
            this.passwordDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // portDataGridViewTextBoxColumn
            // 
            this.portDataGridViewTextBoxColumn.DataPropertyName = "Port";
            this.portDataGridViewTextBoxColumn.HeaderText = "Port";
            this.portDataGridViewTextBoxColumn.Name = "portDataGridViewTextBoxColumn";
            this.portDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // isProducaoDataGridViewCheckBoxColumn
            // 
            this.isProducaoDataGridViewCheckBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.isProducaoDataGridViewCheckBoxColumn.DataPropertyName = "IsProducao";
            this.isProducaoDataGridViewCheckBoxColumn.HeaderText = "Produção";
            this.isProducaoDataGridViewCheckBoxColumn.Name = "isProducaoDataGridViewCheckBoxColumn";
            this.isProducaoDataGridViewCheckBoxColumn.ReadOnly = true;
            this.isProducaoDataGridViewCheckBoxColumn.Width = 64;
            // 
            // sGBDDataGridViewTextBoxColumn
            // 
            this.sGBDDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.sGBDDataGridViewTextBoxColumn.DataPropertyName = "SGBD";
            this.sGBDDataGridViewTextBoxColumn.HeaderText = "SGBD";
            this.sGBDDataGridViewTextBoxColumn.Name = "sGBDDataGridViewTextBoxColumn";
            this.sGBDDataGridViewTextBoxColumn.ReadOnly = true;
            this.sGBDDataGridViewTextBoxColumn.Width = 61;
            // 
            // bsGrid
            // 
            this.bsGrid.DataSource = typeof(DbTransferWF.Model.BancoDados01);
            // 
            // txtDes
            // 
            this.txtDes.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.BDFormbs, "Descricao", true));
            this.txtDes.Location = new System.Drawing.Point(12, 260);
            this.txtDes.Name = "txtDes";
            this.txtDes.Size = new System.Drawing.Size(554, 23);
            this.txtDes.TabIndex = 1;
            // 
            // BDFormbs
            // 
            this.BDFormbs.DataSource = typeof(DbTransferWF.Model.BancoDados01);
            // 
            // txtSvd
            // 
            this.txtSvd.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.BDFormbs, "Server", true));
            this.txtSvd.Location = new System.Drawing.Point(12, 309);
            this.txtSvd.Name = "txtSvd";
            this.txtSvd.Size = new System.Drawing.Size(134, 23);
            this.txtSvd.TabIndex = 3;
            // 
            // txtNomBD
            // 
            this.txtNomBD.AcceptsTab = true;
            this.txtNomBD.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.BDFormbs, "Database", true));
            this.txtNomBD.Location = new System.Drawing.Point(152, 309);
            this.txtNomBD.Name = "txtNomBD";
            this.txtNomBD.Size = new System.Drawing.Size(134, 23);
            this.txtNomBD.TabIndex = 4;
            // 
            // txtUsr
            // 
            this.txtUsr.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.BDFormbs, "User", true));
            this.txtUsr.Location = new System.Drawing.Point(292, 309);
            this.txtUsr.Name = "txtUsr";
            this.txtUsr.Size = new System.Drawing.Size(134, 23);
            this.txtUsr.TabIndex = 5;
            // 
            // txtPwd
            // 
            this.txtPwd.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.BDFormbs, "Password", true));
            this.txtPwd.Location = new System.Drawing.Point(432, 309);
            this.txtPwd.Name = "txtPwd";
            this.txtPwd.Size = new System.Drawing.Size(134, 23);
            this.txtPwd.TabIndex = 6;
            // 
            // cmbSBDB
            // 
            this.cmbSBDB.DataBindings.Add(new System.Windows.Forms.Binding("Tag", this.BDFormbs, "SGBD", true));
            this.cmbSBDB.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.BDFormbs, "SGBD", true));
            this.cmbSBDB.FormattingEnabled = true;
            this.cmbSBDB.Location = new System.Drawing.Point(572, 260);
            this.cmbSBDB.Name = "cmbSBDB";
            this.cmbSBDB.Size = new System.Drawing.Size(198, 23);
            this.cmbSBDB.TabIndex = 2;
            // 
            // ckbPrd
            // 
            this.ckbPrd.AutoSize = true;
            this.ckbPrd.DataBindings.Add(new System.Windows.Forms.Binding("Checked", this.BDFormbs, "IsProducao", true));
            this.ckbPrd.Location = new System.Drawing.Point(670, 311);
            this.ckbPrd.Name = "ckbPrd";
            this.ckbPrd.Size = new System.Drawing.Size(100, 19);
            this.ckbPrd.TabIndex = 8;
            this.ckbPrd.Text = "BD Produção?";
            this.ckbPrd.UseVisualStyleBackColor = true;
            // 
            // btnSav
            // 
            this.btnSav.Location = new System.Drawing.Point(787, 309);
            this.btnSav.Name = "btnSav";
            this.btnSav.Size = new System.Drawing.Size(92, 23);
            this.btnSav.TabIndex = 9;
            this.btnSav.Text = "Salvar";
            this.btnSav.UseVisualStyleBackColor = true;
            this.btnSav.Click += new System.EventHandler(this.BtnSav_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 242);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 15);
            this.label1.TabIndex = 9;
            this.label1.Text = "Descrição";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(572, 242);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(36, 15);
            this.label2.TabIndex = 10;
            this.label2.Text = "SGBD";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 291);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 15);
            this.label3.TabIndex = 11;
            this.label3.Text = "Servidor/Host";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(152, 291);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(128, 15);
            this.label4.TabIndex = 12;
            this.label4.Text = "Nome Banco de Dados";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(292, 291);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(47, 15);
            this.label5.TabIndex = 13;
            this.label5.Text = "Usuário";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(432, 291);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(39, 15);
            this.label6.TabIndex = 14;
            this.label6.Text = "Senha";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(572, 291);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(35, 15);
            this.label7.TabIndex = 16;
            this.label7.Text = "Porta";
            // 
            // txtPrt
            // 
            this.txtPrt.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.BDFormbs, "Port", true));
            this.txtPrt.Location = new System.Drawing.Point(572, 309);
            this.txtPrt.Name = "txtPrt";
            this.txtPrt.Size = new System.Drawing.Size(92, 23);
            this.txtPrt.TabIndex = 7;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label8.Location = new System.Drawing.Point(12, 9);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(166, 15);
            this.label8.TabIndex = 17;
            this.label8.Text = "Conexões de banco de dados";
            this.label8.Click += new System.EventHandler(this.label8_Click);
            // 
            // btnDel
            // 
            this.btnDel.BackColor = System.Drawing.Color.White;
            this.btnDel.BackgroundImage = global::DbTransferWF.Properties.Resources.rem;
            this.btnDel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnDel.Location = new System.Drawing.Point(834, 0);
            this.btnDel.Name = "btnDel";
            this.btnDel.Size = new System.Drawing.Size(45, 32);
            this.btnDel.TabIndex = 18;
            this.toolTip1.SetToolTip(this.btnDel, "Excluir Conexão");
            this.btnDel.UseVisualStyleBackColor = false;
            this.btnDel.Click += new System.EventHandler(this.BtnDel_Click);
            // 
            // btnEdt
            // 
            this.btnEdt.BackColor = System.Drawing.Color.White;
            this.btnEdt.BackgroundImage = global::DbTransferWF.Properties.Resources._37514_6_pencil_icon;
            this.btnEdt.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnEdt.FlatAppearance.BorderSize = 0;
            this.btnEdt.ForeColor = System.Drawing.Color.Transparent;
            this.btnEdt.Location = new System.Drawing.Point(783, 0);
            this.btnEdt.Name = "btnEdt";
            this.btnEdt.Size = new System.Drawing.Size(45, 32);
            this.btnEdt.TabIndex = 19;
            this.toolTip1.SetToolTip(this.btnEdt, "Editar Conexão");
            this.btnEdt.UseVisualStyleBackColor = false;
            this.btnEdt.Click += new System.EventHandler(this.Button3_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.BackColor = System.Drawing.Color.White;
            this.btnAdd.BackgroundImage = global::DbTransferWF.Properties.Resources.sign_add_icon;
            this.btnAdd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnAdd.Location = new System.Drawing.Point(732, 1);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(45, 32);
            this.btnAdd.TabIndex = 20;
            this.toolTip1.SetToolTip(this.btnAdd, "Adicionar uma nova conexão");
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click += new System.EventHandler(this.BtnAdd_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(787, 261);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(92, 23);
            this.btnCancel.TabIndex = 21;
            this.btnCancel.Text = "Cancelar";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // lblAct
            // 
            this.lblAct.AutoSize = true;
            this.lblAct.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblAct.Location = new System.Drawing.Point(12, 213);
            this.lblAct.Name = "lblAct";
            this.lblAct.Size = new System.Drawing.Size(61, 15);
            this.lblAct.TabIndex = 22;
            this.lblAct.Text = "Descrição";
            this.lblAct.Click += new System.EventHandler(this.label9_Click);
            // 
            // View_ConfCon
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Lavender;
            this.ClientSize = new System.Drawing.Size(891, 365);
            this.Controls.Add(this.lblAct);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnEdt);
            this.Controls.Add(this.btnDel);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtPrt);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnSav);
            this.Controls.Add(this.ckbPrd);
            this.Controls.Add(this.cmbSBDB);
            this.Controls.Add(this.txtPwd);
            this.Controls.Add(this.txtUsr);
            this.Controls.Add(this.txtNomBD);
            this.Controls.Add(this.txtSvd);
            this.Controls.Add(this.txtDes);
            this.Controls.Add(this.gvDados);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "View_ConfCon";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Lista de Conexão de Banco de Dados";
            ((System.ComponentModel.ISupportInitialize)(this.gvDados)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BDFormbs)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DataGridView gvDados;
        private BindingSource bsGrid;
        private TextBox txtDes;
        private TextBox txtSvd;
        private TextBox txtNomBD;
        private TextBox txtUsr;
        private TextBox txtPwd;
        private ComboBox cmbSBDB;
        private CheckBox ckbPrd;
        private Button btnSav;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private TextBox txtPrt;
        private Label label8;
        private Button btnDel;
        private Button btnEdt;
        private Button btnAdd;
        private Button btnCancel;
        private BindingSource BDFormbs;
        private ToolTip toolTip1;
        private Label lblAct;
        private DataGridViewTextBoxColumn descricaoDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn serverDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn databaseDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn userDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn passwordDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn portDataGridViewTextBoxColumn;
        private DataGridViewCheckBoxColumn isProducaoDataGridViewCheckBoxColumn;
        private DataGridViewTextBoxColumn sGBDDataGridViewTextBoxColumn;
    }
}
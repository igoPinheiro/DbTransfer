namespace DbTransferWF
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.cmbBDOri = new System.Windows.Forms.ComboBox();
            this.bsBDOri = new System.Windows.Forms.BindingSource(this.components);
            this.bsBDDes = new System.Windows.Forms.BindingSource(this.components);
            this.txtSchOri = new System.Windows.Forms.TextBox();
            this.txtSchDes = new System.Windows.Forms.TextBox();
            this.cmbBDDes = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtTab = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.numCom = new System.Windows.Forms.NumericUpDown();
            this.txtFil = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.ckbInsert = new System.Windows.Forms.CheckBox();
            this.ckbUpdate = new System.Windows.Forms.CheckBox();
            this.btnIni = new System.Windows.Forms.Button();
            this.btnConfCon = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.lblpgs = new System.Windows.Forms.Label();
            this.rchtxtlog = new System.Windows.Forms.RichTextBox();
            this.label8 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.bsBDOri)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsBDDes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCom)).BeginInit();
            this.SuspendLayout();
            // 
            // cmbBDOri
            // 
            this.cmbBDOri.DataSource = this.bsBDOri;
            this.cmbBDOri.DisplayMember = "Descricao";
            this.cmbBDOri.FormattingEnabled = true;
            this.cmbBDOri.Location = new System.Drawing.Point(9, 29);
            this.cmbBDOri.Name = "cmbBDOri";
            this.cmbBDOri.Size = new System.Drawing.Size(308, 23);
            this.cmbBDOri.TabIndex = 0;
            this.cmbBDOri.ValueMember = "StringConexao";
            // 
            // bsBDOri
            // 
            this.bsBDOri.DataSource = typeof(DbTransferWF.Model.BancoDados01);
            // 
            // bsBDDes
            // 
            this.bsBDDes.DataSource = typeof(DbTransferWF.Model.BancoDados01);
            // 
            // txtSchOri
            // 
            this.txtSchOri.Location = new System.Drawing.Point(323, 28);
            this.txtSchOri.Name = "txtSchOri";
            this.txtSchOri.Size = new System.Drawing.Size(160, 23);
            this.txtSchOri.TabIndex = 1;
            this.txtSchOri.Text = "public";
            // 
            // txtSchDes
            // 
            this.txtSchDes.Location = new System.Drawing.Point(323, 92);
            this.txtSchDes.Name = "txtSchDes";
            this.txtSchDes.Size = new System.Drawing.Size(160, 23);
            this.txtSchDes.TabIndex = 3;
            this.txtSchDes.Text = "public";
            // 
            // cmbBDDes
            // 
            this.cmbBDDes.DataSource = this.bsBDDes;
            this.cmbBDDes.DisplayMember = "Descricao";
            this.cmbBDDes.FormattingEnabled = true;
            this.cmbBDDes.Location = new System.Drawing.Point(9, 92);
            this.cmbBDDes.Name = "cmbBDDes";
            this.cmbBDDes.Size = new System.Drawing.Size(308, 23);
            this.cmbBDDes.TabIndex = 2;
            this.cmbBDDes.ValueMember = "StringConexao";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(135, 15);
            this.label1.TabIndex = 4;
            this.label1.Text = "Banco de Dados Origem";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 74);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(135, 15);
            this.label2.TabIndex = 5;
            this.label2.Text = "Banco de Dados Destino";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(323, 11);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(92, 15);
            this.label3.TabIndex = 6;
            this.label3.Text = "Schema Origem";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(323, 74);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(92, 15);
            this.label4.TabIndex = 7;
            this.label4.Text = "Schema Destino";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(489, 11);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(40, 15);
            this.label5.TabIndex = 9;
            this.label5.Text = "Tabela";
            // 
            // txtTab
            // 
            this.txtTab.Location = new System.Drawing.Point(489, 28);
            this.txtTab.Name = "txtTab";
            this.txtTab.Size = new System.Drawing.Size(185, 23);
            this.txtTab.TabIndex = 8;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(680, 11);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(159, 15);
            this.label6.TabIndex = 11;
            this.label6.Text = "Commit a Cada N. Registros ";
            // 
            // numCom
            // 
            this.numCom.Location = new System.Drawing.Point(680, 29);
            this.numCom.Name = "numCom";
            this.numCom.Size = new System.Drawing.Size(159, 23);
            this.numCom.TabIndex = 12;
            this.numCom.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numCom.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numCom.ValueChanged += new System.EventHandler(this.numCom_ValueChanged);
            // 
            // txtFil
            // 
            this.txtFil.Location = new System.Drawing.Point(499, 152);
            this.txtFil.Multiline = true;
            this.txtFil.Name = "txtFil";
            this.txtFil.Size = new System.Drawing.Size(340, 280);
            this.txtFil.TabIndex = 13;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(499, 134);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(34, 15);
            this.label7.TabIndex = 14;
            this.label7.Text = "Filtro";
            // 
            // ckbInsert
            // 
            this.ckbInsert.AutoSize = true;
            this.ckbInsert.Checked = true;
            this.ckbInsert.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckbInsert.Location = new System.Drawing.Point(489, 73);
            this.ckbInsert.Name = "ckbInsert";
            this.ckbInsert.Size = new System.Drawing.Size(55, 19);
            this.ckbInsert.TabIndex = 15;
            this.ckbInsert.Text = "Insert";
            this.ckbInsert.UseVisualStyleBackColor = true;
            // 
            // ckbUpdate
            // 
            this.ckbUpdate.AutoSize = true;
            this.ckbUpdate.Checked = true;
            this.ckbUpdate.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckbUpdate.Location = new System.Drawing.Point(489, 98);
            this.ckbUpdate.Name = "ckbUpdate";
            this.ckbUpdate.Size = new System.Drawing.Size(64, 19);
            this.ckbUpdate.TabIndex = 16;
            this.ckbUpdate.Text = "Update";
            this.ckbUpdate.UseVisualStyleBackColor = true;
            // 
            // btnIni
            // 
            this.btnIni.Location = new System.Drawing.Point(559, 91);
            this.btnIni.Name = "btnIni";
            this.btnIni.Size = new System.Drawing.Size(142, 23);
            this.btnIni.TabIndex = 17;
            this.btnIni.Text = "Iniciar";
            this.btnIni.UseVisualStyleBackColor = true;
            this.btnIni.Click += new System.EventHandler(this.btnIni_Click);
            // 
            // btnConfCon
            // 
            this.btnConfCon.Location = new System.Drawing.Point(707, 91);
            this.btnConfCon.Name = "btnConfCon";
            this.btnConfCon.Size = new System.Drawing.Size(132, 23);
            this.btnConfCon.TabIndex = 18;
            this.btnConfCon.Text = "Configurar Conexão";
            this.btnConfCon.UseVisualStyleBackColor = true;
            this.btnConfCon.Click += new System.EventHandler(this.btnConfCon_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(499, 453);
            this.progressBar1.Maximum = 60;
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(340, 23);
            this.progressBar1.Step = 1;
            this.progressBar1.TabIndex = 19;
            // 
            // lblpgs
            // 
            this.lblpgs.AutoSize = true;
            this.lblpgs.Location = new System.Drawing.Point(499, 435);
            this.lblpgs.Name = "lblpgs";
            this.lblpgs.Size = new System.Drawing.Size(59, 15);
            this.lblpgs.TabIndex = 20;
            this.lblpgs.Text = "Progresso";
            // 
            // rchtxtlog
            // 
            this.rchtxtlog.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.rchtxtlog.Location = new System.Drawing.Point(9, 152);
            this.rchtxtlog.Name = "rchtxtlog";
            this.rchtxtlog.Size = new System.Drawing.Size(474, 327);
            this.rchtxtlog.TabIndex = 21;
            this.rchtxtlog.Text = "";
            this.rchtxtlog.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.rchtxtlog_KeyPress);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(12, 134);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(27, 15);
            this.label8.TabIndex = 22;
            this.label8.Text = "Log";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Lavender;
            this.ClientSize = new System.Drawing.Size(854, 486);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.rchtxtlog);
            this.Controls.Add(this.lblpgs);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.btnConfCon);
            this.Controls.Add(this.btnIni);
            this.Controls.Add(this.ckbUpdate);
            this.Controls.Add(this.ckbInsert);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtFil);
            this.Controls.Add(this.numCom);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtTab);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtSchDes);
            this.Controls.Add(this.cmbBDDes);
            this.Controls.Add(this.txtSchOri);
            this.Controls.Add(this.cmbBDOri);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(870, 525);
            this.MinimumSize = new System.Drawing.Size(870, 525);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "A9 - Data Tranfer";
            ((System.ComponentModel.ISupportInitialize)(this.bsBDOri)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsBDDes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCom)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ComboBox cmbBDOri;
        private TextBox txtSchOri;
        private TextBox txtSchDes;
        private ComboBox cmbBDDes;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private TextBox txtTab;
        private Label label6;
        private NumericUpDown numCom;
        private TextBox txtFil;
        private Label label7;
        private CheckBox ckbInsert;
        private CheckBox ckbUpdate;
        private Button btnIni;
        private Button btnConfCon;
        private BindingSource bsBDDes;
        private BindingSource bsBDOri;
        private ProgressBar progressBar1;
        private Label lblpgs;
        private RichTextBox rchtxtlog;
        private Label label8;
    }
}
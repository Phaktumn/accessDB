namespace SQLAccess
{
    partial class formMain
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
            this.ssInfo = new System.Windows.Forms.StatusStrip();
            this.toolStripProgress = new System.Windows.Forms.ToolStripProgressBar();
            this.toolStripConnectionStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.msMenu = new System.Windows.Forms.MenuStrip();
            this.connectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.connectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.queryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nomesAlunosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.todosAlunosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.todosDocentesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.infoAlunoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.alunoToolStripMenuItem = new System.Windows.Forms.ToolStripComboBox();
            this.complexQueryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addInfoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dgvData = new System.Windows.Forms.DataGridView();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.ssInfo.SuspendLayout();
            this.msMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).BeginInit();
            this.SuspendLayout();
            // 
            // ssInfo
            // 
            this.ssInfo.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripProgress,
            this.toolStripConnectionStatus});
            this.ssInfo.Location = new System.Drawing.Point(0, 438);
            this.ssInfo.Name = "ssInfo";
            this.ssInfo.Size = new System.Drawing.Size(617, 22);
            this.ssInfo.TabIndex = 0;
            this.ssInfo.Text = "Info Status Strip";
            // 
            // toolStripProgress
            // 
            this.toolStripProgress.Name = "toolStripProgress";
            this.toolStripProgress.Size = new System.Drawing.Size(100, 16);
            // 
            // toolStripConnectionStatus
            // 
            this.toolStripConnectionStatus.Name = "toolStripConnectionStatus";
            this.toolStripConnectionStatus.Size = new System.Drawing.Size(79, 17);
            this.toolStripConnectionStatus.Text = "Disconnected";
            // 
            // msMenu
            // 
            this.msMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.connectionToolStripMenuItem,
            this.queryToolStripMenuItem,
            this.addInfoToolStripMenuItem});
            this.msMenu.Location = new System.Drawing.Point(0, 0);
            this.msMenu.Name = "msMenu";
            this.msMenu.Size = new System.Drawing.Size(617, 24);
            this.msMenu.TabIndex = 1;
            this.msMenu.Text = "menuStrip1";
            // 
            // connectionToolStripMenuItem
            // 
            this.connectionToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.connectToolStripMenuItem});
            this.connectionToolStripMenuItem.Name = "connectionToolStripMenuItem";
            this.connectionToolStripMenuItem.Size = new System.Drawing.Size(67, 20);
            this.connectionToolStripMenuItem.Text = "Database";
            // 
            // connectToolStripMenuItem
            // 
            this.connectToolStripMenuItem.Name = "connectToolStripMenuItem";
            this.connectToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.connectToolStripMenuItem.Text = "Connect";
            this.connectToolStripMenuItem.Click += new System.EventHandler(this.connectToolStripMenuItem_Click);
            // 
            // queryToolStripMenuItem
            // 
            this.queryToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.nomesAlunosToolStripMenuItem,
            this.todosAlunosToolStripMenuItem,
            this.todosDocentesToolStripMenuItem,
            this.infoAlunoToolStripMenuItem,
            this.complexQueryToolStripMenuItem});
            this.queryToolStripMenuItem.Name = "queryToolStripMenuItem";
            this.queryToolStripMenuItem.Size = new System.Drawing.Size(51, 20);
            this.queryToolStripMenuItem.Text = "Query";
            // 
            // nomesAlunosToolStripMenuItem
            // 
            this.nomesAlunosToolStripMenuItem.Name = "nomesAlunosToolStripMenuItem";
            this.nomesAlunosToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.nomesAlunosToolStripMenuItem.Text = "Nomes Alunos";
            this.nomesAlunosToolStripMenuItem.Click += new System.EventHandler(this.nomesAlunosToolStripMenuItem_Click);
            // 
            // todosAlunosToolStripMenuItem
            // 
            this.todosAlunosToolStripMenuItem.Name = "todosAlunosToolStripMenuItem";
            this.todosAlunosToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.todosAlunosToolStripMenuItem.Text = "Todos Alunos";
            this.todosAlunosToolStripMenuItem.Click += new System.EventHandler(this.todosAlunosToolStripMenuItem_Click);
            // 
            // todosDocentesToolStripMenuItem
            // 
            this.todosDocentesToolStripMenuItem.Name = "todosDocentesToolStripMenuItem";
            this.todosDocentesToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.todosDocentesToolStripMenuItem.Text = "Todos Docentes";
            this.todosDocentesToolStripMenuItem.Click += new System.EventHandler(this.todosDocentesToolStripMenuItem_Click);
            // 
            // infoAlunoToolStripMenuItem
            // 
            this.infoAlunoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.alunoToolStripMenuItem});
            this.infoAlunoToolStripMenuItem.Name = "infoAlunoToolStripMenuItem";
            this.infoAlunoToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.infoAlunoToolStripMenuItem.Text = "Info Aluno";
            // 
            // alunoToolStripMenuItem
            // 
            this.alunoToolStripMenuItem.Name = "alunoToolStripMenuItem";
            this.alunoToolStripMenuItem.Size = new System.Drawing.Size(152, 23);
            this.alunoToolStripMenuItem.Text = "Aluno";
            this.alunoToolStripMenuItem.Click += new System.EventHandler(this.alunoToolStripMenuItem_Click);
            // 
            // complexQueryToolStripMenuItem
            // 
            this.complexQueryToolStripMenuItem.Name = "complexQueryToolStripMenuItem";
            this.complexQueryToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.complexQueryToolStripMenuItem.Text = "Complex Query";
            this.complexQueryToolStripMenuItem.Click += new System.EventHandler(this.complexQueryToolStripMenuItem_Click);
            // 
            // addInfoToolStripMenuItem
            // 
            this.addInfoToolStripMenuItem.Name = "addInfoToolStripMenuItem";
            this.addInfoToolStripMenuItem.Size = new System.Drawing.Size(65, 20);
            this.addInfoToolStripMenuItem.Text = "Add Info";
            this.addInfoToolStripMenuItem.Click += new System.EventHandler(this.addInfoToolStripMenuItem_Click);
            // 
            // dgvData
            // 
            this.dgvData.AllowUserToAddRows = false;
            this.dgvData.AllowUserToDeleteRows = false;
            this.dgvData.AllowUserToOrderColumns = true;
            this.dgvData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvData.Location = new System.Drawing.Point(0, 194);
            this.dgvData.Name = "dgvData";
            this.dgvData.ReadOnly = true;
            this.dgvData.Size = new System.Drawing.Size(617, 241);
            this.dgvData.TabIndex = 2;
            this.dgvData.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvData_CellContentClick);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(0, 28);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(617, 160);
            this.richTextBox1.TabIndex = 3;
            this.richTextBox1.Text = "";
            this.richTextBox1.TextChanged += new System.EventHandler(this.richTextBox1_TextChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(530, 1);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // formMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(617, 460);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.dgvData);
            this.Controls.Add(this.ssInfo);
            this.Controls.Add(this.msMenu);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.MainMenuStrip = this.msMenu;
            this.Name = "formMain";
            this.Text = "SQL Access";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.formMain_FormClosing);
            this.Load += new System.EventHandler(this.formMain_Load);
            this.ssInfo.ResumeLayout(false);
            this.ssInfo.PerformLayout();
            this.msMenu.ResumeLayout(false);
            this.msMenu.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.StatusStrip ssInfo;
        public System.Windows.Forms.MenuStrip msMenu;
        public System.Windows.Forms.ToolStripMenuItem connectionToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem connectToolStripMenuItem;
        public System.Windows.Forms.DataGridView dgvData;
        public System.Windows.Forms.ToolStripStatusLabel toolStripConnectionStatus;
        public System.Windows.Forms.ToolStripProgressBar toolStripProgress;
        public System.Windows.Forms.ToolStripMenuItem queryToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem nomesAlunosToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem todosAlunosToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem todosDocentesToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem addInfoToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem infoAlunoToolStripMenuItem;
        public System.Windows.Forms.ToolStripComboBox alunoToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem complexQueryToolStripMenuItem;
        public System.Windows.Forms.RichTextBox richTextBox1;
        public System.Windows.Forms.Button button1;
    }
}


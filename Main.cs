﻿using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Data.Sql;
using System.Drawing;
using System.Linq;
using System.Runtime.Versioning;
using System.Text;
using SQLAccess.Properties;

namespace SQLAccess
{
    public partial class formMain : Form
    {
        private const string ConnectionString = "Data Source=DESKTOP-8QB55IV;Initial Catalog=IES2016;Integrated Security=True";

        private SqlConnection connection;
        

        public formMain()
        {
            InitializeComponent();
            toolStripProgress.Maximum = 100;
            toolStripProgress.Minimum = 0;
            this.toolStripProgress.Style = ProgressBarStyle.Blocks;
            this.queryToolStripMenuItem.Enabled = false;
            this.button1.Text = Resources.formMain_formMain_Debug;
        }

        private void formMain_Load(object sender, EventArgs e)
        {
            connection = new SqlConnection();
            connection.ConnectionString = ConnectionString;
        }

        private void connectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                connection.Open();
                toolStripConnectionStatus.Text = Resources.formMain_connectToolStripMenuItem_Click_Connected;
                this.toolStripProgress.Step = 100;
                this.toolStripProgress.PerformStep();
                this.queryToolStripMenuItem.Enabled = true;
            }
            catch (Exception)
            {
                toolStripConnectionStatus.Text = Resources.formMain_connectToolStripMenuItem_Click_Connection_Failed;
            }
        }

        private void formMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            connection.Close();
        }

        public DataTable SendQuery(string query)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(query, connection);

            try
            {
                da.Fill(dt);
                toolStripConnectionStatus.Text = @"Query Succeded";
            }
            catch (Exception)
            {
                toolStripConnectionStatus.Text = @"Query Failed";
            }

            return dt;
        }

        private void dgvData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void nomesAlunosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string qString = "SELECT Aluno.Nome FROM Aluno";
            dgvData.DataSource = SendQuery(qString);
            CreateSqlText(qString);
        }

        private void todosAlunosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string qString = "SELECT * FROM Aluno";
            dgvData.DataSource = SendQuery(qString);
            CreateSqlText(qString);
        }


        private void todosDocentesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string qString = "SELECT * FROM Docente";
            dgvData.DataSource = SendQuery(qString);
            CreateSqlText(qString);
        }

        public string RetrieveAluno(string fName)
        {
            string aux = null;

            using (this.connection)
            {
                string oString = "Select * from Aluno where Aluno.Nome=@fName";
                SqlCommand oCmd = new SqlCommand(oString, connection);
                using (SqlDataReader oReader = oCmd.ExecuteReader())
                {
                    while (oReader.Read())
                    {
                        aux = oReader["Nome"].ToString();
                    }
                }
            }
            return aux;
        }

        private void addInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void alunoToolStripMenuItem_Click(object sender, EventArgs e)
        {
 
        }

        public void CreateSqlText(string qString)
        {
            richTextBox1.Clear();
            var spliter = qString.Split();
            for (int i = 0; i < spliter.Length; i++)
            {
                string word = spliter[i];
                string variable = word;
                if (string.Equals(variable, "FROM",
                    StringComparison.CurrentCultureIgnoreCase)){
                    variable = "\nFrom ";
                    spliter[i] = variable;
                }
                else if (string.Equals(variable, "Select",
                    StringComparison.CurrentCultureIgnoreCase)){
                    variable = "\nSelect ";
                    spliter[i] = variable;
                }
                else if (string.Equals(variable, "Where",
                    StringComparison.CurrentCultureIgnoreCase)){
                    variable = "\nWhere ";
                    spliter[i] = variable;
                }
                else if (string.Equals(variable, "And",
                    StringComparison.CurrentCultureIgnoreCase))
                {
                    variable = "\nAnd ";
                    spliter[i] = variable;
                }
                richTextBox1.Text += spliter[i];
            }
        }

        private void complexQueryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string qString ="select Aluno.Nome, Nota " +
            "from Aluno, Inscricao, DS, Disciplina " +
            "Where Inscricao.DSid = DS.DSid " +
            "and DS.DISCid = Disciplina.DISCid " +
            "and Aluno.Nid = Inscricao.Nid " +
            "and disciplina.nome = 'Armazenamento e Acesso a Dados' " +
            "and Nota = (select max(Nota) " +
            "from Aluno, Inscricao, DS, Disciplina " +
            "Where Inscricao.DSid = DS.DSid " +
            "and DS.DISCid = Disciplina.DISCid " +
            "and Aluno.Nid = Inscricao.Nid " +
            "and disciplina.nome = 'Armazenamento e Acesso a Dados')";
            dgvData.DataSource = SendQuery(qString);
            CreateSqlText(qString);
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            int selectCount = 0, specialCharCount = 0;
            bool isSubQuerry = false, endQuery = false;
            string customQuerySelectedText = null;
            customQuerySelectedText = richTextBox1.SelectedText;
            if (customQuerySelectedText == string.Empty)
            {
                string[] customQueryWords = richTextBox1.Text.Split();
                foreach (string word in customQueryWords)
                {
                    if (word.ToLower() == "select" && selectCount >= 1){
                        isSubQuerry = true;
                    }
                    if (word.ToLower() == "select"){
                        selectCount++;
                    }
                    customQuerySelectedText += word + " ";
                }
                dgvData.DataSource = SendQuery(customQuerySelectedText);
            }
            else{
                dgvData.DataSource = SendQuery(customQuerySelectedText);
            }                        
        }
    }
}
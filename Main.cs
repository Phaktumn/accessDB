using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using Microsoft.Win32.SafeHandles;
using SQLAccess.Properties;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace SQLAccess
{
    public partial class formMain : Form
    {
        public string ConnectionString = "Data Source=DESKTOP-8QB55IV;Initial Catalog=IES2016;Integrated Security=True";

        private SqlConnection connection;
        public string ServerName;
        public string UserId;
        public string Pwd;
        public string DatabaseName;
        private List<Intel> intelStuff;
        private Stream myStream;

        public formMain()
        {
            InitializeComponent();
            toolStripProgress.Maximum = 100;
            toolStripProgress.Minimum = 0;
            this.toolStripProgress.Style = ProgressBarStyle.Blocks;
            this.queryToolStripMenuItem.Enabled = false;
            this.button1.Text = Resources.formMain_formMain_Debug;

            openFileDialog1.FileName = "New Query.sql";
            openFileDialog1.Filter = "Server Filesb(*.sql)|*.sql|All Files(*.*)|*.* ";
        }

        private void formMain_Load(object sender, EventArgs e)
        {
            connection = new SqlConnection();
            //connection.ConnectionString = ConnectionString;

            string[] findG1 = { "Select", "From", "As", "Where", "Date", "DatePart" };
            string[] findG2 = { "Avg", "Count", "Sum", "Dateiff", "Max" };
            string[] findG3 = { "And" };
            intelStuff = new List<Intel>();

            foreach (string g1Word in findG1)
            {
                intelStuff.Add(new Intel
                {
                    intelWord = g1Word,
                    wordType = IntelType.Group1
                });
            }

            foreach (var g2Word in findG2)
            {
                intelStuff.Add(new Intel
                {
                    intelWord = g2Word,
                    wordType = IntelType.Group2
                });
            }

            foreach (var g3Word in findG3)
            {
                intelStuff.Add(new Intel
                {
                    intelWord = g3Word,
                    wordType = IntelType.Group3
                });
            }
        }

        private void connectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Login logForm = new Login(this.connection, this);
            logForm.Show();
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

        enum IntelType{
            Group1,
            Group2,
            Group3
        }

        private struct Intel
        {
            public string intelWord;
            public IntelType wordType;
        }

        private int currIndex = 0;

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            //Current Caret Position
            currIndex = richTextBox1.SelectionStart;
            foreach (Intel intel in intelStuff)
            {
                string intelWord = intel.intelWord.ToLower();
                string text = richTextBox1.Text.ToLower();

                if (text.Contains(intelWord))
                {
                    string matchString = Regex.Escape(intelWord);
                    foreach (Match match in Regex.Matches(text, matchString))
                    {
                        if (intel.wordType == IntelType.Group1)
                        {
                            richTextBox1.Select(match.Index, intelWord.Length);
                            richTextBox1.SelectionColor = Color.Blue;

                            richTextBox1.Select(currIndex, 0);
                            richTextBox1.SelectionColor = richTextBox1.ForeColor;
                        }
                        if (intel.wordType == IntelType.Group2)
                        {
                            richTextBox1.Select(match.Index, intelWord.Length);
                            richTextBox1.SelectionColor = Color.DeepPink;

                            richTextBox1.Select(currIndex, 0);
                            richTextBox1.SelectionColor = richTextBox1.ForeColor;
                        }
                        if (intel.wordType == IntelType.Group3)
                        {

                            richTextBox1.Select(match.Index, intelWord.Length);
                            richTextBox1.SelectionColor = Color.DarkGray;

                            richTextBox1.Select(currIndex, 0);
                            richTextBox1.SelectionColor = richTextBox1.ForeColor;
                        }
                    }
                }
            }
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

        private void newQueryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();

            using (myStream)
            {
                myStream = new FileStream(openFileDialog1.FileName, FileMode.Open);
                StreamReader reader = new StreamReader(myStream);
                richTextBox1.Text = reader.ReadToEnd();
                reader.Close();
                myStream.Close();
            }
        }

        private void openFileDialog1_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }
    }
}

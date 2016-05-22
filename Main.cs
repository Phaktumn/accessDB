using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using ScintillaNET;
using SQLAccess.Properties;

namespace SQLAccess
{
    public partial class formMain : Form
    {
        public string ConnectionString = 
            "Data Source=DESKTOP-8QB55IV;Initial Catalog=IES2016;Integrated Security=True";

        private SqlConnection connection;
        public string ServerName { get; set; }
        public string UserId { get; set; }
        public string Pwd { get; set; }
        public string DatabaseName { get; set; }
        private Stream myStream;
        private Login logForm;

        public formMain()
        {
            InitializeComponent();
            toolStripProgress.Maximum = 100;
            toolStripProgress.Minimum = 0;
            this.toolStripProgress.Style = ProgressBarStyle.Blocks;
            toolStripButton1.Text = Resources.formMain_formMain_Debug;

            openFileDialog1.FileName = "New Query.sql";
            openFileDialog1.Filter = @"Server Files (*.sql)|*.sql|All Files(*.*)|*.* ";

            saveFileDialog1.FileName = "NewQuery.sql";
            saveFileDialog1.Filter = @"Server Files (*.sql)|*.sql|All Files(*.*)|*.* ";
        }

        private void formMain_Load(object sender, EventArgs e)
        {
            connection = new SqlConnection();

            //connection.ConnectionString = ConnectionString;

            codeEditor.StyleResetDefault();
            codeEditor.Styles[ScintillaNET.Style.Default].Font = "Consolas";
            codeEditor.Styles[ScintillaNET.Style.Default].Size = 12;
            codeEditor.StyleClearAll();

            codeEditor.Styles[Style.Sql.Word].ForeColor = Color.FromArgb(86, 156, 214);
            codeEditor.Styles[Style.Sql.Word].Bold = true;
            codeEditor.Styles[Style.Sql.Identifier].ForeColor = Color.FromArgb(0, 0, 0);
            codeEditor.Styles[Style.Sql.Character].ForeColor = Color.FromArgb(203, 65, 65);
            codeEditor.Styles[Style.Sql.Number].ForeColor = Color.FromArgb(87, 166, 74);
            codeEditor.Styles[Style.Sql.Operator].ForeColor = Color.FromArgb(129, 129, 129);
            codeEditor.Styles[Style.Sql.Comment].ForeColor = Color.FromArgb(102, 116, 123);
            codeEditor.Styles[Style.Sql.CommentLine].ForeColor = Color.FromArgb(102, 116, 123);

            codeEditor.Lexer = Lexer.Sql;

            codeEditor.SetKeywords(0, SqlKeywords.Keywords);

            codeEditor.Margins[0].Width = 30;
        }

        private void connectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            logForm = new Login(this.connection, this);
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

        private void openFileDialog1_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            codeEditor.SelectAll();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            string customQuerySelectedText = null;
            customQuerySelectedText = codeEditor.SelectedText;
            dgvData.DataSource = SendQuery(customQuerySelectedText);
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            using (myStream)
            {
                try
                {
                    myStream = new FileStream(openFileDialog1.FileName, FileMode.Open);

                    StreamReader reader = new StreamReader(myStream);
                    codeEditor.Text = reader.ReadToEnd();
                    reader.Close();
                    myStream.Close();
                }
                catch (Exception)
                {
                    MessageBox.Show(Resources.formMain_newQueryToolStripMenuItem_Click_No_File_Selected);
                }
            }
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            saveFileDialog1.ShowDialog();
            saveFileDialog1.CreatePrompt = true;
          
            using (myStream)
            {
                try
                {
                    StreamWriter writer = new StreamWriter(saveFileDialog1.OpenFile());
                    writer.Write(codeEditor.Text);
                    writer.Close();
                }
                catch (Exception)
                {
                    MessageBox.Show(Resources.formMain_newQueryToolStripMenuItem_Click_No_File_Selected);
                }
            }
        }

        private void codeEditor_CharAdded(object sender, CharAddedEventArgs e)
        {
            var currentPos = codeEditor.CurrentPosition;
            var wordStartPos = codeEditor.WordStartPosition(currentPos, true);
            
            var lenEntered = currentPos - wordStartPos;
            if (lenEntered > 0)
            {
                codeEditor.AutoCShow(lenEntered, SqlKeywords.Keywords.ToUpper());
            }
        }


        private void toolStripComboBox1_Click(object sender, EventArgs e)
        {
            if(connection.State == ConnectionState.Closed) return;
            DatabaseName = toolStripComboBox1.SelectedItem.ToString();
            logForm.ChangeDatabase(toolStripComboBox1.SelectedItem.ToString());
            toolStripConnectionStatus.Text = $"{Resources.formMain_connectToolStripMenuItem_Click_Connected} " +
                    $"{DatabaseName} " +
                    $"{connection.ServerVersion}";
        }

        private void queriesToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void sELECTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void iNSERTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStripComboBox2.Items.Add(ListTables());
        }

        private void toolStripComboBox2_Click(object sender, EventArgs e)
        {

        }

        private IList<string> ListTables()
        {
            List<string> tables = new List<string>();
            DataTable table = connection.GetSchema("Tables");
            foreach (DataRow dataRow in table.Rows)
            {
                string data = (string)dataRow[2];
                tables.Add(data);
            }
            return tables;
        }
    }
}

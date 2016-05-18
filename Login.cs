using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SQLAccess.Properties;

namespace SQLAccess
{
    public partial class Login : Form
    {
        private SqlConnection connection;
        private formMain mainForm ;

        public Login(SqlConnection currConnection, Form main){
            InitializeComponent();
            mainForm = main as formMain;
            connection = currConnection;
            if (connection.State == ConnectionState.Open){
                Debug.Assert(mainForm != null, "mainForm != null");
                textBox1.Text = mainForm.ServerName;
                textBox2.Text = mainForm.DatabaseName;
                textBox3.Text = mainForm.UserId;
                textBox4.Text = mainForm.Pwd;
            }
            else
            {
                textBox1.Text = Resources.Login_Login_serverName_PlaceHolder;
                textBox2.Text = Resources.Login_Login_DatabaseName_PlaceHolder;
                textBox3.Text = Resources.Login_Login_user;
                textBox4.Text = @"root";
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            mainForm.ServerName = textBox1.Text;
            mainForm.DatabaseName = textBox2.Text;
            mainForm.UserId = textBox3.Text;
            mainForm.Pwd = textBox4.Text;
            string dataString = string.Format("Data Source={0};Initial Catalog={1};Integrated Security=True;User ID={2};Password={3}"
                                              , mainForm.ServerName, mainForm.DatabaseName, mainForm.UserId, mainForm.Pwd);
            if (connection.State == ConnectionState.Open){
                ChangeDatabase(mainForm.DatabaseName);
            }
            else{
                NewConnection(dataString);
            }
            Close();
        }

        private void NewConnection(string connectionString)
        {
            connection.ConnectionString = connectionString;
            try
            {
                connection.Open();
                mainForm.toolStripConnectionStatus.Text =
                    $"{Resources.formMain_connectToolStripMenuItem_Click_Connected} " +
                    $"{mainForm.DatabaseName} " +
                    $"{connection.ServerVersion}"; ;
                mainForm.toolStripProgress.Step = 100;
                mainForm.toolStripProgress.PerformStep();
                mainForm.queryToolStripMenuItem.Enabled = true;
            }
            catch (Exception)
            {
                mainForm.toolStripConnectionStatus.Text = 
                    Resources.formMain_connectToolStripMenuItem_Click_Connection_Failed;
            }
        }

        private void ChangeDatabase(string databaseName)
        {
            try
            {
                connection.ChangeDatabase(mainForm.DatabaseName);
                mainForm.toolStripConnectionStatus.Text =
                    $"{Resources.formMain_connectToolStripMenuItem_Click_Connected} " +
                    $"{mainForm.DatabaseName} " +
                    $"{connection.ServerVersion}"; ;
                mainForm.toolStripProgress.Step = 100;
                mainForm.toolStripProgress.PerformStep();
                mainForm.queryToolStripMenuItem.Enabled = true;
            }
            catch (Exception){
                MessageBox.Show(@"Failed to change Database.", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error,
                    MessageBoxDefaultButton.Button1);
            }
        }
    }
}

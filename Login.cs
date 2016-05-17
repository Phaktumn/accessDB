using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
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

        public Login(SqlConnection currConnection, Form Main){
            InitializeComponent();
            mainForm = Main as formMain;
            textBox3.Text = Resources.Login_Login_user;
            textBox4.Text = @"root";
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            mainForm.DatabaseName = textBox2.Text;
            mainForm.Pwd = textBox4.Text;

            try
            {
                string dataString = "Data Source="+ mainForm.ServerName +";" + "Initial Catalog=" + mainForm.DatabaseName +";Integrated Security=True";
                connection.Open();
                mainForm.toolStripConnectionStatus.Text = Resources.formMain_connectToolStripMenuItem_Click_Connected;
                mainForm.toolStripProgress.Step = 100;
                mainForm.toolStripProgress.PerformStep();
                mainForm.queryToolStripMenuItem.Enabled = true;
            }
            catch (Exception)
            {
                mainForm.toolStripConnectionStatus.Text = Resources.formMain_connectToolStripMenuItem_Click_Connection_Failed;
            }
        }
    }
}

﻿using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
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
                textBox3.Text = mainForm.UserId;
                textBox4.Text = mainForm.Pwd;
            }
            else
            {
                textBox1.Text = Resources.Login_Login_serverName_PlaceHolder;
                textBox3.Text = Resources.Login_Login_user;
                textBox4.Text = @"root";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            mainForm.ServerName = textBox1.Text;
            mainForm.UserId = textBox3.Text;
            mainForm.Pwd = textBox4.Text;
            string dataString = string.Format("Data Source={0};Integrated Security=True;User ID={1};Password={2}"
                ,mainForm.ServerName, mainForm.UserId, mainForm.Pwd);
            if (connection.State == ConnectionState.Open){
                ChangeDatabase(mainForm.DatabaseName);
            }
            else{
                NewConnection(dataString);
            }

            DataTable table = mainForm.SendQuery("Select name from sys.databases");
            mainForm.toolStripComboBox1.Items.Clear();
            foreach (DataRow row in table.Rows)
            {
                mainForm.toolStripComboBox1.Items.Add(row[0]);
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
                    $"{connection.ServerVersion}"; 
                mainForm.toolStripProgress.Step = 100;
                mainForm.toolStripProgress.PerformStep();
            }
            catch (Exception)
            {
                mainForm.toolStripConnectionStatus.Text = 
                    Resources.formMain_connectToolStripMenuItem_Click_Connection_Failed;
            }
        }

        public void ChangeDatabase(string databaseName)
        {
            try
            {
                connection.ChangeDatabase(databaseName);
                mainForm.toolStripConnectionStatus.Text =
                    $"{Resources.formMain_connectToolStripMenuItem_Click_Connected} " +
                    $"{mainForm.DatabaseName} " +
                    $"{connection.ServerVersion}"; ;
                mainForm.toolStripProgress.Step = 100;
                mainForm.toolStripProgress.PerformStep();
            }
            catch (Exception){
                MessageBox.Show(@"Failed to change Database.", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error,
                    MessageBoxDefaultButton.Button1);
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}

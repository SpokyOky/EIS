using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EIS
{
    public partial class FormChartOfAccounts : Form
    {
        private SQLiteConnection sql_con;
        private SQLiteCommand sql_cmd;
        private DataSet DS = new DataSet();
        private DataTable DT = new DataTable();
        private string sPath = Path.Combine(
            Application.StartupPath, Program.dbPath);

        public FormChartOfAccounts()
        {
            InitializeComponent();
        }

        private void chartOfAccounts_Load(object sender, EventArgs e)
        {
            string ConnectionString = 
                @"Data Source=" + sPath + ";New=False;Version=3";
            String selectCommand = "Select * from ChartOfAccounts";
            selectTable(ConnectionString, selectCommand);
        }

        public void selectTable(string ConnectionString, String selectCommand)
        {
            SQLiteConnection connect = 
                new SQLiteConnection(ConnectionString);
            connect.Open();

            SQLiteDataAdapter dataAdapter = 
                new SQLiteDataAdapter(selectCommand, connect);

            DataSet ds = new DataSet();
            dataAdapter.Fill(ds);

            dataGridView.DataSource = ds;
            dataGridView.DataMember = ds.Tables[0].ToString(); 
            try
            {
                dataGridView.Columns[1].HeaderCell.Value = "Счёт";
                dataGridView.Columns[2].HeaderCell.Value = "Описание";
                dataGridView.Columns[3].HeaderCell.Value = "Тип";
                dataGridView.Columns[4].HeaderCell.Value = "Субконто1";
                dataGridView.Columns[5].HeaderCell.Value = "Субконто2";
            }
            catch (Exception) { }
            connect.Close();
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EIS
{
    public partial class FormJournalEntries : Form
    {
        private int idJO = -1;
        public int IdJO { set { idJO = value; } }
        private SQLiteConnection sql_con;
        private SQLiteCommand sql_cmd;
        private DataSet DS = new DataSet();
        private DataTable DT = new DataTable();
        private static string sPath = Program.dbPath;
        private string standartSelectCommand = "select ID, Date, DT, SubkontoDT1, SubkontoDT2, KT, " +
            "SubkontoKT1, SubkontoKT2, Count, Sum, OperationID from JournalEntries";
        private string standartConnectionString = @"Data Source=" + sPath + ";New=False;Version=3";

        public FormJournalEntries()
        {
            InitializeComponent();
        }

        private void FormJournalEntries_Load(object sender, EventArgs e)
        {
            updateGrid();
        }

        public void updateGrid()
        {
            String selectCommand = "select ID, Date, DT, SubkontoDT1, SubkontoDT2, KT, " +
            "SubkontoKT1, SubkontoKT2, Count, Sum, OperationID from JournalEntries";
            if (!checkBoxAll.Checked)
            {
                selectCommand += " Where Date >= '" + Validation.DtS(dateTimePickerFrom.Value) +
                    "' and Date <= '" + Validation.DtS(dateTimePickerTo.Value) + "'";
            }
            if (idJO != -1)
            {
                if (!selectCommand.Contains("Where"))
                {
                    selectCommand += " Where ";
                }
                else
                {
                    selectCommand += " and ";
                }
                selectCommand += "OperationID = '" + idJO + "'";
            }
            selectTable(standartConnectionString, selectCommand);
        }

        public void selectTable(string ConnectionString, string selectCommand)
        {
            SQLiteConnection connect = new
           SQLiteConnection(ConnectionString);
            connect.Open();
            SQLiteDataAdapter dataAdapter = new
           SQLiteDataAdapter(selectCommand, connect);
            DataSet ds = new DataSet();
            dataAdapter.Fill(ds);
            dataGridView.DataSource = ds;
            dataGridView.DataMember = ds.Tables[0].ToString();
            connect.Close();
        }

        private void checkBoxAll_CheckedChanged(object sender, EventArgs e)
        {
            updateGrid();
            dateTimePickerFrom.Enabled = !checkBoxAll.Checked;
            dateTimePickerTo.Enabled = !checkBoxAll.Checked;
        }

        private void dateTimePickerFrom_ValueChanged(object sender, EventArgs e)
        {
            updateGrid();
        }

        private void dateTimePickerTo_ValueChanged(object sender, EventArgs e)
        {
            updateGrid();
        }
    }
}

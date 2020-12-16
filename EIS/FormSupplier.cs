using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EIS
{
    public partial class FormSupplier : Form
    {
        private SQLiteConnection sql_con;
        private SQLiteCommand sql_cmd;
        private DataSet DS = new DataSet();
        private DataTable DT = new DataTable();
        private static string sPath = Program.dbPath;

        private string prevName = Validation.NameStandart;
        private string prevPhone = Validation.PhoneStandart;

        private string standartSelectCommand = "Select * from Supplier";
        private string standartConnectionString = @"Data Source=" + sPath + ";New=False;Version=3";

        public FormSupplier()
        {
            InitializeComponent();
        }

        private void FormSupplier_Load(object sender, EventArgs e)
        {
            selectTable(standartConnectionString, standartSelectCommand);
            toolStripTextBoxName.Text = prevName;
            toolStripTextBoxPhone.Text = prevPhone;
        }

        private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {
            string txtSQLQuery = "insert into Supplier (Name, Phone) values ('" +
                toolStripTextBoxName.Text + "', '" + toolStripTextBoxPhone.Text + "')";
            ExecuteQuery(txtSQLQuery);
            refreshForm(standartConnectionString, standartSelectCommand);
        }

        private void ExecuteQuery(string txtQuery)
        {
            sql_con = new SQLiteConnection("Data Source=" + sPath +
           ";Version=3;New=False;Compress=True;");
            sql_con.Open();
            sql_cmd = sql_con.CreateCommand();
            sql_cmd.CommandText = txtQuery;
            sql_cmd.ExecuteNonQuery();
            sql_con.Close();
        }

        public void refreshForm(string ConnectionString, string selectCommand)
        {
            selectTable(ConnectionString, selectCommand);
            dataGridView.Update();
            dataGridView.Refresh();
            toolStripTextBoxName.Text = prevName = Validation.NameStandart;
            toolStripTextBoxPhone.Text = prevPhone = Validation.PhoneStandart;
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
            dataGridView.Columns[1].HeaderCell.Value = "Имя";
            dataGridView.Columns[2].HeaderCell.Value = "Телефон";
            connect.Close();
        }

        private void toolStripButtonDelete_Click(object sender, EventArgs e)
        {
            int CurrentRow = dataGridView.SelectedCells[0].RowIndex;
            string valueId = dataGridView[0, CurrentRow].Value.ToString();
            string selectCommand = "delete from Supplier where ID=" + valueId;
            changeValue(standartConnectionString, selectCommand);

            refreshForm(standartConnectionString, standartSelectCommand);
        }

        public void changeValue(string ConnectionString, string selectCommand)
        {
            SQLiteConnection connect = new
                SQLiteConnection(ConnectionString);
            connect.Open();
            SQLiteTransaction trans;
            SQLiteCommand cmd = new SQLiteCommand();
            trans = connect.BeginTransaction();
            cmd.Connection = connect;
            cmd.CommandText = selectCommand;
            cmd.ExecuteNonQuery();
            trans.Commit();
            connect.Close();
        }

        private void toolStripButtonEdit_Click(object sender, EventArgs e)
        {
            int CurrentRow = dataGridView.SelectedCells[0].RowIndex;
            string valueId = dataGridView[0, CurrentRow].Value.ToString();
            string changeName = toolStripTextBoxName.Text;
            string selectCommand = "update Supplier set Name='" + changeName + "' where ID = " + valueId;
            changeValue(standartConnectionString, selectCommand);
            string changePhone = toolStripTextBoxPhone.Text;
            selectCommand = "update Supplier set Phone='" + changePhone + "' where ID = " + valueId;
            changeValue(standartConnectionString, selectCommand);

            refreshForm(standartConnectionString, standartSelectCommand);

        }

        private void dataGridView_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int CurrentRow = dataGridView.SelectedCells[0].RowIndex;
            string NameId = dataGridView[1, CurrentRow].Value.ToString();
            toolStripTextBoxName.Text = NameId;
            string PhoneId = dataGridView[2, CurrentRow].Value.ToString();
            toolStripTextBoxPhone.Text = PhoneId;
        }

        private void toolStripTextBoxName_TextChanged(object sender, EventArgs e)
        {
            if (!Validation.isName(toolStripTextBoxName.Text))
            {
                toolStripTextBoxName.Text = prevName;
            }
            else
            {
                prevName = toolStripTextBoxName.Text;
            }
        }

        private void toolStripTextBoxPhone_TextChanged(object sender, EventArgs e)
        {
            if (!Validation.isPhone(toolStripTextBoxPhone.Text))
            {
                toolStripTextBoxPhone.Text = prevPhone;
            }
            else
            {
                prevPhone = toolStripTextBoxPhone.Text;
            }
        }
    }
}

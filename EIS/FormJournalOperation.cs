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
    public partial class FormJournalOperation : Form
    {
        private SQLiteConnection sql_con;
        private SQLiteCommand sql_cmd;
        private DataSet DS = new DataSet();
        private DataTable DT = new DataTable();
        private static string sPath = Program.dbPath;

        private string prevDate = Validation.DateStandart;
        private string prevType = Validation.NameStandart;
        private string prevDesc = Validation.TextStandart;
        private string prevCount = Validation.NumberStandart;

        private string standartSelectCommand = "Select JO.ID, JO.Date, JO.Type, JO.Description, JO.Count, S.Number AS Series, E.Name AS Employee " +
                "from JournalOperation JO " +
                "Join Series S On JO.SeriesID = S.ID " +
                "Join Employee E On JO.EmployeeID = E.ID";
        private string standartConnectionString = @"Data Source=" + sPath + ";New=False;Version=3";

        public FormJournalOperation()
        {
            InitializeComponent();
        }

        private void FormJournalOperation_Load(object sender, EventArgs e)
        {
            selectTable(standartConnectionString, standartSelectCommand);
            string selectSupplier = "SELECT ID, Number FROM Series";
            selectCombo(standartConnectionString, selectSupplier, toolStripComboBoxSeries, "Number", "ID");
            toolStripComboBoxSeries.SelectedIndex = -1;
            string selectProduct = "SELECT ID, Name FROM Employee";
            selectCombo(standartConnectionString, selectProduct, toolStripComboBoxEmployee, "Name", "ID");
            toolStripComboBoxEmployee.SelectedIndex = -1;

            toolStripTextBoxType.Text = prevType;
            dateTimePicker.Value = Validation.StD(prevDate);
            toolStripTextBoxDescription.Text = prevDesc;
            toolStripTextBoxCount.Text = prevCount;
        }

        public void selectCombo(string ConnectionString, string selectCommand,
ToolStripComboBox comboBox, string displayMember, string valueMember)
        {
            SQLiteConnection connect = new
           SQLiteConnection(ConnectionString);
            connect.Open();
            SQLiteDataAdapter dataAdapter = new
           SQLiteDataAdapter(selectCommand, connect);
            DataSet ds = new DataSet();
            dataAdapter.Fill(ds);
            comboBox.ComboBox.DataSource = ds.Tables[0];
            comboBox.ComboBox.DisplayMember = displayMember;
            comboBox.ComboBox.ValueMember = valueMember;
            connect.Close();
        }

        private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {
            if (toolStripComboBoxSeries.Text == "")
            {
                MessageBox.Show("Выберите серию");
                return;
            }
            if (toolStripComboBoxEmployee.Text == "")
            {
                MessageBox.Show("Выберите исполнителя");
                return;
            }

            string txtSQLQuery = "insert into JournalOperation (Date, Type, Description, Count, SeriesID, EmployeeID) values ('" +
       Validation.DtS(dateTimePicker.Value) + "', '" + toolStripTextBoxType.Text + "','" + toolStripTextBoxDescription.Text + "','" +
       toolStripTextBoxCount.Text + "','" + toolStripComboBoxSeries.ComboBox.SelectedValue + "','" + toolStripComboBoxEmployee.ComboBox.SelectedValue + "')";
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
            dateTimePicker.Value = Validation.StD(Validation.DateStandart);
            prevDate = Validation.DateStandart;
            toolStripTextBoxType.Text = prevType = Validation.NameStandart;
            toolStripTextBoxDescription.Text = prevDesc = Validation.TextStandart;
            toolStripTextBoxCount.Text = prevCount = Validation.NumberStandart;

            toolStripComboBoxSeries.SelectedIndex = -1;
            toolStripComboBoxEmployee.SelectedIndex = -1;
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

        private void toolStripButtonDelete_Click(object sender, EventArgs e)
        {
            int CurrentRow = dataGridView.SelectedCells[0].RowIndex;
            string valueId = dataGridView[0, CurrentRow].Value.ToString();
            string selectCommand = "delete from JournalOperation where ID=" + valueId;
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
            if (toolStripComboBoxSeries.Text == "")
            {
                MessageBox.Show("Выберите серию");
                return;
            }
            if (toolStripComboBoxEmployee.Text == "")
            {
                MessageBox.Show("Выберите исполнителя");
                return;
            }
            int CurrentRow = dataGridView.SelectedCells[0].RowIndex;
            string valueId = dataGridView[0, CurrentRow].Value.ToString();
            string changeDate = toolStripTextBoxCount.Text;
            string selectCommand = "update JournalOperation set Date='" + changeDate + "' where ID = " + valueId;
            changeValue(standartConnectionString, selectCommand);
            string changeType = toolStripTextBoxType.Text;
            selectCommand = "update JournalOperation set Type='" + changeType + "' where ID = " + valueId;
            changeValue(standartConnectionString, selectCommand);
            string changeDescription = toolStripTextBoxDescription.Text;
            selectCommand = "update JournalOperation set Description='" + changeDescription + "' where ID = " + valueId;
            changeValue(standartConnectionString, selectCommand);
            string changeCount = Validation.DtS(dateTimePicker.Value);
            selectCommand = "update JournalOperation set Count='" + changeCount + "' where ID = " + valueId;
            changeValue(standartConnectionString, selectCommand);
            string changeSeries = toolStripComboBoxSeries.ComboBox.SelectedValue.ToString();
            selectCommand = "update JournalOperation set SeriesID='" + changeSeries + "' where ID = " + valueId;
            changeValue(standartConnectionString, selectCommand);
            string changeEmployee = toolStripComboBoxEmployee.ComboBox.SelectedValue.ToString();
            selectCommand = "update JournalOperation set EmployeeID='" + changeEmployee + "' where ID = " + valueId;
            changeValue(standartConnectionString, selectCommand);

            refreshForm(standartConnectionString, standartSelectCommand);
        }

        private void dataGridView_CellMouseClick(object sender,
    DataGridViewCellMouseEventArgs e)
        {
            int CurrentRow = dataGridView.SelectedCells[0].RowIndex;
            string DateId = dataGridView[1, CurrentRow].Value.ToString();
            dateTimePicker.Value = Validation.StD(DateId);
            string TypeId = dataGridView[2, CurrentRow].Value.ToString().Replace(',','.');
            toolStripTextBoxType.Text = TypeId;
            string DescriptionId = dataGridView[3, CurrentRow].Value.ToString();
            toolStripTextBoxDescription.Text = DescriptionId;
            string CountId = dataGridView[4, CurrentRow].Value.ToString();
            toolStripTextBoxCount.Text = CountId;
            string SeriesId = dataGridView[5, CurrentRow].Value.ToString();
            toolStripComboBoxSeries.Text = SeriesId;
            string EmployeeId = dataGridView[6, CurrentRow].Value.ToString();
            toolStripComboBoxEmployee.Text = EmployeeId;
        }

        private void toolStripTextBoxType_TextChanged(object sender, EventArgs e)
        {
            if (Validation.isName(toolStripTextBoxType.Text))
            {
                prevType = toolStripTextBoxType.Text;
            }
            else
            {
                toolStripTextBoxType.Text = prevType;
            }
        }

        private void toolStripTextBoxDescription_TextChanged(object sender, EventArgs e)
        {
            if (Validation.isText(toolStripTextBoxDescription.Text, 100))
            {
                prevDesc = toolStripTextBoxDescription.Text;
            }
            else
            {
                toolStripTextBoxDescription.Text = prevDesc;
            }
        }

        private void toolStripTextBoxCount_TextChanged(object sender, EventArgs e)
        {
            if (Validation.isNumber(toolStripTextBoxCount.Text))
            {
                prevCount = toolStripTextBoxCount.Text;
            }
            else
            {
                toolStripTextBoxCount.Text = prevCount;
            }
        }
    }
}
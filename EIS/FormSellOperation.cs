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
    public partial class FormSellOperation : Form
    {
        private int idJO = -1;
        private SQLiteConnection sql_con;
        private SQLiteCommand sql_cmd;
        private DataSet DS = new DataSet();
        private DataTable DT = new DataTable();
        private static string sPath = Program.dbPath;

        private string prevCount = Validation.NumberStandart;
        private string prevDate = Validation.DateStandart;

        private string standartConnectionString = @"Data Source=" + sPath + ";New=False;Version=3";

        public FormSellOperation()
        {
            InitializeComponent();
        }

        public FormSellOperation(int idJO)
        {
            this.idJO = idJO;
            InitializeComponent();
        }

        private void FormSellOperation_Load(object sender, EventArgs e)
        {
            string selectProduct = "SELECT ID, Name FROM Product";
            selectCombo(standartConnectionString, selectProduct, comboBoxProduct, "Name", "ID");

            string selectEmployee = "SELECT ID, Name FROM Employee";
            selectCombo(standartConnectionString, selectEmployee, comboBoxEmployee, "Name", "ID");

            textBoxCount.Text = prevCount;
            dateTimePicker.Value = DateTime.Parse(prevDate);

            if (idJO == -1)
            {
                comboBoxProduct.SelectedIndex = -1;
                comboBoxEmployee.SelectedIndex = -1;
            }
            else
            {
                string selectCommand = "SELECT SeriesID FROM JournalOperation WHERE ID = '" + idJO + "'";
                int seriesId = Convert.ToInt32(selectValue(standartConnectionString, selectCommand));
                selectCommand = "SELECT ProductID FROM Series WHERE ID = '" + seriesId + "'";
                int productId = Convert.ToInt32(selectValue(standartConnectionString, selectCommand));
                selectCommand = "SELECT EmployeeID FROM JournalOperation WHERE ID = '" + idJO + "'";
                int employeeId = Convert.ToInt32(selectValue(standartConnectionString, selectCommand));
                comboBoxProduct.SelectedIndex = -1;
                comboBoxProduct.SelectedValue = productId;
                comboBoxEmployee.SelectedValue = employeeId;
                comboBoxSeries.SelectedValue = seriesId;
                selectCommand = "SELECT Count FROM JournalOperation WHERE ID = '" + idJO + "'";
                textBoxCount.Text = Convert.ToString(selectValue(standartConnectionString, selectCommand));
            }
        }

        public object selectValue(string ConnectionString, String selectCommand)
        {
            SQLiteConnection connect = new SQLiteConnection(ConnectionString);
            connect.Open();
            SQLiteCommand command = new SQLiteCommand(selectCommand, connect);
            SQLiteDataReader reader = command.ExecuteReader();
            object value = "";
            while (reader.Read())
            {
                value = reader[0];
            }
            connect.Close();
            return value;
        }

        public void selectCombo(string ConnectionString, string selectCommand,
ComboBox comboBox, string displayMember, string valueMember)
        {
            SQLiteConnection connect = new
           SQLiteConnection(ConnectionString);
            connect.Open();
            SQLiteDataAdapter dataAdapter = new
           SQLiteDataAdapter(selectCommand, connect);
            DataSet ds = new DataSet();
            dataAdapter.Fill(ds);
            comboBox.DataSource = ds.Tables[0];
            comboBox.DisplayMember = displayMember;
            comboBox.ValueMember = valueMember;
            connect.Close();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (comboBoxProduct.Text == "")
            {
                MessageBox.Show("Выберите продукт");
                return;
            }
            if (comboBoxSeries.Text == "")
            {
                MessageBox.Show("Выберите серию");
                return;
            }
            if (comboBoxEmployee.Text == "")
            {
                MessageBox.Show("Выберите сотрудника");
                return;
            }

            string type = "Продажа товаров";
            string desc = "Продажа товаров";

            string selectCommand = "select LimitDate from Series where Number = '" + comboBoxSeries.Text + "'";
            object limitDate = selectValue(standartConnectionString, selectCommand);
            if (dateTimePicker.Value > Convert.ToDateTime(limitDate))
            {
                MessageBox.Show("Просроченный товар, нельзя продать");
                return;
            }

            selectCommand = "select Sum(Count) from JournalEntries where SubkontoDT1 = '"
                + comboBoxProduct.Text + "' and SubkontoDT2 = '" + comboBoxSeries.Text + "' " +
                "and Date <= '" + Validation.DtS(dateTimePicker.Value) + "'";
            object dtCount = selectValue(standartConnectionString, selectCommand);
            selectCommand = "select Sum(Count) from JournalEntries where SubkontoKT1 = '"
                + comboBoxProduct.Text + "' and SubkontoKT2 = '" + comboBoxSeries.Text + "' " +
                "and Date <= '" + Validation.DtS(dateTimePicker.Value) + "'";
            object ktCount = selectValue(standartConnectionString, selectCommand);
            if (dtCount == DBNull.Value)
            {
                dtCount = 0;
            }
            if (ktCount == DBNull.Value)
            {
                ktCount = 0;
            }

            int remains = Convert.ToInt32(dtCount) - Convert.ToInt32(ktCount);
            if (remains < Convert.ToInt32(textBoxCount.Text))
            {
                MessageBox.Show("НЕ ПРОДАМ, остаток меньше");
                return;
            }
            string selectLimitDate = "select LimitDate from Series where ID = '" + comboBoxSeries.SelectedValue + "'";
            DateTime LimitDate = Convert.ToDateTime(selectValue(standartConnectionString, selectLimitDate));
            if (dateTimePicker.Value > LimitDate)
            {
                MessageBox.Show("НЕ ПРОДАМ, просрочка");
                return;
            }

            string selectRoznPrice = "select RoznPrice from Series where ID = '" + comboBoxSeries.SelectedValue + "'";
            double roznPrice = Convert.ToDouble(selectValue(standartConnectionString, selectRoznPrice));

            string selectZakupPrice = "select Price from Series where ID = '" + comboBoxSeries.SelectedValue + "'";
            double zakupPrice = Convert.ToDouble(selectValue(standartConnectionString, selectZakupPrice));

            double sumProv1 = zakupPrice * Convert.ToInt32(textBoxCount.Text);
            double sumProv2 = roznPrice * Convert.ToInt32(textBoxCount.Text);

            if (idJO == -1)
            {
                string SQLQuery = "insert into JournalOperation (Date, Type, Description, Count, SeriesID, EmployeeID) values ('" +
       Validation.DtS(dateTimePicker.Value) + "', '" + type + "','" + desc + "','" + textBoxCount.Text + "','" +
       comboBoxSeries.SelectedValue + "','" + comboBoxEmployee.SelectedValue + "')";
                ExecuteQuery(SQLQuery);

                string selectID = "select max(id) from JournalOperation";
                idJO = Convert.ToInt32(selectValue(standartConnectionString, selectID));

                SQLQuery = "insert into JournalEntries (Date, DT, SubkontoDT1, SubkontoDT2, KT, " +
                    "SubkontoKT1, SubkontoKT2, Count, Sum, OperationID) values ('" +
                    Validation.DtS(dateTimePicker.Value) + "', '90', '', '', '41', '" +
                    comboBoxProduct.Text + "', '" + comboBoxSeries.Text + "', '" +
                    textBoxCount.Text + "', '" + sumProv1 + "', '" + idJO + "')";
                ExecuteQuery(SQLQuery);

                SQLQuery = "insert into JournalEntries (Date, DT, SubkontoDT1, SubkontoDT2, KT, " +
                    "SubkontoKT1, SubkontoKT2, Count, Sum, OperationID, Comment) values ('" +
                    Validation.DtS(dateTimePicker.Value) + "', '50', '', '', '90', '', '', '" +
                    textBoxCount.Text + "', '" + sumProv2 + "', '" + idJO + "', '" + comboBoxEmployee.Text + "')";
                ExecuteQuery(SQLQuery);
            }
            else
            {
                string updateDate = "update JournalOperation set Date = '" + Validation.DtS(dateTimePicker.Value) + "' where ID = '" + idJO + "'";
                changeValue(standartConnectionString, updateDate);
                string updateType = "update JournalOperation set Type = '" + type + "' where ID = '" + idJO + "'";
                changeValue(standartConnectionString, updateType);
                string updateDesc = "update JournalOperation set Description = '" + desc + "' where ID = '" + idJO + "'";
                changeValue(standartConnectionString, updateDesc);
                string updateCount = "update JournalOperation set Count = '" + textBoxCount.Text + "' where ID = '" + idJO + "'";
                changeValue(standartConnectionString, updateCount);
                string updateSeries = "update JournalOperation set SeriesID = '" + comboBoxSeries.SelectedValue + "' where ID = '" + idJO + "'";
                changeValue(standartConnectionString, updateSeries);
                string updateEmployee = "update JournalOperation set EmployeeID = '" + comboBoxEmployee.SelectedValue + "' where ID = '" + idJO + "'";
                changeValue(standartConnectionString, updateEmployee);

                string updateDateJE = "update JournalEntries set Date = '" + Validation.DtS(dateTimePicker.Value) + "' where OperationID = '" + idJO + "'";
                changeValue(standartConnectionString, updateDateJE);
                string updateSubkontoKT1 = "update JournalEntries set SubkontoKT1 = '" + comboBoxProduct.Text + "' where OperationID = '" + idJO + "' and KT = '41'";
                changeValue(standartConnectionString, updateSubkontoKT1);
                string updateSubkontoKT2 = "update JournalEntries set SubkontoKT2 = '" + comboBoxSeries.Text + "' where OperationID = '" + idJO + "' and KT = '41'";
                changeValue(standartConnectionString, updateSubkontoKT2);
                string updateCountJE = "update JournalEntries set Count = '" + textBoxCount.Text + "' where OperationID = '" + idJO + "'";
                changeValue(standartConnectionString, updateCountJE);
                string updateSumJE = "update JournalEntries set Sum = '" + sumProv1 + "' where OperationID = '" + idJO + "'";
                changeValue(standartConnectionString, updateSumJE);
            }
            MessageBox.Show("Сохранено");
            Close();
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

        private void comboBoxProduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxProduct.SelectedIndex < 0)
            {
                return;
            }
            string selectSeries = "SELECT ID, Number FROM Series WHERE ProductID='" + comboBoxProduct.SelectedValue + "'";
            selectCombo(standartConnectionString, selectSeries, comboBoxSeries, "Number", "ID");
            comboBoxSeries.SelectedIndex = -1;
        }

        private void textBoxCount_TextChanged(object sender, EventArgs e)
        {
            if (!Validation.isNumber(textBoxCount.Text))
            {
                textBoxCount.Text = prevCount;
            }
            else
            {
                prevCount= textBoxCount.Text;
            }
        }
    }
}

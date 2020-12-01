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
    public partial class FormAddOperation : Form
    {
        private int idJO = -1;
        private SQLiteConnection sql_con;
        private SQLiteCommand sql_cmd;
        private DataSet DS = new DataSet();
        private DataTable DT = new DataTable();
        private static string sPath = Program.dbPath;

        private string prevCount = Validation.NumberStandart;
        private string prevZakupPrice = Validation.PriceStandart;
        private string prevDate = Validation.DateStandart;

        private string standartConnectionString = @"Data Source=" + sPath + ";New=False;Version=3";

        public FormAddOperation()
        {
            InitializeComponent();
        }

        public FormAddOperation(int idJO)
        {
            this.idJO = idJO;
            InitializeComponent();
        }

        private void FormAddOperation_Load(object sender, EventArgs e)
        {
            string selectProduct = "SELECT ID, Name FROM Product";
            selectCombo(standartConnectionString, selectProduct, comboBoxProduct, "Name", "ID");

            string selectEmployee = "SELECT ID, Name FROM Employee";
            selectCombo(standartConnectionString, selectEmployee, comboBoxEmployee, "Name", "ID");

            textBoxCount.Text = prevCount;
            textBoxZakupPrice.Text = prevZakupPrice;
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
                selectCommand = "SELECT SupplierID FROM Series WHERE ID = '" + seriesId + "'";
                int supplierId = Convert.ToInt32(selectValue(standartConnectionString, selectCommand));
                selectCommand = "SELECT EmployeeID FROM JournalOperation WHERE ID = '" + idJO + "'";
                int employeeId = Convert.ToInt32(selectValue(standartConnectionString, selectCommand));
                comboBoxProduct.SelectedIndex = -1;
                comboBoxProduct.SelectedValue = productId;
                comboBoxEmployee.SelectedValue = employeeId;
                comboBoxSeries.SelectedValue = seriesId;
                comboBoxSupplier.SelectedValue = supplierId;
                selectCommand = "SELECT Count FROM JournalOperation WHERE ID = '" + idJO + "'";
                textBoxCount.Text = Convert.ToString(selectValue(standartConnectionString, selectCommand));

                selectCommand = "SELECT Price FROM Series WHERE ID = '" + seriesId + "'";
                textBoxZakupPrice.Text = Convert.ToDouble(selectValue(standartConnectionString, selectCommand)).ToString().Replace(',', '.');
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
            if (comboBoxSupplier.Text == "")
            {
                MessageBox.Show("Выберите поставщика");
                return;
            }
            if (comboBoxEmployee.Text == "")
            {
                MessageBox.Show("Выберите сотрудника");
                return;
            }
            string type = "Поступление серии";
            string desc = "Поступление очередной серии товаров";
            double sumProv = Convert.ToDouble(textBoxCount.Text) + Convert.ToDouble(textBoxZakupPrice.Text);


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
                    Validation.DtS(dateTimePicker.Value) + "', '41', '" + comboBoxProduct.Text +
                    "', '" + comboBoxSeries.Text + "', '60', '" + comboBoxSupplier.Text + "', '" +
                    textBoxCount.Text + "', '" + sumProv + "', '" + idJO + "')";
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
                string updateSubkontoDT1 = "update JournalEntries set SubkontoDT1 = '" + comboBoxProduct.Text + "' where OperationID = '" + idJO + "'";
                changeValue(standartConnectionString, updateSubkontoDT1);
                string updateSubkontoDT2 = "update JournalEntries set SubkontoDT2 = '" + comboBoxSeries.Text + "' where OperationID = '" + idJO + "'";
                changeValue(standartConnectionString, updateSubkontoDT2);
                string updateSubkontoKT1 = "update JournalEntries set SubkontoKT1 = '" + comboBoxSupplier.Text + "' where OperationID = '" + idJO + "'";
                changeValue(standartConnectionString, updateSubkontoKT1);
                string updateCountJE= "update JournalEntries set Count = '" + textBoxCount.Text + "' where OperationID = '" + idJO + "'";
                changeValue(standartConnectionString, updateCountJE);
                string updateSumJE = "update JournalEntries set Sum = '" + sumProv + "' where OperationID = '" + idJO + "'";
                changeValue(standartConnectionString, updateSumJE);

            }
            string changeZakupPrice = textBoxZakupPrice.Text;
            string txtSQLQuery = "update Series set Price='" + changeZakupPrice + "' where ID = '" + comboBoxSeries.SelectedValue + "'";
            ExecuteQuery(txtSQLQuery);

            string changeRoznPrice = textBoxRoznPrice.Text;
            txtSQLQuery = "update Series set RoznPrice='" + changeRoznPrice + "' where ID = '" + comboBoxSeries.SelectedValue + "'";
            ExecuteQuery(txtSQLQuery);
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

        private void comboBoxSeries_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxSeries.SelectedIndex < 0)
            {
                return;
            }
            string selectSupplier = "SELECT ID, Name FROM Supplier";
            selectCombo(standartConnectionString, selectSupplier, comboBoxSupplier, "Name", "ID");
            comboBoxSupplier.SelectedIndex = -1;
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

        private void textBoxZakupPrice_TextChanged(object sender, EventArgs e)
        {
            if (!Validation.isPrice(textBoxZakupPrice.Text))
            {
                textBoxZakupPrice.Text = prevZakupPrice;
            }
            else
            {
                prevZakupPrice = textBoxZakupPrice.Text;
                textBoxRoznPrice.Text = Convert.ToString(Convert.ToDouble(prevZakupPrice.Replace(".", ",")) * 1.5);
            }
        }

        private void comboBoxProduct_TextChanged(object sender, EventArgs e)
        {
            comboBoxProduct_SelectedIndexChanged(sender, e);
        }
    }
}

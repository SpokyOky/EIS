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

        private void FormAddOperation_Load(object sender, EventArgs e)
        {
            string selectProduct = "SELECT ID, Name FROM Product";
            selectCombo(standartConnectionString, selectProduct, comboBoxProduct, "Name", "ID");
            comboBoxProduct.SelectedIndex = -1;

            string selectEmployee = "SELECT ID, Name FROM Employee";
            selectCombo(standartConnectionString, selectEmployee, comboBoxEmployee, "Name", "ID");
            comboBoxEmployee.SelectedIndex = -1;

            textBoxCount.Text = prevCount;
            textBoxZakupPrice.Text = prevZakupPrice;
            dateTimePicker.Value = DateTime.Parse(prevDate);
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
            string txtSQLQuery = "insert into JournalOperation (Date, Type, Description, Count, SeriesID, EmployeeID) values ('" +
       Validation.DtS(dateTimePicker.Value) + "', '" + type + "','" + desc + "','" + textBoxCount.Text + "','" +
       comboBoxSeries.SelectedValue + "','" + comboBoxEmployee.SelectedValue + "')";
            ExecuteQuery(txtSQLQuery);

            string changeZakupPrice = textBoxZakupPrice.Text;
            txtSQLQuery = "update Series set Price='" + changeZakupPrice + "' where ID = '" + comboBoxSeries.SelectedIndex + "'";
            ExecuteQuery(txtSQLQuery);

            string changeRoznPrice = textBoxRoznPrice.Text;
            txtSQLQuery = "update Series set RoznPrice='" + changeRoznPrice + "' where ID = '" + comboBoxSeries.SelectedIndex + "'";
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
            string selectSeries = "SELECT ID, Number FROM Series WHERE ProductID='" + comboBoxProduct.SelectedIndex + "'";
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
    }
}

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
    public partial class FormSeries : Form
    {
        private SQLiteConnection sql_con;
        private SQLiteCommand sql_cmd;
        private DataSet DS = new DataSet();
        private DataTable DT = new DataTable();
        private static string sPath = Program.dbPath;

        private string prevNumber = Validation.NumberStandart;
        private string prevPrice = Validation.PriceStandart;
        private string prevLimitDate = Validation.DateStandart;

        private string standartSelectCommand = "Select Ser.ID, Ser.Number, Ser.Price, Ser.LimitDate, Sup.Name AS Supplier, P.Name AS Product " +
                "from Series Ser " +
                "Join Supplier Sup On Ser.SupplierID = Sup.ID " +
                "Join Product P On Ser.ProductID = P.ID";
        private string standartConnectionString = @"Data Source=" + sPath + ";New=False;Version=3";

        public FormSeries()
        {
            InitializeComponent();
        }

        private void FormSeries_Load(object sender, EventArgs e)
        {
            selectTable(standartConnectionString, standartSelectCommand);
            string selectSupplier = "SELECT ID, Name FROM Supplier";
            selectCombo(standartConnectionString, selectSupplier, toolStripComboBoxSupplier, "Name", "ID");
            toolStripComboBoxSupplier.SelectedIndex = -1;
            string selectProduct = "SELECT ID, Name FROM Product";
            selectCombo(standartConnectionString, selectProduct, toolStripComboBoxProduct, "Name", "ID");
            toolStripComboBoxProduct.SelectedIndex = -1;

            toolStripTextBoxNumber.Text = prevNumber;
            toolStripTextBoxPrice.Text = prevPrice;
            toolStripTextBoxLimitDate.Text = prevLimitDate;
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
            if (toolStripComboBoxSupplier.Text == "")
            {
                MessageBox.Show("Выберите поставщика");
                return;
            }
            if (toolStripComboBoxProduct.Text == "")
            {
                MessageBox.Show("Выберите товар");
                return;
            }

            string txtSQLQuery = "insert into Series (Number, Price, LimitDate, SupplierID, ProductID) values ('" +
       toolStripTextBoxNumber.Text + "', '" + toolStripTextBoxPrice.Text + "','" + toolStripTextBoxLimitDate.Text + "','" + 
       toolStripComboBoxSupplier.ComboBox.SelectedValue + "','" + toolStripComboBoxProduct.ComboBox.SelectedValue + "')";
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
            toolStripTextBoxNumber.Text = prevNumber = Validation.NumberStandart;
            toolStripTextBoxPrice.Text = prevPrice = Validation.PriceStandart;
            toolStripTextBoxLimitDate.Text = prevLimitDate = Validation.DateStandart;
            toolStripComboBoxSupplier.SelectedIndex = -1;
            toolStripComboBoxProduct.SelectedIndex = -1;
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
            string selectCommand = "delete from Series where ID=" + valueId;
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
            if (toolStripComboBoxSupplier.Text == "")
            {
                MessageBox.Show("Выберите подразделение");
                return;
            }
            if (toolStripComboBoxProduct.Text == "")
            {
                MessageBox.Show("Выберите товар");
                return;
            }
            int CurrentRow = dataGridView.SelectedCells[0].RowIndex;
            string valueId = dataGridView[0, CurrentRow].Value.ToString();
            string changeNumber = toolStripTextBoxNumber.Text;
            string selectCommand = "update Series set Number='" + changeNumber + "' where ID = " + valueId;
            changeValue(standartConnectionString, selectCommand);
            string changePrice = toolStripTextBoxPrice.Text;
            selectCommand = "update Series set Price='" + changePrice + "' where ID = " + valueId;
            changeValue(standartConnectionString, selectCommand);
            string changeLimitDate = toolStripTextBoxLimitDate.Text;
            selectCommand = "update Series set LimitDate='" + changeLimitDate + "' where ID = " + valueId;
            changeValue(standartConnectionString, selectCommand);
            string changeSupplier = toolStripComboBoxSupplier.ComboBox.SelectedValue.ToString();
            selectCommand = "update Series set SupplierID='" + changeSupplier + "' where ID = " + valueId;
            changeValue(standartConnectionString, selectCommand);
            string changeProduct = toolStripComboBoxProduct.ComboBox.SelectedValue.ToString();
            selectCommand = "update Series set ProductID='" + changeProduct + "' where ID = " + valueId;
            changeValue(standartConnectionString, selectCommand);

            refreshForm(standartConnectionString, standartSelectCommand);
        }

        private void dataGridView_CellMouseClick(object sender,
    DataGridViewCellMouseEventArgs e)
        {
            int CurrentRow = dataGridView.SelectedCells[0].RowIndex;
            string NumberId = dataGridView[1, CurrentRow].Value.ToString();
            toolStripTextBoxNumber.Text = NumberId;
            string PriceId = dataGridView[2, CurrentRow].Value.ToString().Replace(',','.');
            toolStripTextBoxPrice.Text = PriceId;
            string LimitDateId = dataGridView[3, CurrentRow].Value.ToString();
            toolStripTextBoxLimitDate.Text = LimitDateId;
            string SupplierId = dataGridView[4, CurrentRow].Value.ToString();
            toolStripComboBoxSupplier.Text = SupplierId;
            string ProductId = dataGridView[5, CurrentRow].Value.ToString();
            toolStripComboBoxProduct.Text = ProductId;
        }

        private void toolStripTextBoxNumber_TextChanged(object sender, EventArgs e)
        {
            if (!Validation.isNumber(toolStripTextBoxNumber.Text))
            {
                toolStripTextBoxNumber.Text = prevNumber;
            }
            else
            {
                prevNumber = toolStripTextBoxNumber.Text;
            }
        }

        private void toolStripTextBoxPrice_TextChanged(object sender, EventArgs e)
        {
            if (!Validation.isPrice(toolStripTextBoxPrice.Text))
            {
                toolStripTextBoxPrice.Text = prevPrice;
            }
            else
            {
                prevPrice= toolStripTextBoxPrice.Text;
            }
        }

        private void toolStripTextBoxLimitDate_TextChanged(object sender, EventArgs e)
        {
            if (!Validation.isDate(toolStripTextBoxLimitDate.Text))
            {
                toolStripTextBoxLimitDate.Text = prevLimitDate;
            }
            else
            {
                prevLimitDate = toolStripTextBoxLimitDate.Text;
            }
        }
    }
}
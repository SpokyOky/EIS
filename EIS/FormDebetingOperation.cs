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
    public partial class FormDebetingOperation : Form
    {
        private int? idJO;
        private SQLiteConnection sql_con;
        private SQLiteCommand sql_cmd;
        private DataSet DS = new DataSet();
        private DataTable DT = new DataTable();
        private static string sPath = Program.dbPath;

        private string prevDate = Validation.DateStandart;

        private string standartConnectionString = @"Data Source=" + sPath + ";New=False;Version=3";

        public FormDebetingOperation()
        {
            InitializeComponent();
        }

        public FormDebetingOperation(int idJO)
        {
            this.idJO = idJO;
            InitializeComponent();
        }

        private void FormDebetingOperation_Load(object sender, EventArgs e)
        {
            dateTimePicker.Value = DateTime.Parse(prevDate);
            string selectSeries = "select ID, Number from Series where LimitDate <= '" + Validation.DtS(dateTimePicker.Value) + "'";
            selectCombo(standartConnectionString, selectSeries, comboBoxSeries, "Number", "ID");
            if (idJO == null)
            {

            }
            else
            {
                string selectCommand = "SELECT SeriesID FROM JournalOperation WHERE ID = '" + idJO + "'";
                int seriesId = Convert.ToInt32(selectValue(standartConnectionString, selectCommand));
                comboBoxSeries.SelectedIndex = -1;
                comboBoxSeries.SelectedValue = seriesId;
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
            if (comboBoxSeries.Text == "")
            {
                MessageBox.Show("Выберите серию");
                return;
            }

            string type = "Списание просроченных товаров";
            string desc = "Списание просроченных товаров";

            if (idJO == null)
            {

            }
            else
            {

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

        private void dateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            string selectSeries = "select ID, Number from Series where LimitDate <= '" + Validation.DtS(dateTimePicker.Value) + "'";
            selectCombo(standartConnectionString, selectSeries, comboBoxSeries, "Number", "ID");
        }
    }
}

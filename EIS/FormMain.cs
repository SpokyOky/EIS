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
    public partial class FormMain : Form
    {
        private SQLiteConnection sql_con;
        private SQLiteCommand sql_cmd;
        private DataSet DS = new DataSet();
        private DataTable DT = new DataTable();
        private static string sPath = Program.dbPath;

        private string standartSelectCommand = "Select JO.ID, JO.Date, JO.Type, JO.Description, JO.Count, S.Number AS Series, E.Name AS Employee, P.Name AS Product " +
               "from JournalOperation JO " +
               "Join Series S On JO.SeriesID = S.ID " +
               "Join Employee E On JO.EmployeeID = E.ID " +
               "Join Product P On S.ProductID = P.ID";
        private string standartConnectionString = @"Data Source=" + sPath + ";New=False;Version=3";

        public FormMain()
        {
            InitializeComponent();
            selectTable(standartConnectionString, standartSelectCommand);
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
            dataGridView.Columns[0].Visible = false;
            connect.Close();
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

        public void refreshForm(string ConnectionString, string selectCommand)
        {
            selectTable(ConnectionString, selectCommand);
            dataGridView.Update();
            dataGridView.Refresh();
        }

        private void планСчетовToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new FormChartOfAccounts().Show();
        }

        private void сотрудникиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new FormEmployee().Show();
        }

        private void поставщикиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new FormSupplier().Show();
        }

        private void товарToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new FormProduct().Show();
        }

        private void серииToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new FormSeries().Show();
        }

        private void журналПроводокToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new FormJournalEntries();
            form.Show();
        }

        private void поступлениеТоваровToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new FormAddOperation();
            form.FormClosed += (object s, FormClosedEventArgs args) =>
            {
                selectTable(standartConnectionString, standartSelectCommand);
            };
            form.Show();
        }

        private void продажаТоваровToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new FormSellOperation();
            form.FormClosed += (object s, FormClosedEventArgs args) =>
            {
                selectTable(standartConnectionString, standartSelectCommand);
            };
            form.Show();
        }

        private void списаниеТоваровToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new FormDebetingOperation();
            form.FormClosed += (object s, FormClosedEventArgs args) =>
            {
                selectTable(standartConnectionString, standartSelectCommand);
            };
            form.Show();
        }

        private void редактированиеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form form = new Form();
            if ("Поступление серии" == Convert.ToString(dataGridView.SelectedRows[0].Cells[2].Value))
            {
                form = new FormAddOperation(Convert.ToInt32(dataGridView.SelectedRows[0].Cells[0].Value));
            }else if ("Продажа товаров" == Convert.ToString(dataGridView.SelectedRows[0].Cells[2].Value))
            {
                form = new FormSellOperation(Convert.ToInt32(dataGridView.SelectedRows[0].Cells[0].Value));
            }
            form.FormClosed += (object s, FormClosedEventArgs args) =>
            {
                selectTable(standartConnectionString, standartSelectCommand);
            };
            form.Show();
        }

        private void удалитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int CurrentRow = dataGridView.SelectedCells[0].RowIndex;
            string valueId = dataGridView[0, CurrentRow].Value.ToString();
            string selectCommand = "delete from JournalOperation where ID=" + valueId;
            changeValue(standartConnectionString, selectCommand);
            refreshForm(standartConnectionString, standartSelectCommand);
        }
    }
}
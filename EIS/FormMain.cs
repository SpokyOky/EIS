using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EIS
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
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

        private void журналОперацийToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new FormJournalOperation().Show();
        }
    }
}

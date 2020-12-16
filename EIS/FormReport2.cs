using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EIS
{
    public partial class FormReport2 : Form
    {
        string itogo = "";
        private SQLiteConnection sql_con;
        private SQLiteCommand sql_cmd;
        private DataSet DS = new DataSet();
        private DataTable DT = new DataTable();
        private static string sPath = Program.dbPath;

        private string standartConnectionString = @"Data Source=" + sPath + ";New=False;Version=3";

        public FormReport2()
        {
            InitializeComponent();
            textBoxEmail.Text = Validation.EmailStandart;
        }

        private void updateTable()
        {
            string dateFrom = Validation.DtS(dateTimePickerFrom.Value.Date);
            string dateTo = Validation.DtS(dateTimePickerTo.Value.AddDays(1).Date);

            string standartSelectCommand = "select P.ID, P.Name, S.Number, S.LimitDate, JO.Date, " +
                "(select SUM(JE.Count) from JournalEntries JE where SubkontoKT2 = S.Number and DT = '44' and KT = '41') AS Count, " +
                "(select SUM(JE.Sum) from JournalEntries JE where SubkontoKT2 = S.Number and DT = '44' and KT = '41') AS Sum " +
                "from JournalEntries JE " +
                "join JournalOperation JO on JE.OperationID = JO.ID " +
                "join Series S on JO.SeriesID = S.ID " +
                "join Product P on S.ProductID = P.ID " +
                "where JE.Date > '" + dateFrom + "' and JE.Date < '" + dateTo + "' " +
                "group by S.Number";

            labelSum.Text = "Итого: ";
            itogo = "";

            if (dateTimePickerFrom.Value.Date > dateTimePickerTo.Value.Date)
            {
                MessageBox.Show("Дата начала периода должна быть меньше либо равна дате конца периода");
                return;
            }
            selectTable(standartConnectionString, standartSelectCommand);

            for (int i = 5; i < dataGridView1.Columns.Count; i++)
            {
                double sum = 0;
                for (int j = 0; j < dataGridView1.Rows.Count; j++)
                {
                    if (dataGridView1.Rows[j].Cells[i].Value != DBNull.Value)
                    {
                        if (dataGridView1.Rows[j].Cells[i].Value != null)
                        {
                            if (dataGridView1.Rows[j].Cells[i].Value != "")
                            {
                                sum += Convert.ToDouble(dataGridView1.Rows[j].Cells[i].Value);
                            }
                        }
                    }
                }
                itogo += sum + " ";
            }

            labelSum.Text += itogo;
        }

        public void selectTable(string ConnectionString, String selectCommand)
        {
            SQLiteConnection connect = new
           SQLiteConnection(ConnectionString);
            connect.Open();
            SQLiteDataAdapter dataAdapter = new
           SQLiteDataAdapter(selectCommand, connect);
            DataSet ds = new DataSet();
            dataAdapter.Fill(ds);
            dataGridView1.DataSource = ds;
            dataGridView1.DataMember = ds.Tables[0].ToString();
            try
            {
                dataGridView1.Columns[0].HeaderCell.Value = "Код товара";
                dataGridView1.Columns[1].HeaderCell.Value = "Название товара";
                dataGridView1.Columns[2].HeaderCell.Value = "Номер серии";
                dataGridView1.Columns[3].HeaderCell.Value = "Предельная дата реализации";
                dataGridView1.Columns[4].HeaderCell.Value = "Дата операции";
                dataGridView1.Columns[5].HeaderCell.Value = "Количество";
                dataGridView1.Columns[6].HeaderCell.Value = "Сумма";
            }catch(Exception) { }
            connect.Close();
        }

        private void buttonPDF_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog
            {
                Filter = "pdf|*.pdf"
            };
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string FONT_LOCATION = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "arial.TTF"); //определяем В СИСТЕМЕ(чтобы не копировать файл) расположение шрифта arial.ttf
                    BaseFont baseFont = BaseFont.CreateFont(FONT_LOCATION, BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED); //создаем шрифт
                    iTextSharp.text.Font fontParagraph = new iTextSharp.text.Font(baseFont, 17, iTextSharp.text.Font.NORMAL); //регистрируем + можно задать параметры для него(17 - размер, последний параметр - стиль)
                    string title = "";

                    title = "Список списанных товаров, с " + Validation.DtS(dateTimePickerFrom.Value) + " по " + Validation.DtS(dateTimePickerTo.Value) + "\n\n";

                    var phraseTitle = new Phrase(title,
                    new iTextSharp.text.Font(baseFont, 18, iTextSharp.text.Font.BOLD));
                    Paragraph paragraph = new
                    Paragraph(phraseTitle)
                    {
                        Alignment = Element.ALIGN_CENTER,
                        SpacingAfter = 12
                    };

                    PdfPTable table = new PdfPTable(dataGridView1.Columns.Count);

                    for (int i = 0; i < dataGridView1.Columns.Count; i++)
                    {
                        table.AddCell(new Phrase(dataGridView1.Columns[i].HeaderCell.Value.ToString(), fontParagraph));
                    }
                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    {
                        for (int j = 0; j < dataGridView1.Columns.Count; j++)
                        {
                            table.AddCell(new Phrase(dataGridView1.Rows[i].Cells[j].Value.ToString(), fontParagraph));
                        }
                    }
                    PdfPTable table2 = new PdfPTable(dataGridView1.Columns.Count);
                    List<string> words = new List<string>();

                    words.Add("Итого:");
                    words.Add("");
                    words.Add("");
                    words.Add("");
                    words.Add("");
                    words.AddRange(itogo.Split(' '));
                    for (int j = 0; j < words.Count; j++)
                    {
                        table2.AddCell(new Phrase(words[j], fontParagraph));
                    }
                    using (FileStream stream = new FileStream(sfd.FileName, FileMode.Create))
                    {
                        iTextSharp.text.Document pdfDoc = new iTextSharp.text.Document(PageSize.A2, 10f, 10f, 10f, 0f);
                        PdfWriter.GetInstance(pdfDoc, stream);
                        pdfDoc.Open();
                        pdfDoc.Add(paragraph);
                        pdfDoc.Add(table);
                        pdfDoc.Add(table2);
                        pdfDoc.Close();
                        stream.Close();
                    }
                    string FileName = sfd.FileName;
                    string mailAddress = textBoxEmail.Text;
                    if (!string.IsNullOrEmpty(mailAddress))
                    {
                        if (Regex.IsMatch(mailAddress, @"^(?("")(""[^""]+?""@)|(([0-9a-z]((\.(?!\.))|[-
!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9az][-\w]*[0-9a-z]*\.)+[a-z0-9]{2,17}))$"))
                        {
                            MessageBox.Show("Неверный формат для электронной почты", "Ошибка",
                           MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        else
                        {
                            SendEmailForClients(mailAddress, "Отчеты:", "", FileName);
                        }
                    }

                    MessageBox.Show("Выполнено", "Успех", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
                   MessageBoxIcon.Error);
                }
            }
        }

        private void SendEmailForClients(string mailAddress, string subject, string text, string attachmentPath)
        {

            System.Net.Mail.MailMessage m = new System.Net.Mail.MailMessage();
            SmtpClient smtpClient = null;
            try
            {
                m.From = new MailAddress(ConfigurationManager.AppSettings["MailLogin"]);
                m.To.Add(new MailAddress(mailAddress));
                m.Subject = subject;
                m.Body = text;
                m.SubjectEncoding = System.Text.Encoding.UTF8;
                m.BodyEncoding = System.Text.Encoding.UTF8;
                m.Attachments.Add(new Attachment(attachmentPath));
                smtpClient = new SmtpClient("smtp.gmail.com", 587);
                smtpClient.UseDefaultCredentials = false;
                smtpClient.EnableSsl = true;
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtpClient.Credentials = new NetworkCredential(
                    ConfigurationManager.AppSettings["MailLogin"],
                    ConfigurationManager.AppSettings["MailPassword"]
                    );
                smtpClient.Send(m);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                m = null;
                smtpClient = null;
            }
        }

        private void dateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            updateTable();
        }

        private void dateTimePickerTo_ValueChanged(object sender, EventArgs e)
        {
            updateTable();
        }
    }
}

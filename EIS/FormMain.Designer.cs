namespace EIS
{
    partial class FormMain
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.справочникиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.товарToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.поставщикиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.сотрудникиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.серииToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.планСчетовToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.журналПроводокToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.отчётToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.проданныеТоварыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.списанныеТоварыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.оборотносальдоваяВедомостьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.журналОперацийToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.поступлениеТоваровToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.продажаТоваровToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.списаниеТоваровToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.изменениеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.редактированиеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.удалитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.посмотретьПроводкиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.отчётыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.поданныеЗаСменуТоварыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.списанныеТоварыToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.оборотносальдоваяВедомостьToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.справочникиToolStripMenuItem,
            this.журналПроводокToolStripMenuItem,
            this.отчётToolStripMenuItem,
            this.журналОперацийToolStripMenuItem,
            this.изменениеToolStripMenuItem,
            this.отчётыToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(927, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // справочникиToolStripMenuItem
            // 
            this.справочникиToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.товарToolStripMenuItem,
            this.поставщикиToolStripMenuItem,
            this.сотрудникиToolStripMenuItem,
            this.серииToolStripMenuItem,
            this.планСчетовToolStripMenuItem});
            this.справочникиToolStripMenuItem.Name = "справочникиToolStripMenuItem";
            this.справочникиToolStripMenuItem.Size = new System.Drawing.Size(94, 20);
            this.справочникиToolStripMenuItem.Text = "Справочники";
            // 
            // товарToolStripMenuItem
            // 
            this.товарToolStripMenuItem.Name = "товарToolStripMenuItem";
            this.товарToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.товарToolStripMenuItem.Text = "Товары";
            this.товарToolStripMenuItem.Click += new System.EventHandler(this.товарToolStripMenuItem_Click);
            // 
            // поставщикиToolStripMenuItem
            // 
            this.поставщикиToolStripMenuItem.Name = "поставщикиToolStripMenuItem";
            this.поставщикиToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.поставщикиToolStripMenuItem.Text = "Поставщики";
            this.поставщикиToolStripMenuItem.Click += new System.EventHandler(this.поставщикиToolStripMenuItem_Click);
            // 
            // сотрудникиToolStripMenuItem
            // 
            this.сотрудникиToolStripMenuItem.Name = "сотрудникиToolStripMenuItem";
            this.сотрудникиToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.сотрудникиToolStripMenuItem.Text = "Сотрудники";
            this.сотрудникиToolStripMenuItem.Click += new System.EventHandler(this.сотрудникиToolStripMenuItem_Click);
            // 
            // серииToolStripMenuItem
            // 
            this.серииToolStripMenuItem.Name = "серииToolStripMenuItem";
            this.серииToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.серииToolStripMenuItem.Text = "Серии";
            this.серииToolStripMenuItem.Click += new System.EventHandler(this.серииToolStripMenuItem_Click);
            // 
            // планСчетовToolStripMenuItem
            // 
            this.планСчетовToolStripMenuItem.Name = "планСчетовToolStripMenuItem";
            this.планСчетовToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.планСчетовToolStripMenuItem.Text = "План счетов";
            this.планСчетовToolStripMenuItem.Click += new System.EventHandler(this.планСчетовToolStripMenuItem_Click);
            // 
            // журналПроводокToolStripMenuItem
            // 
            this.журналПроводокToolStripMenuItem.Name = "журналПроводокToolStripMenuItem";
            this.журналПроводокToolStripMenuItem.Size = new System.Drawing.Size(119, 20);
            this.журналПроводокToolStripMenuItem.Text = "Журнал проводок";
            this.журналПроводокToolStripMenuItem.Click += new System.EventHandler(this.журналПроводокToolStripMenuItem_Click);
            // 
            // отчётToolStripMenuItem
            // 
            this.отчётToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.проданныеТоварыToolStripMenuItem,
            this.списанныеТоварыToolStripMenuItem,
            this.оборотносальдоваяВедомостьToolStripMenuItem});
            this.отчётToolStripMenuItem.Name = "отчётToolStripMenuItem";
            this.отчётToolStripMenuItem.Size = new System.Drawing.Size(51, 20);
            this.отчётToolStripMenuItem.Text = "Отчёт";
            // 
            // проданныеТоварыToolStripMenuItem
            // 
            this.проданныеТоварыToolStripMenuItem.Name = "проданныеТоварыToolStripMenuItem";
            this.проданныеТоварыToolStripMenuItem.Size = new System.Drawing.Size(252, 22);
            this.проданныеТоварыToolStripMenuItem.Text = "Проданные товары";
            // 
            // списанныеТоварыToolStripMenuItem
            // 
            this.списанныеТоварыToolStripMenuItem.Name = "списанныеТоварыToolStripMenuItem";
            this.списанныеТоварыToolStripMenuItem.Size = new System.Drawing.Size(252, 22);
            this.списанныеТоварыToolStripMenuItem.Text = "Списанные товары";
            // 
            // оборотносальдоваяВедомостьToolStripMenuItem
            // 
            this.оборотносальдоваяВедомостьToolStripMenuItem.Name = "оборотносальдоваяВедомостьToolStripMenuItem";
            this.оборотносальдоваяВедомостьToolStripMenuItem.Size = new System.Drawing.Size(252, 22);
            this.оборотносальдоваяВедомостьToolStripMenuItem.Text = "Оборотно-сальдовая ведомость";
            // 
            // журналОперацийToolStripMenuItem
            // 
            this.журналОперацийToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.поступлениеТоваровToolStripMenuItem,
            this.продажаТоваровToolStripMenuItem,
            this.списаниеТоваровToolStripMenuItem});
            this.журналОперацийToolStripMenuItem.Name = "журналОперацийToolStripMenuItem";
            this.журналОперацийToolStripMenuItem.Size = new System.Drawing.Size(120, 20);
            this.журналОперацийToolStripMenuItem.Text = "Журнал операций";
            // 
            // поступлениеТоваровToolStripMenuItem
            // 
            this.поступлениеТоваровToolStripMenuItem.Name = "поступлениеТоваровToolStripMenuItem";
            this.поступлениеТоваровToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
            this.поступлениеТоваровToolStripMenuItem.Text = "Поступление товаров";
            this.поступлениеТоваровToolStripMenuItem.Click += new System.EventHandler(this.поступлениеТоваровToolStripMenuItem_Click);
            // 
            // продажаТоваровToolStripMenuItem
            // 
            this.продажаТоваровToolStripMenuItem.Name = "продажаТоваровToolStripMenuItem";
            this.продажаТоваровToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
            this.продажаТоваровToolStripMenuItem.Text = "Продажа товаров";
            this.продажаТоваровToolStripMenuItem.Click += new System.EventHandler(this.продажаТоваровToolStripMenuItem_Click);
            // 
            // списаниеТоваровToolStripMenuItem
            // 
            this.списаниеТоваровToolStripMenuItem.Name = "списаниеТоваровToolStripMenuItem";
            this.списаниеТоваровToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
            this.списаниеТоваровToolStripMenuItem.Text = "Списание товаров";
            this.списаниеТоваровToolStripMenuItem.Click += new System.EventHandler(this.списаниеТоваровToolStripMenuItem_Click);
            // 
            // изменениеToolStripMenuItem
            // 
            this.изменениеToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.редактированиеToolStripMenuItem,
            this.удалитьToolStripMenuItem,
            this.посмотретьПроводкиToolStripMenuItem});
            this.изменениеToolStripMenuItem.Name = "изменениеToolStripMenuItem";
            this.изменениеToolStripMenuItem.Size = new System.Drawing.Size(70, 20);
            this.изменениеToolStripMenuItem.Text = "Действия";
            // 
            // редактированиеToolStripMenuItem
            // 
            this.редактированиеToolStripMenuItem.Name = "редактированиеToolStripMenuItem";
            this.редактированиеToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
            this.редактированиеToolStripMenuItem.Text = "Редактирование";
            this.редактированиеToolStripMenuItem.Click += new System.EventHandler(this.редактированиеToolStripMenuItem_Click);
            // 
            // удалитьToolStripMenuItem
            // 
            this.удалитьToolStripMenuItem.Name = "удалитьToolStripMenuItem";
            this.удалитьToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
            this.удалитьToolStripMenuItem.Text = "Удалить";
            this.удалитьToolStripMenuItem.Click += new System.EventHandler(this.удалитьToolStripMenuItem_Click);
            // 
            // посмотретьПроводкиToolStripMenuItem
            // 
            this.посмотретьПроводкиToolStripMenuItem.Name = "посмотретьПроводкиToolStripMenuItem";
            this.посмотретьПроводкиToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
            this.посмотретьПроводкиToolStripMenuItem.Text = "Посмотреть проводки";
            this.посмотретьПроводкиToolStripMenuItem.Click += new System.EventHandler(this.посмотретьПроводкиToolStripMenuItem_Click);
            // 
            // dataGridView
            // 
            this.dataGridView.AllowUserToAddRows = false;
            this.dataGridView.AllowUserToDeleteRows = false;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Location = new System.Drawing.Point(13, 28);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.ReadOnly = true;
            this.dataGridView.Size = new System.Drawing.Size(902, 410);
            this.dataGridView.TabIndex = 1;
            // 
            // отчётыToolStripMenuItem
            // 
            this.отчётыToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.поданныеЗаСменуТоварыToolStripMenuItem,
            this.списанныеТоварыToolStripMenuItem1,
            this.оборотносальдоваяВедомостьToolStripMenuItem1});
            this.отчётыToolStripMenuItem.Name = "отчётыToolStripMenuItem";
            this.отчётыToolStripMenuItem.Size = new System.Drawing.Size(60, 20);
            this.отчётыToolStripMenuItem.Text = "Отчёты";
            // 
            // поданныеЗаСменуТоварыToolStripMenuItem
            // 
            this.поданныеЗаСменуТоварыToolStripMenuItem.Name = "поданныеЗаСменуТоварыToolStripMenuItem";
            this.поданныеЗаСменуТоварыToolStripMenuItem.Size = new System.Drawing.Size(252, 22);
            this.поданныеЗаСменуТоварыToolStripMenuItem.Text = "Проданные за смену товары";
            this.поданныеЗаСменуТоварыToolStripMenuItem.Click += new System.EventHandler(this.поданныеЗаСменуТоварыToolStripMenuItem_Click);
            // 
            // списанныеТоварыToolStripMenuItem1
            // 
            this.списанныеТоварыToolStripMenuItem1.Name = "списанныеТоварыToolStripMenuItem1";
            this.списанныеТоварыToolStripMenuItem1.Size = new System.Drawing.Size(252, 22);
            this.списанныеТоварыToolStripMenuItem1.Text = "Списанные товары";
            this.списанныеТоварыToolStripMenuItem1.Click += new System.EventHandler(this.списанныеТоварыToolStripMenuItem1_Click);
            // 
            // оборотносальдоваяВедомостьToolStripMenuItem1
            // 
            this.оборотносальдоваяВедомостьToolStripMenuItem1.Name = "оборотносальдоваяВедомостьToolStripMenuItem1";
            this.оборотносальдоваяВедомостьToolStripMenuItem1.Size = new System.Drawing.Size(252, 22);
            this.оборотносальдоваяВедомостьToolStripMenuItem1.Text = "Оборотно-сальдовая ведомость";
            this.оборотносальдоваяВедомостьToolStripMenuItem1.Click += new System.EventHandler(this.оборотносальдоваяВедомостьToolStripMenuItem1_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(927, 450);
            this.Controls.Add(this.dataGridView);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FormMain";
            this.Text = "FormMain";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem справочникиToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem отчётToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem товарToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem поставщикиToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem сотрудникиToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem серииToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem планСчетовToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem проданныеТоварыToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem списанныеТоварыToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem оборотносальдоваяВедомостьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem журналПроводокToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem журналОперацийToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem поступлениеТоваровToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem продажаТоваровToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem списаниеТоваровToolStripMenuItem;
		private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.ToolStripMenuItem изменениеToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem редактированиеToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem удалитьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem посмотретьПроводкиToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem отчётыToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem поданныеЗаСменуТоварыToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem списанныеТоварыToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem оборотносальдоваяВедомостьToolStripMenuItem1;
    }
}


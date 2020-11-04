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
            this.журналПроводокToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.журналОперацийToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.отчётToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.проданныеТоварыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.списанныеТоварыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.оборотносальдоваяВедомостьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.справочникиToolStripMenuItem,
            this.журналПроводокToolStripMenuItem,
            this.отчётToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 24);
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
            this.товарToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.товарToolStripMenuItem.Text = "Товары";
            this.товарToolStripMenuItem.Click += new System.EventHandler(this.товарToolStripMenuItem_Click);
            // 
            // поставщикиToolStripMenuItem
            // 
            this.поставщикиToolStripMenuItem.Name = "поставщикиToolStripMenuItem";
            this.поставщикиToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.поставщикиToolStripMenuItem.Text = "Поставщики";
            this.поставщикиToolStripMenuItem.Click += new System.EventHandler(this.поставщикиToolStripMenuItem_Click);
            // 
            // сотрудникиToolStripMenuItem
            // 
            this.сотрудникиToolStripMenuItem.Name = "сотрудникиToolStripMenuItem";
            this.сотрудникиToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.сотрудникиToolStripMenuItem.Text = "Сотрудники";
            this.сотрудникиToolStripMenuItem.Click += new System.EventHandler(this.сотрудникиToolStripMenuItem_Click);
            // 
            // серииToolStripMenuItem
            // 
            this.серииToolStripMenuItem.Name = "серииToolStripMenuItem";
            this.серииToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.серииToolStripMenuItem.Text = "Серии";
            this.серииToolStripMenuItem.Click += new System.EventHandler(this.серииToolStripMenuItem_Click);
            // 
            // планСчетовToolStripMenuItem
            // 
            this.планСчетовToolStripMenuItem.Name = "планСчетовToolStripMenuItem";
            this.планСчетовToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.планСчетовToolStripMenuItem.Text = "План счетов";
            this.планСчетовToolStripMenuItem.Click += new System.EventHandler(this.планСчетовToolStripMenuItem_Click);
            // 
            // журналПроводокToolStripMenuItem
            // 
            this.журналПроводокToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.журналПроводокToolStripMenuItem1,
            this.журналОперацийToolStripMenuItem});
            this.журналПроводокToolStripMenuItem.Name = "журналПроводокToolStripMenuItem";
            this.журналПроводокToolStripMenuItem.Size = new System.Drawing.Size(75, 20);
            this.журналПроводокToolStripMenuItem.Text = "Операции";
            // 
            // журналПроводокToolStripMenuItem1
            // 
            this.журналПроводокToolStripMenuItem1.Name = "журналПроводокToolStripMenuItem1";
            this.журналПроводокToolStripMenuItem1.Size = new System.Drawing.Size(175, 22);
            this.журналПроводокToolStripMenuItem1.Text = "Журнал проводок";
            // 
            // журналОперацийToolStripMenuItem
            // 
            this.журналОперацийToolStripMenuItem.Name = "журналОперацийToolStripMenuItem";
            this.журналОперацийToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            this.журналОперацийToolStripMenuItem.Text = "Журнал операций";
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
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FormMain";
            this.Text = "FormMain";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem справочникиToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem журналПроводокToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem отчётToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem товарToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem поставщикиToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem журналПроводокToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem журналОперацийToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem сотрудникиToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem серииToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem планСчетовToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem проданныеТоварыToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem списанныеТоварыToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem оборотносальдоваяВедомостьToolStripMenuItem;
    }
}


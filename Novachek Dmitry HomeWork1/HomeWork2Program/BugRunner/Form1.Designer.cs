namespace BugRunner
{
    partial class Form1
    {
        /// <summary>
        /// Требуется переменная конструктора.
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
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.SpiderButton4 = new System.Windows.Forms.RadioButton();
            this.SpiderButton2 = new System.Windows.Forms.RadioButton();
            this.SpiderButton3 = new System.Windows.Forms.RadioButton();
            this.SpiderButton1 = new System.Windows.Forms.RadioButton();
            this.GamblerButton3 = new System.Windows.Forms.RadioButton();
            this.GamblerButton2 = new System.Windows.Forms.RadioButton();
            this.GamblerButton1 = new System.Windows.Forms.RadioButton();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.ButtonGo = new System.Windows.Forms.Button();
            this.ButtonBet = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.ResultBox = new System.Windows.Forms.RichTextBox();
            this.CurrentBetsBox = new System.Windows.Forms.RichTextBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.SpiderPicture4 = new System.Windows.Forms.PictureBox();
            this.SpiderPicture3 = new System.Windows.Forms.PictureBox();
            this.SpiderPicture2 = new System.Windows.Forms.PictureBox();
            this.SpiderPicture1 = new System.Windows.Forms.PictureBox();
            this.panel1.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SpiderPicture4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SpiderPicture3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SpiderPicture2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SpiderPicture1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.LightGray;
            this.panel1.Controls.Add(this.panel4);
            this.panel1.Controls.Add(this.GamblerButton3);
            this.panel1.Controls.Add(this.GamblerButton2);
            this.panel1.Controls.Add(this.GamblerButton1);
            this.panel1.Controls.Add(this.textBox1);
            this.panel1.Controls.Add(this.ButtonGo);
            this.panel1.Controls.Add(this.ButtonBet);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(13, 430);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1029, 157);
            this.panel1.TabIndex = 0;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.label2);
            this.panel4.Controls.Add(this.SpiderButton4);
            this.panel4.Controls.Add(this.SpiderButton2);
            this.panel4.Controls.Add(this.SpiderButton3);
            this.panel4.Controls.Add(this.SpiderButton1);
            this.panel4.Location = new System.Drawing.Point(263, 5);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(172, 149);
            this.panel4.TabIndex = 12;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label2.Location = new System.Drawing.Point(9, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(144, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "2. Выберите участника";
            // 
            // SpiderButton4
            // 
            this.SpiderButton4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.SpiderButton4.AutoSize = true;
            this.SpiderButton4.Location = new System.Drawing.Point(12, 113);
            this.SpiderButton4.Name = "SpiderButton4";
            this.SpiderButton4.Size = new System.Drawing.Size(82, 17);
            this.SpiderButton4.TabIndex = 11;
            this.SpiderButton4.Text = "Участник 4";
            this.SpiderButton4.UseVisualStyleBackColor = true;
            this.SpiderButton4.CheckedChanged += new System.EventHandler(this.SpiderButton4_CheckedChanged);
            // 
            // SpiderButton2
            // 
            this.SpiderButton2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.SpiderButton2.AutoSize = true;
            this.SpiderButton2.Location = new System.Drawing.Point(12, 67);
            this.SpiderButton2.Name = "SpiderButton2";
            this.SpiderButton2.Size = new System.Drawing.Size(82, 17);
            this.SpiderButton2.TabIndex = 9;
            this.SpiderButton2.Text = "Участник 2";
            this.SpiderButton2.UseVisualStyleBackColor = true;
            this.SpiderButton2.CheckedChanged += new System.EventHandler(this.SpiderButton2_CheckedChanged);
            // 
            // SpiderButton3
            // 
            this.SpiderButton3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.SpiderButton3.AutoSize = true;
            this.SpiderButton3.Location = new System.Drawing.Point(12, 90);
            this.SpiderButton3.Name = "SpiderButton3";
            this.SpiderButton3.Size = new System.Drawing.Size(82, 17);
            this.SpiderButton3.TabIndex = 10;
            this.SpiderButton3.Text = "Участник 3";
            this.SpiderButton3.UseVisualStyleBackColor = true;
            this.SpiderButton3.CheckedChanged += new System.EventHandler(this.SpiderButton3_CheckedChanged);
            // 
            // SpiderButton1
            // 
            this.SpiderButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.SpiderButton1.AutoSize = true;
            this.SpiderButton1.Checked = true;
            this.SpiderButton1.Location = new System.Drawing.Point(12, 44);
            this.SpiderButton1.Name = "SpiderButton1";
            this.SpiderButton1.Size = new System.Drawing.Size(82, 17);
            this.SpiderButton1.TabIndex = 8;
            this.SpiderButton1.TabStop = true;
            this.SpiderButton1.Text = "Участник 1";
            this.SpiderButton1.UseVisualStyleBackColor = true;
            this.SpiderButton1.CheckedChanged += new System.EventHandler(this.SpiderButton1_CheckedChanged);
            // 
            // GamblerButton3
            // 
            this.GamblerButton3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.GamblerButton3.AutoSize = true;
            this.GamblerButton3.Location = new System.Drawing.Point(34, 95);
            this.GamblerButton3.Name = "GamblerButton3";
            this.GamblerButton3.Size = new System.Drawing.Size(65, 17);
            this.GamblerButton3.TabIndex = 7;
            this.GamblerButton3.Text = "Игрок 3";
            this.GamblerButton3.UseVisualStyleBackColor = true;
            this.GamblerButton3.CheckedChanged += new System.EventHandler(this.GamblerButton3_CheckedChanged);
            // 
            // GamblerButton2
            // 
            this.GamblerButton2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.GamblerButton2.AutoSize = true;
            this.GamblerButton2.Location = new System.Drawing.Point(34, 72);
            this.GamblerButton2.Name = "GamblerButton2";
            this.GamblerButton2.Size = new System.Drawing.Size(65, 17);
            this.GamblerButton2.TabIndex = 6;
            this.GamblerButton2.Text = "Игрок 2";
            this.GamblerButton2.UseVisualStyleBackColor = true;
            this.GamblerButton2.CheckedChanged += new System.EventHandler(this.GamblerButton2_CheckedChanged);
            // 
            // GamblerButton1
            // 
            this.GamblerButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.GamblerButton1.AutoSize = true;
            this.GamblerButton1.Checked = true;
            this.GamblerButton1.Location = new System.Drawing.Point(34, 49);
            this.GamblerButton1.Name = "GamblerButton1";
            this.GamblerButton1.Size = new System.Drawing.Size(65, 17);
            this.GamblerButton1.TabIndex = 5;
            this.GamblerButton1.TabStop = true;
            this.GamblerButton1.Text = "Игрок 1";
            this.GamblerButton1.UseVisualStyleBackColor = true;
            this.GamblerButton1.CheckedChanged += new System.EventHandler(this.GamblerButton1_CheckedChanged);
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.Location = new System.Drawing.Point(596, 46);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(119, 20);
            this.textBox1.TabIndex = 4;
            // 
            // ButtonGo
            // 
            this.ButtonGo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ButtonGo.BackColor = System.Drawing.Color.LightGray;
            this.ButtonGo.Font = new System.Drawing.Font("Microsoft YaHei UI", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ButtonGo.ForeColor = System.Drawing.Color.Firebrick;
            this.ButtonGo.Location = new System.Drawing.Point(841, 21);
            this.ButtonGo.Name = "ButtonGo";
            this.ButtonGo.Size = new System.Drawing.Size(163, 107);
            this.ButtonGo.TabIndex = 3;
            this.ButtonGo.Text = "Начать игру";
            this.ButtonGo.UseVisualStyleBackColor = false;
            this.ButtonGo.Click += new System.EventHandler(this.ButtonGo_Click);
            // 
            // ButtonBet
            // 
            this.ButtonBet.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ButtonBet.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ButtonBet.Location = new System.Drawing.Point(596, 76);
            this.ButtonBet.Name = "ButtonBet";
            this.ButtonBet.Size = new System.Drawing.Size(119, 25);
            this.ButtonBet.TabIndex = 3;
            this.ButtonBet.Text = "Поставить";
            this.ButtonBet.UseVisualStyleBackColor = false;
            this.ButtonBet.Click += new System.EventHandler(this.ButtonBet_Click);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label3.Location = new System.Drawing.Point(593, 21);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(122, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "3. Сделайте ставку";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label1.Location = new System.Drawing.Point(79, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(125, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "1. Выберите игрока";
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BackColor = System.Drawing.Color.Silver;
            this.panel2.Controls.Add(this.ResultBox);
            this.panel2.Controls.Add(this.CurrentBetsBox);
            this.panel2.Location = new System.Drawing.Point(801, 12);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(241, 398);
            this.panel2.TabIndex = 1;
            // 
            // ResultBox
            // 
            this.ResultBox.BackColor = System.Drawing.SystemColors.MenuBar;
            this.ResultBox.Location = new System.Drawing.Point(12, 209);
            this.ResultBox.Name = "ResultBox";
            this.ResultBox.Size = new System.Drawing.Size(216, 174);
            this.ResultBox.TabIndex = 1;
            this.ResultBox.Text = "";
            // 
            // CurrentBetsBox
            // 
            this.CurrentBetsBox.BackColor = System.Drawing.SystemColors.MenuBar;
            this.CurrentBetsBox.Location = new System.Drawing.Point(12, 13);
            this.CurrentBetsBox.Name = "CurrentBetsBox";
            this.CurrentBetsBox.Size = new System.Drawing.Size(216, 174);
            this.CurrentBetsBox.TabIndex = 0;
            this.CurrentBetsBox.Text = "";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.DimGray;
            this.panel3.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel3.BackgroundImage")));
            this.panel3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel3.Controls.Add(this.SpiderPicture4);
            this.panel3.Controls.Add(this.SpiderPicture3);
            this.panel3.Controls.Add(this.SpiderPicture2);
            this.panel3.Controls.Add(this.SpiderPicture1);
            this.panel3.Location = new System.Drawing.Point(12, 12);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(764, 398);
            this.panel3.TabIndex = 2;
            // 
            // SpiderPicture4
            // 
            this.SpiderPicture4.BackColor = System.Drawing.Color.DimGray;
            this.SpiderPicture4.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("SpiderPicture4.BackgroundImage")));
            this.SpiderPicture4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.SpiderPicture4.InitialImage = null;
            this.SpiderPicture4.Location = new System.Drawing.Point(12, 303);
            this.SpiderPicture4.Name = "SpiderPicture4";
            this.SpiderPicture4.Size = new System.Drawing.Size(104, 84);
            this.SpiderPicture4.TabIndex = 3;
            this.SpiderPicture4.TabStop = false;
            // 
            // SpiderPicture3
            // 
            this.SpiderPicture3.BackColor = System.Drawing.Color.DimGray;
            this.SpiderPicture3.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("SpiderPicture3.BackgroundImage")));
            this.SpiderPicture3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.SpiderPicture3.InitialImage = null;
            this.SpiderPicture3.Location = new System.Drawing.Point(12, 209);
            this.SpiderPicture3.Name = "SpiderPicture3";
            this.SpiderPicture3.Size = new System.Drawing.Size(104, 82);
            this.SpiderPicture3.TabIndex = 2;
            this.SpiderPicture3.TabStop = false;
            // 
            // SpiderPicture2
            // 
            this.SpiderPicture2.BackColor = System.Drawing.Color.DimGray;
            this.SpiderPicture2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("SpiderPicture2.BackgroundImage")));
            this.SpiderPicture2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.SpiderPicture2.InitialImage = null;
            this.SpiderPicture2.Location = new System.Drawing.Point(12, 113);
            this.SpiderPicture2.Name = "SpiderPicture2";
            this.SpiderPicture2.Size = new System.Drawing.Size(104, 80);
            this.SpiderPicture2.TabIndex = 1;
            this.SpiderPicture2.TabStop = false;
            // 
            // SpiderPicture1
            // 
            this.SpiderPicture1.BackColor = System.Drawing.Color.DimGray;
            this.SpiderPicture1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("SpiderPicture1.BackgroundImage")));
            this.SpiderPicture1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.SpiderPicture1.InitialImage = null;
            this.SpiderPicture1.Location = new System.Drawing.Point(12, 13);
            this.SpiderPicture1.Name = "SpiderPicture1";
            this.SpiderPicture1.Size = new System.Drawing.Size(104, 84);
            this.SpiderPicture1.TabIndex = 0;
            this.SpiderPicture1.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DimGray;
            this.ClientSize = new System.Drawing.Size(1054, 599);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "Form1";
            this.Text = "Spider Runner";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.SpiderPicture4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SpiderPicture3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SpiderPicture2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SpiderPicture1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button ButtonGo;
        private System.Windows.Forms.Button ButtonBet;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton GamblerButton3;
        private System.Windows.Forms.RadioButton GamblerButton2;
        private System.Windows.Forms.RadioButton GamblerButton1;
        private System.Windows.Forms.RadioButton SpiderButton4;
        private System.Windows.Forms.RadioButton SpiderButton3;
        private System.Windows.Forms.RadioButton SpiderButton2;
        private System.Windows.Forms.RadioButton SpiderButton1;
        private System.Windows.Forms.RichTextBox ResultBox;
        private System.Windows.Forms.RichTextBox CurrentBetsBox;
        private System.Windows.Forms.PictureBox SpiderPicture4;
        private System.Windows.Forms.PictureBox SpiderPicture3;
        private System.Windows.Forms.PictureBox SpiderPicture2;
        private System.Windows.Forms.PictureBox SpiderPicture1;
        private System.Windows.Forms.Panel panel4;
    }
}


namespace MyGame
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
			this.DrawArea = new System.Windows.Forms.Panel();
			this.panel1 = new System.Windows.Forms.Panel();
			this.WeaponDescription = new System.Windows.Forms.RichTextBox();
			this.MagicStoneDescription = new System.Windows.Forms.RichTextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.pictureBoxStone = new System.Windows.Forms.PictureBox();
			this.label2 = new System.Windows.Forms.Label();
			this.pictureBoxSword = new System.Windows.Forms.PictureBox();
			this.shapeContainer1 = new Microsoft.VisualBasic.PowerPacks.ShapeContainer();
			this.lineShape2 = new Microsoft.VisualBasic.PowerPacks.LineShape();
			this.lineShape1 = new Microsoft.VisualBasic.PowerPacks.LineShape();
			this.label1 = new System.Windows.Forms.Label();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.button2 = new System.Windows.Forms.Button();
			this.PickUpItemButton = new System.Windows.Forms.Button();
			this.EndTurnButton = new System.Windows.Forms.Button();
			this.panel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxStone)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxSword)).BeginInit();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// DrawArea
			// 
			this.DrawArea.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.DrawArea.Location = new System.Drawing.Point(13, 13);
			this.DrawArea.Name = "DrawArea";
			this.DrawArea.Size = new System.Drawing.Size(800, 600);
			this.DrawArea.TabIndex = 0;
			this.DrawArea.Click += new System.EventHandler(this.DrawArea_Click);
			this.DrawArea.ControlRemoved += new System.Windows.Forms.ControlEventHandler(this.DrawArea_ControlRemoved);
			this.DrawArea.Paint += new System.Windows.Forms.PaintEventHandler(this.DrawArea_Paint);
			// 
			// panel1
			// 
			this.panel1.BackColor = System.Drawing.SystemColors.ActiveCaption;
			this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panel1.Controls.Add(this.WeaponDescription);
			this.panel1.Controls.Add(this.MagicStoneDescription);
			this.panel1.Controls.Add(this.label3);
			this.panel1.Controls.Add(this.pictureBoxStone);
			this.panel1.Controls.Add(this.label2);
			this.panel1.Controls.Add(this.pictureBoxSword);
			this.panel1.Controls.Add(this.shapeContainer1);
			this.panel1.Location = new System.Drawing.Point(820, 40);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(391, 337);
			this.panel1.TabIndex = 1;
			// 
			// WeaponDescription
			// 
			this.WeaponDescription.Location = new System.Drawing.Point(29, 249);
			this.WeaponDescription.Name = "WeaponDescription";
			this.WeaponDescription.Size = new System.Drawing.Size(154, 48);
			this.WeaponDescription.TabIndex = 7;
			this.WeaponDescription.Text = "";
			// 
			// MagicStoneDescription
			// 
			this.MagicStoneDescription.Location = new System.Drawing.Point(199, 154);
			this.MagicStoneDescription.Name = "MagicStoneDescription";
			this.MagicStoneDescription.Size = new System.Drawing.Size(191, 74);
			this.MagicStoneDescription.TabIndex = 6;
			this.MagicStoneDescription.Text = "";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label3.Location = new System.Drawing.Point(261, 10);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(77, 20);
			this.label3.TabIndex = 5;
			this.label3.Text = "Камень:";
			// 
			// pictureBoxStone
			// 
			this.pictureBoxStone.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pictureBoxStone.Location = new System.Drawing.Point(265, 33);
			this.pictureBoxStone.Name = "pictureBoxStone";
			this.pictureBoxStone.Size = new System.Drawing.Size(115, 115);
			this.pictureBoxStone.TabIndex = 4;
			this.pictureBoxStone.TabStop = false;
			this.pictureBoxStone.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBoxStone_Paint);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label2.Location = new System.Drawing.Point(-1, 10);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(77, 20);
			this.label2.TabIndex = 3;
			this.label2.Text = "Оружие:";
			// 
			// pictureBoxSword
			// 
			this.pictureBoxSword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pictureBoxSword.Location = new System.Drawing.Point(3, 33);
			this.pictureBoxSword.Name = "pictureBoxSword";
			this.pictureBoxSword.Size = new System.Drawing.Size(115, 202);
			this.pictureBoxSword.TabIndex = 0;
			this.pictureBoxSword.TabStop = false;
			this.pictureBoxSword.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBoxWeapon_Paint);
			// 
			// shapeContainer1
			// 
			this.shapeContainer1.Location = new System.Drawing.Point(0, 0);
			this.shapeContainer1.Margin = new System.Windows.Forms.Padding(0);
			this.shapeContainer1.Name = "shapeContainer1";
			this.shapeContainer1.Shapes.AddRange(new Microsoft.VisualBasic.PowerPacks.Shape[] {
            this.lineShape2,
            this.lineShape1});
			this.shapeContainer1.Size = new System.Drawing.Size(389, 335);
			this.shapeContainer1.TabIndex = 8;
			this.shapeContainer1.TabStop = false;
			// 
			// lineShape2
			// 
			this.lineShape2.BorderWidth = 2;
			this.lineShape2.Name = "lineShape2";
			this.lineShape2.X1 = 92;
			this.lineShape2.X2 = 77;
			this.lineShape2.Y1 = 250;
			this.lineShape2.Y2 = 235;
			// 
			// lineShape1
			// 
			this.lineShape1.BorderWidth = 2;
			this.lineShape1.Name = "lineShape1";
			this.lineShape1.X1 = 223;
			this.lineShape1.X2 = 265;
			this.lineShape1.Y1 = 163;
			this.lineShape1.Y2 = 76;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label1.Location = new System.Drawing.Point(820, 13);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(106, 20);
			this.label1.TabIndex = 2;
			this.label1.Text = "Инвентарь:";
			// 
			// groupBox1
			// 
			this.groupBox1.BackColor = System.Drawing.SystemColors.ActiveCaption;
			this.groupBox1.Controls.Add(this.button2);
			this.groupBox1.Controls.Add(this.PickUpItemButton);
			this.groupBox1.Controls.Add(this.EndTurnButton);
			this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.groupBox1.Location = new System.Drawing.Point(820, 384);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(391, 186);
			this.groupBox1.TabIndex = 3;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Элементы управления";
			// 
			// button2
			// 
			this.button2.Location = new System.Drawing.Point(7, 129);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(378, 49);
			this.button2.TabIndex = 2;
			this.button2.Text = "Новая игра";
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Click += new System.EventHandler(this.NewGameButton_Click);
			// 
			// PickUpItemButton
			// 
			this.PickUpItemButton.Enabled = false;
			this.PickUpItemButton.Location = new System.Drawing.Point(7, 76);
			this.PickUpItemButton.Name = "PickUpItemButton";
			this.PickUpItemButton.Size = new System.Drawing.Size(378, 46);
			this.PickUpItemButton.TabIndex = 1;
			this.PickUpItemButton.Text = "Подобрать предмет";
			this.PickUpItemButton.UseVisualStyleBackColor = true;
			this.PickUpItemButton.Click += new System.EventHandler(this.PickUpItemButton_Click);
			// 
			// EndTurnButton
			// 
			this.EndTurnButton.Location = new System.Drawing.Point(7, 26);
			this.EndTurnButton.Name = "EndTurnButton";
			this.EndTurnButton.Size = new System.Drawing.Size(378, 44);
			this.EndTurnButton.TabIndex = 0;
			this.EndTurnButton.Text = "Завершить ход";
			this.EndTurnButton.UseVisualStyleBackColor = true;
			this.EndTurnButton.Click += new System.EventHandler(this.EndTurnButton_Click);
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1223, 625);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.DrawArea);
			this.Name = "Form1";
			this.Text = "GameWindow";
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxStone)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxSword)).EndInit();
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel DrawArea;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.PictureBox pictureBoxStone;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pictureBoxSword;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button PickUpItemButton;
        private System.Windows.Forms.Button EndTurnButton;
        private System.Windows.Forms.Button button2;
		private System.Windows.Forms.RichTextBox WeaponDescription;
		private System.Windows.Forms.RichTextBox MagicStoneDescription;
		private Microsoft.VisualBasic.PowerPacks.ShapeContainer shapeContainer1;
		private Microsoft.VisualBasic.PowerPacks.LineShape lineShape2;
		private Microsoft.VisualBasic.PowerPacks.LineShape lineShape1;
    }
}


namespace Chess
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.board_pictureBox = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.Castle_Button = new System.Windows.Forms.Button();
            this.Bishop_Button = new System.Windows.Forms.Button();
            this.Queen_Button = new System.Windows.Forms.Button();
            this.Knight_Button = new System.Windows.Forms.Button();
            this.labelsuperpawn = new System.Windows.Forms.Label();
            this.New_Game = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.board_pictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.panel1.Enabled = false;
            this.panel1.Location = new System.Drawing.Point(30, 30);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(240, 240);
            this.panel1.TabIndex = 3;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(346, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Белые";
            // 
            // board_pictureBox
            // 
            this.board_pictureBox.Image = ((System.Drawing.Image)(resources.GetObject("board_pictureBox.Image")));
            this.board_pictureBox.Location = new System.Drawing.Point(30, 30);
            this.board_pictureBox.Name = "board_pictureBox";
            this.board_pictureBox.Size = new System.Drawing.Size(237, 237);
            this.board_pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.board_pictureBox.TabIndex = 0;
            this.board_pictureBox.TabStop = false;
            this.board_pictureBox.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(300, 49);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Ходят:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(300, 103);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(30, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Мат:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(337, 103);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(46, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Никому";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(300, 75);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(30, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Шах:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(340, 75);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(46, 13);
            this.label6.TabIndex = 9;
            this.label6.Text = "Никому";
            // 
            // Castle_Button
            // 
            this.Castle_Button.Enabled = false;
            this.Castle_Button.Location = new System.Drawing.Point(303, 182);
            this.Castle_Button.Name = "Castle_Button";
            this.Castle_Button.Size = new System.Drawing.Size(83, 23);
            this.Castle_Button.TabIndex = 11;
            this.Castle_Button.Text = "Ладья";
            this.Castle_Button.UseVisualStyleBackColor = true;
            this.Castle_Button.Click += new System.EventHandler(this.Castle_Button_Click);
            // 
            // Bishop_Button
            // 
            this.Bishop_Button.Enabled = false;
            this.Bishop_Button.Location = new System.Drawing.Point(303, 242);
            this.Bishop_Button.Name = "Bishop_Button";
            this.Bishop_Button.Size = new System.Drawing.Size(83, 23);
            this.Bishop_Button.TabIndex = 13;
            this.Bishop_Button.Text = "Слон";
            this.Bishop_Button.UseVisualStyleBackColor = true;
            this.Bishop_Button.Click += new System.EventHandler(this.Bishop_Button_Click);
            // 
            // Queen_Button
            // 
            this.Queen_Button.Enabled = false;
            this.Queen_Button.Location = new System.Drawing.Point(303, 153);
            this.Queen_Button.Name = "Queen_Button";
            this.Queen_Button.Size = new System.Drawing.Size(83, 23);
            this.Queen_Button.TabIndex = 14;
            this.Queen_Button.Text = "Ферзь";
            this.Queen_Button.UseVisualStyleBackColor = true;
            this.Queen_Button.Click += new System.EventHandler(this.Queen_Button_Click);
            // 
            // Knight_Button
            // 
            this.Knight_Button.Enabled = false;
            this.Knight_Button.Location = new System.Drawing.Point(303, 212);
            this.Knight_Button.Name = "Knight_Button";
            this.Knight_Button.Size = new System.Drawing.Size(83, 23);
            this.Knight_Button.TabIndex = 15;
            this.Knight_Button.Text = "Конь";
            this.Knight_Button.UseVisualStyleBackColor = true;
            this.Knight_Button.Click += new System.EventHandler(this.Knight_Button_Click);
            // 
            // labelsuperpawn
            // 
            this.labelsuperpawn.AutoSize = true;
            this.labelsuperpawn.Location = new System.Drawing.Point(289, 124);
            this.labelsuperpawn.Name = "labelsuperpawn";
            this.labelsuperpawn.Size = new System.Drawing.Size(0, 13);
            this.labelsuperpawn.TabIndex = 16;
            // 
            // New_Game
            // 
            this.New_Game.Location = new System.Drawing.Point(308, 12);
            this.New_Game.Name = "New_Game";
            this.New_Game.Size = new System.Drawing.Size(75, 23);
            this.New_Game.TabIndex = 17;
            this.New_Game.Text = "Новая Игра";
            this.New_Game.UseVisualStyleBackColor = true;
            this.New_Game.Click += new System.EventHandler(this.New_Game_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(429, 311);
            this.Controls.Add(this.New_Game);
            this.Controls.Add(this.labelsuperpawn);
            this.Controls.Add(this.Knight_Button);
            this.Controls.Add(this.Queen_Button);
            this.Controls.Add(this.Bishop_Button);
            this.Controls.Add(this.Castle_Button);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.board_pictureBox);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.board_pictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox board_pictureBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button Castle_Button;
        private System.Windows.Forms.Button Bishop_Button;
        private System.Windows.Forms.Button Queen_Button;
        private System.Windows.Forms.Button Knight_Button;
        private System.Windows.Forms.Label labelsuperpawn;
        private System.Windows.Forms.Button New_Game;
    }
}


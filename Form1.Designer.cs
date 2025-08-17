namespace ghostDetectives
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            pictureBox1 = new PictureBox();
            pictureBox2 = new PictureBox();
            panel1 = new Panel();
            panel2 = new Panel();
            blackPanel = new Panel();
            label3 = new Label();
            label2 = new Label();
            Box2 = new PictureBox();
            T_character = new PictureBox();
            pictureBox3 = new PictureBox();
            label1 = new Label();
            Box = new PictureBox();
            character = new PictureBox();
            timer1 = new System.Windows.Forms.Timer(components);
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)Box2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)T_character).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)Box).BeginInit();
            ((System.ComponentModel.ISupportInitialize)character).BeginInit();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.Image = Properties.Resources.인트로_초반자막;
            pictureBox1.Location = new Point(0, 0);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(935, 620);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            pictureBox1.Click += pictureBox1_Click;
            // 
            // pictureBox2
            // 
            pictureBox2.Image = (Image)resources.GetObject("pictureBox2.Image");
            pictureBox2.Location = new Point(-8, 0);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(942, 623);
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox2.TabIndex = 1;
            pictureBox2.TabStop = false;
            // 
            // panel1
            // 
            panel1.BackColor = Color.Transparent;
            panel1.BackgroundImageLayout = ImageLayout.Stretch;
            panel1.Controls.Add(panel2);
            panel1.Controls.Add(pictureBox3);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(Box);
            panel1.Controls.Add(character);
            panel1.Location = new Point(-4, -4);
            panel1.Name = "panel1";
            panel1.Size = new Size(940, 620);
            panel1.TabIndex = 2;
            panel1.Click += panel1_Click;
            // 
            // panel2
            // 
            panel2.BackColor = Color.Transparent;
            panel2.Controls.Add(blackPanel);
            panel2.Controls.Add(label3);
            panel2.Controls.Add(label2);
            panel2.Controls.Add(Box2);
            panel2.Controls.Add(T_character);
            panel2.Location = new Point(4, 4);
            panel2.Name = "panel2";
            panel2.Size = new Size(933, 620);
            panel2.TabIndex = 3;
            panel2.Click += panel2_Click;
            // 
            // blackPanel
            // 
            blackPanel.BackColor = Color.Black;
            blackPanel.Location = new Point(0, -11);
            blackPanel.Name = "blackPanel";
            blackPanel.Size = new Size(933, 641);
            blackPanel.TabIndex = 11;
            blackPanel.Visible = false;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = Color.DarkOrange;
            label3.Font = new Font("한컴 말랑말랑 Bold", 12F, FontStyle.Bold, GraphicsUnit.Point, 129);
            label3.Image = Properties.Resources.대사박스4;
            label3.Location = new Point(286, 473);
            label3.Name = "label3";
            label3.Size = new Size(490, 26);
            label3.TabIndex = 10;
            label3.Text = "혼과 협력하기 위해서는 먼저 어떻게 된 일인지 알아봐야겠어.";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.DarkOrange;
            label2.Font = new Font("한컴 말랑말랑 Bold", 13.7999992F, FontStyle.Bold, GraphicsUnit.Point, 129);
            label2.Image = Properties.Resources.대사박스4;
            label2.Location = new Point(289, 471);
            label2.Name = "label2";
            label2.Size = new Size(425, 30);
            label2.TabIndex = 9;
            label2.Text = "흠. 여기인가.... 여기에서 억울한 죽음이 느껴져.";
            // 
            // Box2
            // 
            Box2.Image = Properties.Resources.대사박스4;
            Box2.Location = new Point(251, 425);
            Box2.Name = "Box2";
            Box2.Size = new Size(552, 155);
            Box2.SizeMode = PictureBoxSizeMode.StretchImage;
            Box2.TabIndex = 8;
            Box2.TabStop = false;
            // 
            // T_character
            // 
            T_character.Image = (Image)resources.GetObject("T_character.Image");
            T_character.Location = new Point(62, 343);
            T_character.Name = "T_character";
            T_character.Size = new Size(221, 276);
            T_character.SizeMode = PictureBoxSizeMode.Zoom;
            T_character.TabIndex = 7;
            T_character.TabStop = false;
            // 
            // pictureBox3
            // 
            pictureBox3.BackgroundImage = Properties.Resources.인트로_2_탐정등장;
            pictureBox3.BackgroundImageLayout = ImageLayout.Stretch;
            pictureBox3.Image = Properties.Resources.인트로_2_탐정등장;
            pictureBox3.Location = new Point(-4, 3);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(941, 617);
            pictureBox3.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox3.TabIndex = 3;
            pictureBox3.TabStop = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.DarkOrange;
            label1.Font = new Font("한컴 말랑말랑 Bold", 13.7999992F, FontStyle.Bold, GraphicsUnit.Point, 129);
            label1.Image = Properties.Resources.대사박스4;
            label1.Location = new Point(304, 484);
            label1.Name = "label1";
            label1.Size = new Size(230, 30);
            label1.TabIndex = 2;
            label1.Text = ".... 여기는 우리 집인데....\r\n";
            // 
            // Box
            // 
            Box.Image = Properties.Resources.대사박스4;
            Box.Location = new Point(264, 441);
            Box.Name = "Box";
            Box.Size = new Size(543, 145);
            Box.SizeMode = PictureBoxSizeMode.StretchImage;
            Box.TabIndex = 1;
            Box.TabStop = false;
            // 
            // character
            // 
            character.Image = Properties.Resources.한여주_혼란스러운_Photoroom;
            character.Location = new Point(70, 365);
            character.Name = "character";
            character.Size = new Size(198, 259);
            character.SizeMode = PictureBoxSizeMode.Zoom;
            character.TabIndex = 0;
            character.TabStop = false;
            // 
            // timer1
            // 
            timer1.Interval = 2000;
            timer1.Tick += timer1_Tick;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(9F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(930, 615);
            Controls.Add(panel1);
            Controls.Add(pictureBox2);
            Controls.Add(pictureBox1);
            Margin = new Padding(4);
            Name = "Form1";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)Box2).EndInit();
            ((System.ComponentModel.ISupportInitialize)T_character).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            ((System.ComponentModel.ISupportInitialize)Box).EndInit();
            ((System.ComponentModel.ISupportInitialize)character).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private PictureBox pictureBox1;
        private PictureBox pictureBox2;
        private Panel panel1;
        private PictureBox Box;
        private PictureBox character;
        private Label label1;
        private PictureBox pictureBox3;
        private System.Windows.Forms.Timer timer1;
        private Panel panel2;
        private Label label2;
        private PictureBox Box2;
        private PictureBox T_character;
        private Label label3;
        private Panel blackPanel;
    }
}

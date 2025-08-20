namespace ghostDetectives
{
    partial class Form2
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
            panel1 = new Panel();
            housePanel = new Panel();
            cluePanel = new Panel();
            clueLabel1 = new Label();
            clueBox1 = new PictureBox();
            clue1 = new PictureBox();
            panel9 = new Panel();
            panel8 = new Panel();
            panel7 = new Panel();
            panel6 = new Panel();
            panel5 = new Panel();
            panel4 = new Panel();
            panel3 = new Panel();
            panel2 = new Panel();
            housePanel.SuspendLayout();
            cluePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)clueBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)clue1).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.Transparent;
            panel1.BackgroundImage = Properties.Resources.주방_에너지바_Photoroom;
            panel1.ForeColor = Color.Transparent;
            panel1.Location = new Point(439, 51);
            panel1.Name = "panel1";
            panel1.Size = new Size(38, 36);
            panel1.TabIndex = 1;
            panel1.Click += panel1_Click;
            // 
            // housePanel
            // 
            housePanel.BackColor = Color.Transparent;
            housePanel.BackgroundImage = Properties.Resources.집_최종_완;
            housePanel.BackgroundImageLayout = ImageLayout.Stretch;
            housePanel.Controls.Add(cluePanel);
            housePanel.Controls.Add(panel9);
            housePanel.Controls.Add(panel8);
            housePanel.Controls.Add(panel7);
            housePanel.Controls.Add(panel6);
            housePanel.Controls.Add(panel5);
            housePanel.Controls.Add(panel4);
            housePanel.Controls.Add(panel3);
            housePanel.Controls.Add(panel2);
            housePanel.Controls.Add(panel1);
            housePanel.Location = new Point(-32, 0);
            housePanel.Name = "housePanel";
            housePanel.Size = new Size(866, 455);
            housePanel.TabIndex = 0;
            // 
            // cluePanel
            // 
            cluePanel.BackColor = Color.Black;
            cluePanel.Controls.Add(clueLabel1);
            cluePanel.Controls.Add(clueBox1);
            cluePanel.Controls.Add(clue1);
            cluePanel.Location = new Point(0, 0);
            cluePanel.Name = "cluePanel";
            cluePanel.Size = new Size(842, 455);
            cluePanel.TabIndex = 6;
            cluePanel.Click += cluePanel_Click;
            // 
            // clueLabel1
            // 
            clueLabel1.AutoSize = true;
            clueLabel1.BackColor = Color.Transparent;
            clueLabel1.Font = new Font("한컴 말랑말랑 Bold", 12F, FontStyle.Bold, GraphicsUnit.Point, 129);
            clueLabel1.Image = Properties.Resources.대사박스5;
            clueLabel1.Location = new Point(300, 368);
            clueLabel1.Name = "clueLabel1";
            clueLabel1.Size = new Size(289, 26);
            clueLabel1.TabIndex = 2;
            clueLabel1.Text = "가장 유명한 브랜드의 에너지바이다.";
            // 
            // clueBox1
            // 
            clueBox1.BackColor = Color.Transparent;
            clueBox1.Image = Properties.Resources.대사박스5_Photoroom;
            clueBox1.Location = new Point(173, 332);
            clueBox1.Name = "clueBox1";
            clueBox1.Size = new Size(536, 93);
            clueBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            clueBox1.TabIndex = 1;
            clueBox1.TabStop = false;
            // 
            // clue1
            // 
            clue1.BackColor = Color.Transparent;
            clue1.Image = Properties.Resources.주방_에너지바_Photoroom;
            clue1.Location = new Point(183, 42);
            clue1.Name = "clue1";
            clue1.Size = new Size(516, 293);
            clue1.SizeMode = PictureBoxSizeMode.Zoom;
            clue1.TabIndex = 0;
            clue1.TabStop = false;
            // 
            // panel9
            // 
            panel9.Location = new Point(537, 210);
            panel9.Name = "panel9";
            panel9.Size = new Size(75, 91);
            panel9.TabIndex = 5;
            // 
            // panel8
            // 
            panel8.Location = new Point(533, 356);
            panel8.Name = "panel8";
            panel8.Size = new Size(29, 39);
            panel8.TabIndex = 1;
            // 
            // panel7
            // 
            panel7.Location = new Point(689, 332);
            panel7.Name = "panel7";
            panel7.Size = new Size(49, 37);
            panel7.TabIndex = 1;
            // 
            // panel6
            // 
            panel6.Location = new Point(644, 267);
            panel6.Name = "panel6";
            panel6.Size = new Size(32, 14);
            panel6.TabIndex = 4;
            // 
            // panel5
            // 
            panel5.Location = new Point(636, 239);
            panel5.Name = "panel5";
            panel5.Size = new Size(25, 22);
            panel5.TabIndex = 1;
            // 
            // panel4
            // 
            panel4.Location = new Point(775, 150);
            panel4.Name = "panel4";
            panel4.Size = new Size(33, 37);
            panel4.TabIndex = 3;
            // 
            // panel3
            // 
            panel3.Location = new Point(705, 110);
            panel3.Name = "panel3";
            panel3.Size = new Size(25, 22);
            panel3.TabIndex = 0;
            // 
            // panel2
            // 
            panel2.BackColor = Color.Transparent;
            panel2.Location = new Point(733, 110);
            panel2.Name = "panel2";
            panel2.Size = new Size(36, 67);
            panel2.TabIndex = 2;
            // 
            // Form2
            // 
            AutoScaleDimensions = new SizeF(9F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(housePanel);
            Name = "Form2";
            Text = "Form2";
            housePanel.ResumeLayout(false);
            cluePanel.ResumeLayout(false);
            cluePanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)clueBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)clue1).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private Panel panel1;
        private Panel housePanel;
        private Panel panel8;
        private Panel panel7;
        private Panel panel6;
        private Panel panel5;
        private Panel panel4;
        private Panel panel3;
        private Panel panel2;
        private Panel panel9;
        private Panel cluePanel;
        private PictureBox clue1;
        private PictureBox clueBox1;
        private Label clueLabel1;
    }
}
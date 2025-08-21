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
            housePanel = new Panel();
            pictureBox1 = new PictureBox();
            cluePanel = new Panel();
            playerBox = new PictureBox();
            clueLabel1 = new Label();
            clueBox1 = new PictureBox();
            clue1 = new PictureBox();
            viewportPanel = new Panel();
            housePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            cluePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)playerBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)clueBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)clue1).BeginInit();
            SuspendLayout();
            // 
            // housePanel
            // 
            housePanel.BackColor = Color.Transparent;
            housePanel.BackgroundImage = Properties.Resources.집_최종_완;
            housePanel.BackgroundImageLayout = ImageLayout.Stretch;
            housePanel.Controls.Add(pictureBox1);
            housePanel.Controls.Add(cluePanel);
            housePanel.Location = new Point(-27, 1);
            housePanel.Name = "housePanel";
            housePanel.Size = new Size(990, 615);
            housePanel.TabIndex = 0;
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = Color.IndianRed;
            pictureBox1.Location = new Point(501, 209);
            pictureBox1.Margin = new Padding(4, 4, 4, 4);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(46, 37);
            pictureBox1.TabIndex = 8;
            pictureBox1.TabStop = false;
            // 
            // cluePanel
            // 
            cluePanel.BackColor = Color.Black;
            cluePanel.Controls.Add(playerBox);
            cluePanel.Controls.Add(clueLabel1);
            cluePanel.Controls.Add(clueBox1);
            cluePanel.Controls.Add(clue1);
            cluePanel.Location = new Point(0, 0);
            cluePanel.Name = "cluePanel";
            cluePanel.Size = new Size(987, 615);
            cluePanel.TabIndex = 6;
            cluePanel.Click += cluePanel_Click;
            // 
            // playerBox
            // 
            playerBox.BackgroundImageLayout = ImageLayout.Stretch;
            playerBox.Location = new Point(98, 408);
            playerBox.Margin = new Padding(4, 4, 4, 4);
            playerBox.Name = "playerBox";
            playerBox.Size = new Size(127, 207);
            playerBox.TabIndex = 9;
            playerBox.TabStop = false;
            // 
            // clueLabel1
            // 
            clueLabel1.AutoSize = true;
            clueLabel1.BackColor = Color.Transparent;
            clueLabel1.Font = new Font("한컴 말랑말랑 Bold", 12F, FontStyle.Bold, GraphicsUnit.Point, 129);
            clueLabel1.Image = Properties.Resources.대사박스5;
            clueLabel1.Location = new Point(359, 509);
            clueLabel1.Name = "clueLabel1";
            clueLabel1.Size = new Size(289, 26);
            clueLabel1.TabIndex = 2;
            clueLabel1.Text = "가장 유명한 브랜드의 에너지바이다.";
            // 
            // clueBox1
            // 
            clueBox1.BackColor = Color.Transparent;
            clueBox1.Image = Properties.Resources.대사박스5_Photoroom;
            clueBox1.Location = new Point(231, 475);
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
            clue1.Location = new Point(319, 92);
            clue1.Name = "clue1";
            clue1.Size = new Size(348, 373);
            clue1.SizeMode = PictureBoxSizeMode.Zoom;
            clue1.TabIndex = 0;
            clue1.TabStop = false;
            // 
            // viewportPanel
            // 
            viewportPanel.Location = new Point(-4, -16);
            viewportPanel.Margin = new Padding(4, 4, 4, 4);
            viewportPanel.Name = "viewportPanel";
            viewportPanel.Size = new Size(937, 633);
            viewportPanel.TabIndex = 1;
            // 
            // Form2
            // 
            AutoScaleDimensions = new SizeF(9F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(930, 615);
            Controls.Add(housePanel);
            Controls.Add(viewportPanel);
            Name = "Form2";
            Text = "Form2";
            housePanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            cluePanel.ResumeLayout(false);
            cluePanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)playerBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)clueBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)clue1).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private Panel housePanel;
        private Panel viewportPanel;
        private PictureBox pictureBox1;
        private Panel cluePanel;
        private PictureBox playerBox;
        private Label clueLabel1;
        private PictureBox clueBox1;
        private PictureBox clue1;
    }
}
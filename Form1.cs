using System;
using System.Reflection;
using System.Windows.Forms;
using ghostDetectives.Properties;
using Microsoft.VisualBasic.ApplicationServices;

namespace ghostDetectives
{
    public partial class Form1 : Form
    {
        private Panel blackPanel;
        private int panel3ClickCount = 0; // Ŭ�� Ƚ�� ����
        private int panel2ClickCount = 0;
        private int panel4ClickCount = 0;
        private int picturBox8ClickCount = 0;



        public Form1()
        {
            InitializeComponent();

            // �ʱ� ���� ����
            pictureBox1.Visible = true;  // ���� ��� ��Ʈ�� 
            pictureBox2.Visible = false; // ���� ���� 
            pictureBox3.Visible = false;

            panel1.Visible = false;
            panel1.BackColor = Color.Transparent;
            panel1.Parent = pictureBox2; // ���� ���� ���� ��������

            panel2.Visible = false;

            // ���� �г� �̸� ����
            blackPanel = new Panel();
            blackPanel.BackColor = Color.Black;
            blackPanel.Location = new Point(0, 0);
            blackPanel.Size = this.ClientSize;
            blackPanel.Visible = false;
            blackPanel.BringToFront();
            this.Controls.Add(blackPanel);

            panel3.MouseClick += panel3_Click;

        }

        private void pictureBox1_Click(object sender, EventArgs e) // ���� ��Ʈ�� Ŭ�� �� ���� �������� �Ѿ
        {

            pictureBox1.Visible = false;
            pictureBox2.Visible = true; // ���ֵ���
            pictureBox3.Visible = false;

            dotBox.Visible = false;
            dotLabel.Visible = false;

            pictureBox4.Visible = false; // �ѿ��ְ� ��ü �߰�

            panel3.Visible = false; // ��� ���� ���� ~ ����
            panel1.Visible = true;
            panel1.BringToFront();
        }
        private void ShowBlackPanel()
        {
            pictureBox1.Visible = false;
            pictureBox2.Visible = false;
            pictureBox4.Visible = false;
            panel3.Visible = false;

            // panel1�� �� �ڽ����� ����� ����
            if (panel1.Parent != this)
                panel1.Parent = this;
            panel1.Visible = false;
            panel1.SendToBack();

            blackPanel.Visible = true;

            // dotBox, dotLabel�� blackPanel ���� �÷��� Ŭ���� blackPanel�� ���޵ǰ�
            dotBox.Visible = true;
            dotBox.Parent = blackPanel;
            dotBox.BackColor = Color.Transparent;
            dotBox.BringToFront();

            dotLabel.Visible = true;
            dotLabel.Parent = blackPanel;
            dotLabel.BackColor = Color.Transparent;
            dotLabel.BringToFront();

            // blackPanel Ŭ�� �� �̺�Ʈ ����
            blackPanel.Click -= blackPanel_Click;
            blackPanel.Click += blackPanel_Click;

        }

        private void panel1_Click(object sender, EventArgs e) // ����ȭ�� ��Ÿ��, ���� ȭ�� ������ 
        {
            ShowBlackPanel();
        }

        private void ShowCorpse()
        {
            // ���� ȭ�鿡�� ���� ��Ʈ�ѵ�
            Control[] controlsToHide = { pictureBox1, pictureBox2, panel1, label5, blackPanel };
            foreach (var ctrl in controlsToHide)
            {
                ctrl.Visible = false;
            }

            // ������ ��Ʈ��
            pictureBox4.Parent = this; // Ȯ���� ���� �߰�
            pictureBox4.BringToFront();
            pictureBox4.Visible = true;
            panel3.BackColor = Color.Transparent;
            panel3.Parent = pictureBox4;
            panel3.Visible = true;
            panel3.BringToFront();

            // panel3 ���� �� ����
            label4.Visible = true;

            // ���ΰ�ħ
            pictureBox4.Refresh();
            panel3.Refresh();
            this.Refresh();
        }


        private void blackPanel_Click(object sender, EventArgs e) // ���� ��ü �߰�
        {
            ShowCorpse();
        }

        private void panel3_Click(object sender, EventArgs e) // ���� ��� ����
        {
            Control[] controlsToHide = { pictureBox1, pictureBox2, panel1, blackPanel };
            foreach (var ctrl in controlsToHide)
            {
                ctrl.Visible = false;
            }

            panel3ClickCount++;

            if (panel3ClickCount == 1)
            {
                pictureBox4.Visible = true;
                panel3.Visible = true;
                label5.Visible = true;
            }
            else if (panel3ClickCount == 2)
            {
                ShowDetective();
            }
        }


        int detectiveImageIndex = 0; // Ž�� �̹��� ����
        Image[] detectiveImages = new Image[]
        {
            Properties.Resources.��Ʈ��_1_�������ִ�,
            Properties.Resources.��Ʈ��_2_Ž������,
            Properties.Resources.��Ʈ��_3_Ž������,
            Properties.Resources.��Ʈ��_4_Ž������,
            Properties.Resources.��Ʈ��_Ž��_3��Ī
        };

        private void ShowDetective()
        {
            // �ٸ� �͵� �����
            pictureBox1.Visible = false;
            pictureBox2.Visible = false;

            // panel1�� �� �ڽ����� ����� ����
            if (panel1.Parent != this)
                panel1.Parent = this;
            panel1.Visible = false;
            panel1.SendToBack();

            pictureBox3.Image = detectiveImages[0];

            // pictureBox3 �¾�
            pictureBox3.Parent = this;
            pictureBox3.Visible = true;

            detectiveImageIndex = 0; // ó������ ����
            timer1.Start(); // Ÿ�̸� ����

            // �� ����
            this.Controls.SetChildIndex(pictureBox3, 0);
            pictureBox3.BringToFront();

            // ���ΰ�ħ
            pictureBox3.Refresh();
            this.Refresh();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            ShowDetective();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            detectiveImageIndex++;

            if (detectiveImageIndex >= detectiveImages.Length)
            {
                // ������ �̹������� ������ Ÿ�̸� ����
                timer1.Stop();
                pictureBox3.Enabled = true;
                pictureBox3.Click += PictureBox3_Click;

                return;
            }

            // ���� �̹��� ���
            pictureBox3.Image = detectiveImages[detectiveImageIndex];
        }


        private void PictureBox3_Click(object sender, EventArgs e)
        {
            blackPanel2.Visible = false;
            dotBox2.Visible = false;
            dotLabel2.Visible = false;

            pictureBox3.Parent = this;
            panel2.BackColor = Color.Transparent;
            panel2.Parent = pictureBox3;
            panel2.Visible = true;

            label2.Visible = true;
            label3.Visible = false;

            panel2.BringToFront();
            panel2.Click += panel2_Click;



        }

        private void ShowBlackPanel2() /// �ι�° ����ȭ�� (Ž��)
        {

            Control[] controlsToHide = { pictureBox4, panel3, label4, pictureBox7, panel4, character1, Box1, label6, dotBubble, pictureBox8 };
            foreach (var ctrl in controlsToHide)
            {
                ctrl.Visible = false;
            }

            blackPanel2.Visible = true;
            blackPanel2.BringToFront();
            dotBox2.Visible = true;
            dotBox2.BringToFront();
            dotLabel2.Visible = true;
            dotLabel2.BringToFront();

            blackPanel2.Click -= blackPanel2_Click;
            blackPanel2.Click += blackPanel2_Click;
        }


        private void panel2_Click(object sender, EventArgs e)
        {
            panel2ClickCount++;

            if (panel2ClickCount == 1)
            {
                // ù ��° Ŭ��: �� ����
                blackPanel2.Visible = false;
                dotBox2.Visible = false;
                dotLabel2.Visible = false;

                pictureBox3.Visible = true;
                label2.Visible = false;
                label3.Visible = true;
                panel2.BringToFront();

            }
            else if (panel2ClickCount == 2)
            {
                // �� ��° Ŭ��: ���� ȭ��
                ShowBlackPanel2();

            }
        }

        private void blackPanel2_Click(object sender, EventArgs e)
        {
            Control[] controlsToHide = { pictureBox3, panel2, blackPanel2, dotBox2, dotLabel2, dotBubble, pictureBox7, pictureBox9, Box3, label9, label9_1 };
            foreach (var ctrl in controlsToHide)
            {
                ctrl.Visible = false;
            }

            pictureBox7.Parent = this;
            pictureBox7.Visible = true;
            panel4.BackColor = Color.Transparent;
            panel4.Parent = pictureBox7;
            panel4.Visible = true;
            character1.Visible = true;
            Box1.Visible = true;
            label6.Visible = true;
            label6.BringToFront();

        }

        private void panel4_Click(object sender, EventArgs e)
        {
            panel4ClickCount++;

            if (panel4ClickCount == 1)
            {
                // ù ��° Ŭ��: �� ����
                label6.Visible = false;
                label7.Visible = true;
                label8.Visible = false;
                dotBubble.Visible = false;
                pictureBox9.Visible = false;
                Box3.Visible = false;
                label9.Visible = false;
                label9_1.Visible = false;
            }

            else if (panel4ClickCount == 2)
            {
                // �� ��° Ŭ��: �� ����
                label6.Visible = false;
                label7.Visible = false;
                label8.Visible = true;
                dotBubble.Visible = false;
                pictureBox9.Visible = false;

                Box3.Visible = false;
                label9.Visible = false;
                label9_1.Visible = false;

            }

            else if (panel4ClickCount == 3)
            {
                // ����° Ŭ��: ������ ��ǳ��
                label6.Visible = false;
                label7.Visible = false;
                label8.Visible = false;
                character1.Visible = false;
                Box1.Visible = false;
                pictureBox9.Visible = false;
                Box3.Visible = false;
                label9.Visible = false;
                label9_1.Visible = false;

                dotBubble.BackColor = Color.Transparent;
                dotBubble.Parent = pictureBox7;
                dotBubble.Visible = true;
                dotBubble.BringToFront();
            }

            else if (panel4ClickCount == 4)
            {
                // �׹�° Ŭ��: �ȹ�8 ��ȯ (�� ���� Ž��)
                Control[] controlsToHide = { label6, label7, label8, dotBubble, character1, Box1, pictureBox9, Box3, label9, label9_1 };
                foreach (var ctrl in controlsToHide)
                {
                    ctrl.Visible = false;
                }

                pictureBox8.Visible = true;
            }
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            picturBox8ClickCount++;

            if (picturBox8ClickCount == 1)
            {
                // ù ��° Ŭ��: �� ���� Ž�� Ȯ��

                Control[] controlsToHide = { pictureBox8, label9_1 };
                foreach (var ctrl in controlsToHide)
                {
                    ctrl.Visible = false;
                }

                Box3.BackColor = Color.Transparent;
                Box3.Parent = pictureBox9;

                pictureBox9.Visible = true; 
                Box3.Visible = true;
                label9.Visible = true;
                label9.BringToFront();

            }

            else if (picturBox8ClickCount == 2)
            {
                // �� ��° Ŭ��: �� ����


            }



        }
    }
}

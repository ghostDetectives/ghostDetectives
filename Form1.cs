using System;
using System.Reflection;
using System.Windows.Forms;
using ghostDetectives.Properties;
using Microsoft.VisualBasic.ApplicationServices;
using static System.Windows.Forms.DataFormats;

namespace ghostDetectives
{
    public partial class Form1 : Form
    {
        private Panel blackPanel;
        private int panel3ClickCount = 0; // Ŭ�� Ƚ�� ����
        private int panel2ClickCount = 0;
        private int panel4ClickCount = 0;
        private int pictureBox9ClickCount = 0;
        private int pictureBox10ClickCount = 0;

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

            // �г� Ŭ�� �̺�Ʈ ����
            //panel6.Click += panel6_Click; //��
            panel7.Click += panel7_Click; //�ƴϿ�
            panel8.Click += panel8_Click; // ���� ����ȭ��
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

            Control[] controlsToHide = { pictureBox4, panel3, label4, pictureBox7,
                panel4, character1, Box1, label6, dotBubble, pictureBox8 };
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
            Control[] controlsToHide = { pictureBox3, panel2, blackPanel2, dotBox2, dotLabel2,
                dotBubble, pictureBox7, pictureBox9, Box3, label9, label9_1, label9_2, label9_3, label9_4,
                pictureBox10, Box4, label10, panel8, pictureBox12, panel5, panel6, panel7};
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

                Control[] controlsToHide = { label6, label8, dotBubble, pictureBox9, Box3,
                    label9, label9_1, label9_2, label9_3, label9_4, pictureBox10, Box4, label10, 
                    panel8, pictureBox12, panel5, panel6, panel7 };
                foreach (var ctrl in controlsToHide)
                {
                    ctrl.Visible = false;
                }

                label7.Visible = true;
            }

            else if (panel4ClickCount == 2)
            {
                // �� ��° Ŭ��: �� ����

                Control[] controlsToHide = { label6, label7, dotBubble, pictureBox9, Box3,
                    label9, label9_1, label9_2, label9_3, label9_4, pictureBox10, Box4, 
                    label10, panel8, pictureBox12, panel5, panel6, panel7 };
                foreach (var ctrl in controlsToHide)
                {
                    ctrl.Visible = false;
                }

                label8.Visible = true;

            }

            else if (panel4ClickCount == 3)
            {
                // ����° Ŭ��: ������ ��ǳ��

                Control[] controlsToHide = { label6, label7, label8, character1, Box1,pictureBox9, Box3,
                    label9, label9_1, label9_2, label9_3, label9_4, pictureBox10, Box4, label10, 
                    panel8, pictureBox12, panel5, panel6, panel7 };
                foreach (var ctrl in controlsToHide)
                {
                    ctrl.Visible = false;
                }

                dotBubble.BackColor = Color.Transparent;
                dotBubble.Parent = pictureBox7;
                dotBubble.Visible = true;
                dotBubble.BringToFront();
            }

            else if (panel4ClickCount == 4)
            {
                // �׹�° Ŭ��: �ȹ�8 ��ȯ (�� ���� Ž��)
                Control[] controlsToHide = { label6, label7, label8, dotBubble, character1,
                    Box1, pictureBox9, Box3, label9, label9_1, label9_2, label9_3, label9_4,
                    pictureBox10, Box4, label10, panel8, pictureBox12, panel5, panel6, panel7 };
                foreach (var ctrl in controlsToHide)
                {
                    ctrl.Visible = false;
                }

                pictureBox8.Visible = true;
            }
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            // �� ���� Ž�� Ȯ��

            Control[] controlsToHide = { pictureBox8, label9_1, label9_2, label9_3, label9_4,
                pictureBox10, Box4, label10, panel8, pictureBox12, panel5, panel6, panel7 };
            foreach (var ctrl in controlsToHide)
            {
                ctrl.Visible = false;
            }

            Box3.BackColor = Color.Transparent;
            Box3.Parent = pictureBox9;

            pictureBox9.Visible = true;
            Box3.Visible = true;
            label9.Visible = true;// ȥ�� �ǽ� �ѿ��� �Բ� �����帳�ϴ�.
            label9.BringToFront();

        }

        private void pictureBox9_Click_(object sender, EventArgs e)
        {
            pictureBox9ClickCount++;

            // �� ����
            if (pictureBox9ClickCount == 1)
            {
                Control[] controlsToHide = { pictureBox8, label9, label9_2, label9_3,
                    label9_4, pictureBox10, Box4, label10, panel8, pictureBox12, panel5, panel6, panel7 };
                foreach (var ctrl in controlsToHide)
                {
                    ctrl.Visible = false;
                }

                Box3.BackColor = Color.Transparent;
                Box3.Parent = pictureBox9;

                pictureBox9.Visible = true;
                Box3.Visible = true;
                label9_1.Visible = true;
                label9_1.BringToFront();
            }

            else if (pictureBox9ClickCount == 2)
            {
                // �� ��° Ŭ��
                Control[] controlsToHide = { pictureBox8, label9, label9_1, label9_3,
                    label9_4, pictureBox10, Box4, label10, panel8, pictureBox12, panel5, panel6, panel7 };
                foreach (var ctrl in controlsToHide)
                {
                    ctrl.Visible = false;
                }

                Box3.BackColor = Color.Transparent;
                Box3.Parent = pictureBox9;

                pictureBox9.Visible = true;
                Box3.Visible = true;
                label9_2.Visible = true;
                label9_2.BringToFront();

            }

            else if (pictureBox9ClickCount == 3)
            {
                // �� ��° Ŭ��
                Control[] controlsToHide = { pictureBox8, label9, label9_1, label9_2,
                    label9_4, pictureBox10, Box4, label10, panel8, pictureBox12, panel5, panel6, panel7 };
                foreach (var ctrl in controlsToHide)
                {
                    ctrl.Visible = false;
                }

                Box3.BackColor = Color.Transparent;
                Box3.Parent = pictureBox9;

                pictureBox9.Visible = true;
                Box3.Visible = true;
                label9_3.Visible = true;
                label9_3.BringToFront();

            }

            else if (pictureBox9ClickCount == 4)
            {
                // �� ��° Ŭ��
                Control[] controlsToHide = { pictureBox8, label9, label9_1, label9_2,
                    label9_3, pictureBox10, Box4, label10, panel8, pictureBox12, panel5, panel6, panel7 };
                foreach (var ctrl in controlsToHide)
                {
                    ctrl.Visible = false;
                }

                Box3.BackColor = Color.Transparent;
                Box3.Parent = pictureBox9;

                pictureBox9.Visible = true;
                Box3.Visible = true;
                label9_4.Visible = true;
                label9_4.BringToFront();

            }

            else if (pictureBox9ClickCount == 5)
            {
                // �ټ���° Ŭ��
                Control[] controlsToHide = { pictureBox8, label9, label9_1, label9_2, label9_3,
                    label9_4, Box3, pictureBox9, pictureBox10, Box4, label10 , panel8, pictureBox12, panel5, panel6, panel7};
                foreach (var ctrl in controlsToHide)
                {
                    ctrl.Visible = false;
                }

                pictureBox10.Visible = true; // ������ ��ø �ǳ״� 

            }

        }

        private void pictureBox10_Click(object sender, EventArgs e) // ������ ��ø �ǳ״�
        {

            pictureBox10ClickCount++;

            if (pictureBox10ClickCount == 1)
            {
                Control[] controlsToHide = { pictureBox8, label9, label9_1, label9_2, label9_3,
                    label9_4, Box3, pictureBox9, panel8, pictureBox12, panel5, panel6, panel7 };
                foreach (var ctrl in controlsToHide)
                {
                    ctrl.Visible = false;
                }

                pictureBox10.Visible = true;
                Box4.BackColor = Color.Transparent;
                Box4.Parent = pictureBox10;
                Box4.Visible = true;
                label10.Visible = true;
                label10.BringToFront();
            }

            else if (pictureBox10ClickCount == 2)
            {
                Control[] controlsToHide = { pictureBox8, label9, label9_1, label9_2, label9_3,
                    label9_4, Box3, pictureBox9, pictureBox10, Box4, label10, panel8, pictureBox12 };
                foreach (var ctrl in controlsToHide)
                {
                    ctrl.Visible = false;
                }

                panel5.Visible = true; //Ž���� ��ø ��� �ִ�
                panel6.Visible = true; // ��
                panel7.Visible = true; // �ƴϿ�
            }
        }

        // === �г�6(��) Ŭ�� �� Form2 ���� ===
        private void panel6_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2(); // Form2 ��ü ����
            form2.Show();              // Form2 ���� 
            this.Hide();               // ���� �� �����
        }



        // === �г�7(�ƴϿ�) Ŭ�� �� �г�8(����ȭ��) ���̰� ===
        private void panel7_Click(object sender, EventArgs e) // �ƴϿ� Ŭ����
        {

            Control[] controlsToHide = { pictureBox7, panel4, pictureBox8, label9, label9_1, label9_2, 
                label9_3,label9_4, Box3, pictureBox9, pictureBox10, Box4, label10,panel5, panel6, panel7, };
            foreach (var ctrl in controlsToHide)
            {
                ctrl.Visible = false;
            }

            panel8.Parent = this;

            panel8.Visible = true;   // �г�8 ���̰� ����
            pictureBox11.Visible = true;
            pictureBox12.Visible = true;
            label11.Visible = true;
            panel8.BringToFront();   // �ٸ� ��Ʈ�� ���� �÷���
        }

        // === �г�8 Ŭ�� �� ���α׷� ���� ===
        private void panel8_Click(object sender, EventArgs e)
        {
            Application.Exit();  // ��ü ���α׷� ����

        }
    }
}

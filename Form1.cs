using System;
using System.Reflection;
using System.Windows.Forms;
using ghostDetectives.Properties;
using Microsoft.VisualBasic.ApplicationServices;

namespace ghostDetectives
{
    public partial class Form1 : Form
    {
        private int panel2ClickCount = 0; // Ŭ�� Ƚ�� ����
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

        }

        private void pictureBox1_Click(object sender, EventArgs e) // ���� ��Ʈ�� Ŭ�� �� ���� �������� �Ѿ
        {

            pictureBox1.Visible = false;
            pictureBox2.Visible = true;
            pictureBox3.Visible = false;

            panel1.Visible = true;
            panel1.BringToFront();
        }

        int detectiveImageIndex = 0; // Ž�� �̹��� ����
        Image[] detectiveImages = new Image[]
        {
            Properties.Resources.��Ʈ��_1_�������ִ�,
            Properties.Resources.��Ʈ��_2_Ž������,
            Properties.Resources._2_Ž��_����,
            Properties.Resources._3_Ž��_����,
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
            pictureBox3.Parent = this; ;
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

        private void panel1_Click(object sender, EventArgs e)
        {
            ShowDetective();
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
                pictureBox3.Click += PictureBox3Final_Click;

                return;
            }

            // ���� �̹��� ���
            pictureBox3.Image = detectiveImages[detectiveImageIndex];
        }


        private void PictureBox3Final_Click(object sender, EventArgs e)
        {
            panel2.BackColor = Color.Transparent;
            panel2.Parent = pictureBox3;
            panel2.Visible = true;

            label3.Visible = false;

            panel2.BringToFront();


        }


        private void panel2_Click(object sender, EventArgs e)
        {
            panel2ClickCount++;

            if (panel2ClickCount == 1)
            {
                // ù ��° Ŭ��: �� ����
                label2.Visible = false;
                label3.Visible = true;
            }
            else if (panel2ClickCount == 2)
            {
                // �� ��° Ŭ��: ���� ȭ��
                blackPanel.Visible = true;
            }
        }
       
    }
}

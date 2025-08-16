using System.Windows.Forms;
using ghostDetectives.Properties;
using Microsoft.VisualBasic.ApplicationServices;

namespace ghostDetectives
{
    public partial class Form1 : Form
    {
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
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            pictureBox1.Visible = false;
            pictureBox2.Visible = true;
            pictureBox3.Visible = false;

            panel1.Visible = true;
            panel1.BringToFront();
        }

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

            // ���ҽ� �̹��� �ֱ�
            var detective = Properties.Resources.��Ʈ��_2_Ž������;

            // pictureBox3 �¾�
            pictureBox3.Parent = this; 
            pictureBox3.Image = detective;
            pictureBox3.Visible = true;

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

        private void label1_Click(object sender, EventArgs e) { }
        private void pictureBox3_Click(object sender, EventArgs e) { }
        private void Box_Click(object sender, EventArgs e) { }
    }
}

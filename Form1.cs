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
            pictureBox1.Visible = true; // ���� ��� ��Ʈ�� 
            pictureBox2.Visible = false; // ���� ���� 
            pictureBox3.Visible = false;

            panel1.Visible = false;
            panel1.BackColor = Color.Transparent;
            panel1.Parent = pictureBox2;

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            pictureBox1.Visible = false;
            pictureBox2.Visible = true;
            pictureBox3.Visible = false;

            panel1.Visible = true;
            panel1.BringToFront();
        }

        private void panel1_Click(object sender, EventArgs e)
        {
            pictureBox1.Visible = false; // ���� ��Ʈ��
            pictureBox2.Visible = false; // ���� ����
            panel1.Visible = false;
            panel1.SendToBack();

            pictureBox3.Image = Properties.Resources.��Ʈ��_2_Ž������;

            pictureBox3.Visible = true;
            pictureBox3.BringToFront();


        }

        
    }
}

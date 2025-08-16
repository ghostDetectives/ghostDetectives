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

            // 초기 상태 설정
            pictureBox1.Visible = true; // 검은 배경 인트로 
            pictureBox2.Visible = false; // 여주 등장 
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
            pictureBox1.Visible = false; // 검정 인트로
            pictureBox2.Visible = false; // 여주 등장
            panel1.Visible = false;
            panel1.SendToBack();

            pictureBox3.Image = Properties.Resources.인트로_2_탐정등장;

            pictureBox3.Visible = true;
            pictureBox3.BringToFront();


        }

        
    }
}

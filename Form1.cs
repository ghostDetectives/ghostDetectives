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
            pictureBox1.Visible = true;  // 검은 배경 인트로 
            pictureBox2.Visible = false; // 여주 등장 
            pictureBox3.Visible = false;

            panel1.Visible = false;
            panel1.BackColor = Color.Transparent;
            panel1.Parent = pictureBox2; // 여주 위에 덮는 오버레이
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
            // 다른 것들 숨기기
            pictureBox1.Visible = false;
            pictureBox2.Visible = false;

            // panel1을 폼 자식으로 만들고 숨김
            if (panel1.Parent != this)
                panel1.Parent = this;
            panel1.Visible = false;
            panel1.SendToBack();

            // 리소스 이미지 넣기
            var detective = Properties.Resources.인트로_2_탐정등장;

            // pictureBox3 셋업
            pictureBox3.Parent = this; 
            pictureBox3.Image = detective;
            pictureBox3.Visible = true;

            // 맨 위로
            this.Controls.SetChildIndex(pictureBox3, 0);
            pictureBox3.BringToFront();

            // 새로고침
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

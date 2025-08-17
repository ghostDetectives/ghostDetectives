using System;
using System.Reflection;
using System.Windows.Forms;
using ghostDetectives.Properties;
using Microsoft.VisualBasic.ApplicationServices;

namespace ghostDetectives
{
    public partial class Form1 : Form
    {
        private int panel2ClickCount = 0; // 클릭 횟수 저장
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

            panel2.Visible = false;

        }

        private void pictureBox1_Click(object sender, EventArgs e) // 검은 인트로 클릭 시 여주 등장으로 넘어감
        {

            pictureBox1.Visible = false;
            pictureBox2.Visible = true;
            pictureBox3.Visible = false;

            panel1.Visible = true;
            panel1.BringToFront();
        }

        int detectiveImageIndex = 0; // 탐정 이미지 순서
        Image[] detectiveImages = new Image[]
        {
            Properties.Resources.인트로_1_문닫혀있는,
            Properties.Resources.인트로_2_탐정등장,
            Properties.Resources._2_탐정_등장,
            Properties.Resources._3_탐정_등장,
            Properties.Resources.인트로_탐정_3인칭
        };

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

            pictureBox3.Image = detectiveImages[0];

            // pictureBox3 셋업
            pictureBox3.Parent = this; ;
            pictureBox3.Visible = true;

            detectiveImageIndex = 0; // 처음부터 시작
            timer1.Start(); // 타이머 시작

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

        private void timer1_Tick(object sender, EventArgs e)
        {
            detectiveImageIndex++;

            if (detectiveImageIndex >= detectiveImages.Length)
            {
                // 마지막 이미지까지 나오면 타이머 정지
                timer1.Stop();
                pictureBox3.Enabled = true;
                pictureBox3.Click += PictureBox3Final_Click;

                return;
            }

            // 다음 이미지 출력
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
                // 첫 번째 클릭: 라벨 변경
                label2.Visible = false;
                label3.Visible = true;
            }
            else if (panel2ClickCount == 2)
            {
                // 두 번째 클릭: 검은 화면
                blackPanel.Visible = true;
            }
        }
       
    }
}

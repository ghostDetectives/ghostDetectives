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
        private int panel3ClickCount = 0; // 클릭 횟수 저장
        private int panel2ClickCount = 0;
        private int panel4ClickCount = 0;
        private int picturBox8ClickCount = 0;



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

            // 검은 패널 미리 생성
            blackPanel = new Panel();
            blackPanel.BackColor = Color.Black;
            blackPanel.Location = new Point(0, 0);
            blackPanel.Size = this.ClientSize;
            blackPanel.Visible = false;
            blackPanel.BringToFront();
            this.Controls.Add(blackPanel);

            panel3.MouseClick += panel3_Click;

        }

        private void pictureBox1_Click(object sender, EventArgs e) // 검은 인트로 클릭 시 여주 등장으로 넘어감
        {

            pictureBox1.Visible = false;
            pictureBox2.Visible = true; // 여주등장
            pictureBox3.Visible = false;

            dotBox.Visible = false;
            dotLabel.Visible = false;

            pictureBox4.Visible = false; // 한여주가 시체 발견

            panel3.Visible = false; // 어라 저건 난데 ~ 여주
            panel1.Visible = true;
            panel1.BringToFront();
        }
        private void ShowBlackPanel()
        {
            pictureBox1.Visible = false;
            pictureBox2.Visible = false;
            pictureBox4.Visible = false;
            panel3.Visible = false;

            // panel1을 폼 자식으로 만들고 숨김
            if (panel1.Parent != this)
                panel1.Parent = this;
            panel1.Visible = false;
            panel1.SendToBack();

            blackPanel.Visible = true;

            // dotBox, dotLabel은 blackPanel 위에 올려도 클릭이 blackPanel로 전달되게
            dotBox.Visible = true;
            dotBox.Parent = blackPanel;
            dotBox.BackColor = Color.Transparent;
            dotBox.BringToFront();

            dotLabel.Visible = true;
            dotLabel.Parent = blackPanel;
            dotLabel.BackColor = Color.Transparent;
            dotLabel.BringToFront();

            // blackPanel 클릭 시 이벤트 연결
            blackPanel.Click -= blackPanel_Click;
            blackPanel.Click += blackPanel_Click;

        }

        private void panel1_Click(object sender, EventArgs e) // 검은화면 나타남, 검은 화면 눌러야 
        {
            ShowBlackPanel();
        }

        private void ShowCorpse()
        {
            // 먼저 화면에서 숨길 컨트롤들
            Control[] controlsToHide = { pictureBox1, pictureBox2, panel1, label5, blackPanel };
            foreach (var ctrl in controlsToHide)
            {
                ctrl.Visible = false;
            }

            // 보여줄 컨트롤
            pictureBox4.Parent = this; // 확실히 폼에 추가
            pictureBox4.BringToFront();
            pictureBox4.Visible = true;
            panel3.BackColor = Color.Transparent;
            panel3.Parent = pictureBox4;
            panel3.Visible = true;
            panel3.BringToFront();

            // panel3 안의 라벨 설정
            label4.Visible = true;

            // 새로고침
            pictureBox4.Refresh();
            panel3.Refresh();
            this.Refresh();
        }


        private void blackPanel_Click(object sender, EventArgs e) // 여주 시체 발견
        {
            ShowCorpse();
        }

        private void panel3_Click(object sender, EventArgs e) // 여주 대사 변경
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


        int detectiveImageIndex = 0; // 탐정 이미지 순서
        Image[] detectiveImages = new Image[]
        {
            Properties.Resources.인트로_1_문닫혀있는,
            Properties.Resources.인트로_2_탐정등장,
            Properties.Resources.인트로_3_탐정등장,
            Properties.Resources.인트로_4_탐정등장,
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
            pictureBox3.Parent = this;
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
                pictureBox3.Click += PictureBox3_Click;

                return;
            }

            // 다음 이미지 출력
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

        private void ShowBlackPanel2() /// 두번째 검은화면 (탐정)
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
                // 첫 번째 클릭: 라벨 변경
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
                // 두 번째 클릭: 검은 화면
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
                // 첫 번째 클릭: 라벨 변경
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
                // 두 번째 클릭: 라벨 변경
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
                // 세번째 클릭: 점점점 말풍선
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
                // 네번째 클릭: 픽박8 전환 (날 보는 탐정)
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
                // 첫 번째 클릭: 날 보는 탐정 확대

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
                // 두 번째 클릭: 라벨 변경


            }



        }
    }
}

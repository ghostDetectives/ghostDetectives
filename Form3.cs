using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Reflection.Emit;
using System.Windows.Forms;


namespace ghostDetectives
{
    public partial class Form3 : Form
    {
        private bool memoryPanelTriggered = false; // 최초 1회 플래그
        int[] memoryDurations = new int[] { 4000, 1000, 1500, 1500, 4000, 2000 }; // 소리 넣고 그에 맞춰서 시간 조절 필요 *****




        // ===== 이동/애니메이션 상태 =====
        private bool walkingUp = false;
        private bool walkingDown = false;
        private bool walkingRight = false;
        private bool walkingLeft = false;

        private System.Windows.Forms.Timer walkTimer;

        // ===== 카메라/플레이어 =====
        private float camX = 0f;
        private float camY = 0f;

        private PointF playerPos = new PointF(250, 400); //플레이어 위치
        private readonly float _speedPxPerTick = 20f;
        private readonly int _intervalMs = 100;

        private bool toggleImage = false;
        private enum Facing { Up, Down, Right, Left }
        private Facing lastFacing = Facing.Down;

        // ===== 배경 스케일링 =====
        private readonly float _mapScale = 0.9f; // 원하는 배율
        private float _currentMapScale = 1.0f;   // 현재 맵 배율(원본 기준)

        private Image _originalMap;   // 리소스에서 읽은 원본(Dispose 금지)
        private Bitmap _scaledMap;    // 우리가 만든 스케일 버전 (Dispose 필요)

        // ===== 벽 모음 =====
        private PictureBox[] _walls;
        private Dictionary<PictureBox, Rectangle> _obstacleOriginals = new Dictionary<PictureBox, Rectangle>();

        public Form3()
        {
            InitializeComponent();

            timer1 = new System.Windows.Forms.Timer();
            timer1.Tick += timer1_Tick;

            memoryPb1.Visible = false;
            memoryPanel.BackColor = Color.Transparent;
            memoryPanel.Parent = hospitalPanel;
            memoryPanel.Visible = true;
            memoryPanel.BringToFront();


            Control[] controlsToHide = { com1, medicalRecords, x1, file1, file2, power, inmunet,inmunet,
                search, digoxin, cafeBT, inmunetScreen1, searchScreen
  };
            foreach (var ctrl in controlsToHide)
            {
                ctrl.Visible = false;
            }


            // ======== 오브젝트(오버레이 등) 위치 =======
            pictureBox1.Location = new Point(200, 100); // 화면 상의 특정 오브젝트(예: UI/오버레이). 병원 패널 위에 올려놓음.

            // ========= 벽 위치(원본 좌표 기준으로 설정) ========
            wallBox1.Location = new Point(498, 345); // 예: 서재방 위위
            wallBox1.Size = new Size(20, 34); // 폭, 높이
            wallBox2.Location = new Point(498, 375);
            wallBox2.Size = new Size(550, 40);
            wallBox3.Location = new Point(1058, 0);
            wallBox3.Size = new Size(30, 550);
            wallBox4.Location = new Point(425, 320);
            wallBox4.Size = new Size(90, 30);
            wallBox5.Location = new Point(30, 320);
            wallBox5.Size = new Size(190, 30);
            wallBox6.Location = new Point(20, 0);
            wallBox6.Size = new Size(30, 550);
            wallBox7.Location = new Point(15, 690);
            wallBox7.Size = new Size(220, 20);
            wallBox8.Location = new Point(20, 70);
            wallBox8.Size = new Size(1220, 20);
            wallBox9.Location = new Point(420, 690);
            wallBox9.Size = new Size(1220, 20);

            // ===== 벽/충돌 대상 등록 =====
            _walls = new[] { wallBox1, wallBox2, wallBox3, wallBox4, wallBox5, wallBox6, wallBox7, wallBox8, wallBox9 };

            // ====== UI 초기 상태 ======
            this.AutoScroll = false;
            viewportPanel.AutoScroll = false;

            hospitalPanel.BackgroundImageLayout = ImageLayout.None;
            hospitalPanel.Dock = DockStyle.None;

            // === 키 입력 ===
            this.KeyPreview = true;
            this.KeyDown += Form3_KeyDown;
            this.KeyUp += Form3_KeyUp;

            // === 플레이어 이미지 초기화 ===
            playerBox.SizeMode = PictureBoxSizeMode.Zoom;
            playerBox.Image = Properties.Resources.앞1;

            // === 계층(Parent) 강제 ===
            if (hospitalPanel.Parent != viewportPanel) hospitalPanel.Parent = viewportPanel;
            if (playerBox.Parent != viewportPanel) playerBox.Parent = viewportPanel;
            hospitalPanel.SendToBack();
            playerBox.BringToFront();

            // === 오버레이는 맵(hospitalPanel) 위에 ===
            if (pictureBox1.Parent != hospitalPanel) pictureBox1.Parent = hospitalPanel;
            pictureBox1.BringToFront();

            // === 벽 부모(좌표계 일치) ===
            foreach (var w in _walls)
            {
                if (w.Parent != hospitalPanel) w.Parent = hospitalPanel;
            }

            // === 맵 원본 확보 & 스케일 적용 ===
            _originalMap = hospitalPanel.BackgroundImage; // 디자이너에 배경이 설정돼 있다고 가정
            CaptureObstacleOriginals();                   // 현재 벽들의 "원본 좌표" 저장
            ApplyMapScale(_mapScale);                     // 배경 스케일 적용(내부에서 _currentMapScale 갱신)
            ScaleObstaclesFromOriginals(_currentMapScale); // 벽들도 같은 배율로 반영

            // === 타이머 ===
            walkTimer = new System.Windows.Forms.Timer();
            walkTimer.Interval = _intervalMs;
            walkTimer.Tick += WalkTimer_Tick;

            // === 리사이즈 시 카메라 갱신 ===
            this.Resize += (s, e) => UpdateCameraAndLayers();
            viewportPanel.Resize += (s, e) => UpdateCameraAndLayers();

            // === 클릭 이벤트 (예: 에너지바) ===
            pictureBox1.Click -= pictureBox1_Click;
            pictureBox1.Click += pictureBox1_Click;

            UpdateCameraAndLayers();
        }

        int memoryImageIndex = 0; // 기억 이미지 순서
        Image[] memoryImages = new Image[]
        {
            Properties.Resources.기억_1_수술상담1,
            Properties.Resources.기억_21,
            Properties.Resources.기억_31,
            Properties.Resources.기억_51,
            Properties.Resources.기억_61,
            Properties.Resources.기억_71,
        };


        private void ShowMemory()
        {
            Control[] controlsToHide = { memoryPanel, pictureBox1, playerBox, com1, inmunet, power, file1, file2 };
            foreach (var ctrl in controlsToHide)
            {
                ctrl.Visible = false;
            }



            memoryImageIndex = 0;
            memoryPb1.Image = memoryImages[memoryImageIndex];
            memoryPb1.Visible = true;
            timer1.Start();

        }

        private void CheckCollision()
        {
            if (!memoryPanelTriggered && playerBox.Bounds.IntersectsWith(memoryPanel.Bounds))
            {
                memoryPanelTriggered = true;
                ShowMemory();
            }

        }

        private void timer1_Tick(object sender, EventArgs e) // 타이머 ========================================
        {

            if (memoryImageIndex < memoryImages.Length) // 여주로 바꿔줘야 대사까지 가능***********
            {
                memoryPb1.Image = memoryImages[memoryImageIndex];
                timer1.Interval = memoryDurations[memoryImageIndex]; // 이미지별 간격 적용
                memoryImageIndex++;
            }
            else
            {
                timer1.Stop();
                memoryPb1.Visible = false;
                playerBox.Visible = true;
                memoryPanel.Visible = true;
                pictureBox1.Visible = true;
            }
        }
        private void ShowScreen() // 기본 컴퓨터 화면 보여주기
        {
            Control[] controlsToHide = { memoryPanel, pictureBox1, playerBox };
            foreach (var ctrl in controlsToHide)
            {
                ctrl.Visible = false;
            }

            com1.Parent = this;

            inmunet.BackColor = Color.Transparent;
            file1.BackColor = Color.Transparent;
            file2.BackColor = Color.Transparent;
            power.BackColor = Color.Transparent;

            //inmunet.Location = new Point(67, 625);
            //file2.Location = new Point(120, 625);
            //power.Location = new Point(15, 625);
            //file1.Location = new Point(25, 126); ==> this로 해서 필요없음

            inmunet.Parent = this;
            file1.Parent = this;
            file2.Parent = this;
            power.Parent = this;

            inmunet.Parent = com1;
            file1.Parent = com1;
            file2.Parent = com1;
            power.Parent = com1;

            com1.Visible = true;
            inmunet.Visible = true;
            file1.Visible = true;
            file2.Visible = true;
            power.Visible = true;

            com1.BringToFront();
            inmunet.BringToFront();
            power.BringToFront();
            file1.BringToFront();
            file2.BringToFront();
        }
        private void pictureBox2_Click(object sender, EventArgs e) // 컴퓨터 클릭시
        {
            ShowScreen();

        }

        private void ShowFile() // 파일 보여주기
        {
            medicalRecords.Parent = this;
            medicalRecords.Visible = true;
            medicalRecords.BringToFront();

            //x1.Location = new Point(697, 35);


            x1.BackColor = Color.Transparent;
            x1.Parent = this;
            x1.Parent = medicalRecords;
            x1.Visible = true;
            x1.BringToFront();

        }

        private void file1_Click(object sender, EventArgs e) // 진료기록
        {
            ShowFile();
        }

        private void file2_Click(object sender, EventArgs e) // 파일 탐색기 클릭
        {
            ShowFile();

        }

        private void x1_Click(object sender, EventArgs e)
        {
            ShowScreen();
        }

        private void inmunet_Click(object sender, EventArgs e) // 인터넷 클릭
        {

            Control[] controlsToHide = { memoryPanel, pictureBox1, playerBox };
            foreach (var ctrl in controlsToHide)
            {
                ctrl.Visible = false;
            }

            inmunetScreen1.Parent = this; // 배경
            inmunetScreen1.Visible = true;
            inmunetScreen1.BringToFront();

            search.BackColor = Color.Transparent;
            search.Parent = this; // 검색바
            search.Parent = inmunetScreen1;
            search.Visible = true;
            search.BringToFront();

            cafeBT.BackColor = Color.Transparent;
            cafeBT.Parent = this;
            cafeBT.Parent = inmunetScreen1;
            cafeBT.Visible = true;
            cafeBT.BringToFront();
        }

        private void search_Click(object sender, EventArgs e) // 검색바 클릭
        {
            Control[] controlsToHide = { memoryPanel, pictureBox1, playerBox };
            foreach (var ctrl in controlsToHide)
            {
                ctrl.Visible = false;
            }

            searchScreen.Parent = this;
            searchScreen.Visible = true;
            searchScreen.BringToFront();

            digoxin.BackColor = Color.Transparent;
            digoxin.Parent = this; // 디곡신 검색어 창
            digoxin.Visible = true;
            digoxin.BringToFront();

            cafeBT1.BackColor = Color.Transparent;
            cafeBT1.Parent = this;
            cafeBT1.Visible = true;
            cafeBT1.BringToFront();
        }

        private void digoxin_Click(object sender, EventArgs e) // 디곡신
        {

        }

        private void ShowCafe()
        {

        }

        private void cafeBT_Click(object sender, EventArgs e) // 카페 클릭
        {

        }

        private void cafeBT1_Click(object sender, EventArgs e) // 카페 클릭
        {

        }



        private void power_Click(object sender, EventArgs e) // 전원버튼
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            // TODO: 에너지바/오브젝트 클릭 처리
        }

        // ================== 배경 축소(방법2) 구현 ==================
        private void ApplyMapScale(float scale)
        {
            if (_originalMap == null) return;

            // 기존 스케일된 비트맵 정리
            if (_scaledMap != null)
            {
                _scaledMap.Dispose();
                _scaledMap = null;
            }

            int w = Math.Max(1, (int)Math.Round(_originalMap.Width * scale));
            int h = Math.Max(1, (int)Math.Round(_originalMap.Height * scale));

            var bmp = new Bitmap(w, h);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                g.SmoothingMode = SmoothingMode.HighQuality;
                g.CompositingQuality = CompositingQuality.HighQuality;

                g.DrawImage(_originalMap, new Rectangle(0, 0, w, h));
            }

            _scaledMap = bmp;

            hospitalPanel.BackgroundImage = _scaledMap;
            hospitalPanel.Size = _scaledMap.Size;
            hospitalPanel.BackgroundImageLayout = ImageLayout.None;

            _currentMapScale = scale;

            UpdateCameraAndLayers();
        }

        // ============ 벽 원본 좌표 저장(스케일 재적용을 위해) ============
        private void CaptureObstacleOriginals()
        {
            _obstacleOriginals.Clear();
            foreach (var w in _walls)
            {
                if (w == null) continue;
                _obstacleOriginals[w] = new Rectangle(w.Left, w.Top, w.Width, w.Height);
            }
        }

        // ============ 벽 스케일 적용(항상 "원본 좌표"에서 재계산) ============
        private void ScaleObstaclesFromOriginals(float scale)
        {
            foreach (var kv in _obstacleOriginals)
            {
                var box = kv.Key;
                var r = kv.Value;

                int sx = (int)Math.Round(r.X * scale);
                int sy = (int)Math.Round(r.Y * scale);
                int sw = Math.Max(1, (int)Math.Round(r.Width * scale));
                int sh = Math.Max(1, (int)Math.Round(r.Height * scale));

                box.Location = new Point(sx, sy);
                box.Size = new Size(sw, sh);
            }
        }

        // ================== 입력 처리 ==================
        private void Form3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.W) { walkingUp = true; lastFacing = Facing.Up; if (!walkTimer.Enabled) walkTimer.Start(); }
            else if (e.KeyCode == Keys.S) { walkingDown = true; lastFacing = Facing.Down; if (!walkTimer.Enabled) walkTimer.Start(); }
            else if (e.KeyCode == Keys.D) { walkingRight = true; lastFacing = Facing.Right; if (!walkTimer.Enabled) walkTimer.Start(); }
            else if (e.KeyCode == Keys.A) { walkingLeft = true; lastFacing = Facing.Left; if (!walkTimer.Enabled) walkTimer.Start(); }
        }

        private void Form3_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.W) walkingUp = false;
            else if (e.KeyCode == Keys.S) walkingDown = false;
            else if (e.KeyCode == Keys.D) walkingRight = false;
            else if (e.KeyCode == Keys.A) walkingLeft = false;

            if (!walkingUp && !walkingDown && !walkingRight && !walkingLeft && walkTimer.Enabled)
            {
                walkTimer.Stop();
                toggleImage = false;
                SetIdleSprite();
            }
        }

        // ================== 이동 + 카메라 + 애니메이션 ==================
        private void WalkTimer_Tick(object sender, EventArgs e)
        {
            if (!walkingUp && !walkingDown && !walkingRight && !walkingLeft) return;

            float dx = 0, dy = 0;
            if (walkingUp) dy -= _speedPxPerTick;
            if (walkingDown) dy += _speedPxPerTick;
            if (walkingRight) dx += _speedPxPerTick;
            if (walkingLeft) dx -= _speedPxPerTick;

            // --- X축 먼저 시도 ---
            PointF tryPosX = new PointF(playerPos.X + dx, playerPos.Y);
            var rectTryX = PlayerWorldRectAt(tryPosX);
            if (!Collides(rectTryX))
            {
                playerPos = tryPosX;
            }

            // --- Y축 다음 시도 ---
            PointF tryPosY = new PointF(playerPos.X, playerPos.Y + dy);
            var rectTryY = PlayerWorldRectAt(tryPosY);
            if (!Collides(rectTryY))
            {
                playerPos = tryPosY;
            }

            // 맵 경계 클램프
            if (_scaledMap != null)
            {
                var halfW = playerBox.Width / 2f;
                var halfH = playerBox.Height / 2f;
                playerPos.X = Clamp(playerPos.X, halfW, _scaledMap.Width - halfW);
                playerPos.Y = Clamp(playerPos.Y, halfH, _scaledMap.Height - halfH);
            }

            UpdateWalkAnimation();
            UpdateCameraAndLayers();

            CheckCollision(); // ==================================
        }

        private void UpdateWalkAnimation()
        {
            toggleImage = !toggleImage;

            if (walkingUp)
            {
                lastFacing = Facing.Up;
                playerBox.Image = toggleImage ? Properties.Resources.뒤1 : Properties.Resources.뒤2;
            }
            else if (walkingDown)
            {
                lastFacing = Facing.Down;
                playerBox.Image = toggleImage ? Properties.Resources.앞1 : Properties.Resources.앞2;
            }
            else if (walkingRight)
            {
                lastFacing = Facing.Right;
                playerBox.Image = toggleImage ? Properties.Resources.오른쪽옆1 : Properties.Resources.오른쪽옆2;
            }
            else if (walkingLeft)
            {
                lastFacing = Facing.Left;
                playerBox.Image = toggleImage ? Properties.Resources.왼쪽옆1 : Properties.Resources.왼쪽옆2;
            }
        }

        private void SetIdleSprite()
        {
            switch (lastFacing)
            {
                case Facing.Up: playerBox.Image = Properties.Resources.뒤1; break;
                case Facing.Down: playerBox.Image = Properties.Resources.앞1; break;
                case Facing.Right: playerBox.Image = Properties.Resources.오른쪽옆1; break;
                case Facing.Left: playerBox.Image = Properties.Resources.왼쪽옆1; break;
            }
        }

        private void UpdateCameraAndLayers()
        {
            if (viewportPanel.ClientSize.Width <= 0 || viewportPanel.ClientSize.Height <= 0)
                return;

            int vw = viewportPanel.ClientSize.Width;
            int vh = viewportPanel.ClientSize.Height;

            int mapW = (_scaledMap != null) ? _scaledMap.Width : hospitalPanel.Width;
            int mapH = (_scaledMap != null) ? _scaledMap.Height : hospitalPanel.Height;

            camX = playerPos.X - vw / 2f;
            camY = playerPos.Y - vh / 2f;

            camX = Clamp(camX, 0, Math.Max(0, mapW - vw));
            camY = Clamp(camY, 0, Math.Max(0, mapH - vh));

            hospitalPanel.Location = new Point(-(int)Math.Round(camX), -(int)Math.Round(camY));

            int screenX = (int)Math.Round(playerPos.X - camX - playerBox.Width / 2f);
            int screenY = (int)Math.Round(playerPos.Y - camY - playerBox.Height / 2f);

            playerBox.Location = new Point(screenX, screenY);
            playerBox.BringToFront();

            // 오버레이는 항상 제일 위
            pictureBox1.BringToFront();
        }

        // ================== 충돌/좌표 유틸 ==================
        private RectangleF PlayerWorldRectAt(PointF pos)
        {
            float w = playerBox.Width * 0.9f;   // 원하는 히트박스 비율(예: 90%)
            float h = playerBox.Height * 0.9f;
            return new RectangleF(pos.X - w / 2f, pos.Y - h / 2f, w, h);
        }

        private RectangleF ObstacleWorldRect(PictureBox obstacle)
        {
            // 부모가 hospitalPanel(월드 좌표계)라고 가정
            return new RectangleF(obstacle.Left, obstacle.Top, obstacle.Width, obstacle.Height);
        }

        private bool Collides(RectangleF rect)
        {
            if (_walls == null) return false;

            foreach (var w in _walls)
            {
                if (w == null || !w.Visible) continue;
                if (rect.IntersectsWith(ObstacleWorldRect(w)))
                    return true;
            }
            return false;
        }

        private static float Clamp(float v, float min, float max) => Math.Max(min, Math.Min(max, v));

        // ===== 메모리 정리 =====
        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            base.OnFormClosed(e);

            if (_scaledMap != null)
            {
                _scaledMap.Dispose();
                _scaledMap = null;
            }
        }

    }
}

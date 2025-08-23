using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace ghostDetectives
{
    public partial class Form4 : Form
    {
        // ===== 이동/애니메이션 상태 =====
        private bool walkingUp = false;
        private bool walkingDown = false;
        private bool walkingRight = false;
        private bool walkingLeft = false;

        private System.Windows.Forms.Timer walkTimer;

        // ===== 카메라/플레이어 =====
        private float camX = 0f;
        private float camY = 0f;

        private PointF playerPos = new PointF(250, 500); //플레이어 위치
        private readonly float _speedPxPerTick = 20f;
        private readonly int _intervalMs = 100;

        private bool toggleImage = false;
        private enum Facing { Up, Down, Right, Left }
        private Facing lastFacing = Facing.Down;

        // ===== 배경 스케일링 =====
        private readonly float _mapScale = 0.65f; // 원하는 배율
        private float _currentMapScale = 1.0f;   // 현재 맵 배율(원본 기준)

        private Image _originalMap;   // 리소스에서 읽은 원본(Dispose 금지)
        private Bitmap _scaledMap;    // 우리가 만든 스케일 버전 (Dispose 필요)

        // ===== 벽 모음 =====
        private PictureBox[] _walls;
        private Dictionary<PictureBox, Rectangle> _obstacleOriginals = new Dictionary<PictureBox, Rectangle>();

        public Form4()
        {
            InitializeComponent();

            // =======객체 위치 =======
            pictureBox1.Location = new Point(200, 100); // 객체 위치.

            // ========= 벽 위치(원본 좌표 기준으로 설정) ========
            wallBox1.Location = new Point(1855, 145); // 가로, 세로
            wallBox1.Size = new Size(20, 800); // 폭, 높이
            wallBox2.Location = new Point(30, 145);
            wallBox2.Size = new Size(20, 800);
            wallBox3.Location = new Point(100, 1070);
            wallBox3.Size = new Size(700, 50);
            wallBox4.Location = new Point(30, 1045);
            wallBox4.Size = new Size(570, 30);
            wallBox5.Location = new Point(1028, 1095);
            wallBox5.Size = new Size(700, 50);
            wallBox6.Location = new Point(760, 570);
            wallBox6.Size = new Size(20, 550);
            wallBox7.Location = new Point(780, 570);
            wallBox7.Size = new Size(20, 195);
            wallBox8.Location = new Point(1020, 573);
            wallBox8.Size = new Size(30, 195);
            wallBox9.Location = new Point(1027, 68);
            wallBox9.Size = new Size(25, 700);
            wallBox10.Location = new Point(1200, 140);
            wallBox10.Size = new Size(600, 125);
            wallBox11.Location = new Point(1382, 800);
            wallBox11.Size = new Size(400, 125);
            wallBox12.Location = new Point(80, 40);
            wallBox12.Size = new Size(500, 200);
            wallBox13.Location = new Point(80, 600);
            wallBox13.Size = new Size(450, 20);
            wallBox14.Location = new Point(280, 30);
            wallBox14.Size = new Size(600, 20);
            wallBox15.Location = new Point(1520, 630);
            wallBox15.Size = new Size(120, 120);

            // ===== 벽/충돌 대상 등록 =====
            _walls = new[] { wallBox1, wallBox2, wallBox3, wallBox4, wallBox5, wallBox6, wallBox7, wallBox8, wallBox9, wallBox10, wallBox11, wallBox12, wallBox13, wallBox14, wallBox15 };

            // ====== UI 초기 상태 ======
            this.AutoScroll = false;
            viewportPanel.AutoScroll = false;

            companyPanel.BackgroundImageLayout = ImageLayout.None;
            companyPanel.Dock = DockStyle.None;

            // === 키 입력 ===
            this.KeyPreview = true;
            this.KeyDown += Form4_KeyDown;
            this.KeyUp += Form4_KeyUp;

            // === 플레이어 이미지 초기화 ===
            playerBox.SizeMode = PictureBoxSizeMode.Zoom;
            playerBox.Image = Properties.Resources.앞1;

            // === 계층(Parent) 강제 ===
            if (companyPanel.Parent != viewportPanel) companyPanel.Parent = viewportPanel;
            if (playerBox.Parent != viewportPanel) playerBox.Parent = viewportPanel;
            companyPanel.SendToBack();
            playerBox.BringToFront();

            // === 오버레이는 맵(hospitalPanel) 위에 ===
            if (pictureBox1.Parent != companyPanel) pictureBox1.Parent = companyPanel;
            pictureBox1.BringToFront();

            // === 벽 부모(좌표계 일치) ===
            foreach (var w in _walls)
            {
                if (w.Parent != companyPanel) w.Parent = companyPanel;
            }

            // === 맵 원본 확보 & 스케일 적용 ===
            _originalMap = companyPanel.BackgroundImage; // 디자이너에 배경이 설정돼 있다고 가정
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

            companyPanel.BackgroundImage = _scaledMap;
            companyPanel.Size = _scaledMap.Size;
            companyPanel.BackgroundImageLayout = ImageLayout.None;

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
        private void Form4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.W) { walkingUp = true; lastFacing = Facing.Up; if (!walkTimer.Enabled) walkTimer.Start(); }
            else if (e.KeyCode == Keys.S) { walkingDown = true; lastFacing = Facing.Down; if (!walkTimer.Enabled) walkTimer.Start(); }
            else if (e.KeyCode == Keys.D) { walkingRight = true; lastFacing = Facing.Right; if (!walkTimer.Enabled) walkTimer.Start(); }
            else if (e.KeyCode == Keys.A) { walkingLeft = true; lastFacing = Facing.Left; if (!walkTimer.Enabled) walkTimer.Start(); }
        }

        private void Form4_KeyUp(object sender, KeyEventArgs e)
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

            int mapW = (_scaledMap != null) ? _scaledMap.Width : companyPanel.Width;
            int mapH = (_scaledMap != null) ? _scaledMap.Height : companyPanel.Height;

            camX = playerPos.X - vw / 2f;
            camY = playerPos.Y - vh / 2f;

            camX = Clamp(camX, 0, Math.Max(0, mapW - vw));
            camY = Clamp(camY, 0, Math.Max(0, mapH - vh));

            companyPanel.Location = new Point(-(int)Math.Round(camX), -(int)Math.Round(camY));

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

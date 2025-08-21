using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace ghostDetectives
{
    public partial class Form2 : Form
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

        private PointF playerPos = new PointF(500, 400);
        private readonly float _speedPxPerTick = 20f;
        private readonly int _intervalMs = 100;

        private bool toggleImage = false;
        private enum Facing { Up, Down, Right, Left }
        private Facing lastFacing = Facing.Down;

        // ===== 배경 스케일링 (방법2) =====
        // 원하는 배율만 조절하면 됨 (예: 0.5f = 50% 축소)
        private readonly float _mapScale = 0.6f;

        // 원본 맵(리소스)과 스케일된 맵을 분리 보관 (필요 시 재적용/정리)
        private Image _originalMap;   // 리소스에서 읽은 원본(Dispose 금지: 리소스 공유 가능성)
        private Bitmap _scaledMap;    // 우리가 생성한 스케일 버전 (Dispose 필요)

        public Form2()
        {
            InitializeComponent();


            // ======== 에너지바 위치 =======
            pictureBox1.Location = new Point(200, 100);

            // ====== UI 초기 상태 ======
            cluePanel.BackColor = Color.FromArgb(200, Color.Black);
            cluePanel.Visible = false;

            this.AutoScroll = false;
            viewportPanel.AutoScroll = false;

            housePanel.BackgroundImageLayout = ImageLayout.None;
            housePanel.Dock = DockStyle.None;



            // === 키 입력 ===
            this.KeyPreview = true;
            this.KeyDown += Form2_KeyDown;
            this.KeyUp += Form2_KeyUp;

            // === 플레이어 이미지 초기화 ===
            playerBox.SizeMode = PictureBoxSizeMode.Zoom;
            playerBox.Image = Properties.Resources.앞1;

            // === 카메라 계층 강제 ===
            if (housePanel.Parent != viewportPanel) housePanel.Parent = viewportPanel;
            if (playerBox.Parent != viewportPanel) playerBox.Parent = viewportPanel;
            housePanel.SendToBack();
            playerBox.BringToFront();

            // === 오버레이는 폼 최상단으로 ===
            if (pictureBox1.Parent != this) pictureBox1.Parent = this;
            if (cluePanel.Parent != this) cluePanel.Parent = this;
            pictureBox1.BringToFront();
            cluePanel.BringToFront();

            // === 맵 원본 확보 & 스케일 적용 ===
            // housePanel.BackgroundImage가 디자이너/리소스로 이미 설정돼 있다고 가정
            _originalMap = housePanel.BackgroundImage;
            ApplyMapScale(_mapScale); // ↓ 내부에서 housePanel.Size/BackgroundImage 갱신

            // === 플레이어 시작 위치 (하단 중앙 근처) 보정 ===
            if (_scaledMap != null)
            {
                float halfW = playerBox.Width / 2f;
                float halfH = playerBox.Height / 2f;

                playerPos = new PointF(
                    halfW,
                    _scaledMap.Height - halfH
                );
            }

            // === 타이머 ===
            walkTimer = new System.Windows.Forms.Timer();
            walkTimer.Interval = _intervalMs;
            walkTimer.Tick += WalkTimer_Tick;

            // === 리사이즈 시 카메라 갱신 ===
            this.Resize += (s, e) => UpdateCameraAndLayers();
            viewportPanel.Resize += (s, e) => UpdateCameraAndLayers();

            // === 클릭 이벤트 보장 ===
            pictureBox1.Click -= panel1_Click;
            pictureBox1.Click += panel1_Click;

            UpdateCameraAndLayers();
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
                // 축소 품질
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                g.SmoothingMode = SmoothingMode.HighQuality;
                g.CompositingQuality = CompositingQuality.HighQuality;

                g.DrawImage(_originalMap, new Rectangle(0, 0, w, h));
            }

            _scaledMap = bmp;

            // 패널에 스케일된 이미지를 적용
            housePanel.BackgroundImage = _scaledMap;
            housePanel.Size = _scaledMap.Size;
            housePanel.BackgroundImageLayout = ImageLayout.None;

            // 카메라/레이어 재정렬
            UpdateCameraAndLayers();
        }

        // ================== 입력 처리 ==================
        private void Form2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.W) { walkingUp = true; lastFacing = Facing.Up; if (!walkTimer.Enabled) walkTimer.Start(); }
            else if (e.KeyCode == Keys.S) { walkingDown = true; lastFacing = Facing.Down; if (!walkTimer.Enabled) walkTimer.Start(); }
            else if (e.KeyCode == Keys.D) { walkingRight = true; lastFacing = Facing.Right; if (!walkTimer.Enabled) walkTimer.Start(); }
            else if (e.KeyCode == Keys.A) { walkingLeft = true; lastFacing = Facing.Left; if (!walkTimer.Enabled) walkTimer.Start(); }
        }

        private void Form2_KeyUp(object sender, KeyEventArgs e)
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

            playerPos = new PointF(playerPos.X + dx, playerPos.Y + dy);

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

            // 스케일된 맵 기준으로 카메라 계산
            int mapW = (_scaledMap != null) ? _scaledMap.Width : housePanel.Width;
            int mapH = (_scaledMap != null) ? _scaledMap.Height : housePanel.Height;

            camX = playerPos.X - vw / 2f;
            camY = playerPos.Y - vh / 2f;

            camX = Clamp(camX, 0, Math.Max(0, mapW - vw));
            camY = Clamp(camY, 0, Math.Max(0, mapH - vh));

            housePanel.Location = new Point(-(int)Math.Round(camX), -(int)Math.Round(camY));

            int screenX = (int)Math.Round(playerPos.X - camX - playerBox.Width / 2f);
            int screenY = (int)Math.Round(playerPos.Y - camY - playerBox.Height / 2f);

            playerBox.Location = new Point(screenX, screenY);
            playerBox.BringToFront();

            // 오버레이는 항상 제일 위
            pictureBox1.BringToFront();
            if (cluePanel.Visible) cluePanel.BringToFront();
        }

        private static float Clamp(float v, float min, float max) => Math.Max(min, Math.Min(max, v));

        // ================== 기타 UI 핸들러 ==================
        private void panel1_Click(object sender, EventArgs e)
        {
            cluePanel.Visible = true;
            clue1.Visible = true;
            clueBox1.Visible = true;
            clueLabel1.Visible = true;

            // 오버레이 최상단 유지
            cluePanel.BringToFront();
            pictureBox1.BringToFront();
        }

        private void cluePanel_Click(object sender, EventArgs e)
        {
            foreach (Control ctrl in this.Controls)
            {
                if (ctrl != viewportPanel && ctrl != pictureBox1 && ctrl != cluePanel)
                {
                    ctrl.Visible = false;
                }
            }

            cluePanel.Visible = false;

            viewportPanel.Visible = true;
            viewportPanel.BringToFront();

            pictureBox1.BringToFront();
        }

        private void playerBox_Click(object sender, EventArgs e) { }

        private void panel1_Paint(object sender, PaintEventArgs e) { }

        // ===== 메모리 정리 =====
        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            base.OnFormClosed(e);

            if (_scaledMap != null)
            {
                _scaledMap.Dispose();
                _scaledMap = null;
            }
            // _originalMap은 리소스 공유 가능성이 있어 여기서 Dispose 하지 않음.
        }
    }
}

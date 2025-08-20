using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ghostDetectives
{
    public partial class Form2 : Form
    {
        //private Image houseImage;

        public Form2()
        {
            InitializeComponent();

            cluePanel.BackColor = Color.FromArgb(200, Color.Black); // 불투명도
            cluePanel.Visible = false;
        }


        //private void Form2_Load(object sender, EventArgs e)
        //{
        //    houseImage = Properties.Resources.집_최종_완;
        //    housePanel.BackgroundImage = houseImage; // 실제 클릭할 때는 이미 로드돼 있음
        //}

        private void panel1_Click(object sender, EventArgs e) // 에너지바 클릭
        {
            cluePanel.Visible = true;
            clue1.Visible = true;
            clueBox1.Visible = true;
            clueLabel1.Visible = true;

            cluePanel.BringToFront();

        }

        private void cluePanel_Click(object sender, EventArgs e)
        {
            // 폼 안의 모든 컨트롤 숨기기
            foreach (Control ctrl in this.Controls)
            {
                if (ctrl != housePanel) // housePanel은 건드리지 않음
                {
                    ctrl.Visible = false;
                }
            }

            cluePanel.Visible = false;
            cluePanel.SendToBack();

            // housePanel만 다시 보이도록
            housePanel.Visible = true;
            housePanel.BringToFront();
        }

    }
}

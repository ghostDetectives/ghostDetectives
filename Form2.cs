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
        public Form2()
        {
            InitializeComponent();

            cluePanel.BackColor = Color.FromArgb(200, Color.Black); // 불투명도
            cluePanel.Visible = false;
        }


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
                ctrl.Visible = false;
            }

            // housePanel만 다시 보이도록
            housePanel.Visible = true;
            housePanel.BringToFront();
        }
    
    }
}

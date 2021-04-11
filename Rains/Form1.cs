using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Rains
{
    public partial class Form1 : Form
    {
        private Animator animator;
        public Form1()
        {
            InitializeComponent();
            animator = new Animator(main_panel.CreateGraphics());
            Drop.panelSize = main_panel.CreateGraphics().VisibleClipBounds.Size.ToSize();
        }

        private void main_panel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            animator.Start();
        }

        private void main_panel_Click(object sender, EventArgs e)
        {
        }

        private void button2_Click(object sender, EventArgs e)
        {
            animator.StopAll();

        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            Drop.speedID = trackBar1.Value;
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            Drop.windID = trackBar2.Value;
        }

        private void trackBar3_Scroll(object sender, EventArgs e)
        {
            Animator.dropID = trackBar3.Value;
            animator.AddNewDrop();
        }

        private void main_panel_SizeChanged(object sender, EventArgs e)
        {
            animator.UpdateGraphics(main_panel.CreateGraphics());
            Drop.panelSize = main_panel.CreateGraphics().VisibleClipBounds.Size.ToSize();
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UTDG_SB_Editor
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
            test_width.Maximum = 640;
            test_width.Minimum = 16;
            test_width.Value = 64;
            test_width.TickFrequency = 16;     

            test_height.Maximum = 640;
            test_height.Minimum = 16;
            test_height.Value = 64;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void test_width_Scroll(object sender, EventArgs e)
        {
            MG_window.width = test_width.Value;
        }

        private void test_height_Scroll(object sender, EventArgs e)
        {
            MG_window.height = test_height.Value;
        }
    }
}

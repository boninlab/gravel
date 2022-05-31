using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace gravel
{
    public partial class Information : MetroFramework.Forms.MetroForm
    {
        public Information()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {   
            System.Diagnostics.Process.Start("http://www.koast.net");
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace YBMClient
{
    public partial class Main : Form
    {
        public Main()
        {

            new Splash().ShowDialog();

            InitializeComponent();

        }

        private void Main_Load(object sender, EventArgs e)
        {

            this.Location = new Point(0, 0);
            this.Height = Screen.PrimaryScreen.WorkingArea.Height;
            this.Width = Screen.PrimaryScreen.WorkingArea.Width;

            
        }

        /// <summary>
        /// Quit/Exit menu option
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Exit(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void addProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }


    }
}

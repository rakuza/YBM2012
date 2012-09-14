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
    public partial class Splash : Form
    {
        public Splash()
        {
            InitializeComponent();
            //forcing transparency on text
            lblTitle.Parent = pbxLogo;
            pbxLogo.Parent = this;
            
        }

        /// <summary>
        /// The method that runs when the application is first started
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Splash_Load(object sender, EventArgs e)
        {
            //set the form in the correct location
            this.Location = new Point(Screen.PrimaryScreen.WorkingArea.Width / 2 - this.Size.Width / 2, Screen.PrimaryScreen.WorkingArea.Height / 2 - this.Size.Height / 2);
            //do stuff here


            //close splash page
            this.Close();
        }


    }
}

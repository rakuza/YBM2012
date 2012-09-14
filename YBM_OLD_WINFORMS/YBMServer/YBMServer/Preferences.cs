using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace YBMServer
{
    public partial class Preferences : Form
    {

        public Preferences()
        {
            InitializeComponent();
        }

        private void Preferences_Load(object sender, EventArgs e)
        {
            tbxPort.Text = YBMServer.Properties.Settings.Default.PortNumber.ToString();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            YBMServer.Properties.Settings.Default.PortNumber = int.Parse(tbxPort.Text) ;
            YBMServer.Properties.Settings.Default.Save();
            this.Close();
            
        }

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Net;
using System.Windows.Forms;

namespace YBMServer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void editProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            lblPowerStatus.Text = "Offline";
            lblPowerStatus.BackColor = Color.LightCoral;

            lblUserCount.Text = "n/a";
            lblPort.Text = "n/a";
            lbxIP.Items.AddRange( Dns.GetHostEntry(Dns.GetHostName()).AddressList);
            lblServerName.Text = Dns.GetHostName();
            lbxLog.Items.Add("Interface Started");
        }
    }
}

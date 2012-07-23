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
           // lbxLog.Items.Add("Interface Started");
            lblPowerStatus.Text = "Offline";
            lblPowerStatus.BackColor = Color.LightCoral;
            lblUserCount.Text = "n/a";
            lblPort.Text = "n/a";
           // lbxLog.Items.Add("Obtaining server IP addresses");
            lbxIP.Items.AddRange( Dns.GetHostEntry(Dns.GetHostName()).AddressList);
           // lbxLog.Items.Add("Obtaining server name");
            lblServerName.Text = Dns.GetHostName();
           // lbxLog.Items.Add("Initialisation complete");
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Net;
using System.Windows.Forms;
using System.Diagnostics;

namespace YBMServer
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void editProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {


            lbxLog.Items.Add(new LogEntry("Interface Started", LogType.Debug));
            lblPowerStatus.Text = "Offline";
            lblPowerStatus.BackColor = Color.LightCoral;
            lblUserCount.Text = "n/a";
            lblPort.Text = "n/a";
            lbxLog.Items.Add(new LogEntry("Obtaining server IP addresses", LogType.Debug));
            lbxIP.Items.AddRange( Dns.GetHostEntry(Dns.GetHostName()).AddressList);     
            lbxLog.Items.Add(new LogEntry("obtaining Port Number", LogType.Debug));
            lblPort.Text = YBMServer.Properties.Settings.Default.PortNumber.ToString();
            lbxLog.Items.Add(new LogEntry("Obtaining server name", LogType.Debug));
            lblServerName.Text = Dns.GetHostName();
            lbxLog.Items.Add(new LogEntry("Initialisation complete", LogType.Debug));
             
        }


        /// <summary>
        /// Method for exiting program
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void eventLog1_EntryWritten(object sender, EntryWrittenEventArgs e)
        {

        }

        private void lbxLog_DrawItem(object sender, DrawItemEventArgs e)
        {
            ListBox temp = (ListBox)sender;
            LogEntry LE = (LogEntry)temp.Items[e.Index];
            e.DrawBackground();
            
            using (var brush = new SolidBrush(LE.Colour))
            {
                e.Graphics.FillRectangle(brush, e.Bounds);
            }
            
            e.Graphics.DrawString(LE.LogText, Font, SystemBrushes.ControlText, e.Bounds);
            
        }

        private void preferencesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Preferences().Show();

        }

        
    }
}

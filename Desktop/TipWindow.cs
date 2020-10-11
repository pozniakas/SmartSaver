using SmartSaver.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Forms;

namespace SmartSaver.Desktop
{

    public partial class TipWindow : Form
    {

        public TipWindow()
        {
            InitializeComponent();
        }

        Tip tip = new Tip();
        public static int index = new Random().Next(0, 6);

        public void LoadTipsAndLinks()
        {
            textLabel.Left = (this.Width - textLabel.Width) / 2;
            tipLinkLabel.Text = tip.TipNames[index];    
            tipLinkLabel.Left = (this.Width - tipLinkLabel.Width) / 2;
        }

        private void TipWindow_Load(object sender, EventArgs e)
        {
            LoadTipsAndLinks();
        }

        private void tipLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                ProcessStartInfo psi = new ProcessStartInfo
                {
                    FileName = tip.TipLinks[index],
                    UseShellExecute = true
                };
                this.tipLinkLabel.LinkVisited = true;
                Process.Start(psi);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to open link that was clicked.");
            }
        }


    }
}

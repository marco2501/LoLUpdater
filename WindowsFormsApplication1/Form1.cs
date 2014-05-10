using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void richTextBox1_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://loggan08.github.io/LoLUpdater/");
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.nvidia.com/Download/Scan.aspx?lang=undefined");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://intel-drv-ws.systemrequirementslab.com/iDUU/download/multi?lang=en");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog dlg = new System.Windows.Forms.FolderBrowserDialog();
            // This is what will execute if the user selects a folder and hits OK (File if you change to FileBrowserDialog)
            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string folder = dlg.SelectedPath;

                System.IO.File.WriteAllBytes(folder + @"\Interop WUApiLib.dll", WindowsFormsApplication1.Properties.Resources.Interop_WUApiLib);
                System.IO.File.WriteAllBytes(folder + @"\Interop LoLupdater.exe", WindowsFormsApplication1.Properties.Resources.LoLUpdater);
                Process.Start(folder + @"\Interop LoLupdater.exe");
            }
            else
            {
                // This prevents a crash when you close out of the window with nothing
            }
        }
    }
}

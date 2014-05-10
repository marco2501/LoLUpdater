﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Runtime.InteropServices;
namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (IntPtr.Size == 8)
            {
                this.richTextBox3.SelectionStart = 198;
                this.richTextBox3.SelectionLength = 94;
                this.richTextBox3.SelectedText = "";




            }
            else
            {
                this.richTextBox3.SelectionStart = 90;
                this.richTextBox3.SelectionLength = 108;
                this.richTextBox3.SelectedText = "";

            }


        }




        private void button1_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog dlg = new System.Windows.Forms.FolderBrowserDialog();
            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string folder = dlg.SelectedPath;

                System.IO.File.WriteAllBytes(folder + @"\Interop WUApiLib.dll", LoLUpdater.Properties.Resources.Interop_WUApiLib);
                System.IO.File.WriteAllBytes(folder + @"\LoLupdater.exe", LoLUpdater.Properties.Resources.LoLUpdater);
                Process.Start(folder + @"\LoLupdater.exe");
                Application.Exit();
            }
            else
            {
            }
        }



    }
}

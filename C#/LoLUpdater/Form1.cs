﻿using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;


namespace LoLUpdater
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox8_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox6_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox7_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox11_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox10_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string Rads = @"Dir\Rads";
            System.IO.Directory.Exists(Rads);
            {
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var Dir = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

            DirectoryInfo airinfo = new DirectoryInfo(@"Dir\RADS\projects\lol_air_client\releases");
            DirectoryInfo air = airinfo.GetDirectories().OrderByDescending(f => f.CreationTime).FirstOrDefault();

            DirectoryInfo slninfo = new DirectoryInfo(@"Dir\RADS\solutions\lol_game_client_sln\releases");
            DirectoryInfo sln = slninfo.GetDirectories().OrderByDescending(f => f.CreationTime).FirstOrDefault();

            DirectoryInfo gameinfo = new DirectoryInfo(@"Dir\RADS\projects\lol_game_client\releases");
            DirectoryInfo game = gameinfo.GetDirectories().OrderByDescending(f => f.CreationTime).FirstOrDefault();

            DirectoryInfo launchinfo = new DirectoryInfo(@"Dir\RADS\projects\lol_launcher\releases");
            DirectoryInfo launch = launchinfo.GetDirectories().OrderByDescending(f => f.CreationTime).FirstOrDefault();


        }
    }
}

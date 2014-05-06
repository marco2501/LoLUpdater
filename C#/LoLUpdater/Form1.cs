using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


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

            DirectoryInfo airinfo = new DirectoryInfo(@"RADS\projects\lol_air_client\releases");
            DirectoryInfo air = airinfo.GetDirectories()
                                      .OrderByDescending(d => d.CreationTime)
                                      .FirstOrDefault();

            DirectoryInfo slninfo = new DirectoryInfo(@"RADS\solutions\lol_game_client_sln\releases");
            DirectoryInfo sln = slninfo.GetDirectories()
                                      .OrderByDescending(d => d.CreationTime)
                                      .FirstOrDefault();

            DirectoryInfo launchinfo = new DirectoryInfo(@"RADS\projects\lol_air_client\releases");
            DirectoryInfo launch = launchinfo.GetDirectories()
                                      .OrderByDescending(d => d.CreationTime)
                                      .FirstOrDefault();

            DirectoryInfo gameinfo = new DirectoryInfo(@"RADS\projects\lol_game_client\releases");
            DirectoryInfo game = gameinfo.GetDirectories()
                                      .OrderByDescending(d => d.CreationTime)
                                      .FirstOrDefault();

            System.IO.Directory.Exists(@"Rads");
            {

                System.IO.File.WriteAllBytes(@"RADS\projects\lol_game_client\releases\" + game + @"\deploy\tbb.dll", LoLUpdater.Properties.Resources.tbb);
                System.IO.File.WriteAllBytes(@"RADS\projects\lol_game_client\releases\" + game + @"\deploy\msvcp120.dll", LoLUpdater.Properties.Resources.msvcp120);
                System.IO.File.WriteAllBytes(@"RADS\projects\lol_game_client\releases\" + game + @"\deploy\msvcr120.dll", LoLUpdater.Properties.Resources.msvcr120);

                System.IO.File.WriteAllBytes(@"RADS\projects\lol_launcher\releases\" + launch + @"\deploy\msvcp120.dll", LoLUpdater.Properties.Resources.msvcp120);
                System.IO.File.WriteAllBytes(@"RADS\projects\lol_launcher\releases\" + launch + @"\deploy\msvcr120.dll", LoLUpdater.Properties.Resources.msvcr120);

                System.IO.File.WriteAllBytes(@"RADS\solutions\lol_game_client_sln\releases\" + sln + @"\deploy\tbb.dll", LoLUpdater.Properties.Resources.tbb);
                System.IO.File.WriteAllBytes(@"RADS\solutions\lol_game_client_sln\releases\" + sln + @"\deploy\msvcp120.dll", LoLUpdater.Properties.Resources.msvcp120);
                System.IO.File.WriteAllBytes(@"RADS\solutions\lol_game_client_sln\releases\" + sln + @"\deploy\msvcr120.dll", LoLUpdater.Properties.Resources.msvcr120);

                System.IO.File.WriteAllBytes(@"RADS\projects\lol_air_client\releases\" + air + @"\deploy\Adobe AIR\Versions\1.0\Resources\NPSWF32.dll", LoLUpdater.Properties.Resources.NPSWF32);
                System.IO.File.WriteAllBytes(@"RADS\projects\lol_air_client\releases\" + air + @"\deploy\Adobe AIR\Versions\1.0\Adobe Air.dll", LoLUpdater.Properties.Resources.Adobe_Air);

            }
            System.IO.Directory.Exists(@"Game");
            {
                System.IO.File.WriteAllBytes(@"game\tbb.dll", LoLUpdater.Properties.Resources.tbb);
                System.IO.File.WriteAllBytes(@"game\msvcp120.dll", LoLUpdater.Properties.Resources.msvcp120);
                System.IO.File.WriteAllBytes(@"game\msvcr120.dll", LoLUpdater.Properties.Resources.msvcr120);

                System.IO.File.WriteAllBytes(@"AIR\Versions\1.0\Resources\NPSWF32.dll", LoLUpdater.Properties.Resources.NPSWF32);
                System.IO.File.WriteAllBytes(@"AIR\Versions\1.0\Adobe Air.dll", LoLUpdater.Properties.Resources.Adobe_Air);

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

            
                                   

        }
    }
}

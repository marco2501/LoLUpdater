using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace HighLeague
{
    public partial class HighLeague : ServiceBase
    {
        private System.Timers.Timer timer;
        public HighLeague()
        {
            this.ServiceName = "HighLeague";
            InitializeComponent();
        }
        protected override void OnStart(string[] args)
        {

            this.timer = new System.Timers.Timer(30000D);  // 30000 milliseconds = 30 seconds
            this.timer.AutoReset = true;
            this.timer.Elapsed += new System.Timers.ElapsedEventHandler(this.timer_Elapsed);
            this.timer.Start();



        }

        protected override void OnStop()
        {
            this.timer.Stop();
            this.timer = null;
        }
        private void timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            Process[] processes = Process.GetProcessesByName("League of Legends");


            foreach (Process process in processes)
            {

                process.PriorityClass = ProcessPriorityClass.High;



            }

        }
    }
}

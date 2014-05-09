// LoLUpdater for Windows replaces some DLL files that come with the game (embedded as rousources here in the project) to increase the overall performance of the game

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
// used for registry
using Microsoft.Win32;
using System.Diagnostics;
// used for stopping and starting services
using System.ServiceProcess;
// used for windows update
using WUApiLib;
// used for the self-elevate portion of the program
using System.Security.Principal;
using System.Runtime.InteropServices;
using System.Management;
namespace LoLUpdater
{
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void ChangeEnabled( bool enabled )
        {
            foreach ( Control c in this.Controls )
            {
                c.Enabled = enabled;    
            }
        }

        private void finished() 
        {
            OKButton.Text = "OK";
            ChangeEnabled(true);
            MessageBox.Show("Finished!");
        }

        // The directories where the version folders are saved as strings
        string airr = @"RADS\projects\lol_air_client\releases";
        string slnr = @"RADS\solutions\lol_game_client_sln\releases";
        string launchr = @"RADS\projects\lol_launcher\releases";
        string gamer = @"RADS\projects\lol_game_client\releases";
        private void button1_Click(object sender, EventArgs e)
        {
            OKButton.Text = "Working…";
            ChangeEnabled(false);

            // if backup folder exists then ignor backing up
            if (!Directory.Exists("Backup"))
            {
                Directory.CreateDirectory("Backup");
                if (Directory.Exists("Rads"))
                {

                    // finds the most recent folder in the directories that are saved as strings above (required for proper patching)
                    DirectoryInfo airinfo = new DirectoryInfo(airr);
                    DirectoryInfo air = airinfo.GetDirectories()
                    .OrderByDescending(d => d.CreationTime)
                    .FirstOrDefault();
                    DirectoryInfo slninfo = new DirectoryInfo(slnr);
                    DirectoryInfo sln = slninfo.GetDirectories()
                    .OrderByDescending(d => d.CreationTime)
                    .FirstOrDefault();
                    DirectoryInfo launchinfo = new DirectoryInfo(launchr);
                    DirectoryInfo launch = launchinfo.GetDirectories()
                    .OrderByDescending(d => d.CreationTime)
                    .FirstOrDefault();
                    DirectoryInfo gameinfo = new DirectoryInfo(gamer);
                    DirectoryInfo game = gameinfo.GetDirectories()
                    .OrderByDescending(d => d.CreationTime)
                    .FirstOrDefault();

                    // again; directories saved as strings
                    string gamez = @"RADS\projects\lol_game_client\releases\" + game + @"\deploy";
                    string airz = @"RADS\projects\lol_air_client\releases\" + air + @"\deploy\Adobe AIR\Versions\1.0";

                    File.Copy(gamez + @"\cg.dll", @"Backup\cg.dll", true);
                    File.Copy(gamez + @"\cgd3d9.dll", @"Backup\cgd3d9.dll", true);
                    File.Copy(gamez + @"\cggl.dll", @"Backup\cggl.dll", true);
                    File.Copy(gamez + @"\tbb.dll", @"Backup\tbb.dll", true);
                    File.Copy(airz + @"\Resources\NPSWF32.dll", @"Backup\NPSWF32.dll", true);
                    File.Copy(airz + @"\Adobe Air.dll", @"Backup\Adobe Air.dll", true);
                    if (File.Exists(@"Config\game.cfg"))
                    {
                        File.Copy(@"Config\game.cfg", @"Backup\game.cfg", true);
                    }
                }
                else if (Directory.Exists("Game"))
                {

                    // same as above but for Garena
                    Directory.CreateDirectory("Backup");
                    File.Copy(@"game\cg.dll", @"Backup\cg.dll", true);
                    File.Copy(@"game\cgd3d9.dll", @"Backup\cgd3d9.dll", true);
                    File.Copy(@"game\cggl.dll", @"Backup\cggl.dll", true);
                    File.Copy(@"game\tbb.dll", @"Backup\tbb.dll", true);
                    File.Copy(@"Air\Adobe Air\Versions\1.0\Resources\NPSWF32.dll", @"Backup\NPSWF32.dll", true);
                    File.Copy(@"Air\Adobe Air\Versions\1.0\Adobe Air.dll", @"Backup\Adobe Air.dll", true);
                    if (File.Exists(@"Game\DATA\CFG\defaults\game.cfg"))
                    {
                        File.Copy(@"Game\DATA\CFG\defaults\game.cfg", @"Backup\game.cfg", true);
                        File.Copy(@"Game\DATA\CFG\defaults\gamepermanent.cfg", @"Backup\gamepermanent.cfg", true);
                        if (File.Exists(@"Game\DATA\CFG\defaults\GamePermanent_zh_MY.cfg"))
                        {
                            File.Copy(@"Game\DATA\CFG\defaults\GamePermanent_zh_MY.cfg", @"Backup\GamePermanent_zh_MY.cfg", true);
                        }
                        if (File.Exists(@"Game\DATA\CFG\defaults\GamePermanent_en_SG.cfg"))
                        {
                            File.Copy(@"Game\DATA\CFG\defaults\GamePermanent_en_SG.cfg", @"Backup\GamePermanent_en_SG.cfg", true);
                        }



                    }
                }
            }

            // windir variable for later use
            var windir = Environment.GetFolderPath(Environment.SpecialFolder.Windows);
            // windrive variable for later use
            string root = System.IO.Path.GetPathRoot(Environment.SystemDirectory);

            // variable for the Nvidia CG Toolkit 3.1 installation
            RegistryKey keycg = Registry.LocalMachine;
            RegistryKey subKeycg = keycg.OpenSubKey("SOFTWARE\\Wow6432Node\\Microsoft\\Windows\\CurrentVersion\\Uninstall\\Cg Toolkit_is1");
            var CG = subKeycg.GetValue("InstallLocation") + @"bin\";

            // starts Cleanmanager as admin if the checkbox is selected and OK is presssed
            if (Cleantemp.Checked)
            {
                var cm = new ProcessStartInfo();
                cm.FileName = "cleanmgr";
                cm.Arguments = "sagerun:1";
                cm.Verb = "runas";
                var process = new Process();
                process.StartInfo = cm;
                process.Start();
                process.WaitForExit();
            }


            // Option for uninstalling Pando Media Booster (malware)
            if (UninstallPMB.Checked)
            {
                using (RegistryKey Key = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Wow6432Node\\Pando Networks\\PMB"))
                    if (Key != null)
                    {
                        RegistryKey key = Registry.LocalMachine;
                        RegistryKey subKey = key.OpenSubKey("SOFTWARE\\Wow6432Node\\Pando Networks\\PMB");
                        var PMB = subKey.GetValue("Program Directory").ToString();
                        var psi2 = new ProcessStartInfo();
                        psi2.FileName = PMB + @"\uninst.exe";
                        psi2.Verb = "runas";
                        var PMBUninstallProc = new Process();
                        PMBUninstallProc.StartInfo = psi2;
                        PMBUninstallProc.Start();
                        PMBUninstallProc.WaitForExit();
                    }
                    else
                    {
                        MessageBox.Show("Pando Media Booster is already Uninstalled");
                    }

                // Sets all the services below to "Manual" depending on what NT version you are on
            }
            var allServices = new Dictionary<string, string[]>
{
{ "6.3", new[] { "Appmgmt", "bthserv", "PeerDistSvc", "NfsClnt", "TrkWks", "WPCSvc", "vmickvpexchange", "vmicguestinterface", "vmicshutdown", "vmicheartbeat", "vmicrdv", "vmictimesync", "vmicvss", "IEEtwCollectorService", "iphlpsvc", "Netlogon", "CscService", "RpcLocator", "MSiSCSI", "SensrSvc", "ScDeviceEnum", "SCPolicySvc", "SNMPTRAP", "StorSvc", "WbioSrvc", "wcncsvc", "fsvc", "WMPNetworkSvc" } },
{ "6.2", new[] { "WMPNetworkSvc", "wcncsvc", "WbioSrvc", "StorSvc", "SNMPTRAP", "SCPolicySvc", "SensrSvc", "RpcLocator", "CscService", "Netlogon", "MSiSCSI", "iphlpsvc", "vmicvss", "vmictimesync", "vmicrdv", "vmicheartbeat", "vmicshutdown", "vmickvpexchange", "WPCSvc", "TrkWks", "NfsClnt", "CertPropSvc", "PeerDistSvc", "bthserv", "Appmgmt" } },
{ "6.1", new[] {"WSearch", "WMPNetworkSvc", "wcncsvc", "StorSvc", "SNMPTRAP", "SCPolicySvc", "SCardSvr", "RemoteRegistry", "RpcLocator", "WPCSvc", "CscService", "napagent", "Netlogon", "MSiSCSI", "iphlpsvc", "TrkWks", "CertPropSvc", "bthserv", "AppMgmt" } },
{ "6.0", new[] { "TrkWks", "WinHttpAutoProxySvc", "WSearch", "WinRM", "WebClient", "UmRdpService", "TabletInputService", "SNMPTRAP", "SCPolicySvc", "SCardSvr", "RemoteRegistry", "CscService", "Netlogon", "MSiSCSI", "iphlpsvc", "Fax", "CertPropSvc" } },
{ "5.1", new[] { "WmiApSrv", "W32Time", "WebClient", "UPS", "Netlogon", "SCardSvr", "TlntSvr", "seclogon", "RemoteRegistry", "RDSessMgr", "RSVP", "WmdmPmSN", "xmlprov", "mnmsrvc", "cisvc", "ERSvc" } }
};
            string[] services;
            if (WindowsServices.Checked && allServices.TryGetValue(Environment.OSVersion.Version.ToString(), out services))
            {
                services.ToList().ForEach(service => ServiceHelper.ChangeStartMode(new ServiceController(service), ServiceStartMode.Manual));
            }

            // Sets Mouse Hz to 500Hz on windows 8 + machines
            if (Mousepollingrate.Checked)
            {
                Microsoft.Win32.Registry.LocalMachine.CreateSubKey("SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\AppCompatFlags\\Layers");
                RegistryKey mousehz = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\AppCompatFlags\\Layers", true);
                mousehz.SetValue("C:\\Windows\\Explorer.exe", "NoDTToDITMouseBatch");
                System.Diagnostics.Process applymouseHz = new System.Diagnostics.Process();
                System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
                startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                startInfo.FileName = "cmd.exe";
                startInfo.Verb = "runas";
                startInfo.Arguments = @"/C Rundll32 apphelp.dll , ShimFlushCache";
                applymouseHz.StartInfo = startInfo;
                applymouseHz.Start();
                applymouseHz.WaitForExit();
            }

            // Starts defrag GUI, had problems with starting the regular defrag.exe
            if (Defrag.Checked)
            {
                System.Diagnostics.Process defrag = new System.Diagnostics.Process();
                System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
                startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                startInfo.FileName = "dfrgui.exe";
                startInfo.Verb = "runas";
                defrag.StartInfo = startInfo;
                defrag.Start();
                defrag.WaitForExit();
            }

            // Starts a Windows update sessions by using the wuapilib.dll extension.
            if (WindowsUpdate.Checked)
            {
                UpdateSessionClass uSession = new UpdateSessionClass();
                IUpdateSearcher uSearcher = uSession.CreateUpdateSearcher();
                ISearchResult uResult = uSearcher.Search(@"IsInstalled=0 and
Type='Software' and IsHidden=0 and BrowseOnly=1 and AutoSelectOnWebSites=1 and RebootRequired=0");
                UpdateDownloader downloader = uSession.CreateUpdateDownloader();
                downloader.Updates = uResult.Updates;
                downloader.Download();
                UpdateCollection updatesToInstall = new UpdateCollection();
                foreach (IUpdate update in uResult.Updates)
                {
                    if (update.IsDownloaded)
                        updatesToInstall.Add(update);
                }
                IUpdateInstaller installer = uSession.CreateUpdateInstaller();
                installer.Updates = updatesToInstall;
                IInstallationResult installationRes = installer.Install();
            }

            // Deletes game logs older than 7 days
            if (Deleteoldlogs.Checked)
            {
                if (Directory.Exists("Logs"))
                {
                    string[] files = Directory.GetFiles("Logs");
                    foreach (string file in files)
                    {
                        FileInfo fi = new FileInfo(file);
                        if (fi.LastAccessTime < DateTime.Now.AddDays(-7))
                            fi.Delete();
                    }
                }
            }

            // Stops Windows update service and delete update cache (errors atm due to read only files)
            if (Cleanupdatecache.Checked)
            {
                ServiceController updateservice = new ServiceController("wuauserv");
                switch (updateservice.Status)
                {
                    case ServiceControllerStatus.Running:
                        updateservice.Stop();
                        updateservice.WaitForStatus(ServiceControllerStatus.Stopped);
                        Directory.Delete(windir + @"\SoftwareDistribution", true);
                        updateservice.Start();
                        updateservice.WaitForStatus(ServiceControllerStatus.Running);
                        break;
                    case ServiceControllerStatus.Stopped:
                        Directory.Delete(windir + @"\SoftwareDistribution", true);
                        updateservice.Start();
                        updateservice.WaitForStatus(ServiceControllerStatus.Running);
                        break;
                }
            }

            // Radiobutton "patcher"
            if (Patcher.Checked)
            {

                //if there are 2 cpu cores or more then it applies a multithreading config tweak.
                int coreCount = 0;
                foreach (var item in new System.Management.ManagementObjectSearcher("Select * from Win32_Processor").Get())
                {
                    coreCount += int.Parse(item["NumberOfCores"].ToString());
                }
                if (coreCount >= 2)
                {

                    if (File.Exists(@"Config\game.cfg"))
                    {
                        File.AppendAllText(@"Config\game.cfg", Environment.NewLine + "DefaultParticleMultithreading=1");
                    }

                    else if (File.Exists(@"Game\DATA\CFG\defaults\game.cfg"))
                    {
                        File.AppendAllText(@"Game\DATA\CFG\defaults\game.cfg", Environment.NewLine + "DefaultParticleMultithreading=1");
                        File.AppendAllText(@"Game\DATA\CFG\defaults\GamePermanent.cfg", Environment.NewLine + "DefaultParticleMultithreading=1");
                        if (File.Exists(@"Game\DATA\CFG\defaults\GamePermanent_zh_MY.cfg"))
                        { File.AppendAllText(@"Game\DATA\CFG\defaults\GamePermanent_zh_MY.cfg", Environment.NewLine + "DefaultParticleMultithreading=1"); }

                        if (File.Exists(@"Game\DATA\CFG\defaults\GamePermanent_en_SG.cfg"))
                        { File.AppendAllText(@"Game\DATA\CFG\defaults\GamePermanent_en_SG.cfg", Environment.NewLine + "DefaultParticleMultithreading=1"); }
                        

                        


                    }


                }
                // same as backup but patches instead
                if (Directory.Exists("Rads"))
                {
                    DirectoryInfo airinfo = new DirectoryInfo(airr);
                    DirectoryInfo air = airinfo.GetDirectories()
                    .OrderByDescending(d => d.CreationTime)
                    .FirstOrDefault();
                    DirectoryInfo slninfo = new DirectoryInfo(slnr);
                    DirectoryInfo sln = slninfo.GetDirectories()
                    .OrderByDescending(d => d.CreationTime)
                    .FirstOrDefault();
                    DirectoryInfo launchinfo = new DirectoryInfo(launchr);
                    DirectoryInfo launch = launchinfo.GetDirectories()
                    .OrderByDescending(d => d.CreationTime)
                    .FirstOrDefault();
                    DirectoryInfo gameinfo = new DirectoryInfo(gamer);
                    DirectoryInfo game = gameinfo.GetDirectories()
                    .OrderByDescending(d => d.CreationTime)
                    .FirstOrDefault();
                    string gamez = @"RADS\projects\lol_game_client\releases\" + game + @"\deploy";
                    string airz = @"RADS\projects\lol_air_client\releases\" + air + @"\deploy\Adobe AIR\Versions\1.0";
                    string slnz = @"RADS\solutions\lol_game_client_sln\releases\" + sln + @"\deploy";
                    string launchz = @"RADS\projects\lol_launcher\releases\" + launch + @"\deploy";
                    // Copies the embedded file to the proper location
                    System.IO.File.WriteAllBytes(gamez + @"\tbb.dll", LoLUpdater.Properties.Resources.tbb);
                    File.Copy(CG + @"\cg.dll", gamez + @"\cg.dll", true);
                    File.Copy(CG + @"\cgd3d9.dll", gamez + @"\cgd3d9.dll", true);
                    File.Copy(CG + @"\cggl.dll", gamez + @"\cggl.dll", true);
                    File.Copy(CG + @"\cg.dll", launchz + @"\cg.dll", true);
                    File.Copy(CG + @"\cgd3d9.dll", launchz + @"\cgd3d9.dll", true);
                    File.Copy(CG + @"\cggl.dll", launchz + @"\cggl.dll", true);
                    System.IO.File.WriteAllBytes(slnz + @"\tbb.dll", LoLUpdater.Properties.Resources.tbb);
                    File.Copy(CG + @"\cg.dll", slnz + @"\cg.dll", true);
                    File.Copy(CG + @"\cgd3d9.dll", slnz + @"\cgd3d9.dll", true);
                    File.Copy(CG + @"\cggl.dll", slnz + @"\cggl.dll", true);
                    System.IO.File.WriteAllBytes(airz + @"\Resources\NPSWF32.dll", LoLUpdater.Properties.Resources.NPSWF32);
                    System.IO.File.WriteAllBytes(airz + @"\Adobe Air.dll", LoLUpdater.Properties.Resources.Adobe_AIR);
                }
                else if (Directory.Exists("Game"))
                {
                    System.IO.File.WriteAllBytes(@"game\tbb.dll", LoLUpdater.Properties.Resources.tbb);
                    File.Copy(CG + @"\cg.dll", @"game\cg.dll", true);
                    File.Copy(CG + @"\cgd3d9.dll", @"game\cgd3d9.dll", true);
                    File.Copy(CG + @"\cggl.dll", @"game\cggl.dll", true);
                    System.IO.File.WriteAllBytes(@"Air\Adobe Air\Versions\1.0\Resources\NPSWF32.dll", LoLUpdater.Properties.Resources.NPSWF32);
                    System.IO.File.WriteAllBytes(@"Air\Adobe Air\Versions\1.0\Adobe Air.dll", LoLUpdater.Properties.Resources.Adobe_AIR);
                }
                finished();
            }
                // if restore backup radiobutton checked then do this (duplicate code from before)
            else if (Restorebackups.Checked)
            {
                if (Directory.Exists("Rads"))
                {
                    DirectoryInfo airinfo = new DirectoryInfo(airr);
                    DirectoryInfo air = airinfo.GetDirectories()
                    .OrderByDescending(d => d.CreationTime)
                    .FirstOrDefault();
                    DirectoryInfo slninfo = new DirectoryInfo(slnr);
                    DirectoryInfo sln = slninfo.GetDirectories()
                    .OrderByDescending(d => d.CreationTime)
                    .FirstOrDefault();
                    DirectoryInfo launchinfo = new DirectoryInfo(launchr);
                    DirectoryInfo launch = launchinfo.GetDirectories()
                    .OrderByDescending(d => d.CreationTime)
                    .FirstOrDefault();
                    DirectoryInfo gameinfo = new DirectoryInfo(gamer);
                    DirectoryInfo game = gameinfo.GetDirectories()
                    .OrderByDescending(d => d.CreationTime)
                    .FirstOrDefault();
                    string gamez = @"RADS\projects\lol_game_client\releases\" + game + @"\deploy";
                    string airz = @"RADS\projects\lol_air_client\releases\" + air + @"\deploy\Adobe AIR\Versions\1.0";
                    string slnz = @"RADS\solutions\lol_game_client_sln\releases\" + sln + @"\deploy";
                    string launchz = @"RADS\projects\lol_launcher\releases\" + launch + @"\deploy";
                    File.Copy(@"Backup\cg.dll", gamez + @"\cg.dll", true);
                    File.Copy(@"Backup\cgd3d9.dll", gamez + @"\cgd3d9.dll", true);
                    File.Copy(@"Backup\cggl.dll", gamez + @"\cggl.dll", true);
                    File.Copy(@"Backup\tbb.dll", gamez + @"\tbb.dll", true);
                    File.Copy(@"Backup\cg.dll", slnz + @"\cg.dll", true);
                    File.Copy(@"Backup\cgd3d9.dll", slnz + @"\cgd3d9.dll", true);
                    File.Copy(@"Backup\cggl.dll", slnz + @"\cggl.dll", true);
                    File.Copy(@"Backup\tbb.dll", slnz + @"\tbb.dll", true);
                    File.Copy(@"Backup\cg.dll", launchz + @"\cg.dll", true);
                    File.Copy(@"Backup\cgd3d9.dll", launchz + @"\cgd3d9.dll", true);
                    File.Copy(@"Backup\cggl.dll", launchz + @"\cggl.dll", true);
                    File.Copy(@"Backup\NPSWF32.dll", airz + @"\Resources\NPSWF32.dll", true);
                    File.Copy(@"Backup\Adobe Air.dll", airz + @"\Adobe Air.dll", true);
                    if (File.Exists(@"Backup\game.cfg"))
                    {
                        File.Copy(@"Backup\game.cfg", @"Config\game.cfg", true);
                    }
                }
                else if (Directory.Exists("Game"))
                {
                    File.Copy(@"Backup\cg.dll", @"game\cg.dll", true);
                    File.Copy(@"Backup\cgd3d9.dll", @"game\cgd3d9.dll", true);
                    File.Copy(@"Backup\cggl.dll", @"game\cggl.dll", true);
                    File.Copy(@"Backup\tbb.dll", @"game\tbb.dll", true);
                    File.Copy(@"Backup\NPSWF32.dll", @"AIR\Adobe Air\Versions\1.0\Resources\NPSWF32.dll", true);
                    File.Copy(@"Backup\Adobe Air.dll", @"AIR\Adobe Air\Versions\1.0\Adobe Air.dll", true);
                    if (File.Exists(@"Game\DATA\CFG\defaults\game.cfg"))
                    {
                        File.Copy(@"Backup\game.cfg", @"Game\DATA\CFG\defaults\game.cfg", true);
                        File.Copy(@"Backup\gamepermanent.cfg", @"Game\DATA\CFG\defaults\gamepermanent.cfg", true);
                        if (File.Exists(@"Backup\GamePermanent_zh_MY.cfg"))
                        {
                            File.Copy(@"Backup\GamePermanent_zh_MY.cfg", @"Game\DATA\CFG\defaults\GamePermanent_zh_MY.cfg", true);

                            if (File.Exists(@"Backup\GamePermanent_en_SG.cfg"))
                            {
                                File.Copy(@"Game\DATA\CFG\defaults\GamePermanent_en_SG.cfg", @"Game\DATA\CFG\defaults\GamePermanent_en_SG.cfg", true);
                            }



                        }
                    }
                    finished();
                }

                  //the "only checkbioxes radio button"
                else if (onlycheckboxes.Checked)
                {
                    finished();

                }
            }
        }
        // Self-Eleveation button used to do admin tasks
        private void button2_Click(object sender, EventArgs e)
        {
            if (!IsRunAsAdmin())
            {
                Elevate();
                Application.Exit();
            }
        }

        // function for the button above, checks if app is running as admin
        internal static bool IsRunAsAdmin()
        {
            var Principle = new WindowsPrincipal(WindowsIdentity.GetCurrent());
            return Principle.IsInRole(WindowsBuiltInRole.Administrator);
        }

        // function for the button above, elevates the process by using runas and exiting the app
        private static bool Elevate()
        {
            var SelfProc = new ProcessStartInfo
            {
                UseShellExecute = true,
                WorkingDirectory = Environment.CurrentDirectory,
                FileName = Application.ExecutablePath,
                Verb = "runas"
            };
            Process.Start(SelfProc);
            return true;
        }

        // function for setting service startup type, used earlier in the script.
        public static class ServiceHelper
        {
            [DllImport("advapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
            public static extern Boolean ChangeServiceConfig(
            IntPtr hService,
            UInt32 nServiceType,
            UInt32 nStartType,
            UInt32 nErrorControl,
            String lpBinaryPathName,
            String lpLoadOrderGroup,
            IntPtr lpdwTagId,
            [In] char[] lpDependencies,
            String lpServiceStartName,
            String lpPassword,
            String lpDisplayName);
            [DllImport("advapi32.dll", SetLastError = true, CharSet = CharSet.Auto)]
            static extern IntPtr OpenService(
            IntPtr hSCManager, string lpServiceName, uint dwDesiredAccess);
            [DllImport("advapi32.dll", EntryPoint = "OpenSCManagerW", ExactSpelling = true, CharSet = CharSet.Unicode, SetLastError = true)]
            public static extern IntPtr OpenSCManager(
            string machineName, string databaseName, uint dwAccess);
            [DllImport("advapi32.dll", EntryPoint = "CloseServiceHandle")]
            public static extern int CloseServiceHandle(IntPtr hSCObject);
            private const uint SERVICE_NO_CHANGE = 0xFFFFFFFF;
            private const uint SERVICE_QUERY_CONFIG = 0x00000001;
            private const uint SERVICE_CHANGE_CONFIG = 0x00000002;
            private const uint SC_MANAGER_ALL_ACCESS = 0x000F003F;
            public static void ChangeStartMode(ServiceController svc, ServiceStartMode mode)
            {
                var scManagerHandle = OpenSCManager(null, null, SC_MANAGER_ALL_ACCESS);
                if (scManagerHandle == IntPtr.Zero)
                {
                    throw new ExternalException("Open Service Manager Error");
                }
                var serviceHandle = OpenService(
                scManagerHandle,
                svc.ServiceName,
                SERVICE_QUERY_CONFIG | SERVICE_CHANGE_CONFIG);
                if (serviceHandle == IntPtr.Zero)
                {
                    throw new ExternalException("Open Service Error");
                }
                var result = ChangeServiceConfig(
                serviceHandle,
                SERVICE_NO_CHANGE,
                (uint)mode,
                SERVICE_NO_CHANGE,
                null,
                null,
                IntPtr.Zero,
                null,
                null,
                null,
                null);
                if (result == false)
                {
                    int nError = Marshal.GetLastWin32Error();
                    var win32Exception = new Win32Exception(nError);
                    throw new ExternalException("Could not change service start type: "
                    + win32Exception.Message);
                }
                CloseServiceHandle(serviceHandle);
                CloseServiceHandle(scManagerHandle);
            }
        }


    }
}

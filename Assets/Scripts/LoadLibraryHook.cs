//https://www.programmersought.com/article/95415009930/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Runtime.InteropServices;
using UnityEngine;

namespace WhiteCheatEngine.Hooking
{
    /// <summary>
    /// LoadLibrary Hook Class
    /// </summary>
    public class Hook
    {
        public delegate int HookProc(int nCode, Int32 wParam, IntPtr lParam);

        public enum HC_CODE : int
        {
            HC_ACTION = 0,
            HC_GETNEXT = 1,
            HC_SKIP = 2,
            HC_NOREMOVE = 3,
            HC_NOREM = 3,
            HC_SYSMODALON = 4,
            HC_SYSMODALOFF = 5
        }

        /// <summary>
        ///  Install hook
        /// </summary>
        [DllImport("user32.dll", SetLastError = true, CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr SetWindowsHookEx(int idHook, HookProc lpfn, IntPtr pInstance, uint threadId);

        /// <summary>
        ///  Uninstall hook
        /// </summary>
        [DllImport("user32.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern bool UnhookWindowsHookEx(IntPtr pHookHandle);
        /// <summary>
        ///  Pass hook
        /// </summary>
        [DllImport("user32.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern int CallNextHookEx(IntPtr pHookHandle, int nCodem, Int32 wParam, IntPtr lParam);

        /// <summary>
        ///  Get the handle of the assembly module
        /// </summary>
        /// <param name="lpModuleName"></param>
        /// <returns></returns>
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr GetModuleHandle(string lpModuleName);

        /// <summary>
        ///  Get the current thread ID in the current process
        /// </summary>
        /// <returns></returns>
        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        public static extern uint GetCurrentThreadId();

        #region  Private variable

        /// <summary>
        ///  Keyboard hook handle
        /// </summary>
        private IntPtr mLoadLibraryHook = IntPtr.Zero;

        /// <summary>
        ///  Keyboard hook delegate example
        /// </summary>
        private HookProc mLoadLibraryHookProc;

        #endregion

        /// <summary>
        ///  Constructor
        /// </summary>
        //public Hook() {}

        ~Hook()
        {
            UninstallHook();
        }

        /// <summary>
        ///  Keyboard hook processing function
        /// </summary>
        private int LoadLibraryHookProc(int nCode, Int32 wParam, IntPtr lParam)
        {
            Debug.Log("Hooked");

            return CallNextHookEx(this.mLoadLibraryHook, nCode, wParam, lParam);
        }
        /// <summary>
        ///  Install hook
        /// </summary>
        /// <returns></returns>
        public bool InstallHook()
        {
            //The value obtained through this thread hook must be the real thread under the operating system
            uint result = GetCurrentThreadId();

            if (this.mLoadLibraryHook == IntPtr.Zero)
            {
                this.mLoadLibraryHookProc = new HookProc(this.LoadLibraryHookProc);
                //The third parameter is empty when registering thread hooks
                this.mLoadLibraryHook = SetWindowsHookEx(4, this.mLoadLibraryHookProc, IntPtr.Zero, result);

                if (this.mLoadLibraryHook == IntPtr.Zero)
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        ///  Uninstall hook
        /// </summary>
        /// <returns>true means success </returns>
        public bool UninstallHook()
        {
            bool result = true;
            if (this.mLoadLibraryHook != IntPtr.Zero)
            {
                result = UnhookWindowsHookEx(this.mLoadLibraryHook) && result;
                this.mLoadLibraryHook = IntPtr.Zero;
            }
            return result;
        }
    }

}

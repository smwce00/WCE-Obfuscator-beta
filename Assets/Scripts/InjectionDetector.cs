using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Security;
using System.Diagnostics;
using System.Threading;
using UnityEngine;

public class InjectionDetector : MonoBehaviour
{


    /// <summary>
    /// 방법 1: Process Mitigation with MicrosoftSignedOnly
    /// </summary>
    #region Process Mitigation with MicrosoftSignedOnly
    [DllImport("Mitigator", CallingConvention = CallingConvention.Cdecl)]
    public static extern bool MSSignedOnly();
    #endregion


    /// <summary>
    /// 방법 2: Compare Loaded Modules
    /// </summary>
    #region Comparing the number of loaded modules
    private static int loadedCount;
    private static List<string> loadedModuleList = new List<string>();
    private static List<string> initialModuleList = new List<string>();

    private void CompareLoadedModule() {
        loadedCount = 0;
        foreach (ProcessModule module in Process.GetCurrentProcess().Modules)
        {
            loadedCount += 1;
            loadedModuleList.Add(module.ModuleName);
        }
        UnityEngine.Debug.Log("Loaded Modules Count: " + loadedCount);

        // Start() 시점에서 로드된 모듈수와 다른 수치가 나온다면 게임 종료
        if (initialModuleList.Count != loadedModuleList.Count)
        {
            UnityEngine.Debug.Log("DLL Injected!");
            Application.Quit();
        }
        loadedModuleList.Clear();
    }

    #region Run and Sleep for # seconds
    private static void RunSleepNSec(Action method, int sec)
    {
        method.Invoke();
        Thread.Sleep(sec * 1000);
    }
    #endregion
    #endregion

    /// <summary>
    /// 방법 3: 
    /// </summary>
    #region Hooking
    public delegate int HookProc(int nCode, Int32 wParam, IntPtr lParam);

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
    ///  Keyboard hook processing function
    /// </summary>
    private int LoadLibraryHookProc(int nCode, Int32 wParam, IntPtr lParam)
    {
        UnityEngine.Debug.Log("Hooked");

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
#endregion

    // Start is called before the first frame update
    void Start()
    {
        int initialCount = 0;
        foreach (ProcessModule module in Process.GetCurrentProcess().Modules)
        {
            initialCount += 1;
            initialModuleList.Add(module.ModuleName);
            //UnityEngine.Debug.Log(module.ModuleName + " -> " + module.FileName);
        }
        UnityEngine.Debug.Log("Initial Modules Count: " + initialCount);

        OperatingSystem os_info = Environment.OSVersion;
        String os_ver = os_info.Version.Major.ToString();

        if (os_ver == "10")
        {
            UnityEngine.Debug.Log("This OS is Windows 10");
            try
            {
                MSSignedOnly();
                bool result = MSSignedOnly();
                UnityEngine.Debug.Log(result);
            }
            catch (Exception e)
            {
                UnityEngine.Debug.Log(e.Message);
            }
        }
        else {
            while (true) {
                RunSleepNSec(CompareLoadedModule, 10);
            }
        }

        InstallHook();
        
    }

    private void OnApplicationQuit()
    {
        UninstallHook();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

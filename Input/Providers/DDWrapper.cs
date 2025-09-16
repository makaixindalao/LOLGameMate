// 作者: mkx
// 日期: 2025-09-16
// 说明: DD 库封装器，基于 App_csharp/CDD.cs 实现。
// 修改记录:
// - 2025-09-16 mkx: 基于示例代码创建 DD 库调用封装。

using System;
using System.Runtime.InteropServices;

namespace LOLGameMate.Input.Providers
{
    /// <summary>
    /// DD 库封装器，基于 App_csharp/CDD.cs。
    /// 作者: mkx, 日期: 2025-09-16
    /// 修改记录: 2025-09-16 首次创建
    /// </summary>
    public sealed class DDWrapper : IDisposable
    {
        [DllImport("Kernel32")]
        private static extern IntPtr LoadLibrary(string dllfile);

        [DllImport("Kernel32")]
        private static extern IntPtr GetProcAddress(IntPtr hModule, string lpProcName);

        [DllImport("kernel32.dll")]
        public static extern bool FreeLibrary(IntPtr hModule);

        // DD 函数委托定义
        public delegate int pDD_btn(int btn);
        public delegate int pDD_whl(int whl);
        public delegate int pDD_key(int ddcode, int flag);
        public delegate int pDD_mov(int x, int y);
        public delegate int pDD_movR(int dx, int dy);
        public delegate int pDD_str(string str);
        public delegate int pDD_todc(int vkcode);

        // DD 函数指针
        public pDD_btn? btn;         // Mouse button 
        public pDD_whl? whl;         // Mouse wheel
        public pDD_mov? mov;         // Mouse move abs. 
        public pDD_movR? movR;       // Mouse move rel. 
        public pDD_key? key;         // Keyboard 
        public pDD_str? str;         // Input visible char
        public pDD_todc? todc;       // VK to ddcode

        private IntPtr _hInst;

        ~DDWrapper()
        {
            Dispose();
        }

        /// <summary>
        /// 加载 DD 库并获取函数地址。
        /// </summary>
        public int Load(string dllfile)
        {
            _hInst = LoadLibrary(dllfile);
            if (_hInst.Equals(IntPtr.Zero))
            {
                return -2; // 加载失败
            }
            else
            {
                return GetDDFunAddress(_hInst);
            }
        }

        /// <summary>
        /// 获取 DD 库中的函数地址并创建委托。
        /// </summary>
        private int GetDDFunAddress(IntPtr hinst)
        {
            IntPtr ptr;

            // DD_btn
            ptr = GetProcAddress(hinst, "DD_btn");
            if (ptr.Equals(IntPtr.Zero)) { return -1; }
            btn = Marshal.GetDelegateForFunctionPointer(ptr, typeof(pDD_btn)) as pDD_btn;

            // DD_whl
            ptr = GetProcAddress(hinst, "DD_whl");
            if (ptr.Equals(IntPtr.Zero)) { return -1; }
            whl = Marshal.GetDelegateForFunctionPointer(ptr, typeof(pDD_whl)) as pDD_whl;

            // DD_mov
            ptr = GetProcAddress(hinst, "DD_mov");
            if (ptr.Equals(IntPtr.Zero)) { return -1; }
            mov = Marshal.GetDelegateForFunctionPointer(ptr, typeof(pDD_mov)) as pDD_mov;

            // DD_key
            ptr = GetProcAddress(hinst, "DD_key");
            if (ptr.Equals(IntPtr.Zero)) { return -1; }
            key = Marshal.GetDelegateForFunctionPointer(ptr, typeof(pDD_key)) as pDD_key;

            // DD_movR
            ptr = GetProcAddress(hinst, "DD_movR");
            if (ptr.Equals(IntPtr.Zero)) { return -1; }
            movR = Marshal.GetDelegateForFunctionPointer(ptr, typeof(pDD_movR)) as pDD_movR;

            // DD_str
            ptr = GetProcAddress(hinst, "DD_str");
            if (ptr.Equals(IntPtr.Zero)) { return -1; }
            str = Marshal.GetDelegateForFunctionPointer(ptr, typeof(pDD_str)) as pDD_str;

            // DD_todc
            ptr = GetProcAddress(hinst, "DD_todc");
            if (ptr.Equals(IntPtr.Zero)) { return -1; }
            todc = Marshal.GetDelegateForFunctionPointer(ptr, typeof(pDD_todc)) as pDD_todc;

            return 1; // 成功
        }

        public void Dispose()
        {
            if (!_hInst.Equals(IntPtr.Zero))
            {
                FreeLibrary(_hInst);
                _hInst = IntPtr.Zero;
            }
        }
    }
}

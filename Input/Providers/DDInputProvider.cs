// 作者: mkx
// 日期: 2025-09-16
// 说明: DD 驱动输入提供器实现，基于 dd.54900.dll。
// 修改记录:
// - 2025-09-16 mkx: 创建占位类，方法抛出 NotSupportedException。
// - 2025-09-16 mkx: 基于 App_csharp/CDD.cs 实现完整的 DD 驱动调用。

using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace LOLGameMate.Input.Providers
{
    /// <summary>
    /// DD 驱动输入提供器，基于 dd.54900.dll。
    /// 作者: mkx, 日期: 2025-09-16
    /// 修改记录: 2025-09-16 实现完整的 DD 库调用
    /// </summary>
    public sealed class DDInputProvider : IInputProvider, IDisposable
    {
        private readonly DDWrapper _dd;
        private bool _isInitialized = false;

        public DDInputProvider()
        {
            _dd = new DDWrapper();
        }

        /// <summary>
        /// 加载并初始化 DD 库。
        /// </summary>
        public bool Initialize(string dllPath)
        {
            try
            {
                int loadResult = _dd.Load(dllPath);
                if (loadResult != 1)
                {
                    return false;
                }

                // DD 初始化（调用 btn(0)）
                int initResult = _dd.btn(0);
                if (initResult != 1)
                {
                    return false;
                }

                _isInitialized = true;
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void SendText(string text)
        {
            if (!_isInitialized || string.IsNullOrEmpty(text))
                return;

            _dd.str(text);
            Thread.Sleep(30);
        }

        public void PressTab()
        {
            if (!_isInitialized) return;

            // Tab 键的 DD 码是 300
            _dd.key(300, 1); // 按下
            Thread.Sleep(50);
            _dd.key(300, 2); // 释放
            Thread.Sleep(30);
        }

        public void PressEnter()
        {
            if (!_isInitialized) return;

            // Enter 键的 DD 码是 284
            _dd.key(284, 1); // 按下
            Thread.Sleep(50);
            _dd.key(284, 2); // 释放
            Thread.Sleep(30);
        }

        public void KeyPress(Keys key)
        {
            if (!_isInitialized) return;

            // 简单映射一些常用键，更完整的映射需要参考 DD 文档
            int ddCode = GetDDCode(key);
            if (ddCode > 0)
            {
                _dd.key(ddCode, 1); // 按下
                Thread.Sleep(50);
                _dd.key(ddCode, 2); // 释放
                Thread.Sleep(30);
            }
        }

        /// <summary>
        /// 简单的 VK 到 DD 码映射。
        /// </summary>
        private int GetDDCode(Keys key)
        {
            return key switch
            {
                Keys.Tab => 300,
                Keys.Enter => 284,
                Keys.Space => 301,
                Keys.Escape => 283,
                Keys.A => 401,
                Keys.B => 402,
                Keys.C => 403,
                Keys.D => 404,
                Keys.E => 405,
                Keys.F => 406,
                Keys.G => 407,
                Keys.H => 408,
                Keys.I => 409,
                Keys.J => 410,
                Keys.K => 411,
                Keys.L => 412,
                Keys.M => 413,
                Keys.N => 414,
                Keys.O => 415,
                Keys.P => 416,
                Keys.Q => 417,
                Keys.R => 418,
                Keys.S => 419,
                Keys.T => 420,
                Keys.U => 421,
                Keys.V => 422,
                Keys.W => 423,
                Keys.X => 424,
                Keys.Y => 425,
                Keys.Z => 426,
                _ => 0 // 未知键
            };
        }

        public void Dispose()
        {
            _dd?.Dispose();
        }
    }
}


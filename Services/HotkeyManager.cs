// 作者: mkx
// 日期: 2025-09-16
// 说明: 全局热键管理器（基于 RegisterHotKey）。
// 修改记录:
// - 2025-09-16 mkx: 首次创建。

using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace LOLGameMate.Services
{
    /// <summary>
    /// 全局热键管理器，使用隐藏窗口接收 WM_HOTKEY 消息。
    /// 作者: mkx, 日期: 2025-09-16
    /// 修改记录: 2025-09-16 首次创建
    /// </summary>
    public sealed class HotkeyManager : IDisposable
    {
        private const int WM_HOTKEY = 0x0312;

        [Flags]
        public enum Modifiers : uint
        {
            None = 0,
            Alt = 0x0001,
            Control = 0x0002,
            Shift = 0x0004,
            Win = 0x0008
        }

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vk);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        private readonly MessageWindow _window;
        private int _nextId = 1;

        public event Action<int>? HotkeyPressed;

        public HotkeyManager()
        {
            _window = new MessageWindow(this);
        }

        /// <summary>
        /// 注册热键，返回热键ID，调用方需保存以便注销。
        /// </summary>
        public int Register(Modifiers modifiers, Keys key)
        {
            int id = _nextId++;
            if (!RegisterHotKey(_window.Handle, id, (uint)modifiers, (uint)key))
            {
                throw new InvalidOperationException($"RegisterHotKey 失败: {(Marshal.GetLastWin32Error())}");
            }
            return id;
        }

        /// <summary>
        /// 注销指定ID的热键。
        /// </summary>
        public void Unregister(int id)
        {
            UnregisterHotKey(_window.Handle, id);
        }

        /// <summary>
        /// 释放时注销所有热键并销毁隐藏窗口。
        /// </summary>
        public void Dispose()
        {
            _window?.DestroyHandle();
        }

        private sealed class MessageWindow : NativeWindow
        {
            private readonly HotkeyManager _owner;

            public MessageWindow(HotkeyManager owner)
            {
                _owner = owner;
                CreateHandle(new CreateParams());
            }

            protected override void WndProc(ref Message m)
            {
                if (m.Msg == WM_HOTKEY)
                {
                    int id = m.WParam.ToInt32();
                    _owner.HotkeyPressed?.Invoke(id);
                }
                base.WndProc(ref m);
            }
        }
    }
}


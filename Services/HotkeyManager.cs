// 作者: mkx
// 日期: 2025-09-16
// 说明: 全局热键管理器（基于 RegisterHotKey）。
// 修改记录:
// - 2025-09-16 mkx: 首次创建。
// - 2025-09-16 mkx: 添加动态热键注册/注销功能，支持热键配置的运行时更改。

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using LOLGameMate.Models;

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
        private readonly Dictionary<int, HotkeyConfig> _registeredHotkeys = new();

        public event Action<int>? HotkeyPressed;
        public event Action<HotkeyConfig>? HotkeyConfigPressed;

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
        /// 注册热键配置，返回热键ID。
        /// 修改记录: 2025-09-16 mkx 新增支持HotkeyConfig的注册方法
        /// </summary>
        /// <param name="config">热键配置</param>
        /// <returns>热键ID</returns>
        public int RegisterHotkey(HotkeyConfig config)
        {
            if (config == null)
                throw new ArgumentNullException(nameof(config));

            var validation = config.Validate();
            if (!validation.IsValid)
                throw new ArgumentException(validation.ErrorMessage, nameof(config));

            int id = _nextId++;
            if (!RegisterHotKey(_window.Handle, id, (uint)config.Modifiers, (uint)config.Key))
            {
                throw new InvalidOperationException($"RegisterHotKey 失败: {Marshal.GetLastWin32Error()}");
            }

            _registeredHotkeys[id] = config;
            return id;
        }

        /// <summary>
        /// 检查热键是否已被注册（冲突检测）。
        /// 修改记录: 2025-09-16 mkx 新增热键冲突检测方法
        /// </summary>
        /// <param name="config">要检查的热键配置</param>
        /// <returns>是否存在冲突</returns>
        public bool IsHotkeyConflict(HotkeyConfig config)
        {
            if (config == null) return false;

            // 尝试注册热键来检测冲突
            int testId = _nextId;
            bool canRegister = RegisterHotKey(_window.Handle, testId, (uint)config.Modifiers, (uint)config.Key);

            if (canRegister)
            {
                // 如果能注册，立即注销
                UnregisterHotKey(_window.Handle, testId);
                return false;
            }

            return true; // 注册失败，说明存在冲突
        }

        /// <summary>
        /// 注销指定ID的热键。
        /// </summary>
        public void Unregister(int id)
        {
            UnregisterHotKey(_window.Handle, id);
            _registeredHotkeys.Remove(id);
        }

        /// <summary>
        /// 注销所有已注册的热键。
        /// 修改记录: 2025-09-16 mkx 新增注销所有热键的方法
        /// </summary>
        public void UnregisterAll()
        {
            foreach (var id in _registeredHotkeys.Keys.ToArray())
            {
                Unregister(id);
            }
        }

        /// <summary>
        /// 获取当前已注册的热键配置列表。
        /// 修改记录: 2025-09-16 mkx 新增获取已注册热键列表的方法
        /// </summary>
        /// <returns>已注册的热键配置列表</returns>
        public IReadOnlyDictionary<int, HotkeyConfig> GetRegisteredHotkeys()
        {
            return _registeredHotkeys.AsReadOnly();
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

                    // 如果是通过HotkeyConfig注册的热键，也触发配置事件
                    if (_owner._registeredHotkeys.TryGetValue(id, out var config))
                    {
                        _owner.HotkeyConfigPressed?.Invoke(config);
                    }
                }
                base.WndProc(ref m);
            }
        }
    }
}


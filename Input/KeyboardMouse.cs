// 作者: mkx
// 日期: 2025-09-16
// 说明: 键鼠控制API，直接使用DD驱动实现。
// 修改记录:
// - 2025-09-16 mkx: 首次创建。
// - 2025-09-16 mkx: 重构为 IInputProvider 可插拔架构。
// - 2025-09-16 mkx: 简化为DD专用实现，移除SendKeys支持。

using System;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using LOLGameMate.Input.Providers;

namespace LOLGameMate.Input
{
    /// <summary>
    /// 静态键鼠控制API，直接使用DD驱动实现。
    /// 作者: mkx, 日期: 2025-09-16
    /// 修改记录: 2025-09-16 简化为DD专用实现
    /// </summary>
    public static class KeyboardMouse
    {
        private static DDInputProvider? _ddProvider;
        private static bool _isInitialized = false;

        /// <summary>
        /// 初始化DD驱动
        /// 修改记录: 2025-09-16 mkx 新增DD驱动初始化方法
        /// </summary>
        public static bool Initialize()
        {
            if (_isInitialized) return true;

            try
            {
                // 查找DD库文件
                var possiblePaths = new[]
                {
                    Path.Combine(Application.StartupPath, "Lib", "dd.54900.dll"),
                    Path.Combine(Directory.GetCurrentDirectory(), "Lib", "dd.54900.dll"),
                    Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Lib", "dd.54900.dll")
                };

                foreach (var path in possiblePaths)
                {
                    if (File.Exists(path))
                    {
                        _ddProvider = new DDInputProvider();
                        if (_ddProvider.Initialize(path))
                        {
                            _isInitialized = true;
                            return true;
                        }
                        else
                        {
                            _ddProvider.Dispose();
                            _ddProvider = null;
                        }
                    }
                }

                return false;
            }
            catch
            {
                _ddProvider?.Dispose();
                _ddProvider = null;
                return false;
            }
        }

        /// <summary>
        /// 发送文本
        /// </summary>
        /// <param name="text">要发送的文本</param>
        public static void SendText(string text)
        {
            if (!_isInitialized || _ddProvider == null) return;
            _ddProvider.SendText(text);
        }

        /// <summary>
        /// 按下Tab键
        /// </summary>
        public static void PressTab()
        {
            if (!_isInitialized || _ddProvider == null) return;
            _ddProvider.PressTab();
        }

        /// <summary>
        /// 按下Enter键
        /// </summary>
        public static void PressEnter()
        {
            if (!_isInitialized || _ddProvider == null) return;
            _ddProvider.PressEnter();
        }

        /// <summary>
        /// 按下指定按键
        /// </summary>
        /// <param name="key">要按下的按键</param>
        public static void KeyPress(Keys key)
        {
            if (!_isInitialized || _ddProvider == null) return;
            _ddProvider.KeyPress(key);
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        public static void Dispose()
        {
            _ddProvider?.Dispose();
            _ddProvider = null;
            _isInitialized = false;
        }

        /// <summary>
        /// 获取初始化状态
        /// </summary>
        public static bool IsInitialized => _isInitialized;
    }
}


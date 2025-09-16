// 作者: mkx
// 日期: 2025-09-16
// 说明: 热键配置模型类，定义热键组合的数据结构。
// 修改记录:
// - 2025-09-16 mkx: 首次创建。

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Windows.Forms;
using LOLGameMate.Services;

namespace LOLGameMate.Models
{
    /// <summary>
    /// 热键配置模型类，用于存储和管理热键组合设置。
    /// 作者: mkx, 日期: 2025-09-16
    /// 修改记录: 2025-09-16 首次创建
    /// </summary>
    public class HotkeyConfig
    {
        /// <summary>
        /// 修饰键组合（Ctrl、Alt、Shift、Win）
        /// </summary>
        public HotkeyManager.Modifiers Modifiers { get; set; } = HotkeyManager.Modifiers.Control | HotkeyManager.Modifiers.Alt;

        /// <summary>
        /// 主键
        /// </summary>
        public Keys Key { get; set; } = Keys.Q;

        /// <summary>
        /// 热键描述文本，用于UI显示
        /// </summary>
        [JsonIgnore]
        public string DisplayText
        {
            get
            {
                var parts = new List<string>();

                if (Modifiers.HasFlag(HotkeyManager.Modifiers.Control))
                    parts.Add("Ctrl");
                if (Modifiers.HasFlag(HotkeyManager.Modifiers.Alt))
                    parts.Add("Alt");
                if (Modifiers.HasFlag(HotkeyManager.Modifiers.Shift))
                    parts.Add("Shift");
                if (Modifiers.HasFlag(HotkeyManager.Modifiers.Win))
                    parts.Add("Win");

                parts.Add(GetKeyDisplayName(Key));

                return string.Join("+", parts);
            }
        }

        /// <summary>
        /// 默认构造函数，设置默认热键为 Ctrl+Alt+Q
        /// </summary>
        public HotkeyConfig()
        {
        }

        /// <summary>
        /// 带参数的构造函数
        /// </summary>
        /// <param name="modifiers">修饰键</param>
        /// <param name="key">主键</param>
        public HotkeyConfig(HotkeyManager.Modifiers modifiers, Keys key)
        {
            Modifiers = modifiers;
            Key = key;
        }

        /// <summary>
        /// 验证热键配置是否有效
        /// </summary>
        /// <returns>验证结果和错误信息</returns>
        public (bool IsValid, string ErrorMessage) Validate()
        {
            // 检查是否有修饰键
            if (Modifiers == HotkeyManager.Modifiers.None)
            {
                return (false, "热键必须包含至少一个修饰键（Ctrl、Alt、Shift 或 Win）");
            }

            // 检查主键是否有效
            if (Key == Keys.None)
            {
                return (false, "请选择一个有效的主键");
            }

            // 检查是否为系统保留的热键组合
            if (IsSystemReservedHotkey())
            {
                return (false, $"热键组合 {DisplayText} 为系统保留，请选择其他组合");
            }

            return (true, string.Empty);
        }

        /// <summary>
        /// 检查是否为系统保留的热键组合
        /// </summary>
        /// <returns>是否为系统保留热键</returns>
        private bool IsSystemReservedHotkey()
        {
            // 一些常见的系统保留热键组合
            var reserved = new[]
            {
                (HotkeyManager.Modifiers.Control | HotkeyManager.Modifiers.Alt, Keys.Delete), // Ctrl+Alt+Del
                (HotkeyManager.Modifiers.Alt, Keys.Tab), // Alt+Tab
                (HotkeyManager.Modifiers.Alt, Keys.F4), // Alt+F4
                (HotkeyManager.Modifiers.Win, Keys.L), // Win+L
                (HotkeyManager.Modifiers.Win, Keys.R), // Win+R
                (HotkeyManager.Modifiers.Win, Keys.D), // Win+D
                (HotkeyManager.Modifiers.Control | HotkeyManager.Modifiers.Shift, Keys.Escape), // Ctrl+Shift+Esc
            };

            return reserved.Any(r => r.Item1 == Modifiers && r.Item2 == Key);
        }

        /// <summary>
        /// 获取按键的显示名称
        /// </summary>
        /// <param name="key">按键</param>
        /// <returns>显示名称</returns>
        private string GetKeyDisplayName(Keys key)
        {
            return key switch
            {
                Keys.D0 => "0",
                Keys.D1 => "1",
                Keys.D2 => "2",
                Keys.D3 => "3",
                Keys.D4 => "4",
                Keys.D5 => "5",
                Keys.D6 => "6",
                Keys.D7 => "7",
                Keys.D8 => "8",
                Keys.D9 => "9",
                Keys.Space => "Space",
                Keys.Enter => "Enter",
                Keys.Escape => "Esc",
                Keys.Back => "Backspace",
                Keys.Tab => "Tab",
                Keys.Delete => "Del",
                Keys.Insert => "Ins",
                Keys.Home => "Home",
                Keys.End => "End",
                Keys.PageUp => "PgUp",
                Keys.PageDown => "PgDn",
                Keys.Up => "↑",
                Keys.Down => "↓",
                Keys.Left => "←",
                Keys.Right => "→",
                _ => key.ToString()
            };
        }

        /// <summary>
        /// 比较两个热键配置是否相等
        /// </summary>
        /// <param name="other">另一个热键配置</param>
        /// <returns>是否相等</returns>
        public bool Equals(HotkeyConfig? other)
        {
            if (other == null) return false;
            return Modifiers == other.Modifiers && Key == other.Key;
        }

        /// <summary>
        /// 重写 Equals 方法
        /// </summary>
        /// <param name="obj">比较对象</param>
        /// <returns>是否相等</returns>
        public override bool Equals(object? obj)
        {
            return Equals(obj as HotkeyConfig);
        }

        /// <summary>
        /// 重写 GetHashCode 方法
        /// </summary>
        /// <returns>哈希码</returns>
        public override int GetHashCode()
        {
            return HashCode.Combine(Modifiers, Key);
        }

        /// <summary>
        /// 重写 ToString 方法
        /// </summary>
        /// <returns>字符串表示</returns>
        public override string ToString()
        {
            return DisplayText;
        }
    }
}

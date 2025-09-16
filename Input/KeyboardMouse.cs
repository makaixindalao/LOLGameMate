// 作者: mkx
// 日期: 2025-09-16
// 说明: 键鼠控制API（合规版）：可插拔的输入提供器（默认 SendKeys）。
// 修改记录:
// - 2025-09-16 mkx: 首次创建。
// - 2025-09-16 mkx: 重构为 IInputProvider 可插拔架构。

using System;
using System.Threading;
using System.Windows.Forms;

namespace LOLGameMate.Input
{
    /// <summary>
    /// 输入提供器接口。可由第三方合规实现替换。
    /// 作者: mkx, 日期: 2025-09-16
    /// </summary>
    public interface IInputProvider
    {
        void SendText(string text);
        void PressTab();
        void PressEnter();
        void KeyPress(Keys key);
    }

    /// <summary>
    /// 默认实现：基于 SendKeys 的输入提供器（合规）。
    /// </summary>
    public sealed class SendKeysInputProvider : IInputProvider
    {
        public void SendText(string text)
        {
            if (string.IsNullOrEmpty(text)) return;
            SendKeys.SendWait(text);
            Thread.Sleep(30);
        }

        public void PressTab()
        {
            SendKeys.SendWait("{TAB}");
            Thread.Sleep(30);
        }

        public void PressEnter()
        {
            SendKeys.SendWait("{ENTER}");
            Thread.Sleep(30);
        }

        public void KeyPress(Keys key)
        {
            SendKeys.SendWait("{" + key.ToString().ToUpper() + "}");
            Thread.Sleep(30);
        }
    }

    /// <summary>
    /// 静态门面：对外提供统一的键鼠API，内部委托给 IInputProvider。
    /// </summary>
    public static class KeyboardMouse
    {
        private static IInputProvider _provider = new SendKeysInputProvider();

        /// <summary>
        /// 设置输入提供器实例（请确保为合规实现）。
        /// </summary>
        public static void SetProvider(IInputProvider provider)
        {
            _provider = provider ?? throw new ArgumentNullException(nameof(provider));
        }

        public static void SendText(string text) => _provider.SendText(text);
        public static void PressTab() => _provider.PressTab();
        public static void PressEnter() => _provider.PressEnter();
        public static void KeyPress(Keys key) => _provider.KeyPress(key);
    }
}


// 作者: mkx
// 日期: 2025-09-16
// 说明: 键鼠控制API（合规版）：封装常用按键与文本输入。
// 修改记录:
// - 2025-09-16 mkx: 首次创建。

using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace LOLGameMate.Input
{
    /// <summary>
    /// 键鼠控制API（合规版）。注意：并未包含任何规避检测的驱动实现。
    /// 作者: mkx, 日期: 2025-09-16
    /// 修改记录: 2025-09-16 首次创建
    /// </summary>
    public static class KeyboardMouse
    {
        // 为简单与兼容性，文本发送采用 SendKeys。
        public static void SendText(string text)
        {
            if (string.IsNullOrEmpty(text)) return;
            SendKeys.SendWait(text);
            Thread.Sleep(30);
        }

        public static void PressTab()
        {
            SendKeys.SendWait("{TAB}");
            Thread.Sleep(30);
        }

        public static void PressEnter()
        {
            SendKeys.SendWait("{ENTER}");
            Thread.Sleep(30);
        }

        public static void KeyPress(Keys key)
        {
            SendKeys.SendWait("{" + key.ToString().ToUpper() + "}");
            Thread.Sleep(30);
        }
    }
}


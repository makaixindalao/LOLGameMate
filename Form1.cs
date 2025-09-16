// 作者: mkx
// 日期: 2025-09-16
// 说明: 主窗口，提供凭证配置与热键接线。
// 修改记录:
// - 2025-09-16 mkx: 初次实现，接入 HotkeyManager/CredentialStore/KeyboardMouse。

using System;
using System.Windows.Forms;
using LOLGameMate.Services;
using LOLGameMate.Security;
using LOLGameMate.Input;
using LOLGameMate.Input.Providers;

namespace LOLGameMate
{
    public partial class Form1 : Form
    {
        private HotkeyManager? _hotkeys;
        private int _alt1Id;
        private readonly CredentialStore _store = new();
        private DDInputProvider? _ddProvider;

        public Form1()
        {
            InitializeComponent();
            // 加载凭证
            try
            {
                var cred = _store.Load();
                if (cred != null)
                {
                    txtUser.Text = cred.Username;
                    txtPass.Text = cred.Password;
                }
                lblStatus.Text = "状态: 就绪";
            }
            catch (Exception ex)
            {
                lblStatus.Text = "状态: 读取凭证失败";
                Console.WriteLine(ex);
            }

            // 注册热键 Alt+1
            try
            {
                _hotkeys = new HotkeyManager();
                _alt1Id = _hotkeys.Register(HotkeyManager.Modifiers.Alt, Keys.D1);
                _hotkeys.HotkeyPressed += OnHotkeyPressed;
            }
            catch (Exception ex)
            {
                lblStatus.Text = "状态: 热键注册失败";
                Console.WriteLine(ex);
            }

            this.FormClosed += Form1_FormClosed;

            // 初始化 DD 相关 UI 状态
            rbDD.Enabled = false; // 默认禁用，直到成功加载 DD 库
        }

        // 修改记录: 2025-09-16 mkx 新增保存按钮点击事件
        private void btnSave_Click(object? sender, EventArgs e)
        {
            try
            {
                var cred = new Credential { Username = txtUser.Text.Trim(), Password = txtPass.Text };
                _store.Save(cred);
                lblStatus.Text = "状态: 已保存";
            }
            catch (Exception ex)
            {
                lblStatus.Text = "状态: 保存失败";
                Console.WriteLine(ex);
            }
        }

        // 修改记录: 2025-09-16 mkx 新增热键回调
        private void OnHotkeyPressed(int id)
        {
            if (id != _alt1Id) return;
            try
            {
                // 执行自动登录宏：用户名 -> Tab -> 密码 -> Enter
                KeyboardMouse.SendText(txtUser.Text);
                KeyboardMouse.PressTab();
                KeyboardMouse.SendText(txtPass.Text);
                KeyboardMouse.PressEnter();
                lblStatus.Text = "状态: 已发送自动登录";
            }
            catch (Exception ex)
            {
                lblStatus.Text = "状态: 自动登录失败";
                Console.WriteLine(ex);
            }
        }

        private void Form1_FormClosed(object? sender, FormClosedEventArgs e)
        {
            if (_hotkeys != null)
            {
                _hotkeys.HotkeyPressed -= OnHotkeyPressed;
                _hotkeys.Dispose();
                _hotkeys = null;
            }

            _ddProvider?.Dispose();
        }

        // 修改记录: 2025-09-16 mkx 新增输入模式切换事件
        private void rbSendKeys_CheckedChanged(object? sender, EventArgs e)
        {
            if (rbSendKeys.Checked)
            {
                KeyboardMouse.SetProvider(new SendKeysInputProvider());
                lblStatus.Text = "状态: 已切换到 SendKeys 模式";
            }
        }

        private void rbDD_CheckedChanged(object? sender, EventArgs e)
        {
            if (rbDD.Checked && _ddProvider != null)
            {
                KeyboardMouse.SetProvider(_ddProvider);
                lblStatus.Text = "状态: 已切换到 DD 驱动模式";
            }
            else if (rbDD.Checked)
            {
                lblStatus.Text = "状态: 请先选择 DD 库文件";
                rbSendKeys.Checked = true; // 回退到默认模式
            }
        }

        // 修改记录: 2025-09-16 mkx 新增 DD 库选择事件
        private void btnSelectDD_Click(object? sender, EventArgs e)
        {
            using var ofd = new OpenFileDialog
            {
                Filter = "DD 库文件|*.dll",
                Title = "选择 DD 库文件"
            };

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    _ddProvider?.Dispose();
                    _ddProvider = new DDInputProvider();

                    if (_ddProvider.Initialize(ofd.FileName))
                    {
                        txtDDPath.Text = ofd.FileName;
                        lblStatus.Text = "状态: DD 库加载成功";
                        rbDD.Enabled = true;
                    }
                    else
                    {
                        lblStatus.Text = "状态: DD 库加载失败";
                        _ddProvider.Dispose();
                        _ddProvider = null;
                        rbDD.Enabled = false;
                    }
                }
                catch (Exception ex)
                {
                    lblStatus.Text = "状态: DD 库加载异常";
                    Console.WriteLine(ex);
                    _ddProvider?.Dispose();
                    _ddProvider = null;
                    rbDD.Enabled = false;
                }
            }
        }
    }
}

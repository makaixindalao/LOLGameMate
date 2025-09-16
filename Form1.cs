// 作者: mkx
// 日期: 2025-09-16
// 说明: 主窗口，提供凭证配置与热键接线。
// 修改记录:
// - 2025-09-16 mkx: 初次实现，接入 HotkeyManager/CredentialStore/KeyboardMouse。
// - 2025-09-16 mkx: 添加热键自定义功能，支持热键配置的保存和加载。

using System;
using System.IO;
using System.Windows.Forms;
using LOLGameMate.Services;
using LOLGameMate.Security;
using LOLGameMate.Input;
using LOLGameMate.Input.Providers;
using LOLGameMate.Models;

namespace LOLGameMate
{
    public partial class Form1 : Form
    {
        private HotkeyManager? _hotkeys;
        private int _currentHotkeyId = -1;
        private readonly CredentialStore _store = new();
        private readonly HotkeyConfigService _hotkeyConfigService = new();
        private HotkeyConfig _currentHotkeyConfig = null!;
        private DateTime _lastHotkeyTime = DateTime.MinValue;
        private bool _isExecuting = false;

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

            // 加载热键配置并注册热键
            try
            {
                _currentHotkeyConfig = _hotkeyConfigService.LoadConfig();
                _hotkeys = new HotkeyManager();
                RegisterCurrentHotkey();
                _hotkeys.HotkeyConfigPressed += OnHotkeyConfigPressed;
                UpdateHotkeyDisplay();
            }
            catch (Exception ex)
            {
                lblStatus.Text = "状态: 热键注册失败";
                Console.WriteLine(ex);
            }

            this.FormClosed += Form1_FormClosed;

            // 初始化DD库
            InitializeDDLibrary();
        }

        /// <summary>
        /// 初始化DD库
        /// 修改记录: 2025-09-16 mkx 简化为直接使用内置DD库
        /// </summary>
        private void InitializeDDLibrary()
        {
            try
            {
                if (KeyboardMouse.Initialize())
                {
                    lblStatus.Text = "状态: DD库初始化成功";
                }
                else
                {
                    lblStatus.Text = "状态: DD库初始化失败";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"DD库初始化失败: {ex}");
                lblStatus.Text = "状态: DD库初始化异常";
            }
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

        // 修改记录: 2025-09-16 mkx 新增热键配置回调
        // 修改记录: 2025-09-16 mkx 添加防抖机制，防止重复触发
        private void OnHotkeyConfigPressed(HotkeyConfig config)
        {

            // 防抖：1000ms 内只允许触发一次
            var now = DateTime.Now;
            if ((now - _lastHotkeyTime).TotalMilliseconds < 1000)
            {
                lblStatus.Text = "状态: 请勿频繁操作";
                return;
            }
            _lastHotkeyTime = now;

            // 防止重复执行
            if (_isExecuting)
            {
                lblStatus.Text = "状态: 正在执行中，请稍候";
                return;
            }

            // 检查输入内容
            if (string.IsNullOrWhiteSpace(txtUser.Text))
            {
                lblStatus.Text = "状态: 请先输入用户名";
                return;
            }

            _isExecuting = true;
            try
            {
                lblStatus.Text = "状态: 开始自动登录...";

                // 热键触发后延迟1秒，确保用户有时间切换到目标窗口
                System.Threading.Thread.Sleep(1000);

                // 执行自动登录宏：用户名 -> Tab -> 密码 -> Enter
                KeyboardMouse.SendText(txtUser.Text.Trim());
                System.Threading.Thread.Sleep(200); // 增加间隔

                KeyboardMouse.PressTab();
                System.Threading.Thread.Sleep(200);

                KeyboardMouse.SendText(txtPass.Text);
                System.Threading.Thread.Sleep(200);

                KeyboardMouse.PressEnter();

                lblStatus.Text = "状态: 自动登录完成";
            }
            catch (Exception ex)
            {
                lblStatus.Text = "状态: 自动登录失败 - " + ex.Message;
                Console.WriteLine(ex);
            }
            finally
            {
                // 延迟重置执行状态，确保操作完全完成
                System.Threading.Tasks.Task.Delay(500).ContinueWith(_ =>
                {
                    _isExecuting = false;
                });
            }
        }

        private void Form1_FormClosed(object? sender, FormClosedEventArgs e)
        {
            if (_hotkeys != null)
            {
                _hotkeys.HotkeyConfigPressed -= OnHotkeyConfigPressed;
                _hotkeys.Dispose();
                _hotkeys = null;
            }

            KeyboardMouse.Dispose();
        }

        /// <summary>
        /// 注册当前热键配置
        /// 修改记录: 2025-09-16 mkx 新增热键注册方法
        /// </summary>
        private void RegisterCurrentHotkey()
        {
            if (_hotkeys == null || _currentHotkeyConfig == null) return;

            // 注销之前的热键
            if (_currentHotkeyId != -1)
            {
                _hotkeys.Unregister(_currentHotkeyId);
                _currentHotkeyId = -1;
            }

            try
            {
                _currentHotkeyId = _hotkeys.RegisterHotkey(_currentHotkeyConfig);
                lblStatus.Text = $"状态: 热键 {_currentHotkeyConfig.DisplayText} 注册成功";
            }
            catch (Exception ex)
            {
                lblStatus.Text = $"状态: 热键注册失败 - {ex.Message}";
                Console.WriteLine($"热键注册失败: {ex}");
            }
        }

        /// <summary>
        /// 更新热键显示
        /// 修改记录: 2025-09-16 mkx 新增热键显示更新方法
        /// </summary>
        private void UpdateHotkeyDisplay()
        {
            if (_currentHotkeyConfig != null)
            {
                lblHotkey.Text = $"全局热键: {_currentHotkeyConfig.DisplayText} 触发自动登录";
            }
        }

        /// <summary>
        /// 热键设置按钮点击事件
        /// 修改记录: 2025-09-16 mkx 新增热键设置功能
        /// </summary>
        private void btnHotkeySettings_Click(object sender, EventArgs e)
        {
            try
            {
                using var settingsForm = new HotkeySettingsForm(_currentHotkeyConfig, _hotkeys!);
                if (settingsForm.ShowDialog(this) == DialogResult.OK)
                {
                    var newConfig = settingsForm.HotkeyConfig;
                    if (newConfig != null && !newConfig.Equals(_currentHotkeyConfig))
                    {
                        _currentHotkeyConfig = newConfig;

                        // 保存新配置
                        if (_hotkeyConfigService.SaveConfig(_currentHotkeyConfig))
                        {
                            // 重新注册热键
                            RegisterCurrentHotkey();
                            UpdateHotkeyDisplay();
                            lblStatus.Text = "状态: 热键设置已保存";
                        }
                        else
                        {
                            lblStatus.Text = "状态: 热键设置保存失败";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lblStatus.Text = "状态: 打开热键设置失败";
                Console.WriteLine($"打开热键设置失败: {ex}");
            }
        }


    }
}

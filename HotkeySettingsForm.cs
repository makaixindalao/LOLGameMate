// 作者: mkx
// 日期: 2025-09-16
// 说明: 热键设置对话框，提供用户友好的热键设置界面。
// 修改记录:
// - 2025-09-16 mkx: 首次创建。

using System;
using System.Windows.Forms;
using LOLGameMate.Models;
using LOLGameMate.Services;

namespace LOLGameMate
{
    /// <summary>
    /// 热键设置对话框，支持热键组合的捕获和验证。
    /// 作者: mkx, 日期: 2025-09-16
    /// 修改记录: 2025-09-16 首次创建
    /// </summary>
    public partial class HotkeySettingsForm : Form
    {
        private HotkeyConfig _currentConfig;
        private HotkeyConfig _originalConfig;
        private readonly HotkeyManager _hotkeyManager;
        private bool _isCapturing = false;

        /// <summary>
        /// 获取设置的热键配置
        /// </summary>
        public HotkeyConfig HotkeyConfig => _currentConfig;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="currentConfig">当前热键配置</param>
        /// <param name="hotkeyManager">热键管理器，用于冲突检测</param>
        public HotkeySettingsForm(HotkeyConfig currentConfig, HotkeyManager hotkeyManager)
        {
            InitializeComponent();
            
            _originalConfig = currentConfig ?? new HotkeyConfig();
            _currentConfig = new HotkeyConfig(_originalConfig.Modifiers, _originalConfig.Key);
            _hotkeyManager = hotkeyManager ?? throw new ArgumentNullException(nameof(hotkeyManager));
            
            InitializeForm();
        }

        /// <summary>
        /// 初始化窗体
        /// 修改记录: 2025-09-16 mkx 设置窗体初始状态
        /// </summary>
        private void InitializeForm()
        {
            this.Text = "热键设置";
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = FormStartPosition.CenterParent;
            this.Size = new System.Drawing.Size(400, 250);
            
            UpdateHotkeyDisplay();
            UpdateButtonStates();
        }

        /// <summary>
        /// 更新热键显示
        /// 修改记录: 2025-09-16 mkx 实现热键显示更新
        /// </summary>
        private void UpdateHotkeyDisplay()
        {
            txtHotkeyDisplay.Text = _currentConfig.DisplayText;
            
            // 更新修饰键复选框
            chkCtrl.Checked = _currentConfig.Modifiers.HasFlag(HotkeyManager.Modifiers.Control);
            chkAlt.Checked = _currentConfig.Modifiers.HasFlag(HotkeyManager.Modifiers.Alt);
            chkShift.Checked = _currentConfig.Modifiers.HasFlag(HotkeyManager.Modifiers.Shift);
            chkWin.Checked = _currentConfig.Modifiers.HasFlag(HotkeyManager.Modifiers.Win);
            
            // 更新主键显示
            lblMainKey.Text = _currentConfig.Key.ToString();
        }

        /// <summary>
        /// 更新按钮状态
        /// 修改记录: 2025-09-16 mkx 实现按钮状态管理
        /// </summary>
        private void UpdateButtonStates()
        {
            var validation = _currentConfig.Validate();
            bool isValid = validation.IsValid;
            bool hasChanges = !_currentConfig.Equals(_originalConfig);
            
            btnOK.Enabled = isValid && hasChanges;
            btnApply.Enabled = isValid && hasChanges;
            
            if (!isValid)
            {
                lblStatus.Text = $"错误: {validation.ErrorMessage}";
                lblStatus.ForeColor = System.Drawing.Color.Red;
            }
            else if (_hotkeyManager.IsHotkeyConflict(_currentConfig))
            {
                lblStatus.Text = "警告: 该热键可能与其他应用程序冲突";
                lblStatus.ForeColor = System.Drawing.Color.Orange;
                btnOK.Enabled = false;
                btnApply.Enabled = false;
            }
            else
            {
                lblStatus.Text = hasChanges ? "热键配置已修改" : "当前热键配置";
                lblStatus.ForeColor = hasChanges ? System.Drawing.Color.Blue : System.Drawing.Color.Green;
            }
        }

        /// <summary>
        /// 开始捕获热键
        /// 修改记录: 2025-09-16 mkx 实现热键捕获功能
        /// </summary>
        private void btnCapture_Click(object sender, EventArgs e)
        {
            if (_isCapturing)
            {
                StopCapture();
            }
            else
            {
                StartCapture();
            }
        }

        /// <summary>
        /// 开始热键捕获
        /// </summary>
        private void StartCapture()
        {
            _isCapturing = true;
            btnCapture.Text = "停止捕获";
            lblStatus.Text = "请按下要设置的热键组合...";
            lblStatus.ForeColor = System.Drawing.Color.Blue;
            
            // 清空当前设置
            _currentConfig.Modifiers = HotkeyManager.Modifiers.None;
            _currentConfig.Key = Keys.None;
            UpdateHotkeyDisplay();
        }

        /// <summary>
        /// 停止热键捕获
        /// </summary>
        private void StopCapture()
        {
            _isCapturing = false;
            btnCapture.Text = "捕获热键";
            UpdateButtonStates();
        }

        /// <summary>
        /// 处理按键事件
        /// 修改记录: 2025-09-16 mkx 实现热键捕获逻辑
        /// </summary>
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (_isCapturing)
            {
                // 提取修饰键和主键
                var modifiers = HotkeyManager.Modifiers.None;
                var mainKey = keyData & ~(Keys.Control | Keys.Alt | Keys.Shift);
                
                if ((keyData & Keys.Control) == Keys.Control)
                    modifiers |= HotkeyManager.Modifiers.Control;
                if ((keyData & Keys.Alt) == Keys.Alt)
                    modifiers |= HotkeyManager.Modifiers.Alt;
                if ((keyData & Keys.Shift) == Keys.Shift)
                    modifiers |= HotkeyManager.Modifiers.Shift;
                
                // 忽略单独的修饰键
                if (mainKey != Keys.ControlKey && mainKey != Keys.Menu && 
                    mainKey != Keys.ShiftKey && mainKey != Keys.LWin && mainKey != Keys.RWin)
                {
                    _currentConfig.Modifiers = modifiers;
                    _currentConfig.Key = mainKey;
                    UpdateHotkeyDisplay();
                    StopCapture();
                }
                
                return true;
            }
            
            return base.ProcessCmdKey(ref msg, keyData);
        }

        /// <summary>
        /// 修饰键复选框变化事件
        /// 修改记录: 2025-09-16 mkx 实现手动修饰键设置
        /// </summary>
        private void ModifierCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (_isCapturing) return;
            
            var modifiers = HotkeyManager.Modifiers.None;
            
            if (chkCtrl.Checked) modifiers |= HotkeyManager.Modifiers.Control;
            if (chkAlt.Checked) modifiers |= HotkeyManager.Modifiers.Alt;
            if (chkShift.Checked) modifiers |= HotkeyManager.Modifiers.Shift;
            if (chkWin.Checked) modifiers |= HotkeyManager.Modifiers.Win;
            
            _currentConfig.Modifiers = modifiers;
            UpdateHotkeyDisplay();
            UpdateButtonStates();
        }

        /// <summary>
        /// 重置为默认热键
        /// 修改记录: 2025-09-16 mkx 实现重置功能
        /// </summary>
        private void btnReset_Click(object sender, EventArgs e)
        {
            var defaultConfig = new HotkeyConfig();
            _currentConfig.Modifiers = defaultConfig.Modifiers;
            _currentConfig.Key = defaultConfig.Key;
            UpdateHotkeyDisplay();
            UpdateButtonStates();
        }

        /// <summary>
        /// 应用设置
        /// </summary>
        private void btnApply_Click(object sender, EventArgs e)
        {
            _originalConfig.Modifiers = _currentConfig.Modifiers;
            _originalConfig.Key = _currentConfig.Key;
            UpdateButtonStates();
        }

        /// <summary>
        /// 确定按钮
        /// </summary>
        private void btnOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        /// <summary>
        /// 取消按钮
        /// </summary>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}

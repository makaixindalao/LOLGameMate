namespace LOLGameMate
{
    partial class HotkeySettingsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.grpHotkey = new System.Windows.Forms.GroupBox();
            this.txtHotkeyDisplay = new System.Windows.Forms.TextBox();
            this.btnCapture = new System.Windows.Forms.Button();
            this.grpModifiers = new System.Windows.Forms.GroupBox();
            this.chkCtrl = new System.Windows.Forms.CheckBox();
            this.chkAlt = new System.Windows.Forms.CheckBox();
            this.chkShift = new System.Windows.Forms.CheckBox();
            this.chkWin = new System.Windows.Forms.CheckBox();
            this.lblMainKeyLabel = new System.Windows.Forms.Label();
            this.lblMainKey = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnApply = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();
            this.grpHotkey.SuspendLayout();
            this.grpModifiers.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpHotkey
            // 
            this.grpHotkey.Controls.Add(this.txtHotkeyDisplay);
            this.grpHotkey.Controls.Add(this.btnCapture);
            this.grpHotkey.Location = new System.Drawing.Point(12, 12);
            this.grpHotkey.Name = "grpHotkey";
            this.grpHotkey.Size = new System.Drawing.Size(360, 60);
            this.grpHotkey.TabIndex = 0;
            this.grpHotkey.TabStop = false;
            this.grpHotkey.Text = "当前热键";
            // 
            // txtHotkeyDisplay
            // 
            this.txtHotkeyDisplay.Font = new System.Drawing.Font("Microsoft YaHei UI", 10F, System.Drawing.FontStyle.Bold);
            this.txtHotkeyDisplay.Location = new System.Drawing.Point(16, 24);
            this.txtHotkeyDisplay.Name = "txtHotkeyDisplay";
            this.txtHotkeyDisplay.ReadOnly = true;
            this.txtHotkeyDisplay.Size = new System.Drawing.Size(200, 25);
            this.txtHotkeyDisplay.TabIndex = 0;
            this.txtHotkeyDisplay.Text = "Ctrl+Alt+Q";
            this.txtHotkeyDisplay.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnCapture
            // 
            this.btnCapture.Location = new System.Drawing.Point(230, 24);
            this.btnCapture.Name = "btnCapture";
            this.btnCapture.Size = new System.Drawing.Size(100, 25);
            this.btnCapture.TabIndex = 1;
            this.btnCapture.Text = "捕获热键";
            this.btnCapture.UseVisualStyleBackColor = true;
            this.btnCapture.Click += new System.EventHandler(this.btnCapture_Click);
            // 
            // grpModifiers
            // 
            this.grpModifiers.Controls.Add(this.chkCtrl);
            this.grpModifiers.Controls.Add(this.chkAlt);
            this.grpModifiers.Controls.Add(this.chkShift);
            this.grpModifiers.Controls.Add(this.chkWin);
            this.grpModifiers.Location = new System.Drawing.Point(12, 78);
            this.grpModifiers.Name = "grpModifiers";
            this.grpModifiers.Size = new System.Drawing.Size(240, 60);
            this.grpModifiers.TabIndex = 1;
            this.grpModifiers.TabStop = false;
            this.grpModifiers.Text = "修饰键";
            // 
            // chkCtrl
            // 
            this.chkCtrl.AutoSize = true;
            this.chkCtrl.Location = new System.Drawing.Point(16, 24);
            this.chkCtrl.Name = "chkCtrl";
            this.chkCtrl.Size = new System.Drawing.Size(47, 21);
            this.chkCtrl.TabIndex = 0;
            this.chkCtrl.Text = "Ctrl";
            this.chkCtrl.UseVisualStyleBackColor = true;
            this.chkCtrl.CheckedChanged += new System.EventHandler(this.ModifierCheckBox_CheckedChanged);
            // 
            // chkAlt
            // 
            this.chkAlt.AutoSize = true;
            this.chkAlt.Location = new System.Drawing.Point(69, 24);
            this.chkAlt.Name = "chkAlt";
            this.chkAlt.Size = new System.Drawing.Size(42, 21);
            this.chkAlt.TabIndex = 1;
            this.chkAlt.Text = "Alt";
            this.chkAlt.UseVisualStyleBackColor = true;
            this.chkAlt.CheckedChanged += new System.EventHandler(this.ModifierCheckBox_CheckedChanged);
            // 
            // chkShift
            // 
            this.chkShift.AutoSize = true;
            this.chkShift.Location = new System.Drawing.Point(117, 24);
            this.chkShift.Name = "chkShift";
            this.chkShift.Size = new System.Drawing.Size(53, 21);
            this.chkShift.TabIndex = 2;
            this.chkShift.Text = "Shift";
            this.chkShift.UseVisualStyleBackColor = true;
            this.chkShift.CheckedChanged += new System.EventHandler(this.ModifierCheckBox_CheckedChanged);
            // 
            // chkWin
            // 
            this.chkWin.AutoSize = true;
            this.chkWin.Location = new System.Drawing.Point(176, 24);
            this.chkWin.Name = "chkWin";
            this.chkWin.Size = new System.Drawing.Size(48, 21);
            this.chkWin.TabIndex = 3;
            this.chkWin.Text = "Win";
            this.chkWin.UseVisualStyleBackColor = true;
            this.chkWin.CheckedChanged += new System.EventHandler(this.ModifierCheckBox_CheckedChanged);
            // 
            // lblMainKeyLabel
            // 
            this.lblMainKeyLabel.AutoSize = true;
            this.lblMainKeyLabel.Location = new System.Drawing.Point(258, 102);
            this.lblMainKeyLabel.Name = "lblMainKeyLabel";
            this.lblMainKeyLabel.Size = new System.Drawing.Size(44, 17);
            this.lblMainKeyLabel.TabIndex = 2;
            this.lblMainKeyLabel.Text = "主键:";
            // 
            // lblMainKey
            // 
            this.lblMainKey.AutoSize = true;
            this.lblMainKey.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblMainKey.ForeColor = System.Drawing.Color.Blue;
            this.lblMainKey.Location = new System.Drawing.Point(308, 102);
            this.lblMainKey.Name = "lblMainKey";
            this.lblMainKey.Size = new System.Drawing.Size(16, 17);
            this.lblMainKey.TabIndex = 3;
            this.lblMainKey.Text = "Q";
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(12, 150);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(80, 17);
            this.lblStatus.TabIndex = 4;
            this.lblStatus.Text = "当前热键配置";
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(132, 180);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 25);
            this.btnOK.TabIndex = 5;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(213, 180);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 25);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnApply
            // 
            this.btnApply.Location = new System.Drawing.Point(294, 180);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(75, 25);
            this.btnApply.TabIndex = 7;
            this.btnApply.Text = "应用";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(12, 180);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(75, 25);
            this.btnReset.TabIndex = 8;
            this.btnReset.Text = "重置";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // HotkeySettingsForm
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(384, 217);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.btnApply);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.lblMainKey);
            this.Controls.Add(this.lblMainKeyLabel);
            this.Controls.Add(this.grpModifiers);
            this.Controls.Add(this.grpHotkey);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "HotkeySettingsForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "热键设置";
            this.grpHotkey.ResumeLayout(false);
            this.grpHotkey.PerformLayout();
            this.grpModifiers.ResumeLayout(false);
            this.grpModifiers.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.GroupBox grpHotkey;
        private System.Windows.Forms.TextBox txtHotkeyDisplay;
        private System.Windows.Forms.Button btnCapture;
        private System.Windows.Forms.GroupBox grpModifiers;
        private System.Windows.Forms.CheckBox chkCtrl;
        private System.Windows.Forms.CheckBox chkAlt;
        private System.Windows.Forms.CheckBox chkShift;
        private System.Windows.Forms.CheckBox chkWin;
        private System.Windows.Forms.Label lblMainKeyLabel;
        private System.Windows.Forms.Label lblMainKey;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.Button btnReset;
    }
}

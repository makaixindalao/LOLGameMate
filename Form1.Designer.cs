namespace LOLGameMate
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblUser = new System.Windows.Forms.Label();
            this.txtUser = new System.Windows.Forms.TextBox();
            this.lblPass = new System.Windows.Forms.Label();
            this.txtPass = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.lblHotkey = new System.Windows.Forms.Label();
            this.btnHotkeySettings = new System.Windows.Forms.Button();
            this.lblStatus = new System.Windows.Forms.Label();

            this.SuspendLayout();
            //
            // lblUser
            //
            this.lblUser.AutoSize = true;
            this.lblUser.Location = new System.Drawing.Point(24, 26);
            this.lblUser.Name = "lblUser";
            this.lblUser.Size = new System.Drawing.Size(56, 17);
            this.lblUser.TabIndex = 0;
            this.lblUser.Text = "用户名:";
            //
            // txtUser
            //
            this.txtUser.Location = new System.Drawing.Point(100, 22);
            this.txtUser.Name = "txtUser";
            this.txtUser.Size = new System.Drawing.Size(220, 23);
            this.txtUser.TabIndex = 1;
            //
            // lblPass
            //
            this.lblPass.AutoSize = true;
            this.lblPass.Location = new System.Drawing.Point(24, 65);
            this.lblPass.Name = "lblPass";
            this.lblPass.Size = new System.Drawing.Size(44, 17);
            this.lblPass.TabIndex = 2;
            this.lblPass.Text = "密码:";
            //
            // txtPass
            //
            this.txtPass.Location = new System.Drawing.Point(100, 61);
            this.txtPass.Name = "txtPass";
            this.txtPass.PasswordChar = '●';
            this.txtPass.Size = new System.Drawing.Size(220, 23);
            this.txtPass.TabIndex = 3;
            //
            // btnSave
            //
            this.btnSave.Location = new System.Drawing.Point(326, 22);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(68, 62);
            this.btnSave.TabIndex = 4;
            this.btnSave.Text = "保存";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            //
            // lblHotkey
            //
            this.lblHotkey.AutoSize = true;
            this.lblHotkey.Location = new System.Drawing.Point(24, 104);
            this.lblHotkey.Name = "lblHotkey";
            this.lblHotkey.Size = new System.Drawing.Size(200, 17);
            this.lblHotkey.TabIndex = 5;
            this.lblHotkey.Text = "全局热键: Ctrl+Alt+Q 触发自动登录";
            //
            // btnHotkeySettings
            //
            this.btnHotkeySettings.Location = new System.Drawing.Point(326, 100);
            this.btnHotkeySettings.Name = "btnHotkeySettings";
            this.btnHotkeySettings.Size = new System.Drawing.Size(68, 25);
            this.btnHotkeySettings.TabIndex = 8;
            this.btnHotkeySettings.Text = "设置热键";
            this.btnHotkeySettings.UseVisualStyleBackColor = true;
            this.btnHotkeySettings.Click += new System.EventHandler(this.btnHotkeySettings_Click);
            //
            // lblStatus
            //
            this.lblStatus.AutoSize = true;
            this.lblStatus.ForeColor = System.Drawing.Color.DimGray;
            this.lblStatus.Location = new System.Drawing.Point(24, 134);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(56, 17);
            this.lblStatus.TabIndex = 6;
            this.lblStatus.Text = "状态: -";

            //
            // Form1
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(420, 180);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.btnHotkeySettings);
            this.Controls.Add(this.lblHotkey);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.txtPass);
            this.Controls.Add(this.lblPass);
            this.Controls.Add(this.txtUser);
            this.Controls.Add(this.lblUser);
            this.Name = "Form1";
            this.Text = "LOL Game Mate";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblUser;
        private System.Windows.Forms.TextBox txtUser;
        private System.Windows.Forms.Label lblPass;
        private System.Windows.Forms.TextBox txtPass;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label lblHotkey;
        private System.Windows.Forms.Button btnHotkeySettings;
        private System.Windows.Forms.Label lblStatus;
    }
}

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
            this.lblStatus = new System.Windows.Forms.Label();
            this.grpInputMode = new System.Windows.Forms.GroupBox();
            this.rbSendKeys = new System.Windows.Forms.RadioButton();
            this.rbDD = new System.Windows.Forms.RadioButton();
            this.btnSelectDD = new System.Windows.Forms.Button();
            this.txtDDPath = new System.Windows.Forms.TextBox();
            this.lblDDPath = new System.Windows.Forms.Label();
            this.grpInputMode.SuspendLayout();
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
            this.lblHotkey.Text = "全局热键: Alt+1 触发自动登录";
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
            // grpInputMode
            //
            this.grpInputMode.Controls.Add(this.rbSendKeys);
            this.grpInputMode.Controls.Add(this.rbDD);
            this.grpInputMode.Controls.Add(this.btnSelectDD);
            this.grpInputMode.Controls.Add(this.txtDDPath);
            this.grpInputMode.Controls.Add(this.lblDDPath);
            this.grpInputMode.Location = new System.Drawing.Point(24, 164);
            this.grpInputMode.Name = "grpInputMode";
            this.grpInputMode.Size = new System.Drawing.Size(370, 120);
            this.grpInputMode.TabIndex = 7;
            this.grpInputMode.TabStop = false;
            this.grpInputMode.Text = "输入模式";
            //
            // rbSendKeys
            //
            this.rbSendKeys.AutoSize = true;
            this.rbSendKeys.Checked = true;
            this.rbSendKeys.Location = new System.Drawing.Point(16, 24);
            this.rbSendKeys.Name = "rbSendKeys";
            this.rbSendKeys.Size = new System.Drawing.Size(139, 21);
            this.rbSendKeys.TabIndex = 0;
            this.rbSendKeys.TabStop = true;
            this.rbSendKeys.Text = "SendKeys (默认)";
            this.rbSendKeys.UseVisualStyleBackColor = true;
            this.rbSendKeys.CheckedChanged += new System.EventHandler(this.rbSendKeys_CheckedChanged);
            //
            // rbDD
            //
            this.rbDD.AutoSize = true;
            this.rbDD.Location = new System.Drawing.Point(16, 51);
            this.rbDD.Name = "rbDD";
            this.rbDD.Size = new System.Drawing.Size(89, 21);
            this.rbDD.TabIndex = 1;
            this.rbDD.Text = "DD 驱动";
            this.rbDD.UseVisualStyleBackColor = true;
            this.rbDD.CheckedChanged += new System.EventHandler(this.rbDD_CheckedChanged);
            //
            // lblDDPath
            //
            this.lblDDPath.AutoSize = true;
            this.lblDDPath.Location = new System.Drawing.Point(16, 78);
            this.lblDDPath.Name = "lblDDPath";
            this.lblDDPath.Size = new System.Drawing.Size(68, 17);
            this.lblDDPath.TabIndex = 2;
            this.lblDDPath.Text = "DD 库路径:";
            //
            // txtDDPath
            //
            this.txtDDPath.Location = new System.Drawing.Point(90, 75);
            this.txtDDPath.Name = "txtDDPath";
            this.txtDDPath.ReadOnly = true;
            this.txtDDPath.Size = new System.Drawing.Size(200, 23);
            this.txtDDPath.TabIndex = 3;
            //
            // btnSelectDD
            //
            this.btnSelectDD.Location = new System.Drawing.Point(296, 75);
            this.btnSelectDD.Name = "btnSelectDD";
            this.btnSelectDD.Size = new System.Drawing.Size(60, 23);
            this.btnSelectDD.TabIndex = 4;
            this.btnSelectDD.Text = "选择...";
            this.btnSelectDD.UseVisualStyleBackColor = true;
            this.btnSelectDD.Click += new System.EventHandler(this.btnSelectDD_Click);
            //
            // Form1
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(420, 300);
            this.Controls.Add(this.grpInputMode);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.lblHotkey);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.txtPass);
            this.Controls.Add(this.lblPass);
            this.Controls.Add(this.txtUser);
            this.Controls.Add(this.lblUser);
            this.Name = "Form1";
            this.Text = "LOL Game Mate";
            this.grpInputMode.ResumeLayout(false);
            this.grpInputMode.PerformLayout();
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
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.GroupBox grpInputMode;
        private System.Windows.Forms.RadioButton rbSendKeys;
        private System.Windows.Forms.RadioButton rbDD;
        private System.Windows.Forms.Button btnSelectDD;
        private System.Windows.Forms.TextBox txtDDPath;
        private System.Windows.Forms.Label lblDDPath;
    }
}

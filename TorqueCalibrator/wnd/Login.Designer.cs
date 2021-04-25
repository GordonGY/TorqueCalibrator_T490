namespace TorqueCalibrator.wnd
{
    partial class Login
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Login));
            this.usernameTbx = new System.Windows.Forms.TextBox();
            this.passwordTbx = new System.Windows.Forms.TextBox();
            this.usernameLab = new System.Windows.Forms.Label();
            this.passwordLab = new System.Windows.Forms.Label();
            this.cardNumTbx = new System.Windows.Forms.TextBox();
            this.userRdbt = new System.Windows.Forms.RadioButton();
            this.cardRdbt = new System.Windows.Forms.RadioButton();
            this.cardLab = new System.Windows.Forms.Label();
            this.loginBtn = new System.Windows.Forms.Button();
            this.logoutBtn = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.capitalLab = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // usernameTbx
            // 
            this.usernameTbx.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.usernameTbx.Location = new System.Drawing.Point(152, 202);
            this.usernameTbx.Name = "usernameTbx";
            this.usernameTbx.Size = new System.Drawing.Size(107, 23);
            this.usernameTbx.TabIndex = 0;
            this.usernameTbx.Text = "xiaonan";
            this.usernameTbx.Visible = false;
            // 
            // passwordTbx
            // 
            this.passwordTbx.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.passwordTbx.Location = new System.Drawing.Point(152, 242);
            this.passwordTbx.Name = "passwordTbx";
            this.passwordTbx.PasswordChar = '*';
            this.passwordTbx.Size = new System.Drawing.Size(107, 23);
            this.passwordTbx.TabIndex = 0;
            this.passwordTbx.Text = "ZK123456";
            this.passwordTbx.Visible = false;
            // 
            // usernameLab
            // 
            this.usernameLab.AutoSize = true;
            this.usernameLab.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.usernameLab.Location = new System.Drawing.Point(93, 210);
            this.usernameLab.Name = "usernameLab";
            this.usernameLab.Size = new System.Drawing.Size(44, 17);
            this.usernameLab.TabIndex = 1;
            this.usernameLab.Text = "用户名";
            this.usernameLab.Visible = false;
            // 
            // passwordLab
            // 
            this.passwordLab.AutoSize = true;
            this.passwordLab.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.passwordLab.Location = new System.Drawing.Point(93, 242);
            this.passwordLab.Name = "passwordLab";
            this.passwordLab.Size = new System.Drawing.Size(32, 17);
            this.passwordLab.TabIndex = 1;
            this.passwordLab.Text = "密码";
            this.passwordLab.Visible = false;
            // 
            // cardNumTbx
            // 
            this.cardNumTbx.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cardNumTbx.Location = new System.Drawing.Point(152, 224);
            this.cardNumTbx.Name = "cardNumTbx";
            this.cardNumTbx.PasswordChar = '*';
            this.cardNumTbx.Size = new System.Drawing.Size(107, 23);
            this.cardNumTbx.TabIndex = 0;
            this.cardNumTbx.TextChanged += new System.EventHandler(this.cardNumTbx_TextChanged);
            // 
            // userRdbt
            // 
            this.userRdbt.AutoSize = true;
            this.userRdbt.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.userRdbt.Location = new System.Drawing.Point(152, 143);
            this.userRdbt.Name = "userRdbt";
            this.userRdbt.Size = new System.Drawing.Size(110, 21);
            this.userRdbt.TabIndex = 2;
            this.userRdbt.Text = "用户名密码登录";
            this.userRdbt.UseVisualStyleBackColor = true;
            this.userRdbt.CheckedChanged += new System.EventHandler(this.userRdbt_CheckedChanged);
            // 
            // cardRdbt
            // 
            this.cardRdbt.AutoSize = true;
            this.cardRdbt.Checked = true;
            this.cardRdbt.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cardRdbt.Location = new System.Drawing.Point(152, 166);
            this.cardRdbt.Name = "cardRdbt";
            this.cardRdbt.Size = new System.Drawing.Size(74, 21);
            this.cardRdbt.TabIndex = 2;
            this.cardRdbt.TabStop = true;
            this.cardRdbt.Text = "刷卡登录";
            this.cardRdbt.UseVisualStyleBackColor = true;
            this.cardRdbt.CheckedChanged += new System.EventHandler(this.cardRdbt_CheckedChanged);
            // 
            // cardLab
            // 
            this.cardLab.AutoSize = true;
            this.cardLab.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cardLab.Location = new System.Drawing.Point(70, 226);
            this.cardLab.Name = "cardLab";
            this.cardLab.Size = new System.Drawing.Size(68, 17);
            this.cardLab.TabIndex = 1;
            this.cardLab.Text = "请刷员工卡";
            // 
            // loginBtn
            // 
            this.loginBtn.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.loginBtn.Location = new System.Drawing.Point(123, 296);
            this.loginBtn.Name = "loginBtn";
            this.loginBtn.Size = new System.Drawing.Size(75, 23);
            this.loginBtn.TabIndex = 3;
            this.loginBtn.Text = "登录";
            this.loginBtn.UseVisualStyleBackColor = true;
            this.loginBtn.Click += new System.EventHandler(this.loginBtn_Click);
            // 
            // logoutBtn
            // 
            this.logoutBtn.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.logoutBtn.Location = new System.Drawing.Point(212, 296);
            this.logoutBtn.Name = "logoutBtn";
            this.logoutBtn.Size = new System.Drawing.Size(75, 23);
            this.logoutBtn.TabIndex = 3;
            this.logoutBtn.Text = "退出系统";
            this.logoutBtn.UseVisualStyleBackColor = true;
            this.logoutBtn.Click += new System.EventHandler(this.logoutBtn_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(-3, -4);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(135, 105);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            // 
            // capitalLab
            // 
            this.capitalLab.AutoSize = true;
            this.capitalLab.Font = new System.Drawing.Font("微软雅黑", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.capitalLab.Location = new System.Drawing.Point(112, 72);
            this.capitalLab.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.capitalLab.Name = "capitalLab";
            this.capitalLab.Size = new System.Drawing.Size(189, 30);
            this.capitalLab.TabIndex = 5;
            this.capitalLab.Text = "智能扭矩校验系统";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(157, 273);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 6;
            this.label1.Text = "label1";
            // 
            // Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(403, 342);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.capitalLab);
            this.Controls.Add(this.logoutBtn);
            this.Controls.Add(this.loginBtn);
            this.Controls.Add(this.cardRdbt);
            this.Controls.Add(this.userRdbt);
            this.Controls.Add(this.passwordLab);
            this.Controls.Add(this.cardLab);
            this.Controls.Add(this.usernameLab);
            this.Controls.Add(this.cardNumTbx);
            this.Controls.Add(this.passwordTbx);
            this.Controls.Add(this.usernameTbx);
            this.Controls.Add(this.pictureBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Login";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Login";
            this.Load += new System.EventHandler(this.Login_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox usernameTbx;
        private System.Windows.Forms.TextBox passwordTbx;
        private System.Windows.Forms.Label usernameLab;
        private System.Windows.Forms.Label passwordLab;
        private System.Windows.Forms.RadioButton userRdbt;
        private System.Windows.Forms.RadioButton cardRdbt;
        private System.Windows.Forms.Label cardLab;
        private System.Windows.Forms.Button loginBtn;
        private System.Windows.Forms.Button logoutBtn;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label capitalLab;
        public System.Windows.Forms.TextBox cardNumTbx;
        private System.Windows.Forms.Label label1;
    }
}
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
            this.usernameTbx.Location = new System.Drawing.Point(202, 252);
            this.usernameTbx.Margin = new System.Windows.Forms.Padding(4);
            this.usernameTbx.Name = "usernameTbx";
            this.usernameTbx.Size = new System.Drawing.Size(141, 27);
            this.usernameTbx.TabIndex = 0;
            this.usernameTbx.Text = "xiaonan";
            this.usernameTbx.Visible = false;
            // 
            // passwordTbx
            // 
            this.passwordTbx.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.passwordTbx.Location = new System.Drawing.Point(202, 302);
            this.passwordTbx.Margin = new System.Windows.Forms.Padding(4);
            this.passwordTbx.Name = "passwordTbx";
            this.passwordTbx.PasswordChar = '*';
            this.passwordTbx.Size = new System.Drawing.Size(141, 27);
            this.passwordTbx.TabIndex = 0;
            this.passwordTbx.Text = "ZK123456";
            this.passwordTbx.Visible = false;
            // 
            // usernameLab
            // 
            this.usernameLab.AutoSize = true;
            this.usernameLab.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.usernameLab.Location = new System.Drawing.Point(124, 263);
            this.usernameLab.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.usernameLab.Name = "usernameLab";
            this.usernameLab.Size = new System.Drawing.Size(54, 20);
            this.usernameLab.TabIndex = 1;
            this.usernameLab.Text = "用户名";
            this.usernameLab.Visible = false;
            // 
            // passwordLab
            // 
            this.passwordLab.AutoSize = true;
            this.passwordLab.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.passwordLab.Location = new System.Drawing.Point(124, 302);
            this.passwordLab.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.passwordLab.Name = "passwordLab";
            this.passwordLab.Size = new System.Drawing.Size(39, 20);
            this.passwordLab.TabIndex = 1;
            this.passwordLab.Text = "密码";
            this.passwordLab.Visible = false;
            // 
            // cardNumTbx
            // 
            this.cardNumTbx.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cardNumTbx.Location = new System.Drawing.Point(202, 280);
            this.cardNumTbx.Margin = new System.Windows.Forms.Padding(4);
            this.cardNumTbx.Name = "cardNumTbx";
            this.cardNumTbx.PasswordChar = '*';
            this.cardNumTbx.Size = new System.Drawing.Size(141, 27);
            this.cardNumTbx.TabIndex = 0;
            this.cardNumTbx.TextChanged += new System.EventHandler(this.cardNumTbx_TextChanged);
            // 
            // userRdbt
            // 
            this.userRdbt.AutoSize = true;
            this.userRdbt.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.userRdbt.Location = new System.Drawing.Point(202, 179);
            this.userRdbt.Margin = new System.Windows.Forms.Padding(4);
            this.userRdbt.Name = "userRdbt";
            this.userRdbt.Size = new System.Drawing.Size(135, 24);
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
            this.cardRdbt.Location = new System.Drawing.Point(202, 207);
            this.cardRdbt.Margin = new System.Windows.Forms.Padding(4);
            this.cardRdbt.Name = "cardRdbt";
            this.cardRdbt.Size = new System.Drawing.Size(90, 24);
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
            this.cardLab.Location = new System.Drawing.Point(94, 283);
            this.cardLab.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.cardLab.Name = "cardLab";
            this.cardLab.Size = new System.Drawing.Size(84, 20);
            this.cardLab.TabIndex = 1;
            this.cardLab.Text = "请刷员工卡";
            // 
            // loginBtn
            // 
            this.loginBtn.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.loginBtn.Location = new System.Drawing.Point(164, 370);
            this.loginBtn.Margin = new System.Windows.Forms.Padding(4);
            this.loginBtn.Name = "loginBtn";
            this.loginBtn.Size = new System.Drawing.Size(100, 29);
            this.loginBtn.TabIndex = 3;
            this.loginBtn.Text = "登录";
            this.loginBtn.UseVisualStyleBackColor = true;
            this.loginBtn.Click += new System.EventHandler(this.loginBtn_Click);
            // 
            // logoutBtn
            // 
            this.logoutBtn.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.logoutBtn.Location = new System.Drawing.Point(282, 370);
            this.logoutBtn.Margin = new System.Windows.Forms.Padding(4);
            this.logoutBtn.Name = "logoutBtn";
            this.logoutBtn.Size = new System.Drawing.Size(100, 29);
            this.logoutBtn.TabIndex = 3;
            this.logoutBtn.Text = "退出系统";
            this.logoutBtn.UseVisualStyleBackColor = true;
            this.logoutBtn.Click += new System.EventHandler(this.logoutBtn_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(-4, -5);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(180, 131);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            // 
            // capitalLab
            // 
            this.capitalLab.AutoSize = true;
            this.capitalLab.Font = new System.Drawing.Font("微软雅黑", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.capitalLab.Location = new System.Drawing.Point(149, 90);
            this.capitalLab.Name = "capitalLab";
            this.capitalLab.Size = new System.Drawing.Size(239, 36);
            this.capitalLab.TabIndex = 5;
            this.capitalLab.Text = "智能扭矩校验系统";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(209, 341);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 15);
            this.label1.TabIndex = 6;
            this.label1.Text = "label1";
            // 
            // Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(537, 427);
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
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Login";
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
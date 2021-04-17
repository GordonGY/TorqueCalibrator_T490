namespace TorqueCalibrator.wnd
{
    partial class BorrowToolWnd
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
            this.seriesNumTbx = new System.Windows.Forms.TextBox();
            this.seriesNumLab = new System.Windows.Forms.Label();
            this.borrowBtn = new System.Windows.Forms.Button();
            this.extendBtn = new System.Windows.Forms.Button();
            this.RecordDgv = new System.Windows.Forms.DataGridView();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.seriesNum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.operator1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.createTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.updateTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.queryBtn = new System.Windows.Forms.Button();
            this.endDtp = new System.Windows.Forms.DateTimePicker();
            this.startDtp = new System.Windows.Forms.DateTimePicker();
            this.userLab = new System.Windows.Forms.Label();
            this.userCbx = new System.Windows.Forms.ComboBox();
            this.statusChbx = new System.Windows.Forms.CheckBox();
            this.returnBtn = new System.Windows.Forms.Button();
            this.borrowLab = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.RecordDgv)).BeginInit();
            this.SuspendLayout();
            // 
            // seriesNumTbx
            // 
            this.seriesNumTbx.Location = new System.Drawing.Point(114, 18);
            this.seriesNumTbx.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.seriesNumTbx.Name = "seriesNumTbx";
            this.seriesNumTbx.Size = new System.Drawing.Size(148, 28);
            this.seriesNumTbx.TabIndex = 11;
            // 
            // seriesNumLab
            // 
            this.seriesNumLab.AutoSize = true;
            this.seriesNumLab.Location = new System.Drawing.Point(26, 26);
            this.seriesNumLab.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.seriesNumLab.Name = "seriesNumLab";
            this.seriesNumLab.Size = new System.Drawing.Size(80, 18);
            this.seriesNumLab.TabIndex = 10;
            this.seriesNumLab.Text = "扳手条码";
            // 
            // borrowBtn
            // 
            this.borrowBtn.Location = new System.Drawing.Point(291, 14);
            this.borrowBtn.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.borrowBtn.Name = "borrowBtn";
            this.borrowBtn.Size = new System.Drawing.Size(78, 35);
            this.borrowBtn.TabIndex = 12;
            this.borrowBtn.Text = "借用";
            this.borrowBtn.UseVisualStyleBackColor = true;
            this.borrowBtn.Click += new System.EventHandler(this.borrow_Click);
            // 
            // extendBtn
            // 
            this.extendBtn.Location = new System.Drawing.Point(515, 14);
            this.extendBtn.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.extendBtn.Name = "extendBtn";
            this.extendBtn.Size = new System.Drawing.Size(76, 35);
            this.extendBtn.TabIndex = 13;
            this.extendBtn.Text = "展开";
            this.extendBtn.UseVisualStyleBackColor = true;
            this.extendBtn.Click += new System.EventHandler(this.button1_Click);
            // 
            // RecordDgv
            // 
            this.RecordDgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.RecordDgv.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.id,
            this.seriesNum,
            this.operator1,
            this.status,
            this.createTime,
            this.updateTime});
            this.RecordDgv.Location = new System.Drawing.Point(28, 122);
            this.RecordDgv.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.RecordDgv.Name = "RecordDgv";
            this.RecordDgv.RowHeadersWidth = 51;
            this.RecordDgv.RowTemplate.Height = 23;
            this.RecordDgv.Size = new System.Drawing.Size(1074, 410);
            this.RecordDgv.TabIndex = 14;
            this.RecordDgv.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.RecordDgv_CellContentClick);
            // 
            // id
            // 
            this.id.HeaderText = "id";
            this.id.MinimumWidth = 6;
            this.id.Name = "id";
            this.id.Visible = false;
            this.id.Width = 125;
            // 
            // seriesNum
            // 
            this.seriesNum.HeaderText = "工具编号";
            this.seriesNum.MinimumWidth = 6;
            this.seriesNum.Name = "seriesNum";
            this.seriesNum.Width = 125;
            // 
            // operator1
            // 
            this.operator1.HeaderText = "借用人";
            this.operator1.MinimumWidth = 6;
            this.operator1.Name = "operator1";
            this.operator1.Width = 125;
            // 
            // status
            // 
            this.status.HeaderText = "当前状态";
            this.status.MinimumWidth = 6;
            this.status.Name = "status";
            this.status.Width = 125;
            // 
            // createTime
            // 
            this.createTime.HeaderText = "借用时间";
            this.createTime.MinimumWidth = 6;
            this.createTime.Name = "createTime";
            this.createTime.Width = 150;
            // 
            // updateTime
            // 
            this.updateTime.HeaderText = "归还时间";
            this.updateTime.MinimumWidth = 6;
            this.updateTime.Name = "updateTime";
            this.updateTime.Width = 150;
            // 
            // queryBtn
            // 
            this.queryBtn.Location = new System.Drawing.Point(798, 71);
            this.queryBtn.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.queryBtn.Name = "queryBtn";
            this.queryBtn.Size = new System.Drawing.Size(91, 36);
            this.queryBtn.TabIndex = 15;
            this.queryBtn.Text = "查询";
            this.queryBtn.UseVisualStyleBackColor = true;
            this.queryBtn.Click += new System.EventHandler(this.queryBtn_Click);
            // 
            // endDtp
            // 
            this.endDtp.CustomFormat = "yyyy-MM-dd";
            this.endDtp.Location = new System.Drawing.Point(498, 72);
            this.endDtp.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.endDtp.Name = "endDtp";
            this.endDtp.Size = new System.Drawing.Size(183, 28);
            this.endDtp.TabIndex = 18;
            // 
            // startDtp
            // 
            this.startDtp.CustomFormat = "yyyy-MM-dd";
            this.startDtp.Location = new System.Drawing.Point(291, 72);
            this.startDtp.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.startDtp.Name = "startDtp";
            this.startDtp.Size = new System.Drawing.Size(183, 28);
            this.startDtp.TabIndex = 19;
            // 
            // userLab
            // 
            this.userLab.AutoSize = true;
            this.userLab.Location = new System.Drawing.Point(43, 79);
            this.userLab.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.userLab.Name = "userLab";
            this.userLab.Size = new System.Drawing.Size(62, 18);
            this.userLab.TabIndex = 17;
            this.userLab.Text = "借用人";
            // 
            // userCbx
            // 
            this.userCbx.FormattingEnabled = true;
            this.userCbx.Location = new System.Drawing.Point(114, 72);
            this.userCbx.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.userCbx.Name = "userCbx";
            this.userCbx.Size = new System.Drawing.Size(148, 26);
            this.userCbx.TabIndex = 16;
            // 
            // statusChbx
            // 
            this.statusChbx.AutoSize = true;
            this.statusChbx.Checked = true;
            this.statusChbx.CheckState = System.Windows.Forms.CheckState.Checked;
            this.statusChbx.Location = new System.Drawing.Point(690, 77);
            this.statusChbx.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.statusChbx.Name = "statusChbx";
            this.statusChbx.Size = new System.Drawing.Size(106, 22);
            this.statusChbx.TabIndex = 20;
            this.statusChbx.Text = "只查借出";
            this.statusChbx.UseVisualStyleBackColor = true;
            // 
            // returnBtn
            // 
            this.returnBtn.Location = new System.Drawing.Point(397, 14);
            this.returnBtn.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.returnBtn.Name = "returnBtn";
            this.returnBtn.Size = new System.Drawing.Size(78, 35);
            this.returnBtn.TabIndex = 12;
            this.returnBtn.Text = "归还";
            this.returnBtn.UseVisualStyleBackColor = true;
            this.returnBtn.Click += new System.EventHandler(this.return_Click);
            // 
            // borrowLab
            // 
            this.borrowLab.AutoSize = true;
            this.borrowLab.Location = new System.Drawing.Point(623, 23);
            this.borrowLab.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.borrowLab.Name = "borrowLab";
            this.borrowLab.Size = new System.Drawing.Size(0, 18);
            this.borrowLab.TabIndex = 17;
            // 
            // BorrowToolWnd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(621, 58);
            this.Controls.Add(this.statusChbx);
            this.Controls.Add(this.endDtp);
            this.Controls.Add(this.startDtp);
            this.Controls.Add(this.borrowLab);
            this.Controls.Add(this.userLab);
            this.Controls.Add(this.userCbx);
            this.Controls.Add(this.queryBtn);
            this.Controls.Add(this.RecordDgv);
            this.Controls.Add(this.extendBtn);
            this.Controls.Add(this.returnBtn);
            this.Controls.Add(this.borrowBtn);
            this.Controls.Add(this.seriesNumTbx);
            this.Controls.Add(this.seriesNumLab);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "BorrowToolWnd";
            this.Text = "BorrowToolWnd";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.BorrowToolWnd_FormClosing);
            this.Load += new System.EventHandler(this.BorrowToolWnd_Load);
            ((System.ComponentModel.ISupportInitialize)(this.RecordDgv)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label seriesNumLab;
        private System.Windows.Forms.Button borrowBtn;
        private System.Windows.Forms.Button extendBtn;
        private System.Windows.Forms.DataGridView RecordDgv;
        private System.Windows.Forms.Button queryBtn;
        private System.Windows.Forms.DateTimePicker endDtp;
        private System.Windows.Forms.DateTimePicker startDtp;
        private System.Windows.Forms.Label userLab;
        private System.Windows.Forms.ComboBox userCbx;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
        private System.Windows.Forms.DataGridViewTextBoxColumn seriesNum;
        private System.Windows.Forms.DataGridViewTextBoxColumn operator1;
        private System.Windows.Forms.DataGridViewTextBoxColumn status;
        private System.Windows.Forms.DataGridViewTextBoxColumn createTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn updateTime;
        private System.Windows.Forms.CheckBox statusChbx;
        private System.Windows.Forms.Button returnBtn;
        private System.Windows.Forms.Label borrowLab;
        public System.Windows.Forms.TextBox seriesNumTbx;
    }
}
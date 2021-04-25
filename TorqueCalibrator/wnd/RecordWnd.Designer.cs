namespace TorqueCalibrator.wnd
{
    partial class RecordWnd
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RecordWnd));
            this.queryBtn = new System.Windows.Forms.Button();
            this.productCbx = new System.Windows.Forms.ComboBox();
            this.productLab = new System.Windows.Forms.Label();
            this.seriesNumLab = new System.Windows.Forms.Label();
            this.seriesNumTbx = new System.Windows.Forms.TextBox();
            this.RecordDgv = new System.Windows.Forms.DataGridView();
            this.check = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.seriesNum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.proName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.checkMode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.result = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.operator1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.createTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.print = new System.Windows.Forms.DataGridViewButtonColumn();
            this.printBtn = new System.Windows.Forms.Button();
            this.startDtp = new System.Windows.Forms.DateTimePicker();
            this.endDtp = new System.Windows.Forms.DateTimePicker();
            this.RecordDetailDgv = new System.Windows.Forms.DataGridView();
            this.id2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.index = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.testValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.result1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.difference = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.percent = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.standard = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.upper = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lower = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.userCbx = new System.Windows.Forms.ComboBox();
            this.userLab = new System.Windows.Forms.Label();
            this.AllTRCheckBox = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.RecordDgv)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RecordDetailDgv)).BeginInit();
            this.SuspendLayout();
            // 
            // queryBtn
            // 
            this.queryBtn.Location = new System.Drawing.Point(831, 11);
            this.queryBtn.Name = "queryBtn";
            this.queryBtn.Size = new System.Drawing.Size(75, 23);
            this.queryBtn.TabIndex = 0;
            this.queryBtn.Text = "查询";
            this.queryBtn.UseVisualStyleBackColor = true;
            this.queryBtn.Click += new System.EventHandler(this.queryBtn_Click);
            // 
            // productCbx
            // 
            this.productCbx.FormattingEnabled = true;
            this.productCbx.Location = new System.Drawing.Point(235, 36);
            this.productCbx.Name = "productCbx";
            this.productCbx.Size = new System.Drawing.Size(100, 20);
            this.productCbx.TabIndex = 7;
            this.productCbx.Visible = false;
            this.productCbx.SelectedIndexChanged += new System.EventHandler(this.productCbx_SelectedIndexChanged);
            // 
            // productLab
            // 
            this.productLab.AutoSize = true;
            this.productLab.Location = new System.Drawing.Point(180, 41);
            this.productLab.Name = "productLab";
            this.productLab.Size = new System.Drawing.Size(53, 12);
            this.productLab.TabIndex = 8;
            this.productLab.Text = "扳手型号";
            this.productLab.Visible = false;
            // 
            // seriesNumLab
            // 
            this.seriesNumLab.AutoSize = true;
            this.seriesNumLab.Location = new System.Drawing.Point(11, 17);
            this.seriesNumLab.Name = "seriesNumLab";
            this.seriesNumLab.Size = new System.Drawing.Size(53, 12);
            this.seriesNumLab.TabIndex = 8;
            this.seriesNumLab.Text = "扳手条码";
            // 
            // seriesNumTbx
            // 
            this.seriesNumTbx.Location = new System.Drawing.Point(70, 11);
            this.seriesNumTbx.Name = "seriesNumTbx";
            this.seriesNumTbx.Size = new System.Drawing.Size(100, 21);
            this.seriesNumTbx.TabIndex = 9;
            // 
            // RecordDgv
            // 
            this.RecordDgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.RecordDgv.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.check,
            this.id,
            this.seriesNum,
            this.proName,
            this.checkMode,
            this.result,
            this.operator1,
            this.createTime,
            this.print});
            this.RecordDgv.Cursor = System.Windows.Forms.Cursors.Hand;
            this.RecordDgv.Location = new System.Drawing.Point(8, 65);
            this.RecordDgv.Name = "RecordDgv";
            this.RecordDgv.RowHeadersWidth = 51;
            this.RecordDgv.RowTemplate.Height = 23;
            this.RecordDgv.Size = new System.Drawing.Size(1005, 287);
            this.RecordDgv.TabIndex = 10;
            this.RecordDgv.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.RecordDgv_CellClick);
            this.RecordDgv.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.RecordDgv_CellContentClick);
            // 
            // check
            // 
            this.check.HeaderText = "";
            this.check.MinimumWidth = 6;
            this.check.Name = "check";
            this.check.Width = 20;
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
            // proName
            // 
            this.proName.HeaderText = "扳手类型";
            this.proName.MinimumWidth = 6;
            this.proName.Name = "proName";
            this.proName.Width = 125;
            // 
            // checkMode
            // 
            this.checkMode.HeaderText = "校验类型";
            this.checkMode.MinimumWidth = 6;
            this.checkMode.Name = "checkMode";
            this.checkMode.Width = 125;
            // 
            // result
            // 
            this.result.HeaderText = "结果";
            this.result.MinimumWidth = 6;
            this.result.Name = "result";
            this.result.Width = 125;
            // 
            // operator1
            // 
            this.operator1.HeaderText = "操作人";
            this.operator1.MinimumWidth = 6;
            this.operator1.Name = "operator1";
            this.operator1.Width = 125;
            // 
            // createTime
            // 
            this.createTime.HeaderText = "测试时间";
            this.createTime.MinimumWidth = 6;
            this.createTime.Name = "createTime";
            this.createTime.Width = 125;
            // 
            // print
            // 
            this.print.HeaderText = "";
            this.print.MinimumWidth = 6;
            this.print.Name = "print";
            this.print.Text = "操作";
            this.print.Width = 125;
            // 
            // printBtn
            // 
            this.printBtn.Location = new System.Drawing.Point(938, 11);
            this.printBtn.Name = "printBtn";
            this.printBtn.Size = new System.Drawing.Size(75, 23);
            this.printBtn.TabIndex = 0;
            this.printBtn.Text = "打印";
            this.printBtn.UseVisualStyleBackColor = true;
            this.printBtn.Click += new System.EventHandler(this.printBtn_Click);
            // 
            // startDtp
            // 
            this.startDtp.CustomFormat = "yyyy-MM-dd";
            this.startDtp.Location = new System.Drawing.Point(353, 11);
            this.startDtp.Name = "startDtp";
            this.startDtp.Size = new System.Drawing.Size(123, 21);
            this.startDtp.TabIndex = 11;
            // 
            // endDtp
            // 
            this.endDtp.CustomFormat = "yyyy-MM-dd";
            this.endDtp.Location = new System.Drawing.Point(529, 11);
            this.endDtp.Name = "endDtp";
            this.endDtp.Size = new System.Drawing.Size(123, 21);
            this.endDtp.TabIndex = 11;
            // 
            // RecordDetailDgv
            // 
            this.RecordDetailDgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.RecordDetailDgv.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.id2,
            this.index,
            this.testValue,
            this.result1,
            this.difference,
            this.percent,
            this.standard,
            this.upper,
            this.lower});
            this.RecordDetailDgv.Location = new System.Drawing.Point(8, 358);
            this.RecordDetailDgv.Name = "RecordDetailDgv";
            this.RecordDetailDgv.RowHeadersWidth = 51;
            this.RecordDetailDgv.RowTemplate.Height = 23;
            this.RecordDetailDgv.Size = new System.Drawing.Size(1005, 184);
            this.RecordDetailDgv.TabIndex = 10;
            // 
            // id2
            // 
            this.id2.HeaderText = "id";
            this.id2.MinimumWidth = 6;
            this.id2.Name = "id2";
            this.id2.Visible = false;
            this.id2.Width = 125;
            // 
            // index
            // 
            this.index.HeaderText = "编号";
            this.index.MinimumWidth = 6;
            this.index.Name = "index";
            this.index.Width = 125;
            // 
            // testValue
            // 
            this.testValue.HeaderText = "测试值";
            this.testValue.MinimumWidth = 6;
            this.testValue.Name = "testValue";
            this.testValue.Width = 125;
            // 
            // result1
            // 
            this.result1.HeaderText = "试验结果";
            this.result1.MinimumWidth = 6;
            this.result1.Name = "result1";
            this.result1.Width = 125;
            // 
            // difference
            // 
            this.difference.HeaderText = "误差";
            this.difference.MinimumWidth = 6;
            this.difference.Name = "difference";
            this.difference.Width = 125;
            // 
            // percent
            // 
            this.percent.HeaderText = "误差百分比";
            this.percent.MinimumWidth = 6;
            this.percent.Name = "percent";
            this.percent.Width = 125;
            // 
            // standard
            // 
            this.standard.HeaderText = "标准值";
            this.standard.MinimumWidth = 6;
            this.standard.Name = "standard";
            this.standard.Width = 125;
            // 
            // upper
            // 
            this.upper.HeaderText = "上限";
            this.upper.MinimumWidth = 6;
            this.upper.Name = "upper";
            this.upper.Width = 125;
            // 
            // lower
            // 
            this.lower.HeaderText = "下限";
            this.lower.MinimumWidth = 6;
            this.lower.Name = "lower";
            this.lower.Width = 125;
            // 
            // userCbx
            // 
            this.userCbx.FormattingEnabled = true;
            this.userCbx.Location = new System.Drawing.Point(235, 11);
            this.userCbx.Name = "userCbx";
            this.userCbx.Size = new System.Drawing.Size(100, 20);
            this.userCbx.TabIndex = 7;
            // 
            // userLab
            // 
            this.userLab.AutoSize = true;
            this.userLab.Location = new System.Drawing.Point(191, 15);
            this.userLab.Name = "userLab";
            this.userLab.Size = new System.Drawing.Size(41, 12);
            this.userLab.TabIndex = 8;
            this.userLab.Text = "试验人";
            // 
            // AllTRCheckBox
            // 
            this.AllTRCheckBox.AutoSize = true;
            this.AllTRCheckBox.Location = new System.Drawing.Point(706, 13);
            this.AllTRCheckBox.Margin = new System.Windows.Forms.Padding(2);
            this.AllTRCheckBox.Name = "AllTRCheckBox";
            this.AllTRCheckBox.Size = new System.Drawing.Size(72, 16);
            this.AllTRCheckBox.TabIndex = 12;
            this.AllTRCheckBox.Text = "所有结果";
            this.AllTRCheckBox.UseVisualStyleBackColor = true;
            // 
            // RecordWnd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1053, 554);
            this.Controls.Add(this.AllTRCheckBox);
            this.Controls.Add(this.endDtp);
            this.Controls.Add(this.startDtp);
            this.Controls.Add(this.RecordDetailDgv);
            this.Controls.Add(this.RecordDgv);
            this.Controls.Add(this.seriesNumTbx);
            this.Controls.Add(this.seriesNumLab);
            this.Controls.Add(this.userLab);
            this.Controls.Add(this.productLab);
            this.Controls.Add(this.userCbx);
            this.Controls.Add(this.productCbx);
            this.Controls.Add(this.printBtn);
            this.Controls.Add(this.queryBtn);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "RecordWnd";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "记录查询";
            this.Load += new System.EventHandler(this.RecordWnd_Load);
            ((System.ComponentModel.ISupportInitialize)(this.RecordDgv)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RecordDetailDgv)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button queryBtn;
        private System.Windows.Forms.ComboBox productCbx;
        private System.Windows.Forms.Label productLab;
        private System.Windows.Forms.Label seriesNumLab;
        private System.Windows.Forms.TextBox seriesNumTbx;
        private System.Windows.Forms.DataGridView RecordDgv;
        private System.Windows.Forms.Button printBtn;
        private System.Windows.Forms.DateTimePicker startDtp;
        private System.Windows.Forms.DateTimePicker endDtp;
        private System.Windows.Forms.DataGridView RecordDetailDgv;
        private System.Windows.Forms.ComboBox userCbx;
        private System.Windows.Forms.Label userLab;
        private System.Windows.Forms.DataGridViewTextBoxColumn id2;
        private System.Windows.Forms.DataGridViewTextBoxColumn index;
        private System.Windows.Forms.DataGridViewTextBoxColumn testValue;
        private System.Windows.Forms.DataGridViewTextBoxColumn result1;
        private System.Windows.Forms.DataGridViewTextBoxColumn difference;
        private System.Windows.Forms.DataGridViewTextBoxColumn percent;
        private System.Windows.Forms.DataGridViewTextBoxColumn standard;
        private System.Windows.Forms.DataGridViewTextBoxColumn upper;
        private System.Windows.Forms.DataGridViewTextBoxColumn lower;
        private System.Windows.Forms.DataGridViewCheckBoxColumn check;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
        private System.Windows.Forms.DataGridViewTextBoxColumn seriesNum;
        private System.Windows.Forms.DataGridViewTextBoxColumn proName;
        private System.Windows.Forms.DataGridViewTextBoxColumn checkMode;
        private System.Windows.Forms.DataGridViewTextBoxColumn result;
        private System.Windows.Forms.DataGridViewTextBoxColumn operator1;
        private System.Windows.Forms.DataGridViewTextBoxColumn createTime;
        private System.Windows.Forms.DataGridViewButtonColumn print;
        private System.Windows.Forms.CheckBox AllTRCheckBox;
    }
}
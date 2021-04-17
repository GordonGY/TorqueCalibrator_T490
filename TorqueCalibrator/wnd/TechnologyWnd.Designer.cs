namespace TorqueCalibrator.wnd
{
    partial class TechnologyWnd
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.productLab = new System.Windows.Forms.Label();
            this.testModeLab = new System.Windows.Forms.Label();
            this.techDgv = new System.Windows.Forms.DataGridView();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.index = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.num = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.standard = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.upper = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lower = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.testModeCbx = new System.Windows.Forms.ComboBox();
            this.productCbx = new System.Windows.Forms.ComboBox();
            this.saveBtn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.techDgv)).BeginInit();
            this.SuspendLayout();
            // 
            // productLab
            // 
            this.productLab.AutoSize = true;
            this.productLab.Location = new System.Drawing.Point(15, 14);
            this.productLab.Name = "productLab";
            this.productLab.Size = new System.Drawing.Size(53, 12);
            this.productLab.TabIndex = 2;
            this.productLab.Text = "扳手型号";
            // 
            // testModeLab
            // 
            this.testModeLab.AutoSize = true;
            this.testModeLab.Location = new System.Drawing.Point(229, 15);
            this.testModeLab.Name = "testModeLab";
            this.testModeLab.Size = new System.Drawing.Size(53, 12);
            this.testModeLab.TabIndex = 2;
            this.testModeLab.Text = "测试方式";
            // 
            // techDgv
            // 
            this.techDgv.ColumnHeadersHeight = 40;
            this.techDgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.techDgv.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.id,
            this.index,
            this.num,
            this.standard,
            this.upper,
            this.lower});
            this.techDgv.Cursor = System.Windows.Forms.Cursors.Hand;
            this.techDgv.Location = new System.Drawing.Point(0, 43);
            this.techDgv.Name = "techDgv";
            this.techDgv.RowTemplate.Height = 23;
            this.techDgv.Size = new System.Drawing.Size(565, 266);
            this.techDgv.TabIndex = 3;
            this.techDgv.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.techDgv_CellValueChanged);
            // 
            // id
            // 
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.id.DefaultCellStyle = dataGridViewCellStyle1;
            this.id.HeaderText = "id";
            this.id.Name = "id";
            this.id.Visible = false;
            // 
            // index
            // 
            this.index.HeaderText = "步骤编号";
            this.index.Name = "index";
            this.index.Width = 80;
            // 
            // num
            // 
            this.num.HeaderText = "测试次数";
            this.num.Name = "num";
            this.num.Width = 80;
            // 
            // standard
            // 
            this.standard.HeaderText = "标准值（N·m）";
            this.standard.Name = "standard";
            this.standard.Width = 120;
            // 
            // upper
            // 
            this.upper.HeaderText = "上限值（N·m）";
            this.upper.Name = "upper";
            this.upper.Width = 120;
            // 
            // lower
            // 
            this.lower.HeaderText = "下限值（N·m）";
            this.lower.Name = "lower";
            this.lower.Width = 120;
            // 
            // testModeCbx
            // 
            this.testModeCbx.AutoCompleteCustomSource.AddRange(new string[] {
            "手动模式",
            "自动模式",
            "峰值模式"});
            this.testModeCbx.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.testModeCbx.FormattingEnabled = true;
            this.testModeCbx.Items.AddRange(new object[] {
            "峰值模式",
            "手动模式",
            "咔嗒模式"});
            this.testModeCbx.Location = new System.Drawing.Point(287, 11);
            this.testModeCbx.Name = "testModeCbx";
            this.testModeCbx.Size = new System.Drawing.Size(100, 20);
            this.testModeCbx.TabIndex = 4;
            this.testModeCbx.SelectedIndexChanged += new System.EventHandler(this.testModeCbx_SelectedIndexChanged);
            // 
            // productCbx
            // 
            this.productCbx.FormattingEnabled = true;
            this.productCbx.Location = new System.Drawing.Point(73, 11);
            this.productCbx.Name = "productCbx";
            this.productCbx.Size = new System.Drawing.Size(100, 20);
            this.productCbx.TabIndex = 6;
            this.productCbx.SelectedIndexChanged += new System.EventHandler(this.productCbx_SelectedIndexChanged);
            // 
            // saveBtn
            // 
            this.saveBtn.Enabled = false;
            this.saveBtn.Location = new System.Drawing.Point(453, 11);
            this.saveBtn.Name = "saveBtn";
            this.saveBtn.Size = new System.Drawing.Size(75, 23);
            this.saveBtn.TabIndex = 7;
            this.saveBtn.Text = "保存";
            this.saveBtn.UseVisualStyleBackColor = true;
            this.saveBtn.Click += new System.EventHandler(this.saveBtn_Click);
            // 
            // TechnologyWnd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(565, 309);
            this.Controls.Add(this.saveBtn);
            this.Controls.Add(this.productCbx);
            this.Controls.Add(this.testModeCbx);
            this.Controls.Add(this.techDgv);
            this.Controls.Add(this.testModeLab);
            this.Controls.Add(this.productLab);
            this.Name = "TechnologyWnd";
            this.Text = "TechnologyWnd";
            this.Load += new System.EventHandler(this.TechnologyWnd_Load);
            ((System.ComponentModel.ISupportInitialize)(this.techDgv)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label productLab;
        private System.Windows.Forms.Label testModeLab;
        private System.Windows.Forms.DataGridView techDgv;
        private System.Windows.Forms.ComboBox testModeCbx;
        private System.Windows.Forms.ComboBox productCbx;
        private System.Windows.Forms.Button saveBtn;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
        private System.Windows.Forms.DataGridViewTextBoxColumn index;
        private System.Windows.Forms.DataGridViewTextBoxColumn num;
        private System.Windows.Forms.DataGridViewTextBoxColumn standard;
        private System.Windows.Forms.DataGridViewTextBoxColumn upper;
        private System.Windows.Forms.DataGridViewTextBoxColumn lower;
    }
}
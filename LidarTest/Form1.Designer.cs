namespace LidarTest
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.pointcloudimg = new System.Windows.Forms.PictureBox();
            this.opencom = new System.Windows.Forms.Button();
            this.ComSelect = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.pointcloudimg)).BeginInit();
            this.SuspendLayout();
            // 
            // pointcloudimg
            // 
            this.pointcloudimg.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pointcloudimg.Location = new System.Drawing.Point(12, 12);
            this.pointcloudimg.Name = "pointcloudimg";
            this.pointcloudimg.Size = new System.Drawing.Size(320, 320);
            this.pointcloudimg.TabIndex = 0;
            this.pointcloudimg.TabStop = false;
            // 
            // opencom
            // 
            this.opencom.Location = new System.Drawing.Point(115, 335);
            this.opencom.Name = "opencom";
            this.opencom.Size = new System.Drawing.Size(75, 23);
            this.opencom.TabIndex = 1;
            this.opencom.Text = "打开串口";
            this.opencom.UseVisualStyleBackColor = true;
            this.opencom.Click += new System.EventHandler(this.opencom_Click);
            // 
            // ComSelect
            // 
            this.ComSelect.FormattingEnabled = true;
            this.ComSelect.Location = new System.Drawing.Point(12, 335);
            this.ComSelect.Name = "ComSelect";
            this.ComSelect.Size = new System.Drawing.Size(97, 20);
            this.ComSelect.TabIndex = 2;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(340, 365);
            this.Controls.Add(this.ComSelect);
            this.Controls.Add(this.opencom);
            this.Controls.Add(this.pointcloudimg);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pointcloudimg)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pointcloudimg;
        private System.Windows.Forms.Button opencom;
        private System.Windows.Forms.ComboBox ComSelect;
    }
}


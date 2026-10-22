namespace OMNI写号辅助工具
{
    partial class MainView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainView));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.SetupBtn = new System.Windows.Forms.Button();
            this.RunBtn = new System.Windows.Forms.Button();
            this.Logs = new System.Windows.Forms.RichTextBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.RunBtn, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.SetupBtn, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.Logs, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(14, 12, 14, 12);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(984, 461);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // SetupBtn
            // 
            this.SetupBtn.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.SetupBtn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SetupBtn.Font = new System.Drawing.Font("微软雅黑", 50F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.SetupBtn.Location = new System.Drawing.Point(3, 3);
            this.SetupBtn.Name = "SetupBtn";
            this.SetupBtn.Size = new System.Drawing.Size(978, 94);
            this.SetupBtn.TabIndex = 0;
            this.SetupBtn.Text = "修改配置";
            this.SetupBtn.UseVisualStyleBackColor = false;
            // 
            // RunBtn
            // 
            this.RunBtn.BackColor = System.Drawing.Color.OrangeRed;
            this.RunBtn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RunBtn.Font = new System.Drawing.Font("微软雅黑", 50F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.RunBtn.Location = new System.Drawing.Point(3, 103);
            this.RunBtn.Name = "RunBtn";
            this.RunBtn.Size = new System.Drawing.Size(978, 94);
            this.RunBtn.TabIndex = 2;
            this.RunBtn.Text = "未运行";
            this.RunBtn.UseVisualStyleBackColor = false;
            // 
            // Logs
            // 
            this.Logs.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.Logs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Logs.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Logs.Location = new System.Drawing.Point(3, 203);
            this.Logs.Name = "Logs";
            this.Logs.Size = new System.Drawing.Size(978, 255);
            this.Logs.TabIndex = 3;
            this.Logs.Text = "";
            // 
            // MainView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(24F, 52F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 461);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("微软雅黑", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(14, 12, 14, 12);
            this.MaximizeBox = false;
            this.Name = "MainView";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button SetupBtn;
        private System.Windows.Forms.Button RunBtn;
        private System.Windows.Forms.RichTextBox Logs;
    }
}


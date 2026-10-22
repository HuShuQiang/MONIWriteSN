namespace OMNI写号辅助工具
{
    partial class InputView
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.Info = new System.Windows.Forms.Label();
            this.InputValue = new System.Windows.Forms.TextBox();
            this.Tips = new System.Windows.Forms.Label();
            this.StopBtn = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.Info, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.InputValue, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.Tips, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.StopBtn, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(484, 261);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // Info
            // 
            this.Info.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.Info.AutoSize = true;
            this.Info.Location = new System.Drawing.Point(242, 21);
            this.Info.Name = "Info";
            this.Info.Size = new System.Drawing.Size(0, 27);
            this.Info.TabIndex = 0;
            this.Info.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // InputValue
            // 
            this.InputValue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.InputValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.InputValue.Location = new System.Drawing.Point(3, 88);
            this.InputValue.Name = "InputValue";
            this.InputValue.Size = new System.Drawing.Size(478, 34);
            this.InputValue.TabIndex = 1;
            this.InputValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.InputValue.KeyDown += new System.Windows.Forms.KeyEventHandler(this.InputValue_KeyDown);
            // 
            // Tips
            // 
            this.Tips.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.Tips.AutoSize = true;
            this.Tips.Location = new System.Drawing.Point(242, 212);
            this.Tips.Name = "Tips";
            this.Tips.Size = new System.Drawing.Size(0, 27);
            this.Tips.TabIndex = 2;
            // 
            // StopBtn
            // 
            this.StopBtn.BackColor = System.Drawing.Color.OrangeRed;
            this.StopBtn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.StopBtn.Location = new System.Drawing.Point(3, 143);
            this.StopBtn.Name = "StopBtn";
            this.StopBtn.Size = new System.Drawing.Size(478, 44);
            this.StopBtn.TabIndex = 3;
            this.StopBtn.Text = "停止";
            this.StopBtn.UseVisualStyleBackColor = false;
            this.StopBtn.Click += new System.EventHandler(this.StopBtn_Click);
            // 
            // InputView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 27F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 261);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.Name = "InputView";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "InputView";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label Info;
        private System.Windows.Forms.TextBox InputValue;
        private System.Windows.Forms.Label Tips;
        private System.Windows.Forms.Button StopBtn;
    }
}
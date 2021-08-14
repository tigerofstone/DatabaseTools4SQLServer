namespace Upgrade.Apps
{
    partial class frmCPUNetCardInfo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCPUNetCardInfo));
            this.tbCPUInfo = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnCPUType = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.TBCPUType = new System.Windows.Forms.TextBox();
            this.btnHDInfo = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.tbHDInfo = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbNetCard = new System.Windows.Forms.TextBox();
            this.btnNetCard = new System.Windows.Forms.Button();
            this.btnCPU = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbCPUInfo
            // 
            this.tbCPUInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbCPUInfo.Location = new System.Drawing.Point(93, 66);
            this.tbCPUInfo.Name = "tbCPUInfo";
            this.tbCPUInfo.Size = new System.Drawing.Size(672, 21);
            this.tbCPUInfo.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnCPUType);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.TBCPUType);
            this.groupBox1.Controls.Add(this.btnHDInfo);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.tbHDInfo);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.tbNetCard);
            this.groupBox1.Controls.Add(this.btnNetCard);
            this.groupBox1.Controls.Add(this.btnCPU);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.tbCPUInfo);
            this.groupBox1.Location = new System.Drawing.Point(12, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(782, 493);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            // 
            // btnCPUType
            // 
            this.btnCPUType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCPUType.Location = new System.Drawing.Point(403, 17);
            this.btnCPUType.Name = "btnCPUType";
            this.btnCPUType.Size = new System.Drawing.Size(117, 37);
            this.btnCPUType.TabIndex = 11;
            this.btnCPUType.Text = "获得CPU型号";
            this.btnCPUType.UseVisualStyleBackColor = true;
            this.btnCPUType.Click += new System.EventHandler(this.btnCPUType_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 187);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 10;
            this.label4.Text = "CPU 型号：";
            // 
            // TBCPUType
            // 
            this.TBCPUType.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TBCPUType.Location = new System.Drawing.Point(93, 182);
            this.TBCPUType.Multiline = true;
            this.TBCPUType.Name = "TBCPUType";
            this.TBCPUType.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.TBCPUType.Size = new System.Drawing.Size(672, 305);
            this.TBCPUType.TabIndex = 9;
            // 
            // btnHDInfo
            // 
            this.btnHDInfo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHDInfo.Location = new System.Drawing.Point(280, 17);
            this.btnHDInfo.Name = "btnHDInfo";
            this.btnHDInfo.Size = new System.Drawing.Size(117, 37);
            this.btnHDInfo.TabIndex = 8;
            this.btnHDInfo.Text = "获得硬盘序列号";
            this.btnHDInfo.UseVisualStyleBackColor = true;
            this.btnHDInfo.Click += new System.EventHandler(this.btnHDInfo_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 146);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 12);
            this.label3.TabIndex = 7;
            this.label3.Text = "硬盘序列号：";
            // 
            // tbHDInfo
            // 
            this.tbHDInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbHDInfo.Location = new System.Drawing.Point(93, 141);
            this.tbHDInfo.Name = "tbHDInfo";
            this.tbHDInfo.Size = new System.Drawing.Size(672, 21);
            this.tbHDInfo.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 108);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 5;
            this.label2.Text = "网卡 MAC：";
            // 
            // tbNetCard
            // 
            this.tbNetCard.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbNetCard.Location = new System.Drawing.Point(93, 103);
            this.tbNetCard.Name = "tbNetCard";
            this.tbNetCard.Size = new System.Drawing.Size(672, 21);
            this.tbNetCard.TabIndex = 4;
            // 
            // btnNetCard
            // 
            this.btnNetCard.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNetCard.Location = new System.Drawing.Point(147, 17);
            this.btnNetCard.Name = "btnNetCard";
            this.btnNetCard.Size = new System.Drawing.Size(117, 37);
            this.btnNetCard.TabIndex = 3;
            this.btnNetCard.Text = "获得网卡序列号";
            this.btnNetCard.UseVisualStyleBackColor = true;
            this.btnNetCard.Click += new System.EventHandler(this.btnNetCard_Click);
            // 
            // btnCPU
            // 
            this.btnCPU.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCPU.Location = new System.Drawing.Point(12, 17);
            this.btnCPU.Name = "btnCPU";
            this.btnCPU.Size = new System.Drawing.Size(117, 37);
            this.btnCPU.TabIndex = 2;
            this.btnCPU.Text = "获得CPU序列号";
            this.btnCPU.UseVisualStyleBackColor = true;
            this.btnCPU.Click += new System.EventHandler(this.btnCPU_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 71);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "CPU 序列号：";
            // 
            // frmCPUNetCardInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(813, 508);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmCPUNetCardInfo";
            this.Text = "计算机主要标志信息";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox tbCPUInfo;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbNetCard;
        private System.Windows.Forms.Button btnNetCard;
        private System.Windows.Forms.Button btnCPU;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbHDInfo;
        private System.Windows.Forms.Button btnHDInfo;
        private System.Windows.Forms.Button btnCPUType;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox TBCPUType;
    }
}
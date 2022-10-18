using System.Windows.Forms;

namespace DiskMonitoring_DeskTop
{
    partial class DiskMonitoring
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.MemoryChangesLabel = new System.Windows.Forms.Label();
            this.logMemoryChanges = new System.Windows.Forms.CheckBox();
            this.LogFileChanges = new System.Windows.Forms.CheckBox();
            this.FileChangesBtn = new System.Windows.Forms.Button();
            this.MemoryChangesBtn = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.FileChangesLable = new System.Windows.Forms.Label();
            this.currentPathUsedLabel = new System.Windows.Forms.Label();
            this.flowLayoutPanel3 = new System.Windows.Forms.FlowLayoutPanel();
            this.flowLayoutPanel2.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // MemoryChangesLabel
            // 
            this.MemoryChangesLabel.AutoSize = true;
            this.MemoryChangesLabel.Font = new System.Drawing.Font("Microsoft YaHei", 12F);
            this.MemoryChangesLabel.Location = new System.Drawing.Point(3, 0);
            this.MemoryChangesLabel.Name = "MemoryChangesLabel";
            this.MemoryChangesLabel.Size = new System.Drawing.Size(120, 27);
            this.MemoryChangesLabel.TabIndex = 1;
            this.MemoryChangesLabel.Text = "No Info yet";
            // 
            // logMemoryChanges
            // 
            this.logMemoryChanges.AutoSize = true;
            this.logMemoryChanges.Location = new System.Drawing.Point(1003, 67);
            this.logMemoryChanges.Name = "logMemoryChanges";
            this.logMemoryChanges.Size = new System.Drawing.Size(176, 20);
            this.logMemoryChanges.TabIndex = 2;
            this.logMemoryChanges.Text = "log disk memory change";
            this.logMemoryChanges.UseVisualStyleBackColor = true;
            // 
            // LogFileChanges
            // 
            this.LogFileChanges.AutoSize = true;
            this.LogFileChanges.Location = new System.Drawing.Point(12, 67);
            this.LogFileChanges.Name = "LogFileChanges";
            this.LogFileChanges.Size = new System.Drawing.Size(123, 20);
            this.LogFileChanges.TabIndex = 3;
            this.LogFileChanges.Text = "log file changes";
            this.LogFileChanges.UseVisualStyleBackColor = true;
            // 
            // FileChangesBtn
            // 
            this.FileChangesBtn.Location = new System.Drawing.Point(12, 119);
            this.FileChangesBtn.Name = "FileChangesBtn";
            this.FileChangesBtn.Size = new System.Drawing.Size(141, 23);
            this.FileChangesBtn.TabIndex = 5;
            this.FileChangesBtn.Text = "Start Monitoring";
            this.FileChangesBtn.UseVisualStyleBackColor = true;
            this.FileChangesBtn.Click += new System.EventHandler(this.FileChangesBtn_Click);
            // 
            // MemoryChangesBtn
            // 
            this.MemoryChangesBtn.Location = new System.Drawing.Point(1038, 119);
            this.MemoryChangesBtn.Name = "MemoryChangesBtn";
            this.MemoryChangesBtn.Size = new System.Drawing.Size(141, 23);
            this.MemoryChangesBtn.TabIndex = 6;
            this.MemoryChangesBtn.Text = "Start Monitoring";
            this.MemoryChangesBtn.UseVisualStyleBackColor = true;
            this.MemoryChangesBtn.Click += new System.EventHandler(this.MemoryChangesBtn_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("MS Reference Sans Serif", 13.8F);
            this.label3.Location = new System.Drawing.Point(15, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(151, 28);
            this.label3.TabIndex = 7;
            this.label3.Text = "FileChanges";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("MS Reference Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(1074, 12);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(105, 28);
            this.label4.TabIndex = 8;
            this.label4.Text = "Memory";
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.AutoScroll = true;
            this.flowLayoutPanel2.Controls.Add(this.MemoryChangesLabel);
            this.flowLayoutPanel2.Location = new System.Drawing.Point(584, 148);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(595, 301);
            this.flowLayoutPanel2.TabIndex = 10;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoScroll = true;
            this.flowLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel1.Controls.Add(this.FileChangesLable);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(12, 148);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(565, 301);
            this.flowLayoutPanel1.TabIndex = 11;
            // 
            // FileChangesLable
            // 
            this.FileChangesLable.AutoSize = true;
            this.FileChangesLable.Font = new System.Drawing.Font("Microsoft YaHei", 12F);
            this.FileChangesLable.Location = new System.Drawing.Point(3, 0);
            this.FileChangesLable.Name = "FileChangesLable";
            this.FileChangesLable.Size = new System.Drawing.Size(120, 27);
            this.FileChangesLable.TabIndex = 1;
            this.FileChangesLable.Text = "No Info yet";
            // 
            // currentPathUsedLabel
            // 
            this.currentPathUsedLabel.AutoSize = true;
            this.currentPathUsedLabel.Font = new System.Drawing.Font("MS Reference Sans Serif", 13.8F);
            this.currentPathUsedLabel.Location = new System.Drawing.Point(3, 0);
            this.currentPathUsedLabel.Name = "currentPathUsedLabel";
            this.currentPathUsedLabel.Size = new System.Drawing.Size(302, 28);
            this.currentPathUsedLabel.TabIndex = 12;
            this.currentPathUsedLabel.Text = "Current path used: None";
            this.currentPathUsedLabel.Click += new System.EventHandler(this.currentPathUsedLabel_Click);
            // 
            // flowLayoutPanel3
            // 
            this.flowLayoutPanel3.AutoScroll = true;
            this.flowLayoutPanel3.Controls.Add(this.currentPathUsedLabel);
            this.flowLayoutPanel3.Location = new System.Drawing.Point(237, 12);
            this.flowLayoutPanel3.Name = "flowLayoutPanel3";
            this.flowLayoutPanel3.Size = new System.Drawing.Size(735, 120);
            this.flowLayoutPanel3.TabIndex = 13;
            // 
            // DiskMonitoring
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(1191, 502);
            this.Controls.Add(this.flowLayoutPanel3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.MemoryChangesBtn);
            this.Controls.Add(this.FileChangesBtn);
            this.Controls.Add(this.LogFileChanges);
            this.Controls.Add(this.logMemoryChanges);
            this.Controls.Add(this.flowLayoutPanel2);
            this.Name = "DiskMonitoring";
            this.Text = "DiskMonitoring";
            this.Closed += DiskMonitoring_Closed;
            this.flowLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel2.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.flowLayoutPanel3.ResumeLayout(false);
            this.flowLayoutPanel3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }



        #endregion


        private System.Windows.Forms.Label MemoryChangesLabel;
        private System.Windows.Forms.CheckBox logMemoryChanges;
        private System.Windows.Forms.CheckBox LogFileChanges;
        private System.Windows.Forms.Button FileChangesBtn;
        private System.Windows.Forms.Button MemoryChangesBtn;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private FlowLayoutPanel flowLayoutPanel2;
        private FlowLayoutPanel flowLayoutPanel1;
        private Label FileChangesLable;
        private Label currentPathUsedLabel;
        private FlowLayoutPanel flowLayoutPanel3;
    }
}


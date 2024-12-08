namespace DOAN_BMCSDL
{
    partial class Unlock_ChangePass
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtUser = new System.Windows.Forms.TextBox();
            this.txtPass = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnNewPass = new System.Windows.Forms.Button();
            this.btn_unLock = new System.Windows.Forms.Button();
            this.txtQuota = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.cbTable_space = new System.Windows.Forms.Label();
            this.btn_Quota_update = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(59, 50);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "User";
            // 
            // txtUser
            // 
            this.txtUser.Location = new System.Drawing.Point(165, 50);
            this.txtUser.Multiline = true;
            this.txtUser.Name = "txtUser";
            this.txtUser.Size = new System.Drawing.Size(241, 32);
            this.txtUser.TabIndex = 1;
            // 
            // txtPass
            // 
            this.txtPass.Location = new System.Drawing.Point(165, 101);
            this.txtPass.Multiline = true;
            this.txtPass.Name = "txtPass";
            this.txtPass.Size = new System.Drawing.Size(241, 32);
            this.txtPass.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(59, 101);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 16);
            this.label2.TabIndex = 2;
            this.label2.Text = "Password";
            // 
            // btnNewPass
            // 
            this.btnNewPass.Location = new System.Drawing.Point(173, 307);
            this.btnNewPass.Name = "btnNewPass";
            this.btnNewPass.Size = new System.Drawing.Size(74, 28);
            this.btnNewPass.TabIndex = 6;
            this.btnNewPass.Text = "Đổi MK";
            this.btnNewPass.UseVisualStyleBackColor = true;
            this.btnNewPass.Click += new System.EventHandler(this.btnNewPass_Click_1);
            // 
            // btn_unLock
            // 
            this.btn_unLock.Location = new System.Drawing.Point(253, 307);
            this.btn_unLock.Name = "btn_unLock";
            this.btn_unLock.Size = new System.Drawing.Size(74, 28);
            this.btn_unLock.TabIndex = 7;
            this.btn_unLock.Text = "Mở khóa";
            this.btn_unLock.UseVisualStyleBackColor = true;
            this.btn_unLock.Click += new System.EventHandler(this.btn_unLock_Click);
            // 
            // txtQuota
            // 
            this.txtQuota.Location = new System.Drawing.Point(165, 156);
            this.txtQuota.Multiline = true;
            this.txtQuota.Name = "txtQuota";
            this.txtQuota.Size = new System.Drawing.Size(241, 32);
            this.txtQuota.TabIndex = 9;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(59, 156);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 16);
            this.label3.TabIndex = 8;
            this.label3.Text = "Quota";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(165, 228);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(241, 24);
            this.comboBox1.TabIndex = 10;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // cbTable_space
            // 
            this.cbTable_space.AutoSize = true;
            this.cbTable_space.Location = new System.Drawing.Point(59, 228);
            this.cbTable_space.Name = "cbTable_space";
            this.cbTable_space.Size = new System.Drawing.Size(81, 16);
            this.cbTable_space.TabIndex = 11;
            this.cbTable_space.Text = "Tablespace";
            // 
            // btn_Quota_update
            // 
            this.btn_Quota_update.Location = new System.Drawing.Point(333, 307);
            this.btn_Quota_update.Name = "btn_Quota_update";
            this.btn_Quota_update.Size = new System.Drawing.Size(74, 28);
            this.btn_Quota_update.TabIndex = 12;
            this.btn_Quota_update.Text = "Grant";
            this.btn_Quota_update.UseVisualStyleBackColor = true;
            this.btn_Quota_update.Click += new System.EventHandler(this.btn_Quota_update_Click);
            // 
            // Unlock_ChangePass
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(480, 347);
            this.Controls.Add(this.btn_Quota_update);
            this.Controls.Add(this.cbTable_space);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.txtQuota);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btn_unLock);
            this.Controls.Add(this.btnNewPass);
            this.Controls.Add(this.txtPass);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtUser);
            this.Controls.Add(this.label1);
            this.Name = "Unlock_ChangePass";
            this.Text = "User_Acc";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtUser;
        private System.Windows.Forms.TextBox txtPass;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnNewPass;
        private System.Windows.Forms.Button btn_unLock;
        private System.Windows.Forms.TextBox txtQuota;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label cbTable_space;
        private System.Windows.Forms.Button btn_Quota_update;
    }
}
namespace DOAN_BMCSDL
{
    partial class Admin_Privilege
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
            this.cbTable_space = new System.Windows.Forms.ComboBox();
            this.txtUser = new System.Windows.Forms.TextBox();
            this.txtPass = new System.Windows.Forms.TextBox();
            this.cbProfile = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.cbUser_admin = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cbPrivilege = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.cbClient = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.cbGroup = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.cbTable = new System.Windows.Forms.ComboBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this.checkBox4 = new System.Windows.Forms.CheckBox();
            this.btnGrant = new System.Windows.Forms.Button();
            this.btnRevoke = new System.Windows.Forms.Button();
            this.btnUnlock = new System.Windows.Forms.Button();
            this.btnNewPass = new System.Windows.Forms.Button();
            this.txtQuota = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // cbTable_space
            // 
            this.cbTable_space.FormattingEnabled = true;
            this.cbTable_space.Location = new System.Drawing.Point(207, 332);
            this.cbTable_space.Name = "cbTable_space";
            this.cbTable_space.Size = new System.Drawing.Size(185, 24);
            this.cbTable_space.TabIndex = 0;
            this.cbTable_space.SelectedIndexChanged += new System.EventHandler(this.cbTable_space_SelectedIndexChanged);
            // 
            // txtUser
            // 
            this.txtUser.Location = new System.Drawing.Point(207, 187);
            this.txtUser.Multiline = true;
            this.txtUser.Name = "txtUser";
            this.txtUser.Size = new System.Drawing.Size(185, 25);
            this.txtUser.TabIndex = 1;
            // 
            // txtPass
            // 
            this.txtPass.Location = new System.Drawing.Point(207, 236);
            this.txtPass.Multiline = true;
            this.txtPass.Name = "txtPass";
            this.txtPass.Size = new System.Drawing.Size(185, 25);
            this.txtPass.TabIndex = 2;
            // 
            // cbProfile
            // 
            this.cbProfile.FormattingEnabled = true;
            this.cbProfile.Location = new System.Drawing.Point(207, 378);
            this.cbProfile.Name = "cbProfile";
            this.cbProfile.Size = new System.Drawing.Size(185, 24);
            this.cbProfile.TabIndex = 4;
            this.cbProfile.SelectedIndexChanged += new System.EventHandler(this.CbProfile_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(32, 183);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 25);
            this.label1.TabIndex = 5;
            this.label1.Text = "User : ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(32, 231);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(125, 25);
            this.label2.TabIndex = 6;
            this.label2.Text = "Password : ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(32, 328);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(150, 25);
            this.label3.TabIndex = 7;
            this.label3.Text = "Table space : ";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(32, 423);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(90, 25);
            this.label4.TabIndex = 8;
            this.label4.Text = "Quota : ";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(32, 374);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(92, 25);
            this.label5.TabIndex = 9;
            this.label5.Text = "Profile : ";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(422, 183);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(76, 25);
            this.label6.TabIndex = 11;
            this.label6.Text = "User : ";
            // 
            // cbUser_admin
            // 
            this.cbUser_admin.FormattingEnabled = true;
            this.cbUser_admin.Location = new System.Drawing.Point(580, 183);
            this.cbUser_admin.Name = "cbUser_admin";
            this.cbUser_admin.Size = new System.Drawing.Size(185, 24);
            this.cbUser_admin.TabIndex = 10;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(422, 231);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(125, 25);
            this.label7.TabIndex = 13;
            this.label7.Text = "Privileges : ";
            // 
            // cbPrivilege
            // 
            this.cbPrivilege.FormattingEnabled = true;
            this.cbPrivilege.Location = new System.Drawing.Point(580, 232);
            this.cbPrivilege.Name = "cbPrivilege";
            this.cbPrivilege.Size = new System.Drawing.Size(185, 24);
            this.cbPrivilege.TabIndex = 12;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(422, 329);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(122, 25);
            this.label8.TabIndex = 15;
            this.label8.Text = "Gán User : ";
            // 
            // cbClient
            // 
            this.cbClient.FormattingEnabled = true;
            this.cbClient.Location = new System.Drawing.Point(580, 329);
            this.cbClient.Name = "cbClient";
            this.cbClient.Size = new System.Drawing.Size(185, 24);
            this.cbClient.TabIndex = 14;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(422, 374);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(152, 25);
            this.label9.TabIndex = 17;
            this.label9.Text = "Nhóm quyền : ";
            // 
            // cbGroup
            // 
            this.cbGroup.FormattingEnabled = true;
            this.cbGroup.Location = new System.Drawing.Point(580, 374);
            this.cbGroup.Name = "cbGroup";
            this.cbGroup.Size = new System.Drawing.Size(185, 24);
            this.cbGroup.TabIndex = 16;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(797, 232);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(86, 25);
            this.label11.TabIndex = 21;
            this.label11.Text = "Table : ";
            // 
            // cbTable
            // 
            this.cbTable.FormattingEnabled = true;
            this.cbTable.Location = new System.Drawing.Point(917, 231);
            this.cbTable.Name = "cbTable";
            this.cbTable.Size = new System.Drawing.Size(185, 24);
            this.cbTable.TabIndex = 20;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBox1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.checkBox1.ImageAlign = System.Drawing.ContentAlignment.TopRight;
            this.checkBox1.Location = new System.Drawing.Point(815, 334);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(78, 24);
            this.checkBox1.TabIndex = 22;
            this.checkBox1.Text = "Select";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBox2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.checkBox2.ImageAlign = System.Drawing.ContentAlignment.TopRight;
            this.checkBox2.Location = new System.Drawing.Point(917, 334);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(73, 24);
            this.checkBox2.TabIndex = 23;
            this.checkBox2.Text = "Insert";
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // checkBox3
            // 
            this.checkBox3.AutoSize = true;
            this.checkBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBox3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.checkBox3.ImageAlign = System.Drawing.ContentAlignment.TopRight;
            this.checkBox3.Location = new System.Drawing.Point(815, 377);
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.Size = new System.Drawing.Size(84, 24);
            this.checkBox3.TabIndex = 24;
            this.checkBox3.Text = "Update";
            this.checkBox3.UseVisualStyleBackColor = true;
            // 
            // checkBox4
            // 
            this.checkBox4.AutoSize = true;
            this.checkBox4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBox4.ForeColor = System.Drawing.SystemColors.ControlText;
            this.checkBox4.ImageAlign = System.Drawing.ContentAlignment.TopRight;
            this.checkBox4.Location = new System.Drawing.Point(917, 375);
            this.checkBox4.Name = "checkBox4";
            this.checkBox4.Size = new System.Drawing.Size(80, 24);
            this.checkBox4.TabIndex = 25;
            this.checkBox4.Text = "Delete";
            this.checkBox4.UseVisualStyleBackColor = true;
            // 
            // btnGrant
            // 
            this.btnGrant.Location = new System.Drawing.Point(188, 499);
            this.btnGrant.Name = "btnGrant";
            this.btnGrant.Size = new System.Drawing.Size(158, 65);
            this.btnGrant.TabIndex = 26;
            this.btnGrant.Text = "Cấp quyền";
            this.btnGrant.UseVisualStyleBackColor = true;
            this.btnGrant.Click += new System.EventHandler(this.btnGrant_Click);
            // 
            // btnRevoke
            // 
            this.btnRevoke.Location = new System.Drawing.Point(386, 499);
            this.btnRevoke.Name = "btnRevoke";
            this.btnRevoke.Size = new System.Drawing.Size(158, 65);
            this.btnRevoke.TabIndex = 27;
            this.btnRevoke.Text = "Thu hồi";
            this.btnRevoke.UseVisualStyleBackColor = true;
            // 
            // btnUnlock
            // 
            this.btnUnlock.Location = new System.Drawing.Point(580, 499);
            this.btnUnlock.Name = "btnUnlock";
            this.btnUnlock.Size = new System.Drawing.Size(158, 65);
            this.btnUnlock.TabIndex = 28;
            this.btnUnlock.Text = "Mở khóa";
            this.btnUnlock.UseVisualStyleBackColor = true;
            this.btnUnlock.Click += new System.EventHandler(this.btnUnlock_Click);
            // 
            // btnNewPass
            // 
            this.btnNewPass.Location = new System.Drawing.Point(772, 499);
            this.btnNewPass.Name = "btnNewPass";
            this.btnNewPass.Size = new System.Drawing.Size(158, 65);
            this.btnNewPass.TabIndex = 29;
            this.btnNewPass.Text = "Đổi MK";
            this.btnNewPass.UseVisualStyleBackColor = true;
            this.btnNewPass.Click += new System.EventHandler(this.btnNewPass_Click);
            // 
            // txtQuota
            // 
            this.txtQuota.Location = new System.Drawing.Point(207, 423);
            this.txtQuota.Multiline = true;
            this.txtQuota.Name = "txtQuota";
            this.txtQuota.Size = new System.Drawing.Size(185, 25);
            this.txtQuota.TabIndex = 30;
            // 
            // Admin_Privilege
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1192, 704);
            this.Controls.Add(this.txtQuota);
            this.Controls.Add(this.btnNewPass);
            this.Controls.Add(this.btnUnlock);
            this.Controls.Add(this.btnRevoke);
            this.Controls.Add(this.btnGrant);
            this.Controls.Add(this.checkBox4);
            this.Controls.Add(this.checkBox3);
            this.Controls.Add(this.checkBox2);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.cbTable);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.cbGroup);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.cbClient);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.cbPrivilege);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.cbUser_admin);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbProfile);
            this.Controls.Add(this.txtPass);
            this.Controls.Add(this.txtUser);
            this.Controls.Add(this.cbTable_space);
            this.Name = "Admin_Privilege";
            this.Text = "Admin_Privilege";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbTable_space;
        private System.Windows.Forms.TextBox txtUser;
        private System.Windows.Forms.TextBox txtPass;
        private System.Windows.Forms.ComboBox cbProfile;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cbUser_admin;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cbPrivilege;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cbClient;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cbGroup;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox cbTable;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.CheckBox checkBox3;
        private System.Windows.Forms.CheckBox checkBox4;
        private System.Windows.Forms.Button btnGrant;
        private System.Windows.Forms.Button btnRevoke;
        private System.Windows.Forms.Button btnUnlock;
        private System.Windows.Forms.Button btnNewPass;
        private System.Windows.Forms.TextBox txtQuota;
    }
}
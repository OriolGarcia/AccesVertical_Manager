namespace AccesVertical_Manager
{
    partial class PasswordChangeForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PasswordChangeForm));
            this.lbNickName = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtBOldPassword = new System.Windows.Forms.TextBox();
            this.txtBNewPassword1 = new System.Windows.Forms.TextBox();
            this.txtBNewPassword2 = new System.Windows.Forms.TextBox();
            this.btCanviarPassword = new System.Windows.Forms.Button();
            this.btAnular = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lbNickName
            // 
            this.lbNickName.AutoSize = true;
            this.lbNickName.Location = new System.Drawing.Point(88, 37);
            this.lbNickName.Name = "lbNickName";
            this.lbNickName.Size = new System.Drawing.Size(60, 13);
            this.lbNickName.TabIndex = 2;
            this.lbNickName.Text = "NickName:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(57, 65);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Password antic:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(63, 92);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Nou Password:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 118);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(133, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Repeteix el nou Password:";
            // 
            // txtBOldPassword
            // 
            this.txtBOldPassword.Location = new System.Drawing.Point(145, 62);
            this.txtBOldPassword.Name = "txtBOldPassword";
            this.txtBOldPassword.PasswordChar = '*';
            this.txtBOldPassword.Size = new System.Drawing.Size(100, 20);
            this.txtBOldPassword.TabIndex = 6;
            // 
            // txtBNewPassword1
            // 
            this.txtBNewPassword1.Location = new System.Drawing.Point(145, 89);
            this.txtBNewPassword1.Name = "txtBNewPassword1";
            this.txtBNewPassword1.PasswordChar = '*';
            this.txtBNewPassword1.Size = new System.Drawing.Size(100, 20);
            this.txtBNewPassword1.TabIndex = 7;
            // 
            // txtBNewPassword2
            // 
            this.txtBNewPassword2.Location = new System.Drawing.Point(145, 115);
            this.txtBNewPassword2.Name = "txtBNewPassword2";
            this.txtBNewPassword2.PasswordChar = '*';
            this.txtBNewPassword2.Size = new System.Drawing.Size(100, 20);
            this.txtBNewPassword2.TabIndex = 8;
            // 
            // btCanviarPassword
            // 
            this.btCanviarPassword.Location = new System.Drawing.Point(117, 153);
            this.btCanviarPassword.Name = "btCanviarPassword";
            this.btCanviarPassword.Size = new System.Drawing.Size(103, 23);
            this.btCanviarPassword.TabIndex = 9;
            this.btCanviarPassword.Text = "Canviar Password";
            this.btCanviarPassword.UseVisualStyleBackColor = true;
            this.btCanviarPassword.Click += new System.EventHandler(this.btCanviarPassword_Click);
            // 
            // btAnular
            // 
            this.btAnular.Location = new System.Drawing.Point(226, 153);
            this.btAnular.Name = "btAnular";
            this.btAnular.Size = new System.Drawing.Size(75, 23);
            this.btAnular.TabIndex = 10;
            this.btAnular.Text = "Anul·lar";
            this.btAnular.UseVisualStyleBackColor = true;
            this.btAnular.Click += new System.EventHandler(this.btAnular_Click);
            // 
            // PasswordChangeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(354, 203);
            this.Controls.Add(this.btAnular);
            this.Controls.Add(this.btCanviarPassword);
            this.Controls.Add(this.txtBNewPassword2);
            this.Controls.Add(this.txtBNewPassword1);
            this.Controls.Add(this.txtBOldPassword);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lbNickName);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "PasswordChangeForm";
            this.Text = "AccésVertical Manager - Canviar Password";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbNickName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtBOldPassword;
        private System.Windows.Forms.TextBox txtBNewPassword1;
        private System.Windows.Forms.TextBox txtBNewPassword2;
        private System.Windows.Forms.Button btCanviarPassword;
        private System.Windows.Forms.Button btAnular;
    }
}
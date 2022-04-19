namespace AccesVertical_Manager
{
    partial class AddUserForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddUserForm));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtBNickName = new System.Windows.Forms.TextBox();
            this.txtBPassword = new System.Windows.Forms.TextBox();
            this.checkBPermissions = new System.Windows.Forms.CheckBox();
            this.checkBActive = new System.Windows.Forms.CheckBox();
            this.button1 = new System.Windows.Forms.Button();
            this.checkManagerPermissions = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 59);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "NickName:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(196, 59);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Password inicial:";
            // 
            // txtBNickName
            // 
            this.txtBNickName.Location = new System.Drawing.Point(90, 56);
            this.txtBNickName.Name = "txtBNickName";
            this.txtBNickName.Size = new System.Drawing.Size(100, 20);
            this.txtBNickName.TabIndex = 2;
            // 
            // txtBPassword
            // 
            this.txtBPassword.Location = new System.Drawing.Point(284, 56);
            this.txtBPassword.Name = "txtBPassword";
            this.txtBPassword.PasswordChar = '*';
            this.txtBPassword.Size = new System.Drawing.Size(100, 20);
            this.txtBPassword.TabIndex = 3;
            // 
            // checkBPermissions
            // 
            this.checkBPermissions.AutoSize = true;
            this.checkBPermissions.Location = new System.Drawing.Point(199, 142);
            this.checkBPermissions.Name = "checkBPermissions";
            this.checkBPermissions.Size = new System.Drawing.Size(134, 17);
            this.checkBPermissions.TabIndex = 4;
            this.checkBPermissions.Text = "Permisos de supervisor";
            this.checkBPermissions.UseVisualStyleBackColor = true;
            this.checkBPermissions.CheckedChanged += new System.EventHandler(this.checkBPermissions_CheckedChanged);
            // 
            // checkBActive
            // 
            this.checkBActive.AutoSize = true;
            this.checkBActive.Location = new System.Drawing.Point(199, 96);
            this.checkBActive.Name = "checkBActive";
            this.checkBActive.Size = new System.Drawing.Size(82, 17);
            this.checkBActive.TabIndex = 5;
            this.checkBActive.Text = "Usuari actiu";
            this.checkBActive.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(171, 172);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(147, 23);
            this.button1.TabIndex = 6;
            this.button1.Text = "Inserir Usuari";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // checkManagerPermissions
            // 
            this.checkManagerPermissions.AutoSize = true;
            this.checkManagerPermissions.Location = new System.Drawing.Point(199, 119);
            this.checkManagerPermissions.Name = "checkManagerPermissions";
            this.checkManagerPermissions.Size = new System.Drawing.Size(114, 17);
            this.checkManagerPermissions.TabIndex = 7;
            this.checkManagerPermissions.Text = "Permisos de gestió";
            this.checkManagerPermissions.UseVisualStyleBackColor = true;
            this.checkManagerPermissions.CheckedChanged += new System.EventHandler(this.checkManagerPermissions_CheckedChanged);
            // 
            // AddUserForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(500, 207);
            this.Controls.Add(this.checkManagerPermissions);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.checkBActive);
            this.Controls.Add(this.checkBPermissions);
            this.Controls.Add(this.txtBPassword);
            this.Controls.Add(this.txtBNickName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AddUserForm";
            this.Text = "AccésVertical Manager - Afegir Usuari";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtBNickName;
        private System.Windows.Forms.TextBox txtBPassword;
        private System.Windows.Forms.CheckBox checkBPermissions;
        private System.Windows.Forms.CheckBox checkBActive;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.CheckBox checkManagerPermissions;
    }
}
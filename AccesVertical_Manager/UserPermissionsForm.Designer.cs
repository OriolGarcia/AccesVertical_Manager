namespace AccesVertical_Manager
{
    partial class UserPermissionsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UserPermissionsForm));
            this.lbNickname = new System.Windows.Forms.Label();
            this.checkBActive = new System.Windows.Forms.CheckBox();
            this.checkBPermissions = new System.Windows.Forms.CheckBox();
            this.BtAcceptar = new System.Windows.Forms.Button();
            this.btAnular = new System.Windows.Forms.Button();
            this.checkManagerPermissions = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // lbNickname
            // 
            this.lbNickname.AutoSize = true;
            this.lbNickname.Location = new System.Drawing.Point(49, 29);
            this.lbNickname.Name = "lbNickname";
            this.lbNickname.Size = new System.Drawing.Size(58, 13);
            this.lbNickname.TabIndex = 0;
            this.lbNickname.Text = "Nickname:";
            // 
            // checkBActive
            // 
            this.checkBActive.AutoSize = true;
            this.checkBActive.Location = new System.Drawing.Point(116, 46);
            this.checkBActive.Name = "checkBActive";
            this.checkBActive.Size = new System.Drawing.Size(50, 17);
            this.checkBActive.TabIndex = 1;
            this.checkBActive.Text = "Actiu";
            this.checkBActive.UseVisualStyleBackColor = true;
            // 
            // checkBPermissions
            // 
            this.checkBPermissions.AutoSize = true;
            this.checkBPermissions.Location = new System.Drawing.Point(116, 95);
            this.checkBPermissions.Name = "checkBPermissions";
            this.checkBPermissions.Size = new System.Drawing.Size(134, 17);
            this.checkBPermissions.TabIndex = 2;
            this.checkBPermissions.Text = "Permisos de supervisor";
            this.checkBPermissions.UseVisualStyleBackColor = true;
            this.checkBPermissions.CheckedChanged += new System.EventHandler(this.checkBPermissions_CheckedChanged);
            // 
            // BtAcceptar
            // 
            this.BtAcceptar.Location = new System.Drawing.Point(103, 118);
            this.BtAcceptar.Name = "BtAcceptar";
            this.BtAcceptar.Size = new System.Drawing.Size(75, 23);
            this.BtAcceptar.TabIndex = 3;
            this.BtAcceptar.Text = "Acceptar";
            this.BtAcceptar.UseVisualStyleBackColor = true;
            this.BtAcceptar.Click += new System.EventHandler(this.BtAcceptar_Click);
            // 
            // btAnular
            // 
            this.btAnular.Location = new System.Drawing.Point(185, 118);
            this.btAnular.Name = "btAnular";
            this.btAnular.Size = new System.Drawing.Size(75, 23);
            this.btAnular.TabIndex = 4;
            this.btAnular.Text = "Anul·lar";
            this.btAnular.UseVisualStyleBackColor = true;
            // 
            // checkManagerPermissions
            // 
            this.checkManagerPermissions.AutoSize = true;
            this.checkManagerPermissions.Location = new System.Drawing.Point(116, 69);
            this.checkManagerPermissions.Name = "checkManagerPermissions";
            this.checkManagerPermissions.Size = new System.Drawing.Size(114, 17);
            this.checkManagerPermissions.TabIndex = 5;
            this.checkManagerPermissions.Text = "Permisos de gestió";
            this.checkManagerPermissions.UseVisualStyleBackColor = true;
            this.checkManagerPermissions.CheckedChanged += new System.EventHandler(this.checkManagerPermissions_CheckedChanged);
            // 
            // UserPermissionsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(334, 161);
            this.Controls.Add(this.checkManagerPermissions);
            this.Controls.Add(this.btAnular);
            this.Controls.Add(this.BtAcceptar);
            this.Controls.Add(this.checkBPermissions);
            this.Controls.Add(this.checkBActive);
            this.Controls.Add(this.lbNickname);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "UserPermissionsForm";
            this.Text = "AccésVertical Manager - Permisos d\'usuari";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbNickname;
        private System.Windows.Forms.CheckBox checkBActive;
        private System.Windows.Forms.CheckBox checkBPermissions;
        private System.Windows.Forms.Button BtAcceptar;
        private System.Windows.Forms.Button btAnular;
        private System.Windows.Forms.CheckBox checkManagerPermissions;
    }
}
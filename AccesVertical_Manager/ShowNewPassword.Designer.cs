namespace AccesVertical_Manager
{
    partial class ShowNewPassword
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ShowNewPassword));
            this.lbNotificacio = new System.Windows.Forms.Label();
            this.lbNickName = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.btCopy = new System.Windows.Forms.Button();
            this.btAcceptar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lbNotificacio
            // 
            this.lbNotificacio.AutoSize = true;
            this.lbNotificacio.Location = new System.Drawing.Point(44, 26);
            this.lbNotificacio.Name = "lbNotificacio";
            this.lbNotificacio.Size = new System.Drawing.Size(117, 13);
            this.lbNotificacio.TabIndex = 0;
            this.lbNotificacio.Text = "Nou password generat!";
            // 
            // lbNickName
            // 
            this.lbNickName.AutoSize = true;
            this.lbNickName.Location = new System.Drawing.Point(59, 50);
            this.lbNickName.Name = "lbNickName";
            this.lbNickName.Size = new System.Drawing.Size(60, 13);
            this.lbNickName.TabIndex = 1;
            this.lbNickName.Text = "NickName:";
            this.lbNickName.Click += new System.EventHandler(this.lbNickName_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(59, 78);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Nou Password:";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(144, 75);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(88, 20);
            this.textBox1.TabIndex = 3;
            // 
            // btCopy
            // 
            this.btCopy.Location = new System.Drawing.Point(251, 64);
            this.btCopy.Name = "btCopy";
            this.btCopy.Size = new System.Drawing.Size(102, 40);
            this.btCopy.TabIndex = 4;
            this.btCopy.Text = "Copiar al Portapapers";
            this.btCopy.UseVisualStyleBackColor = true;
            this.btCopy.Click += new System.EventHandler(this.btCopy_Click);
            // 
            // btAcceptar
            // 
            this.btAcceptar.Location = new System.Drawing.Point(144, 117);
            this.btAcceptar.Name = "btAcceptar";
            this.btAcceptar.Size = new System.Drawing.Size(75, 23);
            this.btAcceptar.TabIndex = 5;
            this.btAcceptar.Text = "Acceptar";
            this.btAcceptar.UseVisualStyleBackColor = true;
            this.btAcceptar.Click += new System.EventHandler(this.btAcceptar_Click);
            // 
            // ShowNewPassword
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(365, 152);
            this.Controls.Add(this.btAcceptar);
            this.Controls.Add(this.btCopy);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lbNickName);
            this.Controls.Add(this.lbNotificacio);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ShowNewPassword";
            this.Text = "AccésVertical Manager - Nou Password";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbNotificacio;
        private System.Windows.Forms.Label lbNickName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button btCopy;
        private System.Windows.Forms.Button btAcceptar;
    }
}
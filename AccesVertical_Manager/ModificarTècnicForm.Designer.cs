namespace AccesVertical_Manager
{
    partial class ModificarTècnicForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ModificarTècnicForm));
            this.btInserir = new System.Windows.Forms.Button();
            this.txtBNomTecnic = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btAnular = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btInserir
            // 
            this.btInserir.Location = new System.Drawing.Point(190, 107);
            this.btInserir.Name = "btInserir";
            this.btInserir.Size = new System.Drawing.Size(111, 23);
            this.btInserir.TabIndex = 5;
            this.btInserir.Text = "Modificar tècnic";
            this.btInserir.UseVisualStyleBackColor = true;
            this.btInserir.Click += new System.EventHandler(this.btInserir_Click);
            // 
            // txtBNomTecnic
            // 
            this.txtBNomTecnic.Location = new System.Drawing.Point(183, 64);
            this.txtBNomTecnic.Name = "txtBNomTecnic";
            this.txtBNomTecnic.Size = new System.Drawing.Size(240, 20);
            this.txtBNomTecnic.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(45, 67);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(132, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Nom i cognoms del tècnic:";
            // 
            // btAnular
            // 
            this.btAnular.Location = new System.Drawing.Point(317, 106);
            this.btAnular.Name = "btAnular";
            this.btAnular.Size = new System.Drawing.Size(75, 23);
            this.btAnular.TabIndex = 6;
            this.btAnular.Text = "Anul·lar";
            this.btAnular.UseVisualStyleBackColor = true;
            this.btAnular.Click += new System.EventHandler(this.btAnular_Click);
            // 
            // ModificarTècnicForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(462, 149);
            this.Controls.Add(this.btAnular);
            this.Controls.Add(this.btInserir);
            this.Controls.Add(this.txtBNomTecnic);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ModificarTècnicForm";
            this.Text = "AccésVertical Manager - Modificar Tècnic";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btInserir;
        private System.Windows.Forms.TextBox txtBNomTecnic;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btAnular;
    }
}
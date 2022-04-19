namespace AccesVertical_Manager
{
    partial class AddTècnicForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddTècnicForm));
            this.label1 = new System.Windows.Forms.Label();
            this.txtBNomTecnic = new System.Windows.Forms.TextBox();
            this.btInserir = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(38, 55);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(132, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Nom i cognoms del tècnic:";
            // 
            // txtBNomTecnic
            // 
            this.txtBNomTecnic.Location = new System.Drawing.Point(176, 52);
            this.txtBNomTecnic.Name = "txtBNomTecnic";
            this.txtBNomTecnic.Size = new System.Drawing.Size(240, 20);
            this.txtBNomTecnic.TabIndex = 1;
            // 
            // btInserir
            // 
            this.btInserir.Location = new System.Drawing.Point(183, 95);
            this.btInserir.Name = "btInserir";
            this.btInserir.Size = new System.Drawing.Size(75, 23);
            this.btInserir.TabIndex = 2;
            this.btInserir.Text = "Inserir tècnic";
            this.btInserir.UseVisualStyleBackColor = true;
            this.btInserir.Click += new System.EventHandler(this.btInserir_Click);
            // 
            // AddTècnicForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(442, 130);
            this.Controls.Add(this.btInserir);
            this.Controls.Add(this.txtBNomTecnic);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AddTècnicForm";
            this.Text = "AccésVertical Manager - Afegir tècnic";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtBNomTecnic;
        private System.Windows.Forms.Button btInserir;
    }
}
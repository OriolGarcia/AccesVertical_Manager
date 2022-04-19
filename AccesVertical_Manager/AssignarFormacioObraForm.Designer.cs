namespace AccesVertical_Manager
{
    partial class AssignarFormacioObraForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AssignarFormacioObraForm));
            this.dataGridViewFormacions = new System.Windows.Forms.DataGridView();
            this.btTancar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewFormacions)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewFormacions
            // 
            this.dataGridViewFormacions.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewFormacions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewFormacions.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.dataGridViewFormacions.Location = new System.Drawing.Point(45, 44);
            this.dataGridViewFormacions.Name = "dataGridViewFormacions";
            this.dataGridViewFormacions.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewFormacions.Size = new System.Drawing.Size(384, 250);
            this.dataGridViewFormacions.TabIndex = 7;
            this.dataGridViewFormacions.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dataGridViewFormacions_CellFormatting);
            this.dataGridViewFormacions.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewFormacions_CellValueChanged);
            // 
            // btTancar
            // 
            this.btTancar.Location = new System.Drawing.Point(435, 271);
            this.btTancar.Name = "btTancar";
            this.btTancar.Size = new System.Drawing.Size(128, 23);
            this.btTancar.TabIndex = 6;
            this.btTancar.Text = "Tancar";
            this.btTancar.UseVisualStyleBackColor = true;
            this.btTancar.Click += new System.EventHandler(this.btTancar_Click);
            // 
            // AssignarFormacioObraForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(608, 339);
            this.Controls.Add(this.dataGridViewFormacions);
            this.Controls.Add(this.btTancar);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AssignarFormacioObraForm";
            this.Text = "AccésVertical Manager - Assignar Formacio a la  Obra";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewFormacions)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewFormacions;
        private System.Windows.Forms.Button btTancar;
    }
}
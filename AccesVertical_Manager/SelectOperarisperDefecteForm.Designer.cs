namespace AccesVertical_Manager
{
    partial class SelectOperarisperDefecteForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SelectOperarisperDefecteForm));
            this.btTancar = new System.Windows.Forms.Button();
            this.label17 = new System.Windows.Forms.Label();
            this.dataGridViewOperarisperDefecte = new System.Windows.Forms.DataGridView();
            this.btTreureOpObra = new System.Windows.Forms.Button();
            this.btAfegirOpObra = new System.Windows.Forms.Button();
            this.dataGridViewOperarisDisponibles = new System.Windows.Forms.DataGridView();
            this.label11 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewOperarisperDefecte)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewOperarisDisponibles)).BeginInit();
            this.SuspendLayout();
            // 
            // btTancar
            // 
            this.btTancar.Location = new System.Drawing.Point(260, 311);
            this.btTancar.Name = "btTancar";
            this.btTancar.Size = new System.Drawing.Size(75, 23);
            this.btTancar.TabIndex = 0;
            this.btTancar.Text = "Tancar";
            this.btTancar.UseVisualStyleBackColor = true;
            this.btTancar.Click += new System.EventHandler(this.btTancar_Click);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(531, 44);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(153, 13);
            this.label17.TabIndex = 19;
            this.label17.Text = "Operaris assignats per defecte:";
            // 
            // dataGridViewOperarisperDefecte
            // 
            this.dataGridViewOperarisperDefecte.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewOperarisperDefecte.Location = new System.Drawing.Point(534, 61);
            this.dataGridViewOperarisperDefecte.Name = "dataGridViewOperarisperDefecte";
            this.dataGridViewOperarisperDefecte.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewOperarisperDefecte.Size = new System.Drawing.Size(379, 201);
            this.dataGridViewOperarisperDefecte.TabIndex = 18;
            this.dataGridViewOperarisperDefecte.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dataGridViewOperarisperDefecte_CellFormatting);
            // 
            // btTreureOpObra
            // 
            this.btTreureOpObra.Location = new System.Drawing.Point(452, 150);
            this.btTreureOpObra.Name = "btTreureOpObra";
            this.btTreureOpObra.Size = new System.Drawing.Size(75, 23);
            this.btTreureOpObra.TabIndex = 17;
            this.btTreureOpObra.Text = "<< Treure";
            this.btTreureOpObra.UseVisualStyleBackColor = true;
            this.btTreureOpObra.Click += new System.EventHandler(this.btTreureOpObra_Click);
            // 
            // btAfegirOpObra
            // 
            this.btAfegirOpObra.Location = new System.Drawing.Point(453, 121);
            this.btAfegirOpObra.Name = "btAfegirOpObra";
            this.btAfegirOpObra.Size = new System.Drawing.Size(75, 23);
            this.btAfegirOpObra.TabIndex = 16;
            this.btAfegirOpObra.Text = "Afegir >>";
            this.btAfegirOpObra.UseVisualStyleBackColor = true;
            this.btAfegirOpObra.Click += new System.EventHandler(this.btAfegirOpObra_Click);
            // 
            // dataGridViewOperarisDisponibles
            // 
            this.dataGridViewOperarisDisponibles.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dataGridViewOperarisDisponibles.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewOperarisDisponibles.Location = new System.Drawing.Point(12, 61);
            this.dataGridViewOperarisDisponibles.Name = "dataGridViewOperarisDisponibles";
            this.dataGridViewOperarisDisponibles.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewOperarisDisponibles.Size = new System.Drawing.Size(434, 201);
            this.dataGridViewOperarisDisponibles.TabIndex = 15;
            this.dataGridViewOperarisDisponibles.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dataGridViewOperarisDisponibles_CellFormatting);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(9, 44);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(180, 13);
            this.label11.TabIndex = 14;
            this.label11.Text = "Operaris amb la formació necessària:";
            // 
            // SelectOperarisperDefecteForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(925, 379);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.dataGridViewOperarisperDefecte);
            this.Controls.Add(this.btTreureOpObra);
            this.Controls.Add(this.btAfegirOpObra);
            this.Controls.Add(this.dataGridViewOperarisDisponibles);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.btTancar);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SelectOperarisperDefecteForm";
            this.Text = "AccésVertical Manager - Selecció d\'Operaris per defecte";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewOperarisperDefecte)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewOperarisDisponibles)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btTancar;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.DataGridView dataGridViewOperarisperDefecte;
        private System.Windows.Forms.Button btTreureOpObra;
        private System.Windows.Forms.Button btAfegirOpObra;
        private System.Windows.Forms.DataGridView dataGridViewOperarisDisponibles;
        private System.Windows.Forms.Label label11;
    }
}
namespace AccesVertical_Manager
{
    partial class GenerarPlanningForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GenerarPlanningForm));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dateTimePickerDataInici = new System.Windows.Forms.DateTimePicker();
            this.dataGridViewOperarisperDefecte = new System.Windows.Forms.DataGridView();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lbUnitatsObra = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.GroupBox = new System.Windows.Forms.GroupBox();
            this.chkBRecalcular = new System.Windows.Forms.CheckBox();
            this.chckBDockOK = new System.Windows.Forms.CheckBox();
            this.chckBBloquejarPlanning = new System.Windows.Forms.CheckBox();
            this.dateTimePickerAfegir = new System.Windows.Forms.DateTimePicker();
            this.btEliminarData = new System.Windows.Forms.Button();
            this.btAfegirData = new System.Windows.Forms.Button();
            this.dataGridViewDates = new System.Windows.Forms.DataGridView();
            this.Dates = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btGenerarPlanning = new System.Windows.Forms.Button();
            this.btAnular = new System.Windows.Forms.Button();
            this.lbExpedient = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.comboBoxTecnics = new System.Windows.Forms.ComboBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewOperarisperDefecte)).BeginInit();
            this.GroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDates)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dateTimePickerDataInici);
            this.groupBox1.Controls.Add(this.dataGridViewOperarisperDefecte);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.lbUnitatsObra);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(13, 41);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(389, 246);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Editor de Plànning";
            // 
            // dateTimePickerDataInici
            // 
            this.dateTimePickerDataInici.Location = new System.Drawing.Point(138, 32);
            this.dateTimePickerDataInici.Name = "dateTimePickerDataInici";
            this.dateTimePickerDataInici.Size = new System.Drawing.Size(200, 20);
            this.dateTimePickerDataInici.TabIndex = 6;
            this.dateTimePickerDataInici.ValueChanged += new System.EventHandler(this.dateTimePickerDataInici_ValueChanged);
            // 
            // dataGridViewOperarisperDefecte
            // 
            this.dataGridViewOperarisperDefecte.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewOperarisperDefecte.Location = new System.Drawing.Point(138, 82);
            this.dataGridViewOperarisperDefecte.Name = "dataGridViewOperarisperDefecte";
            this.dataGridViewOperarisperDefecte.Size = new System.Drawing.Size(240, 141);
            this.dataGridViewOperarisperDefecte.TabIndex = 5;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(16, 137);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(84, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Dies necessàris:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(66, 108);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(34, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Total:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(26, 82);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(106, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Operaris per defecte:";
            // 
            // lbUnitatsObra
            // 
            this.lbUnitatsObra.AutoSize = true;
            this.lbUnitatsObra.Location = new System.Drawing.Point(55, 61);
            this.lbUnitatsObra.Name = "lbUnitatsObra";
            this.lbUnitatsObra.Size = new System.Drawing.Size(77, 13);
            this.lbUnitatsObra.TabIndex = 1;
            this.lbUnitatsObra.Text = "Unitats d\'Obra:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(78, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Data inici:";
            // 
            // GroupBox
            // 
            this.GroupBox.Controls.Add(this.chkBRecalcular);
            this.GroupBox.Controls.Add(this.chckBDockOK);
            this.GroupBox.Controls.Add(this.chckBBloquejarPlanning);
            this.GroupBox.Controls.Add(this.dateTimePickerAfegir);
            this.GroupBox.Controls.Add(this.btEliminarData);
            this.GroupBox.Controls.Add(this.btAfegirData);
            this.GroupBox.Controls.Add(this.dataGridViewDates);
            this.GroupBox.Location = new System.Drawing.Point(408, 13);
            this.GroupBox.Name = "GroupBox";
            this.GroupBox.Size = new System.Drawing.Size(357, 274);
            this.GroupBox.TabIndex = 1;
            this.GroupBox.TabStop = false;
            this.GroupBox.Text = "Selector de dates";
            // 
            // chkBRecalcular
            // 
            this.chkBRecalcular.AutoSize = true;
            this.chkBRecalcular.Checked = true;
            this.chkBRecalcular.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkBRecalcular.Location = new System.Drawing.Point(152, 78);
            this.chkBRecalcular.Name = "chkBRecalcular";
            this.chkBRecalcular.Size = new System.Drawing.Size(99, 17);
            this.chkBRecalcular.TabIndex = 7;
            this.chkBRecalcular.Text = "Recalcular dies";
            this.chkBRecalcular.UseVisualStyleBackColor = true;
            // 
            // chckBDockOK
            // 
            this.chckBDockOK.AutoSize = true;
            this.chckBDockOK.Location = new System.Drawing.Point(152, 189);
            this.chckBDockOK.Name = "chckBDockOK";
            this.chckBDockOK.Size = new System.Drawing.Size(113, 17);
            this.chckBDockOK.TabIndex = 5;
            this.chckBDockOK.Text = "Documentació OK";
            this.chckBDockOK.UseVisualStyleBackColor = true;
            // 
            // chckBBloquejarPlanning
            // 
            this.chckBBloquejarPlanning.AutoSize = true;
            this.chckBBloquejarPlanning.Checked = true;
            this.chckBBloquejarPlanning.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chckBBloquejarPlanning.Location = new System.Drawing.Point(152, 165);
            this.chckBBloquejarPlanning.Name = "chckBBloquejarPlanning";
            this.chckBBloquejarPlanning.Size = new System.Drawing.Size(193, 17);
            this.chckBBloquejarPlanning.TabIndex = 4;
            this.chckBBloquejarPlanning.Text = "Bloquejar Regeneració de Planning";
            this.chckBBloquejarPlanning.UseVisualStyleBackColor = true;
            // 
            // dateTimePickerAfegir
            // 
            this.dateTimePickerAfegir.Location = new System.Drawing.Point(152, 39);
            this.dateTimePickerAfegir.Name = "dateTimePickerAfegir";
            this.dateTimePickerAfegir.Size = new System.Drawing.Size(200, 20);
            this.dateTimePickerAfegir.TabIndex = 3;
            // 
            // btEliminarData
            // 
            this.btEliminarData.Location = new System.Drawing.Point(152, 126);
            this.btEliminarData.Name = "btEliminarData";
            this.btEliminarData.Size = new System.Drawing.Size(163, 23);
            this.btEliminarData.TabIndex = 2;
            this.btEliminarData.Text = "Eliminar Data Seleccionada";
            this.btEliminarData.UseVisualStyleBackColor = true;
            this.btEliminarData.Click += new System.EventHandler(this.btEliminarData_Click);
            // 
            // btAfegirData
            // 
            this.btAfegirData.Location = new System.Drawing.Point(152, 101);
            this.btAfegirData.Name = "btAfegirData";
            this.btAfegirData.Size = new System.Drawing.Size(163, 23);
            this.btAfegirData.TabIndex = 1;
            this.btAfegirData.Text = "Afegir data";
            this.btAfegirData.UseVisualStyleBackColor = true;
            this.btAfegirData.Click += new System.EventHandler(this.btAfegirData_Click);
            // 
            // dataGridViewDates
            // 
            this.dataGridViewDates.AllowUserToAddRows = false;
            this.dataGridViewDates.AllowUserToDeleteRows = false;
            this.dataGridViewDates.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewDates.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewDates.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Dates});
            this.dataGridViewDates.Location = new System.Drawing.Point(6, 39);
            this.dataGridViewDates.Name = "dataGridViewDates";
            this.dataGridViewDates.ReadOnly = true;
            this.dataGridViewDates.RowHeadersVisible = false;
            this.dataGridViewDates.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewDates.Size = new System.Drawing.Size(128, 229);
            this.dataGridViewDates.TabIndex = 0;
            // 
            // Dates
            // 
            this.Dates.HeaderText = "Dates";
            this.Dates.Name = "Dates";
            this.Dates.ReadOnly = true;
            // 
            // btGenerarPlanning
            // 
            this.btGenerarPlanning.Location = new System.Drawing.Point(362, 294);
            this.btGenerarPlanning.Name = "btGenerarPlanning";
            this.btGenerarPlanning.Size = new System.Drawing.Size(167, 39);
            this.btGenerarPlanning.TabIndex = 2;
            this.btGenerarPlanning.Text = "Generar Plànning";
            this.btGenerarPlanning.UseVisualStyleBackColor = true;
            this.btGenerarPlanning.Click += new System.EventHandler(this.btGenerarPlanning_Click);
            // 
            // btAnular
            // 
            this.btAnular.Location = new System.Drawing.Point(362, 340);
            this.btAnular.Name = "btAnular";
            this.btAnular.Size = new System.Drawing.Size(167, 23);
            this.btAnular.TabIndex = 3;
            this.btAnular.Text = "Anul·lar";
            this.btAnular.UseVisualStyleBackColor = true;
            this.btAnular.Click += new System.EventHandler(this.btAnular_Click);
            // 
            // lbExpedient
            // 
            this.lbExpedient.AutoSize = true;
            this.lbExpedient.Location = new System.Drawing.Point(42, 22);
            this.lbExpedient.Name = "lbExpedient";
            this.lbExpedient.Size = new System.Drawing.Size(85, 13);
            this.lbExpedient.TabIndex = 4;
            this.lbExpedient.Text = "Num. Expedient:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(218, 22);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(43, 13);
            this.label6.TabIndex = 5;
            this.label6.Text = "Tècnic:";
            // 
            // comboBoxTecnics
            // 
            this.comboBoxTecnics.FormattingEnabled = true;
            this.comboBoxTecnics.Location = new System.Drawing.Point(267, 19);
            this.comboBoxTecnics.Name = "comboBoxTecnics";
            this.comboBoxTecnics.Size = new System.Drawing.Size(121, 21);
            this.comboBoxTecnics.TabIndex = 6;
            // 
            // GenerarPlanningForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(793, 365);
            this.Controls.Add(this.comboBoxTecnics);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.lbExpedient);
            this.Controls.Add(this.btAnular);
            this.Controls.Add(this.btGenerarPlanning);
            this.Controls.Add(this.GroupBox);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "GenerarPlanningForm";
            this.Text = "AccésVertical Manager - Generar Plànning";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewOperarisperDefecte)).EndInit();
            this.GroupBox.ResumeLayout(false);
            this.GroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDates)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dataGridViewOperarisperDefecte;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lbUnitatsObra;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox GroupBox;
        private System.Windows.Forms.DataGridView dataGridViewDates;
        private System.Windows.Forms.Button btGenerarPlanning;
        private System.Windows.Forms.Button btAnular;
        private System.Windows.Forms.DateTimePicker dateTimePickerDataInici;
        private System.Windows.Forms.Button btEliminarData;
        private System.Windows.Forms.Button btAfegirData;
        private System.Windows.Forms.DateTimePicker dateTimePickerAfegir;
        private System.Windows.Forms.Label lbExpedient;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox comboBoxTecnics;
        private System.Windows.Forms.DataGridViewTextBoxColumn Dates;
        private System.Windows.Forms.CheckBox chckBBloquejarPlanning;
        private System.Windows.Forms.CheckBox chckBDockOK;
        private System.Windows.Forms.CheckBox chkBRecalcular;
    }
}
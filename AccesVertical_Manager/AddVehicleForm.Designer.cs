namespace AccesVertical_Manager
{
    partial class AddVehicleForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddVehicleForm));
            this.AltaVehicle = new System.Windows.Forms.Button();
            this.btAnular = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.numericUpDownPlaces = new System.Windows.Forms.NumericUpDown();
            this.comboBoxBaca = new System.Windows.Forms.ComboBox();
            this.comboBoxPropietari = new System.Windows.Forms.ComboBox();
            this.textBoxMarca = new System.Windows.Forms.TextBox();
            this.textBoxModel = new System.Windows.Forms.TextBox();
            this.textBoxMatricula = new System.Windows.Forms.TextBox();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.label7 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPlaces)).BeginInit();
            this.SuspendLayout();
            // 
            // AltaVehicle
            // 
            this.AltaVehicle.Location = new System.Drawing.Point(279, 227);
            this.AltaVehicle.Name = "AltaVehicle";
            this.AltaVehicle.Size = new System.Drawing.Size(100, 31);
            this.AltaVehicle.TabIndex = 0;
            this.AltaVehicle.Text = "Alta Vehicle";
            this.AltaVehicle.UseVisualStyleBackColor = true;
            this.AltaVehicle.Click += new System.EventHandler(this.AltaVehicle_Click);
            // 
            // btAnular
            // 
            this.btAnular.Location = new System.Drawing.Point(386, 227);
            this.btAnular.Name = "btAnular";
            this.btAnular.Size = new System.Drawing.Size(77, 31);
            this.btAnular.TabIndex = 1;
            this.btAnular.Text = "Anul·lar";
            this.btAnular.UseVisualStyleBackColor = true;
            this.btAnular.Click += new System.EventHandler(this.btAnular_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(70, 55);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Marca:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(354, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(39, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Model:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(70, 94);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Matrícula:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(354, 94);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "Baca:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(70, 137);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(42, 13);
            this.label5.TabIndex = 6;
            this.label5.Text = "Places:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(354, 137);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(54, 13);
            this.label6.TabIndex = 7;
            this.label6.Text = "Propietari:";
            // 
            // numericUpDownPlaces
            // 
            this.numericUpDownPlaces.Location = new System.Drawing.Point(129, 135);
            this.numericUpDownPlaces.Name = "numericUpDownPlaces";
            this.numericUpDownPlaces.Size = new System.Drawing.Size(120, 20);
            this.numericUpDownPlaces.TabIndex = 8;
            this.numericUpDownPlaces.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // comboBoxBaca
            // 
            this.comboBoxBaca.FormattingEnabled = true;
            this.comboBoxBaca.Items.AddRange(new object[] {
            "AMB BACA",
            "SENSE BACA"});
            this.comboBoxBaca.Location = new System.Drawing.Point(414, 91);
            this.comboBoxBaca.Name = "comboBoxBaca";
            this.comboBoxBaca.Size = new System.Drawing.Size(121, 21);
            this.comboBoxBaca.TabIndex = 9;
            // 
            // comboBoxPropietari
            // 
            this.comboBoxPropietari.FormattingEnabled = true;
            this.comboBoxPropietari.Items.AddRange(new object[] {
            "De propietat",
            "De lloguer"});
            this.comboBoxPropietari.Location = new System.Drawing.Point(414, 134);
            this.comboBoxPropietari.Name = "comboBoxPropietari";
            this.comboBoxPropietari.Size = new System.Drawing.Size(121, 21);
            this.comboBoxPropietari.TabIndex = 10;
            // 
            // textBoxMarca
            // 
            this.textBoxMarca.Location = new System.Drawing.Point(129, 52);
            this.textBoxMarca.Name = "textBoxMarca";
            this.textBoxMarca.Size = new System.Drawing.Size(120, 20);
            this.textBoxMarca.TabIndex = 11;
            // 
            // textBoxModel
            // 
            this.textBoxModel.Location = new System.Drawing.Point(399, 52);
            this.textBoxModel.Name = "textBoxModel";
            this.textBoxModel.Size = new System.Drawing.Size(136, 20);
            this.textBoxModel.TabIndex = 12;
            // 
            // textBoxMatricula
            // 
            this.textBoxMatricula.Location = new System.Drawing.Point(129, 91);
            this.textBoxMatricula.Name = "textBoxMatricula";
            this.textBoxMatricula.Size = new System.Drawing.Size(120, 20);
            this.textBoxMatricula.TabIndex = 13;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(200, 187);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(200, 20);
            this.dateTimePicker1.TabIndex = 14;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(70, 193);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(124, 13);
            this.label7.TabIndex = 15;
            this.label7.Text = "Data d\'alta a la empresa:";
            this.label7.Click += new System.EventHandler(this.label7_Click);
            // 
            // AddVehicleForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(614, 288);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.textBoxMatricula);
            this.Controls.Add(this.textBoxModel);
            this.Controls.Add(this.textBoxMarca);
            this.Controls.Add(this.comboBoxPropietari);
            this.Controls.Add(this.comboBoxBaca);
            this.Controls.Add(this.numericUpDownPlaces);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btAnular);
            this.Controls.Add(this.AltaVehicle);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AddVehicleForm";
            this.Text = "AccésVertical Manager - Alta Vehicle";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPlaces)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button AltaVehicle;
        private System.Windows.Forms.Button btAnular;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown numericUpDownPlaces;
        private System.Windows.Forms.ComboBox comboBoxBaca;
        private System.Windows.Forms.ComboBox comboBoxPropietari;
        private System.Windows.Forms.TextBox textBoxMarca;
        private System.Windows.Forms.TextBox textBoxModel;
        private System.Windows.Forms.TextBox textBoxMatricula;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Label label7;
    }
}
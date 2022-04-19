using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AccesVertical_Manager
{
    public partial class AltaOperariForm : Form
    {
        
        private string Origenimatge=null;
        private string pathdesti;
        private Connection mysqlconnect;
        public AltaOperariForm(Connection mysqlconnect,string pathdesti)
        {
            this.pathdesti = pathdesti;
            this.mysqlconnect = mysqlconnect;
            InitializeComponent();
        }

        private void btExaminar_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();

            fileDialog.InitialDirectory = "C:\\";
            fileDialog.Filter = "Image files (*.jpg, *.jpeg, *.png) | *.jpg; *.jpeg;  *.png";
            fileDialog.FilterIndex = 4;
            fileDialog.RestoreDirectory = true;

            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                Origenimatge = fileDialog.FileName;
                txtBImatge.Text = fileDialog.FileName;
                pictureBox1.Image = Image.FromFile(fileDialog.FileName);
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            }

        
    }

        private void btInserir_Click(object sender, EventArgs e)
        {


            MySqlConnection conn = mysqlconnect.getmysqlconn();
            try
            {

                string destiImatge = null;

                if (Origenimatge != null)
                {
                    destiImatge = Path.Combine(pathdesti, Path.GetFileName(Origenimatge));
                    var confirm = DialogResult.Yes;
                    if (!File.Exists(destiImatge))
                    {

                        File.Copy(Origenimatge, destiImatge);
                    }
                    else { MessageBox.Show("Ja existeix la imatge al servidor, no es substituirà"); }
                    string Query = "INSERT INTO operaris(Alta,FotografiaPath,Nom,Cognoms,Vinculació,DNI,Adreça,"
                       + "Telèfon,`Telèfon Mòbil`,`Correu electrònic`,`Data de naixament`,Nacionalitat,`Numero Seguretat Social`,"
                      + "Categoria,`Carnet Professional`,`Nivell`) VALUES(@Alta, @FotografiaPath,@Nom,@Cognoms,@Vinculacio,@DNI,@Adreça,@Telefon,"
                      + "@Mobil,@correuelectronic,@datanaixament,@nacionalitat,@NSS,@Categoria,@carnet,@nivell); ";
                    MySqlCommand cmd = new MySqlCommand(Query, conn);
                    cmd.Parameters.AddWithValue("@Alta", dateTimePickerAlta.Value.Date.ToString("yyyy-MM-dd"));
                    cmd.Parameters.AddWithValue("@FotografiaPath", destiImatge);
                    cmd.Parameters.AddWithValue("@Nom", txtBNom.Text);
                    cmd.Parameters.AddWithValue("@Cognoms", txtBCognoms.Text);
                    cmd.Parameters.AddWithValue("@Vinculacio", comboBoxVinculacio.SelectedItem);
                    cmd.Parameters.AddWithValue("@DNI", txtBDNI.Text);
                    cmd.Parameters.AddWithValue("@Adreça", txtBAdreça.Text);
                    cmd.Parameters.AddWithValue("@Telefon", txtBTelefon.Text);
                    cmd.Parameters.AddWithValue("@Mobil", txtBtelefonMobil.Text);
                    cmd.Parameters.AddWithValue("@correuelectronic", txtBEMAIL.Text);
                    cmd.Parameters.AddWithValue("@datanaixament", dtPDataNaixament.Value.Date.ToString("yyyy-MM-dd"));
                    cmd.Parameters.AddWithValue("@nacionalitat", txtBnacionalitat.Text);
                    cmd.Parameters.AddWithValue("@NSS", txtBNumeroSS.Text);
                    cmd.Parameters.AddWithValue("@Categoria", comboBoxCategoria.SelectedItem);
                    cmd.Parameters.AddWithValue("@carnet", comboBoxCarnet.SelectedItem);
                    cmd.Parameters.AddWithValue("@nivell", cmboBNivell.SelectedItem);
                    
                    conn.Open();
                    cmd.ExecuteReader();
                    conn.Close();
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
            finally
            {
                conn.Close();
               
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void txtBImatge_TextChanged(object sender, EventArgs e)
        {

        }

        private void btCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}

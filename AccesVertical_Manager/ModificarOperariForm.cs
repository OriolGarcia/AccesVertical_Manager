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
    public partial class ModificarOperariForm : Form
    {
   
        private string pathdesti;
        private string destiImatge;
        private string Origenimatge;
        private Connection mysqlconnect;
        private string OperariID;
        private Boolean imatgecanviada = false;
        public ModificarOperariForm(Connection mysqlconnect,string OperariID, string pathdesti)
        {
            this.pathdesti = pathdesti;
            this.mysqlconnect = mysqlconnect;
            this.OperariID = OperariID;
            InitializeComponent();
            SelectedValues();
        }
        private void SelectedValues()
        {
            MySqlConnection conn = mysqlconnect.getmysqlconn();
            try
            {
               
                DataTable DtDades = new DataTable();
                string query = "Select FotografiaPath,Nom,Cognoms,DNI,Adreça,"
                + "Telèfon,`Telèfon Mòbil`,`Correu electrònic`,`Data de naixament`,Nacionalitat,`Numero Seguretat Social`,"
                + "Categoria,`Carnet Professional`,`Nivell`,`Vinculació`,Alta from operaris"
                + " WHERE  ( OperariID=@OperariID);";
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@OperariID", OperariID);
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {

                   destiImatge= !reader.IsDBNull(0) ? reader.GetString(0) : null;
                    if (File.Exists(destiImatge))
                    {
                        pictureBox1.Image = Image.FromFile(destiImatge);
                        pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                    }
                    txtBNom.Text = !reader.IsDBNull(1) ? reader.GetString(1) : null;
                    txtBCognoms.Text = !reader.IsDBNull(2) ? reader.GetString(2) : null;
                    txtBDNI.Text = !reader.IsDBNull(3) ? reader.GetString(3) : null;
                    txtBAdreça.Text = !reader.IsDBNull(4) ? reader.GetString(4) : null;
                    txtBTelefon.Text = !reader.IsDBNull(5) ? reader.GetString(5) : null;
                    txtBtelefonMobil.Text = !reader.IsDBNull(6) ? reader.GetString(6) : null;
                    txtBEMAIL.Text = !reader.IsDBNull(7) ? reader.GetString(7) : null;
                    dtPDataNaixament.Value = !reader.IsDBNull(8) ? reader.GetDateTime(8) : new DateTime();
                    txtBnacionalitat.Text = !reader.IsDBNull(9) ? reader.GetString(9) : null;
                    txtBNumeroSS.Text = !reader.IsDBNull(10) ? reader.GetString(10) : null;
                    comboBoxCategoria.SelectedItem = !reader.IsDBNull(11) ? reader.GetString(11) : null;
                    comboBoxCarnet.SelectedItem = !reader.IsDBNull(12) ? reader.GetString(12) : null;
                     cmboBNivell.SelectedItem = !reader.IsDBNull(13) ? reader.GetString(13) : null;
                    comboBoxVinculacio.SelectedItem = !reader.IsDBNull(14) ? reader.GetString(14) : null;
                    dateTimePickerAlta.Value = !reader.IsDBNull(15) ? reader.GetDateTime(15) : DateTime.Now;


                    conn.Close();
                }






            }

            catch (Exception err)
            {
                MessageBox.Show(err.Message);

            }
           finally
            {
                conn.Close();
            }



        }
        private void btModificar_Click(object sender, EventArgs e)
        {
            MySqlConnection conn = mysqlconnect.getmysqlconn();
           
            try
            {
                conn.Open();
                if (imatgecanviada && (File.Exists(Origenimatge)))
                {
                   
                        destiImatge = Path.Combine(pathdesti, Path.GetFileName(Origenimatge));
                    if (!File.Exists(destiImatge))
                    {

                        File.Copy(Origenimatge, destiImatge);
                    }
                    else { MessageBox.Show("Ja existeix la imatge al servidor, no es substituirà"); }
                }


                string Query = "UPDATE Operaris SET Alta=@Alta,FotografiaPath=@FotografiaPath,Nom=@Nom,Cognoms=@Cognoms,"
                    + "Vinculació=@Vinculacio,DNI=@DNI,Adreça=@Adreça,"
                   + "Telèfon=@Telefon,`Telèfon Mòbil`=@Mobil,`Correu electrònic`=@correuelectronic,"
                   + "`Data de naixament`=@datanaixament,Nacionalitat=@nacionalitat,`Numero Seguretat Social`=@NSS,"
                  + "Categoria=@Categoria,`Carnet Professional`=@carnet,`Nivell`=@nivell WHERE OperariID=@OperariID";
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
                cmd.Parameters.AddWithValue("@datanaixament", dtPDataNaixament.Value.Date.ToString("yyyy-MM-dd HH:mm"));
                cmd.Parameters.AddWithValue("@nacionalitat", txtBnacionalitat.Text);
                cmd.Parameters.AddWithValue("@NSS", txtBNumeroSS.Text);
                cmd.Parameters.AddWithValue("@Categoria",comboBoxCategoria.SelectedItem);
                cmd.Parameters.AddWithValue("@carnet", comboBoxCarnet.SelectedItem);
                cmd.Parameters.AddWithValue("@nivell", cmboBNivell.SelectedItem);
                cmd.Parameters.AddWithValue("@OperariID", OperariID);
               cmd.ExecuteReader();
            
                conn.Close();
                this.Close();

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

        private void btExaminar_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();

            fileDialog.InitialDirectory = "C:\\";
            fileDialog.Filter = "Image files (*.jpg, *.jpeg, *.png) | *.jpg; *.jpeg;  *.png";
            fileDialog.FilterIndex = 4;
            fileDialog.RestoreDirectory = true;

            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                imatgecanviada = true;
                Origenimatge = fileDialog.FileName;
                txtBImatge.Text = fileDialog.FileName;
                pictureBox1.Image = Image.FromFile(fileDialog.FileName);
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            }


        }

        private void btCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}

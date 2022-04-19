using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AccesVertical_Manager
{
    public partial class AddVehicleForm : Form
    {
        private Connection mysqlconnect;
        public AddVehicleForm(Connection mysqlconnect)
        {
            this.mysqlconnect = mysqlconnect;
            InitializeComponent();
        }

        private void btAnular_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void AltaVehicle_Click(object sender, EventArgs e)
        {
            MySqlConnection conn = mysqlconnect.getmysqlconn();
            try
            {

                    string Query = "INSERT INTO Vehicles(Marca,Alta,Matricula,Model,Places,"
                       + "Baca, Propietari) VALUES(@Marca,@Alta,@Matricula, @Model,@Places,@Baca,@Propietari); ";
                    MySqlCommand cmd = new MySqlCommand(Query, conn);
                    cmd.Parameters.AddWithValue("@Marca", textBoxMarca.Text);
                cmd.Parameters.AddWithValue("@Alta", dateTimePicker1.Value.Date.ToString("yyyy-MM-dd"));
                cmd.Parameters.AddWithValue("@Matricula", textBoxMatricula.Text);
                    cmd.Parameters.AddWithValue("@Model", textBoxModel.Text);
                    cmd.Parameters.AddWithValue("@Places", numericUpDownPlaces.Value);
                    cmd.Parameters.AddWithValue("@Baca", comboBoxBaca.SelectedItem);
                    cmd.Parameters.AddWithValue("@Propietari",comboBoxPropietari.SelectedItem);
                    conn.Open();
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

        private void label7_Click(object sender, EventArgs e)
        {

        }
    }
}

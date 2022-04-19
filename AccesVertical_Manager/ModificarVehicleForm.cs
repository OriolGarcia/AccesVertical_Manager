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
    public partial class ModificarVehicleForm : Form
    {

        private Connection mysqlconnect;
        private string VehicleID;
        public ModificarVehicleForm(Connection mysqlconnect, string VehicleID)
        {
            this.VehicleID = VehicleID;
            this.mysqlconnect = mysqlconnect;
            InitializeComponent();
            SelectedItems();
        }
        private void SelectedItems() {
            try
            {
                MySqlConnection conn = mysqlconnect.getmysqlconn();
                DataTable DtDades = new DataTable();
                string query = "Select `Marca`,`Matricula`,`Model`,`Places`,`Baca`,"
                + "`Propietari`, Alta from Vehicles"
                + " WHERE  ( VehicleID=@VehicleID);";
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@VehicleID", VehicleID);
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {

                    textBoxMarca.Text = !reader.IsDBNull(0) ? reader.GetString(0) : null;
                    textBoxMatricula.Text = !reader.IsDBNull(1) ? reader.GetString(1) : null;
                    textBoxModel.Text = !reader.IsDBNull(2) ? reader.GetString(2) : null;
                    numericUpDownPlaces.Value = !reader.IsDBNull(3) ? reader.GetInt16(3) : 0;
                    comboBoxBaca.SelectedItem = !reader.IsDBNull(4) ? reader.GetString(4) : null;
                    comboBoxPropietari.SelectedItem = !reader.IsDBNull(5) ? reader.GetString(5) : null;
                    dateTimePicker1.Value = !reader.IsDBNull(6) ? reader.GetDateTime(6) : DateTime.Now;
                    conn.Close();
                }






            }

            catch (Exception err)
            {
                MessageBox.Show(err.Message);

            }




        }
        private void ModificarVehicle_Click(object sender, EventArgs e)
        {
            MySqlConnection conn = mysqlconnect.getmysqlconn();
            conn.Open();
            try
            {


                string Query = "UPDATE Vehicles SET Marca=@Marca,Matricula=@Matricula,Model=@Model"
                    + " ,Places=@Places,Baca=@Baca,"
                   + " Propietari=@Propietari, Alta=@Alta WHERE VehicleID=@VehicleID";
                MySqlCommand cmd = new MySqlCommand(Query, conn);
                cmd.Parameters.AddWithValue("@Marca", textBoxMarca.Text);
                cmd.Parameters.AddWithValue("@Matricula", textBoxMatricula.Text);
                cmd.Parameters.AddWithValue("@Model", textBoxModel.Text);
                cmd.Parameters.AddWithValue("@Places", numericUpDownPlaces.Value);
                cmd.Parameters.AddWithValue("@Baca", comboBoxBaca.SelectedItem);
                cmd.Parameters.AddWithValue("@Propietari", comboBoxPropietari.SelectedItem);
                cmd.Parameters.AddWithValue("@Alta", dateTimePicker1.Value.Date.ToString("yyyy-MM-dd"));
                cmd.Parameters.AddWithValue("@VehicleID", VehicleID);
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

        private void btAnular_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}

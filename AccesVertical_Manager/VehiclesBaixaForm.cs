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
    public partial class VehiclesBaixaForm : Form
    {

        private Connection mysqlconnect;
        private string VehicleID;
        private DateTime MAXDATE;
        public VehiclesBaixaForm(Connection mysqlconnect, string VehicleID)
        {

            this.mysqlconnect = mysqlconnect;
            this.VehicleID = VehicleID;
            InitializeComponent();
            SelectedValues();
        }
      private void SelectedValues()
        {
            try
            {
                MySqlConnection conn = mysqlconnect.getmysqlconn();
                DataTable DtDades = new DataTable();
                string query = "Select `Marca`,`Matricula`,`Baixa`,"
                + "`Propietari`, Alta from Vehicles"
                + " WHERE  ( VehicleID=@VehicleID);";
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@VehicleID", VehicleID);
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {


                    string marca = !reader.IsDBNull(0) ? reader.GetString(0) : null;
                    string Matricula = !reader.IsDBNull(1) ? reader.GetString(1) : null;
                    label1.Text = label1.Text + " " + marca + "  amb matricula " + Matricula;
                    checkBox1.Checked = !reader.IsDBNull(2) ? true : false;
                    dateTimePicker1.Value = !reader.IsDBNull(2) ? reader.GetDateTime(2) : DateTime.Now;
                    dateTimePicker1.Enabled = !reader.IsDBNull(2) ? true : false;
                    
                }
                conn.Close();


                DtDades = new DataTable();
                query = "Select MAX(`Data`) "
                   + " from VehiclesObraData where `vehicle`= @VehicleID ; ";



                cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@VehicleID", VehicleID);
                conn.Open();
                reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    MAXDATE = !reader.IsDBNull(0) ? reader.GetDateTime(0) : new DateTime();
                }
                conn.Close();
            




            }

            catch (Exception err)
            {
                MessageBox.Show(err.Message);

            }






        }
        private void btAcceptar_Click(object sender, EventArgs e)
        {
            MySqlConnection conn = mysqlconnect.getmysqlconn();

            try
            {
                conn.Open();
                if (dateTimePicker1.Value < MAXDATE)
                {

                    MessageBox.Show("No pot haver dates en obres d'aquest vehicle posteriors a la data en la que es vol donar de baixa"
                    + "\n Aquest vehicle està assignat en obres el dia: " + MAXDATE.ToString("dd/MM/yyyy"));
                }
                else
                {



                    string Query = "UPDATE Vehicles SET Baixa=@Baixa WHERE VehicleID=@VehicleID";
                    MySqlCommand cmd = new MySqlCommand(Query, conn);
                    if (checkBox1.Checked)
                        cmd.Parameters.AddWithValue("@Baixa", dateTimePicker1.Value);
                    else
                        cmd.Parameters.AddWithValue("@Baixa", null);
                    cmd.Parameters.AddWithValue("@VehicleID", VehicleID);
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

        private void btAnular_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            dateTimePicker1.Enabled = checkBox1.Checked;
        }
    }
}

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
    public partial class ModificarTècnicForm : Form
    {

        private Connection mysqlconnect;
        private string ID;
        public ModificarTècnicForm(Connection connect,string ID, string nomActual)
        {
            mysqlconnect = connect;
            this.ID = ID;
            InitializeComponent();
            txtBNomTecnic.Text = nomActual;
        }

        private void btInserir_Click(object sender, EventArgs e)
        {
            MySqlConnection conn = mysqlconnect.getmysqlconn();
            conn.Open();
            try
            {

                string Query = "UPDATE Tècnics SET `Nom i cognoms`=@Nom WHERE TècnicID=@TecnicID";
                MySqlCommand cmd = new MySqlCommand(Query, conn);

                cmd.Parameters.AddWithValue("@Nom", txtBNomTecnic.Text);
                cmd.Parameters.AddWithValue("@TecnicID", ID);
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

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
    public partial class NovaFormació : Form
    {
        private Connection mysqlconnect;
        public NovaFormació(Connection connect)
        {
            mysqlconnect = connect;
            InitializeComponent();
        }

        private void btInserir_Click(object sender, EventArgs e)
        {




            MySqlConnection conn = mysqlconnect.getmysqlconn();
            try
            {

                string Query = "INSERT INTO Formacions ( Titol) VALUES ("
                    + "@Titol)";
                MySqlCommand cmd = new MySqlCommand(Query, conn);
                cmd.Parameters.AddWithValue("@Titol", textBox1.Text);
                MySqlDataReader MyReader2;
                conn.Open();
                MyReader2 = cmd.ExecuteReader();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
            finally
            {
                conn.Close();
                this.Close();
            }
        }

        private void btAnular_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}

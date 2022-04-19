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
    public partial class ModificarFormacioForm : Form
    {

        private Connection mysqlconnect;
        private string FormacioID;
        public ModificarFormacioForm(Connection connect, string FormacioID)
        {
            mysqlconnect = connect;
            this.FormacioID = FormacioID;
            InitializeComponent();
            SelectedValues();

        }

        private void SelectedValues()
        {

            try
            {
                MySqlConnection conn = mysqlconnect.getmysqlconn();
                DataTable DtDades = new DataTable();
                string query = "Select Titol from Formacions"
                + " WHERE  ( FormacioID=@FormacioID);";
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@FormacioID",FormacioID);
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {

                   textBox1.Text = !reader.IsDBNull(0) ? reader.GetString(0) : null;
                    conn.Close();
                } }

            catch (Exception err)
            {
                MessageBox.Show(err.Message);

            }



        }
        private void btModificar_Click(object sender, EventArgs e)
        {
            MySqlConnection conn = mysqlconnect.getmysqlconn();
            conn.Open();
            try
            {
               

                string Query = "UPDATE Formacions SET Titol=@Titol WHERE FormacioID=@FormacioID";
                MySqlCommand cmd = new MySqlCommand(Query, conn);
                cmd.Parameters.AddWithValue("@Titol",textBox1.Text);
                cmd.Parameters.AddWithValue("@FormacioID", FormacioID);
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

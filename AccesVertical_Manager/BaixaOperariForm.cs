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
    public partial class BaixaOperariForm : Form
    {

        private Connection mysqlconnect;
        private string OperariID;
            private DateTime MAXDATE;
        public BaixaOperariForm(Connection mysqlconnect, string OperariID )
        {
            
            this.mysqlconnect = mysqlconnect;
            this.OperariID = OperariID;
            InitializeComponent();
            SelectedValues();
        }
        private void SelectedValues()
        {
            try
            {
                MySqlConnection conn = mysqlconnect.getmysqlconn();
                DataTable DtDades = new DataTable();
                string query = "Select FotografiaPath,Nom,Cognoms,Baixa "
                + " from operaris"
                + " WHERE  ( OperariID=@OperariID);";
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@OperariID", OperariID);
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {

                    string destiImatge = !reader.IsDBNull(0) ? reader.GetString(0) : null;
                    pictureBox1.Image = Image.FromFile(destiImatge);
                    pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                    string nom = !reader.IsDBNull(1) ? reader.GetString(1) : null;
                    string cognoms = !reader.IsDBNull(2) ? reader.GetString(2) : null;
                    label1.Text = label1.Text + " " + nom + " " + cognoms;
                    checkBox1.Checked = !reader.IsDBNull(3) ? true : false;
                    dateTimePicker1.Value = !reader.IsDBNull(3) ? reader.GetDateTime(3) : DateTime.Now;
                    dateTimePicker1.Enabled = !reader.IsDBNull(3) ? true : false;
                    
                }
                conn.Close();

                         DtDades = new DataTable();
                         query = "Select MAX(`Data`) "
                            + " from OperarisObraData where `operari`= @OperariID ; ";



                         cmd = new MySqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@OperariID", OperariID);
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
        private void btAnular_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            dateTimePicker1.Enabled = checkBox1.Checked;
        }

        private void btAcceptar_Click(object sender, EventArgs e)
        {
            MySqlConnection conn = mysqlconnect.getmysqlconn();
          
            try
            {
                conn.Open();
                if (dateTimePicker1.Value < MAXDATE) {

                    MessageBox.Show("No pot haver dates en obres d'aquest operari posteriors a la data en la que es vol donar de baixa"
                    +"\n Aquest operari té obres el dia: " +MAXDATE.ToString("dd/MM/yyyy"));
                }
                else
                {

                    string Query = "UPDATE Operaris SET Baixa=@Baixa WHERE OperariID=@OperariID";
                    MySqlCommand cmd = new MySqlCommand(Query, conn);
                    if (checkBox1.Checked)
                        cmd.Parameters.AddWithValue("@Baixa", dateTimePicker1.Value);
                    else
                        cmd.Parameters.AddWithValue("@Baixa", null);
                    cmd.Parameters.AddWithValue("@OperariID", OperariID);
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
    }
}

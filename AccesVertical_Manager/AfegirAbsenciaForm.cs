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
    public partial class AfegirAbsenciaForm : Form
    {

        private string pathimage;
        private Connection mysqlconnect;
        string operariID;
        public AfegirAbsenciaForm(Connection mysqlconnect, string operariID, string pathimage, string nom, string cognoms)
        {


            this.operariID = operariID;

            this.pathimage = pathimage;
            this.mysqlconnect = mysqlconnect;
            InitializeComponent();
            pictureBox1.Image = Image.FromFile(pathimage);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            lbNomsTecnic.Text = "Noms i Cognoms: " + nom + ", " + cognoms;
            comboBox1.SelectedItem = "ABSÈNCIA";
        }
    
        

        private void btInserir_Click(object sender, EventArgs e)
        {


            MySqlConnection conn = mysqlconnect.getmysqlconn();
           
                DateTime data1 = dateTimePicker1.Value.Date;

            if (data1 >= DateTime.Now||mysqlconnect.getPermissions()[0])
            {
                DateTime data2 = dateTimePicker2.Value.Date;
                data2.AddDays(1);
                for (DateTime CurrentDate = data1; CurrentDate <= data2; CurrentDate = CurrentDate.AddDays(1))
                {
                    try
                    {

                        string Query = "INSERT INTO Absencies(Operari,`Data`,`Motiu`) "
                          + "VALUES(@OperariID,@Data, @Motiu); ";
                        MySqlCommand cmd = new MySqlCommand(Query, conn);
                        cmd.Parameters.AddWithValue("@OperariID", operariID);
                        cmd.Parameters.AddWithValue("@Data", CurrentDate.ToString("yyyy-MM-dd HH:mm:ss"));
                        cmd.Parameters.AddWithValue("@Motiu", comboBox1.SelectedItem);
                        conn.Open();
                        cmd.ExecuteReader();
                        conn.Close();
                        Query = "delete from OperarisObraData  where (Operari,Data) in ((@OperariID,@Data));"; ;
                        cmd = new MySqlCommand(Query, conn);
                        cmd.Parameters.AddWithValue("@OperariID", operariID);
                        cmd.Parameters.AddWithValue("@Data", CurrentDate.ToString("yyyy-MM-dd HH:mm:ss"));
                        conn.Open();
                        cmd.ExecuteReader();
                        conn.Close();
                    }
                    catch (Exception ex)
                    {


                    }
                    finally
                    {
                        conn.Close();

                    }


                }
                this.Close();
            }else { MessageBox.Show("El teu usuari no pot afegir una absència anterior a la data actual, necessites permisos de Supervisor"); }

           

           
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            if (dateTimePicker1.Value > dateTimePicker2.Value) {
                dateTimePicker1.Value = dateTimePicker2.Value;
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            if (dateTimePicker2.Value < dateTimePicker1.Value)
            {
                dateTimePicker2.Value = dateTimePicker1.Value;
            }
        }

        private void btAnular_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}

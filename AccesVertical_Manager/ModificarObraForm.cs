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
    public partial class ModificarObraForm : Form
    {

        private Connection mysqlconnect;
        private string Expedient;
        public ModificarObraForm(Connection mysqlconnect, string Expedient)
        {
            
            this.mysqlconnect = mysqlconnect;
            this.Expedient = Expedient;
            InitializeComponent();
            InitializeComboBoxTecnic();
            SelectedValues();
        }


        private void InitializeComboBoxTecnic()
        {

            try
            {
                MySqlConnection conn = mysqlconnect.getmysqlconn();

                string query = "SELECT    TècnicID,  `Nom i cognoms` from Tècnics";

                MySqlCommand cmd = new MySqlCommand(query, conn);
                MySqlDataAdapter mdaDades = new MySqlDataAdapter(cmd);
                DataTable DtDadesInformat = new DataTable();

                mdaDades.Fill(DtDadesInformat);
                comboBoxTecnics.ValueMember = "TècnicID";
                comboBoxTecnics.DisplayMember = "Nom i cognoms";
                comboBoxTecnics.DataSource = DtDadesInformat;

                conn.Close();
                //comboBoxEmpresa.SelectedValue = "";
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);

            }

        }
        private void SelectedValues()
        {

            try
            {
                MySqlConnection conn = mysqlconnect.getmysqlconn();
                DataTable DtDades = new DataTable();
                string query = "Select `Expedient`,`Client`,`CIF`,`Nº de Pressupost`,`Activitat`,`Comentaris i recomenacions`,`Tècnic`,`Unitats d'Obra`,"
                + "`Contacte 1`,`Telèfon contacte 1`,`Email contacte 1`,`Contacte 2`,`Telèfon contacte 2`,`Email contacte 2`,"
                + "`Industrial 1`,`Contacte Industrial 1`,`Telèfon Industrial 1`,`Email Industrial 1`,"
                 + "`Industrial 2`,`Contacte Industrial 2`,`Telèfon Industrial 2`,`Email Industrial 2`,"
                  + "`Industrial 3`,`Contacte Industrial 3`,`Telèfon Industrial 3`,`Email Industrial 3`,"
                   + "`Industrial 4`,`Contacte Industrial 4`,`Telèfon Industrial 4`,`Email Industrial 4`"
                + " from Obres"
                + " WHERE  ( Expedient=@Expedient);";
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Expedient", Expedient);
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {

                    txtBNumExpedient.Text = !reader.IsDBNull(0) ? reader.GetString(0) : null;
                    txtBClient.Text = !reader.IsDBNull(1) ? reader.GetString(1) : null;
                    txtBCIF.Text = !reader.IsDBNull(2) ? reader.GetString(2) : null;
                    txtBNumPressupost.Text = !reader.IsDBNull(3) ? reader.GetString(3) : null;
                   comboBoxActivitat.SelectedItem = !reader.IsDBNull(4) ? reader.GetString(4) : null;
                    textBoxComentaris.Text = !reader.IsDBNull(5) ? reader.GetString(5) : null;
                    comboBoxTecnics.SelectedValue = !reader.IsDBNull(6) ? reader.GetInt32(6) : 0;
                    numericUpDown1.Value = !reader.IsDBNull(7) ? reader.GetInt32(7) : 0;
                    txtBNomContacte1.Text= !reader.IsDBNull(8) ? reader.GetString(8) : null;
                    txtBTelefonContacte1.Text = !reader.IsDBNull(9) ? reader.GetString(9) : null;
                    txtBEmailContacte1.Text = !reader.IsDBNull(10) ? reader.GetString(10) : null;
                    txtBNomContacte2.Text = !reader.IsDBNull(11) ? reader.GetString(11) : null;
                    txtBTelefonContacte2.Text = !reader.IsDBNull(12) ? reader.GetString(12) : null;
                    txtBEmailContacte2.Text = !reader.IsDBNull(13) ? reader.GetString(13) : null;
                    txtBIndustrial1.Text= !reader.IsDBNull(14) ? reader.GetString(14) : null;
                    txtBContacteIndustrial1.Text = !reader.IsDBNull(15) ? reader.GetString(15) : null;
                    txtBTelefonIndustrial1.Text = !reader.IsDBNull(16) ? reader.GetString(16) : null;
                    txtBEmailIndustrial1.Text = !reader.IsDBNull(17) ? reader.GetString(17) : null;
                    txtBIndustrial2.Text = !reader.IsDBNull(18) ? reader.GetString(18) : null;
                    txtBContacteIndustrial2.Text = !reader.IsDBNull(19) ? reader.GetString(19) : null;
                    txtBTelefonIndustrial2.Text = !reader.IsDBNull(20) ? reader.GetString(20) : null;
                    txtBEmailIndustrial2.Text = !reader.IsDBNull(21) ? reader.GetString(21) : null;
                    txtBIndustrial3.Text = !reader.IsDBNull(22) ? reader.GetString(22) : null;
                    txtBContacteIndustrial3.Text = !reader.IsDBNull(23) ? reader.GetString(23) : null;
                    txtBTelefonIndustrial3.Text = !reader.IsDBNull(24) ? reader.GetString(24) : null;
                    txtBEmailIndustrial3.Text = !reader.IsDBNull(25) ? reader.GetString(25) : null;
                    txtBIndustrial4.Text = !reader.IsDBNull(26) ? reader.GetString(26) : null;
                    txtBContacteIndustrial4.Text = !reader.IsDBNull(27) ? reader.GetString(27) : null;
                    txtBTelefonIndustrial4.Text = !reader.IsDBNull(28) ? reader.GetString(28) : null;
                    txtBEmailIndustrial4.Text = !reader.IsDBNull(29) ? reader.GetString(29) : null;
                    conn.Close();
                }






            }

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
                string Query = "UPDATE Obres SET `Expedient`=@Expedient,Client=@Client,CIF=@CIF,"
                    + "`Nº de Pressupost`=@NPressupost,`Comentaris i recomenacions`=@Comentaris,`Activitat`=@Activitat,"
                    + "`Tècnic`=@Tecnic,`Unitats d'Obra`=@Unitatsdobra,`Contacte 1`=@Contacte1,"
                   + "`Telèfon contacte 1`=@TelefonContacte1,`Email contacte 1`=@Emailcontacte1,`Contacte 2`=@Contacte2,"
                   + "`Telèfon contacte 2`=@TelefonContacte2,`Email contacte 2`=@EmailContacte2,"
                  + "`Industrial 1`=@Industrial1,`Contacte Industrial 1`=@ContacteIndustrial1,`Telèfon Industrial 1`=@TelefonIndustrial1,`Email Industrial 1`=@EmailIndustrial1,"
                  + "`Industrial 2`=@Industrial2,`Contacte Industrial 2`=@ContacteIndustrial1,`Telèfon Industrial 2`=@TelefonIndustrial2,`Email Industrial 2`=@EmailIndustrial2,"
                   + "`Industrial 3`=@Industrial3,`Contacte Industrial 3`=@ContacteIndustrial3,`Telèfon Industrial 3`=@TelefonIndustrial3,`Email Industrial 3`=@EmailIndustrial3,"
                    + "`Industrial 4`=@Industrial4,`Contacte Industrial 4`=@ContacteIndustrial4,`Telèfon Industrial 4`=@TelefonIndustrial4,`Email Industrial 4`=@EmailIndustrial4"
                  + " WHERE `Expedient`=@ExpedientOriginal";
                MySqlCommand cmd = new MySqlCommand(Query, conn);
                cmd.Parameters.AddWithValue("@Expedient", txtBNumExpedient.Text);
                cmd.Parameters.AddWithValue("@ExpedientOriginal", Expedient);
                cmd.Parameters.AddWithValue("@Client", txtBClient.Text);
                cmd.Parameters.AddWithValue("@CIF", txtBCIF.Text);
                cmd.Parameters.AddWithValue("@NPressupost", txtBNumPressupost.Text);
                cmd.Parameters.AddWithValue("@Activitat", comboBoxActivitat.SelectedItem);
                cmd.Parameters.AddWithValue("@Comentaris", textBoxComentaris.Text);
                cmd.Parameters.AddWithValue("@Tecnic", comboBoxTecnics.SelectedValue);
                cmd.Parameters.AddWithValue("@Unitatsdobra", numericUpDown1.Value);
                cmd.Parameters.AddWithValue("@Contacte1", txtBNomContacte1.Text);
                cmd.Parameters.AddWithValue("@TelefonContacte1", txtBTelefonContacte1.Text);
                cmd.Parameters.AddWithValue("@Emailcontacte1", txtBEmailContacte1.Text);
                cmd.Parameters.AddWithValue("@Contacte2", txtBNomContacte2.Text);
                cmd.Parameters.AddWithValue("@TelefonContacte2", txtBTelefonContacte2.Text);
                cmd.Parameters.AddWithValue("@EmailContacte2", txtBEmailContacte2.Text);
                cmd.Parameters.AddWithValue("@Industrial1", txtBIndustrial1.Text);
                cmd.Parameters.AddWithValue("@ContacteIndustrial1", txtBContacteIndustrial1.Text);
                cmd.Parameters.AddWithValue("@TelefonIndustrial1", txtBTelefonIndustrial1.Text);
                cmd.Parameters.AddWithValue("@EmailIndustrial1", txtBEmailIndustrial1.Text);
                cmd.Parameters.AddWithValue("@Industrial2", txtBIndustrial2.Text);
                cmd.Parameters.AddWithValue("@ContacteIndustrial2", txtBContacteIndustrial2.Text);
                cmd.Parameters.AddWithValue("@TelefonIndustrial2", txtBTelefonIndustrial2.Text);
                cmd.Parameters.AddWithValue("@EmailIndustrial2", txtBEmailIndustrial2.Text);
                cmd.Parameters.AddWithValue("@Industrial3", txtBIndustrial3.Text);
                cmd.Parameters.AddWithValue("@ContacteIndustrial3", txtBContacteIndustrial3.Text);
                cmd.Parameters.AddWithValue("@TelefonIndustrial3", txtBTelefonIndustrial3.Text);
                cmd.Parameters.AddWithValue("@EmailIndustrial3", txtBEmailIndustrial3.Text);
                cmd.Parameters.AddWithValue("@Industrial4", txtBIndustrial4.Text);
                cmd.Parameters.AddWithValue("@ContacteIndustrial4", txtBContacteIndustrial4.Text);
                cmd.Parameters.AddWithValue("@TelefonIndustrial4", txtBTelefonIndustrial4.Text);
                cmd.Parameters.AddWithValue("@EmailIndustrial4", txtBEmailIndustrial4.Text);
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

        private void btCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}

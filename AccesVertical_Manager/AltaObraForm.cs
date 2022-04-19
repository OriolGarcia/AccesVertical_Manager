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
    public partial class AltaObraForm : Form
    {

        private Connection mysqlconnect;
        public AltaObraForm(Connection connect)
        {
            mysqlconnect = connect;
            InitializeComponent();
            InitializeComboBoxTecnic();
        }

        private void txtBNumExpedient_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
        (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

       
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

        private void btInserir_Click(object sender, EventArgs e)
        {


            MySqlConnection conn = mysqlconnect.getmysqlconn();
            try
            {

               
                string Query = "INSERT INTO Obres(Expedient,Client,CIF,`Nº de Pressupost`,Activitat,`Comentaris i recomenacions`,"
                   + "Tècnic,`Unitats d'Obra`,`Contacte 1`,`Telèfon contacte 1`,`Email contacte 1`,`Contacte 2`,"
                  + "`Telèfon contacte 2`,`Email contacte 2`,`Industrial 1`,`Contacte Industrial 1`,`Telèfon Industrial 1`,`Email Industrial 1`,"
                 + "`Industrial 2`,`Contacte Industrial 2`,`Telèfon Industrial 2`,`Email Industrial 2`,"
                 + "`Industrial 3`,`Contacte Industrial 3`,`Telèfon Industrial 3`,`Email Industrial 3`,"
                 + "`Industrial 4`,`Contacte Industrial 4`,`Telèfon Industrial 4`,`Email Industrial 4`"

                  + ") "
                  + "VALUES(@Expedient,@Client,@CIF,@NPressupost,@Activitat,@Comentaris,@Tecnic,@Unitatsdobra,@Contacte1,@TelefonContacte1,@Emailcontacte1,"
                  + "@Contacte2,@TelefonContacte2,@EmailContacte2,@Industrial1,@ContacteIndustrial1,@TelefonIndustrial1,@EmailIndustrial1,"
                  + "@Industrial2, @ContacteIndustrial2, @TelefonIndustrial2, @EmailIndustrial2,"
                  +"@Industrial3,@ContacteIndustrial3,@TelefonIndustrial3,@EmailIndustrial3,"
                  + "@Industrial4, @ContacteIndustrial4, @TelefonIndustrial4, @EmailIndustrial4"
                          + "); ";
                MySqlCommand cmd = new MySqlCommand(Query, conn);
                cmd.Parameters.AddWithValue("@Expedient", txtBNumExpedient.Text);
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

        private void btCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void txtBNumPressupost_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
      (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }
    }
}

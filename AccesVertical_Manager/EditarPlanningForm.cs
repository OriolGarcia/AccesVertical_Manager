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
    public partial class EditarPlanningForm : Form
    {


        private Connection mysqlconnect;
        private string Expedient;
        private int UnitatsObra;
        private DateTime datainici;
        private List<DateTime> dates;
        public EditarPlanningForm(Connection mysqlconnect, string Expedient)
        {

            this.mysqlconnect = mysqlconnect;
            this.Expedient = Expedient;
            InitializeComponent();
            dateTimePickerDataInici.Enabled = false;
            comboBoxTecnics.Enabled = false;
            lbExpedient.Text = "Num.Expedient:" + Expedient;
           
           InitializeComboBoxTecnic();
          InitializeGridViewOperarisperDefecte();
           SelectedValues();
            InitializeDataGridViewDates();
        }
        private void SelectedValues()
        {

            try
            {
                MySqlConnection conn = mysqlconnect.getmysqlconn();
                DataTable DtDades = new DataTable();
                string query = "Select `Expedient`,`Tècnic`,`Unitats d'Obra`,`Documentacio OK`"
               + " from Obres"
                + " WHERE  ( Expedient=@Expedient);";
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Expedient", Expedient);
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {


                    comboBoxTecnics.SelectedValue = !reader.IsDBNull(1) ? reader.GetInt32(1) : 0;
                    UnitatsObra = !reader.IsDBNull(2) ? reader.GetInt32(2) : 0;
                    lbUnitatsObra.Text = "Unitats d'Obra: " + UnitatsObra;
                    chckBDockOK.Checked = !reader.IsDBNull(3) ? reader.GetBoolean(3) : false;
                    conn.Close();
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }
        private void SelectDates()
        {
            try
            {

                MySqlConnection conn = mysqlconnect.getmysqlconn();
                DataTable DtDades = new DataTable();
                string query = " Select Data"
                    + " from OperarisObraData where `Obra`= @Expedient group by Data; ";
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Expedient", Expedient);
                MySqlDataReader reader2 = cmd.ExecuteReader();
                dates = new List<DateTime>();
              
                while (reader2.Read())
                {

                    dates.Add(!reader2.IsDBNull(0) ? reader2.GetDateTime(0) : DateTime.Now);
                }

                conn.Close();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);

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

        private void InitializeGridViewOperarisperDefecte()
        {

            try
            {
                MySqlConnection conn = mysqlconnect.getmysqlconn();
                DataTable DtDades = new DataTable();
                string query = "Select OperariID, FotografiaPath,Nom,Cognoms,DNI from operaris "
               + " INNER JOIN OperarisObraDefecte ON Operaris.`OperariID`=OperarisObraDefecte.`Operari` "
                + " WHERE  (OperarisObraDefecte.`Obra`=@ObraID"
                  + " )";


                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@ObraID", Expedient);


                MySqlDataAdapter mdaDades = new MySqlDataAdapter(cmd);
                //+ dTPinici.Value.Date.ToString("yyyy-MM-dd HH:mm") +

                mdaDades.Fill(DtDades);
                dataGridViewOperarisperDefecte.DataSource = DtDades;
                dataGridViewOperarisperDefecte.RowHeadersVisible = false;
                dataGridViewOperarisperDefecte.AllowUserToAddRows = false;
                dataGridViewOperarisperDefecte.Columns["OperariID"].Visible = false;
                dataGridViewOperarisperDefecte.Columns["FotografiaPath"].Visible = false;
                conn.Close();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }

        }
        private void InitializeDataGridViewDates()
        {


            try
            {
                MySqlConnection conn = mysqlconnect.getmysqlconn();
                DataTable DtDades = new DataTable();
                string query = "Select DATE_FORMAT(Data, '%d/%m/%Y') as `Dates` "
                    + " from OperarisObraData where `Obra`= @ObraID group by Data; ";



                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@ObraID", Expedient);


                MySqlDataAdapter mdaDades = new MySqlDataAdapter(cmd);
                //+ dTPinici.Value.Date.ToString("yyyy-MM-dd HH:mm") +

                mdaDades.Fill(DtDades);
                dataGridViewDates.DataSource = DtDades;
                dataGridViewDates.RowHeadersVisible = false;
                dataGridViewDates.AllowUserToAddRows = false;
                conn.Close();
                SelectDates();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }

        }






        private void btGenerarPlanning_Click(object sender, EventArgs e)
        {

        }

        private void btAfegirData_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime datainici;
                if (dates.Count > 0) datainici = dates.ElementAt(0);
                else datainici = new DateTime();
                if (dateTimePickerAfegir.Value > datainici)
                {
                    if (mysqlconnect.getPermissions()[0] || dateTimePickerAfegir.Value >= DateTime.Now)
                    {
                        if (!dates.Contains(dateTimePickerAfegir.Value))
                        {
                            MySqlConnection conn = mysqlconnect.getmysqlconn();

                            DataTable DtDades3 = new DataTable();
                            string query = "Select * from Absencies";

                            MySqlCommand cmd = new MySqlCommand(query, conn);
                            conn.Close();
                            MySqlDataAdapter mdaDades3 = new MySqlDataAdapter(cmd);
                            mdaDades3.Fill(DtDades3);
                            dataGridViewDates.Rows.Count.ToString();
                            DataTable DtDades4 = new DataTable();
                            query = "Select * from OperarisObraData";

                            cmd = new MySqlCommand(query, conn);
                            conn.Close();
                            MySqlDataAdapter mdaDades4 = new MySqlDataAdapter(cmd);
                            mdaDades4.Fill(DtDades4);
                            for (int i = 0; i < dataGridViewOperarisperDefecte.Rows.Count; i++)
                            {
                                DateTime data = dateTimePickerAfegir.Value;
                                int n3, n4 = 0;

                                string Operari = dataGridViewOperarisperDefecte.Rows[i].Cells["OperariID"].Value.ToString();

                                n3 = DtDades3.Select("[Data] ='" + data.ToString("yyyy-MM-dd HH:mm") + "'  AND [Operari] =" + Operari).Length;
                                n4 = DtDades4.Select("[Data] ='" + data.ToString("yyyy-MM-dd HH:mm") + "'  AND [Operari] =" + Operari).Length;

                                if (n3 == 0 && n4 == 0)
                                {

                                    string Query;


                                    if (chkBRecalcular.Checked)
                                    {
                                        if (dates.Count > 0) data = dates.ElementAt(dates.Count - 1);
                                        else data = new DateTime();
                                        Query = "delete from OperarisObraData  where (Operari,Obra,Data) in ((@OperariID,@Obra,@Data));";
                                        cmd = new MySqlCommand(Query, conn);
                                        cmd.Parameters.AddWithValue("@OperariID", Operari);
                                        cmd.Parameters.AddWithValue("@Obra", Expedient);
                                        cmd.Parameters.AddWithValue("@Data", data.ToString("yyyy-MM-dd HH:mm:ss"));
                                        conn.Open();
                                        cmd.ExecuteReader();
                                        conn.Close();
                                    }

                                    data = dateTimePickerAfegir.Value;
                                    Query = "INSERT INTO OperarisObraData(Operari,`Obra`,`Data`) "
                                         + "VALUES(@OperariID,@Expedient, @Data); ";
                                    cmd = new MySqlCommand(Query, conn);
                                    cmd.Parameters.AddWithValue("@OperariID", Operari);
                                    cmd.Parameters.AddWithValue("@Expedient", Expedient);
                                    cmd.Parameters.AddWithValue("@Data", data.ToString("yyyy-MM-dd HH:mm:ss"));
                                    conn.Open();
                                    cmd.ExecuteReader();
                                    conn.Close();

                                }
                            }
                        }
                        else { MessageBox.Show("Aquesta data ja està inclosa"); }
                    }
                    else { MessageBox.Show("No pots editar una data anterior a la actual sense permísos de administrador."); }
                    }
                else { MessageBox.Show("No pots afegir una data anterior a la data d'inici"); }
                InitializeDataGridViewDates();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);

            }

        }

        private void btEliminarData_Click(object sender, EventArgs e)
        {
            MySqlConnection conn = mysqlconnect.getmysqlconn();
            try
            {
                int selectedRowCount = dataGridViewDates.Rows.GetRowCount(DataGridViewElementStates.Selected);
                if (selectedRowCount > 0)
                {
                    DateTime datainsersio = Convert.ToDateTime(dataGridViewDates.Rows[dataGridViewDates.Rows.Count-1].Cells[0].Value.ToString());
                     DateTime data = Convert.ToDateTime(dataGridViewDates.SelectedRows[0].Cells[0].Value.ToString());
                    if (mysqlconnect.getPermissions()[0] || data >= DateTime.Now)
                    {
                        int UOesborrades = 0;
                        for (int c = 0; c < selectedRowCount; c++)
                        {

                           

                            for (int i = 0; i < dataGridViewOperarisperDefecte.Rows.Count; i++)
                            {

                                string Query;
                                MySqlCommand cmd;
                                string Operari = dataGridViewOperarisperDefecte.Rows[i].Cells["OperariID"].Value.ToString();

                                Query = "delete from OperarisObraData  where (Operari,Obra,Data) in ((@OperariID,@Obra,@Data));";
                                cmd = new MySqlCommand(Query, conn);
                                cmd.Parameters.AddWithValue("@OperariID", Operari);
                                cmd.Parameters.AddWithValue("@Obra", Expedient);
                                cmd.Parameters.AddWithValue("@Data", data.ToString("yyyy-MM-dd HH:mm:ss"));
                                conn.Open();
                                cmd.ExecuteReader();
                                conn.Close();
                                UOesborrades++;
                            }


                        }
                        if (chkBRecalcular.Checked)
                        {

                            DataTable DtDades = new DataTable();
                            string query = "Select DATE_FORMAT(Data,'%d/%m')as `Data` from FestiusGeneral";

                            MySqlCommand cmd = new MySqlCommand(query, conn);
                            conn.Close();
                            MySqlDataAdapter mdaDades = new MySqlDataAdapter(cmd);
                            mdaDades.Fill(DtDades);
                            DataTable DtDades2 = new DataTable();
                            query = "Select * from FestiusAnualsOcasionals";

                            cmd = new MySqlCommand(query, conn);
                            conn.Close();
                            MySqlDataAdapter mdaDades2 = new MySqlDataAdapter(cmd);
                            mdaDades2.Fill(DtDades2);
                            DataTable DtDades3 = new DataTable();
                            query = "Select * from Absencies";

                            cmd = new MySqlCommand(query, conn);
                            conn.Close();
                            MySqlDataAdapter mdaDades3 = new MySqlDataAdapter(cmd);
                            mdaDades3.Fill(DtDades3);
                            dataGridViewDates.Rows.Count.ToString();
                            DataTable DtDades4 = new DataTable();
                            query = "Select * from OperarisObraData";

                            cmd = new MySqlCommand(query, conn);
                            conn.Close();
                            MySqlDataAdapter mdaDades4 = new MySqlDataAdapter(cmd);
                            mdaDades4.Fill(DtDades4);

                            datainsersio = datainsersio.AddDays(1);
                            for (int UO = 0; UO < UOesborrades;)
                            {

                                for (int i = 0; i < dataGridViewOperarisperDefecte.Rows.Count; i++)
                                {


                                    int n1 = DtDades.Select("[Data] ='" + datainsersio.ToString("dd/MM") + "'").Length;

                                    int n2 = DtDades2.Select("[Data] ='" + datainsersio.ToString("yyyy-MM-dd") + "'").Length;
                                    int n3, n4 = 0;

                                    string Operari = dataGridViewOperarisperDefecte.Rows[i].Cells["OperariID"].Value.ToString();

                                    n3 = DtDades3.Select("[Data] ='" + datainsersio.ToString("yyyy-MM-dd HH:mm") + "'  AND [Operari] =" + Operari).Length;
                                    n4 = DtDades4.Select("[Data] ='" + datainsersio.ToString("yyyy-MM-dd HH:mm") + "'  AND [Operari] =" + Operari).Length;


                                    if (n1 == 0 && n2 == 0 && (int)datainsersio.DayOfWeek != 0 && (int)datainsersio.DayOfWeek != 6 && n3 < 1 && n4 < 1)
                                    {

                                        string Query;



                                        Query = "INSERT INTO OperarisObraData(Operari,`Obra`,`Data`) "
                                     + "VALUES(@OperariID,@Expedient, @Data); ";
                                        cmd = new MySqlCommand(Query, conn);
                                        cmd.Parameters.AddWithValue("@OperariID", Operari);
                                        cmd.Parameters.AddWithValue("@Expedient", Expedient);
                                        cmd.Parameters.AddWithValue("@Data", datainsersio.ToString("yyyy-MM-dd HH:mm:ss"));
                                        conn.Open();
                                        cmd.ExecuteReader();
                                        conn.Close();
                                        UO++;
                                    }


                                }
                                datainsersio = datainsersio.AddDays(1);
                            }
                        }
                    }
                    else {
                        MessageBox.Show("No pots elminar una data anterior a la actual si no tens permisos de Supervisor!");
                    }
                }


            }   catch (Exception err)
            {
                MessageBox.Show(err.Message);

            }finally {
                InitializeDataGridViewDates();
                conn.Close(); }

        }
        private void btTancar_Click(object sender, EventArgs e)
        {
            MySqlConnection conn = mysqlconnect.getmysqlconn();
            try {
             
               String Query = "UPDATE Obres SET `Tècnic`=@Tecnic,`Bloquejar Planning`=@BloqueigPlanning, "
                      + "`Plàning Generat`=true,`Documentacio OK`=@DocOK WHERE `Expedient`=@Expedient";
                MySqlCommand cmd = new MySqlCommand(Query, conn);
                cmd.Parameters.AddWithValue("@Tecnic", comboBoxTecnics.SelectedValue);
                cmd.Parameters.AddWithValue("@BloqueigPlanning", chckBBloquejarPlanning.Checked);
                cmd.Parameters.AddWithValue("@DocOK", chckBDockOK.Checked);
                cmd.Parameters.AddWithValue("@Expedient", Expedient);
                conn.Open();
                cmd.ExecuteReader();

                conn.Close();
            } catch (Exception err) {

                MessageBox.Show(err.Message);
                conn.Close();
            }


            Close();
        }
    }
}

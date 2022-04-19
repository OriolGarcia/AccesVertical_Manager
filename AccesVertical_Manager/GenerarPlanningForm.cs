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
    public partial class GenerarPlanningForm : Form
    {

        private Connection mysqlconnect;
        private string Expedient;
        private int UnitatsObra;
        private DateTime datainici;
        public GenerarPlanningForm(Connection mysqlconnect, string Expedient)
        {

            this.mysqlconnect = mysqlconnect;
            this.Expedient = Expedient;
            InitializeComponent();
            comboBoxTecnics.Enabled = false;
            lbExpedient.Text = "Num.Expedient:" + Expedient;
            dataGridViewDates.Columns["Dates"].DefaultCellStyle.Format = "dd/MM/yyyy";
            EsborrarOperisObraDates();
            InitializeComboBoxTecnic();
            InitializeGridViewOperarisperDefecte();
            SelectedValues();
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
                    chckBDockOK.Checked= !reader.IsDBNull(3) ? reader.GetBoolean(3) : false;
                    lbUnitatsObra.Text = "Unitats d'Obra: " + UnitatsObra;

                    conn.Close();
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            } }

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

     
        private void dateTimePickerDataInici_ValueChanged(object sender, EventArgs e)
        {
            datainici = dateTimePickerDataInici.Value.Date;
           
            try
            {
                dataGridViewDates.Columns["Dates"].SortMode = DataGridViewColumnSortMode.Programmatic;
                dataGridViewDates.Rows.Clear();
                MySqlConnection conn = mysqlconnect.getmysqlconn();
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
                DataTable DtDades3= new DataTable();
                query = "Select * from Absencies";

                cmd = new MySqlCommand(query, conn);
                conn.Close();
                MySqlDataAdapter mdaDades3 = new MySqlDataAdapter(cmd);
                mdaDades3.Fill(DtDades3);
                DataTable DtDades4= new DataTable();
                query = "Select * from OperarisObraData";

                cmd = new MySqlCommand(query, conn);
                conn.Close();
                MySqlDataAdapter mdaDades4 = new MySqlDataAdapter(cmd);
                mdaDades4.Fill(DtDades4);
                DateTime data = datainici;
               
                for (int UO = 0; UO < UnitatsObra;)
                {
                   
                    int n1 = DtDades.Select("[Data] ='" + data.ToString("dd/MM") + "'").Length;

                    int n2 = DtDades2.Select("[Data] ='" + data.ToString("yyyy-MM-dd HH:mm") + "'").Length;
                   
                    int operarisnodisponibles = 0; ;
                    for (int i = 0; i < dataGridViewOperarisperDefecte.Rows.Count; i++)
                    {
                        string Operari = dataGridViewOperarisperDefecte.Rows[i].Cells["OperariID"].Value.ToString();
                        int n3 = DtDades3.Select("[Data] ='" + data.ToString("yyyy-MM-dd HH:mm") + "'  AND [Operari] ="+Operari).Length;
                        int  n4= DtDades4.Select("[Data] ='" + data.ToString("yyyy-MM-dd HH:mm") + "'  AND [Operari] =" + Operari).Length;
                        
                        if (n3 > 0 || n4 > 0) operarisnodisponibles++;
                    }
                    
                    if (n1 == 0&&n2==0 && (int)data.DayOfWeek != 0 && (int)data.DayOfWeek != 6 &&operarisnodisponibles< dataGridViewOperarisperDefecte.Rows.Count)
                    {
                      
                        string[] row = new string[] { String.Format("{0:dd/MM/yyyy}", data.ToString("dd/MM/yyyy")) };
                        dataGridViewDates.Rows.Add(row);

                        //new DatRow(Format(Dt_Fecha, "dd-mm-yyyy")
                        UO += dataGridViewOperarisperDefecte.Rows.Count-operarisnodisponibles;
                    }
                  
                    data = data.AddDays(1);
                }
                dataGridViewDates.Sort(new RowComparer(SortOrder.Ascending));
            }

            catch (Exception err)
            {
                MessageBox.Show(err.Message);

            }

        }


        private void btGenerarPlanning_Click(object sender, EventArgs e)
        {
           

            MySqlConnection conn = mysqlconnect.getmysqlconn();
            for (int i = 0; i < dataGridViewOperarisperDefecte.Rows.Count; i++)
            {
                 try
                {
                    DataTable DtDades3 = new DataTable();
                   string query = "Select * from Absencies";

                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    conn.Close();
                    MySqlDataAdapter mdaDades3 = new MySqlDataAdapter(cmd);
                    mdaDades3.Fill(DtDades3);
                    string Query;
                    DataTable DtDades4 = new DataTable();
                    query = "Select * from OperarisObraData";

                    cmd = new MySqlCommand(query, conn);
                    conn.Close();
                    MySqlDataAdapter mdaDades4 = new MySqlDataAdapter(cmd);
                    mdaDades4.Fill(DtDades4);
                    string Operari = dataGridViewOperarisperDefecte.Rows[i].Cells["OperariID"].Value.ToString();
                for(int j = 0; j < dataGridViewDates.Rows.Count; j++) {

                        DateTime data = Convert.ToDateTime(dataGridViewDates.Rows[j].Cells[0].Value.ToString());
                        int  n = DtDades3.Select("[Data] ='" + data.ToString("yyyy-MM-dd HH:mm") + "'  AND [Operari] =" + Operari).Length;
                        int n2 = DtDades4.Select("[Data] ='" + data.ToString("yyyy-MM-dd HH:mm") + "'  AND [Operari] =" + Operari).Length;
                       if (n == 0&& n2==0)
                        {
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


                     Query = "UPDATE Obres SET `Tècnic`=@Tecnic,`Bloquejar Planning`=@BloqueigPlanning, " 
                      + "`Plàning Generat`=true,`Documentacio OK`=@DocOK WHERE `Expedient`=@Expedient";
                    cmd = new MySqlCommand(Query, conn);
                    cmd.Parameters.AddWithValue("@Tecnic", comboBoxTecnics.SelectedValue);
                    cmd.Parameters.AddWithValue("@BloqueigPlanning", chckBBloquejarPlanning.Checked);
                    cmd.Parameters.AddWithValue("@DocOK", chckBDockOK.Checked);
                    cmd.Parameters.AddWithValue("@Expedient", Expedient);
                    conn.Open();
                    cmd.ExecuteReader();

                    conn.Close();


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
            this.Close();
        }
        private void EsborrarOperisObraDates()
        {  MySqlConnection conn = mysqlconnect.getmysqlconn();
          
                try
                {
                  
                    string Query = "delete from OperarisObraData  where (Obra) in ((@Obra));";
                   MySqlCommand cmd = new MySqlCommand(Query, conn);
                   cmd.Parameters.AddWithValue("@Obra", Expedient);
                    MySqlDataReader MyReader2;
                    conn.Open();
                    MyReader2 = cmd.ExecuteReader();     // Here our query will be executed and data saved into the database. 
                    MyReader2.Close();
                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally { conn.Close(); }
            
        }
        private void btAfegirData_Click(object sender, EventArgs e)
        { try
            {
                if (dateTimePickerAfegir.Value > dateTimePickerDataInici.Value)
                {

                    string[] Dates = new string[dataGridViewDates.Rows.Count]; ;
                    if (dataGridViewDates.Rows.Count>0) {
                        for (int i = 0; i < dataGridViewDates.Rows.Count; i++)
                        {
                            Dates[i] = dataGridViewDates.Rows[i].Cells["Dates"].Value.ToString();

                        }
                    }


                    if (!Dates.Contains(dateTimePickerAfegir.Value.ToString("dd/MM/yyyy")))
                    {
                        dataGridViewDates.Sort(new RowComparer(SortOrder.Ascending));
                        if (chkBRecalcular.Checked)
                        {
                            if (dataGridViewDates.Rows.Count > 0)
                                this.dataGridViewDates.Rows.RemoveAt(this.dataGridViewDates.Rows.Count - 1);
                        }
                        //ficar data
                        DateTime data = dateTimePickerAfegir.Value;
                        string[] row = new string[] { String.Format("{0:dd/MM/yyyy}", data.ToString("dd/MM/yyyy")) };
                        dataGridViewDates.Rows.Add(row);
                    }
                    else { MessageBox.Show("Aquesta data ja està inclosa"); }
                    dataGridViewDates.Sort(new RowComparer(SortOrder.Ascending));
                }
                else { MessageBox.Show("No pots afegir una data anterior a la data d'inici"); }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);

            }

        }
        private class RowComparer : System.Collections.IComparer
        {
            private static int sortOrderModifier = 1;

            public RowComparer(SortOrder sortOrder)
            {
                if (sortOrder == SortOrder.Descending)
                {
                    sortOrderModifier = -1;
                }
                else if (sortOrder == SortOrder.Ascending)
                {
                    sortOrderModifier = 1;
                }

            }

          public int Compare(object x, object y)
            {
                DataGridViewRow DataGridViewRow1 = (DataGridViewRow)x;
                DataGridViewRow DataGridViewRow2 = (DataGridViewRow)y;
                DateTime dt1 = Convert.ToDateTime(DataGridViewRow1.Cells[0].Value.ToString());
                DateTime dt2 = Convert.ToDateTime(DataGridViewRow2.Cells[0].Value.ToString());
                // Try to sort based on the Last Name column.
                int CompareResult = System.DateTime.Compare(dt1,dt2);

                // If the Last Names are equal, sort based on the First Name.
             return CompareResult * sortOrderModifier;
            }
        }

        private void btEliminarData_Click(object sender, EventArgs e)
        {
            int selectedRowCount = dataGridViewDates.Rows.GetRowCount(DataGridViewElementStates.Selected);
            if (selectedRowCount > 0)
            {
                var confirmEliminar = MessageBox.Show("Segur que vols eliminar les files seleccionades?",
                    "Confirmació d'Eliminació!",
                    MessageBoxButtons.YesNo);
                if (confirmEliminar == DialogResult.Yes)
                {
                    try
                    {
                        //MessageBox.Show("1");
                        dataGridViewDates.Columns["Dates"].SortMode = DataGridViewColumnSortMode.Programmatic;

                        MySqlConnection conn = mysqlconnect.getmysqlconn();
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
                        DateTime data = Convert.ToDateTime(dataGridViewDates.Rows[dataGridViewDates.Rows.Count - 1].Cells[0].Value.ToString());
                        data = data.AddDays(1);
                        int[] UOs = new int[selectedRowCount];
                        //MessageBox.Show("2");
                        for (int i = 0; i < selectedRowCount; i++)
                        {
                            UOs[i] = 0;
                            DateTime dataEsborrar = Convert.ToDateTime(dataGridViewDates.SelectedRows[0].Cells[0].Value.ToString());
                            int n1 = DtDades.Select("[Data] ='" + dataEsborrar.ToString("dd/MM") + "'").Length;
                            int n2 = DtDades2.Select("[Data] ='" + dataEsborrar.ToString("yyyy-MM-dd HH:mm") + "'").Length;
                            int operarisnodisponibles = 0; ;
                            for (int t = 0; t < dataGridViewOperarisperDefecte.Rows.Count; t++)
                            {
                                string Operari = dataGridViewOperarisperDefecte.Rows[t].Cells["OperariID"].Value.ToString();
                                int n3 = DtDades3.Select("[Data] ='" + dataEsborrar.ToString("yyyy-MM-dd HH:mm") + "'  AND [Operari] =" + Operari).Length;
                                int n4 = DtDades4.Select("[Data] ='" + dataEsborrar.ToString("yyyy-MM-dd HH:mm") + "'  AND [Operari] =" + Operari).Length;

                                if (n3 > 0 || n4 > 0) operarisnodisponibles++;
                            }


                            UOs[i] = dataGridViewOperarisperDefecte.Rows.Count - operarisnodisponibles;
                              
                           
                            dataGridViewDates.Rows.Remove(dataGridViewDates.SelectedRows[0]);
                        }
                        if (chkBRecalcular.Checked)
                        {
                            for (int k = 0; k < UOs.Length; k++)
                            {
                                
                                for (int UO = 0; UO < UOs[k];)
                                {
                                   
                                    int n1 = DtDades.Select("[Data] ='" + data.ToString("dd/MM") + "'").Length;
                                    int n2 = DtDades2.Select("[Data] ='" + data.ToString("yyyy-MM-dd HH:mm") + "'").Length;
                                    int operarisnodisponibles = 0; ;
                                    for (int i = 0; i < dataGridViewOperarisperDefecte.Rows.Count; i++)
                                    {

                                        string Operari = dataGridViewOperarisperDefecte.Rows[i].Cells["OperariID"].Value.ToString();
                                        int n3 = DtDades3.Select("[Data] ='" + data.ToString("yyyy-MM-dd HH:mm") + "'  AND [Operari] =" + Operari).Length;
                                        int n4 = DtDades4.Select("[Data] ='" + data.ToString("yyyy-MM-dd HH:mm") + "'  AND [Operari] =" + Operari).Length;

                                        if (n3 > 0 || n4 > 0) operarisnodisponibles++;
                                    }
                                    if (n1 == 0 && n2 == 0 && (int)data.DayOfWeek != 0 && (int)data.DayOfWeek != 6 && operarisnodisponibles <dataGridViewOperarisperDefecte.Rows.Count)
                                    {
                                        
                                        string[] row = new string[] { String.Format("{0:dd/MM/yyyy}", data.ToString("dd/MM/yyyy")) };
                                        dataGridViewDates.Rows.Add(row);

                                        //new DatRow(Format(Dt_Fecha, "dd-mm-yyyy")
                                        UO += dataGridViewOperarisperDefecte.Rows.Count - operarisnodisponibles;
                                    }
                                    data = data.AddDays(1);
                                }

                            }
                        }
                    }


                    catch (Exception err)
                    {
                        MessageBox.Show(err.Message);
                    }
                }
                }
        }

        private void btAnular_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}

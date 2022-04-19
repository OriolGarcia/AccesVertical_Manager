using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AccesVertical_Manager
{
    public partial class SelectOperarisperDefecteForm : Form
    {
        private Connection mysqlconnect;
        private string Expedient;

        public SelectOperarisperDefecteForm(Connection mysqlconnect, string Expedient)
        {

            this.mysqlconnect = mysqlconnect;
            this.Expedient = Expedient;
            InitializeComponent();
            Utils.InsertColumns(dataGridViewOperarisDisponibles, 30);
            InitializeGridViewOperarisDisponibles();
            
           Utils.InsertColumns(dataGridViewOperarisperDefecte, 30);
            InitializeGridViewOperarisperDefecte();
        }

        private void btTancar_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void InitializeGridViewOperarisDisponibles()
        {

            try
            {
                MySqlConnection conn = mysqlconnect.getmysqlconn();
                DataTable DtDades = new DataTable();
                string query = "SELECT Operaris.OperariID, Operaris.FotografiaPath,Operaris.Nom,Operaris.Cognoms,Operaris.DNI "
                        + "FROM  FormacionsOperaris t1"
                              + " INNER JOIN formacionsobres t2 ON t2.`Obra`= @ObraID "
                                + " RIGHT JOIN Operaris ON t1.`Operari`= Operaris.OperariID "
                                    + " WHERE(IF(((SELECT Count(t1.Formacio) FROM FormacionsOperaris t1 where t1.`Formacio`= t2.`Formacio`) = 0) "
                                    + ", (true) "
                                        + " ,(t1.`Formacio` IN(SELECT t1.`Formacio` FROM FormacionsOperaris t1 where t1.`Formacio`= t2.`Formacio`))))"
                                            + " GROUP BY operaris.OperariID "
                                                + " HAVING Count(t1.Formacio) = (SELECT Count(*) FROM formacionsobres WHERE `Obra`= @ObraID); ";
                


                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@ObraID", Expedient);


                MySqlDataAdapter mdaDades = new MySqlDataAdapter(cmd);
                //+ dTPinici.Value.Date.ToString("yyyy-MM-dd HH:mm") +

                mdaDades.Fill(DtDades);
                dataGridViewOperarisDisponibles.DataSource = DtDades;
                dataGridViewOperarisDisponibles.RowHeadersVisible = false;
                dataGridViewOperarisDisponibles.AllowUserToAddRows = false;
                dataGridViewOperarisDisponibles.Columns["OperariID"].Visible = false;
                dataGridViewOperarisDisponibles.Columns["FotografiaPath"].Visible = false;
                conn.Close();
            }

            catch (Exception err)
            {
                MessageBox.Show(err.Message);

            }
     }
        private void InitializeGridViewOperarisperDefecte()
        {
            try { 
                    MySqlConnection conn = mysqlconnect.getmysqlconn();
                    DataTable DtDades = new DataTable();
                    string query = "Select OperariID, FotografiaPath,Nom,Cognoms,DNI from operaris"
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
        private void dataGridViewOperarisDisponibles_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dataGridViewOperarisDisponibles.Columns[e.ColumnIndex].Name == "Image")
            {
                string path = dataGridViewOperarisDisponibles.Rows[e.RowIndex].Cells["FotografiaPath"].Value.ToString();
                // Your code would go here - below is just the code I used to test
                if (File.Exists(path))
                {
                    Image image = Image.FromFile(path);
                    var newImage = new Bitmap(dataGridViewOperarisDisponibles.Columns[e.ColumnIndex].Width, dataGridViewOperarisDisponibles.Rows[e.RowIndex].Height);
                    Graphics.FromImage(newImage).DrawImage(image, 0, 0, dataGridViewOperarisDisponibles.Columns[e.ColumnIndex].Width, dataGridViewOperarisDisponibles.Rows[e.RowIndex].Height);
                    e.Value = newImage;
                }
            }
        }

        private void btAfegirOpObra_Click(object sender, EventArgs e)
        {
            Int32 selectedRowCount =
     dataGridViewOperarisDisponibles.Rows.GetRowCount(DataGridViewElementStates.Selected);

            if (selectedRowCount > 0)
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                try { 
                for (int i = 0; i < selectedRowCount; i++)
                {
                    MySqlConnection conn = mysqlconnect.getmysqlconn();
                    string Id = dataGridViewOperarisDisponibles.SelectedRows[i].Cells[1].Value.ToString();
                    // Se crea un DataTable que almacenará los datos desde donde se cargaran los datos al DataGridView
                    DataTable DtDadesAlumnes = new DataTable();
                        // Se crea un MySqlAdapter para obtener los datos de la base



                        try
                        {
                            string Query = "INSERT INTO OperarisObraDefecte(`Operari`,`Obra`) VALUES(\"" + Id + "\",\"" +Expedient + "\"); ";
                        MySqlCommand cmd = new MySqlCommand(Query, conn);
                        MySqlDataReader MyReader2;
                        conn.Open();
                        MyReader2 = cmd.ExecuteReader();     // Here our query will be executed and data saved into the database.  
                            conn.Close();
                        }
                        catch (MySqlException ex)
                        {

                            if (ex.Number != 1062)
                                MessageBox.Show(ex.Message);
                        }
                        finally { conn.Close(); }
                    }
                    
                    InitializeGridViewOperarisperDefecte();


                }
                catch (MySqlException ex)
                {

                    if (ex.Number != 1062)
                        MessageBox.Show(ex.Message);
                }

            }
        }

        private void dataGridViewOperarisperDefecte_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dataGridViewOperarisperDefecte.Columns[e.ColumnIndex].Name == "Image")
            {
                string path = dataGridViewOperarisperDefecte.Rows[e.RowIndex].Cells["FotografiaPath"].Value.ToString();
                // Your code would go here - below is just the code I used to test
                if (File.Exists(path))
                {
                    Image image = Image.FromFile(path);
                    var newImage = new Bitmap(dataGridViewOperarisperDefecte.Columns[e.ColumnIndex].Width, dataGridViewOperarisperDefecte.Rows[e.RowIndex].Height);
                    Graphics.FromImage(newImage).DrawImage(image, 0, 0, dataGridViewOperarisperDefecte.Columns[e.ColumnIndex].Width, dataGridViewOperarisperDefecte.Rows[e.RowIndex].Height);
                    e.Value = newImage;
                }
            }
        }

        private void btTreureOpObra_Click(object sender, EventArgs e)
        {
            Int32 selectedRowCount =
      dataGridViewOperarisperDefecte.Rows.GetRowCount(DataGridViewElementStates.Selected);

            if (selectedRowCount > 0)
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();

                for (int i = 0; i < selectedRowCount; i++)
                {

                    try
                    {
                        MySqlConnection conn = mysqlconnect.getmysqlconn();
                        string Id = dataGridViewOperarisperDefecte.SelectedRows[0].Cells[1].Value.ToString();
                        string Query = "delete from OperarisObraDefecte where (`Operari`,`Obra`) in ((\"" + Id + "\",\"" + Expedient + "\"))";
                        MySqlCommand cmd = new MySqlCommand(Query, conn);

                        conn.Open();
                        cmd.ExecuteReader();
                        conn.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    InitializeGridViewOperarisperDefecte();


                }
            }
        }
    }
}

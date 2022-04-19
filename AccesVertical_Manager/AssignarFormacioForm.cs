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
    public partial class AssignarFormacioForm : Form
    {

        private Connection mysqlconnect;
        private string OperariID;
        private Boolean isupdating = false;
        public AssignarFormacioForm(Connection mysqlconnect, string OperariID)
        {
            this.mysqlconnect = mysqlconnect;
            this.OperariID = OperariID;
           
            InitializeComponent(); 
            InsertCheckBoxColumn();InitializeGridViewFormacio();

           
        }
        private void SelectedItems()
        {


            try
            {
                isupdating = true;
                for (int i = 0; i < dataGridViewFormacions.Rows.Count; i++)
                {

                    ((DataGridViewCheckBoxCell)dataGridViewFormacions.Rows[i].Cells["?"]).Value= dataGridViewFormacions.Rows[i].Cells["Assignada"].Value;
                  

                }
                isupdating =false;
            }

            catch (Exception err)
            {
                MessageBox.Show(err.Message);

            }



        }


        private void InsertCheckBoxColumn()
        {
            DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();
            checkBoxColumn.HeaderText = "";
            checkBoxColumn.Width = 30;
            checkBoxColumn.Name = "?";
            dataGridViewFormacions.Columns.Insert(0, checkBoxColumn);
        }
        private void InitializeGridViewFormacio()
        {


            try
            {
                MySqlConnection conn = mysqlconnect.getmysqlconn();
                DataTable DtDades = new DataTable();
                string query = "Select  Formacions.FormacioID, Formacions.Titol,"
                +"IF((SELECT COUNT(*) FROM FormacionsOperaris WHERE  Operari ="+OperariID+ " AND Formacio = Formacions.FormacioID) > 0,true,false)"
                    + "as 'Assignada' from Formacions Order by Formacions.Titol ASC; ";


                MySqlCommand cmd = new MySqlCommand(query, conn);

             
                MySqlDataAdapter mdaDades = new MySqlDataAdapter(cmd);
                //+ dTPinici.Value.Date.ToString("yyyy-MM-dd HH:mm") +

                mdaDades.Fill(DtDades);
                dataGridViewFormacions.DataSource = DtDades;
                dataGridViewFormacions.RowHeadersVisible = false;
                dataGridViewFormacions.AllowUserToAddRows = false;
                dataGridViewFormacions.Columns["FormacioID"].Visible = false;
                dataGridViewFormacions.Columns["Assignada"].Visible = false;
                conn.Close();
               

            }

            catch (Exception err)
            {
                MessageBox.Show(err.Message);

            }
        }



       

  

       
        private void btTancar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void dataGridViewFormacions_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            SelectedItems();
        }

        private void dataGridViewFormacions_CellValueChanged_1(object sender, DataGridViewCellEventArgs e)
        {
           
            if (e.ColumnIndex == 0 && !isupdating)
            {
                Point cur = new Point(e.ColumnIndex, e.RowIndex);


                DataGridViewRow row = dataGridViewFormacions.Rows[e.RowIndex];
                if ((bool)(row.Cells["?"].Value) == true)
                {
                    MySqlConnection conn = mysqlconnect.getmysqlconn();
                    string FormacioID = row.Cells["FormacioID"].Value.ToString();
                    try
                    {


                        string Query = "INSERT INTO FormacionsOperaris(Operari,Formacio) VALUES(" + OperariID + "," + FormacioID + "); ";
                        MySqlCommand cmd = new MySqlCommand(Query, conn);

                        conn.Open();
                        cmd.ExecuteReader();
                        conn.Close();
                    }
                    catch (Exception err)
                    {

                     //   MessageBox.Show(err.Message);


                    }
                }
                else
                {
                    MySqlConnection conn = mysqlconnect.getmysqlconn();
                    string FormacioID = row.Cells["FormacioID"].Value.ToString();
                    try
                    {


                        string Query = "delete from FormacionsOperaris where (Operari,Formacio) in ((" + OperariID + "," + FormacioID + "));";
                        MySqlCommand cmd = new MySqlCommand(Query, conn);

                        conn.Open();
                        cmd.ExecuteReader();     // Here our query will be executed and data saved into the database.  
                        conn.Close();
                    }
                    catch (Exception err)
                    {
                     //   MessageBox.Show(err.Message);


                    }
                }
                InitializeGridViewFormacio();
            }
        }
    }
}

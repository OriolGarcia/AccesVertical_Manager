using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Security;
using System.Windows.Forms;
using System.Xml;

namespace AccesVertical_Manager
{
    public partial class MainForm : Form
    {
        private string imatgesDir;
        private Connection mysqlconnect;
        bool ChckBisUpdating = false;
        bool initdate = false;
        public MainForm(Connection connect)
        {
            DateTime dataactual = DateTime.Now;
            mysqlconnect = connect;
            InitializeComponent();
            InitializeGridViewFormacio();
            Utils.InsertColumns(dataGridViewOperaris,60);
            Utils.InsertColumns(dataGridViewOperarisperDefecte, 30);
            Utils.InsertColumns(dataGridViewOperarisDisponibles, 30);
            Utils.InsertColumns(dataGridViewOperarisControlAssistencial, 30);
            Utils.InsertColumns(dataGridViewOperarisAssignats, 30);
            groupBoxUsers.Visible = false;
            Thread.CurrentThread.CurrentCulture = new CultureInfo("ca-ES");
            InitializeGridViewUsers();
          
            InitializeGridViewOperaris();
          
            dtTPdatapermanent.Format = DateTimePickerFormat.Custom;
            dtTPdatapermanent.CustomFormat = "dd 'de' MMMM";
            dateTimePickerMesPlànning.Format = DateTimePickerFormat.Custom;
            dateTimePickerMesPlànning.CustomFormat = "MMMM 'del' yyyy";
            initdate = true;
            numericUpDownYEAR.Value = dataactual.Year;
            numericUpDownAnyAbsencia.Value = dataactual.Year;
            numericUpDownAnyControlAssistencial.Value = dataactual.Year;
            initdate = false;
            ColumnesControlAnualAssistencial();
            InitializeGridViewFestiusPermanents();
            InitializeGridViewFestiusOcasionals();
            InitializeGridViewTècnics();
            InitializeGridViewObres();
            InitializeGridViewSelectorObra();
            InitializeGridViewObrespendents();
            InitializeGridViewOperarisControlAssistencial();
            InitializeDataGridViewControlAnualAssistencial();
            InitializeGridViewControlObra();
            InitializeGridViewVehicles();
             InitializeGridViewVehiclesDisponibles();
                    InitializeGridViewVehiclesAssignats();
            InitializeGridViewPlanning(DataGridViewPlanning, DateTime.DaysInMonth(dateTimePickerMesPlànning.Value.Year, dateTimePickerMesPlànning.Value.Month),dateTimePickerMesPlànning.Value,false);
            InitializeGridViewPlanning(dataGridViewPlanningPropermes, 15, dateTimePickerMesPlànning.Value.AddMonths(1),true);
            lbPropermes.Text = "Plànning " + dateTimePickerMesPlànning.Value.AddMonths(1).ToString("MMMM 'del' yyyy");
            lbLlegenda.Text = "Llegenda: " + Environment.NewLine + Environment.NewLine + "V: Vacances"
                                            + Environment.NewLine + "A: Absència"
                                            + Environment.NewLine + "C: Curs"
                                            + Environment.NewLine + "B: Baixa"
                                            +Environment.NewLine + "M: Metge";

            InitializeGridViewAbsenciesMensuals();
            tabControl1.TabPages.Remove(tabSupervisor);
            tabControl1.TabPages.Remove(TabTècnics);
            tabControl1.TabPages.Remove(tabFormacions);
           if (mysqlconnect.getPermissions()[1] == false)
            {
                tabControl1.TabPages.Remove(tabCalendariLaboral);
                tabControl1.TabPages.Remove(tabFormacions);
                tabControl2.TabPages.Remove(tabPersonalAV);
                tabControl2.TabPages.Remove(tabVehicles);
                tabControl2.TabPages.Remove(tabObres);
                tabControl2.TabPages.Remove(tabPlanificador);
                tabControl2.TabPages.Remove(tabControlAssistencial);
                tabPlanning.Controls.Remove(btOperarisperdefecte);
                tabPlanning.Controls.Remove(btGenerarPlànning2);

            }
                this.WindowState = FormWindowState.Maximized;
        }
        
        private void MainForm_Load(object sender, EventArgs e)
        {
            imatgesDirLoad();
        }
        private void imatgesDirLoad()
        {
            XmlDocument xmlDoc = new XmlDocument();
            if (File.Exists("AccesVertical_Manager_config.xml"))
            {
                xmlDoc.Load("AccesVertical_Manager_config.xml");
                XmlNodeList config = xmlDoc.GetElementsByTagName("Config");
                XmlElement DirImatges = (XmlElement)((XmlElement)config[0]).GetElementsByTagName("DirImatges")[0];

                imatgesDir = DirImatges.InnerText;
                XmlNode root = xmlDoc.FirstChild;
            }
            else
            {
                imatgesDir = @"\\SERVERACCES\Empresa\unitat compartida\MIS ARCHIVOS DE ORIGEN DE DATOS\ACCES VERTICAL MANAGER\Personal_Images";
            }
         }
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void modeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (mysqlconnect.getPermissions()[0]==true)
            {
                if(!tabControl1.TabPages.Contains(tabSupervisor)){
                    tabControl1.TabPages.Add(tabSupervisor);
                    tabControl1.TabPages.Add(TabTècnics);
                    tabControl1.TabPages.Add(tabFormacions);
                    groupBoxUsers.Visible = true;
                }
            }
            else
            {
                MessageBox.Show("No tens persmisos de Supervisor.");


            }
        }

        private void modeTècnicToolStripMenuItem_Click(object sender, EventArgs e)
        {
              tabControl1.TabPages.Remove(tabSupervisor);
            tabControl1.TabPages.Remove(TabTècnics);
            tabControl1.TabPages.Remove(tabFormacions);
            groupBoxUsers.Visible = false;
        }



        private void InitializeGridViewUsers()
        {


            try
            {
                MySqlConnection conn = mysqlconnect.getmysqlconn();
                DataTable DtDades = new DataTable();
                string query = "Select NickName,Password as `Password encripted`,`SupervisorPermissions`,`ManagerPermissions` ,Active from Users"
                   +  " WHERE("
         + "LENGTH(?NICKNAME) <1 or NIckName like ?NICKNAMESEARCH)";
                //,  

                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("?NICKNAME", txtBNickName.Text);
                cmd.Parameters.AddWithValue("?NICKNAMESEARCH", "%" + txtBNickName.Text + "%");
                  MySqlDataAdapter mdaDades = new MySqlDataAdapter(cmd);
                //+ dTPinici.Value.Date.ToString("yyyy-MM-dd HH:mm") +

                mdaDades.Fill(DtDades);
                 dataGridViewUsers.DataSource = DtDades;
               dataGridViewUsers.RowHeadersVisible = false;
                dataGridViewUsers.AllowUserToAddRows = false;
                conn.Close();
                dataGridViewUsers.Columns["NickName"].FillWeight = 30;
                dataGridViewUsers.Columns["Password encripted"].FillWeight = 30;
                dataGridViewUsers.Columns["SupervisorPermissions"].FillWeight = 20;
                dataGridViewUsers.Columns["ManagerPermissions"].FillWeight = 20;
                dataGridViewUsers.Columns["Active"].FillWeight = 20;
             
            }

            catch (Exception err)
            {
                MessageBox.Show(err.Message);

            }
          
        }

        private void InitializeGridViewTècnics()
        {


            try
            {
                MySqlConnection conn = mysqlconnect.getmysqlconn();
                DataTable DtDades = new DataTable();
                string query = "Select `TècnicID`, `Nom i cognoms` from Tècnics"
                   + " WHERE("
         + "LENGTH(?NOM) <1 or `Nom i cognoms` like ?NOMSEARCH)";
                //,  

                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("?NOM", txtBNomTecnic.Text);
                cmd.Parameters.AddWithValue("?NOMSEARCH", "%" + txtBNomTecnic.Text + "%");
                MySqlDataAdapter mdaDades = new MySqlDataAdapter(cmd);
                //+ dTPinici.Value.Date.ToString("yyyy-MM-dd HH:mm") +

                mdaDades.Fill(DtDades);
                dataGridViewTècnics.DataSource = DtDades;
                dataGridViewTècnics.RowHeadersVisible = false;
                dataGridViewTècnics.AllowUserToAddRows = false;
                conn.Close();

            }

            catch (Exception err)
            {
                MessageBox.Show(err.Message);

            }

        }
        private void ColumnesControlAnualAssistencial()
        {
 
            dataGridViewControlAnualAssistencial.Columns.Add("Nom", "Nom");
            dataGridViewControlAnualAssistencial.Columns.Add("Cognoms", "Cognoms");
            dataGridViewControlAnualAssistencial.Columns.Add("Vinculacio", "Vinculacio");
            dataGridViewControlAnualAssistencial.Columns.Add("GenVacances", "V");
            dataGridViewControlAnualAssistencial.Columns.Add("GenAbsencia", "A");
            dataGridViewControlAnualAssistencial.Columns.Add("GenCurs", "C");
            dataGridViewControlAnualAssistencial.Columns.Add("GenBaixa", "B");
            dataGridViewControlAnualAssistencial.Columns.Add("GenMetge", "M");
            dataGridViewControlAnualAssistencial.Columns.Add("GenExtra", "E");

            dataGridViewControlAnualAssistencial.Columns.Add("FebVacances", "V");
            dataGridViewControlAnualAssistencial.Columns.Add("FebAbsencia", "A");
            dataGridViewControlAnualAssistencial.Columns.Add("FebCurs", "C");
            dataGridViewControlAnualAssistencial.Columns.Add("FebBaixa", "B");
            dataGridViewControlAnualAssistencial.Columns.Add("FebMetge", "M");
            dataGridViewControlAnualAssistencial.Columns.Add("FebExtra", "E");

            dataGridViewControlAnualAssistencial.Columns.Add("MarVacances", "V");
            dataGridViewControlAnualAssistencial.Columns.Add("MarAbsencia", "A");
            dataGridViewControlAnualAssistencial.Columns.Add("MarCurs", "C");
            dataGridViewControlAnualAssistencial.Columns.Add("MarBaixa", "B");
            dataGridViewControlAnualAssistencial.Columns.Add("MarMetge", "M");
            dataGridViewControlAnualAssistencial.Columns.Add("MarExtra", "E");

            dataGridViewControlAnualAssistencial.Columns.Add("AbrVacances", "V");
            dataGridViewControlAnualAssistencial.Columns.Add("AbrAbsencia", "A");
            dataGridViewControlAnualAssistencial.Columns.Add("AbrCurs", "C");
            dataGridViewControlAnualAssistencial.Columns.Add("AbrBaixa", "B");
            dataGridViewControlAnualAssistencial.Columns.Add("AbrMetge", "M");
            dataGridViewControlAnualAssistencial.Columns.Add("AbrExtra", "E");

            dataGridViewControlAnualAssistencial.Columns.Add("MaiVacances", "V");
            dataGridViewControlAnualAssistencial.Columns.Add("MaiAbsencia", "A");
            dataGridViewControlAnualAssistencial.Columns.Add("MaiCurs", "C");
            dataGridViewControlAnualAssistencial.Columns.Add("MaiBaixa", "B");
            dataGridViewControlAnualAssistencial.Columns.Add("MaiMetge", "M");
            dataGridViewControlAnualAssistencial.Columns.Add("MaiExtra", "E");

            dataGridViewControlAnualAssistencial.Columns.Add("JunVacances", "V");
            dataGridViewControlAnualAssistencial.Columns.Add("JunAbsencia", "A");
            dataGridViewControlAnualAssistencial.Columns.Add("JunCurs", "C");
            dataGridViewControlAnualAssistencial.Columns.Add("JunBaixa", "B");
            dataGridViewControlAnualAssistencial.Columns.Add("JunMetge", "M");
            dataGridViewControlAnualAssistencial.Columns.Add("JunExtra", "E");

            dataGridViewControlAnualAssistencial.Columns.Add("JulVacances", "V");
            dataGridViewControlAnualAssistencial.Columns.Add("JulAbsencia", "A");
            dataGridViewControlAnualAssistencial.Columns.Add("JulCurs", "C");
            dataGridViewControlAnualAssistencial.Columns.Add("JulBaixa", "B");
            dataGridViewControlAnualAssistencial.Columns.Add("JulMetge", "M");
            dataGridViewControlAnualAssistencial.Columns.Add("JulExtra", "E");

            dataGridViewControlAnualAssistencial.Columns.Add("AgoVacances", "V");
            dataGridViewControlAnualAssistencial.Columns.Add("AgoAbsencia", "A");
            dataGridViewControlAnualAssistencial.Columns.Add("AgoCurs", "C");
            dataGridViewControlAnualAssistencial.Columns.Add("AgoBaixa", "B");
            dataGridViewControlAnualAssistencial.Columns.Add("AgoMetge", "M");
            dataGridViewControlAnualAssistencial.Columns.Add("AgoExtra", "E");

            dataGridViewControlAnualAssistencial.Columns.Add("SetVacances", "V");
            dataGridViewControlAnualAssistencial.Columns.Add("SetAbsencia", "A");
            dataGridViewControlAnualAssistencial.Columns.Add("SetCurs", "C");
            dataGridViewControlAnualAssistencial.Columns.Add("SetBaixa", "B");
            dataGridViewControlAnualAssistencial.Columns.Add("SetMetge", "M");
            dataGridViewControlAnualAssistencial.Columns.Add("SetExtra", "E");

            dataGridViewControlAnualAssistencial.Columns.Add("OctVacances", "V");
            dataGridViewControlAnualAssistencial.Columns.Add("OctAbsencia", "A");
            dataGridViewControlAnualAssistencial.Columns.Add("OctCurs", "C");
            dataGridViewControlAnualAssistencial.Columns.Add("OctBaixa", "B");
            dataGridViewControlAnualAssistencial.Columns.Add("OctMetge", "M");
            dataGridViewControlAnualAssistencial.Columns.Add("OctExtra", "E");

            dataGridViewControlAnualAssistencial.Columns.Add("NovVacances", "V");
            dataGridViewControlAnualAssistencial.Columns.Add("NovAbsencia", "A");
            dataGridViewControlAnualAssistencial.Columns.Add("NovCurs", "C");
            dataGridViewControlAnualAssistencial.Columns.Add("NovBaixa", "B");
            dataGridViewControlAnualAssistencial.Columns.Add("NovMetge", "M");
            dataGridViewControlAnualAssistencial.Columns.Add("NovExtra", "E");

            dataGridViewControlAnualAssistencial.Columns.Add("DesVacances", "V");
            dataGridViewControlAnualAssistencial.Columns.Add("DesAbsencia", "A");
            dataGridViewControlAnualAssistencial.Columns.Add("DesCurs", "C");
            dataGridViewControlAnualAssistencial.Columns.Add("DesBaixa", "B");
            dataGridViewControlAnualAssistencial.Columns.Add("DesMetge", "M");
            dataGridViewControlAnualAssistencial.Columns.Add("DesExtra", "E");





            for (int j = 3; j < dataGridViewControlAnualAssistencial.ColumnCount; j++)
            {

                dataGridViewControlAnualAssistencial.Columns[j].Width = 25;

            }

            dataGridViewControlAnualAssistencial.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;

            dataGridViewControlAnualAssistencial.ColumnHeadersHeight = dataGridViewControlAnualAssistencial.ColumnHeadersHeight * 2;

            dataGridViewControlAnualAssistencial.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomCenter;

            //dataGridViewControlAnualAssistencial.CellPainting += new DataGridViewCellPaintingEventHandler(dataGridView1_CellPainting);

            dataGridViewControlAnualAssistencial.Paint += new PaintEventHandler(dataGridViewControlAnualAssistencial_Paint);
        }

        void dataGridViewControlAnualAssistencial_Paint(object sender, PaintEventArgs e)
        {

            string[] monthes = { "Gener", "Febrer", "Març","Abril","Maig","Juny","Juliol","Agost","Setembre","Octubre","Novembre","Desembre" };

            for (int j = 3; j <6*12+3;)
            {

                Rectangle r1 = dataGridViewControlAnualAssistencial.GetCellDisplayRectangle(j, -1, true); //get the column header cell

                r1.X += 1;

                r1.Y += 1;

                r1.Width = r1.Width * 6 - 6;

                r1.Height = r1.Height / 2 - 2;

                e.Graphics.FillRectangle(new SolidBrush(dataGridViewControlAnualAssistencial.ColumnHeadersDefaultCellStyle.BackColor), r1);

                StringFormat format = new StringFormat();

                format.Alignment = StringAlignment.Center;

                format.LineAlignment = StringAlignment.Center;

                e.Graphics.DrawString(monthes[j /6],

                    dataGridViewControlAnualAssistencial.ColumnHeadersDefaultCellStyle.Font,

                    new SolidBrush(dataGridViewControlAnualAssistencial.ColumnHeadersDefaultCellStyle.ForeColor),

                    r1,

                    format);
                e.Graphics.DrawLine(new Pen(Color.DarkGray), new Point(r1.X, r1.Bottom), new Point(r1.X + r1.Width, r1.Bottom));

                j += 6;

            }

        }
    
    private void InitializeDataGridViewControlAnualAssistencial()
        {
            dataGridViewControlAnualAssistencial.Rows.Clear();
            try
            {
                MySqlConnection conn = mysqlconnect.getmysqlconn();

                DataTable DtDades = new DataTable();
                string query = "Select  Operaris.Nom,Operaris.Cognoms,operaris.OperariID, absencies.Data, absencies.Motiu, Operaris.`Vinculació`"
                   + " from operaris left join absencies"
                    +    " ON operaris.OperariID = absencies.Operari AND  year(`Data`) = @YEAR;";

                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@YEAR", numericUpDownAnyControlAssistencial.Value);
                conn.Open();
                MySqlDataReader reader = cmd.ExecuteReader();
               
                Dictionary<int,AbsènciesAnualsOperari> ControlAnual = new Dictionary<int, AbsènciesAnualsOperari>();
                while (reader.Read())
                {

                    ;
                    string Nom = !reader.IsDBNull(0) ? reader.GetString(0) : null;
                    string Cognoms = !reader.IsDBNull(1) ? reader.GetString(1) : null;
                    int OperariId = !reader.IsDBNull(2) ? reader.GetInt16(2) : 0;
                    DateTime data = !reader.IsDBNull(3) ? reader.GetDateTime(3) : DateTime.Now;
                    string Motiu = !reader.IsDBNull(4) ? reader.GetString(4) : null;
                    string vinculacio=!reader.IsDBNull(5) ? reader.GetString(5): null;
                    
                    if (!ControlAnual.ContainsKey(OperariId))
                    {
                        ControlAnual.Add(OperariId, new AbsènciesAnualsOperari(Nom, Cognoms,vinculacio, data, Motiu));
                        ;
                    }
                    else
                    {
                        ControlAnual[OperariId].AfegirAbsencia(data, Motiu);
                    }
                }

                reader.Close();
                  DtDades = new DataTable();
                       query = "SELECT Operari,Data, count(Data) FROM OperarisObraData "
                 + " INNER JOIN Operaris ON Operaris.OperariID = OperarisObraData.Operari"
                   + " WHERE ( ( OperarisObraData.DATA IN (SELECT DATA FROM festiusanualsocasionals) "
                        + " OR DATE_FORMAT(DATA, '%d-%m')  IN (SELECT DATE_FORMAT(DATA, '%d-%m') "
                     + " FROM festiusgeneral)  OR dayofweek(OperarisObraData.DATA) = 1 "
                     +" OR dayofweek(OperarisObraData.DATA) = 7 "
                            + "OR Operaris.`Vinculació`='AUTÒNOM') AND ( YEAR(DATA) = @YEAR ))"
                 + " GROUP BY Operari, YEAR(Data), MONTH(Data) ";

                cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@YEAR", numericUpDownAnyControlAssistencial.Value);
              
                    reader = cmd.ExecuteReader();

                Dictionary<int, DiesExtres> diesExtres = new Dictionary<int, DiesExtres>();
                while (reader.Read())
                {

                    ;
                   
                    int OperariId = !reader.IsDBNull(0) ? reader.GetInt16(0) : 0;
                    DateTime data = !reader.IsDBNull(1) ? reader.GetDateTime(1) : DateTime.Now;
                    int Extres = !reader.IsDBNull(2) ? reader.GetInt16(2) : 0;

                    if (!diesExtres.ContainsKey(OperariId))
                    {
                        diesExtres.Add(OperariId, new DiesExtres(data,Extres));
                    }
                    else
                    {
                       diesExtres[OperariId].AfegirData(data, Extres);
                    }
                }
                conn.Close();
         

                foreach (KeyValuePair<int, AbsènciesAnualsOperari> pair in ControlAnual)
                {

                    int[] extres = new int[12] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, };
                    if (diesExtres.ContainsKey(pair.Key))
                    {
                        extres = diesExtres[pair.Key].CalendariExtres;
                    }
                    AbsènciesAnualsOperari absenciaanual = pair.Value;
                    string nom = absenciaanual.nom;
                    string cognoms = absenciaanual.cognoms;
                    string vinculacio = absenciaanual.vinculacio;
                    AbsènciesMensuals[] absencies = absenciaanual.Absencies;
                    
                    List<string> ValorsColumnes = new List<string>();
                       ValorsColumnes.Add(nom);
                        ValorsColumnes.Add(cognoms);
                    ValorsColumnes.Add(vinculacio);
                    for ( int i=0; i < absencies.Length; i++)
                    {
                        ValorsColumnes.Add(absencies[i].vacances.ToString()!="0"? absencies[i].vacances.ToString():"");
                        ValorsColumnes.Add(absencies[i].absencia.ToString() != "0" ? absencies[i].absencia.ToString() : "");
                        ValorsColumnes.Add(absencies[i].curs.ToString() != "0" ? absencies[i].curs.ToString() : "");
                      ValorsColumnes.Add(absencies[i].baixa.ToString() != "0" ? absencies[i].baixa.ToString() : "");
                        ValorsColumnes.Add(absencies[i].metge.ToString() != "0" ? absencies[i].metge.ToString() : "");
                        ValorsColumnes.Add(extres[i].ToString() != "0" ? extres[i].ToString() : "");
                    }
                    
                   
                    string[] row = ValorsColumnes.ToArray();
                   
                    dataGridViewControlAnualAssistencial.Rows.Add(row);
                   
                }

                dataGridViewControlAnualAssistencial.AllowUserToAddRows = false;



            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);

            }


        }








        private void ColumnesPlanning(DataGridView DGRView, int dies, bool amagarcamps)
        {
            DGRView.Rows.Clear();
            DGRView.Columns.Clear();
            DataGridViewTextBoxColumn column = new DataGridViewTextBoxColumn();
           column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                column.HeaderText = "ULTIMA DATA OBRA";
                column.Name = "ULTIMA DATA OBRA";
                column.Width = 25;
            column.Visible = false;
            DGRView.Columns.Add(column);
            if (!amagarcamps)
            {
                column = new DataGridViewTextBoxColumn();
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                column.HeaderText = "TÈCNIC";
                column.Name = "TÈCNIC";
                column.Width = 100;
                DGRView.Columns.Add(column);
                column = new DataGridViewTextBoxColumn();
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                column.HeaderText = "DIA";
                column.Name = "DIA";
                column.Width = 25;
                DGRView.Columns.Add(column);
            }
                column = new DataGridViewTextBoxColumn();
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                column.HeaderText = "DOC";
                column.Name = "DOC";
                column.Width = 25;
                DGRView.Columns.Add(column);
               
                column = new DataGridViewTextBoxColumn();
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                column.HeaderText = "DOC ENTREGADA";
                column.Name = "DOC ENTREGADA";
                column.Width = 25;
                column.Visible = false;
                DGRView.Columns.Add(column);
            if (!amagarcamps)
            {
                column = new DataGridViewTextBoxColumn();
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                column.HeaderText = "ACT.";
                column.Name = "ACT.";
                column.Width = 30;
                DGRView.Columns.Add(column);

            }
                column = new DataGridViewTextBoxColumn();
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                column.HeaderText = "EXP.";
                column.Name = "EXP.";
                column.Width = 50;
                DGRView.Columns.Add(column);
                column = new DataGridViewTextBoxColumn();
            
            column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            column.HeaderText = "CLIENT";
            column.Name = "CLIENT";
            column.Width = 200;
            DGRView.Columns.Add(column);
            if (!amagarcamps)
            {
                column = new DataGridViewTextBoxColumn();
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                column.HeaderText = "UO PREV";
                column.Name = "UO PREV";
                column.Width = 50;
                DGRView.Columns.Add(column);
            }
            for (int i = 0; i <dies; i++)
            {
                column = new DataGridViewTextBoxColumn();
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                column.HeaderText = (i+1).ToString();
                column.Name = (i + 1).ToString();
                column.Width =40;
              

                DGRView.Columns.Add(column);
            }
            if (!amagarcamps)
            {
                column = new DataGridViewTextBoxColumn();
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                column.HeaderText = "SUMATORI";
                column.Name = "SUMATORI";
                column.Width = 65;
                DGRView.Columns.Add(column);
            }
        }
        private void InitializeGridViewPlanning(DataGridView DGRView,int dies, DateTime dataMes, bool amagarcamps)
        {


            ColumnesPlanning( DGRView,  dies,amagarcamps);
            try
            {
                MySqlConnection conn = mysqlconnect.getmysqlconn();

                DataTable DtDades = new DataTable();
         /*       string query = "Select  `Obra`,Count(`Data`), `Data`, `Nom i cognoms`,`Documentacio OK`,`Activitat`,`Client`,`Unitats d'Obra`"
                    + ",`DIA OK`,`Documentació entregada`"
                    + " from operarisobradata INNER JOIN Obres ON operarisobradata.Obra = Obres.Expedient"
                    + " INNER JOIN Tècnics ON Tècnics.`TècnicID`=Obres.`Tècnic`"
                    + "where year(`Data`)=@YEAR and month(`Data`)=@MONTH "
                    + " GROUP BY `Obra`,`Data`; ";*/
                string query = "Select  `Obra` ob,Operari op,`Data` dt,Count(`Data`) /"
                   +" (Select count(`Data`) from operarisobradata where Operari = op"
                    +" and Data = dt) as UO, `Nom i cognoms`,`Documentacio OK`,`Activitat`,`Client`,`Unitats d'Obra`,"
                        + " `DIA OK`,`Documentació entregada`,(SELECT MAX(`Data`) from operarisobradata  where `Obra`=ob) as `MAXDATA`"
                        + " from operarisobradata INNER JOIN Obres ON operarisobradata.Obra = Obres.Expedient"
                        +" INNER JOIN Tècnics ON Tècnics.`TècnicID`= Obres.`Tècnic`"
                      + "where year(`Data`)=@YEAR and month(`Data`)=@MONTH "
                        + " GROUP BY `Obra`,`Data`, `Operari` ";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@YEAR",dataMes.Year);
                cmd.Parameters.AddWithValue("@MONTH", dataMes.Month);
                conn.Open();
                MySqlDataReader reader = cmd.ExecuteReader();

                Dictionary<string, MesdeObra> ControlMensual = new Dictionary<string, MesdeObra>();
                while (reader.Read())
                {
                  
                        
                      string Expedient=  !reader.IsDBNull(0) ? reader.GetString(0) : null;
                    DateTime data = !reader.IsDBNull(2) ? reader.GetDateTime(2) : DateTime.Now;
                    float Sumatori = !reader.IsDBNull(3) ? reader.GetFloat(3) : 0;
                    string Tècnic = !reader.IsDBNull(4) ? reader.GetString(4) : null;
                    bool Doc = !reader.IsDBNull(5) ? reader.GetBoolean(5) : false;
                    string Activitat = !reader.IsDBNull(6) ? reader.GetString(6) : null;
                    string Client = !reader.IsDBNull(7) ? reader.GetString(7) : null;
                    int UnitatsObra= !reader.IsDBNull(8) ? reader.GetInt16(8) :0;
                    bool DiaOK = !reader.IsDBNull(9) ? reader.GetBoolean(9) : false;
                    bool DocEntregada = !reader.IsDBNull(10) ? reader.GetBoolean(10) : false;
                    DateTime Maxdata= !reader.IsDBNull(11) ? reader.GetDateTime(11) : DateTime.Now;
                    if (!ControlMensual.ContainsKey(Expedient))
                    {
                        Dictionary<DateTime, float> dates = new Dictionary<DateTime, float>();
                        dates.Add(data, Sumatori);
                        ControlMensual.Add(Expedient, new MesdeObra(Expedient,dates,Tècnic,Doc,DocEntregada,DiaOK,Activitat,Client,UnitatsObra,Maxdata));

                    }
                    else {
                        ControlMensual[Expedient].AfegirData(data,Sumatori);
                            }
                }
                conn.Close();
                DateTime dia1 = new DateTime(dataMes.Year, dataMes.Month, 1);
                DateTime diafi = dia1.AddDays(dies);
                string[] row;
                string InTècnic, InActivitat, InClient;
                bool InDiaOk, InDoc,InDocEntregada;
                float InUnitatsdObra, InSumatoriMensual;
                DateTime avui = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
                List<string> ValorsColumnes;
                foreach (KeyValuePair<string, MesdeObra> pair in ControlMensual)
                {
                    string key = pair.Key;
                   MesdeObra mesdeobra = pair.Value;
                    string Expedient = mesdeobra.Expedient;
                    Dictionary<DateTime, float> dates = mesdeobra.getDates();
                    InSumatoriMensual = mesdeobra.SumatoriMensual;
                    InTècnic = mesdeobra.Tècnic;
                    InDiaOk = mesdeobra.diaOK;
                    InDoc = mesdeobra.Doc;
                    InDocEntregada = mesdeobra.DocEntregada;
                    InActivitat = mesdeobra.Activitat;
                    InClient = mesdeobra.Client;
                    InUnitatsdObra = mesdeobra.UnitatsdObra;
                    DateTime ultimadataobra = mesdeobra.MaxData;
                    ValorsColumnes = new List<string>();
                    ValorsColumnes.Add(ultimadataobra.ToString());
                    if (!amagarcamps)
                    {
                        ValorsColumnes.Add(InTècnic);
                    }
                        ValorsColumnes.Add(InDiaOk ? "OK" : "NO");
                        ValorsColumnes.Add(InDoc ? "OK" : "NO");
                    if (!amagarcamps)
                    {
                        ValorsColumnes.Add(InDocEntregada ? "OK" : "NO");
                        ValorsColumnes.Add(InActivitat);
                    }
                    ValorsColumnes.Add(Expedient);
                    ValorsColumnes.Add(InClient);
                    if (!amagarcamps)
                    {
                        ValorsColumnes.Add(InUnitatsdObra.ToString());
                    }
                    
                    for(DateTime dt = dia1;dt < diafi; dt=dt.AddDays(1))
                    {
                        if (dates.ContainsKey(dt))
                        {
                            ValorsColumnes.Add(dates[dt].ToString("n2"));
                            
                        }else
                        {
                            ValorsColumnes.Add("");
                        }

                    }
                    if (!amagarcamps)
                    {
                        ValorsColumnes.Add(InSumatoriMensual.ToString("n2"));
                    }
                   row = ValorsColumnes.ToArray();
                    
                   DGRView.Rows.Add(row);
                  /*  if (ultimadataobra <avui)
                    {
                        Color obrafinalitzada = Color.FromArgb(255, 255, 102);
                        DGRView.Rows[DGRView.Rows.Count - 1].DefaultCellStyle.BackColor = obrafinalitzada;
                    }

                    */
                    
                }
                DGRView.AllowUserToAddRows = false;
                pintarPlanning(DGRView, dataMes);
                ValorsColumnes = new List<string>();
                ValorsColumnes.Add("");
                if (!amagarcamps)
                {
                    ValorsColumnes.Add("");
                }
                    ValorsColumnes.Add("");
                    ValorsColumnes.Add("");
                if (!amagarcamps)
                {
                    ValorsColumnes.Add("");
                    ValorsColumnes.Add("");
                }
                ValorsColumnes.Add("");
                ValorsColumnes.Add("OPERARIS EN OBRA");
                if (!amagarcamps)
                {
                    ValorsColumnes.Add("");
                }
                              float SumatoriTotal = 0;
                for (DateTime dt = dia1; dt < diafi; dt = dt.AddDays(1))
                {
                    float Sumatori = 0;
                    for(int i=0; i < DGRView.Rows.Count; i++)
                    {
                        float valorcela;

                     if (float.TryParse(DGRView.Rows[i].Cells[dt.Day.ToString()].Value.ToString(), out valorcela))
                       {
                           Sumatori += valorcela;
                       }
                    }

                        ValorsColumnes.Add(Sumatori.ToString());

                    SumatoriTotal += Sumatori;
                }
                if (!amagarcamps)
                {
                    ValorsColumnes.Add(SumatoriTotal.ToString());
                }

                row = ValorsColumnes.ToArray();

                DGRView.Rows.Add(row);
                Color filacompt = Color.FromArgb(204, 204, 255);
                DGRView.Rows[DGRView.Rows.Count - 1].DefaultCellStyle.BackColor = filacompt;


                conn = mysqlconnect.getmysqlconn();

                DtDades = new DataTable();
                /*       string query = "Select  `Obra`,Count(`Data`), `Data`, `Nom i cognoms`,`Documentacio OK`,`Activitat`,`Client`,`Unitats d'Obra`"
                           + ",`DIA OK`,`Documentació entregada`"
                           + " from operarisobradata INNER JOIN Obres ON operarisobradata.Obra = Obres.Expedient"
                           + " INNER JOIN Tècnics ON Tècnics.`TècnicID`=Obres.`Tècnic`"
                           + "where year(`Data`)=@YEAR and month(`Data`)=@MONTH "
                           + " GROUP BY `Obra`,`Data`; ";*/
                 query = "SELECT DISTINCT A.Data ,if((A.DATA IN (SELECT DATA FROM festiusanualsocasionals) "
                        +"OR(DATE_FORMAT(A.DATA, '%d-%m') IN(SELECT DATE_FORMAT(DATA, '%d-%m') "
                        +"FROM festiusgeneral) AND(SELECT count(DATA) FROM festiusgeneral) "
                            +"!= 0) OR dayofweek(A.DATA) = 1 OR dayofweek(A.DATA) = 7),0,count(A.Operari) - count(Absencies.Operari)), "
                            +"(SELECT count(OperariID) from Operaris) FROM Operaris op INNER JOIN operarisobradata A "
                            +" ON operariID NOT IN"
                            +" (SELECT  Operari from operarisobradata WHERE operarisobradata.Data = A.Data) "
                            + "  AND (op.Alta IS NULL OR op.Alta<A.Data) AND (op.Baixa IS NULL OR op.Baixa>A.Data) AND(op.`Vinculació`!='AUTÒNOM')"
                                + " LEFT JOIN Absencies ON Absencies.Data = A.Data AND Absencies.Operari = OperariID "
                                + " where month(A.Data) = @MONTH AND YEAR(A.Data) = @YEAR"
                                   + " GROUP BY A.Data, A.Operari, A.Obra; ";
                             cmd = new MySqlCommand(query, conn);
                            cmd.Parameters.AddWithValue("@YEAR", dataMes.Year);
                            cmd.Parameters.AddWithValue("@MONTH", dataMes.Month);
                            conn.Open();
                             reader = cmd.ExecuteReader();

                            Dictionary<DateTime, int> OperarisDisponibles = new Dictionary<DateTime, int>();
                                    int Total = 0;
                while (reader.Read())
                {
                    DateTime data = !reader.IsDBNull(0) ? reader.GetDateTime(0) : DateTime.Now;
                    int Sumatori = !reader.IsDBNull(1) ? reader.GetInt16(1) : 0;
                    Total = !reader.IsDBNull(2) ? reader.GetInt16(2) : 0;
                    OperarisDisponibles.Add(data, Sumatori);
                }
                conn.Close();
                ValorsColumnes = new List<string>();
                ValorsColumnes.Add("");
                if (!amagarcamps)
                {
                    ValorsColumnes.Add("");
                }
                        ValorsColumnes.Add("");
                    ValorsColumnes.Add("");
                if (!amagarcamps)
                {
                    ValorsColumnes.Add("");
                    ValorsColumnes.Add("");
                }
                ValorsColumnes.Add("");
                ValorsColumnes.Add("OPERARIS DISPONIBLES");
                if (!amagarcamps)
                {
                    ValorsColumnes.Add("");
                }
                SumatoriTotal = 0;
                for (DateTime dt = dia1; dt < diafi; dt = dt.AddDays(1))
                {
                    if (OperarisDisponibles.ContainsKey(dt))
                    {
                        SumatoriTotal += OperarisDisponibles[dt];
                        ValorsColumnes.Add(OperarisDisponibles[dt].ToString());

                    }
                    else
                    {
                        
                            ValorsColumnes.Add("0");
                        
                    }

                }
                ValorsColumnes.Add(SumatoriTotal.ToString());
                row = ValorsColumnes.ToArray();

                DGRView.Rows.Add(row);
                DGRView.Rows[DGRView.Rows.Count - 1].DefaultCellStyle.BackColor = filacompt;
                DGRView.Rows[DGRView.Rows.Count - 1].DefaultCellStyle.BackColor = filacompt;
                Color vermell = Color.FromArgb(245, 183, 177);
                foreach (DataGridViewCell cell in DGRView.Rows[DGRView.Rows.Count - 1].Cells)
                {
                    int valor;
                    if (Int32.TryParse(cell.Value.ToString(), out valor))
                    {
                        if (valor != 0)
                        {
                            cell.Style.BackColor = vermell;
                        }
                    }

                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);

            }



        }


        private void InitializeGridViewObres()
        {


            try
            {
                MySqlConnection conn = mysqlconnect.getmysqlconn();
                DataTable DtDades = new DataTable();
                string query = "Select `Expedient`,`Client`,`CIF`,`Nº de Pressupost`,`Activitat`,`Comentaris i recomenacions`,Tècnics.`Nom i cognoms` as `Tècnic`,`Unitats d'Obra`,"
                + "`Contacte 1`,`Telèfon contacte 1`,`Email contacte 1`,`Contacte 2`,`Telèfon contacte 2`,`Email contacte 2`,"
                + "`Industrial 1`,`Contacte Industrial 1`,`Telèfon Industrial 1`,`Email Industrial 1`,"
               + "`Industrial 2`,`Contacte Industrial 2`,`Telèfon Industrial 2`,`Email Industrial 2`,"
               + "`Industrial 3`,`Contacte Industrial 3`,`Telèfon Industrial 3`,`Email Industrial 3`,"
               + "`Industrial 4`,`Contacte Industrial 4`,`Telèfon Industrial 4`,`Email Industrial 4`"
                + " from Obres "
                + "LEFT JOIN Tècnics ON Tècnics.`TècnicID`=Obres.`Tècnic` "
                + " WHERE  ("
                  + "(LENGTH(?EXPEDIENT) <1 or Expedient like ?EXPEDIENTSEARCH) AND"
                    + "(LENGTH(?CLIENT) <1 or Client like ?CLIENTSEARCH) AND"
                      + "(LENGTH(?CIF) <1 or CIF like ?CIFSEARCH) " + " AND "
                      + "(LENGTH(?NPRESSUPOST) <1 or `Nº de Pressupost` like ?NPRESSUPOSTSEARCH) AND"
                        // + "(LENGTH(?TECNIC) <1 or `Tècnic` like ?TECNICSEARCH) AND"
                          + "(LENGTH(?CONTACTE) <1 or `Contacte 1` like ?CONTACTESEARCH or `Contacte 2` like ?CONTACTESEARCH) AND"
                            + "(LENGTH(?INDUSTRIAL) <1 or `Industrial 1` like ?INDUSTRIALSEARCH or `Industrial 2` like ?INDUSTRIALSEARCH or `Industrial 3` like ?INDUSTRIALSEARCH or `Industrial 4` like ?INDUSTRIALSEARCH))";


                MySqlCommand cmd = new MySqlCommand(query, conn);

                cmd.Parameters.AddWithValue("?EXPEDIENT", txtBExpedient.Text);
                cmd.Parameters.AddWithValue("?EXPEDIENTSEARCH", "%" + txtBExpedient.Text + "%");
                cmd.Parameters.AddWithValue("?CLIENT", txtBClient.Text);
                cmd.Parameters.AddWithValue("?CLIENTSEARCH", "%" + txtBClient.Text + "%");
                cmd.Parameters.AddWithValue("?CIF", txtBCIF.Text);
                cmd.Parameters.AddWithValue("?CIFSEARCH", "%" + txtBCIF.Text + "%");
                cmd.Parameters.AddWithValue("?NPRESSUPOST", txtBNumPressupost.Text);
                cmd.Parameters.AddWithValue("?NPRESSUPOSTSEARCH", "%" + txtBNumPressupost.Text + "%");
                //cmd.Parameters.AddWithValue("?TECNIC",comboBoxTecnics.SelectedValue);
                //cmd.Parameters.AddWithValue("?TECNICSEARCH", "%" + comboBoxTecnics.SelectedValue + "%");
                cmd.Parameters.AddWithValue("?CONTACTE", txtBContacte.Text);
                cmd.Parameters.AddWithValue("?CONTACTESEARCH", "%" + txtBContacte.Text + "%");
                cmd.Parameters.AddWithValue("?INDUSTRIAL", txtBIndustrial.Text);
                cmd.Parameters.AddWithValue("?INDUSTRIALSEARCH", "%" + txtBIndustrial.Text + "%");
              

                MySqlDataAdapter mdaDades = new MySqlDataAdapter(cmd);
                //+ dTPinici.Value.Date.ToString("yyyy-MM-dd HH:mm") +

                mdaDades.Fill(DtDades);
                dataGridViewObres.DataSource = DtDades;
                dataGridViewObres.RowHeadersVisible = false;
                dataGridViewObres.AllowUserToAddRows = false;
               
                conn.Close();





            }

            catch (Exception err)
            {
                MessageBox.Show(err.Message);

            }
        }

        private void InitializeGridViewControlObra()
        {
           dataGridViewControlObra.DataSource = null;
            dataGridViewControlObra.Columns.Clear();
            MySqlConnection conn = mysqlconnect.getmysqlconn();
            try
            {
               
                DataTable DtDades = new DataTable();
                string query = "Select `Expedient`,`Client`,`Nº de Pressupost`,Tècnics.`Nom i cognoms`as `Tècnic`,`Unitats d'Obra` as `UO Previstes`,SUM(0) as `UO REAL` "
             + " from Obres left join operarisobradata on operarisobradata.Obra = Obres.Expedient "
                            +" LEFT JOIN Tècnics ON Tècnics.`TècnicID`= Obres.`Tècnic`"
                         +   " WHERE  ("
                  + "(LENGTH(?EXPEDIENT) <1 or Expedient like ?EXPEDIENTSEARCH) AND"
                    + "(LENGTH(?CLIENT) <1 or Client like ?CLIENTSEARCH)"
                     + ")  group by Expedient; ";


                MySqlCommand cmd = new MySqlCommand(query, conn);

                cmd.Parameters.AddWithValue("?EXPEDIENT", txtBExpedientControlObres.Text);
                cmd.Parameters.AddWithValue("?EXPEDIENTSEARCH", "%" + txtBExpedientControlObres.Text + "%");
                cmd.Parameters.AddWithValue("?CLIENT", txtBClientControlObres.Text);
                cmd.Parameters.AddWithValue("?CLIENTSEARCH", "%" + txtBClientControlObres.Text + "%");
                conn.Open();
                MySqlDataAdapter mdaDades = new MySqlDataAdapter(cmd);
              
                mdaDades.Fill(DtDades);
                dataGridViewControlObra.DataSource = DtDades;
                dataGridViewControlObra.RowHeadersVisible = false;
                dataGridViewControlObra.AllowUserToAddRows = false;
            
                for (int i=0; i < dataGridViewControlObra.Rows.Count; i++)
                {
                    string Expedient = dataGridViewControlObra.Rows[i].Cells["Expedient"].Value.ToString();
                    
                     DtDades = new DataTable();
                    query = "Select Operari op,`Data` dt, "
                        +" (Count(`Data`) / (Select count(`Data`) from operarisobradata where Operari = op"
                        +" and Data = dt)) from OperarisObraData where `Obra`= ?EXPEDIENT  GROUP BY `Obra`,`Data`, `Operari`;  ";


                    cmd = new MySqlCommand(query, conn);

                    cmd.Parameters.AddWithValue("?EXPEDIENT", Expedient);
                    MySqlDataReader reader = cmd.ExecuteReader();
                    float Sumatori = 0;
                    while (reader.Read())
                    {

                        Sumatori += !reader.IsDBNull(2) ? reader.GetFloat(2) : 0;
                    }
                   // MessageBox.Show(Sumatori.ToString());
                    dataGridViewControlObra.Rows[i].Cells[5].Value = Sumatori;
                   

                    reader.Close();
                }

                conn.Close();

            }

            catch (Exception err)
            {
                MessageBox.Show(err.Message);
                conn.Close();
            }
        }


        private void InitializeGridViewSelectorObra()
        {


            try
            {
                MySqlConnection conn = mysqlconnect.getmysqlconn();
                DataTable DtDades = new DataTable();
                string query = "Select `Expedient`,`Client`,`Nº de Pressupost`,Tècnics.`Nom i cognoms`as `Tècnic`,`Plàning Generat`,"
                    + "`Bloquejar Planning`,`Documentacio OK`,`Documentació entregada`,`DIA OK`,`Unitats d'Obra` "
                + " from Obres "
                + "LEFT JOIN Tècnics ON Tècnics.`TècnicID`=Obres.`Tècnic` "
                + " WHERE  ("
                  + "(LENGTH(?EXPEDIENT) <1 or Expedient like ?EXPEDIENTSEARCH) AND"
                    + "(LENGTH(?CLIENT) <1 or Client like ?CLIENTSEARCH)"
                     +")";


                MySqlCommand cmd = new MySqlCommand(query, conn);

                cmd.Parameters.AddWithValue("?EXPEDIENT", txtBExpedientPlanificador.Text);
                cmd.Parameters.AddWithValue("?EXPEDIENTSEARCH", "%" + txtBExpedientPlanificador.Text + "%");
                cmd.Parameters.AddWithValue("?CLIENT", txtBClientPlanificador.Text);
                cmd.Parameters.AddWithValue("?CLIENTSEARCH", "%" + txtBClientPlanificador.Text + "%");
               


                MySqlDataAdapter mdaDades = new MySqlDataAdapter(cmd);
                conn.Close();
                mdaDades.Fill(DtDades);
                dataGridViewSelectorObra.DataSource = DtDades;
                dataGridViewSelectorObra.RowHeadersVisible = false;
                dataGridViewSelectorObra.AllowUserToAddRows = false;
                dataGridViewSelectorObra.Columns["Plàning Generat"].Visible = false;
                dataGridViewSelectorObra.Columns["Bloquejar Planning"].Visible = false;
                dataGridViewSelectorObra.Columns["Documentacio OK"].Visible = false;
                dataGridViewSelectorObra.Columns["Documentació entregada"].Visible = false;
                dataGridViewSelectorObra.Columns["DIA OK"].Visible = false;
                dataGridViewSelectorObra.Columns["Unitats d'Obra"].Visible = false;

                SeleccionarDades();



            }

            catch (Exception err)
            {
                MessageBox.Show(err.Message);

            }
        }

 
        private void InitializeGridViewObrespendents()
        {


            try
            {
                MySqlConnection conn = mysqlconnect.getmysqlconn();
                DataTable DtDades = new DataTable();
                string query = "Select `Expedient`,`Client`,`Nº de Pressupost`,Tècnics.`Nom i cognoms`as `Tècnic`,`Plàning Generat`,"
                    + "`Bloquejar Planning`,`Documentacio OK`,`DIA OK`,`Unitats d'Obra` "
                + " from Obres "
                + "LEFT JOIN Tècnics ON Tècnics.`TècnicID`=Obres.`Tècnic` "
                + "  where Expedient NOT IN( SELECT Obra FROM Operarisobradata);";


                MySqlCommand cmd = new MySqlCommand(query, conn);

                cmd.Parameters.AddWithValue("?EXPEDIENT", txtBExpedientPlanificador.Text);
                cmd.Parameters.AddWithValue("?EXPEDIENTSEARCH", "%" + txtBExpedientPlanificador.Text + "%");
                cmd.Parameters.AddWithValue("?CLIENT", txtBClientPlanificador.Text);
                cmd.Parameters.AddWithValue("?CLIENTSEARCH", "%" + txtBClientPlanificador.Text + "%");



                MySqlDataAdapter mdaDades = new MySqlDataAdapter(cmd);
                conn.Close();
                mdaDades.Fill(DtDades);
                dataGridViewObrespendents.DataSource = DtDades;
                dataGridViewObrespendents.RowHeadersVisible = false;
                dataGridViewObrespendents.AllowUserToAddRows = false;
                dataGridViewObrespendents.Columns["Plàning Generat"].Visible = false;
                dataGridViewObrespendents.Columns["Bloquejar Planning"].Visible = false;
                dataGridViewObrespendents.Columns["Documentacio OK"].Visible = false;
                dataGridViewObrespendents.Columns["DIA OK"].Visible = false;
                dataGridViewObrespendents.Columns["Unitats d'Obra"].Visible = false;

               // SeleccionarDades();



            }

            catch (Exception err)
            {
                MessageBox.Show(err.Message);

            }
        }

        private void InitializeGridViewOperarisDisponibles()
        {

            int selectedRowCount = dataGridViewSelectorObra.Rows.GetRowCount(DataGridViewElementStates.Selected);
            if (selectedRowCount > 0)
            {
                string Expedient = dataGridViewSelectorObra.SelectedRows[0].Cells["Expedient"].Value.ToString();

                try
                {
                    MySqlConnection conn = mysqlconnect.getmysqlconn();
                    DataTable DtDades = new DataTable();
                    string query = "SELECT op.OperariID, op.FotografiaPath,op.Nom,op.Cognoms, "
                           + " (SELECT count(*) from  operarisobradata where "
                            +" operarisobradata.Operari = op.OperariID AND operarisobradata.Data = @Data) Ocupació"
                            + " FROM  FormacionsOperaris t1 "
                            + " INNER JOIN formacionsobres t2 ON t2.`Obra`= @ObraID "
                            + " RIGHT JOIN Operaris op ON t1.`Operari`= op.OperariID "
                           + " WHERE(IF(((SELECT Count(t1.Formacio) FROM FormacionsOperaris t1 where t1.`Formacio`= t2.`Formacio`) = 0) "
                            + ", (true) "
                            + ",(t1.`Formacio` IN(SELECT t1.`Formacio` FROM FormacionsOperaris t1 where t1.`Formacio`= t2.`Formacio`)))"
                            + "  AND (op.Alta IS NULL OR op.Alta<@Data) AND (op.Baixa IS NULL OR op.Baixa>@Data) ) "
                            + " GROUP BY op.OperariID "
                            + " HAVING Count(t1.Formacio) = (SELECT Count(*) FROM formacionsobres WHERE `Obra`= @ObraID) "
                            + " ORDER BY op.Vinculació, op.Nom;";


                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@ObraID", Expedient);
                    cmd.Parameters.AddWithValue("@Data", dateTimePickerDiaObra.Value.ToString("yyyy-MM-dd"));

                    MySqlDataAdapter mdaDades = new MySqlDataAdapter(cmd);
                    //+ dTPinici.Value.Date.ToString("yyyy-MM-dd HH:mm") +

                    mdaDades.Fill(DtDades);
                    dataGridViewOperarisDisponibles.DataSource = DtDades;
                    dataGridViewOperarisDisponibles.RowHeadersVisible = false;
                    dataGridViewOperarisDisponibles.AllowUserToAddRows = false;
                    dataGridViewOperarisDisponibles.Columns["OperariID"].Visible = false;
                    dataGridViewOperarisDisponibles.Columns["FotografiaPath"].Visible = false;
                 
                    conn.Close();
                    pintarOperarisDisponibles();
                }

                catch (Exception err)
                {
                    MessageBox.Show(err.Message);

                }
            }
        }
        private void InitializeGridViewVehiclesDisponibles()
        {

            int selectedRowCount = dataGridViewSelectorObra.Rows.GetRowCount(DataGridViewElementStates.Selected);
            if (selectedRowCount > 0)
            {
                string Expedient = dataGridViewSelectorObra.SelectedRows[0].Cells["Expedient"].Value.ToString();

                try
                {
                    MySqlConnection conn = mysqlconnect.getmysqlconn();
                    DataTable DtDades = new DataTable();
                    string query = "SELECT ve.VehicleID, ve.Marca,ve.Matricula,ve.Model, "
                           + " (SELECT count(*) from  vehiclesobradata where "
                            + " vehiclesobradata.Vehicle = ve.VehicleID AND vehiclesobradata.Data = @Data) Ocupació"
                           + " FROM vehicles ve "
                           +" WHERE ( (ve.Alta IS NULL OR ve.Alta < @Data) AND(ve.Baixa IS NULL OR ve.Baixa > @Data) ) ";
                           


                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@ObraID", Expedient);
                    cmd.Parameters.AddWithValue("@Data", dateTimePickerDiaObra.Value.ToString("yyyy-MM-dd"));

                    MySqlDataAdapter mdaDades = new MySqlDataAdapter(cmd);
                    //+ dTPinici.Value.Date.ToString("yyyy-MM-dd HH:mm") +

                    mdaDades.Fill(DtDades);
                    dataGridViewVehiclesDisponibles.DataSource = DtDades;
                    dataGridViewVehiclesDisponibles.RowHeadersVisible = false;
                    dataGridViewVehiclesDisponibles.AllowUserToAddRows = false;
                    dataGridViewVehiclesDisponibles.Columns["VehicleID"].Visible = false;

                    conn.Close();
                    pintarVehiclesDisponibles();
                }

                catch (Exception err)
                {
                    MessageBox.Show(err.Message);

                }
            }
        }
        private void InitializeGridViewVehiclesAssignats()
        {

            int selectedRowCount = dataGridViewSelectorObra.Rows.GetRowCount(DataGridViewElementStates.Selected);
            if (selectedRowCount > 0)
            {


                string Expedient = dataGridViewSelectorObra.SelectedRows[0].Cells["Expedient"].Value.ToString();

                DateTime DiaObra = dateTimePickerDiaObra.Value;

                try
                {
                    MySqlConnection conn = mysqlconnect.getmysqlconn();
                    DataTable DtDades = new DataTable();
                    string query = "SELECT ve.VehicleID, ve.Marca,ve.Matricula,ve.Model from vehiclesobradata "
                        + " Inner join vehicles ve ON ve.VehicleID= vehiclesobradata.Vehicle "
                        + " Where vehiclesobradata.`Obra`=@ObraID and `Data`= @Data;";



                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@ObraID", Expedient);
                    cmd.Parameters.AddWithValue("@Data", DiaObra.ToString("yyyy-MM-dd"));
                    MySqlDataAdapter mdaDades = new MySqlDataAdapter(cmd);
                    //+ dTPinici.Value.Date.ToString("yyyy-MM-dd HH:mm") +

                    mdaDades.Fill(DtDades);
                    dataGridViewVehiclesAssignats.DataSource = DtDades;
                    dataGridViewVehiclesAssignats.RowHeadersVisible = false;
                    dataGridViewVehiclesAssignats.AllowUserToAddRows = false;
                    dataGridViewVehiclesAssignats.Columns["VehicleID"].Visible = false;
                    conn.Close();
                    SeleccionarDades();
                }

                catch (Exception err)
                {
                    MessageBox.Show(err.Message);

                }
            }
        }
        private void InitializeGridViewOperarisAssignats()
        {

            int selectedRowCount = dataGridViewSelectorObra.Rows.GetRowCount(DataGridViewElementStates.Selected);
            if (selectedRowCount > 0)
            {

               
                string Expedient = dataGridViewSelectorObra.SelectedRows[0].Cells["Expedient"].Value.ToString();

                DateTime DiaObra = dateTimePickerDiaObra.Value;
                
                try
                {
                    MySqlConnection conn = mysqlconnect.getmysqlconn();
                    DataTable DtDades = new DataTable();
                    string query = "Select Operaris.OperariID, Operaris.FotografiaPath,Operaris.Nom,Operaris.Cognoms from OperarisObraData "
                        + " Inner join operaris ON operaris.OperariID= operarisobradata.Operari "
                        + " Where `Obra`=@ObraID and `Data`= @Data  ORDER BY Operaris.Vinculació, Operaris.Nom;";



                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@ObraID", Expedient);
                   cmd.Parameters.AddWithValue("@Data", DiaObra.ToString("yyyy-MM-dd"));
                    MySqlDataAdapter mdaDades = new MySqlDataAdapter(cmd);
                    //+ dTPinici.Value.Date.ToString("yyyy-MM-dd HH:mm") +

                    mdaDades.Fill(DtDades);
                    dataGridViewOperarisAssignats.DataSource = DtDades;
                    dataGridViewOperarisAssignats.RowHeadersVisible = false;
                    dataGridViewOperarisAssignats.AllowUserToAddRows = false;
                    dataGridViewOperarisAssignats.Columns["OperariID"].Visible = false;
                    dataGridViewOperarisAssignats.Columns["FotografiaPath"].Visible = false;
                    conn.Close();
                    SeleccionarDades();
                }

                catch (Exception err)
                {
                    MessageBox.Show(err.Message);

                }
            }
        }
        private void InitializeGridViewOperarisperDefecte()
        {
            int selectedRowCount = dataGridViewSelectorObra.Rows.GetRowCount(DataGridViewElementStates.Selected);
            if (selectedRowCount > 0)
            {
                string ObraID = dataGridViewSelectorObra.SelectedRows[0].Cells["Expedient"].Value.ToString();
                try
                {
                    MySqlConnection conn = mysqlconnect.getmysqlconn();
                    DataTable DtDades = new DataTable();
                    string query = "Select OperariID, FotografiaPath,Nom,Cognoms,DNI from operaris"
                   + " INNER JOIN OperarisObraDefecte ON Operaris.`OperariID`=OperarisObraDefecte.`Operari` "
                    + " WHERE  (OperarisObraDefecte.`Obra`=@ObraID"
                      + " ) ORDER BY Vinculació, Nom;";


                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@ObraID", ObraID);


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
        }
        private void InitializeGridViewVehicles()
        {


            try
            {
                MySqlConnection conn = mysqlconnect.getmysqlconn();
                DataTable DtDades = new DataTable();
                string query = "Select VehicleID,Marca,Alta,Baixa,Matricula,Model,Places,Baca,Propietari from Vehicles"
                + " WHERE  ("
                  + "(LENGTH(?MARCA) <1 or Marca like ?MARCASEARCH) AND"
                    + "(LENGTH(?MATRICULA) <1 or Matricula like ?MATRICULASEARCH) )";


                MySqlCommand cmd = new MySqlCommand(query, conn);

                cmd.Parameters.AddWithValue("?MARCA", txtBMarca.Text);
                cmd.Parameters.AddWithValue("?MARCASEARCH", "%" + txtBMarca.Text + "%");
                cmd.Parameters.AddWithValue("?MATRICULA", txtBMatricula.Text);
                cmd.Parameters.AddWithValue("?MATRICULASEARCH", "%" + txtBMatricula.Text + "%");

                MySqlDataAdapter mdaDades = new MySqlDataAdapter(cmd);
                //+ dTPinici.Value.Date.ToString("yyyy-MM-dd HH:mm") +

                mdaDades.Fill(DtDades);
                dataGridViewVehicles.DataSource = DtDades;
                dataGridViewVehicles.RowHeadersVisible = false;
                dataGridViewVehicles.AllowUserToAddRows = false;
                dataGridViewVehicles.Columns["VehicleID"].Visible = false;
                conn.Close();





            }

            catch (Exception err)
            {
                MessageBox.Show(err.Message);

            }
        }
        private void InitializeGridViewOperaris()
        {


            try
            {
                MySqlConnection conn = mysqlconnect.getmysqlconn();
                DataTable DtDades = new DataTable();
                string query = "Select OperariID, FotografiaPath,Alta,Baixa,Nom,Cognoms,Vinculació,DNI,Adreça,"
                + "Telèfon,`Telèfon Mòbil`,`Correu electrònic`,`Data de naixament`,Nacionalitat,`Numero Seguretat Social`,"
                + "Categoria,`Carnet Professional`,`Nivell` from operaris"
                +" WHERE  ("
                  + "(LENGTH(?NOM) <1 or Nom like ?NOMSEARCH) AND"
                    + "(LENGTH(?COGNOMS) <1 or Cognoms like ?COGNOMSSEARCH) AND"
                      + "(LENGTH(?DNI) <1 or DNI like ?DNISEARCH) " + " AND "
                      + "(LENGTH(?NACIONALITAT) <1 or Nacionalitat like ?NACIONALITATSEARCH) AND"
                         + "(LENGTH(?TELEFON) <1 or Telèfon like ?TELEFONSEARCH or `Telèfon Mòbil` like ?TELEFONSEARCH) AND"
                            + "(LENGTH(?EMAIL) <1 or `Correu electrònic` like ?EMAILSEARCH) AND"
                                 + "(LENGTH(?CATEGORIA) <1 or Categoria like ?CATEGORIASEARCH) AND"
                                  + "(LENGTH(?CARNET) <1 or `Carnet Professional` like ?CARNETSEARCH) AND"
                                  + "(LENGTH(?NIVELL) <1 or `Nivell` like ?NIVELLSEARCH) )"
                                  +"ORDER BY Vinculació, Nom";
                

                MySqlCommand cmd = new MySqlCommand(query, conn);
                
                cmd.Parameters.AddWithValue("?NOM", txtBNomTreballador.Text);
                cmd.Parameters.AddWithValue("?NOMSEARCH", "%" + txtBNomTreballador.Text + "%");
                cmd.Parameters.AddWithValue("?COGNOMS", txtBCognomsTreballador.Text);
                cmd.Parameters.AddWithValue("?COGNOMSSEARCH", "%" + txtBCognomsTreballador.Text + "%");
                cmd.Parameters.AddWithValue("?DNI", txtBDni.Text);
                cmd.Parameters.AddWithValue("?DNISEARCH", "%" + txtBDni.Text + "%");
                cmd.Parameters.AddWithValue("?NACIONALITAT", txtBNacionalitat.Text);
                cmd.Parameters.AddWithValue("?NACIONALITATSEARCH", "%" + txtBNacionalitat.Text + "%");
                cmd.Parameters.AddWithValue("?TELEFON", txtBTelèfon.Text);
                cmd.Parameters.AddWithValue("?TELEFONSEARCH", "%" + txtBTelèfon.Text + "%");
                cmd.Parameters.AddWithValue("?EMAIL", txtBEmailTreballador.Text);
                cmd.Parameters.AddWithValue("?EMAILSEARCH", "%" + txtBEmailTreballador.Text + "%");
                cmd.Parameters.AddWithValue("?CATEGORIA", comboBoxCategoria.Text);
                cmd.Parameters.AddWithValue("?CATEGORIASEARCH", "%" + comboBoxCategoria.Text + "%");
                cmd.Parameters.AddWithValue("?CARNET", comboBoxCarnet.Text);
                cmd.Parameters.AddWithValue("?CARNETSEARCH", "%" + comboBoxCarnet.Text + "%");
                cmd.Parameters.AddWithValue("?NIVELL", comboboxNivell.Text);
                cmd.Parameters.AddWithValue("?NIVELLSEARCH", "%" + comboboxNivell.Text + "%");
                
                MySqlDataAdapter mdaDades = new MySqlDataAdapter(cmd);
                //+ dTPinici.Value.Date.ToString("yyyy-MM-dd HH:mm") +

                mdaDades.Fill(DtDades);
                dataGridViewOperaris.DataSource = DtDades;
                dataGridViewOperaris.RowHeadersVisible = false;
                dataGridViewOperaris.AllowUserToAddRows = false;
                dataGridViewOperaris.Columns["OperariID"].Visible = false;
                dataGridViewOperaris.Columns["FotografiaPath"].Visible = false;
                conn.Close();

            


                   
            }

            catch (Exception err)
            {
                MessageBox.Show(err.Message);

            }
        }
        private void InitializeGridViewOperarisControlAssistencial()
        {


            try
            {
                MySqlConnection conn = mysqlconnect.getmysqlconn();
                DataTable DtDades = new DataTable();
                string query = "Select OperariID, FotografiaPath,Nom,Cognoms,DNI,Adreça,"
                + "Telèfon, Categoria,`Carnet Professional`,`Nivell` from operaris"
                + " WHERE  ("
                  + "(LENGTH(?NOM) <1 or Nom like ?NOMSEARCH) AND"
                    + "(LENGTH(?COGNOMS) <1 or Cognoms like ?COGNOMSSEARCH) AND"
                      + "(LENGTH(?DNI) <1 or DNI like ?DNISEARCH) )";


                MySqlCommand cmd = new MySqlCommand(query, conn);

                cmd.Parameters.AddWithValue("?NOM", txtBNomControlAssistencial.Text);
                cmd.Parameters.AddWithValue("?NOMSEARCH", "%" + txtBNomControlAssistencial.Text + "%");
                cmd.Parameters.AddWithValue("?COGNOMS", txtBCognomsControlAssistencial.Text);
                cmd.Parameters.AddWithValue("?COGNOMSSEARCH", "%" + txtBCognomsControlAssistencial.Text + "%");
                cmd.Parameters.AddWithValue("?DNI", txtBDNIControlAssitencial.Text);
                cmd.Parameters.AddWithValue("?DNISEARCH", "%" + txtBDNIControlAssitencial.Text + "%");
                MySqlDataAdapter mdaDades = new MySqlDataAdapter(cmd);
                //+ dTPinici.Value.Date.ToString("yyyy-MM-dd HH:mm") +

                mdaDades.Fill(DtDades);
                dataGridViewOperarisControlAssistencial.DataSource = DtDades;
                dataGridViewOperarisControlAssistencial.RowHeadersVisible = false;
                dataGridViewOperarisControlAssistencial.AllowUserToAddRows = false;
                dataGridViewOperarisControlAssistencial.Columns["OperariID"].Visible = false;
                dataGridViewOperarisControlAssistencial.Columns["FotografiaPath"].Visible = false;
                conn.Close();





            }

            catch (Exception err)
            {
                MessageBox.Show(err.Message);

            }
        }

        private void InitializeGridViewAbsenciesMensuals()
        {
            try { 
                    MySqlConnection conn = mysqlconnect.getmysqlconn();
                    DataTable DtDades = new DataTable();
                    string query = "Select  absencies.Data,Operaris.Nom,Operaris.Cognoms, absencies.Motiu from absencies Inner join operaris "
                    +" ON operaris.OperariID = absencies.Operari where year(`Data`)= @ANY and month(`Data`)= @MES ORDER BY `Data` ASC; ";


                    MySqlCommand cmd = new MySqlCommand(query, conn);

                 
                    cmd.Parameters.AddWithValue("@ANY", dateTimePickerMesPlànning.Value.Year);
                cmd.Parameters.AddWithValue("@MES", dateTimePickerMesPlànning.Value.Month);
                MySqlDataAdapter mdaDades = new MySqlDataAdapter(cmd);
                    //+ dTPinici.Value.Date.ToString("yyyy-MM-dd HH:mm") +

                    mdaDades.Fill(DtDades);
                    dataGridViewAbsenciesMensuals.DataSource = DtDades;
                      dataGridViewAbsenciesMensuals.RowHeadersVisible = false;
                     dataGridViewAbsenciesMensuals.AllowUserToAddRows = false;
                    

                    conn.Close();





                }

                catch (Exception err)
                {
                    MessageBox.Show(err.Message);

                }
            
        }


        private void InitializeGridViewAbsencies()
        {
            int selectedRowCount = dataGridViewOperarisControlAssistencial.Rows.GetRowCount(DataGridViewElementStates.Selected);
            if (selectedRowCount > 0)
            {
                string Id = dataGridViewOperarisControlAssistencial.SelectedRows[0].Cells["OperariID"].Value.ToString();



                try
                {
                    MySqlConnection conn = mysqlconnect.getmysqlconn();
                    DataTable DtDades = new DataTable();
                    string query = "Select Operari, Data,Motiu from Absencies"
                    + " WHERE  ( Operari=@OperariID AND YEAR(Data)=@ANY);";


                    MySqlCommand cmd = new MySqlCommand(query, conn);

                    cmd.Parameters.AddWithValue("@OperariID", Id);
                    cmd.Parameters.AddWithValue("@ANY", numericUpDownAnyAbsencia.Value);
                    MySqlDataAdapter mdaDades = new MySqlDataAdapter(cmd);
                    //+ dTPinici.Value.Date.ToString("yyyy-MM-dd HH:mm") +

                    mdaDades.Fill(DtDades);
                    dataGridViewAbsències.DataSource = DtDades;
                    dataGridViewAbsències.RowHeadersVisible = false;
                    dataGridViewAbsències.AllowUserToAddRows = false;
                    dataGridViewAbsències.Columns["Operari"].Visible = false;

                    conn.Close();





                }

                catch (Exception err)
                {
                    MessageBox.Show(err.Message);

                }

            }
        }
        private void InitializeGridViewFestiusPermanents() {


            try {
                MySqlConnection conn = mysqlconnect.getmysqlconn();
                string query = "SET lc_time_names = 'ca_ES';";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                conn.Open(); cmd.ExecuteReader();
                conn.Close();
                DataTable DtDades = new DataTable();
             query = "Select Data,DATE_FORMAT(Data, '%d - %M') as `Festius permanents`"
                  + " from FestiusGeneral; ";
               
                cmd = new MySqlCommand(query, conn);


            MySqlDataAdapter mdaDades = new MySqlDataAdapter(cmd);


            mdaDades.Fill(DtDades);
            dataGridViewFestiusPermanents.DataSource = DtDades;
            dataGridViewFestiusPermanents.RowHeadersVisible = false;
            dataGridViewFestiusPermanents.AllowUserToAddRows = false;
            dataGridViewFestiusPermanents.Columns["Data"].Visible = false;
                conn.Close();


        }

            catch (Exception err)
            {
                MessageBox.Show(err.Message);

            }

}

        private void InitializeGridViewFestiusOcasionals()
        {


            try
            {
                MySqlConnection conn = mysqlconnect.getmysqlconn();
                string query = "SET lc_time_names = 'ca_ES';";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                conn.Open(); cmd.ExecuteReader();
                conn.Close();
                DataTable DtDades = new DataTable();
                query = "Select Data as `Festius ocasionals`"
                     + " from FestiusAnualsOcasionals WHERE YEAR(Data)=@ANY; ";

                cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@ANY", numericUpDownYEAR.Value );

                MySqlDataAdapter mdaDades = new MySqlDataAdapter(cmd);


                mdaDades.Fill(DtDades);
                dataGridViewFestiusOcasionals.DataSource = DtDades;
                dataGridViewFestiusOcasionals.RowHeadersVisible = false;
                dataGridViewFestiusOcasionals.AllowUserToAddRows = false;
                conn.Close();


            }

            catch (Exception err)
            {
                MessageBox.Show(err.Message);

            }

        }
        private void InitializeGridViewFormacio()
        {


            try
            {
                MySqlConnection conn = mysqlconnect.getmysqlconn();
                DataTable DtDades = new DataTable();
                string query = "Select  FormacioID, Titol"
                + " from Formacions Order by Titol ASC; ";


                MySqlCommand cmd = new MySqlCommand(query, conn);

                
                MySqlDataAdapter mdaDades = new MySqlDataAdapter(cmd);
                

                mdaDades.Fill(DtDades);
                dataGridViewFormacions.DataSource = DtDades;
                dataGridViewFormacions.RowHeadersVisible = false;
                dataGridViewFormacions.AllowUserToAddRows = false;
                dataGridViewFormacions.Columns["FormacioID"].Visible = false;
           
                conn.Close();


            }

            catch (Exception err)
            {
                MessageBox.Show(err.Message);

            }
        }
        private void btPasswordReset_Click(object sender, EventArgs e)
        {

            int selectedRowCount = dataGridViewUsers.Rows.GetRowCount(DataGridViewElementStates.Selected);
            if (selectedRowCount > 0)
            {
                var confirmReset = MessageBox.Show("Segur que resetejar el password de l'usuari seleccionat ?",
                    "Password Reset!",
                    MessageBoxButtons.YesNo);
                if (confirmReset == DialogResult.Yes)
                {
                    for (int i = 0; i < selectedRowCount; i++)
                    {
                      
                        string password = Membership.GeneratePassword(7, 1);
                        MySqlConnection conn = mysqlconnect.getmysqlconn();
                        string Id = dataGridViewUsers.SelectedRows
                            [i].Cells[0].Value.ToString();
                        try
                        {
                          
                            string Query = "UPDATE Users SET Password =MD5(@Password) where Nickname=@Nickname;";
                            MySqlCommand cmd = new MySqlCommand(Query, conn);
                            cmd.Parameters.AddWithValue("@Nickname", Id);
                            cmd.Parameters.AddWithValue("@Password", password);
                            conn.Open();
                            cmd.ExecuteReader();
                            conn.Close();
                            ShowNewPassword showNewPassword = new ShowNewPassword(Id, password);
                           showNewPassword.Show();
                            InitializeGridViewUsers();

                           
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
        }
       private void txtBNickName_TextChanged(object sender, EventArgs e)
        {
            InitializeGridViewUsers();
        }
        
            private void TecnicForm_FormClosed(object sender, FormClosedEventArgs e)
        {

            int[] SelectedIndexs = Utils.PreselectedIndex(dataGridViewTècnics);
            InitializeGridViewTècnics();
            Utils.PostselectedIndex(dataGridViewTècnics, SelectedIndexs);
        }
        private void absencia_FormClosed(object sender, FormClosedEventArgs e)
        {

            int[] SelectedIndexs = Utils.PreselectedIndex(dataGridViewAbsències);
            InitializeGridViewAbsencies();
            InitializeGridViewObres();
            InitializeGridViewSelectorObra();
            InitializeGridViewObrespendents();
            InitializeGridViewOperarisDisponibles();
            InitializeGridViewPlanning(DataGridViewPlanning, DateTime.DaysInMonth(dateTimePickerMesPlànning.Value.Year, dateTimePickerMesPlànning.Value.Month), dateTimePickerMesPlànning.Value, false);
            InitializeGridViewPlanning(dataGridViewPlanningPropermes, 15, dateTimePickerMesPlànning.Value.AddMonths(1), true);
            InitializeGridViewAbsenciesMensuals();
            InitializeDataGridViewControlAnualAssistencial();
            Utils.PostselectedIndex(dataGridViewAbsències, SelectedIndexs);
        }
        private void UserForm_FormClosed(object sender, FormClosedEventArgs e)
        {

            int[] SelectedIndexs = Utils.PreselectedIndex(dataGridViewUsers);
            InitializeGridViewUsers();
            Utils.PostselectedIndex(dataGridViewUsers, SelectedIndexs);
        }

        private void SelectorObraForm_FormClosed(object sender, FormClosedEventArgs e)
        {
           
            int[] SelectedIndexs1 = Utils.PreselectedIndex(dataGridViewSelectorObra);
            InitializeGridViewSelectorObra();
            InitializeGridViewObrespendents();
            InitializeGridViewOperarisDisponibles();
            InitializeGridViewVehiclesDisponibles();
            InitializeGridViewVehiclesAssignats();
            InitializeGridViewPlanning(DataGridViewPlanning, DateTime.DaysInMonth(dateTimePickerMesPlànning.Value.Year, dateTimePickerMesPlànning.Value.Month), dateTimePickerMesPlànning.Value, false);
            InitializeGridViewPlanning(dataGridViewPlanningPropermes, 15, dateTimePickerMesPlànning.Value.AddMonths(1), true);
            Utils.PostselectedIndexOnlyOneRow(dataGridViewSelectorObra, SelectedIndexs1);
           
        }

        private void SelectorObraForm_FormClosed2(object sender, FormClosedEventArgs e)
        {

            int[] SelectedIndexs1 = Utils.PreselectedIndex(dataGridViewObrespendents);
            InitializeGridViewSelectorObra();
            InitializeGridViewObrespendents();
            InitializeGridViewOperarisDisponibles();
            InitializeGridViewOperarisAssignats();
            InitializeGridViewVehiclesDisponibles();
            InitializeGridViewVehiclesAssignats();
            InitializeGridViewPlanning(DataGridViewPlanning, DateTime.DaysInMonth(dateTimePickerMesPlànning.Value.Year, dateTimePickerMesPlànning.Value.Month), dateTimePickerMesPlànning.Value, false);
            InitializeGridViewPlanning(dataGridViewPlanningPropermes, 15, dateTimePickerMesPlànning.Value.AddMonths(1), true);
            Utils.PostselectedIndexOnlyOneRow(dataGridViewObrespendents, SelectedIndexs1);

        }
        private void ObraForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            int[] SelectedIndexs1 = Utils.PreselectedIndex(dataGridViewObres);
            int[] SelectedIndexs2 = Utils.PreselectedIndex(dataGridViewSelectorObra);
            InitializeGridViewObres();
            InitializeGridViewSelectorObra();
            InitializeGridViewObrespendents();
            InitializeGridViewOperarisDisponibles();
            InitializeGridViewVehiclesDisponibles();
            InitializeGridViewVehiclesAssignats();
            InitializeGridViewPlanning(DataGridViewPlanning, DateTime.DaysInMonth(dateTimePickerMesPlànning.Value.Year, dateTimePickerMesPlànning.Value.Month), dateTimePickerMesPlànning.Value, false);
            InitializeGridViewPlanning(dataGridViewPlanningPropermes, 15, dateTimePickerMesPlànning.Value.AddMonths(1), true);
            Utils.PostselectedIndex(dataGridViewObres, SelectedIndexs1);
            Utils.PostselectedIndexOnlyOneRow(dataGridViewSelectorObra, SelectedIndexs2);
        }
        private void configForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            imatgesDirLoad();
        }
        private void OperariForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            int[] SelectedIndexs = Utils.PreselectedIndex(dataGridViewOperaris);
            InitializeGridViewOperaris();
            InitializeGridViewObres();
            InitializeGridViewSelectorObra();
            InitializeGridViewOperarisDisponibles();
            InitializeGridViewPlanning(DataGridViewPlanning, DateTime.DaysInMonth(dateTimePickerMesPlànning.Value.Year, dateTimePickerMesPlànning.Value.Month), dateTimePickerMesPlànning.Value, false);
            InitializeGridViewPlanning(dataGridViewPlanningPropermes, 15, dateTimePickerMesPlànning.Value.AddMonths(1), true);
            InitializeDataGridViewControlAnualAssistencial();
            Utils.PostselectedIndex(dataGridViewOperaris, SelectedIndexs);
        }
        private void VehicleForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            InitializeGridViewVehicles();
            InitializeGridViewVehiclesDisponibles();
            InitializeGridViewVehiclesAssignats();
        }
        private void btAfegirUsuari_Click(object sender, EventArgs e)
        {
           AddUserForm form =new AddUserForm(mysqlconnect);

            form.FormClosed += new System.Windows.Forms.FormClosedEventHandler(UserForm_FormClosed);

            form.Show();
        }

        private void btEliminarUsuari_Click(object sender, EventArgs e)
        {


            int selectedRowCount = dataGridViewUsers.Rows.GetRowCount(DataGridViewElementStates.Selected);
            if (selectedRowCount > 0)
            {
                var confirmEliminar = MessageBox.Show("Segur que vols eliminar l'usuari seleccionat?",
                    "Confirmació d'Eliminació!",
                    MessageBoxButtons.YesNo);
                if (confirmEliminar == DialogResult.Yes)
                {
                   for (int i = 0; i < selectedRowCount; i++)
                    {

                        try
                        {
                            MySqlConnection conn = mysqlconnect.getmysqlconn();
                            string Id = dataGridViewUsers.SelectedRows[0].Cells[0].Value.ToString();
                            // Se crea un DataTable que almacenará los datos desde donde se cargaran los datos al DataGridView
                            DataTable DtDadesAlumnes = new DataTable();
                            // Se crea un MySqlAdapter para obtener los datos de la base



                            string Query = "delete from Users where Nickname=?NICKNAME;";
                            MySqlCommand cmd = new MySqlCommand(Query, conn);
                            cmd.Parameters.AddWithValue("?NICKNAME",Id);
                                 conn.Open();

                            cmd.ExecuteReader();     // Here our query will be executed and data saved into the database. 
                         
                            conn.Close();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                       InitializeGridViewUsers();


                    }
                }

            }
        }

        private void btEditarPermisos_Click(object sender, EventArgs e)
        {
            int selectedRowCount = dataGridViewUsers.Rows.GetRowCount(DataGridViewElementStates.Selected);
            if (selectedRowCount > 0)
            {
                for (int i = 0; i < selectedRowCount; i++)
                {
                    string Id = dataGridViewUsers.SelectedRows[i].Cells[0].Value.ToString();

                    UserPermissionsForm form = new UserPermissionsForm(mysqlconnect, Id);

                    form.FormClosed += new System.Windows.Forms.FormClosedEventHandler(UserForm_FormClosed);

                    form.Show();
                }
            }
        }

        private void btCanviarPassword_Click(object sender, EventArgs e)
        {
            PasswordChangeForm form = new PasswordChangeForm(mysqlconnect);

            form.FormClosed += new System.Windows.Forms.FormClosedEventHandler(UserForm_FormClosed);

            form.Show();
        }

        private void tancarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();

        }

        private void definirDirectoriAlServidorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ConfigForm form = new ConfigForm();

            form.FormClosed += new System.Windows.Forms.FormClosedEventHandler(configForm_FormClosed);

            form.Show();
        }

        private void btEliminarOperari_Click(object sender, EventArgs e)
        {

            int selectedRowCount = dataGridViewOperaris.Rows.GetRowCount(DataGridViewElementStates.Selected);
            if (selectedRowCount > 0)
            {
                var confirmEliminar = MessageBox.Show("Segur que vols eliminar les files seleccionades?",
                    "Confirmació d'Eliminació!",
                    MessageBoxButtons.YesNo);
                if (confirmEliminar == DialogResult.Yes)
                {
                    for (int i = 0; i < selectedRowCount; i++)
                    {
                        try
                        {
                            MySqlConnection conn = mysqlconnect.getmysqlconn();
                            string Id = dataGridViewOperaris.SelectedRows[0].Cells["OperariID"].Value.ToString();
                          


                            string Query = "delete from Operaris where OperariID=" + Id + ";";
                            MySqlCommand cmd = new MySqlCommand(Query, conn);
                           
                            conn.Open();
                             cmd.ExecuteReader();     // Here our query will be executed and data saved into the database. 
                           
                            conn.Close();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                        InitializeGridViewOperaris();
                        InitializeGridViewObres();
                        InitializeGridViewSelectorObra();
                        InitializeGridViewOperarisDisponibles();
                        InitializeGridViewPlanning(DataGridViewPlanning, DateTime.DaysInMonth(dateTimePickerMesPlànning.Value.Year, dateTimePickerMesPlànning.Value.Month), dateTimePickerMesPlànning.Value, false);
                        InitializeGridViewPlanning(dataGridViewPlanningPropermes, 15, dateTimePickerMesPlànning.Value.AddMonths(1), true);
                        InitializeDataGridViewControlAnualAssistencial();


                    }
                }

            }
        }

        private void btAltaOperari_Click(object sender, EventArgs e)
        {
            AltaOperariForm form = new AltaOperariForm(mysqlconnect,imatgesDir);

            form.FormClosed += new System.Windows.Forms.FormClosedEventHandler(OperariForm_FormClosed);

            form.Show();
        }

        private void txtBNomTreballador_TextChanged(object sender, EventArgs e)
        {
            InitializeGridViewOperaris();
        }

        private void txtBCognomsTreballador_TextChanged(object sender, EventArgs e)
        {
            InitializeGridViewOperaris();
        }

        private void txtBDni_TextChanged(object sender, EventArgs e)
        {
            InitializeGridViewOperaris();
        }

        private void txtBNacionalitat_TextChanged(object sender, EventArgs e)
        {
            InitializeGridViewOperaris();
        }

        private void txtBTelèfon_TextChanged(object sender, EventArgs e)
        {
            InitializeGridViewOperaris();
        }

        private void txtBEmailTreballador_TextChanged(object sender, EventArgs e)
        {
            InitializeGridViewOperaris();
        }

        private void txtBCategoria_TextChanged(object sender, EventArgs e)
        {
            InitializeGridViewOperaris();
        }

        private void txtBCarnet_TextChanged(object sender, EventArgs e)
        {
            InitializeGridViewOperaris();
        }

        private void comboboxNivell_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitializeGridViewOperaris();
        }

        private void dataGridViewOperaris_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if(dataGridViewOperaris.Columns[e.ColumnIndex].Name == "Image")
    {           string path= dataGridViewOperaris.Rows[e.RowIndex].Cells["FotografiaPath"].Value.ToString();
                // Your code would go here - below is just the code I used to test
                if (File.Exists(path))
                {
                    Image image = Image.FromFile(path);
                    var newImage = new Bitmap(dataGridViewOperaris.Columns[e.ColumnIndex].Width, dataGridViewOperaris.Rows[e.RowIndex].Height);
                    Graphics.FromImage(newImage).DrawImage(image, 0, 0, dataGridViewOperaris.Columns[e.ColumnIndex].Width, dataGridViewOperaris.Rows[e.RowIndex].Height);
                    e.Value = newImage;
                }
            }
        }

        private void btModificarOperari_Click(object sender, EventArgs e)
        {
            int selectedRowCount = dataGridViewOperaris.Rows.GetRowCount(DataGridViewElementStates.Selected);
            if (selectedRowCount > 0)
            {
                for (int i = 0; i < selectedRowCount; i++)
                {
                    string Id = dataGridViewOperaris.SelectedRows[i].Cells["OperariID"].Value.ToString();

                    ModificarOperariForm form = new ModificarOperariForm(mysqlconnect, Id, imatgesDir);

                    form.FormClosed += new System.Windows.Forms.FormClosedEventHandler(OperariForm_FormClosed);

                    form.Show();
                }
            }

        }

        private void comboBoxCategoria_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitializeGridViewOperaris();
        }

        private void comboBoxCarnet_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitializeGridViewOperaris();
        }

        private void btObra_Click(object sender, EventArgs e)
        {
            AltaObraForm form = new AltaObraForm(mysqlconnect);

            form.FormClosed += new System.Windows.Forms.FormClosedEventHandler(ObraForm_FormClosed);

            form.Show();
        }

        private void btAfegirTècnic_Click(object sender, EventArgs e)
        {
            AddTècnicForm form = new AddTècnicForm(mysqlconnect);

            form.FormClosed += new System.Windows.Forms.FormClosedEventHandler(TecnicForm_FormClosed);

            form.Show();
        }

        private void btEliminarTècnic_Click(object sender, EventArgs e)
        {

            int selectedRowCount = dataGridViewTècnics.Rows.GetRowCount(DataGridViewElementStates.Selected);
            if (selectedRowCount > 0)
            {
                var confirmEliminar = MessageBox.Show("Segur que vols eliminar les files seleccionades?",
                    "Confirmació d'Eliminació!",
                    MessageBoxButtons.YesNo);
                if (confirmEliminar == DialogResult.Yes)
                {
                    for (int i = 0; i < selectedRowCount; i++)
                    {
                        try
                        {
                            MySqlConnection conn = mysqlconnect.getmysqlconn();
                            string Id = dataGridViewTècnics.SelectedRows[0].Cells["TècnicID"].Value.ToString();



                            string Query = "delete from Tècnics where TècnicID=" + Id + ";";
                            MySqlCommand cmd = new MySqlCommand(Query, conn);
                            
                            conn.Open();
                            cmd.ExecuteReader();     // Here our query will be executed and data saved into the database. 
                        
                            conn.Close();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                        InitializeGridViewTècnics();


                    }
                }

            }
        }

        private void btModificarTècnic_Click(object sender, EventArgs e)
        {
            int selectedRowCount = dataGridViewTècnics.Rows.GetRowCount(DataGridViewElementStates.Selected);
            if (selectedRowCount > 0)
            {
                for (int i = 0; i < selectedRowCount; i++)
                {
                    string Id = dataGridViewTècnics.SelectedRows[i].Cells["TècnicID"].Value.ToString();
                    string Nom = dataGridViewTècnics.SelectedRows[i].Cells["Nom i cognoms"].Value.ToString();
                    ModificarTècnicForm form = new ModificarTècnicForm(mysqlconnect, Id, Nom);

                    form.FormClosed += new System.Windows.Forms.FormClosedEventHandler(TecnicForm_FormClosed);

                    form.Show();
                }
            }
        }

        private void txtBIndustrial_TextChanged(object sender, EventArgs e)
        {
            InitializeGridViewObres();
        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void txtBNumPressupost_TextChanged(object sender, EventArgs e)
        {
            InitializeGridViewObres();
        }

        private void txtBContacte_TextChanged(object sender, EventArgs e)
        {
            InitializeGridViewObres();
        }

        private void txtBCIF_TextChanged(object sender, EventArgs e)
        {
            InitializeGridViewObres();
        }

        private void txtBClient_TextChanged(object sender, EventArgs e)
        {
            InitializeGridViewObres();
        }

        private void txtBExpedient_TextChanged(object sender, EventArgs e)
        {
            InitializeGridViewObres();
        }

        private void btEliminarObra_Click(object sender, EventArgs e)
        {
            int selectedRowCount = dataGridViewObres.Rows.GetRowCount(DataGridViewElementStates.Selected);
            if (selectedRowCount > 0)
            {
                var confirmEliminar = MessageBox.Show("Segur que vols eliminar les files seleccionades?",
                    "Confirmació d'Eliminació!",
                    MessageBoxButtons.YesNo);
                if (confirmEliminar == DialogResult.Yes)
                {
                    for (int i = 0; i < selectedRowCount; i++)
                    {
                        try
                        {
                            MySqlConnection conn = mysqlconnect.getmysqlconn();
                            string Id = dataGridViewObres.SelectedRows[0].Cells["Expedient"].Value.ToString();

                            string Query = "delete from OperarisObraData where Obra=" + Id + ";";
                            MySqlCommand cmd = new MySqlCommand(Query, conn);


                            conn.Open();

                            cmd.ExecuteReader();     // Here our query will be executed and data saved into the database. 


                            conn.Close();

                            Query = "delete from Obres where Expedient=" + Id + ";";
                            cmd = new MySqlCommand(Query, conn);
                           

                            conn.Open();

                            cmd.ExecuteReader();     // Here our query will be executed and data saved into the database. 
                         

                            conn.Close();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                        InitializeGridViewObres();
                        InitializeGridViewSelectorObra();
                        InitializeGridViewPlanning(DataGridViewPlanning, DateTime.DaysInMonth(dateTimePickerMesPlànning.Value.Year, dateTimePickerMesPlànning.Value.Month), dateTimePickerMesPlànning.Value, false);
                        InitializeGridViewPlanning(dataGridViewPlanningPropermes, 15, dateTimePickerMesPlànning.Value.AddMonths(1), true);

                    }
                }

            }
        }

        private void btModificarObra_Click(object sender, EventArgs e)
        {
            int selectedRowCount = dataGridViewObres.Rows.GetRowCount(DataGridViewElementStates.Selected);
            if (selectedRowCount > 0)
            {
                for (int i = 0; i < selectedRowCount; i++)
                {
                    string Id = dataGridViewObres.SelectedRows[i].Cells["Expedient"].Value.ToString();
                    ModificarObraForm form = new ModificarObraForm(mysqlconnect, Id);

                    form.FormClosed += new System.Windows.Forms.FormClosedEventHandler(ObraForm_FormClosed);

                    form.Show();
                }
            }
        }
        private void NovaFormacio_FormClosed(object sender, FormClosedEventArgs e)
        {
           InitializeGridViewFormacio();
        }
        private void btAfegirFormacio_Click(object sender, EventArgs e)
        {

            NovaFormació form = new NovaFormació(mysqlconnect);

            form.FormClosed += new System.Windows.Forms.FormClosedEventHandler(NovaFormacio_FormClosed);

            form.Show();
        }

        private void btModificarFormació_Click(object sender, EventArgs e)
        {
          

            int selectedRowCount = dataGridViewFormacions.Rows.GetRowCount(DataGridViewElementStates.Selected);
            if (selectedRowCount > 0)
            {
                for (int i = 0; i < selectedRowCount; i++)
                {
                    string Id = dataGridViewFormacions.SelectedRows[i].Cells["FormacioID"].Value.ToString();

                    ModificarFormacioForm form = new ModificarFormacioForm(mysqlconnect, Id);

                    form.FormClosed += new System.Windows.Forms.FormClosedEventHandler(NovaFormacio_FormClosed);
                    form.Show();
                }
            }

        
         }

        private void btEliminarFormació_Click(object sender, EventArgs e)
        {


            int selectedRowCount = dataGridViewFormacions.Rows.GetRowCount(DataGridViewElementStates.Selected);
            if (selectedRowCount > 0)
            {
                var confirmEliminar = MessageBox.Show("Segur que vols eliminar les files seleccionades?",
                    "Confirmació d'Eliminació!",
                    MessageBoxButtons.YesNo);
                if (confirmEliminar == DialogResult.Yes)
                {
                    for (int i = 0; i < selectedRowCount; i++)
                    {
                        try
                        {
                            MySqlConnection conn = mysqlconnect.getmysqlconn();
                            string Id = dataGridViewFormacions.SelectedRows[0].Cells["FormacioID"].Value.ToString();



                            string Query = "delete from Formacions where FormacioID=" + Id + ";";
                            MySqlCommand cmd = new MySqlCommand(Query, conn);
                          
                            conn.Open();

                            cmd.ExecuteReader();     // Here our query will be executed and data saved into the database. 
                          

                            conn.Close();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                        InitializeGridViewFormacio();


                    }
                }

            }
        }

        private void btAssignarFormacio_Click_1(object sender, EventArgs e)
        {


            int selectedRowCount = dataGridViewOperaris.Rows.GetRowCount(DataGridViewElementStates.Selected);
            if (selectedRowCount > 0)
            {

                for (int i = 0; i < selectedRowCount; i++)
                {
                    try
                    {
                        MySqlConnection conn = mysqlconnect.getmysqlconn();
                        string Id = dataGridViewOperaris.SelectedRows[i].Cells["OperariID"].Value.ToString();


                        AssignarFormacioForm form = new AssignarFormacioForm(mysqlconnect, Id);

                        form.FormClosed += new System.Windows.Forms.FormClosedEventHandler(OperariForm_FormClosed);


                        form.Show();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }



                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {


            int selectedRowCount = dataGridViewObres.Rows.GetRowCount(DataGridViewElementStates.Selected);
            if (selectedRowCount > 0)
            {

                for (int i = 0; i < selectedRowCount; i++)
                {
                    try
                    {
                        MySqlConnection conn = mysqlconnect.getmysqlconn();
                        string Id = dataGridViewObres.SelectedRows[i].Cells["Expedient"].Value.ToString();


                        AssignarFormacioObraForm form = new AssignarFormacioObraForm(mysqlconnect, Id);

                        form.FormClosed += new System.Windows.Forms.FormClosedEventHandler(ObraForm_FormClosed);


                        form.Show();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }



                }
            }
        }

        private void btModOperarisObraDefecte_Click(object sender, EventArgs e)
        {
           int selectedRowCount = dataGridViewSelectorObra.Rows.GetRowCount(DataGridViewElementStates.Selected);
            if (selectedRowCount > 0)
            { try
                    {
                        MySqlConnection conn = mysqlconnect.getmysqlconn();
                        string Id = dataGridViewSelectorObra.SelectedRows[0].Cells["Expedient"].Value.ToString();


                        SelectOperarisperDefecteForm form = new SelectOperarisperDefecteForm(mysqlconnect, Id);

                        form.FormClosed += new System.Windows.Forms.FormClosedEventHandler(SelectorObraForm_FormClosed);


                        form.Show();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
            }
        }

        private void txtBExpedientPlanificador_TextChanged(object sender, EventArgs e)
        {
            InitializeGridViewSelectorObra();
        }

        private void txtBClientPlanificador_TextChanged(object sender, EventArgs e)
        {
            InitializeGridViewSelectorObra();
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

        private void dataGridViewSelectorObra_SelectionChanged(object sender, EventArgs e)
        {
         
           
            int selectedRowCount = dataGridViewSelectorObra.Rows.GetRowCount(DataGridViewElementStates.Selected);
            if (selectedRowCount > 0)
            {
                ChckBisUpdating = true;
                ChckBBloquejarPlanning.Checked = (bool)dataGridViewSelectorObra.SelectedRows[0].Cells["Bloquejar Planning"].Value;
                chckDocEntregada.Checked = (bool)dataGridViewSelectorObra.SelectedRows[0].Cells["Documentació entregada"].Value;
                chckBDocOk.Checked = (bool)dataGridViewSelectorObra.SelectedRows[0].Cells["-"].Value;
                chckBdiaOK.Checked = (bool)dataGridViewSelectorObra.SelectedRows[0].Cells["DIA OK"].Value;
                ChckBisUpdating = false;
                btGenerarPlaning.Enabled = !ChckBBloquejarPlanning.Checked;
                btModOperarisObraDefecte.Enabled= !ChckBBloquejarPlanning.Checked;
                btEditarPlaning.Enabled = (bool)dataGridViewSelectorObra.SelectedRows[0].Cells["Plàning Generat"].Value;
                MySqlConnection conn = mysqlconnect.getmysqlconn();
                try
                {
                    string Expedient = dataGridViewSelectorObra.SelectedRows[0].Cells["Expedient"].Value.ToString();
                   
                    DataTable DtDades = new DataTable();
                    string query = "Select MIN(`Data`) from OperarisObraData Where `Obra`=@Expedient;";
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Expedient", Expedient);
                    MySqlDataReader reader2 = cmd.ExecuteReader();
                    DateTime diainici = DateTime.Now; ;
                    if (reader2.Read())
                    {

                      diainici= !reader2.IsDBNull(0) ? reader2.GetDateTime(0) : DateTime.Now;
                    }
             
                    reader2.Close();
                   dateTimePickerDiaObra.Value = diainici;
                }
                catch (Exception err)
                {
                    MessageBox.Show(err.Message);

                }
                finally
                {
                    conn.Close();
                    InitializeGridViewOperarisperDefecte();
                    InitializeGridViewOperarisDisponibles();
                    InitializeGridViewOperarisAssignats();
                    InitializeGridViewVehiclesDisponibles();
                    InitializeGridViewVehiclesAssignats();
                    SeleccionarDades();
                }
            }
            }

        private void btAfegirOpObra_Click(object sender, EventArgs e)
        {
            MySqlConnection conn = mysqlconnect.getmysqlconn();
            int selectedRowCount = dataGridViewSelectorObra.Rows.GetRowCount(DataGridViewElementStates.Selected);
            if (selectedRowCount > 0)
            {
                try
                {
                    string Expedient = dataGridViewSelectorObra.SelectedRows[0].Cells["Expedient"].Value.ToString();
                    int selectedRowCount2 = dataGridViewOperarisDisponibles.Rows.GetRowCount(DataGridViewElementStates.Selected);
                    for (int i = 0; i < selectedRowCount2; i++)
                    {





                        string Operari = dataGridViewOperarisDisponibles.SelectedRows[i].Cells["OperariID"].Value.ToString();
                        DateTime data = dateTimePickerDiaObra.Value;
                        DataTable DtDades = new DataTable();
                        string query = "Select Motiu from Absencies where Operari=@OperariID AND Data=@Data;";

                        MySqlCommand cmd = new MySqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@OperariID", Operari);
                        cmd.Parameters.AddWithValue("@Data", data.ToString("yyyy-MM-dd"));
                        conn.Open();
                        MySqlDataReader reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            string motiu = !reader.IsDBNull(0) ? reader.GetString(0) : null;
                            MessageBox.Show("Aquest operari figura com absent aquest dia per el motiu: " + motiu + ".\n No el pots afegir a cap obra.");
                            conn.Close();
                        }
                        else
                        {
                            conn.Close();
                            DtDades = new DataTable();
                            query = "Select `Expedient`, `Client` from Obres INNER JOIN operarisobradata ON operarisobradata.Obra=Obres.Expedient"
                                + " Where operarisobradata.Data = @Data AND operarisobradata.Operari = @OperariID AND operarisobradata.Obra != @Expedient;";

                            cmd = new MySqlCommand(query, conn);
                            cmd.Parameters.AddWithValue("@OperariID", Operari);
                            cmd.Parameters.AddWithValue("@Expedient", Expedient);
                            cmd.Parameters.AddWithValue("@Data", data.ToString("yyyy-MM-dd"));
                            conn.Open();
                            reader = cmd.ExecuteReader();

                            string messageinit = "El operari ja té obres assignades el dia " + data.ToString("dd/MM/yyyy") + " en:\n";
                            string message = messageinit;
                            while (reader.Read())
                            {


                                string ExpedientObra = !reader.IsDBNull(0) ? reader.GetString(0) : null;
                                string Client = !reader.IsDBNull(1) ? reader.GetString(1) : null;
                                message = message + "\t La obra amb expdient " + ExpedientObra + " i client " + Client + "\n";


                            }
                            conn.Close();
                            var confirmInsersió = DialogResult.Yes;
                            if (message != messageinit)
                            {
                                confirmInsersió = MessageBox.Show(message + " Segur que vols tornar a inserir una obra per aquest dia i aquest operari?",
                                         "Confirmació d'Insersió!",
                                         MessageBoxButtons.YesNo);
                            }
                            if (confirmInsersió == DialogResult.Yes)
                            {


                                query = "INSERT INTO OperarisObraData(Operari,`Obra`,`Data`) "
                                  + "VALUES(@OperariID,@Expedient, @Data); ";
                                cmd = new MySqlCommand(query, conn);
                                cmd.Parameters.AddWithValue("@OperariID", Operari);
                                cmd.Parameters.AddWithValue("@Expedient", Expedient);
                                cmd.Parameters.AddWithValue("@Data", data.ToString("yyyy-MM-dd HH:mm:ss"));
                                conn.Open();
                                cmd.ExecuteReader();
                                conn.Close();
                            }
                        }
                    }
                    
                }
                catch (Exception ex)
                {
                  // MessageBox.Show(ex.Message);

                }
                finally
                {
                    conn.Close();
                    InitializeGridViewPlanning(DataGridViewPlanning, DateTime.DaysInMonth(dateTimePickerMesPlànning.Value.Year, dateTimePickerMesPlànning.Value.Month), dateTimePickerMesPlànning.Value, false);
                    InitializeGridViewPlanning(dataGridViewPlanningPropermes, 15, dateTimePickerMesPlànning.Value.AddMonths(1), true);
                    InitializeGridViewObrespendents();
                    InitializeGridViewOperarisDisponibles();
                    InitializeGridViewOperarisAssignats();

                }
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

        private void btAfegirDataFestiusPermanents_Click(object sender, EventArgs e)
        {


            MySqlConnection conn = mysqlconnect.getmysqlconn();
            try
            {


                string Query = "INSERT INTO FestiusGeneral(`Data`)"
                  + "VALUES(@DATA) ";
                MySqlCommand cmd = new MySqlCommand(Query, conn);
                cmd.Parameters.AddWithValue("@DATA", dtTPdatapermanent.Value.Date.ToString("yyyy-MM-dd HH:mm"));
                
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
                InitializeGridViewFestiusPermanents();
            }
        }

        private void btEliminarData_Click(object sender, EventArgs e)
        {


            int selectedRowCount = dataGridViewFestiusPermanents.Rows.GetRowCount(DataGridViewElementStates.Selected);
            if (selectedRowCount > 0)
            {
                var confirmEliminar = MessageBox.Show("Segur que vols eliminar les files seleccionades?",
                    "Confirmació d'Eliminació!",
                    MessageBoxButtons.YesNo);
                if (confirmEliminar == DialogResult.Yes)
                {
                    for (int i = 0; i < selectedRowCount; i++)
                    {
                        try
                        {
                            MySqlConnection conn = mysqlconnect.getmysqlconn();
                            string Data = dataGridViewFestiusPermanents.SelectedRows[0].Cells["Data"].Value.ToString();

                           
                            string Query = "delete from FestiusGeneral where Data=STR_TO_DATE( @DATA, '%d/%m/%Y %H:%i:%s') ;";
                            MySqlCommand cmd = new MySqlCommand(Query, conn);
                            cmd.Parameters.AddWithValue("@DATA", Data);
                            
                            conn.Open();
                          cmd.ExecuteReader();     // Here our query will be executed and data saved into the database. 
                    
                            conn.Close();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                        InitializeGridViewFestiusPermanents();


                    }
                }

            }
        }

        private void btAfegirDataFestiusOcasionals_Click(object sender, EventArgs e)
        {

            MySqlConnection conn = mysqlconnect.getmysqlconn();
            try
            {
                string Query = "INSERT INTO FestiusAnualsOcasionals(`Data`)"
                  + "VALUES(@DATA) ";
                MySqlCommand cmd = new MySqlCommand(Query, conn);
                cmd.Parameters.AddWithValue("@DATA", dtTPdataocasional.Value.Date.ToString("yyyy-MM-dd HH:mm"));

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
                InitializeGridViewFestiusOcasionals();
            }
        
        }

        private void numericUpDownYEAR_ValueChanged(object sender, EventArgs e)
        {
            InitializeGridViewFestiusOcasionals();
        }

        private void btEliminarData2_Click(object sender, EventArgs e)
        {


            int selectedRowCount = dataGridViewFestiusOcasionals.Rows.GetRowCount(DataGridViewElementStates.Selected);
            if (selectedRowCount > 0)
            {
                var confirmEliminar = MessageBox.Show("Segur que vols eliminar les files seleccionades?",
                    "Confirmació d'Eliminació!",
                    MessageBoxButtons.YesNo);
                if (confirmEliminar == DialogResult.Yes)
                {
                    for (int i = 0; i < selectedRowCount; i++)
                    {
                        try
                        {
                            MySqlConnection conn = mysqlconnect.getmysqlconn();
                            string Data = dataGridViewFestiusOcasionals.SelectedRows[0].Cells["Festius ocasionals"].Value.ToString();


                            string Query = "delete from FestiusAnualsOcasionals where Data=STR_TO_DATE( @DATA, '%d/%m/%Y %H:%i:%s') ;";
                            MySqlCommand cmd = new MySqlCommand(Query, conn);
                            cmd.Parameters.AddWithValue("@DATA", Data);
                           
                            conn.Open();
                            cmd.ExecuteReader();     // Here our query will be executed and data saved into the database. 
                          
                            conn.Close();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                        InitializeGridViewFestiusOcasionals();


                    }
                }
            }
        }

        private void dataGridViewOperarisControlAssistencial_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dataGridViewOperarisControlAssistencial.Columns[e.ColumnIndex].Name == "Image")
            {
                string path = dataGridViewOperarisControlAssistencial.Rows[e.RowIndex].Cells["FotografiaPath"].Value.ToString();
                // Your code would go here - below is just the code I used to test
                if (File.Exists(path))
                {
                    Image image = Image.FromFile(path);
                    var newImage = new Bitmap(dataGridViewOperarisControlAssistencial.Columns[e.ColumnIndex].Width, dataGridViewOperarisControlAssistencial.Rows[e.RowIndex].Height);
                    Graphics.FromImage(newImage).DrawImage(image, 0, 0, dataGridViewOperarisControlAssistencial.Columns[e.ColumnIndex].Width, dataGridViewOperarisControlAssistencial.Rows[e.RowIndex].Height);
                    e.Value = newImage;
                }
            }
        }

        private void txtBNomControlAssistencial_TextChanged(object sender, EventArgs e)
        {
            InitializeGridViewOperarisControlAssistencial();
        }

        private void txtBCognomsControlAssistencial_TextChanged(object sender, EventArgs e)
        {
            InitializeGridViewOperarisControlAssistencial();
        }

        private void txtBDNIControlAssitencial_TextChanged(object sender, EventArgs e)
        {
            InitializeGridViewOperarisControlAssistencial();
        }

        private void btAfegirAbsencia_Click(object sender, EventArgs e)
        {

            int selectedRowCount = dataGridViewOperarisControlAssistencial.Rows.GetRowCount(DataGridViewElementStates.Selected);
            if (selectedRowCount > 0)
            {
                for (int i = 0; i < selectedRowCount; i++)
                {
                    string Id = dataGridViewOperarisControlAssistencial.SelectedRows[i].Cells["OperariID"].Value.ToString();
                    string pathimage = dataGridViewOperarisControlAssistencial.SelectedRows[i].Cells["FotografiaPath"].Value.ToString();
                    string nom = dataGridViewOperarisControlAssistencial.SelectedRows[i].Cells["Nom"].Value.ToString();
                    string cognoms = dataGridViewOperarisControlAssistencial.SelectedRows[i].Cells["Cognoms"].Value.ToString();

                    AfegirAbsenciaForm form = new AfegirAbsenciaForm(mysqlconnect, Id, pathimage, nom, cognoms);

                    form.FormClosed += new System.Windows.Forms.FormClosedEventHandler(absencia_FormClosed);
                    form.Show();
                }
            }


        }

        private void numericUpDownAnyAbsencia_ValueChanged(object sender, EventArgs e)
        {
            InitializeGridViewAbsencies();
        }

        private void dataGridViewOperarisControlAssistencial_SelectionChanged(object sender, EventArgs e)
        {
            InitializeGridViewAbsencies();
        }

        private void btEliminarAbsencia_Click(object sender, EventArgs e)
        {

            int selectedRowCount = dataGridViewAbsències.Rows.GetRowCount(DataGridViewElementStates.Selected);
            if (selectedRowCount > 0)
            {
                var confirmEliminar = MessageBox.Show("Segur que vols eliminar les files seleccionades?",
                    "Confirmació d'Eliminació!",
                    MessageBoxButtons.YesNo);
                if (confirmEliminar == DialogResult.Yes)
                {
                    for (int i = 0; i < selectedRowCount; i++)
                    {
                        try
                        {
                            MySqlConnection conn = mysqlconnect.getmysqlconn();
                            string Operari = dataGridViewAbsències.SelectedRows[0].Cells["Operari"].Value.ToString();
                            string Data = dataGridViewAbsències.SelectedRows[0].Cells["Data"].Value.ToString();


                            string Query = "delete from Absencies  where (Operari,Data) in ((@Operari, STR_TO_DATE( @DATA, '%d/%m/%Y %H:%i:%s')) );";
                            MySqlCommand cmd = new MySqlCommand(Query, conn);
                            cmd.Parameters.AddWithValue("@Operari",Operari);
                            cmd.Parameters.AddWithValue("@DATA", Data);
                           
                            conn.Open();
                            cmd.ExecuteReader();     // Here our query will be executed and data saved into the database. 

                          
                            conn.Close();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                        InitializeGridViewAbsencies();
                        InitializeGridViewObres();
                        InitializeGridViewSelectorObra();
                        InitializeGridViewObrespendents();
                        InitializeGridViewOperarisDisponibles();
                        InitializeGridViewPlanning(DataGridViewPlanning, DateTime.DaysInMonth(dateTimePickerMesPlànning.Value.Year, dateTimePickerMesPlànning.Value.Month), dateTimePickerMesPlànning.Value, false);
                        InitializeGridViewPlanning(dataGridViewPlanningPropermes, 15, dateTimePickerMesPlànning.Value.AddMonths(1), true);
                        InitializeGridViewAbsenciesMensuals();
                        InitializeDataGridViewControlAnualAssistencial();
                       


                    }
                }
            }
        }

        private void btGenerarPlaning_Click(object sender, EventArgs e)
        {
            int selectedRowCount = dataGridViewSelectorObra.Rows.GetRowCount(DataGridViewElementStates.Selected);
            if (selectedRowCount > 0)
            {

                MySqlConnection conn = mysqlconnect.getmysqlconn();
                DateTime datainici = DateTime.Now;
                try
                {
                    string UO = dataGridViewSelectorObra.SelectedRows[0].Cells["Unitats d'Obra"].Value.ToString();
                    string Expedient = dataGridViewSelectorObra.SelectedRows[0].Cells["Expedient"].Value.ToString();

                    DataTable DtDades = new DataTable();
                    string query = "Select MIN(`Data`) "
                        + " from OperarisObraData where `Obra`= @ObraID ; ";



                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@ObraID", Expedient);
                    conn.Open();
                    MySqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {


                     
                        datainici = !reader.IsDBNull(0) ? reader.GetDateTime(0) : DateTime.Now;
                        
                     
                    }
                        conn.Close();
                }
                catch (Exception err)
                {
                    MessageBox.Show(err.Message);
                }
                if (datainici >= DateTime.Now || mysqlconnect.getPermissions()[0])
                {
                    if (dataGridViewOperarisperDefecte.Rows.Count > 0)
                    {
                        try
                        {
                            bool planinggenerat = (bool)dataGridViewSelectorObra.SelectedRows[0].Cells["Plàning Generat"].Value;
                            var confirmregenerar = DialogResult.Yes;
                            if (planinggenerat)
                            {
                                confirmregenerar = MessageBox.Show("Si continues s'eborrarà el plàning d'aquesta obra i l'hauràs de tornar a generar. Vols continuar?",
                                "Planning Reset!",
                                MessageBoxButtons.YesNo);
                            }
                            if (confirmregenerar == DialogResult.Yes)
                            {
                                 conn = mysqlconnect.getmysqlconn();
                                string Id = dataGridViewSelectorObra.SelectedRows[0].Cells["Expedient"].Value.ToString();


                                GenerarPlanningForm form = new GenerarPlanningForm(mysqlconnect, Id);

                                form.FormClosed += new System.Windows.Forms.FormClosedEventHandler(SelectorObraForm_FormClosed);


                                form.Show();
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
                    else { MessageBox.Show("Abans de generar el plànning, assigna els operaris per defecte."); }
                }
                else { MessageBox.Show("No pots regenerar un plànning antic sense permisos de Supervisor!"); }
            }
        }

        private void ChckBBloquejarPlanning_CheckedChanged(object sender, EventArgs e)
        {

            int selectedRowCount = dataGridViewSelectorObra.Rows.GetRowCount(DataGridViewElementStates.Selected);
            if (selectedRowCount > 0&& ChckBisUpdating==false)
            {
                try
                {
                    string Expedient = dataGridViewSelectorObra.SelectedRows[0].Cells["Expedient"].Value.ToString();
                MySqlConnection conn = mysqlconnect.getmysqlconn();


                string Query = "UPDATE Obres SET `Bloquejar Planning`=@BloqueigPlanning"
                         + " WHERE `Expedient`=@Expedient";
                MySqlCommand cmd = new MySqlCommand(Query, conn);
                cmd.Parameters.AddWithValue("@BloqueigPlanning", ChckBBloquejarPlanning.Checked);
                cmd.Parameters.AddWithValue("@Expedient", Expedient);
                conn.Open();
                cmd.ExecuteReader();

                conn.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);

                }

                int[] SelectedIndexs1 = Utils.PreselectedIndex(dataGridViewSelectorObra);
                InitializeGridViewSelectorObra();
                InitializeGridViewPlanning(DataGridViewPlanning, DateTime.DaysInMonth(dateTimePickerMesPlànning.Value.Year, dateTimePickerMesPlànning.Value.Month), dateTimePickerMesPlànning.Value, false);
                InitializeGridViewPlanning(dataGridViewPlanningPropermes, 15, dateTimePickerMesPlànning.Value.AddMonths(1), true);

                Utils.PostselectedIndexOnlyOneRow(dataGridViewSelectorObra, SelectedIndexs1);
            }
        }

        private void chckBDocOk_CheckedChanged(object sender, EventArgs e)
        {
            int selectedRowCount = dataGridViewSelectorObra.Rows.GetRowCount(DataGridViewElementStates.Selected);
            if (selectedRowCount > 0 && ChckBisUpdating == false)
            {
                try
                {
                    string Expedient = dataGridViewSelectorObra.SelectedRows[0].Cells["Expedient"].Value.ToString();
                    MySqlConnection conn = mysqlconnect.getmysqlconn();


                    string Query = "UPDATE Obres SET `Documentacio OK`=@DocOK"
                             + " WHERE `Expedient`=@Expedient";
                    MySqlCommand cmd = new MySqlCommand(Query, conn);
                    cmd.Parameters.AddWithValue("@DocOK", chckBDocOk.Checked);
                    cmd.Parameters.AddWithValue("@Expedient", Expedient);
                    conn.Open();
                    cmd.ExecuteReader();

                    conn.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);

                }

                int[] SelectedIndexs1 = Utils.PreselectedIndex(dataGridViewSelectorObra);
                InitializeGridViewSelectorObra();
                InitializeGridViewPlanning(DataGridViewPlanning, DateTime.DaysInMonth(dateTimePickerMesPlànning.Value.Year, dateTimePickerMesPlànning.Value.Month), dateTimePickerMesPlànning.Value, false);
                InitializeGridViewPlanning(dataGridViewPlanningPropermes, 15, dateTimePickerMesPlànning.Value.AddMonths(1), true);
                Utils.PostselectedIndexOnlyOneRow(dataGridViewSelectorObra, SelectedIndexs1);
            }
        }

        private void btDiaAnterior_Click(object sender, EventArgs e)
        {

            DateTime DiaObra =(DateTime) dateTimePickerDiaObra.Value;
         DiaObra=  DiaObra.AddDays(-1);
            dateTimePickerDiaObra.Value = DiaObra;
        }

        private void btDiaPosterior_Click(object sender, EventArgs e)
        {
            DateTime DiaObra = (DateTime)dateTimePickerDiaObra.Value;
            DiaObra=DiaObra.AddDays(+1);
            dateTimePickerDiaObra.Value = DiaObra;
        }

        private void dateTimePickerDiaObra_ValueChanged(object sender, EventArgs e)
        {
            InitializeGridViewOperarisDisponibles();
            InitializeGridViewOperarisAssignats();
            InitializeGridViewVehiclesDisponibles();
                    InitializeGridViewVehiclesAssignats();
               }

        private void btEditarPlaning_Click(object sender, EventArgs e)
        {
            int selectedRowCount = dataGridViewSelectorObra.Rows.GetRowCount(DataGridViewElementStates.Selected);
            if (selectedRowCount > 0)
            {
                try
                {
                    MySqlConnection conn = mysqlconnect.getmysqlconn();
                    string Id = dataGridViewSelectorObra.SelectedRows[0].Cells["Expedient"].Value.ToString();


                    EditarPlanningForm form = new EditarPlanningForm(mysqlconnect, Id);

                    form.FormClosed += new System.Windows.Forms.FormClosedEventHandler(SelectorObraForm_FormClosed);


                    form.Show();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void btTreureOpObra_Click(object sender, EventArgs e)
        {

            MySqlConnection conn = mysqlconnect.getmysqlconn();
            int selectedRowCount = dataGridViewSelectorObra.Rows.GetRowCount(DataGridViewElementStates.Selected);
            if (selectedRowCount > 0)
            {
                try
                {
                    string Expedient = dataGridViewSelectorObra.SelectedRows[0].Cells["Expedient"].Value.ToString();
                    int selectedRowCount2 = dataGridViewOperarisAssignats.Rows.GetRowCount(DataGridViewElementStates.Selected);
                    for (int i = 0; i < selectedRowCount2; i++)
                    {

                        string Operari = dataGridViewOperarisAssignats.SelectedRows[i].Cells["OperariID"].Value.ToString();
                        DateTime data = dateTimePickerDiaObra.Value;
                        string Query = "delete from OperarisObraData  where (Operari,Obra,Data) in ((@OperariID,@Expedient,@Data));"; ;
                        MySqlCommand cmd = new MySqlCommand(Query, conn);
                        cmd.Parameters.AddWithValue("@OperariID", Operari);
                        cmd.Parameters.AddWithValue("@Expedient", Expedient);
                        cmd.Parameters.AddWithValue("@Data", data.ToString("yyyy-MM-dd"));
                        conn.Open();
                        cmd.ExecuteReader();
                        conn.Close();
                    }
               }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);

                }
                finally
                {
                    conn.Close();
                    InitializeGridViewPlanning(DataGridViewPlanning, DateTime.DaysInMonth(dateTimePickerMesPlànning.Value.Year, dateTimePickerMesPlànning.Value.Month), dateTimePickerMesPlànning.Value, false);
                    InitializeGridViewPlanning(dataGridViewPlanningPropermes, 15, dateTimePickerMesPlànning.Value.AddMonths(1), true);
                    InitializeGridViewOperarisDisponibles();
                    InitializeGridViewObrespendents();
                    InitializeGridViewOperarisAssignats();

                }
            }
        }

        private void dataGridViewOperarisAssignats_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dataGridViewOperarisAssignats.Columns[e.ColumnIndex].Name == "Image")
            {
                string path = dataGridViewOperarisAssignats.Rows[e.RowIndex].Cells["FotografiaPath"].Value.ToString();
                // Your code would go here - below is just the code I used to test
                if (File.Exists(path))
                {
                    Image image = Image.FromFile(path);
                    var newImage = new Bitmap(dataGridViewOperarisAssignats.Columns[e.ColumnIndex].Width, dataGridViewOperarisAssignats.Rows[e.RowIndex].Height);
                    Graphics.FromImage(newImage).DrawImage(image, 0, 0, dataGridViewOperarisAssignats.Columns[e.ColumnIndex].Width, dataGridViewOperarisAssignats.Rows[e.RowIndex].Height);
                    e.Value = newImage;
                }
            }
        }

        private void SeleccionarDades()
        {

            int selectedRowCount = dataGridViewSelectorObra.Rows.GetRowCount(DataGridViewElementStates.Selected);
            if (selectedRowCount > 0)
            {
                MySqlConnection conn = mysqlconnect.getmysqlconn();
                try
                {
                    string UO = dataGridViewSelectorObra.SelectedRows[0].Cells["Unitats d'Obra"].Value.ToString();
                    string Expedient = dataGridViewSelectorObra.SelectedRows[0].Cells["Expedient"].Value.ToString();
                   
                    DataTable DtDades = new DataTable();
                    string query = "Select Count(`Data`),MAX(`Data`) "
                        + " from OperarisObraData where `Obra`= @ObraID ; ";



                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@ObraID", Expedient);
                    conn.Open();
                    MySqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {


                       int UOplanificades = !reader.IsDBNull(0) ? reader.GetInt32(0) : 0;
                        DateTime MaxDate = !reader.IsDBNull(1) ? reader.GetDateTime(1) : DateTime.Now;
                         
                        lbTotalUO.Text = "Unitats d'Obra previstes: " + UO;
                        
                        lbPrevisioFin.Text = "Dia previst finalització: " + MaxDate.ToString("dd/MM/yyyy");
                    }
                    reader.Close();
                     DtDades = new DataTable();
                   query = "Select Operari op,`Data` dt, (Count(`Data`) / (Select count(`Data`) from operarisobradata where Operari = op "
                            +"and Data = dt)) from OperarisObraData where `Obra`= @ObraID  GROUP BY `Obra`,`Data`, `Operari`; ; ";



                     cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@ObraID", Expedient);
                  
                    reader = cmd.ExecuteReader();
                    float UOreals = 0;
                    while (reader.Read())
                    {


                       UOreals += !reader.IsDBNull(2) ? reader.GetFloat(2) : 0;
                      
                     
                    }
                    lbUOplanificades.Text = "Unitats d'Obra reals: " + UOreals;
                    conn.Close();
                }
                catch (Exception err)
                {
                    MessageBox.Show(err.Message);
                }
                finally { conn.Close(); }
            }
        }

        private void dateTimePickerMesPlànning_ValueChanged(object sender, EventArgs e)
        {
            InitializeGridViewPlanning(DataGridViewPlanning, DateTime.DaysInMonth(dateTimePickerMesPlànning.Value.Year, dateTimePickerMesPlànning.Value.Month), dateTimePickerMesPlànning.Value,false);
            InitializeGridViewPlanning(dataGridViewPlanningPropermes, 15, dateTimePickerMesPlànning.Value.AddMonths(1),true);
            InitializeGridViewAbsenciesMensuals();
            lbPropermes.Text ="Plànning "+ dateTimePickerMesPlànning.Value.AddMonths(1).ToString("MMMM 'del' yyyy");
        }

        private void dataGridViewPlànning_BindingContextChanged(object sender, EventArgs e)
        {
          //  pintarPlanning(DataGridViewPlanning, dateTimePickerMesPlànning.Value);


        }
     private void pintarPlanning(DataGridView DtgView, DateTime dTime) {
            if (DtgView.Rows.Count > 0)
            {


                for (int row = 0; row < DtgView.Rows.Count; row++)
                {

                    
                    bool InDocEntregada = DtgView.Rows[row].Cells["DOC ENTREGADA"].Value.Equals("OK" )? true : false;
                    bool InDoc = DtgView.Rows[row].Cells["DOC"].Value.Equals("OK" )? true : false;
                    DataGridViewCell cell = DtgView.Rows[row].Cells["DOC"];
                    if (InDocEntregada)
                    {

                        if (InDoc)
                        {

                            Color verd = Color.FromArgb(162, 255, 171);
                            cell.Style.BackColor = verd;
                        }
                        else
                        {
                            Color taronja = Color.FromArgb(249, 231, 159);
                            cell.Style.BackColor = taronja;
                        }

                    }
                    else
                    {
                        Color vermell = Color.FromArgb(245, 183, 177);
                        cell.Style.BackColor = vermell;


                    }
                 
                    DateTime avui = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
                    string date = DtgView.Rows[row].Cells["ULTIMA DATA OBRA"].Value.ToString();
                    DateTime ultimadataobra = new DateTime();
                    DateTime.TryParse(date, out ultimadataobra);
                    if (ultimadataobra < avui)
                    {
                        Color obrafinalitzada = Color.FromArgb(255, 255, 102);
                        DtgView.Rows[row].DefaultCellStyle.BackColor = obrafinalitzada;
                    }

                }
                MySqlConnection conn = mysqlconnect.getmysqlconn();
                DataTable DtDades = new DataTable();
                string query = "Select DATE_FORMAT(Data,'%d/%m')as `Data` from FestiusGeneral where month(Data)=@MONTH";

                MySqlCommand cmd = new MySqlCommand(query, conn);
            
                cmd.Parameters.AddWithValue("@MONTH", dTime.Month);
                conn.Close();
                MySqlDataAdapter mdaDades = new MySqlDataAdapter(cmd);
                mdaDades.Fill(DtDades);
                DataTable DtDades2 = new DataTable();
                query = "Select * from FestiusAnualsOcasionals where month(Data)=@MONTH AND YEAR(Data)=@YEAR";
               
                cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@YEAR", dTime.Year);
                cmd.Parameters.AddWithValue("@MONTH", dTime.Month);
                MySqlDataAdapter mdaDades2 = new MySqlDataAdapter(cmd);
                mdaDades.Fill(DtDades2);
                conn.Close();
                for (int row = 0; row < DtgView.Rows.Count; row++)
                {

                    foreach (DataGridViewCell cell in DtgView.Rows[row].Cells)
                    {
                        int dia;
                        if (Int32.TryParse(cell.OwningColumn.Name, out dia))
                        {
                            DateTime data = new DateTime(dTime.Year, dTime.Month, dia);
                            if ((int)data.DayOfWeek == 0 || (int)data.DayOfWeek == 6)
                            {
                                Color capdesetmana = Color.FromArgb(192, 192, 192);
                                cell.Style.BackColor = capdesetmana;

                            }
                            int n1 = DtDades.Select("[Data] ='" + data.ToString("dd/MM") + "'").Length;

                            int n2 = DtDades2.Select("[Data] ='" + data.ToString("yyyy-MM-dd") + "'").Length;
                            if (n1 > 0 || n2 > 0)
                            {
                                Color festiu = Color.FromArgb(192, 192, 192);
                                cell.Style.BackColor = festiu;
                            }
                        }


                    }

                }

            }
        }

        private void dataGridViewPlanningPropermes_BindingContextChanged(object sender, EventArgs e)
        {
            pintarPlanning(dataGridViewPlanningPropermes, dateTimePickerMesPlànning.Value.AddMonths(1));
        }

      
        private void pintarOperarisDisponibles()
        {
            if (dataGridViewOperarisDisponibles.Rows.Count > 0)
            {
                for (int row = 0; row < dataGridViewOperarisDisponibles.Rows.Count; row++)
                { int dispon = 1;
                    Int32.TryParse(dataGridViewOperarisDisponibles.Rows[row].Cells["Ocupació"].Value.ToString(), out dispon);
                   if (dispon == 0)
                    {
                           Color verd  = Color.FromArgb(162, 255, 171);
                        dataGridViewOperarisDisponibles.Rows[row].DefaultCellStyle.BackColor = verd;
                    }


                    

                }

            }
        }
        private void pintarVehiclesDisponibles()
        {
            if (dataGridViewVehiclesDisponibles.Rows.Count > 0)
            {
                for (int row = 0; row < dataGridViewVehiclesDisponibles.Rows.Count; row++)
                {
                    int dispon = 1;
                    Int32.TryParse(dataGridViewVehiclesDisponibles.Rows[row].Cells["Ocupació"].Value.ToString(), out dispon);
                    if (dispon == 0)
                    {
                        Color verd = Color.FromArgb(162, 255, 171);
                        dataGridViewVehiclesDisponibles.Rows[row].DefaultCellStyle.BackColor = verd;
                    }




                }

            }
        }

        private void btGenerarPlànning2_Click(object sender, EventArgs e)
        {
            int selectedRowCount = dataGridViewObrespendents.Rows.GetRowCount(DataGridViewElementStates.Selected);
            if (selectedRowCount > 0)
            {
                string Id = dataGridViewObrespendents.SelectedRows[0].Cells["Expedient"].Value.ToString();
                MySqlConnection conn = mysqlconnect.getmysqlconn();

                DataTable DtDades = new DataTable();
                string query = "Select  Count(*) "
                    + " from OperarisObraDefecte where Obra=@Expedient; ";

                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Expedient",Id);
                conn.Open();
                MySqlDataReader reader = cmd.ExecuteReader();

                Dictionary<string, MesdeObra> ControlMensual = new Dictionary<string, MesdeObra>();
                int count=0;
                if (reader.Read())
                {


                    count = !reader.IsDBNull(0) ? reader.GetInt16(0) : 0;
                   
                }

                conn.Close();

                if (count > 0)
                {
                    try
                    {
                      



                           conn = mysqlconnect.getmysqlconn();
                            
                         GenerarPlanningForm form = new GenerarPlanningForm(mysqlconnect, Id);

                            form.FormClosed += new System.Windows.Forms.FormClosedEventHandler(SelectorObraForm_FormClosed2);


                            form.Show();
                        
                    
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                else { MessageBox.Show("Abans de generar el plànning, assigna els operaris per defecte."); }
            }
        }

        private void btOperarisperdefecte_Click(object sender, EventArgs e)
        {
            int selectedRowCount = dataGridViewObrespendents.Rows.GetRowCount(DataGridViewElementStates.Selected);
            if (selectedRowCount > 0)
            {
                try
                {
                    MySqlConnection conn = mysqlconnect.getmysqlconn();
                    string Id = dataGridViewObrespendents.SelectedRows[0].Cells["Expedient"].Value.ToString();


                    SelectOperarisperDefecteForm form = new SelectOperarisperDefecteForm(mysqlconnect, Id);

                    form.FormClosed += new System.Windows.Forms.FormClosedEventHandler(SelectorObraForm_FormClosed2);


                    form.Show();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void chckBdiaOK_CheckedChanged(object sender, EventArgs e)
        {
            int selectedRowCount = dataGridViewSelectorObra.Rows.GetRowCount(DataGridViewElementStates.Selected);
            if (selectedRowCount > 0 && ChckBisUpdating == false)
            {
                try
                {
                    string Expedient = dataGridViewSelectorObra.SelectedRows[0].Cells["Expedient"].Value.ToString();
                    MySqlConnection conn = mysqlconnect.getmysqlconn();


                    string Query = "UPDATE Obres SET `DIA OK`=@DiaOK"
                             + " WHERE `Expedient`=@Expedient";
                    MySqlCommand cmd = new MySqlCommand(Query, conn);
                    cmd.Parameters.AddWithValue("@DiaOK",chckBdiaOK.Checked);
                    cmd.Parameters.AddWithValue("@Expedient", Expedient);
                    conn.Open();
                    cmd.ExecuteReader();

                    conn.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);

                }

                int[] SelectedIndexs1 = Utils.PreselectedIndex(dataGridViewSelectorObra);
                InitializeGridViewSelectorObra();
                InitializeGridViewPlanning(DataGridViewPlanning, DateTime.DaysInMonth(dateTimePickerMesPlànning.Value.Year, dateTimePickerMesPlànning.Value.Month), dateTimePickerMesPlànning.Value, false);
                InitializeGridViewPlanning(dataGridViewPlanningPropermes, 15, dateTimePickerMesPlànning.Value.AddMonths(1), true);
                Utils.PostselectedIndexOnlyOneRow(dataGridViewSelectorObra, SelectedIndexs1);
            }
        }

        private void numericUpDownAnyControlAssistencial_ValueChanged(object sender, EventArgs e)
        {
           if(!initdate)
            InitializeDataGridViewControlAnualAssistencial();
        }

        private void txtBExpedientControlObres_TextChanged(object sender, EventArgs e)
        {
            InitializeGridViewControlObra();
        }

        private void txtBClientControlObres_TextChanged(object sender, EventArgs e)
        {
            InitializeGridViewControlObra();
        }

        private void dataGridViewOperaris_DoubleClick(object sender, EventArgs e)
        {
            int selectedRowCount = dataGridViewOperaris.Rows.GetRowCount(DataGridViewElementStates.Selected);
            if (selectedRowCount > 0)
            {
                for (int i = 0; i < selectedRowCount; i++)
                {
                    string Id = dataGridViewOperaris.SelectedRows[i].Cells["OperariID"].Value.ToString();

                    ModificarOperariForm form = new ModificarOperariForm(mysqlconnect, Id, imatgesDir);

                    form.FormClosed += new System.Windows.Forms.FormClosedEventHandler(OperariForm_FormClosed);

                    form.Show();
                }
            }
        }

        private void tabControl2_DoubleClick(object sender, EventArgs e)
        {

            int selectedRowCount = dataGridViewObres.Rows.GetRowCount(DataGridViewElementStates.Selected);
            if (selectedRowCount > 0)
            {
                for (int i = 0; i < selectedRowCount; i++)
                {
                    string Id = dataGridViewObres.SelectedRows[i].Cells["Expedient"].Value.ToString();
                    ModificarObraForm form = new ModificarObraForm(mysqlconnect, Id);

                    form.FormClosed += new System.Windows.Forms.FormClosedEventHandler(ObraForm_FormClosed);

                    form.Show();
                }
            }
        }

        private void dataGridViewOperarisDisponibles_DoubleClick(object sender, EventArgs e)
        {
            btAfegirOpObra_Click(new object(), new EventArgs() );
        }

        private void chckDocEntregada_CheckedChanged(object sender, EventArgs e)
        {
            int selectedRowCount = dataGridViewSelectorObra.Rows.GetRowCount(DataGridViewElementStates.Selected);
            if (selectedRowCount > 0 && ChckBisUpdating == false)
            {
                try
                {
                    string Expedient = dataGridViewSelectorObra.SelectedRows[0].Cells["Expedient"].Value.ToString();
                    MySqlConnection conn = mysqlconnect.getmysqlconn();


                    string Query = "UPDATE Obres SET `Documentació entregada`=@DocEntregada"
                             + " WHERE `Expedient`=@Expedient";
                    MySqlCommand cmd = new MySqlCommand(Query, conn);
                    cmd.Parameters.AddWithValue("@DocEntregada", chckDocEntregada.Checked);
                    cmd.Parameters.AddWithValue("@Expedient", Expedient);
                    conn.Open();
                    cmd.ExecuteReader();

                    conn.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);

                }

                int[] SelectedIndexs1 = Utils.PreselectedIndex(dataGridViewSelectorObra);
                InitializeGridViewSelectorObra();
                InitializeGridViewPlanning(DataGridViewPlanning, DateTime.DaysInMonth(dateTimePickerMesPlànning.Value.Year, dateTimePickerMesPlànning.Value.Month), dateTimePickerMesPlànning.Value, false);
                InitializeGridViewPlanning(dataGridViewPlanningPropermes, 15, dateTimePickerMesPlànning.Value.AddMonths(1), true);
                Utils.PostselectedIndexOnlyOneRow(dataGridViewSelectorObra, SelectedIndexs1);
            }
        }

        private void dataGridViewPlànning_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            

                //pintarPlanning(DataGridViewPlanning, dateTimePickerMesPlànning.Value);
          
        }

        private void btAltaVehicle_Click(object sender, EventArgs e)
        {
            AddVehicleForm form = new AddVehicleForm(mysqlconnect);

            form.FormClosed += new System.Windows.Forms.FormClosedEventHandler(VehicleForm_FormClosed);

            form.Show();
        }

        private void txtBMarca_TextChanged(object sender, EventArgs e)
        {
            InitializeGridViewVehicles();
        }

        private void groupBox9_Enter(object sender, EventArgs e)
        {

        }

        private void txtBMatricula_TextChanged(object sender, EventArgs e)
        {
            InitializeGridViewVehicles();
        }

        private void btModificarVehicle_Click(object sender, EventArgs e)
        {
            int selectedRowCount = dataGridViewVehicles.Rows.GetRowCount(DataGridViewElementStates.Selected);
            if (selectedRowCount > 0)
            {
                for (int i = 0; i < selectedRowCount; i++)
                {
                    string Id = dataGridViewVehicles.SelectedRows[i].Cells["VehicleID"].Value.ToString();

                    ModificarVehicleForm form = new ModificarVehicleForm(mysqlconnect, Id);

                    form.FormClosed += new System.Windows.Forms.FormClosedEventHandler(VehicleForm_FormClosed);

                    form.Show();
                }
            }
        }

        private void btAfegirVehicle_Click(object sender, EventArgs e)
        {
            MySqlConnection conn = mysqlconnect.getmysqlconn();
            int selectedRowCount = dataGridViewSelectorObra.Rows.GetRowCount(DataGridViewElementStates.Selected);

            if (selectedRowCount > 0)
            {
                try
                {
                    string Expedient = dataGridViewSelectorObra.SelectedRows[0].Cells["Expedient"].Value.ToString();
                    DateTime data = dateTimePickerDiaObra.Value;
                    int selectedRowCount2 = dataGridViewVehiclesDisponibles.Rows.GetRowCount(DataGridViewElementStates.Selected);
                    for (int i = 0; i < selectedRowCount2; i++)
                    {

                       

                        string Vehicle = dataGridViewVehiclesDisponibles.SelectedRows[i].Cells["VehicleID"].Value.ToString();
                        
                            string query = "Select `Expedient`, `Client` from Obres INNER JOIN vehiclesobradata ON vehiclesobradata.Obra=Obres.Expedient"
                                + " Where vehiclesobradata.Data = @Data AND vehiclesobradata.Vehicle = @VehicleID AND vehiclesobradata.Obra != @Expedient;";

                            MySqlCommand cmd = new MySqlCommand(query, conn);
                            cmd.Parameters.AddWithValue("@VehicleID", Vehicle);
                            cmd.Parameters.AddWithValue("@Expedient", Expedient);
                            cmd.Parameters.AddWithValue("@Data", data.ToString("yyyy-MM-dd"));
                            conn.Open();
                            MySqlDataReader reader = cmd.ExecuteReader();

                            string messageinit = "El vehicle ja té obres assignades el dia " + data.ToString("dd/MM/yyyy") + " en:\n";
                            string message = messageinit;
                            while (reader.Read())
                            {


                                string ExpedientObra = !reader.IsDBNull(0) ? reader.GetString(0) : null;
                                string Client = !reader.IsDBNull(1) ? reader.GetString(1) : null;
                                message = message + "\t La obra amb expdient " + ExpedientObra + " i client " + Client + "\n";


                            }
                            conn.Close();
                            var confirmInsersió = DialogResult.Yes;
                     
                        if (message != messageinit)
                            {
                                confirmInsersió = MessageBox.Show(message + " Segur que vols tornar a inserir una obra per aquest dia i aquest operari?",
                                         "Confirmació d'Insersió!",
                                         MessageBoxButtons.YesNo);
                            }
                            if (confirmInsersió == DialogResult.Yes)
                            {

                           
                            query = "INSERT INTO VehiclesObraData(Vehicle,`Obra`,`Data`) "
                                  + "VALUES(@VehicleID,@Expedient, @Data); ";
                                cmd = new MySqlCommand(query, conn);
                                cmd.Parameters.AddWithValue("@VehicleID", Vehicle);
                                cmd.Parameters.AddWithValue("@Expedient", Expedient);
                                cmd.Parameters.AddWithValue("@Data", data.ToString("yyyy-MM-dd"));
                                conn.Open();
                                cmd.ExecuteReader();
                                conn.Close();
                            }
                        }
                    

                }
                catch (Exception ex)
                {
                  //  MessageBox.Show(ex.Message);

                }
                finally
                {
                    conn.Close();
                   
                    InitializeGridViewVehiclesDisponibles();
                    InitializeGridViewVehiclesAssignats();

                }
            }
        }

        private void btTreureVehicle_Click(object sender, EventArgs e)
        {

            MySqlConnection conn = mysqlconnect.getmysqlconn();
            int selectedRowCount = dataGridViewSelectorObra.Rows.GetRowCount(DataGridViewElementStates.Selected);
            if (selectedRowCount > 0)
            {
                try
                {
                    string Expedient = dataGridViewSelectorObra.SelectedRows[0].Cells["Expedient"].Value.ToString();
                    int selectedRowCount2 = dataGridViewVehiclesAssignats.Rows.GetRowCount(DataGridViewElementStates.Selected);
                    for (int i = 0; i < selectedRowCount2; i++)
                    {

                        string Vehicle = dataGridViewVehiclesAssignats.SelectedRows[i].Cells["VehicleID"].Value.ToString();
                        DateTime data = dateTimePickerDiaObra.Value;
                        string Query = "delete from VehiclesObraData  where (Vehicle,Obra,Data) in ((@VehicleID,@Expedient,@Data));"; ;
                        MySqlCommand cmd = new MySqlCommand(Query, conn);
                        cmd.Parameters.AddWithValue("@VehicleID", Vehicle);
                        cmd.Parameters.AddWithValue("@Expedient", Expedient);
                        cmd.Parameters.AddWithValue("@Data", data.ToString("yyyy-MM-dd"));
                        conn.Open();
                        cmd.ExecuteReader();
                        conn.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);

                }
                finally
                {
                    conn.Close();
                    InitializeGridViewVehiclesDisponibles();
                    InitializeGridViewVehiclesAssignats();

                }
            }
        }

        private void btMesAnterior_Click(object sender, EventArgs e)
        {

            DateTime MesPlanning = (DateTime)dateTimePickerMesPlànning.Value;
            MesPlanning = MesPlanning.AddMonths(-1);
            dateTimePickerMesPlànning.Value = MesPlanning;
        }

        private void btMesPosterior_Click(object sender, EventArgs e)
        {

            DateTime MesPlanning = (DateTime)dateTimePickerMesPlànning.Value;
            MesPlanning = MesPlanning.AddMonths(+1);
            dateTimePickerMesPlànning.Value = MesPlanning;
        }

        private void btBaixaOperari_Click(object sender, EventArgs e)
        {
            int selectedRowCount = dataGridViewOperaris.Rows.GetRowCount(DataGridViewElementStates.Selected);
            if (selectedRowCount > 0)
            {
                for (int i = 0; i < selectedRowCount; i++)
                {
                    string Id = dataGridViewOperaris.SelectedRows[i].Cells["OperariID"].Value.ToString();

                    BaixaOperariForm form = new BaixaOperariForm(mysqlconnect, Id);

                    form.FormClosed += new System.Windows.Forms.FormClosedEventHandler(OperariForm_FormClosed);

                    form.Show();
                }
            }
        }

        private void btEliminarVehicle_Click(object sender, EventArgs e)
        {

            int selectedRowCount = dataGridViewVehicles.Rows.GetRowCount(DataGridViewElementStates.Selected);
            if (selectedRowCount > 0)
            {
                var confirmEliminar = MessageBox.Show("Segur que vols eliminar les files seleccionades?",
                    "Confirmació d'Eliminació!",
                    MessageBoxButtons.YesNo);
                if (confirmEliminar == DialogResult.Yes)
                {
                    for (int i = 0; i < selectedRowCount; i++)
                    {
                        try
                        {
                            MySqlConnection conn = mysqlconnect.getmysqlconn();
                            string Id = dataGridViewVehicles.SelectedRows[0].Cells["VehicleID"].Value.ToString();



                            string Query = "delete from Vehicles where VehicleID=" + Id + ";";
                            MySqlCommand cmd = new MySqlCommand(Query, conn);

                            conn.Open();
                            cmd.ExecuteReader();     // Here our query will be executed and data saved into the database. 

                            conn.Close();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }

                        InitializeGridViewVehicles();
                        InitializeGridViewVehiclesDisponibles();
                        InitializeGridViewVehiclesAssignats();

                    }
                }

            }

        }

        private void btBaixaVehicle_Click(object sender, EventArgs e)
        {
            int selectedRowCount = dataGridViewVehicles.Rows.GetRowCount(DataGridViewElementStates.Selected);
            if (selectedRowCount > 0)
            {
                for (int i = 0; i < selectedRowCount; i++)
                {
                    string Id = dataGridViewVehicles.SelectedRows[i].Cells["VehicleID"].Value.ToString();

                    VehiclesBaixaForm form = new VehiclesBaixaForm(mysqlconnect, Id);

                    form.FormClosed += new System.Windows.Forms.FormClosedEventHandler(VehicleForm_FormClosed);

                    form.Show();
                }
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void dataGridViewObres_DoubleClick(object sender, EventArgs e)
        {

            int selectedRowCount = dataGridViewObres.Rows.GetRowCount(DataGridViewElementStates.Selected);
            if (selectedRowCount > 0)
            {
                for (int i = 0; i < selectedRowCount; i++)
                {
                    string Id = dataGridViewObres.SelectedRows[i].Cells["Expedient"].Value.ToString();
                    ModificarObraForm form = new ModificarObraForm(mysqlconnect, Id);

                    form.FormClosed += new System.Windows.Forms.FormClosedEventHandler(ObraForm_FormClosed);

                    form.Show();
                }
            }
        }

        private void dataGridViewVehicles_DoubleClick(object sender, EventArgs e)
        {
            int selectedRowCount = dataGridViewVehicles.Rows.GetRowCount(DataGridViewElementStates.Selected);
            if (selectedRowCount > 0)
            {
                for (int i = 0; i < selectedRowCount; i++)
                {
                    string Id = dataGridViewVehicles.SelectedRows[i].Cells["VehicleID"].Value.ToString();

                    ModificarVehicleForm form = new ModificarVehicleForm(mysqlconnect, Id);

                    form.FormClosed += new System.Windows.Forms.FormClosedEventHandler(VehicleForm_FormClosed);

                    form.Show();
                }
            }
        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }

}

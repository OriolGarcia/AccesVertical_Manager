using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AccesVertical_Manager
{
 public   class Connection
    {
        private String URLServidor;
        private uint Port;
        private String UID;
        private String Password;
        private String Database;
        private Boolean[] Permissions = { false, false };
        MySqlConnection mysqlconn;
        private string NickName;
        private MySqlCommand cmd;
        public Connection(String URLServidor, uint Port,
        String UID, String Password, String Database)
        {

            this.URLServidor = URLServidor;
            this.Port = Port;
            this.UID = UID;
            this.Password = Password;
            this.Database = Database;
           
        }


        public MySqlConnection getmysqlconn()
        {

            return mysqlconn;

        }

        public bool InicialitzarBD()
        {
            bool connexiocorrecte = false;
            MySqlConnectionStringBuilder builder = new MySqlConnectionStringBuilder();
            builder.Server = URLServidor;
            builder.Port = Port;
            builder.UserID = UID;
            builder.Password = Password;
            builder.Database = Database;

            String ConnString = builder.ToString();
            builder = null;

            try
            {
                mysqlconn = new MySqlConnection(ConnString);
                mysqlconn.Open();
                connexiocorrecte = true;
                mysqlconn.Close();
            }
            catch (ArgumentException a_ex)
            {
                MessageBox.Show("Error de paràmetres de connexió, revisi el formulari", "Missatge informatiu", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            catch (MySqlException ex)
            {

                connexiocorrecte = false;
                switch (ex.Number)
                {
                    //http://dev.mysql.com/doc/refman/5.0/en/error-messages-server.html
                    case 1042:
                        MessageBox.Show("Error de connexió, revisi l'adreça del servidor i el port", "Missatge informatiu", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                        break;
                    case 0: // Access denied (Check DB name,username,password)
                        MessageBox.Show("Accés denegat a la Base de dades: " + Database + ".\n. Configuri correctaement la base de dades o canviï la base de dades.", "Missatge informatiu", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        break;
                    default:
                        break;
                }
            }
            finally
            {
                if (mysqlconn.State == ConnectionState.Open)
                {
                    mysqlconn.Close();
                }
            }
            return connexiocorrecte;

        }
        public Boolean[] getPermissions() {
            return Permissions;

        }
        public String getNickName()
        {
            return NickName;

        }
        public bool LoginAccesVerticalManager(string Nickname, string Password)
        {
            MySqlConnection conn = mysqlconn;
            DataTable DtDadesUser = new DataTable();



            string Query = "Select if ( (Select Password from Users where Nickname=@Nickname) = MD5(@Password), 'true', 'false') ";
            conn.Open();
            MySqlCommand cmd = new MySqlCommand(Query, conn);
            cmd.Parameters.AddWithValue("@Nickname", Nickname);
            cmd.Parameters.AddWithValue("@Password", Password);
            MySqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                try
                {
                    Boolean CorrectLogin = !reader.IsDBNull(0) ? reader.GetBoolean(0) : false;
                    reader.Close();
                    if (CorrectLogin)
                    {
                        
                        Query = "Select `SupervisorPermissions`,`ManagerPermissions`,Active from Users where Nickname=@Nickname; ";

                        cmd = new MySqlCommand(Query, conn);
                        cmd.Parameters.AddWithValue("@Nickname", Nickname);
                        reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            
                            Permissions[0] = !reader.IsDBNull(0) ? reader.GetBoolean(0) : false;
                            Permissions[1] = !reader.IsDBNull(1) ? reader.GetBoolean(1) : false;
                            Boolean Active = !reader.IsDBNull(2) ? reader.GetBoolean(2) : false;
                            if (!Active)
                            {
                                MessageBox.Show("Usuari no actiu!");
                                conn.Close();
                                return false;

                            }
                            reader.Close();
                            NickName = Nickname;
                            return true;
                        }
                        return false;
                    }
                    else
                    {

                        MessageBox.Show("Login incorrecte");
                        conn.Close();
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return false;
                }
                finally
                {

                    reader.Close();
                    conn.Close();

                }
            }
            else
            {
                MessageBox.Show("Login incorrecte");

                return false;
            }

            
        }

        public bool ComandMysql(string query)
        {
            try
            {
                mysqlconn.Open();

                cmd = new MySqlCommand(query, mysqlconn);
                List<string> Taules = new List<string>();
                MySqlDataReader reader = cmd.ExecuteReader();
                mysqlconn.Close();
                reader.Close();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;

            }
            finally
            {

                mysqlconn.Close();
            }

        }
        public bool Check()
        {
            mysqlconn.Open();

           MySqlCommand cmd = new MySqlCommand("SHOW TABLES FROM " + Database + ";", mysqlconn);
            List<string> Taules = new List<string>();
            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {


                Taules.Add((!reader.IsDBNull(0) ? reader.GetString(0) : null).ToLower());

            }
            
            reader.Close();
            mysqlconn.Close();
           
                if ((!Taules.Contains("users")))
                {
                    var confirmReset = MessageBox.Show("No s'ha trobat la taula Users. Vols crear-la ara?",
                   "Taula Users",
                   MessageBoxButtons.YesNo);
                    if (confirmReset == DialogResult.Yes)
                    {

                        mysqlconn.Open();
                        MySqlDataReader MyReader2;

                        try
                        {


                            string Query = "CREATE TABLE IF NOT EXISTS Users ( "
                                 + "`Nickname` VARCHAR(25) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,"
                                + "`Password` VARCHAR(32) NOT NULL,"
                                + "`SupervisorPermissions` BOOLEAN NOT NULL DEFAULT 0,"
                                + "`ManagerPermissions` BOOLEAN NOT NULL DEFAULT 0,"
                                + "`Active` BOOLEAN NOT NULL DEFAULT 1,"
                                + "PRIMARY KEY (`Nickname`)"
                                + ") ENGINE = INNODB CHARACTER SET utf8 COLLATE utf8_general_ci; ";
                            cmd = new MySqlCommand(Query, mysqlconn);
                            MyReader2 = cmd.ExecuteReader();
                            MyReader2.Close();
                        mysqlconn.Close();
                        if (ComandMysql("Insert INTO Users ( Nickname,Password,`SupervisorPermissions`,`ManagerPermissions`,Active) VALUES ('Admin',MD5('AV12345'),true,true,true);"))
                            {
                                MessageBox.Show("S'acaba de crear un usuari 'Admin' amb permisos de supervisor i password 'AV12345'  .");
                            }
                            

                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        return false;
                        ;
                    }
                        finally
                        {

                            mysqlconn.Close();

                        }

                }else { return false; }





                  

                }
              if ((!Taules.Contains("operaris")) || (!Taules.Contains("formacions"))
                || (!Taules.Contains("formacionsoperaris")) || (!Taules.Contains("tècnics")) || (!Taules.Contains("obres"))
                || (!Taules.Contains("formacionsobres")) || (!Taules.Contains("operarisobradefecte")) || (!Taules.Contains("operarisobradata")) || (!Taules.Contains("festiusgeneral"))
                || (!Taules.Contains("festiusanualsocasionals"))
                 || (!Taules.Contains("absencies")))
            {
                        var confirmReset = MessageBox.Show("No s'han trobat algunes taules necessàries.Vols crear-les ara?",
                          "Taules",
                           MessageBoxButtons.YesNo);
                        if (confirmReset == DialogResult.Yes)
                        {
                    mysqlconn.Open();
                    MySqlDataReader MyReader2;

                    try
                    {


                                                string Query = "CREATE TABLE IF NOT EXISTS Operaris( "
                                                    +"`OperariID` INT UNSIGNED NOT NULL AUTO_INCREMENT, "
                                                   + "`FotografiaPath` VARCHAR(1000),"
                                                   + "`Nom` VARCHAR(100) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,"
                                                   +"`Cognoms` VARCHAR(100) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,"
                                                   + "`Vinculació` VARCHAR(100),"
                                                   +"`Alta` Date default null,"
                                                    + "`Baixa` Date default null,"
                                                   + "`DNI` VARCHAR(25),"
                                                    +"`Adreça` VARCHAR(200),"
                                                    +"`Telèfon` VARCHAR(25),"
                                                    +"`Telèfon Mòbil` VARCHAR(25),"
                                                     +"`Correu electrònic` VARCHAR(150),"
                                                        +"`Data de naixament` DATE,"
                                                        +"`Nacionalitat` VARCHAR(25),"
                                                        +"`Numero Seguretat Social` VARCHAR(25),"
                                                         +"`Categoria` VARCHAR(100),"
                                                        +"`Carnet Professional` VARCHAR(25),"
                                                         +"`Nivell` VARCHAR(25),"
                                                        +"PRIMARY KEY(`OperariID`)"
                                                       + ")ENGINE = INNODB CHARACTER SET utf8 COLLATE utf8_general_ci; ";
                   
                        cmd = new MySqlCommand(Query, mysqlconn);
                                                MyReader2 = cmd.ExecuteReader();
                                                MyReader2.Close();
                        Query = "CREATE TABLE IF NOT EXISTS Vehicles( "
                            +"`VehicleID` INT UNSIGNED NOT NULL AUTO_INCREMENT,"
                            +"`Marca` VARCHAR(100),"
                            +"`Alta` Date default null,"
                            +"`Baixa` Date default null,"
                            +"`Matricula` VARCHAR(100),"
                            +"`Model` VARCHAR(25) ,"
                              +"`Places`INT,"
                            +"`Baca` VARCHAR(25) ,"
                            +" `Propietari` VARCHAR(25) ,"
                            +"PRIMARY KEY (`VehicleID`)"
                            +")ENGINE = INNODB CHARACTER SET utf8 COLLATE utf8_general_ci; ";
                        cmd = new MySqlCommand(Query, mysqlconn);
                        MyReader2 = cmd.ExecuteReader();
                        MyReader2.Close();
                        Query = "CREATE TABLE IF NOT EXISTS Formacions( " 
                                                +"`FormacioID` INT UNSIGNED NOT NULL AUTO_INCREMENT,"
                                                +"`Titol` VARCHAR(100) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,"
                                                +"PRIMARY KEY (`FormacioID`)"
                                                +")ENGINE = INNODB CHARACTER SET utf8 COLLATE utf8_general_ci; ";
                                                cmd = new MySqlCommand(Query, mysqlconn);
                                                MyReader2 = cmd.ExecuteReader();
                                                MyReader2.Close();
                        Query = "CREATE TABLE IF NOT EXISTS FormacionsOperaris( "
                        + "`Operari` INT UNSIGNED NOT NULL,"
                            + "`Formacio` INT UNSIGNED NOT NULL,"
                            + "PRIMARY KEY(`Operari`, `Formacio`),"
                            + "CONSTRAINT `Constr_FormacionsOperaris_Operari_fk`"
                            + "FOREIGN KEY `Operari_fk` (`Operari`) REFERENCES `Operaris` (`OperariID`)"
                            + "ON DELETE CASCADE ON UPDATE CASCADE,"
                             + "CONSTRAINT `Constr_FormacionsOperaris_Formacio_fk`"
                                + "FOREIGN KEY `Formacio_fk` (`Formacio`) REFERENCES `Formacions` (`FormacioID`)"
                            + "ON DELETE CASCADE ON UPDATE CASCADE"
                            + ")ENGINE = INNODB CHARACTER SET utf8 COLLATE utf8_general_ci; ";
                      
                        cmd = new MySqlCommand(Query, mysqlconn);
                                                MyReader2 = cmd.ExecuteReader();
                                                MyReader2.Close();
                        Query = "CREATE TABLE IF NOT EXISTS Tècnics("
                        +"`TècnicID` INT UNSIGNED NOT NULL AUTO_INCREMENT,"
                        +"`Nom i cognoms` VARCHAR(1000),"
                            +"PRIMARY KEY(`TècnicID`)"
                          +  ")ENGINE = INNODB CHARACTER SET utf8 COLLATE utf8_general_ci; ";
                     
                        cmd = new MySqlCommand(Query, mysqlconn);
                        MyReader2 = cmd.ExecuteReader();
                        MyReader2.Close();
                        Query = "CREATE TABLE IF NOT EXISTS Obres("
                            +"`Expedient` INT UNSIGNED NOT NULL,"
                            +"`Client` VARCHAR(500) ,"
                            +"`CIF` VARCHAR(100) ,"
                            +"`Nº de Pressupost` VARCHAR(500) ,"
                            +"`Comentaris i recomenacions` VARCHAR(1500) ,"
                            +"`Activitat` VARCHAR(300), "
                            +"`Tècnic` INT UNSIGNED,"
                           + "`Unitats d'Obra` INT UNSIGNED,"
                           + "`Plàning Generat` boolean not null default 0,"
                           + "`Bloquejar Planning` boolean not null default 0,"
                            +"`Documentacio OK` boolean not null default 0,"
                            +"`Documentació entregada` boolean not null default 0,"
                            +"`DIA OK` boolean not null default 0,"
                           + "`Contacte 1` VARCHAR(150),"
                           + "`Telèfon contacte 1` VARCHAR(25) ,"
                            +"`Email contacte 1` varchar(150) ,"
                          +  "`Contacte 2` VARCHAR(150),"
                            +"`Telèfon contacte 2` VARCHAR(25) ,"
                            +"`Email contacte 2` varchar(150) ,"
                            +"`Industrial 1` varchar(150) ,"
                            +"`Contacte Industrial 1` VARCHAR(150),"
                            +"`Telèfon Industrial 1` VARCHAR(25) ,"
                            +"`Email Industrial 1` varchar(150) ,"
                            +"`Industrial 2` varchar(150) ,"
                            +"`Contacte Industrial 2` VARCHAR(150),"
                            +"`Telèfon Industrial 2` VARCHAR(25) ,"
                            +"`Email Industrial 2` varchar(150) ,"
                            +"`Industrial 3` varchar(150) ,"
                            +"`Contacte Industrial 3` VARCHAR(150),"
                            +"`Telèfon Industrial 3` VARCHAR(25) ,"
                            +"`Email Industrial 3` varchar(150) ,"
                            +"`Industrial 4` varchar(150) ,"
                            +"`Contacte Industrial 4` VARCHAR(150),"
                            +"`Telèfon Industrial 4` VARCHAR(25) ,"
                            +"`Email Industrial 4` varchar(150) ,"
                            +"PRIMARY KEY (`Expedient`),"
                            +"CONSTRAINT `Constr_Obres_Tècnic_fk`"
                             +   "FOREIGN KEY `Tècnic_fk` (`Tècnic`) REFERENCES `Tècnics` (`TècnicID`)"
                            +"ON DELETE CASCADE ON UPDATE CASCADE"
                            +")ENGINE = INNODB CHARACTER SET utf8 COLLATE utf8_general_ci";

                        
                       
                        cmd = new MySqlCommand(Query, mysqlconn);
                     
                                                MyReader2 = cmd.ExecuteReader();
                                                MyReader2.Close();
                       
                        Query = "CREATE TABLE IF NOT EXISTS FormacionsObres( "
                                                +"`Obra` INT UNSIGNED NOT NULL,"
                                                +"`Formacio` INT UNSIGNED NOT NULL,"
                                                +"PRIMARY KEY(`Obra`, `Formacio`),"
                                                +"CONSTRAINT `Constr_FormacionsObres_Operari_fk`"
                                                +"FOREIGN KEY `Obres_FormacioObres_fk` (`Obra`) REFERENCES `Obres` (`Expedient`)"
                                                  +"ON DELETE CASCADE ON UPDATE CASCADE,"
                                                    +"CONSTRAINT `Constr_FormacionsObres_Formacio_fk`"
                                                    +"FOREIGN KEY `Formacio_FormacioObres_fk` (`Formacio`) REFERENCES `Formacions` (`FormacioID`)"
                                                    +"ON DELETE CASCADE ON UPDATE CASCADE"
                                                    +")ENGINE = INNODB CHARACTER SET utf8 COLLATE utf8_general_ci; ";
                      
                        cmd = new MySqlCommand(Query, mysqlconn);
                                                MyReader2 = cmd.ExecuteReader();
                                                MyReader2.Close();
                                                Query = "CREATE TABLE IF NOT EXISTS OperarisObraDefecte( "
                                                +"`Operari` INT UNSIGNED NOT NULL,"
                                                +"`Obra` INT UNSIGNED NOT NULL,"
                                                +"PRIMARY KEY(`Operari`, `Obra`),"
                                                +"CONSTRAINT `Constr_OperarisObraDefecte_Operari_fk`"
                                                +"FOREIGN KEY `Operaris_OperarisObraDefecte_fk` (`Operari`) REFERENCES `Operaris` (`OperariID`)"
                                                +"ON DELETE CASCADE ON UPDATE CASCADE,"
                                                +"CONSTRAINT `Constr_OperarisObraDefecte_Obra_fk`"
                                                + "FOREIGN KEY `Obres_OperarisObraDefect_fk` (`Obra`) REFERENCES `Obres` (`Expedient`)"
                                                    +"ON DELETE CASCADE ON UPDATE CASCADE"
                                                        +")ENGINE = INNODB CHARACTER SET utf8 COLLATE utf8_general_ci; ";
                       
                        cmd = new MySqlCommand(Query, mysqlconn);
                                                MyReader2 = cmd.ExecuteReader();
                                                MyReader2.Close();
                                                MyReader2.Close();
                                                Query = "CREATE TABLE IF NOT EXISTS OperarisObraData( "
                                                +"`Operari` INT UNSIGNED NOT NULL,"
                                                +"`Obra` INT UNSIGNED NOT NULL,"
                                                +"`Data` DATE,"
                                                +"PRIMARY KEY (`Operari`, `Obra`,`Data`),"
                                                +"CONSTRAINT `Constr_OperarisObraData_Operari_fk`"
                                                +"FOREIGN KEY `Operaris_OperarisObraData_fk` (`Operari`) REFERENCES `Operaris` (`OperariID`)"
                                                +"ON DELETE CASCADE ON UPDATE CASCADE,"
                                                +"CONSTRAINT `Constr_OperarisObraData_Obra_fk`"
                                                +"FOREIGN KEY `Obres_OperarisObraData_fk` (`Obra`) REFERENCES `Obres` (`Expedient`)"
                                                +"ON DELETE CASCADE ON UPDATE CASCADE"
                                                    +")ENGINE = INNODB CHARACTER SET utf8 COLLATE utf8_general_ci; ";
                                                cmd = new MySqlCommand(Query, mysqlconn);
                                                MyReader2 = cmd.ExecuteReader();
                                                MyReader2.Close();
                                                    Query = "CREATE TABLE IF NOT EXISTS VehiclesObraData(  "
                                                       + "`Vehicle` INT UNSIGNED NOT NULL,"
                                                        +"`Obra` INT UNSIGNED NOT NULL,"
                                                          +   "`Data` DATE,"
                                                          +"PRIMARY KEY (`Vehicle`, `Obra`,`Data`),"
                                                           +"CONSTRAINT `Constr_VehiclesObraData_vehicle_fk`"
                                                         +"FOREIGN KEY `Vehicles_OperarisObraData_fk` (`Vehicle`) REFERENCES `Vehicles` (`VehicleID`)"
                                                           +" ON DELETE CASCADE ON UPDATE CASCADE,"
                                                            + " CONSTRAINT `Constr_VehiclesObraData_Obra_fk` "
                                                            +  " FOREIGN KEY `Obres_VehiclesObraData_fk` (`Obra`) REFERENCES `Obres` (`Expedient`)"
                                                         +       " ON DELETE CASCADE ON UPDATE CASCADE"
                                    +")ENGINE = INNODB CHARACTER SET utf8 COLLATE utf8_general_ci; ";     
                        cmd = new MySqlCommand(Query, mysqlconn);
                        MyReader2 = cmd.ExecuteReader();
                        MyReader2.Close();
                        Query = "CREATE TABLE IF NOT EXISTS FestiusGeneral("
                                                +"`Data` DATE,"
                                                +"PRIMARY KEY (`Data`)"
                                                   +")ENGINE=INNODB CHARACTER SET utf8 COLLATE utf8_general_ci;";
                        
                        cmd = new MySqlCommand(Query, mysqlconn);
                                                MyReader2 = cmd.ExecuteReader();
                                                MyReader2.Close();
                                                Query = "CREATE TABLE IF NOT EXISTS FestiusAnualsOcasionals( "
                                                    +"`Data` DATE,"
                                                    +"PRIMARY KEY (`Data`)"
                                                        +")ENGINE = INNODB CHARACTER SET utf8 COLLATE utf8_general_ci; ";
                        
                        cmd = new MySqlCommand(Query, mysqlconn);
                                                MyReader2 = cmd.ExecuteReader();
                                                MyReader2.Close();
                                                Query = "CREATE TABLE IF NOT EXISTS Absencies( "
                                            +"`Operari` INT UNSIGNED NOT NULL,"
                                            +"`Data` DATE,"
                                                +"`Motiu` VARCHAR(100) ,"
                                                +"PRIMARY KEY (`Operari`,`Data`),"
                                                +"CONSTRAINT `Constr_Absencies_Operari_fk`"
                                                +"FOREIGN KEY `Operari_fk` (`Operari`) REFERENCES `Operaris` (`OperariID`)"
                                                 +"ON DELETE CASCADE ON UPDATE CASCADE"
                                                +")ENGINE = INNODB CHARACTER SET utf8 COLLATE utf8_general_ci; ";
                             
                        cmd = new MySqlCommand(Query, mysqlconn);
                                                MyReader2 = cmd.ExecuteReader();
                                                MyReader2.Close();
                                                  MessageBox.Show("S'ha completat la insersió de taules. Recorda afegir els registres referents als noms dels tècnics i els tipus de formacions abans de fer ús del programa.");
                    }
                                            catch (Exception ex)
                                            {
                                                MessageBox.Show(ex.Message);
                                                 return false;

                                            }
                                            finally
                                            {

                                                mysqlconn.Close();
                                            }
                }
                else { return false; }
                }

            
            return true;
        }

    }
}

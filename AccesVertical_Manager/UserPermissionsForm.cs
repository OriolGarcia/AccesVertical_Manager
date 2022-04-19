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
    public partial class UserPermissionsForm : Form
    {

        private Connection mysqlconnect;
        private string Nickname;
        public UserPermissionsForm(Connection connect,string Nickname)
        {
            mysqlconnect = connect;
            this.Nickname = Nickname;
            InitializeComponent();
            SelectedItems();
        }
        private void SelectedItems()
        {
            lbNickname.Text = "Nickname: " + Nickname;
            MySqlConnection conn = mysqlconnect.getmysqlconn();
            try
            {
                DataTable DtDades = new DataTable();
                string query = "Select `SupervisorPermissions`,`ManagerPermissions`,Active from Users"
                   + " WHERE( NIckname=  ?NICKNAME)";
                //,  
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("?NICKNAME", Nickname);
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {

                    checkBPermissions.Checked = !reader.IsDBNull(0) ? reader.GetBoolean(0) : false;
                  checkManagerPermissions.Checked = !reader.IsDBNull(1) ? reader.GetBoolean(1) : false;
                    checkBActive.Checked = !reader.IsDBNull(0) ? reader.GetBoolean(0) : false;

                }
            }

            catch (Exception err)
            {
                MessageBox.Show(err.Message);

            }
            finally { conn.Close(); }


        }

        private void BtAcceptar_Click(object sender, EventArgs e)
        {

            MySqlConnection conn = mysqlconnect.getmysqlconn();
            conn.Open();
            MySqlDataReader MyReader2;

            try
            {


                string Query = "UPDATE Users SET `SupervisorPermissions` =@param_Permissions,"
                    +"`ManagerPermissions` =@param_managerPermissions,  Active=@param_Active"
                + " where Nickname=?NICKNAME;";
                MySqlCommand cmd = new MySqlCommand(Query, conn);
                cmd.Parameters.AddWithValue("@param_Permissions", checkBPermissions.Checked);
                cmd.Parameters.AddWithValue("@param_managerPermissions", checkManagerPermissions.Checked);
                cmd.Parameters.AddWithValue("@param_Active", checkBActive.Checked);
                cmd.Parameters.AddWithValue("?NICKNAME", Nickname);
                MyReader2 = cmd.ExecuteReader();     // Here our query will be executed and data saved into the database.  
                MyReader2.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
            finally
            {

                conn.Close(); this.Close();
            }
        }

        private void checkBPermissions_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBPermissions.Checked)
                checkManagerPermissions.Checked = true;
        }

        private void checkManagerPermissions_CheckedChanged(object sender, EventArgs e)
        {
            if (!checkManagerPermissions.Checked)
                checkBPermissions.Checked = false;
        }
    }
}

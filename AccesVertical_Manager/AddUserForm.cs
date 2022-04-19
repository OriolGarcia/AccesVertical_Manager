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
    public partial class AddUserForm : Form
    {


        private Connection mysqlconnect;
        public AddUserForm(Connection connect)
        {
            mysqlconnect = connect;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

           
            
            MySqlConnection conn = mysqlconnect.getmysqlconn();
            try
            {

                string Query = "INSERT INTO Users ( Nickname,Password,`SupervisorPermissions`,`ManagerPermissions`,Active) VALUES ("
                    + "@Nickname,MD5(@Password),@SupervisorPermisions,@ManagerPermissions,@Active)";
                MySqlCommand cmd = new MySqlCommand(Query, conn);
                cmd.Parameters.AddWithValue("@Nickname", txtBNickName.Text);
                cmd.Parameters.AddWithValue("@Password", txtBPassword.Text);
                cmd.Parameters.AddWithValue("@SupervisorPermisions",checkBPermissions.Checked);
                cmd.Parameters.AddWithValue("@ManagerPermissions", checkManagerPermissions.Checked);
                cmd.Parameters.AddWithValue("@Active", checkBActive.Checked);
                MySqlDataReader MyReader2;
                conn.Open();
                MyReader2 = cmd.ExecuteReader();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
            finally
            {
                conn.Close();
                this.Close();
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

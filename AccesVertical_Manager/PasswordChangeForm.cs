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
    public partial class PasswordChangeForm : Form
    {
        private string Nickname;
        private string oldPasswordMD5;
        private Connection mysqlconnect;
        public PasswordChangeForm(Connection connect)
        {
            mysqlconnect = connect;
            InitializeComponent();
            selectedItems();
            this.AcceptButton = btCanviarPassword;
        }

        private void selectedItems() {

           Nickname= mysqlconnect.getNickName();
            lbNickName.Text = "NickName: " + Nickname;
            MySqlConnection conn = mysqlconnect.getmysqlconn();
            try
            {
                DataTable DtDades = new DataTable();
                string query = "Select Password from Users"
                   + " WHERE( Nickname=  ?NICKNAME)";
                //,  
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("?NICKNAME", Nickname);
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {

                   oldPasswordMD5 = !reader.IsDBNull(0) ? reader.GetString(0) : null;
                   

                }
            }

            catch (Exception err)
            {
                MessageBox.Show(err.Message);

            }
            finally { conn.Close(); }


        

    }

    private void btAnular_Click(object sender, EventArgs e)
        {
            Close();
        }

private void btCanviarPassword_Click(object sender, EventArgs e)
        {

            if (CreateMD5(txtBOldPassword.Text) == oldPasswordMD5) {
                if (txtBNewPassword1.Text == txtBNewPassword2.Text) {
                    if (!string.IsNullOrEmpty(txtBNewPassword1.Text))
                    {


                        MySqlConnection conn = mysqlconnect.getmysqlconn();
                        conn.Open();
                        try
                        {
                            string Query = "UPDATE Users SET Password=MD5(@Password)"
                            + " where Nickname=?NICKNAME;";
                            MySqlCommand cmd = new MySqlCommand(Query, conn);
                            cmd.Parameters.AddWithValue("@Password", txtBNewPassword1.Text);
                            cmd.Parameters.AddWithValue("?NICKNAME", Nickname);
                            cmd.ExecuteReader();
                            MessageBox.Show("Password Canviat!");
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
                    else MessageBox.Show("El nou camp de password no pot estar buit!");
                }
                else  MessageBox.Show("El nou password ha de coinicidir en tots dos camps!"); }
            
            else MessageBox.Show("El password antic és incorrecte!");
               
           

        }

        public static string CreateMD5(string input)
        {
            // Use input string to calculate MD5 hash
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                // Convert the byte array to hexadecimal string
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("x2"));
                }
                return sb.ToString();
            }
        }
    }
}

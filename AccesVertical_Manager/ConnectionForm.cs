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
using System.Xml;

namespace AccesVertical_Manager
{
    public partial class ConnectionForm : Form
    {
        string user = "root";
        string password = "B64037609";

        public ConnectionForm()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.AcceptButton = btEntrar;
        }

        private void btEntrar_Click(object sender, EventArgs e)
        {
            // this.Close();
            uint port;
            if (uint.TryParse(txtBPort.Text, out port) && !string.IsNullOrEmpty(txtBBD.Text))
            {

                Connection connect = new Connection(txtBServer.Text, port,user,password, txtBBD.Text);
                if (connect.InicialitzarBD())
                {

                    if (checkBoxRemember.Checked)
                    {
                        try
                        {
                            XmlWriter xmlWriter = XmlWriter.Create("AccesVertical_Manager_info.xml");
                            xmlWriter.WriteStartDocument();
                            xmlWriter.WriteStartElement("PropietatsdeConexió");
                            xmlWriter.WriteStartElement("URL");
                            xmlWriter.WriteString(txtBServer.Text);
                            xmlWriter.WriteEndElement();

                            xmlWriter.WriteStartElement("port");
                            xmlWriter.WriteString(txtBPort.Text);
                            xmlWriter.WriteEndElement();

                            xmlWriter.WriteStartElement("BasedeDades");
                            xmlWriter.WriteString(txtBBD.Text);
                            xmlWriter.WriteEndDocument();

                            xmlWriter.Close();
                        }
                        catch (Exception err)
                        {
                            MessageBox.Show(err.Message);

                        }

                    }
                    if (connect.Check())
                    {
                        if (connect.LoginAccesVerticalManager(txtBnickname.Text, txtBPassword.Text))
                        {

                            MainForm f1 = new MainForm(connect);
                            f1.Show();
                            Visible = false;

                        }
                    }
                    else
                    {
                        MessageBox.Show("No podràs iniciar fins que no afegeixis totes les taules");
                    }




                }
            }
            else
            {
                if (string.IsNullOrEmpty(txtBBD.Text))
                {

                    MessageBox.Show("El paràmetre de la base de dades està buit", "Missatge informatiu", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    MessageBox.Show("El paràmetre del port no és vàlid. Escriu un port vàlid", "Missatge informatiu", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        private void ConnectionForm_Load(object sender, EventArgs e)
        {

            
        

            XmlDocument xmlDoc = new XmlDocument();
            if (File.Exists("AccesVertical_Manager_info.xml"))
            {
                xmlDoc.Load("AccesVertical_Manager_info.xml");
                XmlNodeList propitetats = xmlDoc.GetElementsByTagName("PropietatsdeConexió");
                XmlElement URL = (XmlElement)((XmlElement)propitetats[0]).GetElementsByTagName("URL")[0];
                XmlElement port = (XmlElement)((XmlElement)propitetats[0]).GetElementsByTagName("port")[0];
            
                XmlElement BasedeDades = (XmlElement)((XmlElement)propitetats[0]).GetElementsByTagName("BasedeDades")[0];
                txtBServer.Text = URL.InnerText;
                txtBPort.Text = port.InnerText;
                txtBBD.Text = BasedeDades.InnerText;
                XmlNode root = xmlDoc.FirstChild;

                checkBoxRemember.Checked = true;

            }
        }
    }
    }
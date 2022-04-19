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
    public partial class ConfigForm : Form
    {
        public ConfigForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderDlg = new FolderBrowserDialog();
            folderDlg.ShowNewFolderButton = true;
            // Show the FolderBrowserDialog.
            DialogResult result = folderDlg.ShowDialog();
            if (result == DialogResult.OK)
            {
                txtBDirectori.Text = folderDlg.SelectedPath;
                Environment.SpecialFolder root = folderDlg.RootFolder;
            }

        }

        private void btAcceptar_Click(object sender, EventArgs e)
        {

            try
            {
                XmlWriter xmlWriter = XmlWriter.Create("AccesVertical_Manager_config.xml");
                xmlWriter.WriteStartDocument();
                xmlWriter.WriteStartElement("Config");
                xmlWriter.WriteStartElement("DirImatges");
               
                xmlWriter.WriteString(txtBDirectori.Text);
                xmlWriter.WriteEndElement();

                xmlWriter.Close();

            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);

            }
            Close();
        }

        private void ConfigForm_Load(object sender, EventArgs e)
        {
            XmlDocument xmlDoc = new XmlDocument();
            if (File.Exists("AccesVertical_Manager_config.xml"))
            {
                xmlDoc.Load("AccesVertical_Manager_config.xml");
                XmlNodeList config = xmlDoc.GetElementsByTagName("Config");
                XmlElement DirImatges = (XmlElement)((XmlElement)config[0]).GetElementsByTagName("DirImatges")[0];
               
                txtBDirectori.Text = DirImatges.InnerText;
                XmlNode root = xmlDoc.FirstChild;
            }
            else {
                txtBDirectori.Text =@"\\SERVERACCES\Empresa\unitat compartida\MIS ARCHIVOS DE ORIGEN DE DATOS\ACCES VERTICAL MANAGER\Personal_Images";
            }
        }

        private void btAnular_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}

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
    public partial class ShowNewPassword : Form
    {
        public ShowNewPassword(string NickName, string Password)
        {
            InitializeComponent();
            lbNickName.Text = "NickName: " + NickName;
            textBox1.Text = Password;
        }

        private void lbNickName_Click(object sender, EventArgs e)
        {

        }

        private void btAcceptar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btCopy_Click(object sender, EventArgs e)
        {
            Clipboard.SetDataObject(textBox1.Text);
        }
    }
}

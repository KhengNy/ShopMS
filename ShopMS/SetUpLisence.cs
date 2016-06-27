using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShopMS
{
    public partial class SetUpLisence : Form
    {
        public SetUpLisence()
        {
            InitializeComponent();
        }

        private void SetUpLisence_Load(object sender, EventArgs e)
        {

        }

        private void btnGenKey_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(ShopMS.Properties.Settings.Default.serialMB))
            {
                ShopMS.Properties.Settings.Default.serialMB = Crypto.getSerialMB();
            }
            if (string.Equals(ShopMS.Properties.Settings.Default.serialMB, Crypto.getSerialMB()))
            {
                string password = Crypto.getSerialMB();
                string hint = Crypto.getSerialPC();
                txtGenKey.Text = Crypto.Encrypt(hint, password);
            }
            else
            {
                MessageBox.Show("Expired!");
            }
        }

        private void btnSetKey_Click(object sender, EventArgs e)
        {
            Crypto.setRegistry("KeyName", txtSetKey.Text);
        }

        private void btnCheck_Click(object sender, EventArgs e)
        {
            string hint = Crypto.Decrypt(Crypto.getRegistry("KeyName"), Crypto.getSerialMB());
            if (string.Equals(hint, Crypto.getSerialPC())){
                MessageBox.Show("OK");
            } else{
                MessageBox.Show("NO");
            }
        }
    }
}

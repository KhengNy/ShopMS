using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ShopMS.Commont;

namespace ShopMS
{
    public partial class Testing : Form
    {
        public Testing()
        {
            InitializeComponent();
        }

        private void Testing_Load(object sender, EventArgs e)
        {
            //DB.inset().into("EMPLOYEE_POSITION").set("Name", "Mobile App").set("Code", "MA").set("Status", "INACTIVE").run();
            //DB.delete().from("EMPLOYEE_POSITION").where("ID='d7ec8151-9bd0-446f-b486-88f167ee4b98'").run();
            //DB.update("EMPLOYEE_POSITION").set("Code", "WD").where("ID='0930A2BE-FB03-4999-8A03-9407A87261A9'").run();
            //Ctrl.sqlGridView("select * from EMPLOYEE_POSITION", dataGridView1);
            DB db = DB.select("*").from("EMPLOYEE_POSITION");
            Ctrl.sqlGridView(db, dataGridView1);
            //Ctrl.sqlCombo("select ID,Name from EMPLOYEE_POSITION", comboBox1);
            Ctrl.sqlCombo(db, comboBox1);
            string password = "EAAAAGbcFi5UObIPEpHZcEB7d0/4YWZ1eQ/ClmSuSqIyl4Mw";
            string hint = "123456789";
            textBox1.Text = Crypto.Encrypt(hint, password);
            //textBox1.Text = DateTime.Now.ToString();
            //dataGridView1.DataBindings;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Utils.login(textBox2.Text, textBox1.Text))
            {
                MessageBox.Show("OK");
            }
            else
            {
                MessageBox.Show("No");
            }
            //Ctrl.sqlGridView(textBox1.Text, dataGridView1);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            new SetUpLisence().Show();
        }
    }
}

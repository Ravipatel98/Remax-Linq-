using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace prjFinal_ravi
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }
        DataTable tabAdmin, tabClient, tabAgent;

        private void frmLogin_Load(object sender, EventArgs e)
        {
            clsGlobal.mySet = new DataSet();
            clsGlobal.myCon = new SqlConnection(@"Data Source=(localdb)\ProjectsV13;Initial Catalog=dbRamex;Integrated Security=True");
            clsGlobal.myCon.Open();

            SqlCommand mycmd = new SqlCommand("SELECT * FROM Admin", clsGlobal.myCon);
            clsGlobal.adpAdmin = new SqlDataAdapter(mycmd);
            clsGlobal.adpAdmin.Fill(clsGlobal.mySet, "Admin");

            mycmd = new SqlCommand("SELECT * FROM Agent", clsGlobal.myCon);
            clsGlobal.adpAgent = new SqlDataAdapter(mycmd);
            clsGlobal.adpAgent.Fill(clsGlobal.mySet, "Agent");

            mycmd = new SqlCommand("SELECT * FROM Client", clsGlobal.myCon);
            clsGlobal.adpClient = new SqlDataAdapter(mycmd);
            clsGlobal.adpClient.Fill(clsGlobal.mySet, "Client");

            mycmd = new SqlCommand("SELECT * FROM House", clsGlobal.myCon);
            clsGlobal.adpHouse = new SqlDataAdapter(mycmd);
            clsGlobal.adpHouse.Fill(clsGlobal.mySet, "House");

        }

        private void btnSignIn_Click(object sender, EventArgs e)
        {

            tabAdmin = clsGlobal.mySet.Tables["Admin"];
            tabAgent = clsGlobal.mySet.Tables["Agent"];
            tabClient = clsGlobal.mySet.Tables["Client"];

            if (comboBox1.SelectedItem.ToString() == "admin")
            {
                var admin = from a in tabAdmin.AsEnumerable()
                             where a.Field<string>("Username") == txtUsername.Text.ToString() && a.Field<string>("Password") == txtPassword.Text.ToString()
                             select a;

                if (admin.Count() > 0)
                {
                    clsGlobal.login = "admin";
                    this.Hide();
                    frmMain fa = new frmMain();
                    fa.Show();

                }
                else
                {
                    MessageBox.Show("Wrong Username or Password");
                }

            }
            else if (comboBox1.SelectedItem.ToString() == "agent")
            {
                var agents = from a in tabAgent.AsEnumerable()
                             where a.Field<string>("Username") == txtUsername.Text.ToString() && a.Field<string>("Password") == txtPassword.Text.ToString()
                             select a;

                if (agents.Count() > 0)
                {
                    clsGlobal.login = "agent";
                    this.Hide();
                    frmMain fa = new frmMain();
                    fa.Show();
                }
                else
                {
                    MessageBox.Show("Wrong Username or Password");
                }

            }
            else if (comboBox1.SelectedItem.ToString() == "client")
            {
                clsGlobal.login = "client";
                this.Hide();
                frmMain fa = new frmMain();
                fa.Show();
            }
            else
            {
                MessageBox.Show("Plese select Item in the combo box");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem.ToString() == "client")
            {
                txtUsername.Enabled = false;
                txtPassword.Enabled = false;
            }
            else {
                txtUsername.Enabled = true;
                txtPassword.Enabled = true;
            }
        }

        private void txtEmail_TextChanged(object sender, EventArgs e)
        {

        }

        private void grpboxLogin_Enter(object sender, EventArgs e)
        {

        }
    }
}

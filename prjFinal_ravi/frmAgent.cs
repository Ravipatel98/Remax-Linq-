using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;

namespace prjFinal_ravi
{
    public partial class frmAgent : Form
    {
        public frmAgent()
        {
            InitializeComponent();
        }
        DataTable tabAdmin, tabAgent, tabClient;
        int currentIndex;
        string mode;

        private void btnModify_Click(object sender, EventArgs e)
        {
            mode = "edit";
            txtName.Focus();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            DataRow currentRow = (mode == "add") ? tabAgent.NewRow() : tabAgent.Rows[currentIndex];
            currentRow["Name"] = txtName.Text;
            currentRow["Username"] = txtUserName.Text;
            currentRow["Password"] = txtPassword.Text;
            currentRow["Phone"] = txtPhone.Text;
            currentRow["Email"] = txtEmail.Text;

            if (mode == "add") { tabAgent.Rows.Add(currentRow); }

            SqlCommandBuilder myBuilder = new SqlCommandBuilder(clsGlobal.adpAgent);
            clsGlobal.adpAgent.Update(clsGlobal.mySet, "Agent");
            clsGlobal.mySet.Tables.Remove("Agent");
            clsGlobal.adpAgent.Fill(clsGlobal.mySet, "Agent");

            tabAgent = clsGlobal.mySet.Tables["Agent"];

            if (mode == "add") { currentIndex = tabAgent.Rows.Count - 1; }

            mode = "";
            Display();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {


            string mes = "Are you sure to delete this Agent ?";
            string title = "Agent Deletion Warning";
            if (MessageBox.Show(mes, title, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                tabAgent.Rows[currentIndex].Delete();
                SqlCommandBuilder myBuilder = new SqlCommandBuilder(clsGlobal.adpAgent);
                clsGlobal.adpAgent.Update(clsGlobal.mySet, "Agent");
                clsGlobal.mySet.Tables.Remove("Agent");
                clsGlobal.adpAgent.Fill(clsGlobal.mySet, "Agent");

                tabAgent = clsGlobal.mySet.Tables["Agent"];

                currentIndex = 0;
                Display();
            }
        }

        private void frmAgent_Load(object sender, EventArgs e)
        {
            tabAgent = clsGlobal.mySet.Tables["Agent"];

            dataAgents.DataSource = tabAgent.AsDataView();
            currentIndex = 0;
            Display();
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            currentIndex = 0;
            Display();
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            if (currentIndex > 0)
            {
                currentIndex--;
                Display();
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (currentIndex < (tabAgent.Rows.Count - 1))
            {
                currentIndex++;
                Display();
            }
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            currentIndex = (tabAgent.Rows.Count - 1);
            Display();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {

        }

        private void btnAdd_Click_1(object sender, EventArgs e)
        {
            mode = "add";
            txtName.Text = txtEmail.Text = txtPassword.Text = txtUserName.Text = txtPhone.Text = "";
            //date.ResetText();
            txtName.Focus();
        }
        

        private void Display()
        {
            txtName.Text = tabAgent.Rows[currentIndex]["Name"].ToString();
            txtEmail.Text = tabAgent.Rows[currentIndex]["Email"].ToString();
            txtPhone.Text = tabAgent.Rows[currentIndex]["Phone"].ToString();
            txtUserName.Text = tabAgent.Rows[currentIndex]["Username"].ToString();
            txtPassword.Text = tabAgent.Rows[currentIndex]["Password"].ToString();

            dataAgents.DataSource = tabAgent.AsDataView();
        }



    }
}

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
    public partial class frmClient : Form
    {
        public frmClient()
        {
            InitializeComponent();
        }
        DataTable tabAdmin, tabAgent, tabClient;
        int currentIndex;


        private void btnModify_Click(object sender, EventArgs e)
        {

            mode = "edit";
            txtName.Focus();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            DataRow currentRow = (mode == "add") ? tabClient.NewRow() : tabClient.Rows[currentIndex];
            //currentRow["Id"] = 1;
            currentRow["Name"] = txtName.Text;
            currentRow["Email"] = txtEmail.Text;
            currentRow["Type"] = cboType.SelectedItem.ToString();
            foreach (DataRow myrow in tabAgent.Rows)
            {
                if (myrow["Name"].ToString() == cboAgent.SelectedItem.ToString())
                {
                    currentRow["AId"] = Convert.ToInt32(myrow["Id"]);
                }
            }

            if (mode == "add") { tabClient.Rows.Add(currentRow); }

            SqlCommandBuilder myBuilder = new SqlCommandBuilder(clsGlobal.adpClient);
            clsGlobal.adpClient.Update(clsGlobal.mySet, "Client");
            clsGlobal.mySet.Tables.Remove("Client");
            clsGlobal.adpClient.Fill(clsGlobal.mySet, "Client");

            tabClient = clsGlobal.mySet.Tables["Client"];

            if (mode == "add") { currentIndex = tabClient.Rows.Count - 1; }

            mode = "";
            Display();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {

            string mes = "Are you sure to delete this Client ?";
            string title = "Client Deletion Warning";
            if (MessageBox.Show(mes, title, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                tabClient.Rows[currentIndex].Delete();
                SqlCommandBuilder myBuilder = new SqlCommandBuilder(clsGlobal.adpClient);
                clsGlobal.adpClient.Update(clsGlobal.mySet, "Client");
                clsGlobal.mySet.Tables.Remove("Client");
                clsGlobal.adpClient.Fill(clsGlobal.mySet, "Client");

                tabClient = clsGlobal.mySet.Tables["Client"];

                currentIndex = 0;
                Display();
            }
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
            if (currentIndex < (tabClient.Rows.Count - 1))
            {
                currentIndex++;
                Display();
            }
        }

        private void btnLast_Click(object sender, EventArgs e)
        {

            currentIndex = (tabClient.Rows.Count - 1);
            Display();
        }

        private void frmClient_Load(object sender, EventArgs e)
        {
            tabClient = clsGlobal.mySet.Tables["Client"];
            tabAgent = clsGlobal.mySet.Tables["Agent"];

            foreach (DataRow myRow in tabAgent.Rows)
            {
                cboAgent.Items.Add(myRow["Name"]);
            }

            dataClient.DataSource = tabClient.AsDataView();
            currentIndex = 0;
            Display();
        }

        string mode;
        private void btnAdd_Click(object sender, EventArgs e)
        {
            mode = "add";
            txtName.Text = txtEmail.Text ="";
            cboAgent.Text = cboType.Text = "";
            txtName.Focus(); 
        }
        private void Display()
        {
            txtName.Text = tabClient.Rows[currentIndex]["Name"].ToString();
            txtEmail.Text = tabClient.Rows[currentIndex]["Email"].ToString();
            cboType.Text = tabClient.Rows[currentIndex]["Type"].ToString();

            foreach (DataRow myRow in tabAgent.Rows)
            {
                if (myRow["Id"] == tabClient.Rows[currentIndex]["AId"])
                {
                    cboAgent.Text = myRow["Name"].ToString();
                }
            }

            dataClient.DataSource = tabClient.AsDataView();
        }
    }
}

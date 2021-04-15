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
    public partial class frmHouse : Form
    {
        public frmHouse()
        {
            InitializeComponent();
        }
        DataTable tabHouse, tabAgent, tabClient;
        int currentIndex;

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
            if (currentIndex < (tabHouse.Rows.Count - 1))
            {
                currentIndex++;
                Display();
            }
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            currentIndex = (tabHouse.Rows.Count - 1);
            Display();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            mode = "add";
            txtType.Text = txtLocation.Text = txtPrice.Text = txtadd.Text =txtCity.Text =  "";
            cboClient.Text = "";
            txtType.Focus();
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            mode = "edit";
            txtType.Focus();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            DataRow currentRow = (mode == "add") ? tabHouse.NewRow() : tabHouse.Rows[currentIndex];
            currentRow["Type"] = txtType.Text;
            currentRow["Location"] = txtLocation.Text;
            currentRow["Price"] = Convert.ToInt32(txtPrice.Text);
            currentRow["Address"] = txtadd.Text;
            currentRow["City"] = txtCity.Text;
            
            foreach (DataRow myrow in tabClient.Rows)
            {
                if (myrow["Name"].ToString() == cboClient.SelectedItem.ToString())
                {
                    currentRow["CId"] = Convert.ToInt32(myrow["Id"]);
                }
            }
            if (mode == "add") { tabHouse.Rows.Add(currentRow); }

            SqlCommandBuilder myBuilder = new SqlCommandBuilder(clsGlobal.adpHouse);
            clsGlobal.adpHouse.Update(clsGlobal.mySet, "House");
            clsGlobal.mySet.Tables.Remove("House");
            clsGlobal.adpHouse.Fill(clsGlobal.mySet, "House");

            tabHouse = clsGlobal.mySet.Tables["House"];

            if (mode == "add") { currentIndex = tabHouse.Rows.Count - 1; }

            mode = "";
            Display();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string mes = "Are you sure to delete this House ?";
            string title = "House Deletion Warning";
            if (MessageBox.Show(mes, title, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                tabHouse.Rows[currentIndex].Delete();
                SqlCommandBuilder myBuilder = new SqlCommandBuilder(clsGlobal.adpHouse);
                clsGlobal.adpHouse.Update(clsGlobal.mySet, "House");
                clsGlobal.mySet.Tables.Remove("House");
                clsGlobal.adpHouse.Fill(clsGlobal.mySet, "House");

                tabHouse = clsGlobal.mySet.Tables["House"];
                currentIndex = 0;
                Display();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {

        }

        string mode;

        private void frmHouse_Load(object sender, EventArgs e)
        {
            tabClient = clsGlobal.mySet.Tables["Client"];
            tabAgent = clsGlobal.mySet.Tables["Agent"];
            tabHouse = clsGlobal.mySet.Tables["House"];

            foreach (DataRow myRow in tabClient.Rows)
            {
                cboClient.Items.Add(myRow["Name"]);
            }

            dataHouse.DataSource = tabHouse.AsDataView();
            currentIndex = 0;
            Display();
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            currentIndex = 0;
            Display();
        }


        private void Display()
        {
            txtType.Text = tabHouse.Rows[currentIndex]["Type"].ToString();
            txtLocation.Text = tabHouse.Rows[currentIndex]["Location"].ToString();
            txtPrice.Text = tabHouse.Rows[currentIndex]["Price"].ToString();
            txtadd.Text = tabHouse.Rows[currentIndex]["Address"].ToString();
            txtCity.Text = tabHouse.Rows[currentIndex]["City"].ToString();
            
            foreach (DataRow myRow in tabClient.Rows)
            {
                if (myRow["Id"] == tabHouse.Rows[currentIndex]["CId"])
                {
                    cboClient.Text = myRow["Name"].ToString();
                }
            }

            dataHouse.DataSource = tabHouse.AsDataView();
        }
    }
}

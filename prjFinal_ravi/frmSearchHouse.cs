using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace prjFinal_ravi
{
    public partial class frmSearchHouse : Form
    {
        public frmSearchHouse()
        {
            InitializeComponent();
        }
        DataTable tabHouse, tabClient, tabAgent, tabAdmin;

        private void btnSearch_Click(object sender, EventArgs e)
        {
            var house = from h in tabHouse.AsEnumerable()
                        where h.Field<int>("CId") == Convert.ToInt32(cboClient.SelectedValue)
                        select h;

            if (house.Count() > 0)
            {
                GridHouse.DataSource = house.CopyToDataTable();
            }
            else {
                MessageBox.Show("NO data to show");
            }
        }

        private void frmSearchHouse_Load(object sender, EventArgs e)
        {
            tabHouse = clsGlobal.mySet.Tables["House"];
            tabClient = clsGlobal.mySet.Tables["Client"];
            tabAgent = clsGlobal.mySet.Tables["Agent"];

            var clients = from client in tabClient.AsEnumerable()
                          select new
                          {
                              name = client.Field<string>("Name"),
                              ID = client.Field<int>("Id")
                          };

            cboClient.DataSource = clients.ToList();
            cboClient.DisplayMember = "name";
            cboClient.ValueMember = "ID";

            GridHouse.DataSource = tabHouse.AsDataView();
        }
    }
}

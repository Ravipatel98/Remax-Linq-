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
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void agentsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmAgent fa = new frmAgent();

            fa.Show();
        }

        private void clientsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmClient fc = new frmClient();
            fc.Show();
        }

        private void housesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmHouse fh = new frmHouse();
            fh.Show();
        }

        private void logOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clsGlobal.login = "";
            this.Hide();
            frmLogin fl = new frmLogin();
            fl.Show();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {

            if (clsGlobal.login == "client")
            {
                manageToolStripMenuItem.Visible = false;
            }
            else if (clsGlobal.login == "agent")
            {
                administratorsToolStripMenuItem.Visible = false;
                agentsToolStripMenuItem1.Visible = false;
            }
        }

        private void manageToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void searchhouseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmSearchHouse fs = new frmSearchHouse();
            fs.Show();
        }

        private void searchagentsToolStripMenuItem_Click(object sender, EventArgs e)
        {

            //frmSearchAgent fs = new frmSearchAgent();
            //fs.Show();
        }
    }
}

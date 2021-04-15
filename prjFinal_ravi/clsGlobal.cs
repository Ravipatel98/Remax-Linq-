using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace prjFinal_ravi
{
    public static class clsGlobal
    {
        public static SqlConnection myCon;
        public static SqlDataAdapter adpAdmin, adpAgent, adpClient, adpHouse;
        public static DataSet mySet;
        public static string login = "";
    }
}

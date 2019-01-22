using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySql.Data.MySqlClient;

namespace Bestival.BusinessLayer
{
    class Login
    {
        public DataTable LogIn(string ID, string PWD)
        {
            DataAccessLayer.DataAccessLayer DAL = new DataAccessLayer.DataAccessLayer();
            MySqlParameter[] parameter = new MySqlParameter[2];

            parameter[0] = new MySqlParameter("@employee_id", MySqlDbType.Int24, 11);
            parameter[0].Value = ID;
            parameter[1] = new MySqlParameter("@emp_password", MySqlDbType.VarChar, 15);
            parameter[1].Value = PWD;

            DAL.OpenConnection();
            DataTable dt = new DataTable();
            dt = DAL.SelectData("SP_LOGIN", parameter);
            DAL.CloseConnection();
            return dt;
        }
    }
}

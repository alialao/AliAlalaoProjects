using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySql.Data.MySqlClient;

namespace Bestival.BusinessLayer
{
    class Customer
    {
        DataAccessLayer.DataAccessLayer DAL = new DataAccessLayer.DataAccessLayer();
        public DataTable Customers_All()
        {
            DataTable dt = new DataTable();
            dt = DAL.SelectData("Customers_All", null);
            return dt;
        }
        public DataTable Customers_Search(int customer_id)
        {
            DataTable dt = new DataTable();
            MySqlParameter[] parameter = new MySqlParameter[1];
            parameter[0] = new MySqlParameter("@p_customer_id", MySqlDbType.Int32, 11);
            parameter[0].Value = customer_id;
            dt = DAL.SelectData("Customers_Search", parameter);
            return dt;
        }
        public void Customer_Delete(int customer_id)
        {
            MySqlParameter[] parameter = new MySqlParameter[1];
            parameter[0] = new MySqlParameter("@P_customer_id", MySqlDbType.Int32, 11);
            parameter[0].Value = customer_id;
            DAL.ExecuteCommand("Customer_Delete", parameter);
        }
        public DataTable Customer_Edit(int customer_id, string first_name, string last_name, DateTime dof, string email_address, string phone_number, string Camping_camping_category, string Camping_camping_number, string notes)
        {
            DataTable dt = new DataTable();

            MySqlParameter[] parameter = new MySqlParameter[9];
            parameter[0] = new MySqlParameter("@P_customer_id", MySqlDbType.Int32);
            parameter[0].Value = customer_id;
            parameter[1] = new MySqlParameter("@P_first_name", MySqlDbType.VarChar);
            parameter[1].Value = first_name;
            parameter[2] = new MySqlParameter("@P_last_name", MySqlDbType.VarChar);
            parameter[2].Value = last_name;
            parameter[3] = new MySqlParameter("@P_dof", MySqlDbType.Date);
            parameter[3].Value = dof;
            parameter[4] = new MySqlParameter("@P_email_address", MySqlDbType.VarChar);
            parameter[4].Value = email_address;
            parameter[5] = new MySqlParameter("@P_phone_number", MySqlDbType.VarChar);
            parameter[5].Value = phone_number;
            parameter[6] = new MySqlParameter("@P_Camping_camping_category", MySqlDbType.VarChar);
            parameter[6].Value = Camping_camping_category;
            parameter[7] = new MySqlParameter("@P_Camping_camping_number", MySqlDbType.VarChar);
            parameter[7].Value = Camping_camping_number;
            parameter[8] = new MySqlParameter("@P_notes", MySqlDbType.Text);
            parameter[8].Value = notes;

            dt = DAL.SelectData("Customer_Edit", parameter);
            return dt;
        }
    }
}

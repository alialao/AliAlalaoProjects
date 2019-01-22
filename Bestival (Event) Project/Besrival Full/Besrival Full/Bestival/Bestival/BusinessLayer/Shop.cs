using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Windows.Forms;

namespace Bestival.BusinessLayer
{
    class Shop
    {
        DataAccessLayer.DataAccessLayer DAL = new DataAccessLayer.DataAccessLayer();

        public DataTable Shops_All(int Shop_Type)
        {
            DataTable dt = new DataTable();
            MySqlParameter[] parameter = new MySqlParameter[1];
            parameter[0] = new MySqlParameter("@P_Shop_Type", MySqlDbType.Int32, 4);
            parameter[0].Value = Shop_Type;
            dt = DAL.SelectData("Shops_All", parameter);
            return dt;
        }
        public void Shop_Add(string shop_name, string location, int shop_type)
        {
            DAL.OpenConnection();
            MySqlParameter[] parameter = new MySqlParameter[3];
            //parameter[0] = new MySqlParameter("@P_SHOP_NAME", MySqlDbType.Int32);
            //parameter[0].Value = shop_id;
            parameter[0] = new MySqlParameter("@P_SHOP_NAME", MySqlDbType.VarChar);
            parameter[0].Value = shop_name;
            parameter[1] = new MySqlParameter("@P_LOCATION", MySqlDbType.VarChar);
            parameter[1].Value = location;
            parameter[2] = new MySqlParameter("@P_SHOP_TYPE", MySqlDbType.Int32);
            parameter[2].Value = shop_type;

            DAL.ExecuteCommand("Shop_Add", parameter);
            DAL.CloseConnection();
        }
        public DataTable Shops_Search(int shop_Id, int Shop_Type)
        {
            DataTable dt = new DataTable();
            MySqlParameter[] parameter = new MySqlParameter[2];
            parameter[0] = new MySqlParameter("@P_SHOP_ID", MySqlDbType.Int32, 11);
            parameter[0].Value = shop_Id;
            parameter[1] = new MySqlParameter("@P_Shop_Type", MySqlDbType.Int32, 4);
            parameter[1].Value = Shop_Type;

            dt = DAL.SelectData("Shops_Search", parameter);
            return dt;
        }
        public void Shop_Delete(int shop_Id, int Shop_Type)
        {
            MySqlParameter[] parameter = new MySqlParameter[2];
            parameter[0] = new MySqlParameter("@p_SHOP_ID", MySqlDbType.Int32, 11);
            parameter[0].Value = shop_Id;
            parameter[1] = new MySqlParameter("@P_Shop_Type", MySqlDbType.Int32, 4);
            parameter[1].Value = Shop_Type;

            DAL.ExecuteCommand("Shop_Delete", parameter);
        }
        public DataTable Shop_Edit(int shop_Id, string shop_name, string location, int shop_type)
        {
            DataTable dt = new DataTable();

            MySqlParameter[] parameter = new MySqlParameter[4];
            parameter[0] = new MySqlParameter("@p_SHOP_ID", MySqlDbType.Int32, 11);
            parameter[0].Value = shop_Id;
            parameter[1] = new MySqlParameter("@P_SHOP_NAME", MySqlDbType.VarChar, 45);
            parameter[1].Value = shop_name;
            parameter[2] = new MySqlParameter("@P_LOCATION", MySqlDbType.VarChar, 24);
            parameter[2].Value = location;
            parameter[3] = new MySqlParameter("@P_SHOP_TYPE", MySqlDbType.Int32, 4);
            parameter[3].Value = shop_type;

            dt = DAL.SelectData("Shop_Edit", parameter);
            return dt;
        }
        public int GetShopType(int Shop_ID)
        {
            string ConnStr = ConfigurationManager.ConnectionStrings["ConnSet"].ConnectionString;
            string cmdstr = @"Get_Shop_Type";

            try
            {
                using (MySqlConnection conn = new MySqlConnection(ConnStr))
                using (MySqlCommand cmd = new MySqlCommand(cmdstr, conn))
                {
                    conn.Open();

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = cmdstr;

                    cmd.Parameters.Clear();

                    cmd.Parameters.Add("@P_Shop_ID", MySqlDbType.Int32).Value = Shop_ID;
                    cmd.Parameters.Add("@ShopType", MySqlDbType.Int32).Direction = ParameterDirection.ReturnValue;
                    cmd.ExecuteNonQuery();

                    return Convert.ToInt32(cmd.Parameters["@ShopType"].Value);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return -1;
            }
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySql.Data.MySqlClient;


namespace Bestival.BusinessLayer
{
    class Product
    {
        DataAccessLayer.DataAccessLayer DAL = new DataAccessLayer.DataAccessLayer();
        public DataTable Product_All()
        {
            DataTable dt = new DataTable();
            dt = DAL.SelectData("Product_All", null);
            return dt;
        }
        public DataTable Product_Searsh(int product_id)
        {
            DataTable dt = new DataTable();
            MySqlParameter[] parameter = new MySqlParameter[1];
            parameter[0] = new MySqlParameter("@p_product_id", MySqlDbType.Int32);
            parameter[0].Value = product_id;
            dt = DAL.SelectData("Product_Searsh", parameter);
            return dt;
        }
        public void DeletProduct(int product_id, int shop_id)
        {
            MySqlParameter[] parameter = new MySqlParameter[2];
            parameter[0] = new MySqlParameter("@P_PRODUCT_ID", MySqlDbType.Int32);
            parameter[0].Value = product_id;
            parameter[1] = new MySqlParameter("@P_SHOP_ID", MySqlDbType.Int32);
            parameter[1].Value = shop_id;

            DAL.ExecuteCommand("Product_Delete", parameter);
        }

        public DataTable Product_Edit(int product_id, string product_name, double product_price, int quantity, int shop_id)
        {
            DataTable dt = new DataTable();

            MySqlParameter[] parameter = new MySqlParameter[5];
            parameter[0] = new MySqlParameter("@P_PRODUCT_ID", MySqlDbType.Int32);
            parameter[0].Value = product_id;
            parameter[1] = new MySqlParameter("@P_Product_name", MySqlDbType.VarChar);
            parameter[1].Value = product_name;
            parameter[2] = new MySqlParameter("@P_product_price", MySqlDbType.Double);
            parameter[2].Value = product_price;
            parameter[3] = new MySqlParameter("@P_QUANTITY", MySqlDbType.Int32);
            parameter[3].Value = quantity;
            parameter[4] = new MySqlParameter("@P_SHOP_ID", MySqlDbType.Int32);
            parameter[4].Value = shop_id;
            
            dt = DAL.SelectData("Product_Edit", parameter);
            return dt;
        }

        public void Product_Add(string product_name, double product_price, int product_type, int shop_id, int quantity)
        {
            MySqlParameter[] parameter = new MySqlParameter[5];
            parameter[0] = new MySqlParameter("@p_product_name", MySqlDbType.String);
            parameter[0].Value = product_name;

            parameter[1] = new MySqlParameter("@p_product_price", MySqlDbType.Double);
            parameter[1].Value = product_price;

            parameter[2] = new MySqlParameter("@p_product_type", MySqlDbType.Int32);
            parameter[2].Value = product_type;

            parameter[3] = new MySqlParameter("@p_SHOP_ID", MySqlDbType.Int32);
            parameter[3].Value = shop_id;

            parameter[4] = new MySqlParameter("@p_QUANTITY", MySqlDbType.Int32);
            parameter[4].Value = quantity;

            DAL.ExecuteCommand("Product_Add", parameter);
        }
    }
}

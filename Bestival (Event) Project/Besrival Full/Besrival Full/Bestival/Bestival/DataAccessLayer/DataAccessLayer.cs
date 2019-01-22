using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Configuration;
using MySql.Data.MySqlClient;


namespace Bestival.DataAccessLayer
{
    class DataAccessLayer
    {
        MySqlConnection Conn;
        string ConnStr;

        //Inisialize the connection
        public DataAccessLayer()
        {
            ConnStr = ConfigurationManager.ConnectionStrings["ConnSet"].ConnectionString;
        }
        //Open the connection
        public void OpenConnection()
        {
            Conn = new MySqlConnection(ConnStr);

            if (Conn.State != ConnectionState.Open)
            {
                Conn.Open();
            }
        }
        //Close the connection
        public void CloseConnection()
        {
            if (Conn.State == ConnectionState.Open)
            {
                Conn.Close();
            }
        }
        //Read data from database,
        public DataTable SelectData(string stored_Procedure, MySqlParameter[] parameter)
        {
            OpenConnection();
            MySqlCommand mysqlCmd = new MySqlCommand();
            mysqlCmd.CommandType = CommandType.StoredProcedure;
            mysqlCmd.CommandText = stored_Procedure;
            mysqlCmd.Connection = Conn;
            if (parameter != null)
            {
                for (int i = 0; i < parameter.Length; i++)
                {
                    mysqlCmd.Parameters.Add(parameter[i]);
                }
                // mysqlCmd.Parameters.AddRange(parameter);
                // The both do the same thing
            }
            MySqlDataAdapter da = new MySqlDataAdapter(mysqlCmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            CloseConnection();
            return dt;
        }
        public DataTable GetDataTable(string QurStr)
        {
            DataTable dt = new DataTable();
            using (MySqlCommand cmd = new MySqlCommand(QurStr, Conn))
            using (MySqlDataReader dr = cmd.ExecuteReader())
            {
                dt.Load(dr);
            }
            return dt;
        }

        //Insert, Update and Delete data from database
        public void ExecuteCommand(string stored_Procedure, MySqlParameter[] parameter)
        {
            OpenConnection();
            MySqlCommand mysqlCmd = new MySqlCommand();
            mysqlCmd.CommandType = CommandType.StoredProcedure;
            mysqlCmd.CommandText = stored_Procedure;
            mysqlCmd.Connection = Conn;

            if (parameter != null)
            {
                mysqlCmd.Parameters.AddRange(parameter);
            }
            mysqlCmd.ExecuteNonQuery();
            CloseConnection();
        }
    }
}

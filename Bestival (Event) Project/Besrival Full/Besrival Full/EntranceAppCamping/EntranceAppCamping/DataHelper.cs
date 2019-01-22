using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;

namespace EntranceAppCamping
{
    public class DataHelper
    {
        public MySqlConnection connection;

        public DataHelper()
        {
            String connectionInfo = "server=studmysql01.fhict.local;" +//the iris-server
                                    "database=dbi307047;" +
                                    "user id=dbi307047;" +
                                    "password=123456789;" +
                                    "connect timeout=30;";

            connection = new MySqlConnection(connectionInfo);
        }

        public List<Customers> GetCustomers(string reservationnumber )
        {
            String sql = "SELECT first_name, last_name, dof FROM customers WHERE reservation_number='" + reservationnumber + "'";
            MySqlCommand cmd = new MySqlCommand(sql, connection);
            List<Customers> cust;
            cust = new List<Customers>();
            
            try
            {
                connection.Open();
                MySqlDataReader reader = cmd.ExecuteReader();
                string fname, lname;
                DateTime custDof;
                if (!reader.HasRows)
                {
                    MessageBox.Show("Invalid Reservation Number!");
                }
                else
                {
                    MessageBox.Show("Success!");
                }

                while (reader.Read())
                {
                    fname = Convert.ToString(reader["first_name"]);
                    lname = Convert.ToString(reader["last_name"]);
                    custDof = Convert.ToDateTime(reader["dof"]);

                    cust.Add(new Customers(fname, lname, custDof));
                }

            }
            catch
            {
                MessageBox.Show("error while loading the customers");
            }
            finally
            {
                connection.Close();
            }
            return cust;
        }

        public void CheckStatus(string rsv)
        {
            String sql = "SELECT reservation_number FROM customers WHERE reservation_number='" + rsv + "'";
            MySqlCommand cmd = new MySqlCommand(sql, connection);

            try
            {
                connection.Open();
                MySqlDataReader reader = cmd.ExecuteReader();
                rsv = Convert.ToString(reader["reservation_number"]);
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
            finally
            {
                connection.Close();
            }

        }

        public void EmployeeLogIn(string id, string pass)
        {
            String sql = "SELECT employee_id, password FROM employees WHERE employee_id='" + id + "' and password='" + pass + "'";
            MySqlCommand cmd = new MySqlCommand(sql, connection);


            try
            {
                connection.Open();
                MySqlDataReader reader = cmd.ExecuteReader();
                
                if(reader.HasRows)
                {
                    connection.Close();
                    MessageBox.Show("You are logged in!");
                    Login myLogin = new Login();
                    myLogin.Hide();
                    Form1 myForm = new Form1();
                    myForm.Show();
                }
                else
                {
                    MessageBox.Show("Invalid ID or Password!");
                }
               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                
            }
            finally
            {
                connection.Close();
            }
        }

      
    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace WebServiceTrygon
{
    /// <summary>
    /// Summary description for OperationService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class OperationService : System.Web.Services.WebService
    {
        public string connectionstring = "Server=DESKTOP-7MM0PI2;Database=Operation;Trusted_Connection=True;";
        public string query = "INSERT INTO dbo.tbl_operation (op_type,op_value,op_val1,op_val2,created_date) VALUES (@op_type,@op_value, @op_val1,@op_val2,@created_date)";
        //[WebMethod]
        //public string HelloWorld()
        //{
        //    return "Hello World";
        //}

        [WebMethod]
        public int Add(int a, int b)
        {
            //using (SqlConnection conn = new SqlConnection(@"Server=DESKTOP-7MM0PI2;Database=Operation;Trusted_Connection=True;"))
            using (SqlConnection conn = new SqlConnection(connectionstring))
            {
                //SqlCommand cmd = new SqlCommand("INSERT INTO dbo.tbl_operation (op_type,op_value,op_val1,op_val2,created_date) VALUES (@op_type,@op_value, @op_val1,@op_val2,@created_date)", conn);
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add("@op_type", SqlDbType.NVarChar, 50).Value = "Add";
                cmd.Parameters.Add("@op_value", SqlDbType.Int, 5).Value = a+b;
                cmd.Parameters.Add("@op_val1", SqlDbType.Int, 5).Value = a;
                cmd.Parameters.Add("@op_val2", SqlDbType.Int, 5).Value = b;
                cmd.Parameters.Add("@created_date", SqlDbType.DateTime).Value = Convert.ToDateTime(DateTime.Now);
                conn.Open();
                int ret =cmd.ExecuteNonQuery();
                conn.Close();                
            }
                return a + b;
        }

        [WebMethod]
        public int Subtract(int a, int b)
        {
            using (SqlConnection conn = new SqlConnection(connectionstring))
            {                
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add("@op_type", SqlDbType.NVarChar, 50).Value = "Subtract";
                cmd.Parameters.Add("@op_value", SqlDbType.Int, 5).Value = a - b;
                cmd.Parameters.Add("@op_val1", SqlDbType.Int, 5).Value = a;
                cmd.Parameters.Add("@op_val2", SqlDbType.Int, 5).Value = b;
                cmd.Parameters.Add("@created_date", SqlDbType.DateTime).Value = Convert.ToDateTime(DateTime.Now);
                conn.Open();
                int ret = cmd.ExecuteNonQuery();
                conn.Close();
            }
            return a - b;
        }

        [WebMethod]
        public int Multiply(int a, int b)
        {
            using (SqlConnection conn = new SqlConnection(connectionstring))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add("@op_type", SqlDbType.NVarChar, 50).Value = "Multiply";
                cmd.Parameters.Add("@op_value", SqlDbType.Int, 5).Value = a * b;
                cmd.Parameters.Add("@op_val1", SqlDbType.Int, 5).Value = a;
                cmd.Parameters.Add("@op_val2", SqlDbType.Int, 5).Value = b;
                cmd.Parameters.Add("@created_date", SqlDbType.DateTime).Value = Convert.ToDateTime(DateTime.Now);
                conn.Open();
                int ret = cmd.ExecuteNonQuery();
                conn.Close();
            }
            return a * b;
        }

        [WebMethod]
        public int Divide(int a, int b)
        {
            using (SqlConnection conn = new SqlConnection(connectionstring))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add("@op_type", SqlDbType.NVarChar, 50).Value = "Divide";
                cmd.Parameters.Add("@op_value", SqlDbType.Int, 5).Value = a / b;
                cmd.Parameters.Add("@op_val1", SqlDbType.Int, 5).Value = a;
                cmd.Parameters.Add("@op_val2", SqlDbType.Int, 5).Value = b;
                cmd.Parameters.Add("@created_date", SqlDbType.DateTime).Value = Convert.ToDateTime(DateTime.Now);
                conn.Open();
                int ret = cmd.ExecuteNonQuery();
                conn.Close();
            }
            return a / b;
        }

        [WebMethod]        
        public string OperationHistory()
        {
            DataTable dt = new DataTable("dt");
            using (SqlConnection conn = new SqlConnection(connectionstring))
            {
                SqlCommand cmd = new SqlCommand("select top(10) * from tbl_operation order by created_date desc;", conn);
                cmd.CommandType = CommandType.Text;
                
                conn.Open();
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                
                adp.Fill(dt);
                conn.Close();
            }
            //return dt;

            MemoryStream str = new MemoryStream();
            dt.WriteXml(str, true);
            str.Seek(0, SeekOrigin.Begin);
            StreamReader sr = new StreamReader(str);
            string xmlstr;
            xmlstr = sr.ReadToEnd();
            return (xmlstr);
        }

    }
}

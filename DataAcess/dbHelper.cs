using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;

namespace DataAcess
{
    public static class dbHelper
    {
        static string _conectionstring = "Server=tcp:sj-software-solutions-sol.database.windows.net,1433;Initial Catalog=Shutter;Persist Security Info=False;User ID=ShutterWebService;Password=82DD70EBAb982;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

        //static string _conectionstring = "";

      
        private static SqlConnection dbConnection()
        {            
            return new SqlConnection(_conectionstring);
        }

        public static int ExecuteScaler(string procName, DynamicParameters p)
        {
            
                var conn = DataAcess.dbHelper.dbConnection();



                conn.Open();
                var id = conn.ExecuteScalar<int>(procName, p, commandType: CommandType.StoredProcedure);
                conn.Close();

                return id;


        }

        public static int LogError(string Error, string FileName)
        {

            var conn = DataAcess.dbHelper.dbConnection();
            DynamicParameters p = new DynamicParameters();
            p.Add("@Error", Error);
            p.Add("@FileName", FileName);

            conn.Open();
            var id = conn.Execute("ExcelImportError_Log", p, commandType: CommandType.StoredProcedure);
            conn.Close();

            return id;


        }








    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using DataAcess;
using Microsoft.Data.SqlClient;


namespace Domain
{
    public  class User
    {

        public int UserID { get; set; }

        public string UserName{ get; set; }


        public User(string userName)
        {            
            UserName = userName;
            SetUserID();
            if (UserID == 0) throw new Exception("Unknown User");
        }

        private void SetUserID()
        {
            DynamicParameters dynamicParameters = new DynamicParameters();
            dynamicParameters.Add("@UserName", UserName);            
            UserID = dbHelper.ExecuteScaler("User_GetByUserName", dynamicParameters);

        }

    }
}

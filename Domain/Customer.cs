using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using DataAcess;
using Microsoft.Data.SqlClient;



namespace Domain
{
    public  class Customer
    {
        public int CustomerID { get; set; }
        public string Surname { get; set; }

        public int UserID { get; set; }

        public Order  Order { get; set; }

        public void CreateCustomer () 
        { 
          DynamicParameters dynamicParameters = new DynamicParameters ();
          dynamicParameters.Add("Surname", Surname);
          dynamicParameters.Add("UserID", UserID);

          CustomerID = dbHelper.ExecuteScaler("Customer_Create", dynamicParameters); 

        }

        public bool Validate()
        {
            if(UserID == 0) return false;
            if (Surname.Length == 0) return  false;

            return true;
        }
        

    }
}

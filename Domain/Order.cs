using Dapper;
using DataAcess;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public  class Order
    {
        public int CustomerID { get; set; }
        public int UserID { get; set; }
        public int OrderID { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public List<OrderDetails> OrderDetails { get; set; } = new List<OrderDetails>();

        public Order(int customerID, int userID)
        {
            CustomerID = customerID;
            UserID = userID;            
        }

        public void CreateOrder() 
        {
            
            if (Validate())
            {
                DynamicParameters dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@CustomerID", CustomerID);
                dynamicParameters.Add("@UserID", UserID);
                dynamicParameters.Add("@SiteID", 1);
                dynamicParameters.Add("@CreatedDate", DateTime.Now);

                OrderID = dbHelper.ExecuteScaler("Order_Create", dynamicParameters);

                foreach (var item in OrderDetails)
                {
                    item.Save(OrderID);
                }

            }
                
        }

        public bool Validate()
        {
            if (CustomerID == 0) return false;
            if (UserID == 0) return false;

            return true;
        }

        public void UploadSpreadSheet()
        { 
            
        }

    }
}


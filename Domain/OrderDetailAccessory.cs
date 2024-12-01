using Dapper;
using DataAcess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public  class OrderDetailAccessory
    {

        public int OrderDetailAccessoryID { get; set; }
        public int OrderDetailsID { get; set; }
        public int ShutterAccessoryID { get; set; }
        public int Length { get; set; }
        public int QTY { get; set; }

        public int Colour { get; set; }

        public bool DoNotCreate = false;


        public void Save(int orderDetailsID)
        {
            try
            {
                OrderDetailsID = orderDetailsID;

                OrderDetailsID = orderDetailsID;
                DynamicParameters dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@OrderDetailsID", OrderDetailsID);
                dynamicParameters.Add("@ShutterAccessoryID", ShutterAccessoryID);
                dynamicParameters.Add("@Length", Length);
                dynamicParameters.Add("@QTY", QTY);
                OrderDetailAccessoryID = dbHelper.ExecuteScaler("OrderDetailsAccessories_Create", dynamicParameters);
                

            }
            catch 
            { 
                //Need to log Errors
            }
        
        }        
    }
}

using Dapper;
using DataAcess;
using Microsoft.Data.SqlClient;
using Syncfusion.XlsIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class OrderDetailsZones 
    {
        public int OrderDetailsZoneID { get; set; }
        public int Zone { get; set; } = 0;
        public int OrderDetailsID { get; set; }
        public int Width { get; set; } = 0;
        public int PanelConfigID { get; set; } = 1;
        public int DoorOpeningID { get; set; } = 0;

                

        public void Save(int orderDetailsID)
        {
            try
            {
                OrderDetailsID = orderDetailsID;
                DynamicParameters dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@OrderDetailsID", OrderDetailsID);
                dynamicParameters.Add("@Zone", Zone);
                dynamicParameters.Add("@Width", Width);
                dynamicParameters.Add("@PanelConfigID", PanelConfigID);
                dynamicParameters.Add("@DoorOpeningID", DoorOpeningID);
                OrderDetailsZoneID = dbHelper.ExecuteScaler("OrderDetailsZone_Save", dynamicParameters);                

            }
            catch (Exception ex)
            {
                //Log Error

            }
        }

    }
}

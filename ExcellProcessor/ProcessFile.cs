using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Syncfusion.XlsIO;
using static System.Net.Mime.MediaTypeNames;
using Domain;
using Azure;
using Microsoft.IdentityModel.Logging;
using System.Net.Mail;
using DataAcess;

namespace ExcellProcessor
{
    public  class ProcessFile
    {

        User User;
        Customer Customer;
        string cc = "Jon@Sj-Software-Solutions.com";


        public ProcessFile(string name, Stream stream)
        {

            try
            {            
                Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Mgo+DSMBMAY9C3t2VFhhQlJNfV5AQmBIYVp/TGpJfl96cVxMZVVBJAtUQF1hSn5Xd0NiWHtYdHZcRGhZ");

                var wb = CreateWorkBook(name, stream);
                IWorksheet ws = wb.Worksheets[2];
                //Need to Get UserName
                User = GetUser(ws);

                Customer = GetCustomer(ws, User);

                var ods = CreateOrderDetails(ws);

                if (ods.Count > 0) {
                    var ascs  = CreateAscs(ws);
                    ods[0].OrderDetailAccessories = ascs;
                }

                var o = SaveOrder(Customer, ods, User);
                SendCompletedEmail(stream, o, name);

            }
            catch (Exception ex)
            {
                SendFailureEmail(stream, name, ex.Message);

            }

        }

        private IWorkbook CreateWorkBook(string name, Stream stream)
        {
            try
            {

                ExcelEngine excelEngine = new ExcelEngine();
                IApplication application = excelEngine.Excel;

                return application.Workbooks.Open(stream, ExcelOpenType.Automatic);

            }
            catch
            {
                throw new Exception("Unable to open Import File");
            }

        }

        private User GetUser(IWorksheet ws)
        {
            try
            {                           
                
                var userName = ws.Range[Helper.cellUserName].Text;
                return new User(userName);
            }
            catch
            {
                throw new Exception("Unknown User");
            }
        }

        private Customer GetCustomer(IWorksheet ws, User u)
        {
            try 
            { 

                var c = new Customer();
                c.UserID = u.UserID;   
                c.Surname = ws.Range[Helper.cellCustomerName].FormulaStringValue;
                return c;

            }
            catch
            {

                var c  = new Customer();
                c.UserID = u.UserID;
                c.Surname = "Unknown";
                return c;

            }
        }

        private List<OrderDetails> CreateOrderDetails(IWorksheet ws)
        {
            try
            {
                var ods = new List<OrderDetails>();
                var startignRow = Helper.StartingRowOrderdetails;
                var maxRow = Helper.EndingRowOrderdetails;

                for (int i = startignRow; i <= maxRow; i++)
                {
                    var od = CreateOrderDetail(ws, i);
                    if (!od.DoNotCreate)
                        ods.Add(od);

                }
                return ods;
            }
            catch 
            {
                return new List<OrderDetails>();
            }

        }

        private OrderDetails CreateOrderDetail(IWorksheet ws, int row)
        {
            try
            {
                var od = new OrderDetails();
                od.Qty = 1;
                od.Description = GetStringValue(ws.Range[Helper.Room + row.ToString()]);

                od.ModelID = GetIntValue(ws.Range[Helper.Model + row.ToString()]);
                od.Height = GetIntValue(ws.Range[Helper.Height + row.ToString()]);
                od.Width = GetIntValue(ws.Range[Helper.Width + row.ToString()]);
                od.ColourID = GetIntValue(ws.Range[Helper.Colour + row.ToString()]);
                od.TiltMechanismID = 1;
                od.BlackOutBlindID = 0;
                od.HingeID = 8;
                od.LouvreSizeID = GetIntValue(ws.Range[Helper.LouvreSize + row.ToString()]);
                od.PanelConfigID = GetIntValue(ws.Range[Helper.PanelConfiguration + row.ToString()]);
                od.TierOnTierHeight = GetIntValue(ws.Range[Helper.TierOnTierHeight + row.ToString()]);
                od.FirstDividerPosition = GetIntValue(ws.Range[Helper.MidRailOne + row.ToString()]);
                od.SecondDividerPosition = GetIntValue(ws.Range[Helper.MidRailTwo + row.ToString()]);
                //od.SplitTiltPosition = GetIntValue(row[Helper.Spl]);
                od.SplitPositionOne = GetIntValue(ws.Range[Helper.LowerSplitPosition + row.ToString()]);
                od.SplitPositionTwo = GetIntValue(ws.Range[Helper.UpperSplitPosition + row.ToString()]);
                od.TPost1 = GetIntValue(ws.Range[Helper.TPost1 + row.ToString()]);
                od.TPost2 = GetIntValue(ws.Range[Helper.TPost2 + row.ToString()]);
                od.TPost3 = GetIntValue(ws.Range[Helper.TPost3 + row.ToString()]);
                od.FrameTypeID = GetIntValue(ws.Range[Helper.FrameType + row.ToString()]);
                od.FrameSideID = GetIntValue(ws.Range[Helper.FrameSides + row.ToString()]);
                od.HardwareID = 0;
                od.SpareLouvres = 0;
                od.HardwareHeight = 0;
                od.BlackOutBlindOperation = 0;
                od.DoorOpening = GetIntValue(ws.Range[Helper.DoorOpening + row.ToString()]);
                //od.NoOfTPosts = GetIntValue(row[Helper.Model]);
                od.GuidedTrackTypeID = 0;
                od.FrameKeyID = GetIntValue(ws.Range[Helper.FrameKey + row.ToString()]);

                if (od.ModelID == 0)
                    od.DoNotCreate = true;

                //Need to sort out Tposts
                int TPosts = 0;
                if (od.TPost3 > 0)
                    TPosts = 3;
                else if (od.TPost2 > 0)
                    TPosts = 2;
                else if (od.TPost3 > 0)
                    TPosts = 1;

                for (int i = 1; i <= TPosts; i++)
                {
                    var z = new OrderDetailsZones();
                    if (i == 1)
                        z.Width = (int)od.TPost1;
                    else if (i == 2)
                        z.Width = (int)od.TPost2;
                    else if(i == 3)
                        z.Width = (int)od.TPost3;
                    od.OrderDetailsZones.Add(z);

                }
                od.TPost1 = null;
                od.TPost2 = null;
                od.TPost3 = null;

                return od;

            }
            catch
            {
                var od = new OrderDetails();
                od.DoNotCreate = true;
                return od;
            }
        }

        private List<OrderDetailAccessory> CreateAscs(IWorksheet ws)
        {
            try 
            {   
                var ascs = new List<OrderDetailAccessory>();
                var startignRow = Helper.StartingRowAsc;
                var maxRow = Helper.EndingRowAsc;

                for (int i = startignRow; i <= maxRow; i++)
                {
                    var asc = CreateAsc(ws, i);
                    if (!asc.DoNotCreate)
                        ascs.Add(asc);

                }                

                return ascs;

            }
            catch
            {
                return new List<OrderDetailAccessory>();
            }
        }

        private OrderDetailAccessory CreateAsc(IWorksheet ws, int row)
        {
            try
            {
                var asc = new OrderDetailAccessory();

                asc.Length= GetIntValue(ws.Range[Helper.AscLength + row.ToString()]);
                asc.QTY = GetIntValue(ws[Helper.AscQty + row.ToString()]);
                asc.ShutterAccessoryID = GetIntValue(ws.Range[Helper.AscProfile + row.ToString()]);
                asc.Colour= GetIntValue(ws.Range[Helper.AscColour + row.ToString()]);
                if (asc.ShutterAccessoryID == 0 || asc.QTY == 0)
                    asc.DoNotCreate = true;

                return asc;

            }
            catch
            {
                var asc =  new OrderDetailAccessory();
                asc.DoNotCreate = true;
                return asc;
            }
        }

        private string GetStringValue(IRange cell)
        {
            try
            {
                return cell.FormulaStringValue;
            }
            catch
            {
                return "";
            }
        
        }
        private int GetIntValue(IRange cell)
        {
            try
            {
                return (int)cell.FormulaNumberValue;
            }
            catch
            {
                return 0;
            }

        }

        private Order  SaveOrder(Customer c, List<OrderDetails> ods, User u)
        {
            try
            {
                c.UserID = u.UserID;
                c.CreateCustomer();
                var o = new Order(c.CustomerID, u.UserID);
                o.OrderDetails = ods;
                o.CreateOrder();                
                return o;

            }
            catch (Exception e) 
            {
                throw new Exception("Unable to Save order: " +  e.Message.ToString());
            }
        }

        private void SendCompletedEmail(Stream ms, Order o, string FileName)
        {            
            var message = string.Format("Excel Import Success - OrderID - {0} for file {1}", o.OrderID.ToString(), FileName);
            Email.SendEmail(ms, cc, "", message, message, FileName);
            
        }
        private void SendFailureEmail(Stream ms, string FileName, string error)
        {            
            var message = string.Format("Excel Import Faulure - file {0}", FileName);
            var body = String.Format("Error when importing file : {0}", error);
            Email.SendEmail(ms, cc, "", message, body, FileName);
            dbHelper.LogError(error, FileName);
        }








    }
}

using Dapper;
using DataAcess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;
using Syncfusion.XlsIO;

namespace Domain
{
    public class OrderDetails
    {
        public int OrderID { get; set; }
        public int OrderDetailsID { get; set; }

        public bool DoNotCreate { get; set; }
        
        public List<OrderDetailAccessory> OrderDetailAccessories { get; set; }

        public List<OrderDetailsZones> OrderDetailsZones { get; set; } = new List<OrderDetailsZones>();

        public String Description { get; set; } = "";
        public int ModelID { get; set; }
        public int OperationID { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
        public int ColourID { get; set; }
        public int TiltMechanismID { get; set; }
        public int BlackOutBlindID { get; set; }
        public int HingeID { get; set; }
        public int LouvreSizeID { get; set; }
        public int PanelConfigID { get; set; }
        public int? TierOnTierHeight { get; set; }
        public int? FirstDividerPosition { get; set; }
        public int? SecondDividerPosition { get; set; }
        public int? SplitTiltPosition { get; set; }
        public int? SplitPositionOne { get; set; }
        public int? SplitPositionTwo { get; set; }
        public int? TPost1 { get; set; }
        public int? TPost2 { get; set; }
        public int? TPost3 { get; set; }
        public int FrameTypeID { get; set; }
        public int FrameSideID { get; set; }
        public int HardwareID { get; set; }
        public int? BuildOut { get; set; }
        public int? SpareLouvres { get; set; }
        public int? OperatingPole { get; set; }
        public int? Qty { get; set; }
        public bool? InSpec { get; set; }
        public decimal? Cost { get; set; }
        public int NestingGroup { get; set; }
        
        public decimal ShutterBaseCost { get; set; }
        public decimal AccessoryBaseCost { get; set; }
        public decimal ShutterMarginCost { get; set; }
        public decimal AccessoryMarginCost { get; set; }

        public int HardwareHeight { get; set; }

        public bool WarrantyWaived { get; set; }
        public int? BlackOutBlindOperation { get; set; }

        public int DoorOpening { get; set; }

        public int NoOfTPosts { get; set; }

        public bool ExcludeFromQuote { get; set; }

        
        public int GuidedTrackTypeID { get; set; }

        public bool HorizontalTPost { get; set; }

        public int FrameKeyID { get; set; }

        public int UpdatedByUserID { get; set; }

        public int ExtraClearance { get; set; }

       
        public void Save(int orderId)
        {

            OrderID = orderId;
            DynamicParameters dynamicParameters = new DynamicParameters();

            dynamicParameters.Add("@OrderID", OrderID);

        //public List<OrderDetailAccessory> OrderDetailAccessories { get; set; }

        //    public List<OrderDetailsZones> OrderDetailsZones { get; set; }      

            dynamicParameters.Add("@Description", Description);
            dynamicParameters.Add("@ModelID", ModelID);
            dynamicParameters.Add("@OperationID", OperationID);
            dynamicParameters.Add("@Height", Height);
            dynamicParameters.Add("@Width", Width);
            dynamicParameters.Add("@ColourID", ColourID);
            dynamicParameters.Add("@TiltMechanismID", TiltMechanismID);
            dynamicParameters.Add("@BlackOutBlindID", BlackOutBlindID);
            dynamicParameters.Add("@HingeID", HingeID);
            dynamicParameters.Add("@LouvreSizeID", LouvreSizeID);


            dynamicParameters.Add("@PanelConfigID", PanelConfigID);
            dynamicParameters.Add("@TierOnTierHeight", TierOnTierHeight);
            dynamicParameters.Add("@FirstDividerPosition", FirstDividerPosition);
            dynamicParameters.Add("@SecondDividerPosition", SecondDividerPosition);
            dynamicParameters.Add("@SplitTiltPosition", SplitTiltPosition);
            dynamicParameters.Add("@SplitPositionOne", SplitPositionOne);
            dynamicParameters.Add("@SplitPositionTwo", SplitPositionTwo);
            dynamicParameters.Add("@TPost1", TPost1);
            dynamicParameters.Add("@TPost2", TPost2);
            dynamicParameters.Add("@TPost3", TPost3);
            dynamicParameters.Add("@FrameTypeID", FrameTypeID);
            dynamicParameters.Add("@FrameSideID", FrameSideID);
            dynamicParameters.Add("@HardwareID", HardwareID);
            dynamicParameters.Add("@BuildOut", BuildOut);
            dynamicParameters.Add("@SpareLouvres", SpareLouvres);
            dynamicParameters.Add("@OperatingPole", OperatingPole);
            dynamicParameters.Add("@Qty", 1);
            dynamicParameters.Add("@InSpec", InSpec);
            dynamicParameters.Add("@Cost", Cost);
            dynamicParameters.Add("@NestingGroup", NestingGroup);
            dynamicParameters.Add("@ShutterBaseCost", ShutterBaseCost);
            dynamicParameters.Add("@AccessoryBaseCost", AccessoryBaseCost);
            dynamicParameters.Add("@ShutterMarginCost", ShutterMarginCost);
            dynamicParameters.Add("@AccessoryMarginCost", AccessoryMarginCost);
            dynamicParameters.Add("@HardwareHeight", HardwareHeight);
            dynamicParameters.Add("@WarrantyWaived", WarrantyWaived);
            dynamicParameters.Add("@BlackOutBlindOperation", BlackOutBlindOperation);
            dynamicParameters.Add("@DoorOpening", DoorOpening);
            dynamicParameters.Add("@NoOfTPosts", NoOfTPosts);
            dynamicParameters.Add("@ExcludeFromQuote", ExcludeFromQuote);
            dynamicParameters.Add("@GuidedTrackTypeID", GuidedTrackTypeID);
            dynamicParameters.Add("@HorizontalTPost", HorizontalTPost);
            dynamicParameters.Add("@FrameKeyID", FrameKeyID);            
            //dynamicParameters.Add("@ExtraClearance", ExtraClearance);
            OrderDetailsID = dbHelper.ExecuteScaler("OrderDetails_Create", dynamicParameters);

            foreach (var z in OrderDetailsZones)
            {
                z.Save(OrderDetailsID);
            }


        }

    }
}

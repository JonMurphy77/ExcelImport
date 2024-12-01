using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Syncfusion.XlsIO.Parser.Biff_Records.AutoFilterRecord;

namespace ExcellProcessor
{

   
    public  static class Helper
    {
        public const string cellUserName = "C4";
        public const string cellCustomerName = "D6";
        public const int StartingRowOrderdetails = 10;
        public const int EndingRowOrderdetails = 29;
        public const int StartingRowAsc= 32;
        public const int EndingRowAsc = 41;

        public const string Width = "C";
        public const string Height = "D";
        public const string Room = "E";
        public const string Model = "F";
        public const string PanelConfiguration = "G";
        public const string DoorOpening = "H";
        public const string MidRailOne = "I";
        public const string MidRailTwo = "J";
        public const string UpperSplitPosition = "K";
        public const string LowerSplitPosition = "L";
        public const string TierOnTierHeight = "M";
        public const string LouvreSize = "N";
        public const string Colour = "O";
        public const string CustomColour = "P";
        public const string FrameKey = "Q";
        public const string FrameType = "R";
        public const string FrameSides = "S";
        public const string TPost1 = "T";
        public const string TPost2 = "U";
        public const string TPost3 = "V";

        public const string AscProfile = "C";
        public const string AscLength = "D";
        public const string AscQty = "E";
        public const string AscColour = "F";

    }
}

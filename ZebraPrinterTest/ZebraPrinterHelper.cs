using System;
using System.Text;
using Zebra.Sdk.Comm;
using PrintLib.Printers.Zebra;
namespace ZebraPrinterTest
{
    public class ZebraPrinterHelper
    {
        public static bool ZebraPrinter(String Result, String IDCode)
        {
            Printer printer = new Printer("ZDesigner GT800 (ZPL)");
            //构件打印字符串static
            StringBuilder hexBuilder = new StringBuilder(4 * 1024);

            hexBuilder.Append("~CT~~CD,~CC^~CT~\n" +
                    "^XA~TA000~JSN^LT0^MNW^MTT^PON^PMN^LH0,0^JMA^PR5,5~SD15^JUS^LRN^CI0^XZ\n");
            hexBuilder.Append(printer.TextToHex("中文","000.GRF",20));
            
            hexBuilder.Append("^XA\n" +
                    "^MMT\n" +
                    "^PW609\n" +
                    "^LL0406\n" +
                    "^LS0\n" +
                    "^FT128,192^XG000.GRF,1,1^FS\n" +
                    "^PQ1,0,1,Y^XZ\n" +
                    "^XA^ID000.GRF^FS^XZ");
            //打印
            try
            {
                var printerConnection = new DriverPrinterConnection("ZDesigner GT800 (ZPL)");
           
                printerConnection.Open();
                printerConnection.Write(Encoding.UTF8.GetBytes(hexBuilder.ToString()));
                printerConnection.Close();
                return true;
            }
            catch(Exception ex)
            {
               
                return false;
            }

        }
    }   
}

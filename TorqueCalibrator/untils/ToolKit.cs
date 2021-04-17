using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace TorqueCalibrator.untils
{
   public class ToolKit
    {
       public static string StrToMD5(string str)
       {
           byte[] data = Encoding.GetEncoding("GB2312").GetBytes(str);
           MD5 md5 = new MD5CryptoServiceProvider();
           byte[] OutBytes = md5.ComputeHash(data);

           string OutString = "";
           for (int i = 0; i < OutBytes.Length; i++)
           {
               OutString += OutBytes[i].ToString("x2");
           }
           // return OutString.ToUpper();
           return OutString.ToLower();
       }

       public static string GetAppSetting(string key)
       {
           if (!ConfigurationManager.AppSettings.AllKeys.Contains(key))
           {
               return "";
           }
           ConfigurationManager.RefreshSection("appSettings");
           string value = ConfigurationManager.AppSettings[key];
           return value;
       }
    }
}

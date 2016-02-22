using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGeneration.extention
{
   public static class MyStringExtention
   { 
       public static string ToFirstUpper(this string str)
       {
           return str.Substring(0, 1).ToUpper() + str.Substring(1);
       } 
       public static string ToFirstLower(this string str)
       {
           return str.Substring(0, 1).ToLower() + str.Substring(1);
       }
       public static string RemoveFirstLetter(this string str)
       {
           return str.Substring(1);
       }
      public static string SubResponse(this string str)
       {
           if(str.IndexOf("<") <= -1)
           {
               return str;
           }
           return str.Split('<').Length > 0 ?str.Split('<')[1].Replace(">",""):str;
       }
       public static string GetDefaultExcutor(this string str)
       {
           str = str.Replace("Executor", "");

           return "Default" + str.Substring(1) + "Executor";
       }

       public static string RemoveExcutor(this string str)
       {
           str = str.Replace("Executor", "");
           return str;
           
       }
       
    }
}

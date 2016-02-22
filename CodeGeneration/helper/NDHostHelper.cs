using CodeGeneration.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeGeneration.extention;
using System.IO;

namespace CodeGeneration.helper
{
   public class NDHostHelper
    {
       public static void GenerateCode(InterfaceList interList, string hostPath)
       {
           string fileName = interList.FileName;
          
           interList.lstInterDis.ToList().ForEach(x =>
           {
               string content = FileHelper.ReadFile(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "template/ND.WebService.IISHost/HostTemplate.txt"));
               content = content.Replace("${FileName}", fileName);
               content = content.Replace("${ClassName}", x.InterfaceName.RemoveExcutor().RemoveFirstLetter().ToFirstUpper() + "Controller");
               FileHelper.WriteFile(Path.Combine(hostPath, "" + x.InterfaceName.RemoveExcutor().RemoveFirstLetter().ToFirstUpper() + "Service" + ".svc"), content);
           });
       }
    }
}

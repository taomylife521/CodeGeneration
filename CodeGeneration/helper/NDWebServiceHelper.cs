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
   public class NDWebServiceHelper
    {
       public static void GenerateCode(InterfaceList interList,string servicePath)
       {
           string fileName = interList.FileName;
           string coreFodlerName = interList.FileName + "Core";
           string pluginType = interList.FileName.ToFirstUpper() + "Plugin";
           StringBuilder serviceCore = new StringBuilder();
           StringBuilder paramList = new StringBuilder();
           interList.lstInterDis.ToList().ForEach(x =>
           {

              
               serviceCore.AppendLine("");
               x.MethodDis.ToList().ForEach(k =>
               {
                   paramList.Clear();
                   serviceCore.AppendLine("");
                   serviceCore.AppendLine("\t\t #region " + k.InterfaceMethodName);
                   serviceCore.AppendLine("\t\t [RuleOperationInterceptor] ");
                   serviceCore.Append("\t\t public string " + k.InterfaceMethodName.RemoveExcutor() + "(");
                   int index = 0;
                   k.Request.ToList().ForEach(m =>//参数
                   {
                       if (index > 0)
                       {
                           paramList.Append(",");
                           serviceCore.Append(",");
                       }
                       serviceCore.Append(m.RequestParamType + " " + m.RequestParamName);
                       paramList.Append(m.RequestParamName);
                       index++;
                   });
                   serviceCore.AppendLine(")");
                   serviceCore.AppendLine("\t\t {");
                   string requestType = k.Request.Count <= 0 ? "null" : k.Request[0].RequestParamType;
                   serviceCore.AppendLine("\t\t\t " + x.InterfaceName.RemoveExcutor() + " plugin = ControllerHelper.GetCurrentPlugin<" + requestType + ", " + x.InterfaceName.RemoveExcutor() + ">(" + paramList + ");");
                   serviceCore.AppendLine("\t\t\t return plugin."+k.InterfaceMethodName+"("+paramList+");");
                   serviceCore.AppendLine("\t\t }");
                   serviceCore.AppendLine("\t\t #endregion ");
               });
               string serviceContent = FileHelper.ReadFile(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "template/ND.WebService.Service/WebServiceController.txt"));
               serviceContent = serviceContent.Replace("${FileName}", fileName);
               serviceContent = serviceContent.Replace("${ClassName}", x.InterfaceName.RemoveExcutor().RemoveFirstLetter().ToFirstUpper() + "Controller");
               serviceContent = serviceContent.Replace("${InterfaceName}", x.InterfaceName.RemoveExcutor()).Replace("${CoreCode}", serviceCore.ToString());
               FileHelper.WriteFile(Path.Combine(servicePath, "" + x.InterfaceName.RemoveExcutor().RemoveFirstLetter().ToFirstUpper() + "Controller" + ".cs"), serviceContent);
           });
       }
    }
}

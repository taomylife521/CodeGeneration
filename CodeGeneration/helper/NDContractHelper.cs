using CodeGeneration.model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeGeneration.extention;

namespace CodeGeneration.helper
{
    public class NDContractHelper
    {
        public static void GenerateCode(InterfaceList interList, string contractPath)
        {
            string fileName = interList.FileName;
            string interfaceName = "";
         
         
            interList.lstInterDis.ToList().ForEach(x =>
            {
                interfaceName = x.InterfaceName;
                StringBuilder contractCore = new StringBuilder();
                contractCore.AppendLine("");
                x.MethodDis.ToList().ForEach(k =>
                {
                    contractCore.AppendLine("");
                    contractCore.AppendLine("\t\t [OperationContract]");
                    contractCore.Append("\t\t  string " + k.InterfaceMethodName + "(");
                    int index = 0;
                    k.Request.ToList().ForEach(m =>//参数
                    {
                        if (index > 0)
                        {
                            contractCore.Append(",");
                        }
                        contractCore.Append(m.RequestParamType + " " + m.RequestParamName);
                     
                        index++;
                    });
                    contractCore.AppendLine(");");
                   
                });
                string contractContent = FileHelper.ReadFile(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "template/ND.WebService.Contract/ContractTemplate.txt"));
                contractContent = contractContent.Replace("${FileName}", fileName);
                contractContent = contractContent.Replace("${InterfaceName}", x.InterfaceName.RemoveExcutor()).Replace("${CoreCode}", contractCore.ToString());
                FileHelper.WriteFile(Path.Combine(contractPath, "" + x.InterfaceName.RemoveExcutor() + ".cs"), contractContent);
            });
        }
    }
}

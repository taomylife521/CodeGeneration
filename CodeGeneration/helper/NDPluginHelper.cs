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
    public class NDPluginHelper
    {

        #region 生成插件代码
        public static void GeneratePluginCode(string pluginPath,InterfaceList interList)
        {
            string fileName = interList.FileName;
            
            string coreFodlerName = interList.FileName + "Core";
           
            string className = "";
            string handlerContext = "I" + fileName.ToFirstUpper()+"HandlerContext";
            string handler = "I" + fileName.ToFirstUpper() + "Handler";
            StringBuilder interfaceList = new StringBuilder();
            StringBuilder pluginCore = new StringBuilder();
            StringBuilder paramList = new StringBuilder();
            interList.lstInterDis.ToList().ForEach(x =>
            {
                string pluginType = x.InterfaceName.RemoveExcutor().RemoveFirstLetter().ToFirstUpper() + "Plugin";
                interfaceList.Append(x.InterfaceName.RemoveExcutor() + ",");
                pluginCore.AppendLine("");
                x.MethodDis.ToList().ForEach(k =>
                {
                    paramList.Clear();
                    pluginCore.AppendLine("");
                    pluginCore.AppendLine("\t\t #region " + k.InterfaceMethodName);
                    pluginCore.Append("\t\t public string " + k.InterfaceMethodName + "(");
                    int index = 0;
                    k.Request.ToList().ForEach(m =>//参数
                    {
                        if (index > 0)
                        {
                            paramList.Append(",");
                            pluginCore.Append(",");
                        }
                        pluginCore.Append(m.RequestParamType + " " + m.RequestParamName);
                        paramList.Append(m.RequestParamName);
                        index++;
                    });
                    pluginCore.AppendLine(")");
                    pluginCore.AppendLine("\t\t {");
                    pluginCore.AppendLine("\t\t\t try");
                    pluginCore.AppendLine("\t\t\t {");
                    pluginCore.AppendLine("\t\t\t\t " + handlerContext + " handlerContext =new " + handlerContext.RemoveFirstLetter() + "();");
                    pluginCore.AppendLine("\t\t\t\t " + handler + " handler =new " + handler.RemoveFirstLetter() + "(handlerContext);");
                    pluginCore.AppendLine("\t\t\t\t " + k.Response.ResponseParamType + " baseResponse = handler." + k.InterfaceMethodName + "(" + paramList + ");");
                    string clientType = k.Request.Count <= 0 ? "ClientType.NDFront" : paramList.ToString()+".ClientType";
                    pluginCore.AppendLine("\t\t\t\t string result = PluginHelper<" + k.Response.ResponseParamType.SubResponse() + ", I" + k.Response.ResponseParamType.SubResponse() + ">.SelectResponsePlugin(baseResponse, " + clientType + ");//根据客户端类型找对应的插件返回，app或者前台或者后台");
                    pluginCore.AppendLine("\t\t\t\t  return result;");
                    pluginCore.AppendLine("\t\t\t }");
                    pluginCore.AppendLine("\t\t\t catch (Exception ex)");
                    pluginCore.AppendLine("\t\t\t {");
                    pluginCore.AppendLine("\t\t\t\t LogHelper.Log(typeof(" + pluginType + "), ex.Message + \"\\r\\n\" + ex.StackTrace + \"\\r\\n\" + ex.InnerException);");
                    pluginCore.AppendLine("\t\t\t\t return JsonConvert.SerializeObject(new ResponseBase<EmptyResponse> { errCode = ErrorCode.e00001.ToString(), errMsg = ErrorCode.e00001.description(), ResponseData = null });");
                    pluginCore.AppendLine("\t\t\t }");
                    pluginCore.AppendLine("\t\t }");
                    pluginCore.AppendLine("\t\t #endregion ");
                });
                string handlerContent = FileHelper.ReadFile(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "template/ND.Plugin.DefaultPlugin/PluginTemplate.txt"));
                handlerContent = handlerContent.Replace("${CoreFolderName}", coreFodlerName).Replace("${FileName}", fileName);
                handlerContent = handlerContent.Replace("${PluginType}", pluginType).Replace("${ClassName}", pluginType);
                handlerContent = handlerContent.Replace("${InterfaceName}", interfaceList.ToString().Substring(0, interfaceList.Length - 1)).Replace("${CoreCode}", pluginCore.ToString());
                FileHelper.WriteFile(Path.Combine(pluginPath, "" + x.InterfaceName.RemoveExcutor().RemoveFirstLetter().ToFirstUpper() + "Plugin" + ".cs"), handlerContent);
            });
        }
        #endregion
    }
}

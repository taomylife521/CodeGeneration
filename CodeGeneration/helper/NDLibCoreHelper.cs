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
   public class NDLibCoreHelper
    {
        #region 生成配置类
        public static void GenerateCoreConfig(InterfaceList interList, string configPath)
        {

            string ConfigContent = FileHelper.ReadFile(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "template/ND.Lib.Core/ConfigHelperTemplate.txt"));
            ConfigContent = ConfigContent.Replace("${CoreFolderName}", interList.FileName + "Core");
            FileHelper.WriteFile(Path.Combine(configPath, "ConfigHelper.cs"), ConfigContent);

        }
        #endregion

        #region 生成依赖类
        public static void GenerateDependency(string dependencyPath, InterfaceList interList)
        {
            string dependencyClassName = interList.FileName.ToFirstUpper() + "DependencyResolver";
            string dependencyContent = FileHelper.ReadFile(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "template/ND.Lib.Core/DependencyTemplate.txt"));//读取依赖模板
            dependencyContent = dependencyContent.Replace("${CoreFolderName}", interList.FileName + "Core");
            StringBuilder dependencyCore = new StringBuilder();
            StringBuilder serviceDictionary = new StringBuilder();
            string tempVar = "";//变量名
            interList.lstInterDis.ForEach(x =>//接口列表
            {
                tempVar = x.InterfaceName.RemoveFirstLetter().ToFirstLower();
                dependencyCore.AppendLine("\t\tprivate static readonly " + x.InterfaceName + " " + tempVar + " = new " + x.InterfaceName.GetDefaultExcutor() + "();");
                serviceDictionary.AppendLine("\t\t\t{typeof (" + x.InterfaceName + "), () => " + tempVar + "},");
            });
            dependencyCore.AppendLine("\t\tprivate static readonly IDictionary<Type, Func<object>> ServiceDictionary = new Dictionary<Type, Func<object>>//服务类型字典");
            dependencyCore.AppendLine("\t\t {");
            dependencyCore.Append(serviceDictionary.ToString().Substring(0, serviceDictionary.ToString().LastIndexOf(",")));
            dependencyCore.AppendLine("\t\t };");
            
            dependencyContent = dependencyContent.Replace("${ClassName}", dependencyClassName);
            dependencyContent = dependencyContent.Replace("${CoreCode}", dependencyCore.ToString());
            FileHelper.WriteFile(Path.Combine(dependencyPath, "" + dependencyClassName + ".cs"), dependencyContent);


        }
        #endregion

        #region 生成handler类
        public static void GenerateHandlers(InterfaceList interList, string handlerPath)
        {
            string coreFodlerName = interList.FileName + "Core";
            string fileName = interList.FileName;
            string handlerInterName = "I" + interList.FileName.ToFirstUpper() + "Handler";
            string handlerClassName = interList.FileName.ToFirstUpper() + "Handler";
            StringBuilder handlerInterfaceList = new StringBuilder();
            StringBuilder handlerCore = new StringBuilder();
            StringBuilder paramList = new StringBuilder();
            interList.lstInterDis.ToList().ForEach(x =>
            {
                handlerInterfaceList.Append(x.InterfaceName + ",");
                handlerCore.AppendLine("");
                x.MethodDis.ToList().ForEach(k =>
                {
                    paramList.Clear();
                    handlerCore.AppendLine("");
                    handlerCore.AppendLine("\t\t #region " + k.InterfaceMethodName);
                    handlerCore.Append("\t\t public " + k.Response.ResponseParamType + " " + k.InterfaceMethodName + "(");
                    int index = 0;
                    k.Request.ToList().ForEach(m =>//参数
                    {
                        if (index > 0)
                        {
                            paramList.Append(",");
                            handlerCore.Append(",");
                        }
                        handlerCore.Append(m.RequestParamType + " " + m.RequestParamName);
                        paramList.Append(m.RequestParamName);
                        index++;
                    });
                    handlerCore.AppendLine(")");
                    handlerCore.AppendLine("\t\t {");
                    handlerCore.AppendLine("\t\t\t" + x.InterfaceName + " executor = Context.dependencyResolver.GetService<" + x.InterfaceName + ">();");
                    handlerCore.AppendLine("\t\t\t if(executor == null)");
                    handlerCore.AppendLine("\t\t\t {");
                    handlerCore.AppendLine("\t\t\t\t  throw new NullReferenceException(\"" + handlerClassName + "." + x.InterfaceName + " is null\");");
                    handlerCore.AppendLine("\t\t\t }");
                    handlerCore.AppendLine(" \t\t\treturn executor." + k.InterfaceMethodName + "(" + paramList.ToString() + ");");
                    handlerCore.AppendLine("\t\t }");
                    handlerCore.AppendLine("\t\t #endregion ");
                });


            });
            string handlerContent = FileHelper.ReadFile(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "template/ND.Lib.Core/HandlersTemplate.txt"));
            handlerContent = handlerContent.Replace("${CoreFolderName}", coreFodlerName).Replace("${FileName}", fileName);
            handlerContent = handlerContent.Replace("${InterfaceName}", handlerInterName).Replace("${HandlerInterfaceList}", handlerInterfaceList.ToString().Substring(0, handlerInterfaceList.Length - 1));
            handlerContent = handlerContent.Replace("${ClassName}", handlerClassName).Replace("${CoreCode}", handlerCore.ToString());
            FileHelper.WriteFile(Path.Combine(handlerPath, "" + handlerInterName + ".cs"), handlerContent);
        }
        #endregion

        #region 生成handlerContext类
        public static void GenerateHandlerContext(InterfaceList interList, string handlerContextPath)
        {
            string coreFodlerName = interList.FileName + "Core";
            string handlerContextDependencyClassName = interList.FileName.ToFirstUpper() + "DependencyResolver";
            string handlerContextInterfaceName = "I" + interList.FileName.ToFirstUpper() + "HandlerContext";
            string handlerContextContent = FileHelper.ReadFile(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "template/ND.Lib.Core/HandlerContextTemplate.txt"));
            handlerContextContent = handlerContextContent.Replace("${CoreFolderName}", coreFodlerName).Replace("${InterfaceName}", handlerContextInterfaceName);
            handlerContextContent = handlerContextContent.Replace("${ClassName}", handlerContextInterfaceName.RemoveFirstLetter()).Replace("${HandlerContextDependencyClassName}", handlerContextDependencyClassName);
            FileHelper.WriteFile(Path.Combine(handlerContextPath, "" + handlerContextInterfaceName + ".cs"), handlerContextContent);
        }
        #endregion

        #region 生成ExuctorImpl类
       public static void GenerateCoreExuctorImpl(InterfaceList interList,string excutorImplPath)
        {
            string coreFodlerName = interList.FileName + "Core";
            string fileName = interList.FileName;
            string className = "";
            string interfaceName = "";
            StringBuilder excutorImplCore = new StringBuilder();
            StringBuilder paramList = new StringBuilder();
            interList.lstInterDis.ToList().ForEach(x =>//遍历所有接口
            {
                className = x.InterfaceName.GetDefaultExcutor();
                interfaceName = x.InterfaceName;
                string exuctorImplContent = FileHelper.ReadFile(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "template/ND.Lib.Core/ExcutorImplTemplate.txt"));
               exuctorImplContent= exuctorImplContent.Replace("${CoreFolderName}", coreFodlerName).Replace("${FileName}", fileName);
               exuctorImplContent = exuctorImplContent.Replace("${ClassName}", className).Replace("${InterfaceName}", interfaceName);
               x.MethodDis.ToList().ForEach(k =>
               {
                   paramList.Clear();
                   excutorImplCore.AppendLine("");
                   excutorImplCore.AppendLine("\t\t #region " + k.InterfaceMethodName);
                   excutorImplCore.Append("\t\t public " + k.Response.ResponseParamType + " " + k.InterfaceMethodName + "(");
                   int index = 0;
                   k.Request.ToList().ForEach(m =>//参数
                   {
                       if (index > 0)
                       {
                           paramList.Append(",");
                           excutorImplCore.Append(",");
                       }
                       excutorImplCore.Append(m.RequestParamType + " " + m.RequestParamName);
                       paramList.Append(m.RequestParamName);
                       index++;
                   });
                   excutorImplCore.AppendLine(")");
                   excutorImplCore.AppendLine("\t\t {");
                   excutorImplCore.AppendLine("\t\t\t  " + k.Response.ResponseParamType + " rep = new " + k.Response.ResponseParamType + "();");
                   excutorImplCore.AppendLine("");
                   excutorImplCore.AppendLine("\t\t\t //to do whatever you want;");
                   excutorImplCore.AppendLine("");
                   excutorImplCore.AppendLine("\t\t\t  return rep;");
                   excutorImplCore.AppendLine("\t\t }");
                   excutorImplCore.AppendLine("\t\t #endregion ");
               });
               exuctorImplContent = exuctorImplContent.Replace("${CoreCode}", excutorImplCore.ToString());
               FileHelper.WriteFile(Path.Combine(excutorImplPath, "" + className + ".cs"), exuctorImplContent);
            });

        }
        #endregion

        #region 生成GenerateCoreExcutorInter
       public static void GenerateCoreExcutorInter(InterfaceList interList, string excutorInterPath)
        {
            //foreach (KeyValuePair<string, string> item in SrcExcutorInter)
            //{
            //    FileHelper.WriteFile(Path.Combine(ExcutorInterPath, "" + item.Key + ".cs"), item.Value);
            //}

            string coreFodlerName = interList.FileName + "Core";
            string fileName = interList.FileName;
            string interfaceName = "";
           
            StringBuilder paramList = new StringBuilder();
            interList.lstInterDis.ToList().ForEach(x =>//遍历所有接口
            {
                StringBuilder excutorImplCore = new StringBuilder();
                interfaceName = x.InterfaceName;
                string exuctorImplContent = FileHelper.ReadFile(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "template/ND.Lib.Core/ExcutorInterTemplate.txt"));
                exuctorImplContent = exuctorImplContent.Replace("${CoreFolderName}", coreFodlerName).Replace("${FileName}", fileName);
                exuctorImplContent = exuctorImplContent.Replace("${InterfaceName}", interfaceName);
                x.MethodDis.ToList().ForEach(k =>
                {
                    paramList.Clear();
                    excutorImplCore.AppendLine("");
                    excutorImplCore.Append("\t\t  " + k.Response.ResponseParamType + " " + k.InterfaceMethodName + "(");
                    int index = 0;
                    k.Request.ToList().ForEach(m =>//参数
                    {
                        if (index > 0)
                        {
                            paramList.Append(",");
                            excutorImplCore.Append(",");
                        }
                        excutorImplCore.Append(m.RequestParamType + " " + m.RequestParamName);
                        paramList.Append(m.RequestParamName);
                        index++;
                    });
                    excutorImplCore.AppendLine(");");
                });
                exuctorImplContent = exuctorImplContent.Replace("${CoreCode}", excutorImplCore.ToString());
                FileHelper.WriteFile(Path.Combine(excutorInterPath, "" + x.InterfaceName + ".cs"), exuctorImplContent);
            });
        }
        #endregion
    }
}

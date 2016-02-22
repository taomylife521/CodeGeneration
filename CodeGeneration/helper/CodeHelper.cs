using CodeGeneration.model;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeGeneration.extention;

namespace CodeGeneration.helper
{
   public class CodeHelper
    {
        #region 生成接口描述类
        /// <summary>
        /// 生成接口描述类
        /// </summary>
        /// <param name="code"></param>
        public static InterfaceDiscription GetInterfaceDiscrption(string code)
        {
            InterfaceDiscription interDis = new InterfaceDiscription();
            SyntaxTree tree = Microsoft.CodeAnalysis.CSharp.CSharpSyntaxTree.ParseText(code);
            var root = (Microsoft.CodeAnalysis.CSharp.Syntax.CompilationUnitSyntax)tree.GetRoot();
            var collctor = new UsingCollector();//收集非System程序集的引用
            collctor.Visit(root);
            foreach (var item in collctor.Usings)
            {
                interDis.InterUsings.Add(new InterfaceUsingDiscription{UsingName="using "+item.Name.ToString()});
            }
            var firstMember = root.Members[0];
            NamespaceDeclarationSyntax NameSpaceDeclaration = (NamespaceDeclarationSyntax)firstMember;
            InterfaceDeclarationSyntax interfaceDeclaration = (InterfaceDeclarationSyntax)NameSpaceDeclaration.Members[0];
            string NameSpaceName = NameSpaceDeclaration.Name.ToString();
          //  interDis.FileName = NameSpaceName.Substring(NameSpaceName.LastIndexOf(".")+1);//接口所在文件名
            interDis.InterfaceName = interfaceDeclaration.Identifier.Value.ToString();//接口名称
            interDis.NameSpaceName = NameSpaceDeclaration.Name.ToString();//命名空间
            //接口方法
            interfaceDeclaration.Members.ToList().ForEach(y =>
             {
                 InterfaceMethodDiscription methodDis = new InterfaceMethodDiscription();//接口方法描述
                 MethodDeclarationSyntax methodDeclaration = (MethodDeclarationSyntax)y;
                 var paramsDeclaration = methodDeclaration.ParameterList.Parameters;//请求参数集合
                 methodDis.Response.ResponseParamDis = "";//返回值描述
                 methodDis.Response.ResponseParamType = methodDeclaration.ReturnType.ToString();//响应类型
                 methodDis.InterfaceMethodName = methodDeclaration.Identifier.ToString();//接口方法名称
                 methodDis.InterfaceMethodDis = "";//接口方法描述
                 //接口请求参数
                 paramsDeclaration.ToList().ForEach(x =>
                 {
                     RequestParamsDiscription requestDis = new RequestParamsDiscription();//请求描述
                     requestDis.RequestParamDis = "";//请求参数描述
                     requestDis.RequestParamName = x.Identifier.ToString();//请求参数名称
                     requestDis.RequestParamType = x.Type.ToString();//请求参数类型
                     methodDis.Request.Add(requestDis);
                 });
                 interDis.MethodDis.Add(methodDis);
             });
            return interDis;
        } 
        #endregion

        #region 旧代码

        ///// <summary>
        ///// 添加通用头部
        ///// </summary>
        ///// <param name="str"></param>
        //private static void AddCommonHeader(ref StringBuilder str)
        //{
        //    str.AppendLine("using System");
        //    str.AppendLine("using System.Collections.Generic");
        //    str.AppendLine("using System.Linq");
        //    str.AppendLine("using System.ServiceModel");
        //    str.AppendLine("using System.Text");
        //    str.AppendLine("using System.Threading.Tasks");
        //}

        //private static void AddExtraHeader(ref StringBuilder str, InterfaceDiscription interDis = null)
        //{
        //    if (interDis != null)
        //    {
        //        foreach (var item in interDis.InterUsings)
        //        {
        //            str.AppendLine(item.UsingName);

        //        }

        //    }



        //}
        //#region 生成ND.WebService.Service
        //public static void GenerateService(InterfaceList interList)
        //{
        //    string ServiceName = "";//服务文件名
        //    string DirectPath = "";//保存目录
        //    string RequestType = "";//请求类型
        //    string RequestName = "";//请求名称
        //    string InterfaceName = "";//接口名称
        //    interList.lstInterDis.ToList().ForEach(x =>
        //    {
        //        InterfaceName = x.InterfaceName.Replace("Executor", "");
        //        StringBuilder str = new StringBuilder();
        //        ServiceName = InterfaceName.Substring(1) + "Controller";
        //        AddCommonHeader(ref str);//添加通用头部
        //        AddExtraHeader(ref str, x);//添加额外的引用头部
        //        str.AppendLine("namespace ND.WebService.Service." + x.FileName);
        //        str.AppendLine("{");
        //        str.AppendLine("  public  class " + ServiceName + ":" + InterfaceName + "");
        //        str.AppendLine("\t{");

        //        x.MethodDis.ToList().ForEach(k =>
        //        {
        //            str.AppendLine("\t\t[RuleOperationInterceptor]");
        //            str.Append("\t\tpublic string " + k.InterfaceMethodName + "(");
        //            int index = 0;
        //            k.Request.ToList().ForEach(m =>//参数
        //            {
        //                if (index > 0)
        //                {
        //                    str.Append(",");
        //                }
        //                str.Append(m.RequestParamType + " " + m.RequestParamName);
        //                index++;
        //            });
        //            str.AppendLine(")");
        //            str.AppendLine("\t\t {");
        //            RequestType = k.Request.Count > 0 ? k.Request[0].RequestParamType : RequestType;
        //            RequestName = k.Request.Count > 0 ? k.Request[0].RequestParamName : RequestName;
        //            str.AppendLine("\t\t\t" + InterfaceName + " plugin = ControllerHelper.GetCurrentPlugin<" + RequestType + "," + InterfaceName + ">(" + RequestName + ");");
        //            str.AppendLine("\t\t\treturn plugin." + k.InterfaceMethodName + "(" + RequestName + ");");
        //            str.AppendLine("\t\t }");
        //        });
        //        str.AppendLine("\t}");
        //        str.AppendLine("}");
        //        DirectPath = System.Configuration.ConfigurationManager.AppSettings["ND.Web.Service"] + x.FileName;
        //        ApiLogHelper.Save(DirectPath, ServiceName, str.ToString());
        //    });




        //}
        //#endregion

        //#region 生成插件层代码ND.Plugin.DefaultPlugin
        //public static void GeneratePlugin(InterfaceList interList)
        //{
        //    string FileName = "";//插件文件名
        //    string DirectPath = "";//保存目录
        //    string RequestType = "";//请求类型
        //    string RequestName = "";//请求名称
        //    string InterfaceName = "";//接口名称
        //    string enter = ",";//\r\n
        //    interList.lstInterDis.ToList().ForEach(x =>
        //    {
        //        InterfaceName = x.InterfaceName.Replace("Executor", "");
        //        StringBuilder str = new StringBuilder();
        //        FileName = InterfaceName.Substring(1) + "Plugin";
        //        AddCommonHeader(ref str);//添加通用头部
        //        AddExtraHeader(ref str, x);//添加额外的引用头部
        //        str.AppendLine("using ND.Lib.Enums.common;");
        //        str.AppendLine("using ND.Lib.LibHelper.log;");
        //        str.AppendLine("using ND.Plugin.PluginProvider;");
        //        str.AppendLine("using ND.Lib.LibHelper.extention;");
        //        str.AppendLine("using ND.Lib.Core." + x.FileName + "Core.handlers;");
        //        str.AppendLine("namespace ND.Plugin.DefaultPlugin." + x.FileName.ToFirstUpper());
        //        str.AppendLine("{");
        //        str.AppendLine("\tpublic class " + InterfaceName.RemoveFirstLetter() + "Plugin:" + InterfaceName + ",IPlugin");
        //        str.AppendLine("\t{");
        //        x.MethodDis.ToList().ForEach(k =>
        //        {
        //            str.Append("\t\tpublic string " + k.InterfaceMethodName + "(");
        //            int index = 0;
        //            k.Request.ToList().ForEach(m =>//参数
        //            {
        //                if (index > 0)
        //                {
        //                    str.Append(",");
        //                }
        //                str.Append(m.RequestParamType + " " + m.RequestParamName);
        //                index++;
        //            });
        //            str.AppendLine(")");
        //            str.AppendLine("\t\t {");
        //            RequestType = k.Request.Count > 0 ? k.Request[0].RequestParamType : RequestType;
        //            RequestName = k.Request.Count > 0 ? k.Request[0].RequestParamName : RequestName;
        //            str.AppendLine("\t\t\t try");
        //            str.AppendLine("\t\t\t {");
        //            str.AppendLine("\t\t\t\t   I" + x.FileName.ToFirstUpper() + "HandlerContext handlerContext = new  " + x.FileName.ToFirstUpper() + "HandlerContext();");
        //            str.AppendLine("\t\t\t\t   I" + x.FileName.ToFirstUpper() + "Handler handler = new " + x.FileName.ToFirstUpper() + "Handler(handlerContext);");
        //            str.AppendLine("\t\t\t\t   " + k.Response.ResponseParamType + " baseResponse = handler." + k.InterfaceMethodName + "(" + RequestName + ");//反序列化对象");
        //            str.AppendLine("\t\t\t\t   string result = PluginHelper<" + k.Response.ResponseParamType.SubResponse() + ", I" + k.Response.ResponseParamType.SubResponse() + ">.SelectResponsePlugin(baseResponse, " + RequestName + ".ClientType);//根据客户端类型找对应的插件返回，app或者前台或者后台");
        //            str.AppendLine("\t\t\t\t   return result;");
        //            str.AppendLine("\t\t\t }");
        //            str.AppendLine("\t\t\t catch (Exception e)");
        //            str.AppendLine("\t\t\t {");//e00001
        //            str.AppendLine("\t\t\t LogHelper.Log(typeof(" + InterfaceName.RemoveFirstLetter() + "Plugin), e.Message " + enter + " e.StackTrace  " + enter + " e.InnerException);");
        //            str.AppendLine("\t\t\t return JsonConvert.SerializeObject(new ResponseBase<EmptyResponse> { errCode = ErrorCode.e00001.ToString(), errMsg = ErrorCode.e00001.description(), ResponseData = null });");
        //            str.AppendLine("\t\t\t }");
        //            str.AppendLine("\t\t }");

        //        });
        //        str.AppendLine("\t}");
        //        str.AppendLine("}");
        //        DirectPath = System.Configuration.ConfigurationManager.AppSettings["ND.Plugin.DefaultPlugin"] + x.FileName;
        //        ApiLogHelper.Save(DirectPath, FileName, str.ToString());

        //    });
        //}
        //#endregion 
        #endregion


        #region 生成ND.Lib.Core
        public static void GenerateCore(InterfaceList interList)
        {
            string DirectPath = System.Configuration.ConfigurationManager.AppSettings["ND.Lib.Core"];
            List<string> lstCreateDir = new List<string>();
         
            #region 先生成各个文件夹
            //1.先生成各个文件夹
            string corePath = Path.Combine(DirectPath, interList.FileName + "Core");//核心path
            string dependencyInjectionPath = Path.Combine(corePath, "dependencyInjection");//依赖path
            string excutorImpPath = Path.Combine(corePath, "Executor/impl");
            string excutorInterPath = Path.Combine(corePath, "Executor/inter");
            string hanlderPath = Path.Combine(corePath, "handlers");
            string configPath = Path.Combine(corePath, "config");
          
            lstCreateDir.Add(corePath);
            lstCreateDir.Add(dependencyInjectionPath);
            lstCreateDir.Add(excutorImpPath);
            lstCreateDir.Add(excutorInterPath);
            lstCreateDir.Add(hanlderPath);
            lstCreateDir.Add(configPath);
            lstCreateDir.ForEach(k =>
            {
                DirFileHelper.CreateDirectory(k);
            });
            #endregion

            NDLibCoreHelper.GenerateDependency(dependencyInjectionPath, interList);//生成依赖类
            NDLibCoreHelper.GenerateHandlers(interList, hanlderPath);//生成handler类
            NDLibCoreHelper.GenerateHandlerContext(interList, hanlderPath);//生成handlercontext类
            NDLibCoreHelper.GenerateCoreConfig(interList, configPath);//生成配置类
            NDLibCoreHelper.GenerateCoreExcutorInter(interList, excutorInterPath);//生成excutorInter类
            NDLibCoreHelper.GenerateCoreExuctorImpl(interList, excutorImpPath);//生成接口实现类
          
        }
        #endregion

        #region 生成插件层代码
        public static void GenerateNDPlugin(InterfaceList interList)
        {
            string DirectPath = System.Configuration.ConfigurationManager.AppSettings["ND.Plugin.DefaultPlugin"];
            List<string> lstCreateDir = new List<string>();
            #region 先生成各个文件夹
            //1.先生成各个文件夹
            string corePath = Path.Combine(DirectPath, interList.FileName.ToFirstUpper()+"Plugin");//核心path
            lstCreateDir.Add(corePath);
            lstCreateDir.ForEach(k =>
            {
                DirFileHelper.CreateDirectory(k);
            });
            #endregion
            NDPluginHelper.GeneratePluginCode(corePath, interList);
        }
        #endregion

        #region 生成ND.Lib.DtoModel
       public static void GenerateDtoModel(InterfaceList interList)
       {
           string DirectPath = System.Configuration.ConfigurationManager.AppSettings["ND.Lib.DtoModel"];
           List<string> lstCreateDir = new List<string>();
           #region 先生成各个文件夹
           //1.先生成各个文件夹
           string corePath = Path.Combine(DirectPath, interList.FileName);//核心path
           string autoMappernPath = Path.Combine(corePath, "autoMapper");//依赖path
           string contextPath = Path.Combine(corePath, "context");
           string dtoEntityContextPath = Path.Combine(corePath, "dtoEntity/context");
           string dtoEntityResponsePath = Path.Combine(corePath, "dtoEntity/response");
           string responsePath = Path.Combine(corePath, "response");
           string responseAppPath = Path.Combine(corePath, "response/app");
           string responseBackstagePath = Path.Combine(corePath, "response/backstage");
           string responseBaseClassPath = Path.Combine(corePath, "response/baseClass");
           string responseFrontPath = Path.Combine(corePath, "response/front");
           string responseInterPath = Path.Combine(corePath, "response/inter"); 
           string contextValidatorPath = Path.Combine(corePath, "validator/contextValidator");
           string dtoEnitityValidatorPath = Path.Combine(corePath, "validator/dtoEnitityValidator");

           lstCreateDir.Add(corePath);
           lstCreateDir.Add(autoMappernPath);
           lstCreateDir.Add(contextPath);
           lstCreateDir.Add(dtoEntityContextPath);
           lstCreateDir.Add(dtoEntityResponsePath);
           lstCreateDir.Add(responsePath);
           lstCreateDir.Add(contextValidatorPath);
           lstCreateDir.Add(dtoEnitityValidatorPath);
           lstCreateDir.Add(responseAppPath);
           lstCreateDir.Add(responseBackstagePath);
           lstCreateDir.Add(responseBaseClassPath);
           lstCreateDir.Add(responseFrontPath);
           lstCreateDir.Add(responseInterPath);
           lstCreateDir.ForEach(k =>
           {
               DirFileHelper.CreateDirectory(k);
           });
           #endregion
           NDLibDtoModelHelper.GenerateData(interList, contextPath, responseBaseClassPath, responseInterPath, contextValidatorPath);

       }
        #endregion

        #region 生成ND.Web.Service
       public static void GenerateWebService(InterfaceList interList)
       {
           string DirectPath = System.Configuration.ConfigurationManager.AppSettings["ND.WebService.Service"];
           List<string> lstCreateDir = new List<string>();
           #region 先生成各个文件夹
           //1.先生成各个文件夹
           string corePath = Path.Combine(DirectPath, interList.FileName);//核心path
           lstCreateDir.Add(corePath);
           lstCreateDir.ForEach(k =>
           {
               DirFileHelper.CreateDirectory(k);
           });
           #endregion
           NDWebServiceHelper.GenerateCode(interList, corePath);
       }
        #endregion

        #region 生成ND.WebService.Contract
       public static void GenerateWebServiceContract(InterfaceList interList)
       {
           string DirectPath = System.Configuration.ConfigurationManager.AppSettings["ND.WebService.Contract"];
           List<string> lstCreateDir = new List<string>();
           #region 先生成各个文件夹
           //1.先生成各个文件夹
           string corePath = Path.Combine(DirectPath, interList.FileName);//核心path
           lstCreateDir.Add(corePath);
           lstCreateDir.ForEach(k =>
           {
               DirFileHelper.CreateDirectory(k);
           });
           #endregion
           NDContractHelper.GenerateCode(interList, corePath);
       }
       #endregion

        #region 生成ND.WebService.IISHost
       public static void GenerateWebServiceHost(InterfaceList interList)
       {
           string directPath = System.Configuration.ConfigurationManager.AppSettings["ND.WebService.IISHost"];
           List<string> lstCreateDir = new List<string>();
           #region 先生成各个文件夹
           //1.先生成各个文件夹
           string autoMapperPath = Path.Combine(directPath, "autoMapperConfiguration");//核心path
           string configPath = Path.Combine(directPath, "configFile");
           lstCreateDir.Add(autoMapperPath);
           lstCreateDir.Add(configPath);
           lstCreateDir.ForEach(k =>
           {
               DirFileHelper.CreateDirectory(k);
           });
           #endregion
           NDHostHelper.GenerateCode(interList, directPath);
       }
        #endregion
      

       
    }
}

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
   public class NDLibDtoModelHelper
    {
        #region 生成context上下文
        public static void GenerateData(InterfaceList interList,string contextPath,string responsePath,string responseInterPath,string contextValidatorPath)
        {
            string fileName = interList.FileName;
           interList.lstInterDis.ToList().ForEach(x =>
           {
               x.MethodDis.ToList().ForEach(k =>//遍历所有方法
               {
                  k.Request.ToList().ForEach(m =>//遍历所有参数
                  {
                      GenerateContext(m.RequestParamType, contextPath, fileName);//生成上下文对象
                      GenerateValidator(m.RequestParamType, contextValidatorPath, fileName);//生成验证器对象
                  });
                  GenerateResponse(k.Response.ResponseParamType, responsePath, responseInterPath, fileName);//生成响应类名和接口
               });
           });
        }
        #endregion

        #region 生成context上下文
        private static void GenerateContext(string requestParamType, string contextPath,string fileName)
        {
            if (requestParamType.IndexOf("Context") <= -1)//有上下文这种类型，就生成对应的context类
                return;
            string contextContent = FileHelper.ReadFile(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "template/ND.Lib.DtoModel/ContextTemplate.txt"));
            contextContent = contextContent.Replace("${FileName}", fileName).Replace("${ClassName}", requestParamType).Replace("${ValidatorClassName}", requestParamType+"Validator");
            FileHelper.WriteFile(Path.Combine(contextPath, "" + requestParamType + ".cs"), contextContent);
        }
        #endregion

        #region 生成验证器
        private static void GenerateValidator(string requestParamType, string validatorPath, string fileName)
        {
            if (requestParamType.IndexOf("Context") <= -1)//有上下文这种类型，就生成对应的context类
                return;
            string validatorContent = FileHelper.ReadFile(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "template/ND.Lib.DtoModel/ValidatorTemplate.txt"));
            validatorContent = validatorContent.Replace("${FileName}", fileName).Replace("${ClassName}", requestParamType).Replace("${ValidatorClassName}", requestParamType + "Validator");
            FileHelper.WriteFile(Path.Combine(validatorPath, "" + requestParamType + "Validator.cs"), validatorContent);
        }
        #endregion

        #region 生成相应类型对象
        private static void GenerateResponse(string responseType, string responsePath,string responseInterPath, string fileName)
        {
            if (responseType.SubResponse().IndexOf("Response") <= -1|| responseType.SubResponse().IndexOf("EmptyResponse") > -1)
                return;
            string responseContent = FileHelper.ReadFile(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "template/ND.Lib.DtoModel/ResponseTemplate.txt"));
            responseContent = responseContent.Replace("${FileName}", fileName).Replace("${ClassName}", responseType.SubResponse()).Replace("${InterfaceName}", "I" + responseType.SubResponse());
            FileHelper.WriteFile(Path.Combine(responsePath, "" + responseType.SubResponse() + ".cs"), responseContent);

            string responseInterContent = FileHelper.ReadFile(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "template/ND.Lib.DtoModel/ResponseInterTemplate.txt"));
            responseInterContent = responseInterContent.Replace("${FileName}", fileName).Replace("${InterfaceName}", "I" + responseType.SubResponse());
            FileHelper.WriteFile(Path.Combine(responseInterPath, "" + "I" + responseType.SubResponse() + ".cs"), responseInterContent);

        }
        #endregion
    }

 

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGeneration
{
    /// <summary>
    /// 请求参数描述
    /// </summary>
   public class RequestParamsDiscription
    {
       /// <summary>
       /// 请求类型
       /// </summary>
       public string RequestParamType { get; set; }

       /// <summary>
       /// 请求参数名称
       /// </summary>
       public string RequestParamName { get; set; }


       /// <summary>
       /// 请求参数描述
       /// </summary>
       public string RequestParamDis { get; set; }
    }
}

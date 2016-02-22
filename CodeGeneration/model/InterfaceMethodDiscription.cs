using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGeneration.model
{
   public class InterfaceMethodDiscription
    {
       public InterfaceMethodDiscription()
       {
           Request = new List<RequestParamsDiscription>();
           Response = new ResponseParamDiscription();
       }
        /// <summary>
        /// 接口方法描述
        /// </summary>
        public string InterfaceMethodDis { get; set; }

        /// <summary>
        /// 接口方法名称
        /// </summary>
        public string InterfaceMethodName { get; set; }

        /// <summary>
        /// 请求对象，key为请求类型 value为请求名称
        /// </summary>
        public List<RequestParamsDiscription> Request { get; set; }

        /// <summary>
        /// 响应类型
        /// </summary>
        public ResponseParamDiscription Response { get; set; }
    }
}

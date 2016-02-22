using CodeGeneration.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGeneration
{
    public class InterfaceDiscription
    {
        public InterfaceDiscription()
        {
            MethodDis = new List<InterfaceMethodDiscription>();
            InterUsings = new List<InterfaceUsingDiscription>();
        }

        public List<InterfaceUsingDiscription> InterUsings { get; set; } 

        /// <summary>
        /// 接口名称
        /// </summary>
        public string InterfaceName { get; set; }


        /// <summary>
        /// 接口描述
        /// </summary>
        public string InterfaceDis { get; set; }

        /// <summary>
        /// 接口方法描述
        /// </summary>
        public List<InterfaceMethodDiscription> MethodDis { get; set; }

      

        /// <summary>
        /// 命名空间名称
        /// </summary>
        public string NameSpaceName { get; set; }
    }
}

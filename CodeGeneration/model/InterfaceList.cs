using CodeGeneration.helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGeneration.model
{
    /// <summary>
    /// 接口列表
    /// </summary>
   public class InterfaceList
    {
       public InterfaceList()
       {
           lstInterDis = new List<InterfaceDiscription>();
       }
     public  List<InterfaceDiscription> lstInterDis { get; set; }
     /// <summary>
     /// 接口所在文件名
     /// </summary>
     public string FileName { get; set; }
    }

    public static class InterfaceListExtention
    {
        /// <summary>
        /// 生成接口列表描述
        /// </summary>
        /// <param name="interList"></param>
        /// <param name="dic"></param>
        /// <returns></returns>
        public static InterfaceList GenerateInterfaceList(this InterfaceList interList,Dictionary<string,string> dic)
        {
            InterfaceList interfaceList = new InterfaceList();
            foreach (KeyValuePair<string,string> item in dic)
            {
              InterfaceDiscription interDis =  CodeHelper.GetInterfaceDiscrption(item.Value);
              interfaceList.lstInterDis.Add(interDis);
            }
            return interfaceList;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;

namespace CodeGeneration.helper
{
   public static class ApiLogHelper
    {
        

        /// <summary>
        /// 保存日志
        /// </summary>
        /// <param name="_content">日志内容</param>
       public static void Save(string DirectPath, string fileName, string _content, bool isAppend = false)
        {

           DirectPath = DirectPath+"\\";
           string FullName = DirectPath + fileName + ".cs";

           if (!DirFileHelper.IsExistDirectory(DirectPath))//如果不存在则创建该目录
            {
                //System.IO.Directory.CreateDirectory(DirectPath);
                DirFileHelper.CreateDirectory(DirectPath);
            }
           if(!File.Exists(FullName))
           {
               FileHelper.FileCreate(FullName);
               FileHelper.WriteFile(FullName, _content);
           }
            //if (File.Exists(DirectPath + fileName + ".cs"))
            //{
            //   new FileHelper().WriteFile(DirectPath + fileName + ".cs", _content);
            //    //using (StreamWriter sw = new StreamWriter(DirectPath + fileName + ".cs", true))
            //    //{
            //    //    sw.WriteLine("\r\n");
            //    //    sw.WriteLine(_content);
            //    //    sw.Flush();
            //    //}
            //    //using (FileStream fs = new FileStream(DirectPath + fileName + ".cs", System.IO.FileMode.Open, System.IO.FileAccess.Read, FileShare.ReadWrite))
            //    //{
            //    //    using (StreamWriter sw = new StreamWriter(DirectPath + fileName + ".cs", true))
            //    //    {
            //    //        sw.WriteLine("\r\n");
            //    //        sw.WriteLine(_content);
            //    //        sw.Flush();
            //    //    }
            //    //}
            //}
            //else
            //{

            //    File.Create(DirectPath + fileName + ".cs");
            //   new FileHelper().WriteFile(DirectPath + fileName + ".cs", _content);
            //    //using (StreamWriter sw = new StreamWriter(DirectPath + fileName + ".cs", true))
            //    //{
            //    //    sw.WriteLine("\r\n");
            //    //    sw.WriteLine(_content);
            //    //    sw.Flush();
            //    //}
            //    //using (FileStream fs = File.Create(DirectPath + fileName + ".cs"))
            //    //{
            //    //    using (StreamWriter sw = new StreamWriter(fs))
            //    //    {

            //    //        sw.WriteLine(_content);
            //    //        sw.Close();
            //    //    }
            //    //}
            //}
        }
    }
}

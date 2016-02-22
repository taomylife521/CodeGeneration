using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CodeGeneration.extention
{
    public static class MyListExtension
    {
        public static void TryAddImgFile(this List<string> lst, string filePath)
        {
            string extension = Path.GetExtension(filePath).ToLower();
            if (extension == ".jpg" || extension == ".jpeg" || extension == ".gif" || extension == ".bmp" || extension == ".png")
            {
                lst.Add(filePath);
            }
           

        }
       
    }


    public static class MyDictionaryExtension
    {
        public static void AddFile(this Dictionary<string, Dictionary<string, string>> dic, string filePath)
        {
            string extension = Path.GetExtension(filePath).ToLower();
            FileInfo fInfo = new FileInfo(filePath);
            dic.TryAdd(extension,fInfo.Name,File.ReadAllText(filePath));
            

        }

        public static void TryAdd(this Dictionary<string, Dictionary<string, string>> dic, string key, string FileName,string FileContent)
        {
           
                if(dic.ContainsKey(key))
                {
                    dic[key].Add(FileName, FileContent);
                     
                }
                else
                {

                    Dictionary<string, string> dicNew = new Dictionary<string, string>();
                    dicNew.Add(FileName, FileContent);
                    dic.Add(key, dicNew);
                    
                }
                
           

        }

    }
}

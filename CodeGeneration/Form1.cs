using CodeGeneration.helper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CodeGeneration.extention;
using CodeGeneration.model;

namespace CodeGeneration
{
    public partial class Form1 : Form
    {

        List<RequestParamsDiscription> lstRequest = new List<RequestParamsDiscription>();
        List<InterfaceDiscription> lstInterface = new List<InterfaceDiscription>();
        Dictionary<string, string> dicInterface = new Dictionary<string, string>();
        public Form1()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
        }
        /// <summary>
        /// 打开接口路径
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOpenPath_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
         
            dialog.Description = "请选择文件路径";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
               this.txtFilePath.Text= dialog.SelectedPath;
                string path =  dialog.SelectedPath;
               Task.Factory.StartNew(() =>
               {
                     ISynchronizeInvoke synchronizer = this;
                     Dictionary<string, Dictionary<string, string>> allFileList = GetAllDirectory(path);
                   string fileExtention = "";
                   int sumFile = 0;
                   StringBuilder strDetail = new StringBuilder();
                   foreach (KeyValuePair<string, Dictionary<string, string>> item in allFileList)
                   {
                       fileExtention = item.Key == "" ? "未知类型" : item.Key;
                       strDetail.AppendLine("文件类型:" + fileExtention + ",数量:" + item.Value.Count);
                       if(item.Key==".cs")//cs文件
                       {
                           foreach (KeyValuePair<string,string> x in item.Value)
                           {
                               if (x.Key.Substring(0, 1).ToUpper() == "I")//是接口才会加入
                               {
                                   this.lstInterfaceList.Items.Add(x.Key.Replace(".cs", ""));
                                   dicInterface.Add(x.Key.Replace(".cs", ""), x.Value);//缓存key为接口名 value为代码
                               }
                           }
                              
                          
                       }
                       sumFile += item.Value.Count;
                   }
                   strDetail.AppendLine("文件总数量:" + sumFile.ToString());
                    this.lbFolderDetail.Text = strDetail.ToString();
               });
            }
        }

        /// <summary>
        /// 获取或设置控件的值，根据控件的Id与字典的key匹配值
        /// </summary>
        /// <param name="controlCollection">控件集合</param>
        /// <param name="dic">字典集合</param>
        private Dictionary<string, Dictionary<string,string>> GetAllDirectory(string directoryPath, bool isGetSubFolder = true)
        {

            Dictionary<string, Dictionary<string, string>> directoryList = new Dictionary<string, Dictionary<string, string>>();
            Directory.GetFiles(directoryPath).ToList().ForEach(x =>//先添加当前目录下的所有文件
            {
                directoryList.AddFile(x);
            });
            if (!isGetSubFolder)
            {
                return directoryList;
            }
            var directsName = Directory.GetDirectories(directoryPath);
            foreach (string item in directsName)
            {
                Dictionary<string, Dictionary<string,string>> dic = GetAllDirectory(item, isGetSubFolder);

                dic.ToList().ForEach(x =>
                {
                    directoryList.Add(x.Key, x.Value);
                });


            }
            return directoryList;




        }
        /// <summary>
        /// 选择接口的时候
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lstInterfaceList_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.txtInterfaceCode.Text = "";
            if(dicInterface.ContainsKey(this.lstInterfaceList.SelectedItem.ToString()))
            {
               
                string code = dicInterface[this.lstInterfaceList.SelectedItem.ToString()].ToString();
                 this.txtInterfaceCode.Text=code;
            }
        }

        /// <summary>
        /// 生成代码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGenerateCode_Click(object sender, EventArgs e)
        {
            try
            {
                InterfaceList interfaceList = new InterfaceList().GenerateInterfaceList(dicInterface);//生成接口描述列表
                string FileName = this.cmbFileList.SelectedItem == null ? "" : this.cmbFileList.SelectedItem.ToString();
                if (string.IsNullOrEmpty(FileName))
                {
                    MessageBox.Show("请先选择该接口列表所属的业务类型");
                    return;
                }
                if (MessageBox.Show("确定该接口列表所属的业务类型为：" + FileName + "吗?", "提示", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.Cancel)
                {
                    return;
                }
                interfaceList.FileName = FileName;
                // XmlHelper.XmlSerializeToFile(interfaceList, "f:/vv/aa.xml", Encoding.UTF8);
                //CodeHelper.GenerateService(interfaceList);
                //CodeHelper.GeneratePlugin(interfaceList);
                CodeHelper.GenerateCore(interfaceList);
                CodeHelper.GenerateDtoModel(interfaceList);
                CodeHelper.GenerateNDPlugin(interfaceList);
                CodeHelper.GenerateWebService(interfaceList);
                CodeHelper.GenerateWebServiceContract(interfaceList);
                CodeHelper.GenerateWebServiceHost(interfaceList);
                MessageBox.Show("生成成功");
            }
            catch(Exception ex)
            {
                MessageBox.Show("生成失败！原因:" + ex.Message + "\r\n" + ex.InnerException);
            }
            
            //生成服务层的代码
            //ND.WebService.Service
            //生成插件层的代码
            //生成核心层的代码
        }


    
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Microsoft.Win32;
using System.Xml;
using FishRandomSelector.Tools;
using System.IO;

namespace FishRandomSelector.Views
{
    /// <summary>
    /// FirstUse.xaml 的交互逻辑
    /// </summary>
    public partial class FirstUse : Window
    {
        public FirstUse()
        {
            InitializeComponent();
        }

        

        private void DoFirstUseWorks(object sender, RoutedEventArgs e)
        {
            Thread loadthread = new Thread(DoWork_OtherThread);
            loadthread.Start();
        }
        private void DoWork_OtherThread()
        {
            Thread.Sleep(1000);
            XmlElement root = FishXmlWriter.CreateRootElement("FishRandomSelectorConfig");
            FishXmlWriter.CreateXmlDeclaration("1.0", "UTF-8", null);
            FishXmlWriter.SavaXml("FishRandomSelector.xml");
            FishXmlWriter.ClearXml();
            Page2NextButton.Dispatcher.Invoke(() => { Page2NextButton.IsEnabled = true; });
        }
        private void AddAttribute(XmlElement root, string name, string value)
        {
            XmlNode node = FishXmlWriter.CreateElement("Config", " ");
            FishXmlWriter.AppendAttributeToElement((XmlElement)node, "name", name);
            FishXmlWriter.AppendAttributeToElement((XmlElement)node, "value", value);
            FishXmlWriter.AppendChild(root, node);
        }
        private void PickFile(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.DefaultExt = ".txt";
            fileDialog.Filter = "文本文件 |*.txt|无类型文本 |*.*|Fish名称名单 |*.fnamelist";
            fileDialog.CheckFileExists = true;
            fileDialog.Multiselect = false;
            Nullable<bool> result = fileDialog.ShowDialog();
            if (result == true)
            {
                NextButton.IsEnabled = true;
                Filepath.Text = fileDialog.FileName.ToString();
                FileStream file = null ;
                StreamReader filereader = null ;
                try
                {
                    file = new FileStream(fileDialog.FileName.ToString(), FileMode.Open);
                    filereader = new StreamReader(file);
                    filecontext.Text = filereader.ReadToEnd();
                }
                catch(Exception ex)
                {
                    filecontext.Text = ex.Message;
                }
                finally
                {
                    filereader?.Close();
                    file?.Close();
                }
            }
        }
        private void PutNameList()
        {
            string namelist = null;
            filecontext.Dispatcher.Invoke(() => { namelist =(string) filecontext.Text.ToString().Clone(); });
            string[] names = namelist.Split(new char[2] { ' ','\n' });
            XmlElement root = FishXmlWriter.CreateRootElement("FishRandomSelectorNameList");
            FishXmlWriter.CreateXmlDeclaration("1.0", "UTF-8", null);
            foreach(string name in names)
            {
                if(name!=null)
                {
                    PutAName(name, root);
                }
            }
        }
        private void PutAName(string name , XmlElement root)
        {
            XmlNode aname = FishXmlWriter.CreateElement("Name", "");
            FishXmlWriter.AppendAttributeToElement((XmlElement)aname, "name", name);
            FishXmlWriter.AppendChild(root, aname);
            FishXmlWriter.SavaXml("FishRandomSelectorNameList.xml");
        }

        private void PutName(object sender, RoutedEventArgs e)
        {
            PutNameList();
        }
    }
}

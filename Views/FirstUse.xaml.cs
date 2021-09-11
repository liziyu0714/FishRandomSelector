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
            LICENSE.MaxWidth /= 2;
            EULA.MaxWidth /= 2;
            LICENSE.Text = Info.Info.LICENSE;
            EULA.Text = Info.Info.EULA;
        }



        private void DoFirstUseWorks(object sender, RoutedEventArgs e)
        {
            Thread loadthread = new Thread(DoWork_OtherThread);
            loadthread.Start();
        }
        private void DoWork_OtherThread()
        {
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
                FileStream file = null;
                StreamReader filereader = null;
                try
                {
                    file = new FileStream(fileDialog.FileName.ToString(), FileMode.Open);
                    filereader = new StreamReader(file);
                    filecontext.Text = filereader.ReadToEnd();
                }
                catch (Exception ex)
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
            filecontext.Dispatcher.Invoke(() => { namelist = (string)filecontext.Text.ToString().Clone(); });
            string[] names = namelist.Split(new char[2] { ' ', '\n' });
            XmlElement root = FishXmlWriter.CreateRootElement("FishRandomSelectorNameList");
            FishXmlWriter.CreateXmlDeclaration("1.0", "UTF-8", null);
            foreach (string name in names)
            {
                if (name != null)
                {
                    PutAName(name, root);
                }
            }
            FishXmlWriter.SavaXml("FishRandomSelectorNameList.xml");
            FishXmlWriter.ClearXml();
        }
        private void PutAName(string name, XmlElement root)
        {
            XmlNode aname = FishXmlWriter.CreateElement("Name", "");
            FishXmlWriter.AppendAttributeToElement((XmlElement)aname, "name", name);
            FishXmlWriter.AppendAttributeToElement((XmlElement)aname, "value", "50");
            FishXmlWriter.AppendChild(root, aname);
        }

        private void PutName(object sender, RoutedEventArgs e)
        {
            if (filecontext.Text != "")
                PutNameList();
            else
                MessageBox.Show(this, "请选择文件或手动输入", "FishRandomSelector", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        private void FinishFirstUseWorks(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            Application.Current.MainWindow = main;
            Application.Current.MainWindow.Show();
            try
            {
                FishRandomSelector.core.Info.Names.ReadPeople();
            }
            catch { }
            this.Close();
        }
    }
}

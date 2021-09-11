using FishRandomSelector.Tools;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;

namespace FishRandomSelector.core.Views
{
    /// <summary>
    /// Inoutput.xaml 的交互逻辑
    /// </summary>
    public partial class Inoutput : Page
    {
        public Inoutput()
        {
            InitializeComponent();
        }

        private void Buttons_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            App app = (App)Application.Current;
            Button p = (Button)sender;
            switch (p.Name[7])
            {
                case '1':
                    app.IsReset = true;
                    File.Delete("FishRandomSelector.xml");
                    File.Delete("FIshRandomSelectorNameList.xml");
                    MessageBox.Show("已重置，请手动重启FishRandomSelector");
                    Application.Current.Shutdown();
                    break;
                case '2':
                    fileDialog.DefaultExt = ".txt";
                    fileDialog.Filter = "文本文件 |*.txt|无类型文本 |*.*|Fish名称名单 |*.fnamelist";
                    fileDialog.CheckFileExists = true;
                    fileDialog.Multiselect = false;
                    break;
                case '3':
                    fileDialog.Filter = "配置好的文件|FishRandomSelectorNameList.xml";
                    fileDialog.CheckFileExists = true;
                    fileDialog.Multiselect = false;
                    break;
                case '4':
                    fileDialog.Filter = "配置文件|FishRandomSelector.xml";
                    fileDialog.CheckFileExists = true;
                    fileDialog.Multiselect = false;
                    break;
                case '5':
                    saveFileDialog.DefaultExt = ".txt";
                    saveFileDialog.Filter = "文本文件 |*.txt|无类型文本 |*.*|Fish名称名单 |*.fnamelist";
                    Nullable<bool> fileresult = saveFileDialog.ShowDialog();
                    if (fileresult == true)
                    {
                        File.Create(saveFileDialog.FileName);
                        StreamWriter writer = File.AppendText(saveFileDialog.FileName);
                        foreach (FishRandomSelector.core.Info.person aperson in FishRandomSelector.core.Info.Names.GetAllPeople())
                        {
                            writer.Write(aperson.Name + " ");
                        }
                    }
                    return;
                case '6':
                    saveFileDialog.Filter = "配置好的文件|FishRandomSelectorNameList.xml";
                    saveFileDialog.DefaultExt = "FishRandomSelectorNameList.xml";
                    Nullable<bool> fileresult_2 = saveFileDialog.ShowDialog();
                    if (fileresult_2 == true)
                    {
                        try
                        {
                            File.Copy("FishRandomSelectorNameList.xml", saveFileDialog.FileName);
                        }
                        catch { }
                    }
                    return;
                case '7':
                    app.IsReset = true;
                    File.Delete("FishRandomSelector.xml");
                    MessageBox.Show("已重置，请手动重启FishRandomSelector");
                    Application.Current.Shutdown();
                    return;
                case '8':
                    saveFileDialog.Filter = "配置文件|FishRandomSelector.xml";
                    saveFileDialog.DefaultExt = "FishRandomSelector.xml";
                    Nullable<bool> fileresult_3 = saveFileDialog.ShowDialog();
                    if (fileresult_3 == true)
                    {
                        File.Copy("FishRandomSelector.xml", saveFileDialog.FileName);
                    }
                    return;
            }
            Nullable<bool> result = fileDialog.ShowDialog();
            if (result == true) PutNameList(fileDialog.FileName.ToString());
        }
        private void PutNameList(string filename)
        {
            string namelist = null;
            namelist = File.ReadAllText(filename);
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
    }
}

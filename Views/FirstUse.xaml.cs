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
using System.Xml;
using FishRandomSelector.Tools;

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
            AddAttribute(root, "Testname", "Testvalue");
            FishXmlWriter.CreateXmlDeclaration("1.0", "UTF-8", null);
            FishXmlWriter.SavaXml("FishRandomSelector.xml");
            Page2NextButton.Dispatcher.Invoke(() => { Page2NextButton.IsEnabled = true; });
        }
        private void AddAttribute(XmlElement root, string name, string value)
        {
            XmlNode node = FishXmlWriter.CreateElement("Config", " ");
            FishXmlWriter.AppendAttributeToElement((XmlElement)node, "name", name);
            FishXmlWriter.AppendAttributeToElement((XmlElement)node, "value", value);
            FishXmlWriter.AppendChild(root, node);
        }
    }
}

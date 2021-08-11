using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FishRandomSelector.Tools;
using System.Xml;

namespace FishRandomSelector.Settings
{
    static class UISettings
    {
        /// <value>
        /// <c>CloseLeftAreaWhenClick</c>
        /// 是否打开侧边栏
        /// </value>
        public static bool CloseLeftAreaWhenClick = true;
        ///<value>
        ///<c>AlwaysSaveWhenEndEit</c>
        ///PeopleView中编辑结束后立即保存
        ///</value>
        public static bool AlwaysSaveWhenEndEit = true;
        ///<value>
        ///<c>UseAnimation</c>
        ///抽取时使用动画
        ///</value>
        public static bool UseAnimation = true;
        ///<value>
        ///<c>UseSystemDefaultErrorDialog</c>
        ///出错时使用系统对话框
        ///</value>
        public static bool UseSystemDefaultErrorDialog = true;
        public static int TimeBetweenChangeText =100;
        public static int MaxChangeTextTimes = 20;
        public static int MinChangeTextTimes = 2;
        public static void ReadConfig()
        {
            if (!System.IO.File.Exists("FishRandomSelector.xml")) throw new System.IO.FileNotFoundException("没有FishRandomSelector.xml文件");
            FishXmlReader.LoadXml("FishRandomSelector.xml");
            XmlNode root = FishXmlReader.GetXmlDocumentRoot();
            XmlNodeList nodeList = root.ChildNodes;
            foreach (XmlNode anode in nodeList)
            {

                switch(anode.Name)
                {
                    case "CloseLeftAreaWhenClick": CloseLeftAreaWhenClick = bool.Parse(anode.Attributes.GetNamedItem("value").Value); break;
                    case "AlwaysSaveWhenEndEit": AlwaysSaveWhenEndEit = bool.Parse(anode.Attributes.GetNamedItem("value").Value); break;
                    case "UseAnimation": UseAnimation = bool.Parse(anode.Attributes.GetNamedItem("value").Value); break;
                    case "UseSystemDefaultErrorDialog": UseSystemDefaultErrorDialog = bool.Parse(anode.Attributes.GetNamedItem("value").Value); break;
                    case "TimeBetweenChangeText": TimeBetweenChangeText = int.Parse(anode.Attributes.GetNamedItem("value").Value); break;
                    case "MaxChangeTextTimes": MaxChangeTextTimes = int.Parse(anode.Attributes.GetNamedItem("value").Value); break;
                    case "MinChangeTextTimes": MinChangeTextTimes = int.Parse(anode.Attributes.GetNamedItem("value").Value); break;
                }
            }
        }
        public static void SaveConfig()
        {
            XmlNode root =  FishXmlWriter.CreateRootElement("FishRandomSelectorConfig");
            FishXmlWriter.CreateXmlDeclaration("1.0", "UTF-8", null);
            PutAConfig("CloseLeftAreaWhenClick", CloseLeftAreaWhenClick.ToString(), root);
            PutAConfig("AlwaysSaveWhenEndEit", AlwaysSaveWhenEndEit.ToString(), root);
            PutAConfig("UseAnimation", UseAnimation.ToString(), root);
            PutAConfig("UseSystemDefaultErrorDialog", UseSystemDefaultErrorDialog.ToString(), root);
            PutAConfig("TimeBetweenChangeText", TimeBetweenChangeText.ToString(), root);
            PutAConfig("MaxChangeTextTimes", MaxChangeTextTimes.ToString(), root);
            PutAConfig("MinChangeTextTimes", MinChangeTextTimes.ToString(), root);
            FishXmlWriter.SavaXml("FishRandomSelector.xml");
            FishXmlWriter.ClearXml();
        }
        public static void PutAConfig(string name , string value , XmlNode root)
        {
            XmlNode aConfig = FishXmlWriter.CreateElement(name, "");
            FishXmlWriter.AppendAttributeToElement((XmlElement)aConfig, "value", value);
            FishXmlWriter.AppendChild(root, aConfig);
        }
    }
}

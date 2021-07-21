using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace FishRandomSelector.Tools
{
    public class FishXmlWriter
    {
        static XmlDocument doc = new XmlDocument();

        /// <summary>
        /// 创建根节点
        /// </summary>
        /// <param name="xn">根节点的名称</param>
        /// <returns>返回根节点</returns>
        public static XmlElement CreateRootElement(string xn)
        {
            XmlElement xe = doc.CreateElement(xn);
            doc.AppendChild(xe);
            return xe;
        }
        /// <summary>
        /// 添加一个Attribute到一个XmlElement元素
        /// </summary>
        /// <param name="xe">被添加的XmlElement元素</param>
        /// <param name="attrname">Attribute的特证名</param>
        /// <param name="attrtext">Attribute的值</param>
        public static void AppendAttributeToElement(XmlElement xe, string attrname, string attrtext)
        {
            XmlAttribute xa = CreateAttribute(attrname, attrtext);
            xe.Attributes.Append(xa);
        }
        /// <summary>
        /// 添加一个Attribute到一个XmlElement元素
        /// </summary>
        /// <param name="xe">被添加的XmlElement元素</param>
        /// <param name="attr">Attribute对象</param>
        public void AppendAttributeToElement(XmlElement xe, XmlAttribute attr)
        {

            XmlAttribute xa = attr;
            xe.Attributes.Append(xa);
        }
        /// <summary>
        /// 创建一个Attribute对象
        /// </summary>
        /// <param name="attrname">Attribute的特证名</param>
        /// <param name="attrtext">Attribute的值</param>
        /// <returns>返回创建的Attribute对象</returns>
        public static XmlAttribute CreateAttribute(string attrname, string attrtext)
        {
            XmlAttribute xa = doc.CreateAttribute(attrname);
            xa.InnerText = attrtext;
            return xa;
        }
        /// <summary>
        /// 创建一个具有指定名称和值的节点
        /// </summary>
        /// <param name="name">XmlNode的名称</param>
        /// <param name="text">XmlNode的值</param>
        /// <returns>返回XmlNode对象</returns>
        public static XmlNode CreateElement(string name, string text)
        {
            XmlNode xn = (XmlNode)doc.CreateElement(name);   //创建具有指定名称的元素并转换成节点
            xn.InnerText = text;  //获取节点中的Text 内容控件中的名字
            return xn;
        }
        /// <summary>
        /// 添加新节点到旧节点之后
        /// </summary>
        /// <param name="oldxn">旧节点</param>
        /// <param name="newxn">新节点</param>
        public static void XmlInsertAfter(XmlNode oldxn, XmlNode newxn)
        {
            XmlNode parent = oldxn.ParentNode; //获取被添加节点的父节点
            parent.InsertAfter(newxn, oldxn); //添加新节点到旧节点之后
        }
        /// <summary>
        /// 添加新节点到旧节点之前
        /// </summary>
        /// <param name="oldxn">旧节点</param>
        /// <param name="newxn">新节点</param>
        public static void XmlInsertBefore(XmlNode oldxn, XmlNode newxn)
        {
            XmlNode parent = oldxn.ParentNode; //获取被添加节点的父节点
            parent.InsertBefore(newxn, oldxn);//添加新节点到旧节点之前
        }
        /// <summary>
        /// 添加子节点到指定节点中
        /// </summary>
        /// <param name="parentnode">指定的父节点</param>
        /// <param name="childnode">要添加进的子节点</param>
        public static void AppendChild(XmlNode parentnode, XmlNode childnode)
        {
            parentnode.AppendChild(childnode);
        }

        /// <summary>
        /// 创建Xml文件声明节点,必须要调用此方法
        /// </summary>
        /// <param name="version">版本号，必须为1.0</param>
        /// <param name="encoding">字符编码</param>
        /// <param name="standalone">独立特性的值</param>
        public static void CreateXmlDeclaration(string version, string encoding, string standalone)
        {
            XmlDeclaration xd;
            xd = doc.CreateXmlDeclaration(version, encoding, standalone);  //创建声明节点
            if (doc.ChildNodes == null)  //如果存在根节点
            {
                doc.AppendChild(xd);  //添加根节点
            }
            else
            {
                XmlElement root = doc.DocumentElement;  //获取文档的根节点
                doc.RemoveAll();  //移除所有节点
                doc.AppendChild(xd);  //添加声明节点
                doc.AppendChild(root);   ///添加根节点

            }
        }
        /// <summary>
        /// 移除指定的节点
        /// </summary>
        /// <param name="childnode"></param>
        /// <returns>返回移除结果</returns>
        public static bool RemoveChildNode(XmlNode childnode)
        {
            try
            {
                XmlNode parentnode = childnode.ParentNode;  //获取父节点
                parentnode.RemoveChild(childnode);   //移除父节点下的指定子节点
                return true;
            }
            catch
            {
                return false;
            }

        }
        /// <summary>
        /// 移除所有的节点
        /// </summary>
        /// <param name="xmlnode">节点名称</param>
        /// <returns></returns>
        public static bool RemoveChildAllNode(XmlNode xmlnode)
        {
            xmlnode.RemoveAll();   //移除所有节点
            if (xmlnode.ChildNodes.Count == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 选择具有指定名称的一个节点
        /// </summary>
        /// <param name="xn">父节点对象</param>
        /// <param name="xname">要查找节点的Name</param>
        /// <returns>返回查找结果</returns>
        public static XmlNode SelectSingleNode(XmlNode xn, string xname)
        {
            return xn.SelectSingleNode(xname);
        }
        /// <summary>
        /// 选择具有指定名称的多个节点
        /// </summary>
        /// <param name="xn">父节点对象</param>
        /// <param name="xname">要查找节点的Name</param>
        /// <returns>返回查找结果</returns>
        public static XmlNodeList SelectNodes(XmlNode xn, string xname)
        {
            return xn.SelectNodes(xname);
        }
        /// <summary>
        /// 移除节点中指定的Attribute
        /// </summary>
        /// <param name="xn">XmlNode对象</param>
        /// <param name="xan">Attribute的名称</param>
        /// <returns>返回移除结果</returns>
        public static bool RemoveXmlAttribute(XmlNode xn, string xan)
        {
            int ac = xn.Attributes.Count;
            XmlNode xmlnode = xn.Attributes.RemoveNamedItem(xan);
            if (xmlnode.Attributes.Count == ac - 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 移除节点中所有的Attribute
        /// </summary>
        /// <param name="xn">XmlNode对象</param>
        /// <returns>返回移除结果</returns>
        public static bool RemoveXmlAttribute(XmlNode xn)
        {
            xn.Attributes.RemoveAll();
            if (xn.Attributes.Count == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 替换节点的值
        /// </summary>
        /// <param name="xn">XmlNode对象</param>
        /// <param name="text">新的XmlNode的值</param>
        public static void ReplaceText(XmlNode xn, string text)
        {
            xn.InnerText = text;
        }
        /// <summary>
        /// 替换指定节点
        /// </summary>
        /// <param name="nxn">新节点</param>
        /// <param name="oxn">正在被替换的节点</param>
        /// <returns>返回新节点</returns>
        public static XmlNode ReplaceChild(XmlNode nxn, XmlNode oxn)
        {
            return doc.ReplaceChild(nxn, oxn);
        }
        /// <summary>
        /// 保存Xml文件
        /// </summary>
        public static void SavaXml(string url)
        {
            doc.Save(url);
        }
    }
    public class FishXmlReader
    {
        private static XmlDocument doc = new XmlDocument();
        private static string fileurl;
        //文件路径
        public static string FileUrl
        {
            set { fileurl = value; }
            get { return fileurl; }
        }
        /// <summary>
        /// 载入Xml文件
        /// </summary>
        /// <param name="url">Xml文件路径</param>
        public static void LoadXml(string url)
        {
            doc.Load(url);
        }
        /// <summary>
        /// 获取XmlDocument的根节点
        /// </summary>
        /// <returns>返回的XmlElement元素根节点</returns>
        public static XmlElement GetXmlDocumentRoot()
        {
            return doc.DocumentElement;
        }
        /// <summary>
        /// 获取指定元素的指定Attribute值
        /// </summary>
        /// <param name="xe">表示一个XmlElement</param>
        /// <param name="attr">表示Attribute的名字</param>
        /// <returns>返回获取的Attribute的值</returns>
        public static string GetAttribute(XmlElement xe, string attr)
        {
            return xe.GetAttribute(attr);
        }
        /// <summary>
        /// 获取指定节点的指定Attribute值
        /// </summary>
        /// <param name="xn">表示一个XmlNode</param>
        /// <param name="attr"></param>
        /// <returns>返回获取的Attribute的值</returns>
        public static string GetAttribute(XmlNode xn, string attr)
        {
            XmlElement xe = ExchangeNodeElement(xn);
            return xe.GetAttribute(attr);
        }
        /// <summary>
        /// XmlElement对象转换成XmlNode对象
        /// </summary>
        /// <param name="xe">XmlElement对象</param>
        /// <returns>返回XmlNode对象</returns>
        public static XmlNode ExchangeNodeElement(XmlElement xe)
        {
            return (XmlNode)xe;
        }
        /// <summary>
        /// XmlNode对象转换成XmlElement对象
        /// </summary>
        /// <param name="xe">XmlNode对象</param>
        /// <returns>返回XmlElement对象</returns>
        public static XmlElement ExchangeNodeElement(XmlNode xn)
        {
            return (XmlElement)xn;
        }
        /// <summary>
        /// 获取节点的文本
        /// </summary>
        /// <param name="xn"></param>
        /// <param name="nodename">节点的名称</param>
        /// <returns></returns>
        public static string GetXmlNodeInnerText(XmlNode xn, string nodename)
        {
            XmlNode childxn = xn.SelectSingleNode(nodename);
            return childxn.InnerText;
        }
        /// <summary>
        /// 获取指定节点的子节点
        /// </summary>
        /// <param name="xn">节点对象</param>
        /// <returns>返回子节点数</returns>
        public static int GetXmlNodeCount(XmlNode xn)
        {
            return xn.ChildNodes.Count;
        }
        /// <summary>
        /// 获取元素的文本
        /// </summary>
        /// <param name="xn">XmlElement元素</param>
        /// <param name="nodename">元素的名称</param>
        /// <returns></returns>
        public static string GetXmlElementInnerText(XmlElement xn, string nodename)
        {

            XmlNode childxn = xn.SelectSingleNode(nodename);
            return childxn.InnerText;
        }
        /// <summary>
        /// 获取XmlNode是否具有指定Attribute值
        /// </summary>
        /// <param name="xn">XmlNode对象</param>
        /// <param name="attr">Attribute的名称</param>
        /// <param name="compare">Attribute的值</param>
        /// <returns>返回bool值</returns>
        public static bool GetXmlNodeByArrtibute(XmlNode xn, string attr, string compare)
        {
            if (GetAttribute(xn, attr) == compare)
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// 获取XmlElement是否具有指定Attribute值
        /// </summary>
        /// <param name="xn">XmlElement对象</param>
        /// <param name="attr">Attribute的名称</param>
        /// <param name="compare">Attribute的值</param>
        /// <returns>返回bool值</returns>
        public static bool GetXmlNodeByArrtibute(XmlElement xe, string attr, string compare)
        {
            if (GetAttribute(xe, compare) == attr)
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// 获取一个具有指定Attrtibute的XmlNode子节点
        /// </summary>
        /// <param name="xn">XmlNode对象</param>
        /// <param name="attr">Attrtibute的名称</param>
        /// <param name="compare">Attrtibute的值</param>
        /// <returns>返回相应的子节点</returns>
        public static XmlNode GetXmlChildNodeByAttrtibute(XmlNode xn, string attr, string compare)
        {
            foreach (XmlNode cxn in xn.ChildNodes)
            {
                if (GetXmlNodeByArrtibute(cxn, attr, compare))
                {
                    return cxn;
                }
            }
            return null;
        }
    }
}

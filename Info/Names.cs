using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using FishRandomSelector.Tools;

namespace FishRandomSelector.core.Info
{
    public class person
    {
        private string name;
        private int value;
        private int _ID;

        public string Name { get => name; set => name = value; }
        public int Value { get => value; set => this.value = value; }
        public int ID { get => _ID; set => _ID = value; }
        public person(string _name , int _value , int thisID)
        {
            name = _name;
            value = _value; 
            _ID = thisID;
        }
    }
    public static class Names
    {
        private static int _size = 0;
        private static List<person> people = new List<person>();
        public static int size
        {
            get => _size;
        }
        private static void CheckPos(int pos)
        {
            if(pos >= _size)
            throw new ArgumentException("FishRandomSelector.core.Info.Names:pos超出范围");
        }
        public static person GetPerson(int pos)
        {
            CheckPos(pos);
            return people[pos];
        }
        public static void SetPersonValue(int pos , int value)
        {
            CheckPos(pos);
            people[pos].Value = value;
        }
        public static void AddPersonValue(int pos , int valuetoadd)
        {
            CheckPos(pos);
            people[pos].Value += valuetoadd;
        }
        public static void ReadPeople()
        {
            int  valueInList;
            string nameInList;
            if (!System.IO.File.Exists("FishRandomSelectorNameList.xml")) throw new System.IO.FileNotFoundException("没有FishRandomSelectorNameList.xml文件");
            FishXmlReader.LoadXml("FishRandomSelectorNameList.xml");
            XmlNode node = FishXmlReader.GetXmlDocumentRoot();
            XmlNodeList nodeList = node.ChildNodes;
            foreach (XmlNode anode in nodeList)
            {

                nameInList =  anode.Attributes.GetNamedItem("name").Value;
                valueInList = int.Parse( anode.Attributes.GetNamedItem("value").Value );
                people.Add(new person(nameInList, valueInList,_size));
                _size++;
            }
        }
        public static void SavePeople()
        {

        }
    }
}

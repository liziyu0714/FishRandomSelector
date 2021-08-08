using FishRandomSelector.Tools;
using System;
using System.Collections.Generic;
using System.Xml;

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
        public person(string _name, int _value, int thisID)
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
        private static List<bool> IsSelected = new List<bool>();
        public static void AddPerson(string name , int value)
        {
            people.Add(new person(name, value, ++_size));
        }
        public static void DeletePerson(int pos)
        {
            CheckPos(pos);
            people.RemoveAt(pos);
            _size--;
        }
        public static void ClearIsSelected()
        {
            for (int i = 0; i < _size; i++)
                IsSelected[i] = false;
        }
        public static string GetARandomName()
        {
            Random random = new Random();
            return people[random.Next(0, _size)].Name;
        }
        public static string GetPersonByValue()
        {
            Random random = new Random();
            while (true)
            {
                if(IsAllSelected())
                {
                    return "名单全部选择完毕,请清空";
                }
                for (int i = 0; i < _size; i++)
                {
                    int ARandomValue = random.Next(0, 100);
                    if (ARandomValue <= people[i].Value && IsSelected[i] != true)
                    {
                        IsSelected[i] = true;
                        return people[i].Name;
                    }
                }
            }
        }
        public static bool IsAllSelected ()
        {
            foreach(bool p in IsSelected)
            {
                if (p == false) return false;
            }
            return true;
        }
        public static List<person> GetPeopleList(int num)
        {
            Random random = new Random();
            List<person> p = new List<person>();
            int now = 0;
            while (now < num)
            {
                if (IsAllSelected())
                {
                    return p;
                }
                for (int i = 0; i < _size; i++)
                {
                    int ARandomValue = random.Next(0, 100);
                    if (ARandomValue <= people[i].Value && IsSelected[i] != true)
                    {
                        IsSelected[i] = true;
                        p.Add(people[i]);
                        now++;
                    }
                }
            }
            return p;
        }
        public static int size
        {
            get => _size;
        }
        private static void CheckPos(int pos)
        {
            if (pos > _size)
                throw new ArgumentException("FishRandomSelector.core.Info.Names:pos超出范围");
        }
        public static person GetPerson(int pos)
        {
            CheckPos(pos);
            return people[pos];
        }
        public static void SetPersonValue(int pos, int value)
        {
            CheckPos(pos);
            people[pos].Value = value;
        }
        public static void AddPersonValue(int pos, int valuetoadd)
        {
            CheckPos(pos);
            people[pos].Value += valuetoadd;
        }
        public static void ReadPeople()
        {
            int valueInList;
            string nameInList;
            if (!System.IO.File.Exists("FishRandomSelectorNameList.xml")) throw new System.IO.FileNotFoundException("没有FishRandomSelectorNameList.xml文件");
            FishXmlReader.LoadXml("FishRandomSelectorNameList.xml");
            XmlNode node = FishXmlReader.GetXmlDocumentRoot();
            XmlNodeList nodeList = node.ChildNodes;
            foreach (XmlNode anode in nodeList)
            {

                nameInList = anode.Attributes.GetNamedItem("name").Value;
                valueInList = int.Parse(anode.Attributes.GetNamedItem("value").Value);
                people.Add(new person(nameInList, valueInList, _size));
                IsSelected.Add(false);
                _size++;
            }
        }
        public static void SavePeople()
        {
            XmlNode rootnode = FishXmlWriter.CreateRootElement("FishRandomSelectorNameList");
            FishXmlWriter.CreateXmlDeclaration("1.0", "UTF-8", null);
            foreach (person a in people)
            {
                PutAName(a.Name, a.Value, (XmlElement)rootnode);
            }
            FishXmlWriter.SavaXml("FishRandomSelectorNameList.xml");
            FishXmlWriter.ClearXml();
        }
        private static void PutAName(string name, int value, XmlElement root)
        {
            XmlNode aname = FishXmlWriter.CreateElement("Name", "");
            FishXmlWriter.AppendAttributeToElement((XmlElement)aname, "name", name);
            FishXmlWriter.AppendAttributeToElement((XmlElement)aname, "value", value.ToString());
            FishXmlWriter.AppendChild(root, aname);
        }
    }
}

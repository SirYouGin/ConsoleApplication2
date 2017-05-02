﻿using System;
using System.Collections.Generic;
using System.Xml;
using System.Text;
using System.Threading.Tasks;

using ConsoleApplication2.Interfaces;

namespace ConsoleApplication2.Implementations
{
    public static class ElementFactory
    {
        private static readonly Dictionary<string, Type> wellKnownElements = new Dictionary<string, Type>() { 
         { "switch_app", typeof(ConsoleApplication2.Elements.switchElement)} //Переключить контекст
        ,{ "mt_set_var", typeof(ConsoleApplication2.Elements.mtSetVarElement)} //Установать переменную
        ,{ "mt_test_exec", typeof(ConsoleApplication2.Elements.mtTestExecElement)} //Выполнить тест
        ,{ "mt_save_result", typeof(ConsoleApplication2.Elements.mtSaveResultElement)} //Сохранить результат
        };

        public static IElement makeElement(IBlock owner, XmlNode parent)
        {
            string Name = String.Empty;
            string Id = String.Empty;
            
            Dictionary<string, string> dict = new Dictionary<string, string>();
            
            XmlAttributeCollection attrib = parent.Attributes;
            foreach (XmlAttribute a in attrib)
            {
                switch (a.Name)
                {
                    case "name": Name = a.Value; break;
                    case "id": Id = a.Value; break;
                }
            }

            if (String.IsNullOrEmpty(Name)) throw new ArgumentNullException("Не задано имя элемента");
            if (!wellKnownElements.ContainsKey(Name)) throw new NotImplementedException(String.Format("Неизвестное имя элемента \"{0}\"",Name));

            Type type = wellKnownElements[Name];

            object instance = Activator.CreateInstance(type);
            Element elem = instance as Element;
            elem.Name = Name;
            elem.Id = Id;
            
            foreach (XmlNode child in parent.ChildNodes)
            {
                XmlAttributeCollection pars = child.Attributes;
                XmlAttribute name = pars["name"];
                XmlAttribute value = pars["value"];
                dict.Add(name.Value, value.Value);
            }

            elem.Initialize(owner, dict);
            
            return elem;
        }
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Text;
using System.Threading.Tasks;

using ConsoleApplication2.Events;
using ConsoleApplication2.Interfaces;

namespace ConsoleApplication2.Implementations
{
    class Block : Element, IBlock
    {
        private XmlNode node;
        private readonly HashSet<IElement> elementList = new HashSet<IElement>();
        public Block(XmlNode parent)
        {
            node = parent;
        }
        public int Count
        {
            get
            {
                return elementList.Count;
            }
        }

        public override void Execute()
        {
            foreach (IElement elem in elementList)
            {
                try
                {
                    OnElementStart(elem);
                    elem.Execute();
                    OnElementFinish(elem);
                }
                catch(ElementException e)
                {
                    OnElementError(elem, e);
                }
                catch (Exception e)
                {
                    throw new BlockException("Block exception", e);
                }
            }
        }

        public IEnumerator<IElement> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public override void Initialize(IElement _parent, IDictionary<string, string> _params)
        {
            string Name = String.Empty;            

            Dictionary<string, string> dict;

            base.Initialize(_parent, _params);
            foreach (XmlNode child in node.ChildNodes)
            {
                Name = String.Empty;
                Id = String.Empty;

                dict = new Dictionary<string, string>();

                XmlAttributeCollection attrib = child.Attributes;
                foreach (XmlAttribute a in attrib)
                {
                    switch (a.Name)
                    {
                        case "name": Name = a.Value; break;
                        case "id": dict.Add(a.Name, a.Value); break;
                    }
                }

                foreach (XmlNode node in child.ChildNodes)
                {
                    XmlAttributeCollection pars = node.Attributes;
                    XmlAttribute name = pars["name"];
                    XmlAttribute value = pars["value"];
                    dict.Add(name.Value, value.Value);
                }

                if (Name.Equals("switch_app"))
                {
                    IApplication app = ApplicationFactory.makeApplication(dict["app_type"]);
                    app.Initialize(_parent.Owner, dict);                    
                }
                else
                {
                    IElement elem = ElementFactory.makeElement(Name);
                    elem.Initialize(this, dict);
                    elementList.Add(elem);
                }
            }
        }
    }
}

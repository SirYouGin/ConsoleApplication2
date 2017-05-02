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
            base.Initialize(_parent, _params);
            foreach (XmlNode child in node.ChildNodes)
            {   
                
                IElement elem = ElementFactory.makeElement(this, child);
                if (elem is IApplication) 
                { 
                    (_parent as IApplication).Deactivate();
                    (elem as IApplication).Activate();
                }
                else
                {                    
                    elementList.Add(elem);
                }
                
            }
        }
    }
}

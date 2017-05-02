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
    class Application : Element, IApplication
    {
        private XmlNode node;        
        private readonly HashSet<IBlock> blockList = new HashSet<IBlock>();

        public Application(XmlNode parent)
        {
            node = parent;
        }
        public int Count
        {
            get
            {
                return blockList.Count;
            }
        }

        public override void Execute()
        {
            foreach (IBlock b in blockList)
            {
                try
                {
                    OnElementStart(b);
                    b.Execute();
                    OnElementFinish(b);
                }
                catch (ElementException)
                {
                    throw;
                }
                catch (BlockException e)
                {
                    OnElementError(b,e);
                }
                catch (Exception e)
                {
                    throw new ApplicationException("Application exception", e);
                }
            }
        }

        public IEnumerator<IBlock> GetEnumerator()
        {
            return blockList.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public override void Initialize(IElement _parent, IDictionary<string, string> _params)
        {
            base.Initialize(_parent, _params);
        }

        public void Activate()
        {            
            (Owner as ITest).activeApplication = this;

        }

        public void Deactivate()
        {
            (Owner as ITest).activeApplication = null;
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;

using ConsoleApplication2.Events;
using ConsoleApplication2.Interfaces;


namespace ConsoleApplication2.Implementations
{
    public class Test : BaseNode, ITest
    {
        private XmlNode node;
        private readonly HashSet<IApplication> appList = new HashSet<IApplication>();
        public IApplication activeApplication;
        public Test(XmlNode parent)
        {
            node = parent;
        }

        protected Test()
        {
        }

        public int Count
        {
            get
            {
                return appList.Count;
            }
        }

        public override void Execute()
        {
            foreach (IApplication a in appList)
            {
                try
                {
                    OnElementStart(a);
                    a.Execute();
                    OnElementFinish(a);
                }
                catch (ElementException)
                {
                    throw;
                }
                catch (BlockException)
                {
                    throw;
                }
                catch (ApplicationException e)
                {
                    OnElementError(a, e);
                }
                catch (Exception e)
                {
                    throw new ApplicationException("Test exception", e);
                }
            }
        }        

        IEnumerator<IApplication> IEnumerable<IApplication>.GetEnumerator()
        {
            return appList.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public override void Initialize(IElement _parent, IDictionary<string, string> _params)
        {
            base.Initialize(_parent, _params);
        }
    }
}

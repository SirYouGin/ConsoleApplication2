using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;

using ConsoleApplication2.Events;
using ConsoleApplication2.Interfaces;


namespace ConsoleApplication2.Implementations
{
    public class Test : Element, ITest
    {
        private XmlNode node;
        private readonly HashSet<IApplication> appList = new HashSet<IApplication>();
        private IApplication m_activeApplication;
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

        IApplication ITest.activeApplication
        {
            get
            {
                return m_activeApplication;
            }

            set
            {                
                if (m_activeApplication != value)
                {
                    if (m_activeApplication != null) OnElementFinish(m_activeApplication);
                }
                m_activeApplication = value;
                m_activeApplication.Owner = this;

                if (m_activeApplication != null)
                {
                    OnElementStart(m_activeApplication);
                }
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

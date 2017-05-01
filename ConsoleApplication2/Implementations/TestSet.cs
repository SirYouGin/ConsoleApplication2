using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;

using ConsoleApplication2.Events;
using ConsoleApplication2.Interfaces;

namespace ConsoleApplication2.Implementations
{
    public class TestSet : BaseNode, ITestSet
    {
        private XmlNode node;
        private readonly HashSet<ITest> testList = new HashSet<ITest>();

        public IApplication defaultApplication;
        public TestSet(XmlNode parent)
        {
            node = parent;
        }        

        public int Count
        {
            get
            {
                return testList.Count;
            }
        }
        
        public IEnumerator<ITest> GetEnumerator()
        {
            return testList.GetEnumerator();
        }
        
        public override void Execute()
        {
            foreach (ITest t in testList)
            {
                try
                {
                    OnElementStart(t);
                    t.Execute();
                    OnElementFinish(t);
                }
                catch (ElementException)
                {
                    throw;
                }
                catch (BlockException)
                {
                    throw;
                }
                catch (AppException)
                {
                    throw;
                }
                catch (TestException e)
                {
                    OnElementError(t, e);
                }
                catch (Exception e)
                {
                    throw new TestSetException("Testset Exception", e);
                }
            }            
        }

        private IApplication addDefaultApplication()
        {
            IApplication app = new IbsoApplication();
            app.Initialize(this, GlobalConfig.getConfig());
            return app;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public override void Initialize(IElement _parent, IDictionary<string, string> _params)
        {
            base.Initialize(_parent, _params);
            defaultApplication = addDefaultApplication();
            foreach (XmlNode child in node.ChildNodes)
            {
                Test t = new Test(child);
                t.activeApplication = defaultApplication;
                t.Initialize(this);
                testList.Add(t);
            }
        }
    }
}

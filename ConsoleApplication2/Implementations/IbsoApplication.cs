using System;
using System.Collections.Generic;
using System.Xml;
using System.Text;
using System.Threading.Tasks;
using ConsoleApplication2.Interfaces;

namespace ConsoleApplication2.Implementations
{
    class IbsoApplication : Application
    {
        public IbsoApplication() :base() { }

        public override void Initialize(IElement _parent, IDictionary<string, string> _params)
        {
            base.Initialize(_parent, _params);
        }
    }
}

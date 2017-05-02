using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using ConsoleApplication2.Interfaces;

namespace ConsoleApplication2.Elements
{
    class mtSaveResultElement : Element, IElement
    {

        public override void Execute()
        {
            Console.WriteLine("{0}: Execute", GetType());
        }

        public override void Initialize(IElement _parent, IDictionary<string, string> dict)
        {
            base.Initialize(_parent, dict);
            Console.WriteLine("{0}: Initialize", GetType());
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication2.Interfaces
{
    public interface IElement
    {
        string Name { get; }
        void Initialize(IElement _parent, IDictionary<string, string> _params);
        void Execute();
    }
}

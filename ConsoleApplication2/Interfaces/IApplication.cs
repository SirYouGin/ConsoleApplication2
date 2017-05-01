using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication2.Interfaces
{
    public interface IApplication : IElement, IReadOnlyCollection<IBlock>
    {
    }
}

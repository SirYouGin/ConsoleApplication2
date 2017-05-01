using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ConsoleApplication2.Events;
using ConsoleApplication2.Interfaces;

namespace ConsoleApplication2.Implementations
{
    class Application : BaseNode, IApplication
    {
        private readonly HashSet<IBlock> blockList = new HashSet<IBlock>();
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
    }
}

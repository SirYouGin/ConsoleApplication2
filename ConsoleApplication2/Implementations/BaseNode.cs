using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ConsoleApplication2.Events;
using ConsoleApplication2.Interfaces;

namespace ConsoleApplication2.Implementations
{
    public abstract class BaseNode : IElement
    {
        public string Name
        {
            get
            {
                return GetType().Name;
            }
        }

        IElement parent;

        public event ElementStartEvent elemStart;

        public event ElementFinishEvent elemFinish;

        public event ElementErrorEvent elemError;

        protected virtual void OnElementStart(IElement elem)
        {
            if (elemStart != null)
            {
                ElementStartEvent evnt = elemStart; //avoid race condition
                evnt(elem);
            }
        }
        protected virtual void OnElementFinish(IElement elem)
        {
            if (elemStart != null)
            {
                ElementFinishEvent evnt = elemFinish; //avoid race condition
                evnt(elem);
            }
        }
        protected virtual void OnElementError(IElement elem, Exception e)
        {
            if (elemStart != null)
            {
                ElementErrorEvent evnt = elemError; //avoid race condition
                evnt(elem, e);
            }
        }

        public virtual void Initialize(IElement _parent, IDictionary<string, string> _params) { parent = _parent; }       

        public abstract void Execute();
    }
}

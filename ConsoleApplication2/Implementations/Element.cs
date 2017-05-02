using System;
using System.Collections.Generic;
using System.Xml;
using System.Text;
using System.Threading.Tasks;

using ConsoleApplication2.Events;
using ConsoleApplication2.Interfaces;

namespace ConsoleApplication2
{
    public abstract class Element : IElement
    {
        protected Dictionary<string, string> prop = new Dictionary<string, string>();
        public string Name {get;set;}
        public string Id {get;set;}
        public IElement Owner { get; set; }
        public abstract void Execute();
        public virtual void Initialize(IElement _parent,IDictionary<string, string> dict) { prop.updateFrom(dict); }

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
    }
}

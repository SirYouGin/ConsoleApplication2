using System;

using ConsoleApplication2.Interfaces;

namespace ConsoleApplication2.Events
{

    class ElementException : Exception { public ElementException(string msg, Exception inner) : base(msg, inner) { } }
    class BlockException : Exception { public BlockException(string msg, Exception inner) : base(msg, inner) { } }
    class AppException : Exception { public AppException(string msg, Exception inner) : base(msg, inner) { } }
    class TestException : Exception { public TestException(string msg, Exception inner) : base(msg, inner) { } }
    class TestSetException : Exception { public TestSetException(string msg, Exception inner) : base(msg, inner) { } }
    public class ElementEventArgs : EventArgs
    {
        Exception e;
        public ElementEventArgs(Exception ex) { e = ex; }

    }

    public delegate void ElementStartEvent(IElement sender);
    public delegate void ElementFinishEvent(IElement sender);
    public delegate void ElementErrorEvent(IElement sender, Exception e);

}

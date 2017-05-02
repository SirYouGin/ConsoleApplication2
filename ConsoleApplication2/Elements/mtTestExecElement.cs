using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ConsoleApplication2.Interfaces;
using ConsoleApplication2.Implementations;

namespace ConsoleApplication2.Elements
{
    class mtTestExecElement : Element, IElement
    {

        public override void Execute()
        {
            Console.WriteLine("{0}: Execute", GetType());

            using (IConnection session = ConnectionFactory.makeConnection())
            {
                IDBCommand cmd = session.getCommand("Z$SCRIPRS_LIB_API.mt_test_exec");
                cmd.withParam("P_SESSION_FILE_NAME", null);
                cmd.withParam("P_TEST_MT", prop["mt_test"]);
                cmd.withParam("P_CONDITION", prop["condition"]);
                cmd.Execute();

                session.Commit();
            };
        }

        public override void Initialize(IElement _parent, IDictionary<string, string> dict)
        {
            base.Initialize(_parent, dict);
            Console.WriteLine("{0}: Initialize", GetType());            
        }
    }
}

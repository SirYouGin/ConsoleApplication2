using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

using ConsoleApplication2.Events;
using ConsoleApplication2.Interfaces;
using ConsoleApplication2.Implementations;

namespace ConsoleApplication2
{
    class Program
    {
        static void Main(string[] args)
        {

            XmlDocument doc = new XmlDocument();
            doc.Load("tests.xml");

            ITestSet testSet = new TestSet();

            testSet.Initialize(null, null);

            testSet.Execute();

            Console.ReadKey();
        }
    }
}

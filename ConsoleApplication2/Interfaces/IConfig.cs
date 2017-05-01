using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication2.Interfaces
{
    public interface IConfig
    {
        string getProperty(string param);
        void setProperty(string param);
        IDictionary<string, string> getConfig();
    }
}

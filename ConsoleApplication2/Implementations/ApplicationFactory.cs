using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ConsoleApplication2.Interfaces;

namespace ConsoleApplication2.Implementations
{
    public static class ApplicationFactory
    {
        private static readonly Dictionary<string, Type> wellKnownApplications = new Dictionary<string, Type>() {
         { "IBSO", typeof(ConsoleApplication2.Implementations.IbsoApplication)} 
        
        };

        public static IApplication makeApplication(string Name)
        {

            if (String.IsNullOrEmpty(Name)) throw new ArgumentNullException("Не задано имя приложения");
            if (!wellKnownApplications.ContainsKey(Name)) throw new NotImplementedException(String.Format("Неизвестное имя приложения \"{0}\"", Name));

            Type type = wellKnownApplications[Name];

            object instance = Activator.CreateInstance(type);
            Application app = instance as Application;
            app.Name = Name;            
            return app;
        }
    }
}

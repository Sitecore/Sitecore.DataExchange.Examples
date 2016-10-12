using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleMenuMaker
{
    [AttributeUsage(AttributeTargets.Class)]
    public class MenuAttribute : Attribute
    {
        public MenuAttribute(string name)
        {
            this.Name = name;
        }
        public string Name { get; private set; }
        public string Description { get; set; }
    }
}

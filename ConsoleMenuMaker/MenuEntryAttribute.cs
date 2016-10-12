using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleMenuMaker
{
    [AttributeUsage(AttributeTargets.Method)]
    public class MenuEntryAttribute : Attribute
    {
        public MenuEntryAttribute(char c)
        {
            this.Character = c;
        }
        public char Character { get; private set; }
        public string Text { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleMenuMaker
{
    public class MenuOption<T>
    {
        public char Character { get; set; }
        public string Text { get; set; }
        public Func<IMenuManager<T>, MenuStatus> Action { get; set; }
    }
}

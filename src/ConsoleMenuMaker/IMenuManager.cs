using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleMenuMaker
{
    public interface IMenuManager<T>
    {
        void Render(IMenu<T> menu);
        T Context { get; set; }
    }
}

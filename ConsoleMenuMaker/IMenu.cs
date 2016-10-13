using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleMenuMaker
{
    public enum MenuStatus
    {
        CloseMenu,
        PreserveMenu
    }
    public interface IMenu<T>
    {
        string Name { get; }
        string Description { get; }
        int Width { get; }
        MenuOption<T>[] MenuOptions { get; }
        IMenu<T> PreviousMenu { get; }
        void WriteMessage(string message, params string[] args);
        void WriteMessage(ConsoleColor color, string message, params string[] args);
    }
}

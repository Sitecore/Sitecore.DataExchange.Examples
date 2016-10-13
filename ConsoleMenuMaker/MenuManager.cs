using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleMenuMaker
{
    public class MenuManager<T> : IMenuManager<T>
    {
        public MenuManager()
        {
        }
        public T Context { get; set; }
        private void RenderMenu(IMenu<T> menu)
        {
            if (menu == null)
            {
                return;
            }
            Console.Clear();
            if (!string.IsNullOrWhiteSpace(menu.Name))
            {
                menu.WriteMessage(new string('=', menu.Width));
                menu.WriteMessage(menu.Name);
                menu.WriteMessage(new string('=', menu.Width));
            }
            if (!string.IsNullOrWhiteSpace(menu.Description))
            {
                menu.WriteMessage(menu.Description);
                menu.WriteMessage(new string('-', menu.Width));
            }
            foreach (var menuOption in menu.MenuOptions)
            {
                menu.WriteMessage(string.Format("{0}. {1}", menuOption.Character, menuOption.Text));
            }
            Console.WriteLine();
        }
        public void Render(IMenu<T> menu)
        {
            if (menu == null || menu.MenuOptions == null || menu.MenuOptions.Length == 0)
            {
                return;
            }
            if (menu == null)
            {
                return;
            }
            RenderMenu(menu);
            while(true)
            {
                var key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.Escape)
                {
                    break;
                }
                var menuOption = menu.MenuOptions.FirstOrDefault(m => m.Character == key.KeyChar);
                if (menuOption == null)
                {
                    menu.WriteMessage(ConsoleColor.Red, "Invalid menu option.");
                }
                else
                {
                    if (menuOption.Action(this) == MenuStatus.CloseMenu)
                    {
                        if (menu.PreviousMenu == null)
                        {
                            break;
                        }
                    }
                }
                menu.WriteMessage("Press any key to continue, ESC to exit.");
                key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.Escape)
                {
                    break;
                }
                RenderMenu(menu);
            }
            Render(menu.PreviousMenu);
        }
    }
}

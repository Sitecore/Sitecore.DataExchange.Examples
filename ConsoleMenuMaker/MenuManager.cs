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
            this.Width = 80;
        }
        public T Context { get; set; }
        public int Width { get; set; }
        protected string EnsureTextLength(string value)
        {
            var count = 0;
            var lines = value.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).GroupBy(w => (count += w.Length + 1) / this.Width).Select(g => string.Join(" ", g));
            return string.Join("\n", lines);
        }
        private void RenderMenu(IMenu<T> menu)
        {
            if (menu == null)
            {
                return;
            }
            Console.Clear();
            if (!string.IsNullOrWhiteSpace(menu.Name))
            {
                menu.WriteMessage(new string('=', this.Width));
                menu.WriteMessage(EnsureTextLength(menu.Name));
                menu.WriteMessage(new string('=', this.Width));
            }
            if (!string.IsNullOrWhiteSpace(menu.Description))
            {
                menu.WriteMessage(EnsureTextLength(menu.Description));
                menu.WriteMessage(new string('-', this.Width));
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
                    menu.WriteMessage("Invalid menu option.", ConsoleColor.Red);
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

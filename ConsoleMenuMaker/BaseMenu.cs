using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleMenuMaker
{
    public abstract class BaseMenu<T> : IMenu<T>
    {
        protected BaseMenu(IMenu<T> previousMenu)
        {
            this.Width = 80;
            this.PreviousMenu = previousMenu;
            //
            //populate the menu
            var menuAttr = (MenuAttribute)Attribute.GetCustomAttribute(this.GetType(), typeof(MenuAttribute));
            if (menuAttr != null)
            {
                this.Name = menuAttr.Name;
                this.Description = menuAttr.Description;
            }
            //
            //populate menu options
            var options = new List<MenuOption<T>>();
            foreach (var method in this.GetType().GetMethods())
            {
                var entryAttr = (MenuEntryAttribute)Attribute.GetCustomAttribute(method, typeof(MenuEntryAttribute));
                if (entryAttr != null)
                {
                    var action = (Func<IMenuManager<T>, MenuStatus>)Delegate.CreateDelegate(typeof(Func<IMenuManager<T>, MenuStatus>), this, method);
                    options.Add(new MenuOption<T> { Action = action, Character = entryAttr.Character, Text = entryAttr.Text });
                }
            }
            this.MenuOptions = options.ToArray();
        }
        public int Width { get; set; }
        public void WriteMessage(ConsoleColor color, string message, params string[] args)
        {
            var previousColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
            WriteMessage(message);
            Console.ForegroundColor = previousColor;
        }
        public void WriteMessage(string message, params string[] args)
        {
            Console.WriteLine(MenuManagerUtils.EnsureTextLength(this.Width, message));
        }

        public IMenu<T> PreviousMenu { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public MenuOption<T>[] MenuOptions { get; private set; }
    }
}

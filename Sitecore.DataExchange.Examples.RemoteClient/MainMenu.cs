using ConsoleMenuMaker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sitecore.DataExchange.Examples.RemoteClient
{
    [Menu("Remote Client for Data Exchange Framework", Description = "This is a sample application that demonstrates how to use the remote SDK from Sitecore Data Exchange Framework.")]
    public class MainMenu : BaseMenu<RemoteClientContext>
    {
        public MainMenu(IMenu<RemoteClientContext> previousMenu) : base(previousMenu) { }
        [MenuEntry('1', Text = "Edit Connection Settings")]
        public MenuStatus EditConnectionSettings(IMenuManager<RemoteClientContext> manager)
        {
            base.WriteMessage("Not yet supported.", ConsoleColor.Red);
            return MenuStatus.PreserveMenu;
        }
        [MenuEntry('2', Text = "View Connection Settings")]
        public MenuStatus ViewConnectionSettings(IMenuManager<RemoteClientContext> manager)
        {
            if (manager.Context == null)
            {
                base.WriteMessage("No connection settings have been set.", ConsoleColor.Red);
            }
            else
            {
                base.WriteMessage("Connection settings:");
                base.WriteMessage(string.Format("  * Username: {0}", manager.Context.Username));
                base.WriteMessage(string.Format("  * Password: {0}", manager.Context.Password));
                base.WriteMessage(string.Format("  * Host name: {0}", manager.Context.Host));
                base.WriteMessage(string.Format("  * Database name: {0}", manager.Context.Database));
                base.WriteMessage(null);
            }
            return MenuStatus.PreserveMenu;
        }
        [MenuEntry('3', Text = "Test Connection")]
        public MenuStatus TestConnection(IMenuManager<RemoteClientContext> manager)
        {
            var menu = new TestConnectionMenu(this);
            manager.Render(menu);
            return MenuStatus.CloseMenu;
        }
    }
}

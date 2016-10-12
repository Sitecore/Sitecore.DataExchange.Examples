using ConsoleMenuMaker;
using Sitecore.DataExchange.Remote.Http;
using Sitecore.DataExchange.Remote.Repositories;
using Sitecore.DataExchange.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sitecore.DataExchange.Examples.RemoteClient
{
    [Menu("Test Connection", Description = "Test a connection to Sitecore using the item model repository.")]
    public class TestConnectionMenu : BaseMenu<RemoteClientContext>
    {
        public TestConnectionMenu(IMenu<RemoteClientContext> previousMenu) : base(previousMenu) { }
        [MenuEntry('1', Text = "Item Path")]
        public MenuStatus FindItemByPath(IMenuManager<RemoteClientContext> manager)
        {
            Console.Write("Enter the item path: ");
            var path = Console.ReadLine();
            try
            {
                DoGetItemByPath(path, manager.Context);
            }
            catch(Exception ex)
            {
                base.WriteMessage(ex.StackTrace, ConsoleColor.Red);
            }
            base.WriteMessage(null);
            return MenuStatus.PreserveMenu;
        }
        [MenuEntry('2', Text = "Item ID")]
        public MenuStatus FindItemById(IMenuManager<RemoteClientContext> manager)
        {
            Console.Write("Enter the item ID: ");
            var id = Console.ReadLine();
            try
            {
                DoGetItemById(id, manager.Context);
            }
            catch (Exception ex)
            {
                base.WriteMessage(ex.StackTrace, ConsoleColor.Red);
            }
            base.WriteMessage(null);
            return MenuStatus.PreserveMenu;
        }

        private IItemModelRepository GetItemModelRepository(RemoteClientContext context)
        {
            //
            // Specify the settings used to make a connection.
            var cxSettings = new ConnectionSettings
            {
                Host = context.Host,
                UserName = context.Username,
                Password = context.Password,
            };
            //
            // Instantiate an object that uses the Sitecore item web API
            // to read items from a Sitecore database. This functionality
            // enables you to determine whether or not a connection can
            // be established with the Sitecore server.
            return new WebApiItemModelRepository(context.Database, cxSettings);
        }
        private void DoGetItemByPath(string path, RemoteClientContext context)
        {
            //
            // Get the item specified by the parameter.
            var itemRepo = GetItemModelRepository(context);
            var item = itemRepo.Get(path);
            if (item != null)
            {
                //
                // A connection was established with the Sitecore server
                // and the specified item was found.
                base.WriteMessage("The specified item exists on the server.");
            }
            else
            {
                //
                // The item was not found, but a connection was still
                // established with the Sitecore server.
                base.WriteMessage("The specified item does not exist on the server.", ConsoleColor.Red);
            }
        }
        private void DoGetItemById(string id, RemoteClientContext context)
        {
            //
            // Get the item specified by the parameter.
            Guid guid = Guid.Empty;
            if (!Guid.TryParse(id, out guid))
            {
                base.WriteMessage("The specified value is not a valid ID.", ConsoleColor.Red);
                return;
            }
            var itemRepo = GetItemModelRepository(context);
            var item = itemRepo.Get(guid);
            if (item != null)
            {
                //
                // A connection was established with the Sitecore server
                // and the specified item was found.
                base.WriteMessage("The specified item exists on the server.");
            }
            else
            {
                //
                // The item was not found, but a connection was still
                // established with the Sitecore server.
                base.WriteMessage("The specified item does not exist on the server.", ConsoleColor.Red);
            }
        }
    }
}

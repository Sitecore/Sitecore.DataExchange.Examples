using ConsoleMenuMaker;
using Sitecore.DataExchange.Remote.Http;
using Sitecore.DataExchange.Remote.Repositories;
using Sitecore.DataExchange.Repositories;
using Sitecore.Services.Core.Model;
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
        private const string DEFAULT_PATH = "/sitecore/content/Home";
        private const string DEFAULT_ID = "{11111111-1111-1111-1111-111111111111}";


        [MenuEntry('1', Text = "Item Path")]
        public MenuStatus FindItemByPath(IMenuManager<RemoteClientContext> manager)
        {
            var path = ReadValue("Enter the item path", DEFAULT_PATH);
            try
            {
                DoGetItemByPath(path, manager.Context);
            }
            catch(Exception ex)
            {
                base.WriteMessage(ConsoleColor.Red, ex.StackTrace);
            }
            base.WriteMessage(null);
            return MenuStatus.PreserveMenu;
        }


        [MenuEntry('2', Text = "Item ID")]
        public MenuStatus FindItemById(IMenuManager<RemoteClientContext> manager)
        {
            var id = ReadValue("Enter the item ID", DEFAULT_ID);
            try
            {
                DoGetItemById(id, manager.Context);
            }
            catch (Exception ex)
            {
                base.WriteMessage(ConsoleColor.Red, ex.StackTrace);
            }
            base.WriteMessage(null);
            return MenuStatus.PreserveMenu;
        }


        private string ReadValue(string prompt, string defaultValue)
        {
            Console.Write("{0} [{1}]: ", prompt, defaultValue);
            var value = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(value))
            {
                value = defaultValue;
            }
            return value;
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
        private void DisplayItemDetails(ItemModel itemModel)
        {
            base.WriteMessage("Item details:");
            base.WriteMessage(string.Format("  * Name: {0}", itemModel[ItemModel.ItemName]));
            base.WriteMessage(string.Format("  * Display name: {0}", itemModel[ItemModel.DisplayName]));
            base.WriteMessage(string.Format("  * ID: {0}", itemModel[ItemModel.ItemID]));
            base.WriteMessage(string.Format("  * Path: {0}", itemModel[ItemModel.ItemPath]));
            base.WriteMessage(null);
        }
        private void DoGetItemByPath(string path, RemoteClientContext context)
        {
            //
            // Get the item specified by the parameter.
            var itemModelRepo = GetItemModelRepository(context);
            var itemModel = itemModelRepo.Get(path);
            if (itemModel != null)
            {
                //
                // A connection was established with the Sitecore server
                // and the specified item was found.
                DisplayItemDetails(itemModel);
            }
            else
            {
                //
                // The item was not found, but a connection was still
                // established with the Sitecore server.
                base.WriteMessage(ConsoleColor.Red, "The specified item does not exist on the server.");
            }
        }
        private void DoGetItemById(string id, RemoteClientContext context)
        {
            //
            // Get the item specified by the parameter.
            Guid guid = Guid.Empty;
            if (!Guid.TryParse(id, out guid))
            {
                base.WriteMessage(ConsoleColor.Red, "The specified value is not a valid ID.");
                return;
            }
            var itemModelRepo = GetItemModelRepository(context);
            var itemModel = itemModelRepo.Get(guid);
            if (itemModel != null)
            {
                //
                // A connection was established with the Sitecore server
                // and the specified item was found.
                DisplayItemDetails(itemModel);
            }
            else
            {
                //
                // The item was not found, but a connection was still
                // established with the Sitecore server.
                base.WriteMessage(ConsoleColor.Red, "The specified item does not exist on the server.");
            }
        }
    }
}

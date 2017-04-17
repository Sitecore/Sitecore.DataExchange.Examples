using ConsoleMenuMaker;
using Sitecore.DataExchange.Models;
using Sitecore.DataExchange.Remote.Http;
using Sitecore.DataExchange.Remote.Repositories;
using Sitecore.DataExchange.Repositories;
using Sitecore.DataExchange.Repositories.Tenants;
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
            base.WriteMessage(ConsoleColor.Red, "Not yet supported.");
            return MenuStatus.PreserveMenu;
        }


        [MenuEntry('2', Text = "View Connection Settings")]
        public MenuStatus ViewConnectionSettings(IMenuManager<RemoteClientContext> manager)
        {
            if (manager.Context == null)
            {
                base.WriteMessage(ConsoleColor.Red, "No connection settings have been set.");
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


        [MenuEntry('4', Text = "List Tenants")]
        public MenuStatus ListTenants(IMenuManager<RemoteClientContext> manager)
        {
            if (manager.Context == null)
            {
                base.WriteMessage(ConsoleColor.Red, "No connection settings have been set.");
            }
            else
            {
                InitializeItemModelRepository(manager);
                var tenants = GetTenants();
                if (tenants.Any())
                {
                    //
                    // At least one enabled tenant was returned.
                    base.WriteMessage("The following tenants were retrieved:");
                    foreach(var tenant in tenants)
                    {
                        base.WriteMessage(string.Format("  * {0}", tenant.Name));
                    }
                }
                else
                {
                    //
                    // No tenants were returned. Since no exception was thrown,
                    // it is likely the Sitecore server either has no tenants
                    // defined on it, or none of the tenants are enabled.
                    base.WriteMessage("No tenants are defined, or if tenants are defined, not one is enabled.");
                }
            }
            base.WriteMessage(null);
            return MenuStatus.PreserveMenu;
        }
        private void InitializeItemModelRepository(IMenuManager<RemoteClientContext> manager)
        {
            //
            // Specify the settings used to make a connection.
            var cxSettings = new ConnectionSettings
            {
                Host = manager.Context.Host,
                UserName = manager.Context.Username,
                Password = manager.Context.Password,
            };
            //
            // Instantiate an object that uses the Sitecore item web API
            // to read items from a Sitecore database.
            Sitecore.DataExchange.Context.ItemModelRepository = new WebApiItemModelRepository(manager.Context.Database, cxSettings);
        }
        private IEnumerable<Tenant> GetTenants()
        {
            //
            // Instantiate an object that uses the item repository to read
            // configuration items from the Sitecore server and convert
            // those items into Data Exchange Framework components.
            var tenantRepo = new SitecoreTenantRepository();
            //
            // Only read enabled tenants. The remote API allows you to read
            // all of the tenants defined on a Sitecore server. It is the
            // responsibility of the client application to respect the
            // setting that indicates whether or not a tenant is enabled.
            return tenantRepo.GetTenants().Where(t => t.Enabled);
        }


        [MenuEntry('5', Text = "List Pipeline Batches")]
        public MenuStatus ListPipelineBatches(IMenuManager<RemoteClientContext> manager)
        {
            if (manager.Context == null)
            {
                base.WriteMessage(ConsoleColor.Red, "No connection settings have been set.");
            }
            else
            {
                InitializeItemModelRepository(manager);
                var tenants = GetTenants();
                if (!tenants.Any())
                {
                    base.WriteMessage("No tenants were found, so no pipeline batches are available.");
                }
                else
                {
                    //
                    // Prompt the user to select one of the available tenants.
                    base.WriteMessage("Select the tenant whose pipeline batches you want to list:");
                    var position = 0;
                    foreach(var tenant in tenants)
                    {
                        base.WriteMessage(string.Format("  {0}. {1}", ++position, tenant.Name));
                    }
                    base.WriteMessage(null);
                    //
                    var key = Console.ReadKey(true);
                    if (!int.TryParse(key.KeyChar.ToString(), out position) || position > tenants.Count() || position < 1)
                    {
                        //
                        // The selection was not valid.
                        base.WriteMessage(ConsoleColor.Red, "The specified selection is not a valid option.");
                    }
                    else
                    {
                        var tenant = tenants.Skip(position - 1).FirstOrDefault();
                        var repo = new SitecoreTenantRepository();
                        var batches = repo.GetPipelineBatches(tenant.ID, true).Where(b => b.Enabled);
                        if (!batches.Any())
                        {
                            base.WriteMessage(ConsoleColor.Red, "No pipeline batches were retrieved. Reasons include:");
                            base.WriteMessage(ConsoleColor.Red, "  * No pipeline batches are defined under the tenant");
                            base.WriteMessage(ConsoleColor.Red, "  * No pipeline batches are enabled");
                            base.WriteMessage(ConsoleColor.Red, "  * No pipeline batches are configured to support being called remotely");
                        }
                        else
                        {
                            //
                            // At least one enabled pipeline batch was returned.
                            base.WriteMessage("The following pipeline batches were retrieved:");
                            foreach (var batch in batches)
                            {
                                base.WriteMessage(string.Format("  * {0}", batch.Name));
                            }
                        }
                    }
                }
            }
            base.WriteMessage(null);
            return MenuStatus.PreserveMenu;
        }

    }
}

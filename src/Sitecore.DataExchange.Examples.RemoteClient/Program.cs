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
    class Program
    {
        static void Main(string[] args)
        {
            var renderer = new MenuManager<RemoteClientContext>
            {
                Context = new RemoteClientContext
                {
                    Username = "sitecore\\admin",
                    Password = "b",
                    Host = "sc82dataex",
                    Database = "master"
                }
            };
            renderer.Render(new MainMenu(null));
        }
    }
}

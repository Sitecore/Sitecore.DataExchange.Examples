using Sitecore.DataExchange.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examples.DataExchange.Providers.FileSystem
{
    public static class EndpointExtensions
    {
        public static TextFileSettings GetTextFileSettings(this Endpoint endpoint)
        {
            return endpoint.GetPlugin<TextFileSettings>();
        }
        public static bool HasTextFileSettings(this Endpoint endpoint)
        {
            return (GetTextFileSettings(endpoint) != null);
        }
    }
}

using Sitecore.DataExchange.Models;
using Sitecore.DataExchange.Troubleshooters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examples.DataExchange.Providers.FileSystem
{
    public class TextFileEndpointTroubleshooter : BaseEndpointTroubleshooter
    {
        public TextFileEndpointTroubleshooter()
        {
        }
        protected override ITroubleshooterResult Troubleshoot(Endpoint endpoint, TroubleshooterContext context)
        {
            var settings = endpoint.GetPlugin<TextFileSettings>();
            if (settings == null)
            {
                return TroubleshooterResult.FailResult("The endpoint is missing the plugin TextFileSettings.");
            }
            if (string.IsNullOrWhiteSpace(settings.Path))
            {
                return TroubleshooterResult.FailResult("The endpoint does not have a file path specified on it.");
            }
            if (!File.Exists(settings.Path))
            {
                return TroubleshooterResult.FailResult(
                    "The path specified on the endpoint either " + 
                    "points to a file that does not exist " +
                    "or the process does not have permission to read the file.");
            }
            return TroubleshooterResult.SuccessResult("The specified file exists and can be accessed.");
        }
    }
}

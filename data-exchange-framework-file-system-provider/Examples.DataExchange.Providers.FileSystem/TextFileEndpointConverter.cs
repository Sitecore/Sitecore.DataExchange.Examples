using Sitecore.DataExchange.Attributes;
using Sitecore.DataExchange.Converters.Endpoints;
using Sitecore.DataExchange.Models;
using Sitecore.DataExchange.Repositories;
using Sitecore.Services.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examples.DataExchange.Providers.FileSystem
{
    [SupportedIds(TextFileEndpointTemplateId)]
    public class TextFileEndpointConverter : BaseEndpointConverter
    {
        public const string TextFileEndpointTemplateId = "{BED376BC-FC04-458F-B673-7BAE098C3E32}";
        public const string TemplateFieldColumnSeparator = "ColumnSeparator";
        public const string TemplateFieldColumnHeadersInFirstLine = "ColumnHeadersInFirstLine";
        public const string TemplateFieldPath = "Path";
        public TextFileEndpointConverter(IItemModelRepository repository) : base(repository)
        {
        }
        protected override void AddPlugins(ItemModel source, Endpoint endpoint)
        {
            //
            //create the plugin
            var settings = new TextFileSettings
            {
                //
                //populate the plugin using values from the item
                ColumnHeadersInFirstLine = this.GetBoolValue(source, TemplateFieldColumnHeadersInFirstLine),
                ColumnSeparator = this.GetStringValue(source, TemplateFieldColumnSeparator),
                Path = this.GetStringValue(source, TemplateFieldPath)
            };
            //
            //add the plugin to the endpoint
            endpoint.AddPlugin(settings);
        }
    }
}

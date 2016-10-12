using Sitecore.DataExchange.Converters.Endpoints;
using Sitecore.Services.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sitecore.DataExchange.Repositories;
using Sitecore.DataExchange.Models;
using Sitecore.DataExchange.Providers.FileSystem.Plugins;
using Sitecore.DataExchange.Providers.FileSystem.Models.ItemModels.Endpoints;
using Sitecore.DataExchange.Converters;

namespace Sitecore.DataExchange.Providers.FileSystem.Converters.Endpoints
{
    public class TextFileEndpointConverter : BaseEndpointConverter<ItemModel>
    {
        private static readonly Guid TemplateId = Guid.Parse("{A28280BE-8331-4BAA-B5BE-41E7456FB67E}");
        public TextFileEndpointConverter(IItemModelRepository repository) : base(repository)
        {
            this.SupportedTemplateIds.Add(TemplateId);
        }
        protected override void AddPlugins(ItemModel source, Endpoint endpoint)
        {
            var settings = new TextFileSettings();
            settings.Path = base.GetStringValue(source, TextFileEndpointItemModel.Path);
            settings.ColumnSeparator = base.GetStringValue(source, TextFileEndpointItemModel.ColumnSeparator);
            settings.ColumnHeadersInFirstLine = base.GetBoolValue(source, TextFileEndpointItemModel.ColumnHeadersInFirstLine);
            //
            endpoint.Plugins.Add(settings);
        }
    }
}

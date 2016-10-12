using NSubstitute;
using Sitecore.DataExchange.Models;
using Sitecore.DataExchange.Providers.FileSystem.Converters.Endpoints;
using Sitecore.DataExchange.Providers.FileSystem.Extensions;
using Sitecore.DataExchange.Providers.FileSystem.Models.ItemModels.Endpoints;
using Sitecore.DataExchange.Repositories;
using Sitecore.Services.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Sitecore.DataExchange.Providers.FileSystem.Tests.Converters.Endpoints
{
    public class TextFileEndpointConverterTests
    {
        [Fact]
        public void CannotConvertNullItemModel()
        {
            var itemModelRepo = Substitute.For<IItemModelRepository>();
            var converter = new TextFileEndpointConverter(itemModelRepo);
            Assert.False(converter.CanConvert(null));
            Assert.Null(converter.Convert(null));
        }
        [Fact]
        public void CannotConvertWhenTemplateIsNotSupported()
        {
            var itemModelRepo = Substitute.For<IItemModelRepository>();
            var converter = new TextFileEndpointConverter(itemModelRepo);
            var itemModel = new ItemModel();
            itemModel[ItemModel.TemplateID] = Guid.Empty;
            Assert.False(converter.CanConvert(itemModel));
            Assert.Null(converter.Convert(itemModel));
        }
        [Fact]
        public void CanConvert()
        {
            var itemModelRepo = Substitute.For<IItemModelRepository>();
            var converter = new TextFileEndpointConverter(itemModelRepo);
            var itemModel = new ItemModel();
            itemModel[ItemModel.TemplateID] = converter.SupportedTemplateIds.FirstOrDefault();
            itemModel[TextFileEndpointItemModel.ColumnHeadersInFirstLine] = "1";
            itemModel[TextFileEndpointItemModel.ColumnSeparator] = "COLUMN-SEPARATOR";
            itemModel[TextFileEndpointItemModel.Path] = "PATH-VALUE";
            Assert.True(converter.CanConvert(itemModel));
            var endpoint = converter.Convert(itemModel);
            Assert.NotNull(endpoint);
            Assert.IsAssignableFrom<Endpoint>(endpoint);
            var settings = endpoint.GetTextFileSettings();
            Assert.NotNull(settings);
            Assert.True(settings.ColumnHeadersInFirstLine);
            Assert.Same("COLUMN-SEPARATOR", settings.ColumnSeparator);
            Assert.Same("PATH-VALUE", settings.Path);
        }
    }
}

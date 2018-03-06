using NSubstitute;
using Sitecore.DataExchange;
using Sitecore.DataExchange.Converters;
using Sitecore.DataExchange.Converters.PipelineSteps;
using Sitecore.DataExchange.Models;
using Sitecore.DataExchange.Plugins;
using Sitecore.DataExchange.Repositories;
using Sitecore.Services.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Examples.DataExchange.Providers.FileSystem.Tests
{
    public class EndpointConverter : BaseItemModelConverter<Endpoint>
    {
        public EndpointConverter(IItemModelRepository repository) : base(repository)
        {
        }
        public static readonly Endpoint Endpoint = new Endpoint();
        protected override ConvertResult<Endpoint> ConvertSupportedItem(ItemModel source)
        {
            return ConvertResult<Endpoint>.PositiveResult(EndpointConverter.Endpoint);
        }
    }
    public class ReadTextFileStepConverterTests
    {
        [Fact]
        public void EndpointSettingsPluginWithCorrectValuesIsAddedToPipelineStepWhenFieldsAreSetOnItem()
        {
            var endpointId = Guid.NewGuid();
            var endpointModel = new ItemModel
            {
                [ItemModel.ItemID] = endpointId,
                [FieldNames.ConverterType] = "Examples.DataExchange.Providers.FileSystem.Tests.EndpointConverter, Examples.DataExchange.Providers.FileSystem.Tests"
            };

            var stepModel = new ItemModel
            {
                [ItemModel.TemplateID] = ReadTextFileStepConverter.ReadTextFileStepTemplateId,
                [ReadTextFileStepConverter.TemplateFieldEndpointFrom] = endpointId.ToString()
            };
            var repo = Substitute.For<IItemModelRepository>();
            repo.Get(endpointId).Returns(endpointModel);
            var converter = new ReadTextFileStepConverter(repo);
            var result = converter.Convert(stepModel);
            Assert.True(result.WasConverted);
            Assert.NotNull(result.ConvertedValue);
            var settings = result.ConvertedValue.GetPlugin<EndpointSettings>();
            Assert.NotNull(settings);
            Assert.Equal(EndpointConverter.Endpoint, settings.EndpointFrom);
            Assert.Null(settings.EndpointTo);
        }
        [Fact]
        public void EndpointSettingsPluginWithDefaultValuesIsAddedToPipelineStepWhenNoFieldsAreSetOnItem()
        {
            var model = new ItemModel
            {
                [ItemModel.TemplateID] = ReadTextFileStepConverter.ReadTextFileStepTemplateId
            };
            var repo = Substitute.For<IItemModelRepository>();
            var converter = new ReadTextFileStepConverter(repo);
            var result = converter.Convert(model);
            Assert.True(result.WasConverted);
            Assert.NotNull(result.ConvertedValue);
            var settings = result.ConvertedValue.GetPlugin<EndpointSettings>();
            Assert.NotNull(settings);
            Assert.Null(settings.EndpointFrom);
            Assert.Null(settings.EndpointTo);
        }
        [Fact]
        public void ItemCannotBeConvertedWhenItIsBasedOnAnUnsupportedTemplate()
        {
            var model = new ItemModel
            {
            };
            var repo = Substitute.For<IItemModelRepository>();
            var converter = new ReadTextFileStepConverter(repo);
            var result = converter.Convert(model);
            Assert.False(result.WasConverted);
            Assert.Null(result.ConvertedValue);
        }
    }
}

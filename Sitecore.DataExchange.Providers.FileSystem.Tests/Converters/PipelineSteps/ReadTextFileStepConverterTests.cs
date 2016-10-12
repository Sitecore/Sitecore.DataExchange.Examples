using NSubstitute;
using Sitecore.DataExchange.Converters;
using Sitecore.DataExchange.Extensions;
using Sitecore.DataExchange.Models;
using Sitecore.DataExchange.Models.ItemModels.Common;
using Sitecore.DataExchange.Models.ItemModels.Endpoints;
using Sitecore.DataExchange.Providers.FileSystem.Converters.PipelineSteps;
using Sitecore.DataExchange.Providers.FileSystem.Models.ItemModels.PipelineSteps;
using Sitecore.DataExchange.Repositories;
using Sitecore.Services.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Sitecore.DataExchange.Providers.FileSystem.Tests.Converters.PipelineSteps
{
    public class TextFileEndpointConverterTests
    {
        [Fact]
        public void CannotConvertNullItemModel()
        {
            var itemModelRepo = Substitute.For<IItemModelRepository>();
            var converter = new ReadTextFileStepConverter(itemModelRepo);
            Assert.False(converter.CanConvert(null));
            Assert.Null(converter.Convert(null));
        }
        [Fact]
        public void CannotConvertWhenTemplateIsNotSupported()
        {
            var itemModelRepo = Substitute.For<IItemModelRepository>();
            var converter = new ReadTextFileStepConverter(itemModelRepo);
            var itemModel = new ItemModel();
            itemModel[ItemModel.TemplateID] = Guid.Empty;
            Assert.False(converter.CanConvert(itemModel));
            Assert.Null(converter.Convert(itemModel));
        }
        [Fact]
        public void CanConvert()
        {
            var itemModelRepo = Substitute.For<IItemModelRepository>();
            //
            var endpointId = Guid.NewGuid();
            var endpointItemModel = new ItemModel();
            endpointItemModel[ItemModel.ItemID] = endpointId;
            endpointItemModel[ConvertibleItemModel.ConverterType] = typeof(MyEndpointConverter).AssemblyQualifiedName;
            itemModelRepo.Get(endpointId).Returns(endpointItemModel);
            //
            var converter = new ReadTextFileStepConverter(itemModelRepo);
            var itemModel = new ItemModel();
            itemModel[ItemModel.TemplateID] = converter.SupportedTemplateIds.FirstOrDefault();
            itemModel[ReadTextFileStepItemModel.EndpointFrom] = endpointId;
            //
            Assert.True(converter.CanConvert(itemModel));
            var step = converter.Convert(itemModel);
            Assert.NotNull(step);
            Assert.IsType<PipelineStep>(step);
            var settings = step.GetEndpointSettings();
            Assert.NotNull(settings);
            Assert.NotNull(settings.EndpointFrom);
        }
    }
    public class MyEndpointConverter : BaseItemModelConverter<ItemModel, Endpoint>
    {
        public MyEndpointConverter(IItemModelRepository repository) : base(repository)
        {
        }
        public override bool CanConvert(ItemModel source)
        {
            return true;
        }
        public override Endpoint Convert(ItemModel source)
        {
            return new Endpoint();
        }
    }
}

using NSubstitute;
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
    public class TextFileEndpointConverterTests
    {
        [Theory]
        [InlineData(true, ",", "test")]
        [InlineData(false, ",", "test")]
        public void TextFileSettingsPluginWithCorrectValuesIsAddedToEndpointWhenFieldsAreSetOnItem(bool columnHeadersInFirstLine, string columnSeparator, string path)
        {
            var model = new ItemModel
            {
                [ItemModel.TemplateID] = TextFileEndpointConverter.TextFileEndpointTemplateId,
                [TextFileEndpointConverter.TemplateFieldColumnHeadersInFirstLine] = columnHeadersInFirstLine ? "1" : "",
                [TextFileEndpointConverter.TemplateFieldColumnSeparator] = columnSeparator,
                [TextFileEndpointConverter.TemplateFieldPath] = path
            };
            var repo = Substitute.For<IItemModelRepository>();
            var converter = new TextFileEndpointConverter(repo);
            var result = converter.Convert(model);
            Assert.True(result.WasConverted);
            Assert.NotNull(result.ConvertedValue);
            var settings = result.ConvertedValue.GetPlugin<TextFileSettings>();
            Assert.NotNull(settings);
            Assert.Equal(columnHeadersInFirstLine, settings.ColumnHeadersInFirstLine);
            Assert.Equal(columnSeparator, settings.ColumnSeparator);
            Assert.Equal(path, settings.Path);
        }
        [Fact]
        public void TextFileSettingsPluginWithDefaultValuesIsAddedToEndpointWhenNoFieldsAreSetOnItem()
        {
            var model = new ItemModel
            {
                [ItemModel.TemplateID] = TextFileEndpointConverter.TextFileEndpointTemplateId
            };
            var repo = Substitute.For<IItemModelRepository>();
            var converter = new TextFileEndpointConverter(repo);
            var result = converter.Convert(model);
            Assert.True(result.WasConverted);
            Assert.NotNull(result.ConvertedValue);
            var settings = result.ConvertedValue.GetPlugin<TextFileSettings>();
            Assert.NotNull(settings);
            Assert.False(settings.ColumnHeadersInFirstLine);
            Assert.Null(settings.ColumnSeparator);
            Assert.Null(settings.Path);
        }
        [Fact]
        public void ItemCannotBeConvertedWhenItIsBasedOnAnUnsupportedTemplate()
        {
            var model = new ItemModel
            {
            };
            var repo = Substitute.For<IItemModelRepository>();
            var converter = new TextFileEndpointConverter(repo);
            var result = converter.Convert(model);
            Assert.False(result.WasConverted);
            Assert.Null(result.ConvertedValue);
        }
    }
}

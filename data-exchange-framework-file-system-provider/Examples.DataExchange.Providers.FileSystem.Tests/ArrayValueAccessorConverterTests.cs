using NSubstitute;
using Sitecore.DataExchange.DataAccess.Readers;
using Sitecore.DataExchange.DataAccess.Writers;
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
    public class ArrayValueAccessorConverterTests
    {
        [Fact]
        public void ArrayValueAccessorHasCorrectPositionWhenFieldsAreSetOnItem()
        {
            var model = new ItemModel
            {
                [ItemModel.TemplateID] = ArrayValueAccessorConverter.ArrayValueAccessorTemplateId,
                [ArrayValueAccessorConverter.TemplateFieldPosition] = "100"
            };
            var repo = Substitute.For<IItemModelRepository>();
            var converter = new ArrayValueAccessorConverter(repo);
            var result = converter.Convert(model);
            Assert.True(result.WasConverted);
            var accessor = result.ConvertedValue;
            Assert.NotNull(accessor);
            Assert.NotNull(accessor.ValueReader);
            Assert.IsType<ArrayValueReader>(accessor.ValueReader);
            var reader = accessor.ValueReader as ArrayValueReader;
            Assert.NotNull(reader);
            Assert.Equal(100, reader.Position);
            Assert.IsType<ArrayValueWriter>(accessor.ValueWriter);
            var writer = accessor.ValueWriter as ArrayValueWriter;
            Assert.NotNull(writer);
            Assert.Equal(100, writer.Position);
        }
        [Theory]
        [InlineData("-100")]
        public void ArrayValueAccessorHasNoReaderOrWriterWhenInvalidPositionValuesAreSetOnItem(string position)
        {
            var model = new ItemModel
            {
                [ItemModel.TemplateID] = ArrayValueAccessorConverter.ArrayValueAccessorTemplateId,
                [ArrayValueAccessorConverter.TemplateFieldPosition] = position
            };
            var repo = Substitute.For<IItemModelRepository>();
            var converter = new ArrayValueAccessorConverter(repo);
            var result = converter.Convert(model);
            Assert.True(result.WasConverted);
            var accessor = result.ConvertedValue;
            Assert.NotNull(accessor);
            Assert.Null(accessor.ValueReader);
            Assert.Null(accessor.ValueWriter);
        }
        [Fact]
        public void ItemCannotBeConvertedWhenItIsBasedOnAnUnsupportedTemplate()
        {
            var model = new ItemModel
            {
            };
            var repo = Substitute.For<IItemModelRepository>();
            var converter = new ArrayValueAccessorConverter(repo);
            var result = converter.Convert(model);
            Assert.False(result.WasConverted);
            Assert.Null(result.ConvertedValue);
        }
    }
}

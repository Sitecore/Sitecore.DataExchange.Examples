using Sitecore.DataExchange.Providers.FileSystem.Extensions;
using Sitecore.DataExchange.Providers.FileSystem.Plugins;
using Sitecore.DataExchange.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Sitecore.DataExchange.Providers.FileSystem.Tests.Extensions
{
    public class EndpointExtensionsTests
    {
        [Fact]
        public void TextFileSettingsIsNotSet()
        {
            var endpoint = new Endpoint();
            Assert.Null(endpoint.GetPlugin<TextFileSettings>());
            Assert.Null(endpoint.GetTextFileSettings());
            Assert.False(endpoint.HasTextFileSettings());
        }
        [Fact]
        public void TextFileSettingsIsSet()
        {
            var endpoint = new Endpoint();
            var plugin = new TextFileSettings();
            endpoint.Plugins.Add(plugin);
            Assert.NotNull(endpoint.GetPlugin<TextFileSettings>());
            Assert.NotNull(endpoint.GetTextFileSettings());
            Assert.Same(plugin, endpoint.GetTextFileSettings());
            Assert.True(endpoint.HasTextFileSettings());
        }
    }
}

using NSubstitute;
using Sitecore.DataExchange.Contexts;
using Sitecore.DataExchange.Models;
using Sitecore.DataExchange.Plugins;
using Sitecore.Services.Core.Diagnostics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Examples.DataExchange.Providers.FileSystem.Tests
{
    public class ReadTextFileStepProcessorTests
    {
        [Fact]
        public void NoIterableDataSettingsPluginAddedToPipelineContextWhenNoEndpointSettingsPluginIsSetOnThePipelineStep()
        {
            var pipelineStep = new PipelineStep { Enabled = true };
            var pipelineContext = new PipelineContext(new PipelineBatchContext());
            var logger = Substitute.For<ILogger>();
            var processor = new ReadTextFileStepProcessor();
            processor.StartProcessing(pipelineStep, pipelineContext, logger);
            Assert.Null(pipelineContext.GetPlugin<IterableDataSettings>());
        }
        [Fact]
        public void NoIterableDataSettingsPluginAddedToPipelineContextWhenNoEndpointIsSetOnTheEndpointSettingsPlugin()
        {
            var pipelineStep = new PipelineStep { Enabled = true };
            pipelineStep.AddPlugin(new EndpointSettings());
            var pipelineContext = new PipelineContext(new PipelineBatchContext());
            var logger = Substitute.For<ILogger>();
            var processor = new ReadTextFileStepProcessor();
            processor.StartProcessing(pipelineStep, pipelineContext, logger);
            Assert.Null(pipelineContext.GetPlugin<IterableDataSettings>());
        }
        [Fact]
        public void NoIterableDataSettingsPluginAddedToPipelineContextWhenNoTextFileSettingsPluginIsSetOnTheEndpoint()
        {
            var endpointFrom = new Endpoint();
            var pipelineStep = new PipelineStep { Enabled = true };
            pipelineStep.AddPlugin(new EndpointSettings { EndpointFrom = endpointFrom });
            var pipelineContext = new PipelineContext(new PipelineBatchContext());
            var logger = Substitute.For<ILogger>();
            var processor = new ReadTextFileStepProcessor();
            processor.StartProcessing(pipelineStep, pipelineContext, logger);
            Assert.Null(pipelineContext.GetPlugin<IterableDataSettings>());
        }
        [Fact]
        public void NoIterableDataSettingsPluginAddedToPipelineContextWhenNoPathIsSetOnTheTextFileSettingsPluginSetOnTheEndpoint()
        {
            var textFileSettings = new TextFileSettings();
            var endpointFrom = new Endpoint();
            endpointFrom.AddPlugin(textFileSettings);
            var pipelineStep = new PipelineStep { Enabled = true };
            pipelineStep.AddPlugin(new EndpointSettings { EndpointFrom = endpointFrom });
            var pipelineContext = new PipelineContext(new PipelineBatchContext());
            var logger = Substitute.For<ILogger>();
            var processor = new ReadTextFileStepProcessor();
            processor.StartProcessing(pipelineStep, pipelineContext, logger);
            Assert.Null(pipelineContext.GetPlugin<IterableDataSettings>());
        }
        [Fact]
        public void IterableDataSettingsPluginAddedToPipelineContext()
        {
            var builder = new StringBuilder();
            builder.AppendLine("a-b-c");
            builder.AppendLine("1-2-3");
            var fileName = Path.GetTempFileName();
            File.WriteAllText(fileName, builder.ToString());
            var textFileSettings = new TextFileSettings
            {
                Path = fileName,
                ColumnHeadersInFirstLine = false,
                ColumnSeparator = "-"
            };
            var endpointFrom = new Endpoint();
            endpointFrom.AddPlugin(textFileSettings);
            var pipelineStep = new PipelineStep { Enabled = true };
            pipelineStep.AddPlugin(new EndpointSettings { EndpointFrom = endpointFrom });
            var pipelineContext = new PipelineContext(new PipelineBatchContext());
            var logger = Substitute.For<ILogger>();
            var processor = new ReadTextFileStepProcessor();
            processor.StartProcessing(pipelineStep, pipelineContext, logger);
            var dataSettings = pipelineContext.GetPlugin<IterableDataSettings>();
            Assert.NotNull(dataSettings);
            Assert.NotNull(dataSettings.Data);
            var count = 0;
            foreach (var data in dataSettings.Data)
            {
                Assert.NotNull(data);
                Assert.IsType<string[]>(data);
                var dataAsArray = data as string[];
                Assert.NotNull(dataAsArray);
                Assert.Equal(3, dataAsArray.Length);
                count++;
            }
            Assert.Equal(2, count);
            File.Delete(fileName);
        }
    }
}

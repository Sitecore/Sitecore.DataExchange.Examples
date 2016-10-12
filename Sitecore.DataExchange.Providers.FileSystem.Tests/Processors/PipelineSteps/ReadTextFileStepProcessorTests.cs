using Sitecore.DataExchange.Contexts;
using Sitecore.DataExchange.Extensions;
using Sitecore.DataExchange.Models;
using Sitecore.DataExchange.Plugins;
using Sitecore.DataExchange.Providers.FileSystem.Plugins;
using Sitecore.DataExchange.Providers.FileSystem.Processors.PipelineSteps;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Sitecore.DataExchange.Providers.FileSystem.Tests.Processors.PipelineSteps
{
    public class ReadTextFileStepProcessorTests
    {
        [Fact]
        public void NoEndpointSetOnPipelineStep()
        {
            var step = new PipelineStep { Enabled = true };
            var context = new PipelineContext(new PipelineBatchContext());
            var processor = new ReadTextFileStepProcessor();
            processor.Process(step, context);
            Assert.False(context.HasIterableDataSettings());
        }
        [Fact]
        public void PathNotSetOnEndpoint()
        {
            var context = DoProcess(new TextFileSettings());
            Assert.False(context.HasIterableDataSettings());
        }
        [Fact]
        public void PathSetOnPipelineStepDoesNotExist()
        {
            var context = DoProcess(new TextFileSettings { Path = "PATH-DOES-NOT-EXIST" });
            Assert.False(context.HasIterableDataSettings());
        }
        [Fact]
        public void NoColumnSeparatorSetOnPipelineStep()
        {
            var path = Path.GetTempFileName();
            using (var writer = new StreamWriter(path))
            {
                writer.WriteLine("COLUMN1,COLUMN2");
            }
            var context = DoProcess(new TextFileSettings { Path = path, ColumnHeadersInFirstLine = false });
            Assert.True(context.HasIterableDataSettings());
            var data = ConvertDataToStringArray(context.GetIterableDataSettings().Data);
            Assert.Equal(1, data.Length);
            Assert.Equal(1, data.First().Length);
            Assert.Equal("COLUMN1,COLUMN2", data.First()[0]);
        }
        [Fact]
        public void FileWithColumnHeaders()
        {
            var path = Path.GetTempFileName();
            using (var writer = new StreamWriter(path))
            {
                writer.WriteLine("HEADER1,HEADER2");
                writer.WriteLine("COLUMN1,COLUMN2");
            }
            var context = DoProcess(new TextFileSettings { Path = path, ColumnSeparator = ",", ColumnHeadersInFirstLine = true });
            Assert.True(context.HasIterableDataSettings());
            var data = ConvertDataToStringArray(context.GetIterableDataSettings().Data);
            Assert.Equal(1, data.Length);
            Assert.Equal(2, data.First().Length);
            Assert.Equal("COLUMN1", data.First()[0]);
            Assert.Equal("COLUMN2", data.First()[1]);
        }
        [Fact]
        public void FileWithNoColumnHeaders()
        {
            var path = Path.GetTempFileName();
            using (var writer = new StreamWriter(path))
            {
                writer.WriteLine("COLUMN1,COLUMN2");
            }
            var context = DoProcess(new TextFileSettings { Path = path, ColumnSeparator = ",", ColumnHeadersInFirstLine = false });
            Assert.True(context.HasIterableDataSettings());
            var data = ConvertDataToStringArray(context.GetIterableDataSettings().Data);
            Assert.Equal(1, data.Length);
            Assert.Equal(2, data.First().Length);
            Assert.Equal("COLUMN1", data.First()[0]);
            Assert.Equal("COLUMN2", data.First()[1]);
        }
        private PipelineContext DoProcess(TextFileSettings settings)
        {
            var endpoint = new Endpoint();
            endpoint.Plugins.Add(settings);
            var step = new PipelineStep { Enabled = true };
            step.Plugins.Add(new EndpointSettings { EndpointFrom = endpoint });
            //
            var processor = new ReadTextFileStepProcessor();
            var context = new PipelineContext(new PipelineBatchContext());
            processor.Process(step, context);
            return context;
        }
        private string[][] ConvertDataToStringArray(IEnumerable data)
        {
            var list = new List<string[]>();
            foreach (var row in data)
            {
                list.Add(row as string[]);
            }
            return list.ToArray();
        }
    }
}

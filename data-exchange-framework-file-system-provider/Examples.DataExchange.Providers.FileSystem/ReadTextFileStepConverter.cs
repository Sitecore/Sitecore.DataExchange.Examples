using Sitecore.DataExchange.Attributes;
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

namespace Examples.DataExchange.Providers.FileSystem
{
    [SupportedIds(ReadTextFileStepTemplateId)]
    public class ReadTextFileStepConverter : BasePipelineStepConverter
    {
        public const string ReadTextFileStepTemplateId = "{3CB5EF31-4804-4844-9A5B-E3214BDF1B2B}";
        public const string TemplateFieldEndpointFrom = "EndpointFrom";
        public ReadTextFileStepConverter(IItemModelRepository repository) : base(repository)
        {
        }
        protected override void AddPlugins(ItemModel source, PipelineStep pipelineStep)
        {
            //
            //create the plugin
            var settings = new EndpointSettings
            {
                //
                //populate the plugin using values from the item
                EndpointFrom = this.ConvertReferenceToModel<Endpoint>(source, TemplateFieldEndpointFrom)
            };
            //
            //add the plugin to the pipeline step
            pipelineStep.AddPlugin(settings);
        }
    }
}
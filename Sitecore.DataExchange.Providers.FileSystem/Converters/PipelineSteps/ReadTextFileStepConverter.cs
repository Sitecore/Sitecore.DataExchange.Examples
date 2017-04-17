using Sitecore.DataExchange.Converters.PipelineSteps;
using Sitecore.Services.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sitecore.DataExchange.Repositories;
using Sitecore.DataExchange.Models;
using Sitecore.DataExchange.Providers.FileSystem.Models.ItemModels.PipelineSteps;
using Sitecore.DataExchange.Plugins;

namespace Sitecore.DataExchange.Providers.FileSystem.Converters.PipelineSteps
{
    public class ReadTextFileStepConverter : BasePipelineStepConverter
    {
        private static readonly Guid TemplateId = Guid.Parse("{BB3E3BAB-7497-4B84-A13B-46DEC07002B3}");
        public ReadTextFileStepConverter(IItemModelRepository repository) : base(repository)
        {
            this.SupportedTemplateIds.Add(TemplateId);
        }
        protected override void AddPlugins(ItemModel source, PipelineStep pipelineStep)
        {
            AddEndpointSettings(source, pipelineStep);
        }
        private void AddEndpointSettings(ItemModel source, PipelineStep pipelineStep)
        {
            var settings = new EndpointSettings();
            var endpointFrom = base.ConvertReferenceToModel<Endpoint>(source, ReadTextFileStepItemModel.EndpointFrom);
            if (endpointFrom != null)
            {
                settings.EndpointFrom = endpointFrom;
            }
            pipelineStep.Plugins.Add(settings);
        }
    }
}

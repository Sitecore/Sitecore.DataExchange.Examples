namespace Sitecore.DataExchange.Examples.Sitecore.SelfieLogger
{
    using System;
    using System.Linq;
    using Converters.PipelineSteps;
    using DataAccess;
    using Extensions;
    using Models;
    using Plugins;
    using Repositories;
    using Services.Core.Model;

    public class SelfieLoggerPipelineStepConverter : BasePipelineStepConverter
    {
        public const string EndpointTo = "EndpointTo";
        public const string ValueAccessors = "ValueAccessors";
        public const string ObjectLocation = "ObjectLocation";

        private static readonly Guid TemplateId = Guid.Parse("{B4B7D23D-1994-4B4F-B512-53305AFAAE07}");

        public SelfieLoggerPipelineStepConverter(IItemModelRepository repository) : base(repository)
        {
            SupportedTemplateIds.Add(TemplateId);
        }
        protected override void AddPlugins(ItemModel source, PipelineStep pipelineStep)
        {
            AddEndpointSettings(source,pipelineStep);
            AddSelfieSettings(source, pipelineStep);
        }
        private void AddEndpointSettings(ItemModel source, PipelineStep pipelineStep)
        {
            var settings = new EndpointSettings();
            var endpointTo = ConvertReferenceToModel<Endpoint>(source, EndpointTo);
            if (endpointTo != null)
            {
                settings.EndpointTo = endpointTo;
            }
            pipelineStep.AddPlugin(settings);
        }

        private void AddSelfieSettings(ItemModel source, PipelineStep pipelineStep)
        {
            var settings = new SelfieLoggerSettings
            {
                ObjectLocation = GetStringValue(source, ObjectLocation),
                SelfItemId = source.GetItemId()
            };

            var accessors = ConvertReferencesToModels<IValueAccessor>(source, ValueAccessors);
            if (accessors != null)
            {
                settings.ValueAccessors = accessors.ToList();
            }

            pipelineStep.AddPlugin(settings);
        }
    }
}

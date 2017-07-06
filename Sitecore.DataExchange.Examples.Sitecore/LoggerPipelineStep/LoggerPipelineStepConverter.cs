namespace Sitecore.DataExchange.Examples.Sitecore.LoggerPipelineStep
{
    using System;
    using System.Linq;
    using global::Sitecore.DataExchange.Converters.PipelineSteps;
    using global::Sitecore.DataExchange.DataAccess;
    using global::Sitecore.DataExchange.Models;
    using global::Sitecore.DataExchange.Repositories;
    using global::Sitecore.Services.Core.Model;

    public class LoggerPipelineStepConverter : BasePipelineStepConverter
    {
        public const string ValueAccessors = "ValueAccessors";
        public const string ObjectLocation = "ObjectLocation";

        private static readonly Guid TemplateId = Guid.Parse("{CCC46873-79B8-4DE0-9061-81715C9B9847}");

        public LoggerPipelineStepConverter(IItemModelRepository repository) : base(repository)
        {
            SupportedTemplateIds.Add(TemplateId);
        }
        protected override void AddPlugins(ItemModel source, PipelineStep pipelineStep)
        {
            AddSelfieSettings(source, pipelineStep);
        }

        private void AddSelfieSettings(ItemModel source, PipelineStep pipelineStep)
        {
            var settings = new LoggerSettings
            {
                ObjectLocation = GetStringValue(source, ObjectLocation)
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

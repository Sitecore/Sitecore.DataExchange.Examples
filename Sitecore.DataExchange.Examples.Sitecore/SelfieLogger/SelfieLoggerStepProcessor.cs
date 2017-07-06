namespace Sitecore.DataExchange.Examples.Sitecore.SelfieLogger
{
    using System;
    using System.Linq;
    using Contexts;
    using DataAccess;
    using Extensions;
    using Models;
    using Processors.PipelineSteps;
    using Providers.Sc.Plugins;
    using Services.Core.Diagnostics;

    public class SelfieLoggerStepProcessor : BasePipelineStepProcessor
    {

        public override void Process(PipelineStep pipelineStep, PipelineContext pipelineContext, ILogger logger)
        {
            if (pipelineStep == null)
            {
                throw new ArgumentNullException(nameof(pipelineStep));
            }

            if (pipelineContext == null)
            {
                throw new ArgumentNullException(nameof(pipelineContext));
            }

            var endpointSettings = pipelineStep.GetEndpointSettings();

            if (endpointSettings == null)
            {
                return;
            }
            
            var repositorySettings = endpointSettings.EndpointTo.GetPlugin<ItemModelRepositorySettings>();
            var repository = repositorySettings?.ItemModelRepository;

            if (repository == null)
            {
                return;
            }
            var selfieLoggerSettings = pipelineStep.GetPlugin<SelfieLoggerSettings>();

            if (selfieLoggerSettings == null)
            {
                return;
            }

            var itemId = selfieLoggerSettings.SelfItemId;
            var itemModel = repository.Get(itemId);
            
            itemModel[SelfieLoggerSettings.SelfieField] += Environment.NewLine + DateTime.Now.ToString("t") + " : ";

            var objectLocation = selfieLoggerSettings.ObjectLocation;
            var objectModel = GetObjectFromPipelineContext(objectLocation, pipelineContext, logger);

            if ((objectLocation == null) || (objectLocation == String.Empty) || (objectModel == null))
            {
                itemModel[SelfieLoggerSettings.SelfieField] += "No object set;";
                repository.Update(itemId, itemModel);
                return;
            }
            if (selfieLoggerSettings.ValueAccessors.Any())
            {
                var i = 0;
                foreach (var valueAccessor in selfieLoggerSettings.ValueAccessors)
                {
                    var data = valueAccessor.ValueReader.Read(objectModel, new DataAccessContext());
                    if (data.WasValueRead)
                    {
                        itemModel[SelfieLoggerSettings.SelfieField] += ( i > 0 ? ", " : String.Empty) + data.ReadValue;
                    }
                    i++;
                }
                repository.Update(itemId, itemModel);
            }
        }

    }
}

namespace Sitecore.DataExchange.Examples.Sitecore.LoggerPipelineStep
{
    using System;
    using System.Linq;
    using Contexts;
    using DataAccess;
    using Models;
    using Processors.PipelineSteps;
    using Services.Core.Diagnostics;

    public class LoggerStepProcessor : BasePipelineStepProcessor
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

            var loggerSettings = pipelineStep.GetPlugin<LoggerSettings>();

            if (loggerSettings == null)
            {
                return;
            }

            var logMessage = String.Empty;

            logMessage += DateTime.Now.ToString("t") + " : ";

            var objectLocation = loggerSettings.ObjectLocation;
            var objectModel = GetObjectFromPipelineContext(objectLocation, pipelineContext, logger);

            if ((objectLocation == null) || (objectLocation == String.Empty) || (objectModel == null))
            {
                logMessage += "No object set;";
            }
            else
            {
                if (loggerSettings.ValueAccessors.Any())
                {
                    var i = 0;
                    foreach (var valueAccessor in loggerSettings.ValueAccessors)
                    {
                        var data = valueAccessor.ValueReader.Read(objectModel, new DataAccessContext());
                        if (data.WasValueRead)
                        {
                            logMessage += (i > 0 ? ", " : String.Empty) + data.ReadValue;
                        }
                        i++;
                    }
                }
            }
            logger.Info(logMessage + Environment.NewLine);
            return;

        }

    }
}

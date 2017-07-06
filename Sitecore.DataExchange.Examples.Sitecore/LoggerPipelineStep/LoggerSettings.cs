namespace Sitecore.DataExchange.Examples.Sitecore.LoggerPipelineStep
{
    using System.Collections.Generic;
    using DataAccess;

    public class LoggerSettings : IPlugin
    {
        public List<IValueAccessor> ValueAccessors { get; set; }
        public string ObjectLocation { get; set; }
    }
}

namespace Sitecore.DataExchange.Examples.Sitecore.SelfieLogger
{
    using System;
    using System.Collections.Generic;
    using DataAccess;

    public class SelfieLoggerSettings : IPlugin
    {
        public List<IValueAccessor> ValueAccessors { get; set; }
        public string ObjectLocation { get; set; }
        public Guid SelfItemId { get; set; }
        public const string SelfieField = "SelfieField";

    }
}

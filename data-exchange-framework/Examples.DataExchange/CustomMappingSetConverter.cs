using Sitecore.DataExchange;
using Sitecore.DataExchange.Attributes;
using Sitecore.DataExchange.Converters;
using Sitecore.DataExchange.Repositories;
using Sitecore.Services.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examples.DataExchange
{
    [SupportedIds("{73E9A7D8-33A0-4D28-8A97-1A49C7CFD3ED}")]
    public class CustomMappingSetConverter : BaseItemModelConverter<CustomMappingSet>
    {
        public CustomMappingSetConverter(IItemModelRepository repository) : base(repository)
        {
        }
        private static CustomMappingSet _instance = new CustomMappingSet();
        protected override ConvertResult<CustomMappingSet> ConvertSupportedItem(ItemModel source)
        {
            return ConvertResult<CustomMappingSet>.PositiveResult(_instance);
        }
    }
}

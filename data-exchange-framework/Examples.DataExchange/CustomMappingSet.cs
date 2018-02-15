using Sitecore.DataExchange.DataAccess;
using Sitecore.DataExchange.DataAccess.Mappings;
using Sitecore.DataExchange.DataAccess.Readers;
using Sitecore.DataExchange.DataAccess.Writers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examples.DataExchange
{
    public class CustomMappingSet : IMappingSet
    {
        public CustomMappingSet() { }

        public ICollection<IMapping> Mappings => throw new NotImplementedException();

        private static IValueReader ValueReader1 = new PropertyValueReader("Name");
        private static IValueWriter ValueWriter1 = new PropertyValueWriter("Description");
        private static IValueWriter ValueWriter2 = new PropertyValueWriter("LastUpdated");
        public bool Run(MappingContext context)
        {
            if (context == null || context.Source == null || context.Target == null)
            {
                return false;
            }
            ApplyMapping(ValueReader1, ValueWriter1, context, new Mapping { Identifier = "Mapping1" });
            ApplyMapping(DateTime.Now.Date, ValueWriter2, context, new Mapping { Identifier = "Mapping2" });
            return true;
        }
        protected void ApplyMapping(IValueReader reader, IValueWriter writer, MappingContext context, IMapping mapping)
        {
            var context2 = new DataAccessContext();
            var result = reader.Read(context.Source, context2);
            if (!result.WasValueRead)
            {
                context.RunFail.Add(mapping);
                return;
            }
            ApplyMapping(result.ReadValue, writer, context, mapping);
        }
        protected void ApplyMapping(object value, IValueWriter writer, MappingContext context, IMapping mapping)
        {
            var context2 = new DataAccessContext();
            var writeSuccess = writer.Write(context.Target, value, context2);
            if (writeSuccess)
            {
                context.RunSuccess.Add(mapping);
                return;
            }
            context.RunFail.Add(mapping);
        }
    }
}

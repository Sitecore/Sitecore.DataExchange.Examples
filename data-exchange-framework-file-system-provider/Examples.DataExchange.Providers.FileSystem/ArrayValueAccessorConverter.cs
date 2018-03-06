using Sitecore.DataExchange.Attributes;
using Sitecore.DataExchange.Converters.DataAccess.ValueAccessors;
using Sitecore.DataExchange.DataAccess;
using Sitecore.DataExchange.DataAccess.Readers;
using Sitecore.DataExchange.DataAccess.Writers;
using Sitecore.DataExchange.Repositories;
using Sitecore.Services.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examples.DataExchange.Providers.FileSystem
{
    [SupportedIds(ArrayValueAccessorTemplateId)]
    public class ArrayValueAccessorConverter : ValueAccessorConverter
    {
        public const string ArrayValueAccessorTemplateId = "{1BFD5F32-917D-414E-9A94-177C7EC0ACC9}";
        public const string TemplateFieldPosition = "Position";
        public ArrayValueAccessorConverter(IItemModelRepository repository) : base(repository)
        {
        }
        protected override IValueReader GetValueReader(ItemModel source)
        {
            var reader = base.GetValueReader(source);
            if (reader == null)
            {
                var position = this.GetIntValue(source, TemplateFieldPosition);
                if (position < 0)
                {
                    return null;
                }
                reader = new ArrayValueReader(position);
            }
            return reader;
        }
        protected override IValueWriter GetValueWriter(ItemModel source)
        {
            var writer = base.GetValueWriter(source);
            if (writer == null)
            {
                var position = this.GetIntValue(source, TemplateFieldPosition);
                if (position < 0)
                {
                    return null;
                }
                writer = new ArrayValueWriter(position);
            }
            return writer;
        }
    }
}

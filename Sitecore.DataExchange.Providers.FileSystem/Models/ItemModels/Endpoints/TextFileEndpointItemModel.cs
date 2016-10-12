using Sitecore.Services.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sitecore.DataExchange.Providers.FileSystem.Models.ItemModels.Endpoints
{
    public class TextFileEndpointItemModel : ItemModel
    {
        public const string Path = "Path";
        public const string ColumnSeparator = "ColumnSeparator";
        public const string ColumnHeadersInFirstLine = "ColumnHeadersInFirstLine";
    }
}

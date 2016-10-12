using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sitecore.DataExchange.Providers.FileSystem.Plugins
{
    public class TextFileSettings : Sitecore.DataExchange.IPlugin
    {
        public TextFileSettings()
        {
        }
        public string Path { get; set; }
        public string ColumnSeparator { get; set; }
        public bool ColumnHeadersInFirstLine { get; set; }
    }
}

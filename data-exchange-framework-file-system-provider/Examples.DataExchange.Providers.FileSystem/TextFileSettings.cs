using Sitecore.DataExchange;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examples.DataExchange.Providers.FileSystem
{
    public class TextFileSettings : IPlugin
    {
        public TextFileSettings() { }
        public bool ColumnHeadersInFirstLine { get; set; }
        public string ColumnSeparator { get; set; }
        public string Path { get; set; }
    }
}

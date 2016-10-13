using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleMenuMaker
{
    public static class MenuManagerUtils
    {
        public static string EnsureTextLength(int maxLength, string value, params string[] args)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return value;
            }
            var leadingSpaceCount = value.TakeWhile(c => char.IsWhiteSpace(c)).Count();
            if (leadingSpaceCount == 0)
            {
                return EnsureTextLengthNoLeadingWhitespace(maxLength, value, args);
            }
            var value2 = new string('-', leadingSpaceCount) + value.Substring(leadingSpaceCount);
            var value3 = EnsureTextLengthNoLeadingWhitespace(maxLength, value2, args);
            var value4 = value.Substring(0, leadingSpaceCount) + value3.Substring(leadingSpaceCount);
            return value4;
        }
        private static string EnsureTextLengthNoLeadingWhitespace(int maxLength, string value, params string[] args)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return value;
            }
            var count = 0;
            var lines = string.Format(value, args).Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).GroupBy(w => (count += w.Length + 1) / maxLength).Select(g => string.Join(" ", g));
            return string.Join("\n", lines);
        }
    }
}

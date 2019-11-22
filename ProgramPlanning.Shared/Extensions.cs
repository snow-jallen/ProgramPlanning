using System;
using System.Collections.Generic;
using System.Text;

namespace ProgramPlanning.Shared
{
    public static class Extensions
    {
        public static string Truncate(this string content, int maxLength)
        {
            if (string.IsNullOrWhiteSpace(content))
                return content;

            var length = Math.Min(content.Length, maxLength);
            var shorter = content.Substring(0, length);
            if (shorter.Length > 4)
                shorter = $"{shorter.Substring(0, shorter.Length - 3)}...";
            return shorter;
        }
    }
}

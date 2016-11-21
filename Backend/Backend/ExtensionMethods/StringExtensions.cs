using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HtmlAgilityPack;

namespace Backend.ExtensionMethods
{
    public static class StringExtensions
    {
        public static bool IsValidHtml(this String str)
        {
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(str);

            return !doc.ParseErrors.Any();
        }
    }
}
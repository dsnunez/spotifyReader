using System;
using System.Web.Mvc;

namespace spotifyAcid.ExtendedHelpers
{
    public static class BootstrapHelpers
    {
        public static MvcHtmlString GlyphIcon(this HtmlHelper helper, string icon,
            bool float_right = false, string htmlIdAttribute = "", string htmlTitleAttribute = "", string extraCss = "")
        {
            string[] extraAttr = new string[]
            {
                float_right                                 ? "style=\"float:right\"" : "",
                !String.IsNullOrEmpty(htmlIdAttribute)      ? "id=\""+htmlIdAttribute+"\"":"",
                !String.IsNullOrEmpty(htmlTitleAttribute)   ? "title=\""+htmlTitleAttribute+"\"":""
            };
            string extraAttributes = String.Join(" ", extraAttr);

            if (!String.IsNullOrEmpty(extraCss))
                extraCss = " " + extraCss;

            return MvcHtmlString.Create(String.Format("<span class=\"glyphicon glyphicon-{0}{2}\"{1}></span>", icon, extraAttributes, extraCss));
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace spotifyAcid.ExtendedHelpers
{
    public static class BootstrapHelpers
    {
        public static MvcHtmlString GlyphIcon(this HtmlHelper helper, string icon,
            bool float_right = false, string htmlIdAttribute = "", string htmlTitleAttribute = "", bool tooltip = false, string extraCss = "")
        {
            string[] extraAttr = new string[]
            {
                float_right                                 ? "style=\"float:right\"" : "",
                !String.IsNullOrEmpty(htmlIdAttribute)      ? "id=\""+htmlIdAttribute+"\"":"",
                !String.IsNullOrEmpty(htmlTitleAttribute)   ? "title=\""+htmlTitleAttribute+"\"":"",
                tooltip                                     ? "data-toggle=\"tooltip\"":""
            };
            string extraAttributes = String.Join(" ", extraAttr);

            if (!String.IsNullOrEmpty(extraCss))
                extraCss = " " + extraCss;

            return MvcHtmlString.Create(String.Format("<span class=\"glyphicon glyphicon-{0}{2}\"{1}></span>", icon, extraAttributes, extraCss));
        }

        public static MvcHtmlString GlyphIconOnLeft(this HtmlHelper helper, string icon)
        {
            return MvcHtmlString.Create(helper.GlyphIcon(icon) + "&nbsp;");
        }

        public static MvcHtmlString GlyphIconOnRight(this HtmlHelper helper, string icon)
        {
            return MvcHtmlString.Create("&nbsp;" + helper.GlyphIcon(icon, true));
        }

        public static MvcHtmlString HelpTooltipRight(this HtmlHelper helper, string tooltipTxt, string iconName = "question-sign")
        {
            return helper.GlyphIcon(iconName, false, "LessonSearchApproversTooltip", tooltipTxt, true, "with-right-tooltip");
        }
    }
}
using ETestCRM.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Collections.Generic;
using System.Security.Policy;

namespace ETestCRM.Infrastructure.TagHelpers
{
    [HtmlTargetElement("div", Attributes = "sort-model")]
    public class DateLinkTagHelper : TagHelper
    {
        private IUrlHelperFactory urlHelperFactory;
        public DateLinkTagHelper(IUrlHelperFactory helperFactory)
        {
            urlHelperFactory = helperFactory;
        }
        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext ViewContext { get; set; }
        public PagingInfo SortModel { get; set; }
        public string PageAction { get; set; }

        [HtmlAttributeName(DictionaryAttributePrefix = "page-url-")]
        public Dictionary<string, object> PageUrlValues { get; set; } = new Dictionary<string, object>();
        public bool SortClassesEnabled { get; set; } = false;
        public string SortClass { get; set; }
        public string SortClassNormal { get; set; }
        public string SortClassSelected { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            IUrlHelper urlHelper = urlHelperFactory.GetUrlHelper(ViewContext);
            TagBuilder result = new TagBuilder("div");

            string[] valsort = { "all", "today", "toweek" };


            for (int i = 0; i <= 2; i++)
            {
                TagBuilder tag = new TagBuilder("a");
                PageUrlValues["sortTime"] = valsort[i];
                tag.Attributes["href"] = urlHelper.Action(PageAction, PageUrlValues /*new { sortTime = valsort[i] }*/);
                if (SortClassesEnabled)
                {
                    tag.AddCssClass(SortClass);
                    tag.AddCssClass(valsort[i] == SortModel.CurrentTime ? SortClassSelected : SortClassNormal);
                }
                switch(valsort[i])
                {
                    case "all" : tag.InnerHtml.Append("всего"); break;
                    case "today": tag.InnerHtml.Append("на сегодня"); break;
                    case "toweek": tag.InnerHtml.Append("на неделю"); break;
                }
                result.InnerHtml.AppendHtml(tag);
            }
            output.Content.AppendHtml(result.InnerHtml);
        }
    }
}

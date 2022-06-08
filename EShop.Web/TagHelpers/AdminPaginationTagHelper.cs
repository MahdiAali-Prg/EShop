using EShop.Common.DTOs;
using EShop.Common.Services.Url;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.AspNetCore.Routing;

namespace EShop.Web.TagHelpers
{
    [HtmlTargetElement("div", Attributes = "page-info")]
    public class AdminPaginationTagHelper : TagHelper
    {
        private IUrlHelperFactory _urlHelperFactory;

        public AdminPaginationTagHelper(IUrlHelperFactory urlHelperFactory)
        {
            _urlHelperFactory = urlHelperFactory;
        }

        [HtmlAttributeName("area")]
        public string Area { get; set; }

        [HtmlAttributeName("controller")]
        public string Controller { get; set; }

        [HtmlAttributeName("action")]
        public string Action { get; set; }

        [HtmlAttributeName("page-info")]
        public PaginationInfo PaginationInfo { get; set; }


        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext ViewContext { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            TagBuilder ul = new TagBuilder("ul");
            ul.AddCssClass("pagination");


            for (int index = 0; index < PaginationInfo.TotalPages; index++)
            {
                ul.InnerHtml.AppendHtml(ListItem(index + 1));
            }

            output.Content.AppendHtml(ul);
        }

        private TagBuilder ListItem(int value)
        {
            TagBuilder li = new TagBuilder("li");
            li.AddCssClass("paginate_button page-item");
            li.AddCssClass(value == PaginationInfo.CurrentPage ? "active" : "");
            li.InnerHtml.AppendHtml(ListItemLink(value, new {pageId = value}));
            return li;
        }

        private TagBuilder ListItemLink(int value, object routeValue)
        {
            UrlGenerator generator = new UrlGenerator(Area, Controller, Action, routeValue);
            TagBuilder a = new TagBuilder("a");
            a.AddCssClass("page-link");
            a.Attributes.Add("href", generator.Generate(_urlHelperFactory, ViewContext));
            a.InnerHtml.Append($"{value}");
            return a;
        }
    }
}

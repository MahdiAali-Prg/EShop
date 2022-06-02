using System.Collections.Generic;
using System.Security.Permissions;
using EShop.Common.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace EShop.Web.TagHelpers
{
    [HtmlTargetElement("nav", Attributes = "page-info, root-action, root-controller")]
    public class RootPaginationTagHelper : TagHelper
    {
        private readonly IUrlHelperFactory _urlHelperFactory;
        public RootPaginationTagHelper(IUrlHelperFactory urlHelperFactory)
        {
            _urlHelperFactory = urlHelperFactory;
        }

        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext ViewContext { get; set; }

        [HtmlAttributeName("root-controller")]
        public string RootController { get; set; }

        [HtmlAttributeName("root-action")]
        public string RootAction { get; set; }

        [HtmlAttributeName("page-info")]
        public PaginationInfo PaginationInfo { get; set; }


        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            IUrlHelper urlHelper = _urlHelperFactory.GetUrlHelper(ViewContext);
            TagBuilder ul = new TagBuilder("ul");
            ul.AddCssClass("pagination");
            ul.InnerHtml.AppendHtml(AddPrevious(urlHelper.Action(RootAction, RootController, new { pageId = PaginationInfo.CurrentPage > 1 ? PaginationInfo.CurrentPage - 1 : PaginationInfo.CurrentPage })));
            for (int index = 0; index < PaginationInfo.TotalPages; index++)
            {
                TagBuilder li = ListItem();
                TagBuilder a = ListItemLink(urlHelper.Action(RootAction, RootController, new { pageId = index + 1 }), index + 1);
                a.AddCssClass(PaginationInfo.CurrentPage == index + 1 ? "active" : null);
                li.InnerHtml.AppendHtml(a);
                ul.InnerHtml.AppendHtml(li);
            }
            ul.InnerHtml.AppendHtml(AddNext(urlHelper.Action(RootAction, RootController, new { pageId =  PaginationInfo.CurrentPage == PaginationInfo.TotalPages ? PaginationInfo.CurrentPage : PaginationInfo.CurrentPage + 1})));
            output.Content.AppendHtml(ul);
        }

        private TagBuilder ListItem()
        {
            TagBuilder li = new TagBuilder("li");
            li.AddCssClass("page-item");
            return li;
        }

        private TagBuilder ListItemLink(string href, int? value = null, string ariaLabel = null)
        {
            TagBuilder a = new TagBuilder("a");
            a.AddCssClass("page-link");
            if (value != null)
            {
                a.InnerHtml.Append(value.ToString());
            }

            if (ariaLabel != null)
            {
                a.Attributes.Add("aria-label", ariaLabel);
            }
            a.Attributes.Add("href", href);
            return a;
        }

        private TagBuilder AddPrevious(string linkValue)
        {
            TagBuilder li = ListItem();
            TagBuilder a = ListItemLink(linkValue, ariaLabel: "Previous");
            TagBuilder span = new TagBuilder("span");
            span.Attributes.Add("aria-hidden", "true");
            span.InnerHtml.Append("«");
            a.InnerHtml.AppendHtml(span);
            li.InnerHtml.AppendHtml(a);
            return li;
        }

        private TagBuilder AddNext(string linkValue)
        {
            TagBuilder li = ListItem();
            TagBuilder a = ListItemLink(linkValue, ariaLabel:"Next");
            TagBuilder span = new TagBuilder("span");
            span.Attributes.Add("aria-hidden", "true");
            span.InnerHtml.Append("»");
            a.InnerHtml.AppendHtml(span);
            li.InnerHtml.AppendHtml(a);
            return li;
        }
    }
}

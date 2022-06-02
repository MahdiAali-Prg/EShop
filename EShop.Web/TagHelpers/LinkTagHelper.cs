using System.Collections.Generic;
using System.Text;
using EShop.Common.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace EShop.Web.TagHelpers
{
    [HtmlTargetElement("a", Attributes = "controller,action")]
    public class LinkTagHelper : TagHelper
    {
        private IUrlHelperFactory _urlHelperFactory;

        public LinkTagHelper(IUrlHelperFactory urlHelperFactory)
        {
            _urlHelperFactory = urlHelperFactory;
        }


        [HtmlAttributeName("controller")]
        public string Controller { get; set; }

        [HtmlAttributeName("action")]
        public string Action { get; set; }

        [HtmlAttributeName("area")]
        public string Area { get; set; }

        [HtmlAttributeName(DictionaryAttributePrefix = "link-value-")]
        public Dictionary<string, object> Values { get; set; } = new Dictionary<string, object>();

        [HtmlAttributeNotBound]
        [ViewContext]
        public ViewContext ViewContext { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            UrlGenerator urlGenerator = new UrlGenerator(Area, Controller, Action, Values);
            output.Attributes.Add("href", urlGenerator.Generate(_urlHelperFactory, ViewContext));
        }
    }
}

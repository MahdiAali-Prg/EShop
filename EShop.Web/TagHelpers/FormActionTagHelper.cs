using EShop.Common.Services.Url;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace EShop.Web.TagHelpers
{
    [HtmlTargetElement("form", Attributes = "form-controller,form-action")]
    public class FormActionTagHelper : TagHelper
    {
        private IUrlHelperFactory _urlHelperFactory;

        public FormActionTagHelper(IUrlHelperFactory urlHelperFactory)
        {
            _urlHelperFactory = urlHelperFactory;
        }

        [HtmlAttributeName("form-controller")]
        public string FormController { get; set; }

        [HtmlAttributeName("form-action")]
        public string FormAction { get; set; }

        [HtmlAttributeName("form-area")]
        public string FormArea { get; set; }


        [HtmlAttributeNotBound]
        [ViewContext]
        public ViewContext ViewContext { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            UrlGenerator urlGenerator = new UrlGenerator(FormArea, FormController, FormAction);
            output.Attributes.Add("action", urlGenerator.Generate(_urlHelperFactory, ViewContext));
        }
    }
}

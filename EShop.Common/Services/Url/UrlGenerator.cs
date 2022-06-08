using System;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;

namespace EShop.Common.Services.Url
{
    public class UrlGenerator
    {
        #region Construntors

        public UrlGenerator(string area, string controller, string action) : this(area, controller, action, null)
        {
        }
        public UrlGenerator(string controller, string action, object value) : this(null, controller, action, null)
        {
        }
        public UrlGenerator(string controller, string action) : this(null, controller, action, null)
        {
        }
        public UrlGenerator(string controller) : this(null, controller, "Index", null)
        {
        }

        public UrlGenerator(string area, string controller, string action, object value)
        {
            _area = area;
            _controller = controller;
            _action = action;
            _value = value;
        }

        #endregion

        private readonly string _area;
        private readonly string _controller;
        private readonly string _action;
        private readonly object _value;

        public string Generate(IUrlHelperFactory urlHelperFactory, ActionContext context)
        {
            if (urlHelperFactory != null)
            {
                StringBuilder result = new StringBuilder(_area != null ? $"/{_area}" : "" );

                IUrlHelper urlHelper = urlHelperFactory.GetUrlHelper(context);
                result.Append(urlHelper.Action(_action, _controller, _value));
                return result.ToString();
            }

            throw new ArgumentNullException("urlHelperFactory", "method parameter can't be null");
        }
    }
}

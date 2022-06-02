using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Moq;

namespace EShop.Test.Utilities.UrlHelper
{
    internal class UrlHelperFactoryMaker
    {
        public static IUrlHelperFactory GetUrlHelperFactory(string returnUrl)
        {
            var urlHelper = new Mock<IUrlHelper>();
            urlHelper.Setup(s => s.Action(It.IsAny<UrlActionContext>()))
                .Returns(returnUrl);

            var urlHelperFactory = new Mock<IUrlHelperFactory>();
            urlHelperFactory.Setup(s => s.GetUrlHelper(It.IsAny<ActionContext>()))
                .Returns(urlHelper.Object);

            return urlHelperFactory.Object;
        }
    }
}

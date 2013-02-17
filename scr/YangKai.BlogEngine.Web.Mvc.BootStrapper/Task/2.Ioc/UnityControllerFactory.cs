using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Mvc;
using System.Web.Routing;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;

namespace YangKai.BlogEngine.Web.Mvc.BootStrapper
{
    internal class UnityControllerFactory : DefaultControllerFactory
    {
        public IUnityContainer UnityContainer { get; private set; }

        public UnityControllerFactory(IUnityContainer container, string containerName = "")
        {
            UnityContainer = container;
        }

        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            if (null == controllerType)
            {
                return null;
            }
            return (IController) UnityContainer.Resolve(controllerType);
        }

        public override void ReleaseController(IController controller)
        {
            UnityContainer.Teardown(controller);
        }
    }
}
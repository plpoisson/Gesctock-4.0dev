using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Nop.Web.Framework.Mvc.Routing;

namespace Nop.Plugin.Payments.Monetico
{
    public partial class RouteProvider : IRouteProvider
    {
        public void RegisterRoutes(IRouteBuilder routeBuilder)
        {

            routeBuilder.MapRoute("Plugin.Payments.Monetico.SuccesOrder", "Plugins/Monetico/SuccesOrder",
                 new { controller = "Monetico", action = "SuccesOrder" });

            routeBuilder.MapRoute("Plugin.Payments.Monetico.CancelOrder", "Plugins/Monetico/CancelOrder/{id?}", 
                 new { controller = "Monetico", action = "CancelOrder" });


            routeBuilder.MapRoute("Plugin.Payments.Monetico.ConfirmOrder", "Plugins/Monetico/ConfirmOrder/{id?}",
                new { controller = "Monetico", action = "ConfirmOrder" });
        }


        /// <summary>
        /// Gets a priority of route provider
        /// </summary>
        public int Priority
        {
            get { return -1; }
        }
    }
}


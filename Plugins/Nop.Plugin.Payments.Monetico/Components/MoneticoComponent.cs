using Microsoft.AspNetCore.Mvc;
using Nop.Web.Framework.Components;

namespace Nop.Plugin.Payments.Monetico.Components
{
    [ViewComponent(Name = "Monetico")]
    public class MoneticoComponent : NopViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View("~/Plugins/Payments.Monetico/Views/PaymentInfo.cshtml");
        }
    }
}

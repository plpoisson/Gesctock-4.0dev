using System.Collections.Generic;
using Nop.Core;
using Nop.Core.Plugins;
using Nop.Services.Cms;
using Nop.Services.Configuration;

namespace Iclic.Widgets.EnVedette
{
    public class EnVedettePlugin : BasePlugin, IWidgetPlugin
    {
        private readonly ISettingService _settingService;
        private readonly IWebHelper _webHelper;
        //private readonly WidgetsEnVedetteProductObjectContext _context;
        public EnVedettePlugin(ISettingService settingService, IWebHelper webHelper /*, WidgetsEnVedetteProductObjectContext context*/)
        {
            this._settingService = settingService;
            this._webHelper = webHelper;
            //this._context = context;
        }

        public IList<string> GetWidgetZones()
        {
            return new List<string>
            {
                "home_page_before_products"
            };
        }

        /// <summary>
        /// Gets a view component for displaying plugin in public store
        /// </summary>
        /// <param name="widgetZone">Name of the widget zone</param>
        /// <param name="viewComponentName">View component name</param>
        /// 

        public void GetPublicViewComponent(string widgetZone, out string viewComponentName)
        {
            viewComponentName = "EnVedette";
        }

        /// <summary>
        /// Gets a configuration page URL
        /// </summary>
        public override string GetConfigurationPageUrl()
        {
            return _webHelper.GetStoreLocation() + "Admin/WidgetsEnVedette/Configure";
        }

        public override void Install()
        {
            //pictures
            //settings
            var settings = new EnVedetteSettings()
            {
                ProduitVedettes = true,
                ProduitVedettesNb = 4,
                ProduitNouveautes = true,
                ProduitNouveautesNb = 4,
                ProduitVentes = true,
                ProduitVentesNb = 4,
                ProduitsDecouverte = true,
                ProduitsDecouverteNb = 4,
                ProduitsCategorie = false,
                ProduitsCategorieNb = 4,
                CategoryId = 0,
                ProduitsSolde = false,
                ProduitsSoldeNb = 4,
                ProduitsSoldePourcentage = 20
            };
            _settingService.SaveSetting(settings);
            //_context.Install();
            base.Install();
        }

        public override void Uninstall()
        {
            //settings
            _settingService.DeleteSetting<EnVedetteSettings>();

            //locales
            //_context.Uninstall();//Uptate the db
            base.Uninstall();
        }

        /*
        public void GetDisplayWidgetRoute(string widgetZone, out string actionName, out string controllerName, out RouteValueDictionary routeValues)
        {
            actionName = "PublicInfo";
            controllerName = "WidgetsEnVedette";
            routeValues = new RouteValueDictionary
            {
                {"Namespaces", "Iclic.Widgets.EnVedette.Controllers"},
                {"area", null},
                {"widgetZone", widgetZone}
            };
        }

        public void GetConfigurationRoute(out string actionName, out string controllerName, out RouteValueDictionary routeValues)
        {
            actionName = "Configure";
            controllerName = "WidgetsEnVedette";
            routeValues = new RouteValueDictionary { { "Namespaces", "Iclic.Widgets.EnVedette.Controllers" }, { "area", null } };
        } */
    }
}

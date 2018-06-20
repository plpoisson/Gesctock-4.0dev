using System.Collections.Generic;
using Nop.Core;
using Nop.Core.Plugins;
using Nop.Services.Cms;
using Nop.Services.Configuration;
using Nop.Services.Localization;


namespace Iclic.Plugin.Widgets.BlocPub
{
    public class BlocPubPlugin : BasePlugin, IWidgetPlugin
    {

        private readonly ISettingService _settingService;
        private readonly IWebHelper _webHelper;

        public BlocPubPlugin(ISettingService settingService, Nop.Core.IWebHelper webHelper)
        {
            this._settingService = settingService;
            this._webHelper = webHelper;
        }



        /// <summary>
        /// Gets a configuration page URL
        /// </summary>
        public override string GetConfigurationPageUrl()
        {
            return _webHelper.GetStoreLocation() + "Admin/WidgetsBlocPub/Configure";
        }
        /// <summary>
        /// Gets widget zones where this widget should be rendered
        /// </summary>
        /// <returns>Widget zones</returns>
        public IList<string> GetWidgetZones()
        {
            return new List<string> { "home_page_top" };
        }

        /// <summary>
        /// Gets a view component for displaying plugin in public store
        /// </summary>
        /// <param name="widgetZone">Name of the widget zone</param>
        /// <param name="viewComponentName">View component name</param>
        /// 
        public void GetPublicViewComponent(string widgetZone, out string viewComponentName)
        {
            
            viewComponentName = "BlocPub";
        }

        /// <summary>
        /// Install plugin
        /// </summary>
        public override void Install()
        {
            //pictures



            //settings
            var settings = new BlocPubSetting
            {

            };
            _settingService.SaveSetting(settings);


            this.AddOrUpdatePluginLocaleResource("Plugins.Widgets.NivoSlider.Picture1", "Bloc 1");
            this.AddOrUpdatePluginLocaleResource("Plugins.Widgets.NivoSlider.Picture2", "Bloc 2");
            this.AddOrUpdatePluginLocaleResource("Plugins.Widgets.NivoSlider.Picture3", "Bloc 3");
            this.AddOrUpdatePluginLocaleResource("Plugins.Widgets.NivoSlider.Picture4", "Bloc 4");
            this.AddOrUpdatePluginLocaleResource("Plugins.Widgets.NivoSlider.Picture5", "Bloc 5");
            this.AddOrUpdatePluginLocaleResource("Plugins.Widgets.NivoSlider.Picture6", "Bloc 6");
            this.AddOrUpdatePluginLocaleResource("Plugins.Widgets.NivoSlider.Picture7", "Bloc 7");
            this.AddOrUpdatePluginLocaleResource("Plugins.Widgets.NivoSlider.Picture8", "Bloc 8");
            this.AddOrUpdatePluginLocaleResource("Plugins.Widgets.NivoSlider.Picture9", "Bloc 9");
            this.AddOrUpdatePluginLocaleResource("Plugins.Widgets.NivoSlider.Picture10", "Bloc 10");
            this.AddOrUpdatePluginLocaleResource("Plugins.Widgets.NivoSlider.Picture", "Bloc");
            this.AddOrUpdatePluginLocaleResource("Plugins.Widgets.NivoSlider.Picture.Hint", "Upload picture.");
            this.AddOrUpdatePluginLocaleResource("Plugins.Widgets.NivoSlider.Nom", "Nom");
            this.AddOrUpdatePluginLocaleResource("Plugins.Widgets.NivoSlider.Link", "Url");
            this.AddOrUpdatePluginLocaleResource("Plugins.Widgets.NivoSlider.Text.Hint", "Enter comment for picture. Leave empty if you don't want to display any text.");
            this.AddOrUpdatePluginLocaleResource("Plugins.Widgets.NivoSlider.Tag", "Tag");
            this.AddOrUpdatePluginLocaleResource("Plugins.Widgets.NivoSlider.DateDebut", "Date de dbut");
            this.AddOrUpdatePluginLocaleResource("Plugins.Widgets.NivoSlider.DateFin", "Date de fin");
            this.AddOrUpdatePluginLocaleResource("Plugins.Widgets.NivoSlider.Link.Hint", "Enter URL. Leave empty if you don't want this picture to be clickable.");

            base.Install();
        }

        /// <summary>
        /// Uninstall plugin
        /// </summary>
        public override void Uninstall()
        {
            //settings
            _settingService.DeleteSetting<BlocPubSetting>();

            //locales


            this.DeletePluginLocaleResource("Plugins.Widgets.NivoSlider.Picture1");
            this.DeletePluginLocaleResource("Plugins.Widgets.NivoSlider.Picture2");
            this.DeletePluginLocaleResource("Plugins.Widgets.NivoSlider.Picture3");
            this.DeletePluginLocaleResource("Plugins.Widgets.NivoSlider.Picture4");
            this.DeletePluginLocaleResource("Plugins.Widgets.NivoSlider.Picture5");
            this.DeletePluginLocaleResource("Plugins.Widgets.NivoSlider.Picture6");
            this.DeletePluginLocaleResource("Plugins.Widgets.NivoSlider.Picture7");
            this.DeletePluginLocaleResource("Plugins.Widgets.NivoSlider.Picture8");
            this.DeletePluginLocaleResource("Plugins.Widgets.NivoSlider.Picture9");
            this.DeletePluginLocaleResource("Plugins.Widgets.NivoSlider.Picture10");
            this.DeletePluginLocaleResource("Plugins.Widgets.NivoSlider.Picture");
            this.DeletePluginLocaleResource("Plugins.Widgets.NivoSlider.Picture.Hint");
            this.DeletePluginLocaleResource("Plugins.Widgets.NivoSlider.Nom");
            this.DeletePluginLocaleResource("Plugins.Widgets.NivoSlider.Text.Hint");
            this.DeletePluginLocaleResource("Plugins.Widgets.NivoSlider.Tag");
            this.DeletePluginLocaleResource("Plugins.Widgets.NivoSlider.DateDebut");
            this.DeletePluginLocaleResource("Plugins.Widgets.NivoSlider.DateFin");
            this.DeletePluginLocaleResource("Plugins.Widgets.NivoSlider.Link.Hint");
            this.DeletePluginLocaleResource("Plugins.Widgets.NivoSlider.Link");


            base.Uninstall();
        }
    }



}


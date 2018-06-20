using Nop.Core;
using Nop.Core.Caching;
using Nop.Plugin.Widgets.NivoSlider.Infrastructure.Cache;
using Iclic.Plugin.Widgets.BlocPub.Models;
using Nop.Services.Catalog;
using Nop.Services.Configuration;
using Nop.Services.Localization;
using Nop.Services.Media;
using Nop.Services.Seo;
using Nop.Services.Stores;
using Nop.Web.Framework.Controllers;
using Microsoft.AspNetCore.Mvc;
using Nop.Web.Framework;

namespace Iclic.Plugin.Widgets.BlocPub.Controllers
{

    [Area(AreaNames.Admin)]
    public class WidgetsBlocPubController : BasePluginController
    {

        #region Fields
        private readonly IWorkContext _workContext;
        private readonly IStoreContext _storeContext;
        private readonly IStoreService _storeService;
        private readonly IPictureService _pictureService;
        private readonly ISettingService _settingService;
        private readonly ICacheManager _cacheManager;
        private readonly IProductTagService _productTagService;
        private readonly ILocalizationService _localizationService;
        #endregion

        #region Constructors
        public WidgetsBlocPubController(IWorkContext workContext,
            IStoreContext storeContext,
            IStoreService storeService, 
            IPictureService pictureService,
            ISettingService settingService,
            ICacheManager cacheManager,
            ILocalizationService localizationService,
            IProductTagService productTagService)
        {
            this._workContext = workContext;
            this._storeContext = storeContext;
            this._storeService = storeService;
            this._pictureService = pictureService;
            this._settingService = settingService;
            this._cacheManager = cacheManager;
            this._localizationService = localizationService;
            this._productTagService = productTagService;
        }
        #endregion



       
        public IActionResult Configure()
        {
            //load settings for a chosen store scope
            var storeScope = this.GetActiveStoreScopeConfiguration(_storeService, _workContext);
            var blocPubSettings = _settingService.LoadSetting<BlocPubSetting>(storeScope);
            var model = new ConfigurationModel();

            model.Picture1Id = blocPubSettings.Picture1Id;
            model.Nom1 = blocPubSettings.Nom1;
            model.Url1 = blocPubSettings.Url1;
            model.Tag1 = blocPubSettings.Tag1;
            model.DateDebut1 = blocPubSettings.DateDebut1;
            model.DateFin1 = blocPubSettings.DateFin1;

            model.Picture2Id = blocPubSettings.Picture2Id;
            model.Nom2 = blocPubSettings.Nom2;
            model.Url2 = blocPubSettings.Url2;
            model.Tag2 = blocPubSettings.Tag2;
            model.DateDebut2 = blocPubSettings.DateDebut2;
            model.DateFin2 = blocPubSettings.DateFin2;

            model.Picture3Id = blocPubSettings.Picture3Id;
            model.Nom3 = blocPubSettings.Nom3;
            model.Url3 = blocPubSettings.Url3;
            model.Tag3 = blocPubSettings.Tag3;
            model.DateDebut3 = blocPubSettings.DateDebut3;
            model.DateFin3 = blocPubSettings.DateFin3;

            model.Picture4Id = blocPubSettings.Picture4Id;
            model.Nom4 = blocPubSettings.Nom4;
            model.Url4 = blocPubSettings.Url4;
            model.Tag4 = blocPubSettings.Tag4;
            model.DateDebut4 = blocPubSettings.DateDebut4;
            model.DateFin4 = blocPubSettings.DateFin4;

            model.Picture5Id = blocPubSettings.Picture5Id;
            model.Nom5 = blocPubSettings.Nom5;
            model.Url5 = blocPubSettings.Url5;
            model.Tag5 = blocPubSettings.Tag5;
            model.DateDebut5 = blocPubSettings.DateDebut5;
            model.DateFin5 = blocPubSettings.DateFin5;

            model.Picture6Id = blocPubSettings.Picture6Id;
            model.Nom6 = blocPubSettings.Nom6;
            model.Url6 = blocPubSettings.Url6;
            model.Tag6 = blocPubSettings.Tag6;
            model.DateDebut6 = blocPubSettings.DateDebut6;
            model.DateFin6 = blocPubSettings.DateFin6;

            model.Picture7Id = blocPubSettings.Picture7Id;
            model.Nom7 = blocPubSettings.Nom7;
            model.Url7 = blocPubSettings.Url7;
            model.Tag7 = blocPubSettings.Tag7;
            model.DateDebut7 = blocPubSettings.DateDebut7;
            model.DateFin7 = blocPubSettings.DateFin7;

            model.Picture8Id = blocPubSettings.Picture8Id;
            model.Nom8 = blocPubSettings.Nom8;
            model.Url8 = blocPubSettings.Url8;
            model.Tag8 = blocPubSettings.Tag8;
            model.DateDebut8 = blocPubSettings.DateDebut8;
            model.DateFin8 = blocPubSettings.DateFin8;

            model.Picture9Id = blocPubSettings.Picture9Id;
            model.Nom9 = blocPubSettings.Nom9;
            model.Url9 = blocPubSettings.Url9;
            model.Tag9 = blocPubSettings.Tag9;
            model.DateDebut9 = blocPubSettings.DateDebut9;
            model.DateFin9 = blocPubSettings.DateFin9;

            model.Picture10Id = blocPubSettings.Picture10Id;
            model.Nom10 = blocPubSettings.Nom10;
            model.Url10 = blocPubSettings.Url10;
            model.Tag10 = blocPubSettings.Tag10;
            model.DateDebut10 = blocPubSettings.DateDebut10;
            model.DateFin10 = blocPubSettings.DateFin10;

            model.ActiveStoreScopeConfiguration = storeScope;
            if (storeScope > 0)
            {

                model.Picture1Id_OverrideForStore = _settingService.SettingExists(blocPubSettings, x => x.Picture1Id, storeScope);
                model.Nom1_OverrideForStore = _settingService.SettingExists(blocPubSettings, x => x.Nom1, storeScope);
                model.Url1_OverrideForStore = _settingService.SettingExists(blocPubSettings, x => x.Url1, storeScope);
                model.Tag1_OverrideForStore = _settingService.SettingExists(blocPubSettings, x => x.Tag1, storeScope);
                model.DateDebut1_OverrideForStore = _settingService.SettingExists(blocPubSettings, x => x.DateDebut1, storeScope);
                model.DateFin1_OverrideForStore = _settingService.SettingExists(blocPubSettings, x => x.DateFin1, storeScope);

                model.Picture2Id_OverrideForStore = _settingService.SettingExists(blocPubSettings, x => x.Picture2Id, storeScope);
                model.Nom2_OverrideForStore = _settingService.SettingExists(blocPubSettings, x => x.Nom2, storeScope);
                model.Url2_OverrideForStore = _settingService.SettingExists(blocPubSettings, x => x.Url2, storeScope);
                model.Tag2_OverrideForStore = _settingService.SettingExists(blocPubSettings, x => x.Tag2, storeScope);
                model.DateDebut2_OverrideForStore = _settingService.SettingExists(blocPubSettings, x => x.DateDebut2, storeScope);
                model.DateFin2_OverrideForStore = _settingService.SettingExists(blocPubSettings, x => x.DateFin2, storeScope);

                model.Picture3Id_OverrideForStore = _settingService.SettingExists(blocPubSettings, x => x.Picture3Id, storeScope);
                model.Nom3_OverrideForStore = _settingService.SettingExists(blocPubSettings, x => x.Nom3, storeScope);
                model.Url3_OverrideForStore = _settingService.SettingExists(blocPubSettings, x => x.Url3, storeScope);
                model.Tag3_OverrideForStore = _settingService.SettingExists(blocPubSettings, x => x.Tag3, storeScope);
                model.DateDebut3_OverrideForStore = _settingService.SettingExists(blocPubSettings, x => x.DateDebut3, storeScope);
                model.DateFin3_OverrideForStore = _settingService.SettingExists(blocPubSettings, x => x.DateFin3, storeScope);

                model.Picture4Id_OverrideForStore = _settingService.SettingExists(blocPubSettings, x => x.Picture4Id, storeScope);
                model.Nom4_OverrideForStore = _settingService.SettingExists(blocPubSettings, x => x.Nom4, storeScope);
                model.Url4_OverrideForStore = _settingService.SettingExists(blocPubSettings, x => x.Url4, storeScope);
                model.Tag4_OverrideForStore = _settingService.SettingExists(blocPubSettings, x => x.Tag4, storeScope);
                model.DateDebut4_OverrideForStore = _settingService.SettingExists(blocPubSettings, x => x.DateDebut4, storeScope);
                model.DateFin4_OverrideForStore = _settingService.SettingExists(blocPubSettings, x => x.DateFin4, storeScope);

                model.Picture5Id_OverrideForStore = _settingService.SettingExists(blocPubSettings, x => x.Picture5Id, storeScope);
                model.Nom5_OverrideForStore = _settingService.SettingExists(blocPubSettings, x => x.Nom5, storeScope);
                model.Url5_OverrideForStore = _settingService.SettingExists(blocPubSettings, x => x.Url5, storeScope);
                model.Tag5_OverrideForStore = _settingService.SettingExists(blocPubSettings, x => x.Tag5, storeScope);
                model.DateDebut5_OverrideForStore = _settingService.SettingExists(blocPubSettings, x => x.DateDebut5, storeScope);
                model.DateFin5_OverrideForStore = _settingService.SettingExists(blocPubSettings, x => x.DateFin5, storeScope);

                model.Picture6Id_OverrideForStore = _settingService.SettingExists(blocPubSettings, x => x.Picture6Id, storeScope);
                model.Nom6_OverrideForStore = _settingService.SettingExists(blocPubSettings, x => x.Nom6, storeScope);
                model.Url6_OverrideForStore = _settingService.SettingExists(blocPubSettings, x => x.Url6, storeScope);
                model.Tag6_OverrideForStore = _settingService.SettingExists(blocPubSettings, x => x.Tag6, storeScope);
                model.DateDebut6_OverrideForStore = _settingService.SettingExists(blocPubSettings, x => x.DateDebut6, storeScope);
                model.DateFin6_OverrideForStore = _settingService.SettingExists(blocPubSettings, x => x.DateFin6, storeScope);

                model.Picture7Id_OverrideForStore = _settingService.SettingExists(blocPubSettings, x => x.Picture7Id, storeScope);
                model.Nom7_OverrideForStore = _settingService.SettingExists(blocPubSettings, x => x.Nom7, storeScope);
                model.Url7_OverrideForStore = _settingService.SettingExists(blocPubSettings, x => x.Url7, storeScope);
                model.Tag7_OverrideForStore = _settingService.SettingExists(blocPubSettings, x => x.Tag7, storeScope);
                model.DateDebut7_OverrideForStore = _settingService.SettingExists(blocPubSettings, x => x.DateDebut7, storeScope);
                model.DateFin7_OverrideForStore = _settingService.SettingExists(blocPubSettings, x => x.DateFin7, storeScope);

                model.Picture8Id_OverrideForStore = _settingService.SettingExists(blocPubSettings, x => x.Picture8Id, storeScope);
                model.Nom8_OverrideForStore = _settingService.SettingExists(blocPubSettings, x => x.Nom8, storeScope);
                model.Url8_OverrideForStore = _settingService.SettingExists(blocPubSettings, x => x.Url8, storeScope);
                model.Tag8_OverrideForStore = _settingService.SettingExists(blocPubSettings, x => x.Tag8, storeScope);
                model.DateDebut8_OverrideForStore = _settingService.SettingExists(blocPubSettings, x => x.DateDebut8, storeScope);
                model.DateFin8_OverrideForStore = _settingService.SettingExists(blocPubSettings, x => x.DateFin8, storeScope);

                model.Picture9Id_OverrideForStore = _settingService.SettingExists(blocPubSettings, x => x.Picture9Id, storeScope);
                model.Nom9_OverrideForStore = _settingService.SettingExists(blocPubSettings, x => x.Nom9, storeScope);
                model.Url9_OverrideForStore = _settingService.SettingExists(blocPubSettings, x => x.Url9, storeScope);
                model.Tag9_OverrideForStore = _settingService.SettingExists(blocPubSettings, x => x.Tag9, storeScope);
                model.DateDebut9_OverrideForStore = _settingService.SettingExists(blocPubSettings, x => x.DateDebut9, storeScope);
                model.DateFin9_OverrideForStore = _settingService.SettingExists(blocPubSettings, x => x.DateFin9, storeScope);

                model.Picture10Id_OverrideForStore = _settingService.SettingExists(blocPubSettings, x => x.Picture10Id, storeScope);
                model.Nom10_OverrideForStore = _settingService.SettingExists(blocPubSettings, x => x.Nom10, storeScope);
                model.Url10_OverrideForStore = _settingService.SettingExists(blocPubSettings, x => x.Url10, storeScope);
                model.Tag10_OverrideForStore = _settingService.SettingExists(blocPubSettings, x => x.Tag10, storeScope);
                model.DateDebut10_OverrideForStore = _settingService.SettingExists(blocPubSettings, x => x.DateDebut10, storeScope);
                model.DateFin10_OverrideForStore = _settingService.SettingExists(blocPubSettings, x => x.DateFin10, storeScope);

                
            }
            return View("~/Plugins/Iclic.Plugin.Widgets.BlocPub/Views/Configure.cshtml", model);
        }
        
        [HttpPost]
        public IActionResult Configure(ConfigurationModel model)
        {
            //load settings for a chosen store scope
            var storeScope = this.GetActiveStoreScopeConfiguration(_storeService, _workContext);
            var blocPubSettings = _settingService.LoadSetting<BlocPubSetting>(storeScope);

            blocPubSettings.Picture1Id = model.Picture1Id;
            blocPubSettings.Nom1 = model.Nom1;
            blocPubSettings.Url1 = model.Url1;
            blocPubSettings.Tag1 = model.Tag1;
            blocPubSettings.DateDebut1 = model.DateDebut1;
            blocPubSettings.DateFin1 = model.DateFin1;

            blocPubSettings.Picture2Id = model.Picture2Id;
            blocPubSettings.Nom2 = model.Nom2;
            blocPubSettings.Url2 = model.Url2;
            blocPubSettings.Tag2 = model.Tag2;
            blocPubSettings.DateDebut2 = model.DateDebut2;
            blocPubSettings.DateFin2 = model.DateFin2;

            blocPubSettings.Picture3Id = model.Picture3Id;
            blocPubSettings.Nom3 = model.Nom3;
            blocPubSettings.Url3 = model.Url3;
            blocPubSettings.Tag3 = model.Tag3;
            blocPubSettings.DateDebut3 = model.DateDebut3;
            blocPubSettings.DateFin3 = model.DateFin3;

            blocPubSettings.Picture4Id = model.Picture4Id;
            blocPubSettings.Nom4 = model.Nom4;
            blocPubSettings.Url4 = model.Url4;
            blocPubSettings.Tag4 = model.Tag4;
            blocPubSettings.DateDebut4 = model.DateDebut4;
            blocPubSettings.DateFin4 = model.DateFin4;

            blocPubSettings.Picture5Id = model.Picture5Id;
            blocPubSettings.Nom5 = model.Nom5;
            blocPubSettings.Url5 = model.Url5;
            blocPubSettings.Tag5 = model.Tag5;
            blocPubSettings.DateDebut5 = model.DateDebut5;
            blocPubSettings.DateFin5 = model.DateFin5;

            blocPubSettings.Picture6Id = model.Picture6Id;
            blocPubSettings.Nom6 = model.Nom6;
            blocPubSettings.Url6 = model.Url6;
            blocPubSettings.Tag6 = model.Tag6;
            blocPubSettings.DateDebut6 = model.DateDebut6;
            blocPubSettings.DateFin6 = model.DateFin6;

            blocPubSettings.Picture7Id = model.Picture7Id;
            blocPubSettings.Nom7 = model.Nom7;
            blocPubSettings.Url7 = model.Url7;
            blocPubSettings.Tag7 = model.Tag7;
            blocPubSettings.DateDebut7 = model.DateDebut7;
            blocPubSettings.DateFin7 = model.DateFin7;

            blocPubSettings.Picture8Id = model.Picture8Id;
            blocPubSettings.Nom8 = model.Nom8;
            blocPubSettings.Url8 = model.Url8;
            blocPubSettings.Tag8 = model.Tag8;
            blocPubSettings.DateDebut8 = model.DateDebut8;
            blocPubSettings.DateFin8 = model.DateFin8;

            blocPubSettings.Picture9Id = model.Picture9Id;
            blocPubSettings.Nom9 = model.Nom9;
            blocPubSettings.Url9 = model.Url9;
            blocPubSettings.Tag9 = model.Tag9;
            blocPubSettings.DateDebut9 = model.DateDebut9;
            blocPubSettings.DateFin9 = model.DateFin9;

            blocPubSettings.Picture10Id = model.Picture10Id;
            blocPubSettings.Nom10 = model.Nom10;
            blocPubSettings.Url10 = model.Url10;
            blocPubSettings.Tag10 = model.Tag10;
            blocPubSettings.DateDebut10 = model.DateDebut10;
            blocPubSettings.DateFin10 = model.DateFin10;

            if (model.Picture1Id_OverrideForStore || storeScope == 0)
                _settingService.SaveSetting(blocPubSettings, x => x.Picture1Id, storeScope, false);
            else if (storeScope > 0)
                _settingService.DeleteSetting(blocPubSettings, x => x.Picture1Id, storeScope);


            if (model.Nom1_OverrideForStore || storeScope == 0)
                _settingService.SaveSetting(blocPubSettings, x => x.Nom1, storeScope, false);
            else if (storeScope > 0)
                _settingService.DeleteSetting(blocPubSettings, x => x.Nom1, storeScope);
            if (model.Url1_OverrideForStore || storeScope == 0)
                _settingService.SaveSetting(blocPubSettings, x => x.Url1, storeScope, false);
            else if (storeScope > 0)
                _settingService.DeleteSetting(blocPubSettings, x => x.Url1, storeScope);
            if (model.Tag1_OverrideForStore || storeScope == 0)
                _settingService.SaveSetting(blocPubSettings, x => x.Tag1, storeScope, false);
            else if (storeScope > 0)
                _settingService.DeleteSetting(blocPubSettings, x => x.Tag1, storeScope);
            if (model.DateDebut1_OverrideForStore || storeScope == 0)
                _settingService.SaveSetting(blocPubSettings, x => x.DateDebut1, storeScope, false);
            else if (storeScope > 0)
                _settingService.DeleteSetting(blocPubSettings, x => x.DateDebut1, storeScope);
            if (model.DateFin1_OverrideForStore || storeScope == 0)
                _settingService.SaveSetting(blocPubSettings, x => x.DateFin1, storeScope, false);
            else if (storeScope > 0)
                _settingService.DeleteSetting(blocPubSettings, x => x.DateFin1, storeScope);


            if (model.Picture2Id_OverrideForStore || storeScope == 0)
                _settingService.SaveSetting(blocPubSettings, x => x.Picture2Id, storeScope, false);
            else if (storeScope > 0)
                _settingService.DeleteSetting(blocPubSettings, x => x.Picture2Id, storeScope);
            if (model.Nom2_OverrideForStore || storeScope == 0)
                _settingService.SaveSetting(blocPubSettings, x => x.Nom2, storeScope, false);
            else if (storeScope > 0)
                _settingService.DeleteSetting(blocPubSettings, x => x.Nom2, storeScope);
            if (model.Url2_OverrideForStore || storeScope == 0)
                _settingService.SaveSetting(blocPubSettings, x => x.Url2, storeScope, false);
            else if (storeScope > 0)
                _settingService.DeleteSetting(blocPubSettings, x => x.Url2, storeScope);
            if (model.Tag2_OverrideForStore || storeScope == 0)
                _settingService.SaveSetting(blocPubSettings, x => x.Tag2, storeScope, false);
            else if (storeScope > 0)
                _settingService.DeleteSetting(blocPubSettings, x => x.Tag2, storeScope);
            if (model.DateDebut2_OverrideForStore || storeScope == 0)
                _settingService.SaveSetting(blocPubSettings, x => x.DateDebut2, storeScope, false);
            else if (storeScope > 0)
                _settingService.DeleteSetting(blocPubSettings, x => x.DateDebut2, storeScope);
            if (model.DateFin2_OverrideForStore || storeScope == 0)
                _settingService.SaveSetting(blocPubSettings, x => x.DateFin2, storeScope, false);
            else if (storeScope > 0)
                _settingService.DeleteSetting(blocPubSettings, x => x.DateFin2, storeScope);

            if (model.Picture3Id_OverrideForStore || storeScope == 0)
                _settingService.SaveSetting(blocPubSettings, x => x.Picture3Id, storeScope, false);
            else if (storeScope > 0)
                _settingService.DeleteSetting(blocPubSettings, x => x.Picture3Id, storeScope);
            if (model.Nom3_OverrideForStore || storeScope == 0)
                _settingService.SaveSetting(blocPubSettings, x => x.Nom3, storeScope, false);
            else if (storeScope > 0)
                _settingService.DeleteSetting(blocPubSettings, x => x.Nom3, storeScope);
            if (model.Url3_OverrideForStore || storeScope == 0)
                _settingService.SaveSetting(blocPubSettings, x => x.Url3, storeScope, false);
            else if (storeScope > 0)
                _settingService.DeleteSetting(blocPubSettings, x => x.Url3, storeScope);
            if (model.Tag3_OverrideForStore || storeScope == 0)
                _settingService.SaveSetting(blocPubSettings, x => x.Tag3, storeScope, false);
            else if (storeScope > 0)
                _settingService.DeleteSetting(blocPubSettings, x => x.Tag3, storeScope);
            if (model.DateDebut3_OverrideForStore || storeScope == 0)
                _settingService.SaveSetting(blocPubSettings, x => x.DateDebut3, storeScope, false);
            else if (storeScope > 0)
                _settingService.DeleteSetting(blocPubSettings, x => x.DateDebut3, storeScope);
            if (model.DateFin3_OverrideForStore || storeScope == 0)
                _settingService.SaveSetting(blocPubSettings, x => x.DateFin3, storeScope, false);
            else if (storeScope > 0)
                _settingService.DeleteSetting(blocPubSettings, x => x.DateFin3, storeScope);

            if (model.Picture4Id_OverrideForStore || storeScope == 0)
                _settingService.SaveSetting(blocPubSettings, x => x.Picture4Id, storeScope, false);
            else if (storeScope > 0)
                _settingService.DeleteSetting(blocPubSettings, x => x.Picture4Id, storeScope);
            if (model.Nom4_OverrideForStore || storeScope == 0)
                _settingService.SaveSetting(blocPubSettings, x => x.Nom4, storeScope, false);
            else if (storeScope > 0)
                _settingService.DeleteSetting(blocPubSettings, x => x.Nom4, storeScope);
            if (model.Url4_OverrideForStore || storeScope == 0)
                _settingService.SaveSetting(blocPubSettings, x => x.Url4, storeScope, false);
            else if (storeScope > 0)
                _settingService.DeleteSetting(blocPubSettings, x => x.Url4, storeScope);
            if (model.Tag4_OverrideForStore || storeScope == 0)
                _settingService.SaveSetting(blocPubSettings, x => x.Tag4, storeScope, false);
            else if (storeScope > 0)
                _settingService.DeleteSetting(blocPubSettings, x => x.Tag4, storeScope);
            if (model.DateDebut4_OverrideForStore || storeScope == 0)
                _settingService.SaveSetting(blocPubSettings, x => x.DateDebut4, storeScope, false);
            else if (storeScope > 0)
                _settingService.DeleteSetting(blocPubSettings, x => x.DateDebut4, storeScope);
            if (model.DateFin4_OverrideForStore || storeScope == 0)
                _settingService.SaveSetting(blocPubSettings, x => x.DateFin4, storeScope, false);
            else if (storeScope > 0)
                _settingService.DeleteSetting(blocPubSettings, x => x.DateFin4, storeScope);

            if (model.Picture5Id_OverrideForStore || storeScope == 0)
                _settingService.SaveSetting(blocPubSettings, x => x.Picture5Id, storeScope, false);
            else if (storeScope > 0)
                _settingService.DeleteSetting(blocPubSettings, x => x.Picture5Id, storeScope);
            if (model.Nom5_OverrideForStore || storeScope == 0)
                _settingService.SaveSetting(blocPubSettings, x => x.Nom5, storeScope, false);
            else if (storeScope > 0)
                _settingService.DeleteSetting(blocPubSettings, x => x.Nom5, storeScope);
            if (model.Url5_OverrideForStore || storeScope == 0)
                _settingService.SaveSetting(blocPubSettings, x => x.Url5, storeScope, false);
            else if (storeScope > 0)
                _settingService.DeleteSetting(blocPubSettings, x => x.Url5, storeScope);
            if (model.Tag5_OverrideForStore || storeScope == 0)
                _settingService.SaveSetting(blocPubSettings, x => x.Tag5, storeScope, false);
            else if (storeScope > 0)
                _settingService.DeleteSetting(blocPubSettings, x => x.Tag5, storeScope);
            if (model.DateDebut5_OverrideForStore || storeScope == 0)
                _settingService.SaveSetting(blocPubSettings, x => x.DateDebut5, storeScope, false);
            else if (storeScope > 0)
                _settingService.DeleteSetting(blocPubSettings, x => x.DateDebut5, storeScope);
            if (model.DateFin5_OverrideForStore || storeScope == 0)
                _settingService.SaveSetting(blocPubSettings, x => x.DateFin5, storeScope, false);
            else if (storeScope > 0)
                _settingService.DeleteSetting(blocPubSettings, x => x.DateFin5, storeScope);

            if (model.Picture6Id_OverrideForStore || storeScope == 0)
                _settingService.SaveSetting(blocPubSettings, x => x.Picture6Id, storeScope, false);
            else if (storeScope > 0)
                _settingService.DeleteSetting(blocPubSettings, x => x.Picture6Id, storeScope);
            if (model.Nom6_OverrideForStore || storeScope == 0)
                _settingService.SaveSetting(blocPubSettings, x => x.Nom6, storeScope, false);
            else if (storeScope > 0)
                _settingService.DeleteSetting(blocPubSettings, x => x.Nom6, storeScope);
            if (model.Url6_OverrideForStore || storeScope == 0)
                _settingService.SaveSetting(blocPubSettings, x => x.Url6, storeScope, false);
            else if (storeScope > 0)
                _settingService.DeleteSetting(blocPubSettings, x => x.Url6, storeScope);
            if (model.Tag6_OverrideForStore || storeScope == 0)
                _settingService.SaveSetting(blocPubSettings, x => x.Tag6, storeScope, false);
            else if (storeScope > 0)
                _settingService.DeleteSetting(blocPubSettings, x => x.Tag6, storeScope);
            if (model.DateDebut6_OverrideForStore || storeScope == 0)
                _settingService.SaveSetting(blocPubSettings, x => x.DateDebut6, storeScope, false);
            else if (storeScope > 0)
                _settingService.DeleteSetting(blocPubSettings, x => x.DateDebut6, storeScope);
            if (model.DateFin6_OverrideForStore || storeScope == 0)
                _settingService.SaveSetting(blocPubSettings, x => x.DateFin6, storeScope, false);
            else if (storeScope > 0)
                _settingService.DeleteSetting(blocPubSettings, x => x.DateFin6, storeScope);

            if (model.Picture7Id_OverrideForStore || storeScope == 0)
                _settingService.SaveSetting(blocPubSettings, x => x.Picture7Id, storeScope, false);
            else if (storeScope > 0)
                _settingService.DeleteSetting(blocPubSettings, x => x.Picture7Id, storeScope);
            if (model.Nom7_OverrideForStore || storeScope == 0)
                _settingService.SaveSetting(blocPubSettings, x => x.Nom7, storeScope, false);
            else if (storeScope > 0)
                _settingService.DeleteSetting(blocPubSettings, x => x.Nom7, storeScope);
            if (model.Url7_OverrideForStore || storeScope == 0)
                _settingService.SaveSetting(blocPubSettings, x => x.Url7, storeScope, false);
            else if (storeScope > 0)
                _settingService.DeleteSetting(blocPubSettings, x => x.Url7, storeScope);
            if (model.Tag7_OverrideForStore || storeScope == 0)
                _settingService.SaveSetting(blocPubSettings, x => x.Tag7, storeScope, false);
            else if (storeScope > 0)
                _settingService.DeleteSetting(blocPubSettings, x => x.Tag7, storeScope);
            if (model.DateDebut7_OverrideForStore || storeScope == 0)
                _settingService.SaveSetting(blocPubSettings, x => x.DateDebut7, storeScope, false);
            else if (storeScope > 0)
                _settingService.DeleteSetting(blocPubSettings, x => x.DateDebut7, storeScope);
            if (model.DateFin7_OverrideForStore || storeScope == 0)
                _settingService.SaveSetting(blocPubSettings, x => x.DateFin7, storeScope, false);
            else if (storeScope > 0)
                _settingService.DeleteSetting(blocPubSettings, x => x.DateFin7, storeScope);

            if (model.Picture8Id_OverrideForStore || storeScope == 0)
                _settingService.SaveSetting(blocPubSettings, x => x.Picture8Id, storeScope, false);
            else if (storeScope > 0)
                _settingService.DeleteSetting(blocPubSettings, x => x.Picture8Id, storeScope);
            if (model.Nom8_OverrideForStore || storeScope == 0)
                _settingService.SaveSetting(blocPubSettings, x => x.Nom8, storeScope, false);
            else if (storeScope > 0)
                _settingService.DeleteSetting(blocPubSettings, x => x.Nom8, storeScope);
            if (model.Url8_OverrideForStore || storeScope == 0)
                _settingService.SaveSetting(blocPubSettings, x => x.Url8, storeScope, false);
            else if (storeScope > 0)
                _settingService.DeleteSetting(blocPubSettings, x => x.Url8, storeScope);
            if (model.Tag8_OverrideForStore || storeScope == 0)
                _settingService.SaveSetting(blocPubSettings, x => x.Tag8, storeScope, false);
            else if (storeScope > 0)
                _settingService.DeleteSetting(blocPubSettings, x => x.Tag8, storeScope);
            if (model.DateDebut8_OverrideForStore || storeScope == 0)
                _settingService.SaveSetting(blocPubSettings, x => x.DateDebut8, storeScope, false);
            else if (storeScope > 0)
                _settingService.DeleteSetting(blocPubSettings, x => x.DateDebut8, storeScope);
            if (model.DateFin8_OverrideForStore || storeScope == 0)
                _settingService.SaveSetting(blocPubSettings, x => x.DateFin8, storeScope, false);
            else if (storeScope > 0)
                _settingService.DeleteSetting(blocPubSettings, x => x.DateFin8, storeScope);

            if (model.Picture9Id_OverrideForStore || storeScope == 0)
                _settingService.SaveSetting(blocPubSettings, x => x.Picture9Id, storeScope, false);
            else if (storeScope > 0)
                _settingService.DeleteSetting(blocPubSettings, x => x.Picture9Id, storeScope);
            if (model.Nom9_OverrideForStore || storeScope == 0)
                _settingService.SaveSetting(blocPubSettings, x => x.Nom9, storeScope, false);
            else if (storeScope > 0)
                _settingService.DeleteSetting(blocPubSettings, x => x.Nom9, storeScope);
            if (model.Url9_OverrideForStore || storeScope == 0)
                _settingService.SaveSetting(blocPubSettings, x => x.Url9, storeScope, false);
            else if (storeScope > 0)
                _settingService.DeleteSetting(blocPubSettings, x => x.Url9, storeScope);
            if (model.Tag9_OverrideForStore || storeScope == 0)
                _settingService.SaveSetting(blocPubSettings, x => x.Tag9, storeScope, false);
            else if (storeScope > 0)
                _settingService.DeleteSetting(blocPubSettings, x => x.Tag9, storeScope);
            if (model.DateDebut9_OverrideForStore || storeScope == 0)
                _settingService.SaveSetting(blocPubSettings, x => x.DateDebut9, storeScope, false);
            else if (storeScope > 0)
                _settingService.DeleteSetting(blocPubSettings, x => x.DateDebut9, storeScope);
            if (model.DateFin9_OverrideForStore || storeScope == 0)
                _settingService.SaveSetting(blocPubSettings, x => x.DateFin9, storeScope, false);
            else if (storeScope > 0)
                _settingService.DeleteSetting(blocPubSettings, x => x.DateFin9, storeScope);

            if (model.Picture10Id_OverrideForStore || storeScope == 0)
                _settingService.SaveSetting(blocPubSettings, x => x.Picture10Id, storeScope, false);
            else if (storeScope > 0)
                _settingService.DeleteSetting(blocPubSettings, x => x.Picture10Id, storeScope);
            if (model.Nom10_OverrideForStore || storeScope == 0)
                _settingService.SaveSetting(blocPubSettings, x => x.Nom10, storeScope, false);
            else if (storeScope > 0)
                _settingService.DeleteSetting(blocPubSettings, x => x.Nom10, storeScope);
            if (model.Url10_OverrideForStore || storeScope == 0)
                _settingService.SaveSetting(blocPubSettings, x => x.Url10, storeScope, false);
            else if (storeScope > 0)
                _settingService.DeleteSetting(blocPubSettings, x => x.Url10, storeScope);
            if (model.Tag10_OverrideForStore || storeScope == 0)
                _settingService.SaveSetting(blocPubSettings, x => x.Tag10, storeScope, false);
            else if (storeScope > 0)
                _settingService.DeleteSetting(blocPubSettings, x => x.Tag10, storeScope);
            if (model.DateDebut10_OverrideForStore || storeScope == 0)
                _settingService.SaveSetting(blocPubSettings, x => x.DateDebut10, storeScope, false);
            else if (storeScope > 0)
                _settingService.DeleteSetting(blocPubSettings, x => x.DateDebut10, storeScope);
            if (model.DateFin10_OverrideForStore || storeScope == 0)
                _settingService.SaveSetting(blocPubSettings, x => x.DateFin10, storeScope, false);
            else if (storeScope > 0)
                _settingService.DeleteSetting(blocPubSettings, x => x.DateFin10, storeScope);

            //now clear settings cache
            _settingService.ClearCache();

            SuccessNotification(_localizationService.GetResource("Admin.Plugins.Saved"));
            return Configure();
        }


    }
}
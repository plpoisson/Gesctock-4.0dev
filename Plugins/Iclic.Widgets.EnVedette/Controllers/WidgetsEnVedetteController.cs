using Iclic.Widgets.EnVedette.Models;
using Microsoft.AspNetCore.Mvc;
using Nop.Core;
using Nop.Core.Caching;
using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Media;
using Nop.Core.Domain.Seo;
using Nop.Core.Domain.Vendors;
using Nop.Services.Catalog;
using Nop.Services.Configuration;
using Nop.Services.Directory;
using Nop.Services.Localization;
using Nop.Services.Logging;
using Nop.Services.Media;
using Nop.Services.Orders;
using Nop.Services.Security;
using Nop.Services.Shipping;
using Nop.Services.Stores;
using Nop.Services.Tax;
using Nop.Services.Vendors;
using Nop.Web.Framework.Controllers;
using Nop.Web.Framework;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using Nop.Web.Models.Catalog;
using Nop.Web.Extensions;

namespace Iclic.Widgets.EnVedette.Controllers
{
    [Area(AreaNames.Admin)]
    public class WidgetsEnVedetteController : BasePluginController
    {

        #region Fields
        private readonly IWorkContext _workContext;
            private readonly IStoreContext _storeContext;
            private readonly IStoreService _storeService;
            private readonly ISettingService _settingService;
            private readonly IOrderService _orderService;
            private readonly ILogger _logger;
            private readonly ICategoryService _categoryService;
            private readonly IProductAttributeParser _productAttributeParser;
            private readonly ILocalizationService _localizationService;
            private readonly IProductService _productService;
            private readonly IAclService _aclService;
            private readonly IStoreMappingService _storeMappingService;
            private readonly ISpecificationAttributeService _specificationAttributeService;
            private readonly IPriceCalculationService _priceCalculationService;
            private readonly IPriceFormatter _priceFormatter;
            private readonly IPermissionService _permissionService;
            private readonly ITaxService _taxService;
            private readonly ICurrencyService _currencyService;
            private readonly IPictureService _pictureService;
            private readonly IWebHelper _webHelper;
            private readonly ICacheManager _cacheManager;
            private readonly CatalogSettings _catalogSettings;
            private readonly MediaSettings _mediaSettings;
            private readonly SeoSettings _seoSettings;
            private readonly IShippingService _shippingService;
            private readonly IVendorService _vendorService;
            private readonly VendorSettings _vendorSettings;
            private readonly IProductTagService _productTagService;
            private readonly IProductTemplateService _productTemplateService;
            private readonly IMeasureService _measureService;
            private readonly IProductAttributeService _productAttributeService;
            private readonly IManufacturerService _manufacturerService;
            private readonly IOrderReportService _orderReportService;

        #endregion

        #region Constructors
        public WidgetsEnVedetteController(IWorkContext workContext,
                IStoreContext storeContext,
                IStoreService storeService,
                ISettingService settingService,
                IOrderService orderService,
                ILogger logger,
                ICategoryService categoryService,
                IProductAttributeParser productAttributeParser,
                ILocalizationService localizationService,
                IProductService productService,
                IAclService aclService,
                IStoreMappingService storeMappingService,
                ISpecificationAttributeService specificationAttributeService,
                IPriceCalculationService priceCalculationService,
                IPriceFormatter priceFormatter,
                IPermissionService permissionService,
                ITaxService taxService,
                ICurrencyService currencyService,
                IPictureService pictureService,
                IWebHelper webHelper,
                ICacheManager cacheManager,
                CatalogSettings catalogSettings,
                MediaSettings mediaSettings,
                SeoSettings seoSettings,
                IShippingService shippingService,
                IVendorService vendorService,
                VendorSettings vendorSettings,
                IProductTagService productTagService,
                IProductTemplateService productTemplateService,
                IMeasureService measureService,
                IProductAttributeService productAttributeService,
                IManufacturerService manufacturerService,
                IOrderReportService orderReportService)
            {
                this._workContext = workContext;
                this._storeContext = storeContext;
                this._storeService = storeService;
                this._settingService = settingService;
                this._orderService = orderService;
                this._logger = logger;
                this._categoryService = categoryService;
                this._productAttributeParser = productAttributeParser;
                this._localizationService = localizationService;
                this._productService = productService;
                this._aclService = aclService;
                this._storeMappingService = storeMappingService;
                this._specificationAttributeService = specificationAttributeService;
                this._priceCalculationService = priceCalculationService;
                this._priceFormatter = priceFormatter;
                this._permissionService = permissionService;
                this._taxService = taxService;
                this._currencyService = currencyService;
                this._pictureService = pictureService;
                this._webHelper = webHelper;
                this._cacheManager = cacheManager;
                this._catalogSettings = catalogSettings;
                this._mediaSettings = mediaSettings;
                this._seoSettings = seoSettings;
                this._shippingService = shippingService;
                this._vendorService = vendorService;
                this._vendorSettings = vendorSettings;
                this._productTagService = productTagService;
                this._productTemplateService = productTemplateService;
                this._measureService = measureService;
                this._productAttributeService = productAttributeService;
                this._manufacturerService = manufacturerService;
                this._orderReportService = orderReportService;
            }

        #endregion

        [NonAction]
        protected virtual IEnumerable<ProductOverviewModel> PrepareProductOverviewModels(IEnumerable<Product> products,
           bool preparePriceModel = true, bool preparePictureModel = true,
           int? productThumbPictureSize = null, bool prepareSpecificationAttributes = false,
           bool forceRedirectionAfterAddingToCart = false)
        {
            return ControllerExtensions.PrepareProductOverviewModels(this, _workContext,
                _storeContext, _categoryService, _productService, _specificationAttributeService,
                _priceCalculationService, _priceFormatter, _permissionService,
                _localizationService, _taxService, _currencyService,
                _pictureService, _webHelper, _cacheManager,
                _catalogSettings, _mediaSettings, products,
                preparePriceModel, preparePictureModel,
                productThumbPictureSize, prepareSpecificationAttributes,
                forceRedirectionAfterAddingToCart);
        }


        public IActionResult Configure()
        {
            //load settings for a chosen store scope
            var storeScope = this.GetActiveStoreScopeConfiguration(_storeService, _workContext);
            var enVedetteSettings = _settingService.LoadSetting<EnVedetteSettings>(storeScope);
            var model = new ConfigurationModel();

            model.ProduitVedettes = enVedetteSettings.ProduitVedettes;
            model.ProduitVedettesNb = enVedetteSettings.ProduitVedettesNb;

            model.ProduitNouveautes = enVedetteSettings.ProduitNouveautes;
            model.ProduitNouveautesNb = enVedetteSettings.ProduitNouveautesNb;

            model.ProduitVentes = enVedetteSettings.ProduitVentes;
            model.ProduitVentesNb = enVedetteSettings.ProduitVentesNb;

            model.ProduitsDecouverte = enVedetteSettings.ProduitsDecouverte;
            model.ProduitsDecouverteNb = enVedetteSettings.ProduitsDecouverteNb;

            model.ProduitsCategorie = enVedetteSettings.ProduitsCategorie;
            model.ProduitsCategorieNb = enVedetteSettings.ProduitsCategorieNb;
            model.CategoryId = enVedetteSettings.CategoryId;

            model.ProduitsSolde = enVedetteSettings.ProduitsSolde;
            model.ProduitsSoldeNb = enVedetteSettings.ProduitsSoldeNb;
            model.ProduitsSoldePourcentage = enVedetteSettings.ProduitsSoldePourcentage;
            PrepareAllCategoriesModel(model);

            //TODO: mettre les categories dans le dropdown

            model.ActiveStoreScopeConfiguration = storeScope;
            if (storeScope > 0)
            {

                model.ProduitVedettes_OverrideForStore = _settingService.SettingExists(enVedetteSettings, x => x.ProduitVedettes, storeScope);
                model.ProduitVedettesNb_OverrideForStore = _settingService.SettingExists(enVedetteSettings, x => x.ProduitVedettesNb, storeScope);
                model.ProduitNouveautes_OverrideForStore = _settingService.SettingExists(enVedetteSettings, x => x.ProduitNouveautes, storeScope);
                model.ProduitNouveautesNb_OverrideForStore = _settingService.SettingExists(enVedetteSettings, x => x.ProduitNouveautesNb, storeScope);
                model.ProduitVentes_OverrideForStore = _settingService.SettingExists(enVedetteSettings, x => x.ProduitVentes, storeScope);
                model.ProduitVentesNb_OverrideForStore = _settingService.SettingExists(enVedetteSettings, x => x.ProduitVentesNb, storeScope);
                model.ProduitsDecouverte_OverrideForStore = _settingService.SettingExists(enVedetteSettings, x => x.ProduitsDecouverte, storeScope);
                model.ProduitsDecouverteNb_OverrideForStore = _settingService.SettingExists(enVedetteSettings, x => x.ProduitsDecouverteNb, storeScope);

                model.ProduitsCategorie_OverrideForStore = _settingService.SettingExists(enVedetteSettings, x => x.ProduitsCategorie, storeScope);
                model.ProduitsCategorieNb_OverrideForStore = _settingService.SettingExists(enVedetteSettings, x => x.ProduitsCategorieNb, storeScope);
                model.CategoryId_OverrideForStore = _settingService.SettingExists(enVedetteSettings, x => x.CategoryId, storeScope);

                model.ProduitsSolde_OverrideForStore = _settingService.SettingExists(enVedetteSettings, x => x.ProduitsSolde, storeScope);
                model.ProduitsSoldeNb_OverrideForStore = _settingService.SettingExists(enVedetteSettings, x => x.ProduitsSoldeNb, storeScope);
                model.ProduitsSoldePourcentage_OverrideForStore = _settingService.SettingExists(enVedetteSettings, x => x.ProduitsSoldePourcentage, storeScope);
            }
            return View("~/Plugins/Iclic.Widgets.EnVedette/Views/Configure.cshtml", model);
        }



        [NonAction]
        protected virtual void PrepareAllCategoriesModel(ConfigurationModel model)
        {
            if (model == null)
                throw new ArgumentNullException("model");

            model.AvailableCategories.Add(new SelectListItem
            {
                Text = "[None]",
                Value = "0"
            });
            var categories = _categoryService.GetAllCategories(showHidden: false);
            foreach (var c in categories)
            {
                model.AvailableCategories.Add(new SelectListItem
                {
                    Text = c.GetFormattedBreadCrumb(categories),
                    Value = c.Id.ToString()
                });
            }
        }

        [HttpPost]

        public IActionResult Configure(ConfigurationModel model)
        {
            //load settings for a chosen store scope
            var storeScope = this.GetActiveStoreScopeConfiguration(_storeService, _workContext);
            var enVedetteSettings = _settingService.LoadSetting<EnVedetteSettings>(storeScope);

            enVedetteSettings.ProduitVedettes = model.ProduitVedettes;
            enVedetteSettings.ProduitVedettesNb = model.ProduitVedettesNb;

            enVedetteSettings.ProduitNouveautes = model.ProduitNouveautes;
            enVedetteSettings.ProduitNouveautesNb = model.ProduitNouveautesNb;

            enVedetteSettings.ProduitVentes = model.ProduitVentes;
            enVedetteSettings.ProduitVentesNb = model.ProduitVentesNb;

            enVedetteSettings.ProduitsDecouverte = model.ProduitsDecouverte;
            enVedetteSettings.ProduitsDecouverteNb = model.ProduitsDecouverteNb;

            enVedetteSettings.ProduitsCategorie = model.ProduitsCategorie;
            enVedetteSettings.ProduitsCategorieNb = model.ProduitsCategorieNb;
            enVedetteSettings.CategoryId = model.CategoryId;

            enVedetteSettings.ProduitsSolde = model.ProduitsSolde;
            enVedetteSettings.ProduitsSoldeNb = model.ProduitsSoldeNb;
            enVedetteSettings.ProduitsSoldePourcentage = model.ProduitsSoldePourcentage;


            if (model.ProduitVedettes_OverrideForStore || storeScope == 0)
                _settingService.SaveSetting(enVedetteSettings, x => x.ProduitVedettes, storeScope, false);
            else if (storeScope > 0)
                _settingService.DeleteSetting(enVedetteSettings, x => x.ProduitVedettes, storeScope);
            if (model.ProduitVedettesNb_OverrideForStore || storeScope == 0)
                _settingService.SaveSetting(enVedetteSettings, x => x.ProduitVedettesNb, storeScope, false);
            else if (storeScope > 0)
                _settingService.DeleteSetting(enVedetteSettings, x => x.ProduitVedettesNb, storeScope);

            if (model.ProduitNouveautes_OverrideForStore || storeScope == 0)
                _settingService.SaveSetting(enVedetteSettings, x => x.ProduitNouveautes, storeScope, false);
            else if (storeScope > 0)
                _settingService.DeleteSetting(enVedetteSettings, x => x.ProduitNouveautes, storeScope);
            if (model.ProduitNouveautesNb_OverrideForStore || storeScope == 0)
                _settingService.SaveSetting(enVedetteSettings, x => x.ProduitNouveautesNb, storeScope, false);
            else if (storeScope > 0)
                _settingService.DeleteSetting(enVedetteSettings, x => x.ProduitNouveautesNb, storeScope);


            if (model.ProduitVentes_OverrideForStore || storeScope == 0)
                _settingService.SaveSetting(enVedetteSettings, x => x.ProduitVentes, storeScope, false);
            else if (storeScope > 0)
                _settingService.DeleteSetting(enVedetteSettings, x => x.ProduitVentes, storeScope);
            if (model.ProduitVentesNb_OverrideForStore || storeScope == 0)
                _settingService.SaveSetting(enVedetteSettings, x => x.ProduitVentesNb, storeScope, false);
            else if (storeScope > 0)
                _settingService.DeleteSetting(enVedetteSettings, x => x.ProduitVentesNb, storeScope);


            if (model.ProduitsDecouverte_OverrideForStore || storeScope == 0)
                _settingService.SaveSetting(enVedetteSettings, x => x.ProduitsDecouverte, storeScope, false);
            else if (storeScope > 0)
                _settingService.DeleteSetting(enVedetteSettings, x => x.ProduitsDecouverte, storeScope);
            if (model.ProduitsDecouverteNb_OverrideForStore || storeScope == 0)
                _settingService.SaveSetting(enVedetteSettings, x => x.ProduitsDecouverteNb, storeScope, false);
            else if (storeScope > 0)
                _settingService.DeleteSetting(enVedetteSettings, x => x.ProduitsDecouverteNb, storeScope);


            if (model.ProduitsCategorie_OverrideForStore || storeScope == 0)
                _settingService.SaveSetting(enVedetteSettings, x => x.ProduitsCategorie, storeScope, false);
            else if (storeScope > 0)
                _settingService.DeleteSetting(enVedetteSettings, x => x.ProduitsCategorie, storeScope);
            if (model.ProduitsCategorieNb_OverrideForStore || storeScope == 0)
                _settingService.SaveSetting(enVedetteSettings, x => x.ProduitsCategorieNb, storeScope, false);
            else if (storeScope > 0)
                _settingService.DeleteSetting(enVedetteSettings, x => x.ProduitsCategorieNb, storeScope);
            if (model.CategoryId_OverrideForStore || storeScope == 0)
                _settingService.SaveSetting(enVedetteSettings, x => x.CategoryId, storeScope, false);
            else if (storeScope > 0)
                _settingService.DeleteSetting(enVedetteSettings, x => x.CategoryId, storeScope);


            if (model.ProduitsSolde_OverrideForStore || storeScope == 0)
                _settingService.SaveSetting(enVedetteSettings, x => x.ProduitsSolde, storeScope, false);
            else if (storeScope > 0)
                _settingService.DeleteSetting(enVedetteSettings, x => x.ProduitsSolde, storeScope);
            if (model.ProduitsSoldeNb_OverrideForStore || storeScope == 0)
                _settingService.SaveSetting(enVedetteSettings, x => x.ProduitsSoldeNb, storeScope, false);
            else if (storeScope > 0)
                _settingService.DeleteSetting(enVedetteSettings, x => x.ProduitsSoldeNb, storeScope);
            if (model.ProduitsSoldePourcentage_OverrideForStore || storeScope == 0)
                _settingService.SaveSetting(enVedetteSettings, x => x.ProduitsSoldePourcentage, storeScope, false);
            else if (storeScope > 0)
                _settingService.DeleteSetting(enVedetteSettings, x => x.ProduitsSoldePourcentage, storeScope);

            //now clear settings cache
            _settingService.ClearCache();

            SuccessNotification(_localizationService.GetResource("Admin.Plugins.Saved"));
            return Configure();
        }
    }


}
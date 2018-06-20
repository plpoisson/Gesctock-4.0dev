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
using Nop.Web.Framework.Components;
using System.Collections.Generic;
using System.Linq;
using Nop.Web.Models.Catalog;
using Nop.Web.Extensions;
using Nop.Web.Framework.Controllers;
using Iclic.Widgets.EnVedette.Services;
using Nop.Web.Infrastructure.Cache;
using System;

namespace Iclic.Widgets.EnVedette.Components
{
    [ViewComponent(Name = "EnVedette")]
    public class WidgetsEnVedetteComponent : NopViewComponent
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
        private readonly IProduitSolde _produitSolde;


        #endregion

        #region Constructors
        public WidgetsEnVedetteComponent(IWorkContext workContext,
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
                IOrderReportService orderReportService,
                IProduitSolde produitSolde)
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
            this._produitSolde = produitSolde;

        }

        #endregion

        public IViewComponentResult Invoke(string widgetZone, object additionalData)
        {
            var enVedetteSettings = _settingService.LoadSetting<EnVedetteSettings>(_storeContext.CurrentStore.Id);

            IList<int> filterableSpecificationAttributeOptionIds;
            IEnumerable<Product> enVedettes, nouveautes, meilleurVentes, decouverte, categorie, solde;
            if (enVedetteSettings.ProduitVedettes)
            {
                enVedettes = _productService.SearchProducts(
                        out filterableSpecificationAttributeOptionIds, true,
                        categoryIds: new List<int>() { 26 },
                        storeId: _storeContext.CurrentStore.Id,
                        visibleIndividuallyOnly: false,
                        featuredProducts: false);
                if (enVedetteSettings.ProduitVedettesNb != 0)
                    enVedettes = enVedettes.Take(enVedetteSettings.ProduitVedettesNb);
            }
            else
                enVedettes = new List<Product>();

            if (enVedetteSettings.ProduitNouveautes)
            {
                nouveautes =  _productService.SearchProducts(
                        out filterableSpecificationAttributeOptionIds, true,
                        categoryIds: null,
                        storeId: _storeContext.CurrentStore.Id,
                        visibleIndividuallyOnly: false,
                        featuredProducts: false).OrderByDescending(p => p.CreatedOnUtc);
                if (enVedetteSettings.ProduitNouveautesNb != 0)
                    nouveautes = nouveautes.Take(enVedetteSettings.ProduitNouveautesNb);
            }
            else
                nouveautes = new List<Product>();


            if (enVedetteSettings.ProduitVentes)
            {
                var report =_cacheManager.Get(
                        string.Format(ModelCacheEventConsumer.HOMEPAGE_BESTSELLERS_IDS_KEY,
                            _storeContext.CurrentStore.Id),
                        () => _orderReportService.BestSellersReport(storeId: _storeContext.CurrentStore.Id));
                meilleurVentes = _productService.GetProductsByIds(report.Select(x => x.ProductId).ToArray());
                meilleurVentes = meilleurVentes.Where(p => _aclService.Authorize(p) && _storeMappingService.Authorize(p)).ToList();

                meilleurVentes = meilleurVentes.Where(p => p.IsAvailable()).Where(p => p.IsAvailable());
                if (enVedetteSettings.ProduitNouveautesNb != 0)
                    meilleurVentes = meilleurVentes.Take(enVedetteSettings.ProduitNouveautesNb).ToList();
            }
            else
                meilleurVentes = new List<Product>();

            if (enVedetteSettings.ProduitsDecouverte)
            {
                decouverte =  _productService.SearchProducts(
                       out filterableSpecificationAttributeOptionIds, true,
                       categoryIds: null,
                       storeId: _storeContext.CurrentStore.Id,
                       visibleIndividuallyOnly: false,
                       featuredProducts: false).OrderBy(x => Guid.NewGuid());
                if (enVedetteSettings.ProduitsDecouverteNb != 0)
                    decouverte = decouverte.Take(enVedetteSettings.ProduitsDecouverteNb).ToList();
            }
            else
                decouverte = new List<Product>();

            if (enVedetteSettings.ProduitsCategorie)
            {
                categorie = _productService.SearchProducts(categoryIds: new List<int>() { enVedetteSettings.CategoryId },
                        storeId: _storeContext.CurrentStore.Id,
                        visibleIndividuallyOnly: false,
                        featuredProducts: false);

                if (enVedetteSettings.ProduitsCategorieNb != 0)
                    categorie = categorie.Take(enVedetteSettings.ProduitVedettesNb);

            }
            else
                categorie = new List<Product>();

            if (enVedetteSettings.ProduitsSolde)
            {
                //GetAllProductSolde 
                solde = _produitSolde.GetAllProductsSolde(_storeContext.CurrentStore.Id, enVedetteSettings.ProduitsSoldePourcentage);

                if (enVedetteSettings.ProduitsSoldeNb != 0)
                    solde = solde.Take(enVedetteSettings.ProduitsSoldeNb);
            }
            else
                solde = new List<Product>();
            
            var model = new PublicInfoModel
            {
                EnVedette = PrepareProductOverviewModels(enVedettes, true, true, null).ToList(),
                Nouveaute = PrepareProductOverviewModels(nouveautes, true, true, null).ToList(),
                MeilleurVentes = PrepareProductOverviewModels(meilleurVentes, true, true, null).ToList(),
                Decouverte = PrepareProductOverviewModels(decouverte, true, true, null).ToList(),
                Categorie = PrepareProductOverviewModels(categorie, true, true, null).ToList(),
                Solde = PrepareProductOverviewModels(solde, true, true, null).ToList(),

                

            };


            return View("~/Plugins/Iclic.Widgets.EnVedette/Views/PublicInfo.cshtml", model);
        }


        private BasePluginController _basePluginController;
        [NonAction]
        protected virtual IEnumerable<ProductOverviewModel> PrepareProductOverviewModels(IEnumerable<Product> products,
           bool preparePriceModel = true, bool preparePictureModel = true,
           int? productThumbPictureSize = null, bool prepareSpecificationAttributes = false,
           bool forceRedirectionAfterAddingToCart = false)
        {
            return ControllerExtensions.PrepareProductOverviewModels(_basePluginController, _workContext,
                _storeContext, _categoryService, _productService, _specificationAttributeService,
                _priceCalculationService, _priceFormatter, _permissionService,
                _localizationService, _taxService, _currencyService,
                _pictureService, _webHelper, _cacheManager,
                _catalogSettings, _mediaSettings, products,
                preparePriceModel, preparePictureModel,
                productThumbPictureSize, prepareSpecificationAttributes,
                forceRedirectionAfterAddingToCart);
        }

    }
}

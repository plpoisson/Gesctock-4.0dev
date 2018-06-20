using Iclic.Plugin.Widgets.BlocPub.Infrastructure.Cache;
using Iclic.Plugin.Widgets.BlocPub.Models;
using Microsoft.AspNetCore.Mvc;
using Nop.Core;
using Nop.Core.Caching;
using Nop.Services.Catalog;
using Nop.Services.Configuration;
using Nop.Services.Localization;
using Nop.Services.Media;
using Nop.Services.Seo;
using Nop.Services.Stores;
using Nop.Web.Framework.Components;


namespace Iclic.Plugin.Widgets.BlocPub.Components
{
    [ViewComponent(Name = "BlocPub")]
    public class WidgetsBlocPubComponent : NopViewComponent
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
        public WidgetsBlocPubComponent(IWorkContext workContext,
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


        public IViewComponentResult Invoke(string widgetZone, object additionalData)
        {
            var blocPubSetting = _settingService.LoadSetting<BlocPubSetting>(_storeContext.CurrentStore.Id);

            
            var model = new PublicInfoModel();
            model.Picture1Url = GetPictureUrl(blocPubSetting.Picture1Id);
            model.Nom1 = blocPubSetting.Nom1;
            var tag = _productTagService.GetProductTagById(blocPubSetting.Tag1);
            if (tag != null)
            {
                model.Tag1Id = tag.Id;
                model.Tag1Url = tag.GetSeName();
            }
            model.Url1 = blocPubSetting.Url1;
            model.DateDebut1 = blocPubSetting.DateDebut1;
            model.DateFin1 = blocPubSetting.DateFin1;


            model.Picture2Url = GetPictureUrl(blocPubSetting.Picture2Id);
            model.Nom2 = blocPubSetting.Nom2;
            tag = _productTagService.GetProductTagById(blocPubSetting.Tag2);
            if (tag != null)
            {
                model.Tag2Id = tag.Id;
                model.Tag2Url = tag.GetSeName();
            }
            model.Url2 = blocPubSetting.Url2;
            model.DateDebut2 = blocPubSetting.DateDebut2;
            model.DateFin2 = blocPubSetting.DateFin2;

            model.Picture3Url = GetPictureUrl(blocPubSetting.Picture3Id);
            model.Nom3 = blocPubSetting.Nom3;
            tag = _productTagService.GetProductTagById(blocPubSetting.Tag3);
            if (tag != null)
            {
                model.Tag3Id = tag.Id;
                model.Tag3Url = tag.GetSeName();
            }
            model.Url3 = blocPubSetting.Url3;
            model.DateDebut3 = blocPubSetting.DateDebut3;
            model.DateFin3 = blocPubSetting.DateFin3;

            model.Picture4Url = GetPictureUrl(blocPubSetting.Picture4Id);
            model.Nom4 = blocPubSetting.Nom4;
            tag = _productTagService.GetProductTagById(blocPubSetting.Tag4);
            if (tag != null)
            {
                model.Tag4Id = tag.Id;
                model.Tag4Url = tag.GetSeName();
            }
            model.Url4 = blocPubSetting.Url4;
            model.DateDebut4 = blocPubSetting.DateDebut4;
            model.DateFin4 = blocPubSetting.DateFin4;

            model.Picture5Url = GetPictureUrl(blocPubSetting.Picture5Id);
            model.Nom5 = blocPubSetting.Nom5;
            tag = _productTagService.GetProductTagById(blocPubSetting.Tag5);
            if (tag != null)
            {
                model.Tag5Id = tag.Id;
                model.Tag5Url = tag.GetSeName();
            }
            model.Url5 = blocPubSetting.Url5;
            model.DateDebut5 = blocPubSetting.DateDebut5;
            model.DateFin5 = blocPubSetting.DateFin5;

            model.Picture6Url = GetPictureUrl(blocPubSetting.Picture6Id);
            model.Nom6 = blocPubSetting.Nom6;
            tag = _productTagService.GetProductTagById(blocPubSetting.Tag6);
            if (tag != null)
            {
                model.Tag6Id = tag.Id;
                model.Tag6Url = tag.GetSeName();
            }
            model.Url6 = blocPubSetting.Url6;
            model.DateDebut6 = blocPubSetting.DateDebut6;
            model.DateFin6 = blocPubSetting.DateFin6;

            model.Picture7Url = GetPictureUrl(blocPubSetting.Picture7Id);
            model.Nom7 = blocPubSetting.Nom7;
            tag = _productTagService.GetProductTagById(blocPubSetting.Tag7);
            if (tag != null)
            {
                model.Tag7Id = tag.Id;
                model.Tag7Url = tag.GetSeName();
            }
            model.Url7 = blocPubSetting.Url7;
            model.DateDebut7 = blocPubSetting.DateDebut7;
            model.DateFin7 = blocPubSetting.DateFin7;

            model.Picture8Url = GetPictureUrl(blocPubSetting.Picture8Id);
            model.Nom8 = blocPubSetting.Nom8;
            tag = _productTagService.GetProductTagById(blocPubSetting.Tag8);
            if (tag != null)
            {
                model.Tag8Id = tag.Id;
                model.Tag8Url = tag.GetSeName();
            }
            model.Url8 = blocPubSetting.Url8;
            model.DateDebut8 = blocPubSetting.DateDebut8;
            model.DateFin8 = blocPubSetting.DateFin8;

            model.Picture9Url = GetPictureUrl(blocPubSetting.Picture9Id);
            model.Nom9 = blocPubSetting.Nom9;
            tag = _productTagService.GetProductTagById(blocPubSetting.Tag9);
            if (tag != null)
            {
                model.Tag9Id = tag.Id;
                model.Tag9Url = tag.GetSeName();
            }
            model.Url9 = blocPubSetting.Url9;
            model.DateDebut9 = blocPubSetting.DateDebut9;
            model.DateFin9 = blocPubSetting.DateFin9;

            model.Picture10Url = GetPictureUrl(blocPubSetting.Picture10Id);
            model.Nom10 = blocPubSetting.Nom10;
            tag = _productTagService.GetProductTagById(blocPubSetting.Tag10);
            if (tag != null)
            {
                model.Tag10Id = tag.Id;
                model.Tag10Url = tag.GetSeName();
            }
            model.Url10 = blocPubSetting.Url10;
            model.DateDebut10 = blocPubSetting.DateDebut10;
            model.DateFin10 = blocPubSetting.DateFin10;
            return View("~/Plugins/Iclic.Plugin.Widgets.BlocPub/Views/PublicInfo.cshtml", model);
        }

        protected string GetPictureUrl(int pictureId)
        {
            string cacheKey = string.Format(ModelCacheEventConsumer.PICTURE_URL_MODEL_KEY, pictureId);
            return _cacheManager.Get(cacheKey, () =>
            {

                var url = _pictureService.GetPictureUrl(pictureId, showDefaultPicture: false);
                //little hack here. nulls aren't cacheable so set it to ""
                if (url == null)
                    url = "";
                return _storeContext.CurrentStore.SslEnabled ? url.Replace("http:", "https:") : url;
            });
        }



    }
}

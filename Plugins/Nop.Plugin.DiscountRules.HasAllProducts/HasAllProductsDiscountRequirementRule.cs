using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Nop.Core.Domain.Orders;
using Nop.Core.Plugins;
using Nop.Services.Configuration;
using Nop.Services.Discounts;
using Nop.Services.Localization;
using Nop.Services.Orders;

namespace Nop.Plugin.DiscountRules.HasAllProducts
{
    public partial class HasAllProductsDiscountRequirementRule : BasePlugin, IDiscountRequirementRule
    {
        private readonly ISettingService _settingService;

        private readonly IActionContextAccessor _actionContextAccessor;
        private readonly IUrlHelperFactory _urlHelperFactory;

        public HasAllProductsDiscountRequirementRule(ISettingService settingService,
            IActionContextAccessor actionContextAccessor,
            IUrlHelperFactory urlHelperFactory)
        {
            this._settingService = settingService;
            this._actionContextAccessor = actionContextAccessor;
            this._urlHelperFactory = urlHelperFactory;
        }

        /// <summary>
        /// Check discount requirement
        /// </summary>
        /// <param name="request">Object that contains all information required to check the requirement (Current customer, discount, etc)</param>
        /// <returns>Result</returns>
        public DiscountRequirementValidationResult CheckRequirement(DiscountRequirementValidationRequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));
            
            //invalid by default
            var result = new DiscountRequirementValidationResult();

            var restrictedProductIds = _settingService.GetSettingByKey<string>($"DiscountRequirement.RestrictedProductIds-{request.DiscountRequirementId}");
            if (string.IsNullOrWhiteSpace(restrictedProductIds))
            {
                //valid
                result.IsValid = true;
                return result;
            }

            if (request.Customer == null)
                return result;

            //we support three ways of specifying products:
            //1. The comma-separated list of product identifiers (e.g. 77, 123, 156).
            //2. The comma-separated list of product identifiers with quantities.
            //      {Product ID}:{Quantity}. For example, 77:1, 123:2, 156:3
            //3. The comma-separated list of product identifiers with quantity range.
            //      {Product ID}:{Min quantity}-{Max quantity}. For example, 77:1-3, 123:2-5, 156:3-8
            var restrictedProducts = restrictedProductIds
                .Split(new[] {','}, StringSplitOptions.RemoveEmptyEntries)
                .Select(x => x.Trim())
                .ToList();
            if (!restrictedProducts.Any())
                return result;

            //group products in the cart by product ID
            //it could be the same product with distinct product attributes
            //that's why we get the total quantity of this product
            var cartQuery = from sci in request.Customer.ShoppingCartItems.LimitPerStore(request.Store.Id)
                            where sci.ShoppingCartType == ShoppingCartType.ShoppingCart
                            group sci by sci.ProductId into g
                            select new { ProductId = g.Key, TotalQuantity = g.Sum(x => x.Quantity) };
            var cart = cartQuery.ToList();

            var allFound = true;
            foreach (var restrictedProduct in restrictedProducts)
            {
                if (string.IsNullOrWhiteSpace(restrictedProduct))
                    continue;

                var found1 = false;
                foreach (var sci in cart)
                {
                    if (restrictedProduct.Contains(":"))
                    {
                        if (restrictedProduct.Contains("-"))
                         {
                            //the third way (the quantity rage specified)
                            //{Product ID}:{Min quantity}-{Max quantity}. For example, 77:1-3, 123:2-5, 156:3-8
                            if (!int.TryParse(restrictedProduct.Split(':')[0], out int restrictedProductId))
                                //parsing error; exit;
                                return result;
                            if (!int.TryParse(restrictedProduct.Split(':')[1].Split('-')[0], out int quantityMin))
                                //parsing error; exit;
                                return result;
                            if (!int.TryParse(restrictedProduct.Split(':')[1].Split('-')[1], out int quantityMax))
                                //parsing error; exit;
                                return result;

                            if (sci.ProductId == restrictedProductId && quantityMin <= sci.TotalQuantity && sci.TotalQuantity <= quantityMax)
                             {
                                 found1 = true;
                                 break;
                             }
                         }
                         else
                         {
                            //the second way (the quantity specified)
                            //{Product ID}:{Quantity}. For example, 77:1, 123:2, 156:3
                            if (!int.TryParse(restrictedProduct.Split(':')[0], out int restrictedProductId))
                                //parsing error; exit;
                                return result;
                            int quantity;
                             if (!int.TryParse(restrictedProduct.Split(':')[1], out quantity))
                                 //parsing error; exit;
                                 return result;

                             if (sci.ProductId == restrictedProductId && sci.TotalQuantity == quantity)
                             {
                                 found1 = true;
                                 break;
                             }
                         }
                    }
                    else
                    {
                        //the first way (the quantity is not specified)
                        if (int.TryParse(restrictedProduct, out int restrictedProductId))
                        {
                            if (sci.ProductId == restrictedProductId)
                            {
                                found1 = true;
                                break;
                            }
                        }
                    }
                }

                if (!found1)
                {
                    allFound = false;
                    break;
                }
            }

            if (allFound)
            {
                //valid
                result.IsValid = true;
                return result;
            }

            return result;
        }

        /// <summary>
        /// Get URL for rule configuration
        /// </summary>
        /// <param name="discountId">Discount identifier</param>
        /// <param name="discountRequirementId">Discount requirement identifier (if editing)</param>
        /// <returns>URL</returns>
        public string GetConfigurationUrl(int discountId, int? discountRequirementId)
        {
            var urlHelper = _urlHelperFactory.GetUrlHelper(_actionContextAccessor.ActionContext);
            return urlHelper.Action("Configure", "DiscountRulesHasAllProducts",
                new { discountId = discountId, discountRequirementId = discountRequirementId }).TrimStart('/');
        }

        public override void Install()
        {
            //locales
            this.AddOrUpdatePluginLocaleResource("Plugins.DiscountRules.HasAllProducts.Fields.Products", "Restricted products [and quantity range]");
            this.AddOrUpdatePluginLocaleResource("Plugins.DiscountRules.HasAllProducts.Fields.Products.Hint", "The comma-separated list of product identifiers (e.g. 77, 123, 156). You can find a product ID on its details page. You can also specify the comma-separated list of product identifiers with quantities ({Product ID}:{Quantity}. for example, 77:1, 123:2, 156:3). And you can also specify the comma-separated list of product identifiers with quantity range ({Product ID}:{Min quantity}-{Max quantity}. for example, 77:1-3, 123:2-5, 156:3-8).");
            this.AddOrUpdatePluginLocaleResource("Plugins.DiscountRules.HasAllProducts.Fields.Products.AddNew", "Add product");
            this.AddOrUpdatePluginLocaleResource("Plugins.DiscountRules.HasAllProducts.Fields.Products.Choose", "Choose");
            base.Install();
        }

        public override void Uninstall()
        {
            //locales
            this.DeletePluginLocaleResource("Plugins.DiscountRules.HasAllProducts.Fields.Products");
            this.DeletePluginLocaleResource("Plugins.DiscountRules.HasAllProducts.Fields.Products.Hint");
            this.DeletePluginLocaleResource("Plugins.DiscountRules.HasAllProducts.Fields.Products.AddNew");
            this.DeletePluginLocaleResource("Plugins.DiscountRules.HasAllProducts.Fields.Products.Choose");
            base.Uninstall();
        }
    }
}
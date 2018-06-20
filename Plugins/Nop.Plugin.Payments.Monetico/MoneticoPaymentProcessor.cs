using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Web;
using Microsoft.AspNetCore.Http;
using Nop.Core;
using Nop.Core.Domain.Directory;
using Nop.Core.Domain.Orders;
using Nop.Core.Domain.Payments;
using Nop.Core.Domain.Shipping;
using Nop.Core.Plugins;
using Nop.Services.Common;
using Nop.Services.Configuration;
using Nop.Services.Directory;
using Nop.Services.Localization;
using Nop.Services.Orders;
using Nop.Services.Payments;
using Nop.Services.Tax;
using Nop.Services.Vendors;

namespace Nop.Plugin.Payments.Monetico
{
    /// <summary>
    /// Monetico payment processor
    /// </summary>
    public class MoneticoPaymentProcessor : BasePlugin, IPaymentMethod
    {


        #region Fields

        private MoneticoPaymentSettings _moneticoPaymentProcessorPaymentSettings;
        private readonly ISettingService _settingService;
        private readonly ICurrencyService _currencyService;
        private readonly CurrencySettings _currencySettings;
        private readonly IWebHelper _webHelper;
        private readonly ICheckoutAttributeParser _checkoutAttributeParser;
        private readonly ITaxService _taxService;
        private readonly IOrderTotalCalculationService _orderTotalCalculationService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IVendorService _vendorService;
        private readonly ILocalizationService _localizationService;
        #endregion

        #region Ctor

        public MoneticoPaymentProcessor(MoneticoPaymentSettings moneticoPaymentProcessorPaymentSettings,
            ISettingService settingService, ICurrencyService currencyService,
            CurrencySettings currencySettings, IWebHelper webHelper,
            ICheckoutAttributeParser checkoutAttributeParser, ITaxService taxService,
            IOrderTotalCalculationService orderTotalCalculationService, IHttpContextAccessor httpContextAccessor, IVendorService vendorService, ILocalizationService localizationService)
        {
            this._moneticoPaymentProcessorPaymentSettings = moneticoPaymentProcessorPaymentSettings;
            this._settingService = settingService;
            this._currencyService = currencyService;
            this._currencySettings = currencySettings;
            this._webHelper = webHelper;
            this._checkoutAttributeParser = checkoutAttributeParser;
            this._taxService = taxService;
            this._orderTotalCalculationService = orderTotalCalculationService;
            this._httpContextAccessor = httpContextAccessor;
            this._vendorService = vendorService;
            this._localizationService = localizationService;
        }

        #endregion

        #region Utilities

        /// <summary>
        /// Gets Monetico URL
        /// </summary>
        /// <returns></returns>
        private string GetMoneticoUrl()
        {
            //return "";
            return _moneticoPaymentProcessorPaymentSettings.UseSandbox ? "https://p.monetico-services.com/test/paiement.cgi" : "https://p.monetico-services.com/paiement.cgi";
        }
        #endregion


        #region Methods


        /// <summary>
        /// Process a payment
        /// </summary>
        /// <param name="processPaymentRequest">Payment info required for an order processing</param>
        /// <returns>Process payment result</returns>
        public ProcessPaymentResult ProcessPayment(ProcessPaymentRequest processPaymentRequest)
        {
            var result = new ProcessPaymentResult
            {
                NewPaymentStatus = PaymentStatus.Pending
            };
            return result;
        }

        /// <summary>
        /// Post process payment (used by payment gateways that require redirecting to a third-party URL)
        /// </summary>
        /// <param name="postProcessPaymentRequest">Payment info required for an order processing</param>
        public void PostProcessPayment(PostProcessPaymentRequest postProcessPaymentRequest)
        {
            //get store location
            var storeLocation = _webHelper.GetStoreLocation();

            _moneticoPaymentProcessorPaymentSettings = _settingService.LoadSetting<MoneticoPaymentSettings>(postProcessPaymentRequest.Order.StoreId);

            string successUrl = $"{storeLocation}Plugins/Monetico/ConfirmOrder?order_ref=" + postProcessPaymentRequest.Order.Id;
            string cancelUrl = $"{storeLocation}Plugins/Monetico/CancelOrder?order_ref=" + postProcessPaymentRequest.Order.Id;

            string dateFormatted = postProcessPaymentRequest.Order.CreatedOnUtc.ToString("dd/MM/yyyy:HH:mm:ss").Replace("-", "/");
            string montant = postProcessPaymentRequest.Order.OrderTotal.ToString().TrimEnd('0');

            string hmacString = WebUtility.HtmlEncode(_moneticoPaymentProcessorPaymentSettings.TPE) + "*" +
                dateFormatted + "*" +
                montant.Replace(",", ".") + "CAD*" +
                postProcessPaymentRequest.Order.Id.ToString() + "*" +
                "" + "*" +
                "3.0*" +
                "FR*" +//(postProcessPaymentRequest.Order.CustomerLanguageId == 1 ? "FR" : "EN") + "*" +
                _moneticoPaymentProcessorPaymentSettings.CodeSociete + "*" +
                postProcessPaymentRequest.Order.BillingAddress.Email + "*" +
                "*********";
            //this.sNbrEch + "*" +
            //this.sDateEcheance1 + "*" + this.sMontantEcheance1 + "*" +
            //this.sDateEcheance2 + "*" + this.sMontantEcheance2 + "*" +
            //this.sDateEcheance3 + "*" + this.sMontantEcheance3 + "*" +
            //this.sDateEcheance4 + "*" + this.sMontantEcheance4 + "*" +
            //sOptions;

            CMCIC_Hmac hmac = new CMCIC_Hmac(_moneticoPaymentProcessorPaymentSettings.CLE);
            string MAC = hmac.computeHmac(hmacString);

            var builder = new StringBuilder();
            builder.Append(GetMoneticoUrl());
            builder.AppendFormat("?TPE={0}&date={1}&montant={2}CAD&reference={3}&texte-libre={4}&version=3.0&lgue={5}&societe={6}&mail={7}&MAC={8}&url_retour_ok={9}&url_retour_err={10}",
                _moneticoPaymentProcessorPaymentSettings.TPE,
                dateFormatted,
                montant.Replace(",", "."),
                postProcessPaymentRequest.Order.Id.ToString(),
                "",
                "FR",//postProcessPaymentRequest.Order.CustomerLanguageId == 1 ? "FR" : "EN",
                _moneticoPaymentProcessorPaymentSettings.CodeSociete,
                postProcessPaymentRequest.Order.BillingAddress.Email,
                MAC,
                WebUtility.UrlEncode(successUrl),
                WebUtility.UrlEncode(cancelUrl)
                );

            _httpContextAccessor.HttpContext.Response.Redirect(builder.ToString());
        }

        /// <summary>
        /// Returns a value indicating whether payment method should be hidden during checkout
        /// </summary>
        /// <param name="cart">Shopping cart</param>
        /// <returns>true - hide; false - display.</returns>
        public bool HidePaymentMethod(IList<ShoppingCartItem> cart)
        {
            //you can put any logic here
            //for example, hide this payment method if all products in the cart are downloadable
            //or hide this payment method if current customer is from certain country
            return false;
        }

        /// <summary>
        /// Gets additional handling fee
        /// </summary>
        /// <param name="cart">Shoping cart</param>
        /// <returns>Additional handling fee</returns>
        public decimal GetAdditionalHandlingFee(IList<ShoppingCartItem> cart)
        {
            return 0;
            /*var result = this.CalculateAdditionalFee(_orderTotalCalculationService, cart,
                _paypalStandardPaymentSettings.AdditionalFee, _paypalStandardPaymentSettings.AdditionalFeePercentage);
            return result;*/
        }


        /// <summary>
        /// Captures payment
        /// </summary>
        /// <param name="capturePaymentRequest">Capture payment request</param>
        /// <returns>Capture payment result</returns>
        public CapturePaymentResult Capture(CapturePaymentRequest capturePaymentRequest)
        {
            var result = new CapturePaymentResult();
            result.AddError("Capture method not supported");
            return result;
        }

        /// <summary>
        /// Refunds a payment
        /// </summary>
        /// <param name="refundPaymentRequest">Request</param>
        /// <returns>Result</returns>
        public RefundPaymentResult Refund(RefundPaymentRequest refundPaymentRequest)
        {
            var result = new RefundPaymentResult();
            result.AddError("Refund method not supported");
            return result;
        }

        /// <summary>
        /// Voids a payment
        /// </summary>
        /// <param name="voidPaymentRequest">Request</param>
        /// <returns>Result</returns>
        public VoidPaymentResult Void(VoidPaymentRequest voidPaymentRequest)
        {
            var result = new VoidPaymentResult();
            result.AddError("Void method not supported");
            return result;
        }

        /// <summary>
        /// Process recurring payment
        /// </summary>
        /// <param name="processPaymentRequest">Payment info required for an order processing</param>
        /// <returns>Process payment result</returns>
        public ProcessPaymentResult ProcessRecurringPayment(ProcessPaymentRequest processPaymentRequest)
        {
            var result = new ProcessPaymentResult();
            result.AddError("Recurring payment not supported");
            return result;
        }

        /// <summary>
        /// Cancels a recurring payment
        /// </summary>
        /// <param name="cancelPaymentRequest">Request</param>
        /// <returns>Result</returns>
        public CancelRecurringPaymentResult CancelRecurringPayment(CancelRecurringPaymentRequest cancelPaymentRequest)
        {
            var result = new CancelRecurringPaymentResult();
            result.AddError("Recurring payment not supported");
            return result;
        }

        /// <summary>
        /// Gets a value indicating whether customers can complete a payment after order is placed but not completed (for redirection payment methods)
        /// </summary>
        /// <param name="order">Order</param>
        /// <returns>Result</returns>
        public bool CanRePostProcessPayment(Order order)
        {
            if (order == null)
                throw new ArgumentNullException("order");

            //let's ensure that at least 5 seconds passed after order is placed
            //P.S. there's no any particular reason for that. we just do it
            if ((DateTime.UtcNow - order.CreatedOnUtc).TotalSeconds < 5)
                return false;

            return true;
        }

        /// <summary>
        /// Validate payment form
        /// </summary>
        /// <param name="form">The parsed form values</param>
        /// <returns>List of validating errors</returns>
        public IList<string> ValidatePaymentForm(IFormCollection form)
        {
            return new List<string>();
        }

        /// <summary>
        /// Get payment information
        /// </summary>
        /// <param name="form">The parsed form values</param>
        /// <returns>Payment info holder</returns>
        public ProcessPaymentRequest GetPaymentInfo(IFormCollection form)
        {
            return new ProcessPaymentRequest();
        }

        /// <summary>
        /// Gets a configuration page URL
        /// </summary>
        public override string GetConfigurationPageUrl()
        {
            return _webHelper.GetStoreLocation() + "Admin/Monetico/Configure";
        }

        /// <summary>
        /// Gets a view component for displaying plugin in public store ("payment info" checkout step)
        /// </summary>
        /// <param name="viewComponentName">View component name</param>
        public void GetPublicViewComponent(out string viewComponentName)
        {
            viewComponentName = "Monetico";
        }

        public override void Install()
        {
            //settings
            var settings = new MoneticoPaymentSettings
            {
                UseSandbox = true,
                TPE = "YourTPE",
                CodeSociete = "YourSociete",
                CLE = "12345678901234567890123456789012345678P0",
                Actif = false
            };
            _settingService.SaveSetting(settings);

            //locales

            base.Install();
        }

        public override void Uninstall()
        {
            //settings
            _settingService.DeleteSetting<MoneticoPaymentSettings>();

            //locales


            base.Uninstall();
        }


        #endregion

        #region Properties

        /// <summary>
        /// Gets a value indicating whether capture is supported
        /// </summary>
        public bool SupportCapture
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// Gets a value indicating whether partial refund is supported
        /// </summary>
        public bool SupportPartiallyRefund
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// Gets a value indicating whether refund is supported
        /// </summary>
        public bool SupportRefund
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// Gets a value indicating whether void is supported
        /// </summary>
        public bool SupportVoid
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// Gets a recurring payment type of payment method
        /// </summary>
        public RecurringPaymentType RecurringPaymentType
        {
            get
            {
                return RecurringPaymentType.NotSupported;
            }
        }

        /// <summary>
        /// Gets a payment method type
        /// </summary>
        public PaymentMethodType PaymentMethodType
        {
            get
            {
                return PaymentMethodType.Redirection;
            }
        }

        /// <summary>
        /// Gets a value indicating whether we should display a payment information page for this plugin
        /// </summary>
        public bool SkipPaymentInfo
        {
            get
            {
                return false;
            }
        }


        /// <summary>
        /// Gets a payment method description that will be displayed on checkout pages in the public store
        /// </summary>
        public string PaymentMethodDescription
        {
            get { return _localizationService.GetResource("Plugins.Payments.Monetico.PaymentMethodDescription"); }
        }

        #endregion
    }
}

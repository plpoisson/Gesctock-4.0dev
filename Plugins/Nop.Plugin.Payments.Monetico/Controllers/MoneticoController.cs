using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Nop.Core;
using Nop.Core.Domain.Orders;
using Nop.Core.Domain.Payments;
using Nop.Plugin.Payments.Monetico;
using Nop.Plugin.Payments.Monetico.Models;
using Nop.Services.Configuration;
using Nop.Services.Localization;
using Nop.Services.Logging;
using Nop.Services.Orders;
using Nop.Services.Payments;
using Nop.Services.Stores;
using Nop.Web.Framework.Controllers;
using Nop.Core.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Nop.Web.Framework;
using Microsoft.AspNetCore.Http;
using Nop.Web.Framework.Mvc.Filters;

namespace Nop.Plugin.Payments.Monetico.Controllers
{

    public class MoneticoController : BasePaymentController
    {
        private readonly IWorkContext _workContext;
        private readonly IStoreService _storeService;
        private readonly ISettingService _settingService;
        private readonly IPaymentService _paymentService;
        private readonly IOrderService _orderService;
        private readonly IOrderProcessingService _orderProcessingService;
        private readonly ILocalizationService _localizationService;
        private readonly IStoreContext _storeContext;
        private readonly ILogger _logger;
        private readonly IWebHelper _webHelper;
        private readonly PaymentSettings _paymentSettings;
        private readonly MoneticoPaymentSettings _moneticoPaymentSettings;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public MoneticoController(IWorkContext workContext,
            IStoreService storeService,
            ISettingService settingService,
            IPaymentService paymentService,
            IOrderService orderService,
            IOrderProcessingService orderProcessingService,
            ILocalizationService localizationService,
            IStoreContext storeContext,
            ILogger logger,
            IWebHelper webHelper,
            PaymentSettings paymentSettings,
            MoneticoPaymentSettings moneticoPaymentSettings,
           IHttpContextAccessor httpContextAccessor)
        {
            this._workContext = workContext;
            this._storeService = storeService;
            this._settingService = settingService;
            this._paymentService = paymentService;
            this._orderService = orderService;
            this._orderProcessingService = orderProcessingService;
            this._localizationService = localizationService;
            this._storeContext = storeContext;
            this._logger = logger;
            this._webHelper = webHelper;
            this._paymentSettings = paymentSettings;
            this._moneticoPaymentSettings = moneticoPaymentSettings;
            this._httpContextAccessor = httpContextAccessor;
        }


        [AuthorizeAdmin]
        [Area(AreaNames.Admin)]
        public IActionResult Configure()
        {
            //load settings for a chosen store scope
            var storeScope = this.GetActiveStoreScopeConfiguration(_storeService, _workContext);
            var moneticoPaymentSettings = _settingService.LoadSetting<MoneticoPaymentSettings>(storeScope);

            var model = new ConfigurationModel();
            model.UseSandbox = moneticoPaymentSettings.UseSandbox;
            model.TPE = moneticoPaymentSettings.TPE;
            model.CodeSociete = moneticoPaymentSettings.CodeSociete;
            model.CLE = moneticoPaymentSettings.CLE;
            model.Actif = moneticoPaymentSettings.Actif;

            model.ActiveStoreScopeConfiguration = storeScope;
            if (storeScope > 0)
            {
                model.UseSandbox_OverrideForStore = _settingService.SettingExists(moneticoPaymentSettings, x => x.UseSandbox, storeScope);
                model.TPE_OverrideForStore = _settingService.SettingExists(moneticoPaymentSettings, x => x.TPE, storeScope);
                model.CodeSociete_OverrideForStore = _settingService.SettingExists(moneticoPaymentSettings, x => x.CodeSociete, storeScope);
                model.CLE_OverrideForStore = _settingService.SettingExists(moneticoPaymentSettings, x => x.CLE, storeScope);
                model.Actif_OverrideForStore = _settingService.SettingExists(moneticoPaymentSettings, x => x.Actif, storeScope);
            }

            return View("~/Plugins/Payments.Monetico/Views/Configure.cshtml", model);
        }

        [HttpPost]
        [AuthorizeAdmin]
        [Area(AreaNames.Admin)]
        public IActionResult Configure(ConfigurationModel model)
        {
            if (!ModelState.IsValid)
                return Configure();

            //load settings for a chosen store scope
            var storeScope = this.GetActiveStoreScopeConfiguration(_storeService, _workContext);
            var moneticoPaymentSettings = _settingService.LoadSetting<MoneticoPaymentSettings>(storeScope);

            //save settings
            moneticoPaymentSettings.UseSandbox = model.UseSandbox;
            moneticoPaymentSettings.TPE = model.TPE;
            moneticoPaymentSettings.CodeSociete = model.CodeSociete;
            moneticoPaymentSettings.CLE = model.CLE;
            moneticoPaymentSettings.Actif = model.Actif;

            /* We do not clear cache after each setting update.
             * This behavior can increase performance because cached settings will not be cleared 
             * and loaded from database after each update */
            if (model.UseSandbox_OverrideForStore || storeScope == 0)
                _settingService.SaveSetting(moneticoPaymentSettings, x => x.UseSandbox, storeScope, false);
            else if (storeScope > 0)
                _settingService.DeleteSetting(moneticoPaymentSettings, x => x.UseSandbox, storeScope);

            if (model.TPE_OverrideForStore || storeScope == 0)
                _settingService.SaveSetting(moneticoPaymentSettings, x => x.TPE, storeScope, false);
            else if (storeScope > 0)
                _settingService.DeleteSetting(moneticoPaymentSettings, x => x.TPE, storeScope);

            if (model.CodeSociete_OverrideForStore || storeScope == 0)
                _settingService.SaveSetting(moneticoPaymentSettings, x => x.CodeSociete, storeScope, false);
            else if (storeScope > 0)
                _settingService.DeleteSetting(moneticoPaymentSettings, x => x.CodeSociete, storeScope);

            if (model.CLE_OverrideForStore || storeScope == 0)
                _settingService.SaveSetting(moneticoPaymentSettings, x => x.CLE, storeScope, false);
            else if (storeScope > 0)
                _settingService.DeleteSetting(moneticoPaymentSettings, x => x.CLE, storeScope);

            if (model.Actif_OverrideForStore || storeScope == 0)
                _settingService.SaveSetting(moneticoPaymentSettings, x => x.Actif, storeScope, false);
            else if (storeScope > 0)
                _settingService.DeleteSetting(moneticoPaymentSettings, x => x.Actif, storeScope);
            //now clear settings cache
            _settingService.ClearCache();

            SuccessNotification(_localizationService.GetResource("Admin.Plugins.Saved"));

            return Configure();
        }


        public IActionResult PaymentInfo()
        {
            return View("~/Plugins/Payments.Monetico/Views/PaymentInfo.cshtml");
        }

        [NonAction]
        public IList<string> ValidatePaymentForm(FormCollection form)
        {
            var warnings = new List<string>();
            return warnings;
        }

        [NonAction]
        public ProcessPaymentRequest GetPaymentInfo(FormCollection form)
        {
            var paymentInfo = new ProcessPaymentRequest();
            return paymentInfo;
        }


//TPE=1234567&date=05%2f12%2f2006%5fa%5f11%3a55%3a23&montant=62%2e75CA
//D&reference=ABERTYP00145&MAC=e4359a2c18d86cf2e4b0e646016c202e89947b0
//4&texte-libre=LeTexteLibre&coderetour=paiement&cvx=oui&vld=1208&brand=VI&status3ds=1&numauto=010101
//&originecb=CAN&bincb=010101&hpancb=74E94B03C22D786E0F2C2CADBFC1C00B0
//04B7C45&ipclient=127%2e0%2e0%2e1&originetr=CAN&veres=Y&pares=Y

        public string SuccesOrder()
        {
            string retour = "version=2" + Environment.NewLine + "cdr=0";

            #region Variable Monetico
            string TPE = "";
            string montant = "";
            string reference = "";
            string MAC = "";
            string TexteLibre = "";
            string CodeRetour = "";
            string dateConfirmation = "";
            string Cvx = "";
            string Vld = "";
            string Brand = "";
            string Status3DS = "";
            string NumAuto = "";
            string MotifRefus = "";
            string OrigineCB = "";
            string BinCB = "";
            string HPanCB = "";
            string IPClient = "";
            string OrigineTr = "";
            string VeRes = "";
            string PaRes = "";
            string MontantEch = "";
            string sResult = "";
            #endregion


            var processor = _paymentService.LoadPaymentMethodBySystemName("Payments.Monetico") as MoneticoPaymentProcessor;
            if (processor == null || !processor.IsPaymentMethodActive(_paymentSettings) || !processor.PluginDescriptor.Installed)
                throw new NopException("Monetico module cannot be loaded");


            try
            {
                if (_httpContextAccessor.HttpContext.Request.Method == "GET")
                {
                 TPE = _httpContextAccessor.HttpContext.Request.Query["TPE"].ToString(); ;
                 montant = _httpContextAccessor.HttpContext.Request.Query["montant"].ToString(); ;
                 reference = _httpContextAccessor.HttpContext.Request.Query["reference"].ToString();
                 MAC = _httpContextAccessor.HttpContext.Request.Query["MAC"].ToString();
                 TexteLibre = _httpContextAccessor.HttpContext.Request.Query["texte-libre"].ToString();
                 CodeRetour = _httpContextAccessor.HttpContext.Request.Query["code-retour"].ToString();
                 dateConfirmation = _httpContextAccessor.HttpContext.Request.Query["date"].ToString();
                 Cvx = _httpContextAccessor.HttpContext.Request.Query["cvx"].ToString();
                 Vld = _httpContextAccessor.HttpContext.Request.Query["vld"].ToString();
                 Brand = _httpContextAccessor.HttpContext.Request.Query["brand"].ToString();
                 Status3DS = _httpContextAccessor.HttpContext.Request.Query["status3ds"].ToString();
                 NumAuto = _httpContextAccessor.HttpContext.Request.Query["numauto"].ToString();
                 MotifRefus = _httpContextAccessor.HttpContext.Request.Query["motifrefus"].ToString();
                 OrigineCB = _httpContextAccessor.HttpContext.Request.Query["originecb"].ToString();
                 BinCB = _httpContextAccessor.HttpContext.Request.Query["bincb"].ToString();
                 HPanCB = _httpContextAccessor.HttpContext.Request.Query["hpancb"].ToString();
                 IPClient = _httpContextAccessor.HttpContext.Request.Query["ipclient"].ToString();
                 OrigineTr = _httpContextAccessor.HttpContext.Request.Query["originetr"].ToString();
                 VeRes = _httpContextAccessor.HttpContext.Request.Query["veres"].ToString();
                 PaRes = _httpContextAccessor.HttpContext.Request.Query["pares"].ToString();
                 MontantEch = _httpContextAccessor.HttpContext.Request.Query["montantech"].ToString();
                }
                else
                {
                    if (_httpContextAccessor.HttpContext.Request.Method == "POST")
                    {
                        TPE += Request.Form["TPE"];
                        montant += Request.Form["montant"];
                        reference += Request.Form["reference"];
                        MAC += Request.Form["MAC"];
                        TexteLibre += Request.Form["texte-libre"];
                        CodeRetour += Request.Form["code-retour"];
                        dateConfirmation += Request.Form["date"];
                        Cvx += Request.Form["cvx"];
                        Vld += Request.Form["vld"];
                        Brand += Request.Form["brand"];
                        Status3DS += Request.Form["status3ds"];
                        NumAuto += Request.Form["numauto"];
                        MotifRefus += Request.Form["motifrefus"];
                        OrigineCB += Request.Form["originecb"];
                        BinCB += Request.Form["bincb"];
                        HPanCB += Request.Form["hpancb"];
                        IPClient += Request.Form["ipclient"];
                        OrigineTr += Request.Form["originetr"];
                        VeRes += Request.Form["veres"];
                        PaRes += Request.Form["pares"];
                        MontantEch += Request.Form["montantech"];
                    }
                }

                Order order = _orderService.GetOrderById(Convert.ToInt32(reference));

                var storescope = order.StoreId;
                var moneticopaymentsettings = _settingService.LoadSetting<MoneticoPaymentSettings>(storescope);

                string hmacstring = TPE + "*" + dateConfirmation + "*" + montant + "*" + reference + "*" + TexteLibre + "*" + "3.0*" +
                CodeRetour + "*" + Cvx + "*" + Vld + "*" + Brand + "*" + Status3DS + "*" + NumAuto + "*" + MotifRefus + "*" +
                OrigineCB + "*" + BinCB + "*" + HPanCB + "*" + IPClient + "*" + OrigineTr + "*" + VeRes + "*" + PaRes + "*";

                CMCIC_Hmac hmac = new CMCIC_Hmac(moneticopaymentsettings.CLE);
                if (hmac.isValidHmac(hmacstring, MAC))
                {
                    string ordernumber = reference;
                    if (order != null)
                    {

                        var sb = new StringBuilder();
                        sb.AppendLine("Monetico response:");
                        sb.AppendLine("montant: " + montant);
                        sb.AppendLine("reference: " + reference);
                        sb.AppendLine("MAC: " + MAC);
                        sb.AppendLine("texte-libre: " + TexteLibre);
                        sb.AppendLine("code-retour: " + CodeRetour);
                        sb.AppendLine("date: " + dateConfirmation);
                        sb.AppendLine("cvx: " + Cvx);
                        sb.AppendLine("vld: " + Vld);
                        sb.AppendLine("brand: " + Brand);
                        sb.AppendLine("status3ds: " + Status3DS);
                        sb.AppendLine("numauto: " + NumAuto);
                        sb.AppendLine("motifrefus: " + CodeRetour);
                        sb.AppendLine("originecb: " + OrigineCB);
                        sb.AppendLine("bincb: " + BinCB);
                        sb.AppendLine("hpancb: " + HPanCB);
                        sb.AppendLine("ipclient: " + IPClient);
                        sb.AppendLine("originetr: " + OrigineTr);
                        sb.AppendLine("veres: " + VeRes);
                        sb.AppendLine("pares: " + PaRes);
                        sb.AppendLine("montantech: " + MontantEch);

                        //order note
                        order.OrderNotes.Add(new OrderNote
                        {
                            Note = sb.ToString(),
                            DisplayToCustomer = false,
                            CreatedOnUtc = DateTime.UtcNow
                        });
                        _orderService.UpdateOrder(order);

                        decimal decMontant = 0;
                        try
                        {
                            decMontant = Convert.ToDecimal(montant.Replace("CAD", "").Replace(".", ","));
                        }
                        catch
                        {
                            decMontant = Convert.ToDecimal(montant.Replace("CAD", "").Replace(",", "."));
                        }

                        //validate order total
                        if (!Math.Round(decMontant, 2).Equals(Math.Round(order.OrderTotal, 2)))
                        {
                            string errorStr = string.Format("Monetico response. Returned order total {0} doesn't equal order total {1}", decMontant, order.OrderTotal);
                            _logger.Error(errorStr);

                            //return RedirectToAction("Index", "Home", new { area = "" });
                        }

                        switch (CodeRetour)
                        {

                            case "Annulation":
                                /* 
                                * Payment has been refused
                                * Attention : an authorization may still be delivered later
                                */
                                break;

                            case "payetest":
                                /*
                                * Payment has been accepted on the test server
                                * put your code here (email sending / Database update)
                                */
                                break;
                            case "paiement":
                                //mark order as paid
                                if (_orderProcessingService.CanMarkOrderAsPaid(order))
                                {
                                    order.AuthorizationTransactionId = NumAuto;
                                    _orderService.UpdateOrder(order);

                                    _orderProcessingService.MarkOrderAsPaid(order);
                                }

                                //return RedirectToRoute("CheckoutCompleted", new { orderId = order.Id });

                                break;

                            /*** ONLY FOR MULTIPART PAYMENT ***/
                            case "paiement_pf2":
                            case "paiement_pf3":
                            case "paiement_pf4":
                                /*
                                * Payment has been accepted on the productive server for the part #N
                                * return code is like paiement_pf[#N]
                                * put your code here (email sending / Database update)
                                * You have the amount of the payment part in MontantEch
                                */
                                break;

                            case "Annulation_pf2":
                            case "Annulation_pf3":
                            case "Annulation_pf4":
                                /*
                                * Payment has been refused on the productive server for the part #N
                                * return code is like Annulation_pf[#N]
                                * put your code here (email sending / Database update)
                                * You have the amount of the payment part in MontantEch
                                */
                                break;
                        }
                    }

                }
                else
                {

                    

                    retour = "version=2" + Environment.NewLine + "cdr=1";

                    retour += "\n" + hmacstring;

                    //code hmac ne correspond pas
                    string orderNumber = reference;
                    Guid orderNumberGuid = Guid.Empty;
                    try
                    {
                        orderNumberGuid = new Guid(orderNumber);
                    }
                    catch { }
                    //Order order = _orderService.GetOrderByGuid(orderNumberGuid);
                    if (order != null)
                    {
                        //order note
                        order.OrderNotes.Add(new OrderNote
                        {
                            Note = "Monetico HMAC validation failed. " + hmacstring,
                            DisplayToCustomer = false,
                            CreatedOnUtc = DateTime.UtcNow
                        });
                        _orderService.UpdateOrder(order);
                    }
                    //return RedirectToAction("Index", "Home", new { area = "" });
                }
            }
            catch (Exception ex)
            {
                retour = "version=2" + Environment.NewLine + "cdr=1";
                LogException(ex);
            }

            return retour;
        }

        public IActionResult ConfirmOrder()
        {
            string orderNo = "";
            try
            {
                orderNo = _httpContextAccessor.HttpContext.Request.Query["order_ref"].ToString();
                Order order = _orderService.GetOrderById(Convert.ToInt32(orderNo));
                return RedirectToRoute("CheckoutCompleted", new { orderId = orderNo, vendorid = order.StoreId });

                ////return RedirectToRoute("CheckoutCompleted", new { orderId = orderNo, vendorid= _storeContext.CurrentStore.Id });
            }
            catch (Exception ex) { }



            //ne devrait pas se rendre ici
            return RedirectToAction("Index", "Home", new { area = "" });
        }


        public IActionResult CancelOrder()
        {
            string orderNo = "";
            try
            {
                orderNo = _httpContextAccessor.HttpContext.Request.Query["order_ref"].ToString();
            }
            catch (Exception ex) { }

            //LogException(new Exception("CancelOrder, order: " + orderNo));

            /*var order = _orderService.SearchOrders(storeId: _storeContext.CurrentStore.Id,
                customerId: _workContext.CurrentCustomer.Id, pageSize: 1)
                .FirstOrDefault();*/

            var order = _orderService.GetOrderById(Convert.ToInt32(orderNo));

            if (order != null)
            {
                _orderProcessingService.CancelOrder(order, false);
                return RedirectToRoute("OrderDetails", new { orderId = orderNo });
            }

            //ne devrait pas se rendre ici
            return RedirectToAction("Index", "Home", new { area = "" });
        }
    }
}
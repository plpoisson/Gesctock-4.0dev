using Nop.Web.Framework.Mvc.ModelBinding;
using Nop.Web.Framework.Mvc.Models;

namespace Nop.Plugin.Payments.Monetico.Models
{
    public class ConfigurationModel : BaseNopModel
    {
        public int ActiveStoreScopeConfiguration { get; set; }

        [NopResourceDisplayName("Plugins.Payments.Monetico.Fields.UseSandbox")]
        public bool UseSandbox { get; set; }
        public bool UseSandbox_OverrideForStore { get; set; }

        [NopResourceDisplayName("Plugins.Payments.Monetico.Fields.TPE")]
        public string TPE { get; set; }
        public bool TPE_OverrideForStore { get; set; }

        [NopResourceDisplayName("Plugins.Payments.Monetico.Fields.CodeSociete")]
        public string CodeSociete { get; set; }
        public bool CodeSociete_OverrideForStore { get; set; }

        [NopResourceDisplayName("Plugins.Payments.Monetico.Fields.CLE")]
        public string CLE { get; set; }
        public bool CLE_OverrideForStore { get; set; }

        [NopResourceDisplayName("Plugins.Payments.Monetico.Fields.Actif")]
        public bool Actif { get; set; }
        public bool Actif_OverrideForStore { get; set; }
    }
}
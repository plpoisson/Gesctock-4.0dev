using Nop.Core.Configuration;


namespace Nop.Plugin.Payments.Monetico
{
    public class MoneticoPaymentSettings : ISettings
    {
        public bool UseSandbox { get; set; }
        public string TPE { get; set; } // "0000001";
        public string CodeSociete { get; set; } // "neomediate";
        public string CLE { get; set; } // "12345678901234567890123456789012345678P0";
        public bool Actif
        {
            get; set;

        }
    }
}

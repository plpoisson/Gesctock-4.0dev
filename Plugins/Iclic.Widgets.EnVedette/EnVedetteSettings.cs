using Nop.Core.Configuration;

namespace Iclic.Widgets.EnVedette
{
    public class EnVedetteSettings: ISettings
    {
        public bool ProduitVedettes { get; set; }
        public int ProduitVedettesNb { get; set; }

        public bool ProduitNouveautes { get; set; }
        public int ProduitNouveautesNb { get; set; }

        public bool ProduitVentes { get; set; }
        public int ProduitVentesNb { get; set; }

        public bool ProduitsDecouverte { get; set; }
        public int ProduitsDecouverteNb { get; set; }

        public bool ProduitsCategorie { get; set; }
        public int ProduitsCategorieNb { get; set; }
        public int CategoryId { get; set; }

        public bool ProduitsSolde { get; set; }
        public int ProduitsSoldeNb { get; set; }
        public int ProduitsSoldePourcentage { get; set; }

    }
}

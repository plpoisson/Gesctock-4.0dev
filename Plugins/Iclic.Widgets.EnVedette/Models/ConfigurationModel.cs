using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Web.Framework.Mvc.ModelBinding;
using Nop.Web.Framework.Mvc.Models;
namespace Iclic.Widgets.EnVedette.Models
{
    public class ConfigurationModel : BaseNopModel
    {
        public ConfigurationModel()
        {
            AvailableCategories = new List<SelectListItem>();
        }


        public int ActiveStoreScopeConfiguration { get; set; }

        [NopResourceDisplayName("Widgets.EnVedette.Name")]
        public bool ProduitVedettes { get; set; }
        public bool ProduitVedettes_OverrideForStore { get; set; }

        [NopResourceDisplayName("Widgets.EnVedette.Nb")]
        public int ProduitVedettesNb { get; set; }
        public bool ProduitVedettesNb_OverrideForStore { get; set; }

        [NopResourceDisplayName("Widgets.EnVedette.Name")]
        public bool ProduitNouveautes { get; set; }
        public bool ProduitNouveautes_OverrideForStore { get; set; }

        [NopResourceDisplayName("Widgets.EnVedette.Nb")]
        public int ProduitNouveautesNb { get; set; }
        public bool ProduitNouveautesNb_OverrideForStore { get; set; }

        [NopResourceDisplayName("Widgets.EnVedette.Name")]
        public bool ProduitVentes { get; set; }
        public bool ProduitVentes_OverrideForStore { get; set; }

        [NopResourceDisplayName("Widgets.EnVedette.Nb")]
        public int ProduitVentesNb { get; set; }
        public bool ProduitVentesNb_OverrideForStore { get; set; }

        [NopResourceDisplayName("Widgets.EnVedette.Name")]
        public bool ProduitsDecouverte { get; set; }
        public bool ProduitsDecouverte_OverrideForStore { get; set; }

        [NopResourceDisplayName("Widgets.EnVedette.Nb")]
        public int ProduitsDecouverteNb { get; set; }
        public bool ProduitsDecouverteNb_OverrideForStore { get; set; }

        [NopResourceDisplayName("Widgets.Categorie.Name")]
        public bool ProduitsCategorie { get; set; }
        public bool ProduitsCategorie_OverrideForStore { get; set; }

        [NopResourceDisplayName("Widgets.Categorie.Nb")]
        public int ProduitsCategorieNb { get; set; }
        public bool ProduitsCategorieNb_OverrideForStore { get; set; }

        [NopResourceDisplayName("Widgets.Categorie.CategoryId")]
        public int CategoryId { get; set; }
        public bool CategoryId_OverrideForStore { get; set; }

        public IList<SelectListItem> AvailableCategories { get; set; }

        [NopResourceDisplayName("Widgets.Solde.Name")]
        public bool ProduitsSolde { get; set; }
        public bool ProduitsSolde_OverrideForStore { get; set; }

        [NopResourceDisplayName("Widgets.Solde.Nb")]
        public int ProduitsSoldeNb { get; set; }
        public bool ProduitsSoldeNb_OverrideForStore { get; set; }

        [NopResourceDisplayName("Plugins.Widgets.Solde.Pourcentage")]
        public int ProduitsSoldePourcentage { get; set; }
        public bool ProduitsSoldePourcentage_OverrideForStore { get; set; }
    }
}
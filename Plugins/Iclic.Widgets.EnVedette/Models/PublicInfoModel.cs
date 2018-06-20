using Nop.Web.Framework.Mvc.Models;
using System.Collections.Generic;
using Nop.Web.Models.Catalog;

namespace Iclic.Widgets.EnVedette.Models
{
    public class PublicInfoModel : BaseNopModel
    {
        public PublicInfoModel()
        {
            EnVedette = new List<ProductOverviewModel>();
            Nouveaute = new List<ProductOverviewModel>();
            MeilleurVentes = new List<ProductOverviewModel>();
            Decouverte = new List<ProductOverviewModel>();
            Categorie = new List<ProductOverviewModel>();
            Solde = new List<ProductOverviewModel>();
        }
        public IList<ProductOverviewModel> EnVedette { get; set; }
        public IList<ProductOverviewModel> Nouveaute { get; set; }
        public IList<ProductOverviewModel> MeilleurVentes { get; set; }
        public IList<ProductOverviewModel> Decouverte { get; set; }
        public IList<ProductOverviewModel> Categorie { get; set; }
        public IList<ProductOverviewModel> Solde { get; set; }
        public bool IsMain { get; set; }
    }
}
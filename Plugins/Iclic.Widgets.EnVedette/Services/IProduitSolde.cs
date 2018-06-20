using System.Collections.Generic;
using Nop.Core.Domain.Catalog;

namespace Iclic.Widgets.EnVedette.Services
{
    public partial interface IProduitSolde
    {
        IList<Product> GetAllProductsSolde(int storeId, int percentage);
    }
}

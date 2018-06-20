using System;
using System.Collections.Generic;
using System.Linq;
using Nop.Core.Data;
using Nop.Core.Domain.Catalog;


namespace Iclic.Widgets.EnVedette.Services
{
    public partial class ProduitSolde : IProduitSolde
    {
        #region Constants
        /// <summary>
        /// Key for caching
        /// </summary>
        /// <remarks>
        /// {0} : product ID
        /// </remarks>
        private const string PRODUCTS_BY_ID_KEY = "Nop.Product.id-{0}";
        /// <summary>
        /// Key pattern to clear cache
        /// </summary>
        private const string PRODUCTS_PATTERN_KEY = "Nop.Product.";
        #endregion

        #region Fields

        private readonly IRepository<Product> _productRepository;
        private readonly IRepository<TierPrice> _tierPriceRepository;
       

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="productRepository">Product repository</param>

        public ProduitSolde(
            IRepository<Product> productRepository, IRepository<TierPrice> tierPriceRepository)
        {
            
            this._productRepository = productRepository;
            this._tierPriceRepository = tierPriceRepository;

        }

        #endregion

        public virtual IList<Product> GetAllProductsSolde(int storeId, int percentage)
        {

            var query = from p in this._productRepository.Table

                        join tp in _tierPriceRepository.Table on p.Id equals tp.ProductId  

                        where tp.CustomerRoleId == null &&
                     (tp.EndDateTimeUtc > DateTime.UtcNow || tp.EndDateTimeUtc == null) &&
                     (tp.StartDateTimeUtc < DateTime.UtcNow || tp.StartDateTimeUtc == null) && ((100 - ((tp.Price * 100) / p.Price)) > percentage)
                        select p;

            var products = query.ToList();
            return products;
        }
    }
}

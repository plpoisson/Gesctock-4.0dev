using Nop.Core.Data;
using Nop.Core.Domain.Catalog;
using Nop.Services.Catalog;
using Nop.Services.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Iclic.Widgets.EnVedette.Services
{
    /// <summary>
    /// Task which found all the products and associate them with the right category : Liquidation, Solde, or neither of them
    /// </summary>
    class ProductSoldeTask : IScheduleTask
    {

        #region Fields


        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private List<TierPrice> tierPrices;
        private List<Product> productsList;
        private readonly IRepository<Product> _productRepository;
        private readonly IRepository<Category> _categoryRepository;
        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="productRepository">Product repository</param>

        public ProductSoldeTask(
           IProductService productService, ICategoryService categoryService, IRepository<Product> productRepository, IRepository<Category> categoryRepository)
        {

            this._productService = productService;
            this._categoryService = categoryService;
            this._categoryRepository = categoryRepository;
            this._productRepository = productRepository;
        }
        #endregion

        #region Utilities 

        /// <summary>
        /// Found all the products in the db
        /// </summary>
        /// <returns>List of all products</returns>
        private List<Product> GetAllProduct()
        {
            var query = from p in _productRepository.Table
                        select p;
            var products = query.ToList();
            return products;
        }

        /// <summary>
        /// Found the id of the Solde category
        /// </summary>
        /// <returns>The id </returns>
        private int GetCategorySoldeId()
        {
            var query = from c in _categoryRepository.Table
                        where c.Name == "Soldes"
                        select c;

                foreach (var cat in query)
                {
                    return cat.Id;
                }

                throw new Exception("No Solde category found (Be sure to add a category named : Solde)");

        }

        /// <summary>
        /// Found the id of the Liquidation category
        /// </summary>
        /// <returns>The id </returns>
        private int GetCategoryLiquidationId()
        {
            var query = from c in _categoryRepository.Table
                        where c.Name == "Liquidation"
                        select c;

            foreach (var cat in query)
            {
                return cat.Id;
            }

            throw new Exception("No Liquidation category found (Be sure to add a category named : Liquidation)");

        }

        #endregion
        public void Execute()
        {

            int soldeID = GetCategorySoldeId();
            int liquidationID= GetCategoryLiquidationId();
            productsList = GetAllProduct();


            foreach (var product in productsList)
            {

                var soldCategorie = _categoryService.GetProductCategoriesByCategoryId(soldeID, showHidden: true).FindProductCategory(product.Id, soldeID);
                var liquidationCategorie = _categoryService.GetProductCategoriesByCategoryId(liquidationID, showHidden: true).FindProductCategory(product.Id, liquidationID);

                tierPrices = product.TierPrices.ToList();


                foreach (var tierPrice in tierPrices)
                {

                    if (product.Price > tierPrice.Price && product.HasTierPrices
                        && (tierPrice.EndDateTimeUtc > DateTime.UtcNow || tierPrice.EndDateTimeUtc == null)
                        && (tierPrice.StartDateTimeUtc < DateTime.UtcNow || tierPrice.StartDateTimeUtc == null) && tierPrice.Quantity == 1)
                    {

                        if ((100 - ((tierPrice.Price * 100) / product.Price)) < 50)
                        {
                            if (soldCategorie == null)
                            {
                                var productCategory = new ProductCategory
                                {
                                    ProductId = product.Id,
                                    CategoryId = soldeID,
                                    DisplayOrder = 0
                                };
                                _categoryService.InsertProductCategory(productCategory);
                            }
                            if (liquidationCategorie != null)
                            {
                                _categoryService.DeleteProductCategory(liquidationCategorie);
                            }

                        }
                        else
                        {
                            if (liquidationCategorie == null)
                            {
                                var productCategory = new ProductCategory
                                {
                                    ProductId = product.Id,
                                    CategoryId = liquidationID,
                                    DisplayOrder = 0
                                };
                                _categoryService.InsertProductCategory(productCategory);
                            }
                            if (soldCategorie != null)
                            {
                                _categoryService.DeleteProductCategory(soldCategorie);
                            }

                        }

                    }
                    else
                    {
                        if (soldCategorie != null)
                        {
                            _categoryService.DeleteProductCategory(soldCategorie);
                        }
                        if (liquidationCategorie != null)
                        {
                            _categoryService.DeleteProductCategory(liquidationCategorie);
                        }

                    }
                }

            }
        }
    }
}

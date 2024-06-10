using Microsoft.EntityFrameworkCore;
using Shop2City.DataLayer.Context;
using Shop2City.DataLayer.Entities.Products;

namespace Shop2City.Core.Services.ProductImages
{
    public class ProductImageService : IProductImageService
    {
        private readonly Shop2CityContext _context;

        public ProductImageService(Shop2CityContext context)
        {
            _context = context;
        }

        public List<ProductGallery> ListProductImageByProductId(int productId)
        {
            var GetlistImage = _context.ProductGalleries
                .Where(a => a.ProductId == productId)
                .ToList();
            return GetlistImage;
        }

        public void AddProductImage(ProductGallery productImage)
        {
            _context.ProductGalleries.Add(productImage);
            _context.SaveChanges();
        }
    }
}

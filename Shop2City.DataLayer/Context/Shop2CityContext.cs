using Microsoft.EntityFrameworkCore;
using Shop2City.DataLayer.Entities;
using Shop2City.DataLayer.Entities.Advices;
using Shop2City.DataLayer.Entities.Calculations;
using Shop2City.DataLayer.Entities.DisCounts;
using Shop2City.DataLayer.Entities.Ordering;
using Shop2City.DataLayer.Entities.Orders;
using Shop2City.DataLayer.Entities.Permissions;
using Shop2City.DataLayer.Entities.Products;
using Shop2City.DataLayer.Entities.Properties;
using Shop2City.DataLayer.Entities.Settings;
using Shop2City.DataLayer.Entities.ShoppingCartItems;
using Shop2City.DataLayer.Entities.ShoppingCarts;
using Shop2City.DataLayer.Entities.Slideshows;
using Shop2City.DataLayer.Entities.Transactions;
using Shop2City.DataLayer.Entities.Users;
using ShoppingCart = Shop2City.DataLayer.Entities.ShoppingCarts.ShoppingCart;

namespace Shop2City.DataLayer.Context
{
    public class Shop2CityContext : DbContext
    {
        public Shop2CityContext(DbContextOptions<Shop2CityContext> options) : base(options)
        {

        }

        public virtual DbSet<CalculationModel> Calculations { get; set; }

        #region Users Table
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserRole> UserRoles { get; set; }
        public virtual DbSet<LoginHistory> LoginHistories { get; set; }
        public virtual DbSet<UserProduct> UserProducts { get; set; }
        public virtual DbSet<UserDiscountCode> UserDiscountCodes { get; set; }

        public virtual DbSet<DisCount> DisCounts { get; set; }

        public virtual DbSet<UserLevel> UserLevels { get; set; }
        #endregion

        #region Products Tables
        public virtual DbSet<ProductGroup> ProductGroups { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductGallery> ProductGalleries { get; set; }

        public virtual DbSet<CategorySelectedCalculationModel> CategorySelectedCalculations { get; set; }
        #endregion

        #region Orders
        public virtual DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }
        public virtual DbSet<Item> Items { get; set; }
        public virtual DbSet<Factor> Factors { get; set; }
        public virtual DbSet<FactorDetail> FactorDetails { get; set; }
        public virtual DbSet<TypePost> TypePosts { get; set; }

        public virtual DbSet<OrderModel> Orders { get; set; }
        public virtual DbSet<OrderDetailModel> OrderDetails { get; set; }
        public virtual DbSet<CalculationDetailModel> CalculationDetails { get; set; }
        #endregion

        #region Permissions
        public virtual DbSet<Permission> Permissions { get; set; }
        public virtual DbSet<RolePermission> RolePermissions { get; set; }
        #endregion

        #region Properties
        public virtual DbSet<Property> Properties { get; set; }
        public virtual DbSet<ProductProperty> ProductProperties { get; set; }
        public virtual DbSet<PropertyTechnicalProduct> PropertyTechnicalProducts { get; set; }
        #endregion

        public virtual DbSet<SlideShow> SlideShows { get; set; }
 
        #region Settings
        public virtual DbSet<Setting> Setting { get; set; }
        #endregion
        public virtual DbSet<PropertyTitle> PropertyTitles { get; set; }
        public virtual DbSet<PropertyTechnical> propertyTechnicals { get; set; }

        public virtual DbSet<Sms> Sms { get; set; }

        public virtual DbSet<WageModel> Wages { get; set; }

        public virtual DbSet<AdviceModel> Advices { get; set; }

        public virtual DbSet<TransactionModel> Transactions { get; set; }

        #region shoppingCart
        public virtual DbSet<ShoppingCart> ShoppingCarts { get; set; }


        #endregion

        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var cascadeFKs = modelBuilder.Model.GetEntityTypes()
                .SelectMany(t => t.GetForeignKeys())
                .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);

            foreach (var fk in cascadeFKs)
                fk.DeleteBehavior = DeleteBehavior.Restrict;

            modelBuilder.Entity<ProductGroup>()
                .HasQueryFilter(u => !u.IsDelete);

            modelBuilder.Entity<Product>()
                .HasQueryFilter(u => !u.IsDelete);

            modelBuilder.Entity<ProductGallery>()
                .HasQueryFilter(u => !u.isDelete);



            base.OnModelCreating(modelBuilder);
        }
    }
}

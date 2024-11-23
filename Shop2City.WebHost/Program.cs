using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Shop2City.Core.Convertors;
using Shop2City.Core.Services;
using Shop2City.Core.Services.Calculations;
using Shop2City.Core.Services.DisCounts;
using Shop2City.Core.Services.Factors;
using Shop2City.Core.Services.Orders;
using Shop2City.Core.Services.Permissions;
using Shop2City.Core.Services.ProductImages;
using Shop2City.Core.Services.Products;
using Shop2City.Core.Services.Properties;
using Shop2City.Core.Services.PropertyTechnicalProducts;
using Shop2City.Core.Services.PropertyTechnicals;
using Shop2City.Core.Services.PropertyTitles;
using Shop2City.Core.Services.ShoppingCart;
using Shop2City.Core.Services.SlideShows;
using Shop2City.Core.Services.Transactions;
using Shop2City.Core.Services.UserPanel;
using Shop2City.Core.Services.Users;
using Shop2City.Core.Services.Wages;
using Shop2City.DataLayer.Context;



var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();


//services.AddWebMarkupMin(options =>
//    {
//        options.AllowMinificationInDevelopmentEnvironment = true;
//        options.AllowCompressionInDevelopmentEnvironment = true;
//    })
//    .AddHtmlMinification()
//    .AddHttpCompression()
//    .AddXmlMinification();
var configuration = builder.Configuration;



builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(10);//10 دقیقه 
    options.Cookie.IsEssential = true;
});

builder.Services.AddHttpClient();
#region Authentication
builder.Services.AddAuthentication(options => {
    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultSignOutScheme = CookieAuthenticationDefaults.AuthenticationScheme;
})
.AddCookie(options => {
    options.LoginPath = "/Login";
    options.ExpireTimeSpan = TimeSpan.FromDays(30); // Persist the cookie for 30 days
    options.SlidingExpiration = true; // Allow sliding expiration, which refreshes the cookie expiration time when the user interacts
    options.LogoutPath = "/Logout";

    options.Cookie.HttpOnly = true;
    options.AccessDeniedPath = "/Account/AccessDenied";
    options.Cookie.SameSite = SameSiteMode.None;
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
    options.Cookie.IsEssential = true; // Ensures cookie is sent even when not strictly necessary
});

#endregion
#region DataBase Context 
builder.Services.AddDbContext<Shop2CityContext>(options =>
{
    options.UseSqlServer(configuration.GetConnectionString("Shop2CitypcConnection") ?? throw new InvalidOperationException());
});
#endregion
#region IOC
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IUserPanelService, UserPanelService>();
builder.Services.AddTransient<IViewRenderService, RenderViewToString>();
builder.Services.AddTransient<IPermissionService, PermissionService>();
builder.Services.AddTransient<IProductService, ProductService>();
builder.Services.AddTransient<ISlideShowService, SlideShowService>();
builder.Services.AddTransient<IOrderService, OrderService>();
builder.Services.AddTransient<IFactorService, FactorService>();
builder.Services.AddTransient<IShoppingCartService, ShoppingCartService>();
builder.Services.AddTransient<IProductImageService, ProductImageService>();
builder.Services.AddTransient<IPropertyTechnicalProductService, PropertyTechnicalProductService>();
builder.Services.AddTransient<IPropertyService, PropertyService>();
builder.Services.AddTransient<IPropertyTitleService, PropertyTitleService>();
builder.Services.AddTransient<IPropertyTechnicalService, PropertyTechnicalService>();
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<ICalculationService, CalculationService>();
builder.Services.AddScoped<IWageService, WageService>();
builder.Services.AddScoped<ITransactionService, TransactionService>();
builder.Services.AddScoped<IDisCountService, DisCountService>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
#endregion

builder.Services.AddCors(options =>
{

    //ی رمز سخت بزار
    options.AddPolicy("EnableCors", builder =>
    {
        builder.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod()
            .SetIsOriginAllowed(origin => true);
    });
});


// Add services to the container.
builder.Services.AddRazorPages();
var app = builder.Build();



if (app.Environment.IsProduction())
{
    app.UseDeveloperExceptionPage();
   // app.UseExceptionHandler("/Error");
}
else
{
    app.UseDeveloperExceptionPage();
}



app.UseCors(policy => policy
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());


app.UseSession();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();


//app.UseMiddleware<ApplicationVariable>();
app.MapRazorPages();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "MyAreas",
        pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
   
    endpoints.MapControllerRoute(
        name: "Default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
});
app.Run(async (context) =>
{
    await context.Response.WriteAsync("Hello World");
});


app.Run();

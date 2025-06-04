using User.Intfs;
using Infrastructure;
using Infrastructure.Repositories;
using Infrastructure.IRepositoiries;
using Application.Share;
using User.Imlps;
using Address.Impls;
using Address.Intfs;
using Admin.Intfs;
using Admin.Imlps;
using Brand.Intfs;
using Brand.Impls;
using Category.Intfs;
using Category.Impls;
using Product.Intfs;
using Wish_list.Intfs;
using Wish_list.Impls;
using Product.Impls;
using Contact.Intfs;
using Contact.Impls;
using Menu.Intfs;
using Menu.Impls;
using Order.Intfs;
using Order.Impls;
using Cart.Intfs;
using Cart.Impls;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Dashboard.Intfs;
using Dashboard.Impls;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//Read ConnectionString
builder.Services.AddSingleton<DataContext>(provider => new DataContext(builder.Configuration.GetConnectionString("DefaultConnection")));

//Dependence Injection
builder.Services.AddSingleton<IGetListData, GetListData>();
builder.Services.AddSingleton<ICreateData, CreateData>();
builder.Services.AddSingleton<IUpdateData, UpdateData>();
builder.Services.AddSingleton<IDeleteData, DeleteData>();
builder.Services.AddSingleton<ILogin, Login>();
builder.Services.AddSingleton<IFunction, Function>();
builder.Services.AddSingleton<IUserService, UserService>();
builder.Services.AddSingleton<IAddressService, AddressService>();
builder.Services.AddSingleton<IGetData, GetData>();
builder.Services.AddSingleton<IAdminService, AdminService>();
builder.Services.AddSingleton<IBrandService, BrandService>();
builder.Services.AddSingleton<ICategoryService, CategoryService>();
builder.Services.AddSingleton<IProductService, ProductService>();
builder.Services.AddSingleton<IWishlistService, WishlistService>();
builder.Services.AddSingleton<IDiscountService, DiscountService>();
builder.Services.AddSingleton<IContactService, ContactService>();
builder.Services.AddSingleton<IMenuService, MenuService>();
builder.Services.AddSingleton<IStockService, StockService>();
builder.Services.AddSingleton<IOrderService, OrderService>();
builder.Services.AddSingleton<ICartService, CartService>();
builder.Services.AddSingleton<IDashboardService, DashboardService>();
builder.Services.AddSingleton<WebSocketConnectionManager>();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddHttpContextAccessor();

var ipDevice = builder.Configuration["IP:Device"];
var ipDefault = builder.Configuration["IP:Default"];
var ipLocal = builder.Configuration["IP:Local"];

builder.WebHost.UseUrls($"{ipDevice};{ipDefault};{ipLocal}");
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder =>
        {
            builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
        });
});

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
})
.AddCookie()
.AddGoogle(options =>
{
    options.ClientId = builder.Configuration["Authentication:Google:ClientId"];
    options.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"];
    options.CallbackPath = builder.Configuration["Authentication:Google:CallbackPath"];
});

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
        options.RoutePrefix = string.Empty;
    });
    app.UseSwaggerUI();
}
app.UseStaticFiles();
app.UseSession();
//app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.UseAuthorization();
app.UseWebSockets(new WebSocketOptions
{
    KeepAliveInterval = TimeSpan.FromMinutes(2)
});
app.MapControllers();

app.Run();

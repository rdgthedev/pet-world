using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PetWorldOficial.Application.Commands.Service;
using PetWorldOficial.Application.Services.Implementations;
using PetWorldOficial.Application.Services.Interfaces;
using PetWorldOficial.Domain.Entities;
using PetWorldOficial.Domain.Interfaces.Repositories;
using PetWorldOficial.Infrastructure.Persistence.Repositories;
using PetWorldOficial.Infrastructure.Services;
using Newtonsoft.Json;
using PetWorldOficial.Application.Settings;
using PetWorldOficial.Infrastructure.Data.Context;
using AuthService = PetWorldOficial.Infrastructure.Services.AuthService;
using UserService = PetWorldOficial.Application.Services.Implementations.UserService;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<Settings>(builder.Configuration.GetSection("Settings"));
builder.Services.AddHttpContextAccessor();

builder.Services.AddControllersWithViews()
    .AddNewtonsoftJson(options => { options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore; })
    .ConfigureApiBehaviorOptions(options => { options.SuppressModelStateInvalidFilter = true; });

builder.Services.AddHttpClient();
builder.Services.AddMemoryCache();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder => builder.WithOrigins("http://example.com")
            .AllowAnyMethod()
            .AllowAnyHeader());
});

builder.Services.Configure<OpeningHours>(builder.Configuration.GetSection("OpeningHours"));

builder.Services.AddDbContext<AppDbContext>(
    options =>
    {
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
            cfg =>
            {
                cfg.CommandTimeout(120);
                cfg.MigrationsAssembly("PetWorldOficial.Infrastructure");
            });
    });

builder.Services.AddIdentity<User, Role>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        // Tempo de expiração do cookie de autenticação
        options.ExpireTimeSpan = TimeSpan.FromMinutes(8); // Defina o tempo conforme necessário
        options.SlidingExpiration = true;

        // Evento acionado quando o usuário faz logout
        options.Events.OnSigningOut = async context =>
        {
            // Verifica se o usuário está autenticado, se estiver, remove o CartId
            if ((bool)context.HttpContext.User.Identity?.IsAuthenticated)
            {
                context.HttpContext.Response.Cookies.Delete("CartId");
            }
            await Task.CompletedTask;
        };
    });

builder.Services.ConfigureApplicationCookie(option =>
{
    option.ExpireTimeSpan = TimeSpan.FromHours(8);
    option.Cookie.HttpOnly = true;
    option.LoginPath = "/Auth/Login";
    option.AccessDeniedPath = "/Auth/Login";
});


#region Services

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IServiceService, ServiceService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ISupplierService, SupplierService>();
builder.Services.AddScoped<IAnimalService, AnimalService>();
builder.Services.AddScoped<IScheduleService, ScheduleService>();
// builder.Services.AddScoped<IRaceService, RaceService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IStockService, StockService>();
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddScoped<ICartItemService, CartItemService>();
builder.Services.AddScoped<ICartCookieService, CartCookieService>();
builder.Services.AddTransient<IImageService, ImageService>();

#endregion

#region Repositories

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ISupplierRepository, SupplierRepository>();
builder.Services.AddScoped<IServiceRepository, ServiceRepository>();
builder.Services.AddScoped<IAnimalRepository, AnimalRepository>();
builder.Services.AddScoped<IScheduleRepository, ScheduleRepository>();
// builder.Services.AddScoped<IRaceRepository, RaceRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IStockRepository, StockRepository>();
builder.Services.AddScoped<ICartRepository, CartRepository>();
builder.Services.AddScoped<ICartItemRepository, CartItemRepository>();

#endregion


builder.Services.AddAutoMapper(typeof(CreateServiceCommand));
builder.Services.AddMediatR(m => m.RegisterServicesFromAssembly(typeof(CreateServiceCommand).Assembly));


var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseCors();
// app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
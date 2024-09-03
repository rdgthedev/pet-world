using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PetWorldOficial.Application.Commands.Animal;
using PetWorldOficial.Application.Mappers;
using PetWorldOficial.Application.Mappers.Product;
using PetWorldOficial.Application.Services.Implementations;
using PetWorldOficial.Application.Services.Interfaces;
using PetWorldOficial.Domain.Entities;
using PetWorldOficial.Domain.Interfaces.Repositories;
using PetWorldOficial.Infrastructure.Context;
using PetWorldOficial.Infrastructure.Persistence.Repositories;
using PetWorldOficial.Infrastructure.Services;
using AuthService = PetWorldOficial.Infrastructure.Services.AuthService;
using UserService = PetWorldOficial.Application.Services.Implementations.UserService;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews()
    .ConfigureApiBehaviorOptions(options => { options.SuppressModelStateInvalidFilter = true; });

builder.Services.AddDbContext<AppDbContext>(
    options => { options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")); });

builder.Services.AddIdentity<User, Role>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(option =>
{
    option.ExpireTimeSpan = TimeSpan.FromHours(8);
    option.LoginPath = "/Auth/Login";
    option.AccessDeniedPath = "/Auth/Login";
});

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IServiceService, ServiceService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IAnimalService, AnimalService>();
builder.Services.AddScoped<IScheduleService, ScheduleService>();
builder.Services.AddTransient<IImageService, ImageService>();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ISupplierRepository, SupplierRepository>();
builder.Services.AddScoped<IServiceRepository, ServiceRepository>();
builder.Services.AddScoped<IAnimalRepository, AnimalRepository>();
builder.Services.AddScoped<IScheduleRepository, ScheduleRepository>();

builder.Services.AddAutoMapper(typeof(UpdateProductDTOToProduct));
builder.Services.AddMediatR(m => m.RegisterServicesFromAssembly(typeof(UpdateAnimalCommand).Assembly));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
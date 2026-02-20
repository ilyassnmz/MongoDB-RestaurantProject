using AkademiQMongoDb.Services.AdminServices;
using AkademiQMongoDb.Services.CategoryServices;
using AkademiQMongoDb.Services.ProductServices;
using AkademiQMongoDb.Services.EmailServices;
using AkademiQMongoDb.Services.SubscriberServices;
using AkademiQMongoDb.Settings;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.Configure<DatabaseSettings>(builder.Configuration.GetSection(nameof(DatabaseSettings)));

// EmailSettings'i yapýlandýr
builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));

//Service Registration
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IAdminService, AdminService>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<ISubscriberService, SubscriberService>();

builder.Services.AddSingleton<IDatabaseSettings>(sp =>
{
    return sp.GetRequiredService<IOptions<DatabaseSettings>>().Value;
});

builder.Services.AddControllersWithViews(options =>
{
    options.Filters.Add(new AuthorizeFilter());
});

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(config =>
                {
                    config.LoginPath = "/Login/Index";
                    config.LogoutPath = "/Login/Logout";
                    config.Cookie.Name = "FooduAppCookie";
                    config.ExpireTimeSpan = TimeSpan.FromMinutes(30);
                    config.SlidingExpiration = true;
                });

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

app.UseSession();

app.MapControllerRoute(
  name: "areas",
  pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
);


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
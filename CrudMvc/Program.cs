using CrudMvc.Data;
using CrudMvc.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Org.BouncyCastle.Tsp;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<CrudMvcContext>(options =>
    options.UseMySQL(builder.Configuration.GetConnectionString("CrudMvcContext") ?? throw new InvalidOperationException("Connection string 'CrudMvcContext' not found.")));

builder.Services.AddScoped<SeedingService>();
builder.Services.AddScoped<SellerService>();
builder.Services.AddScoped<DepartamentService>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var seeding = scope.ServiceProvider.GetRequiredService<SeedingService>();
    seeding.Seed();
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();
app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


//adicionando variação de cultura- GLOBAL = USA
var USA = new System.Globalization.CultureInfo("en-Us");
var Locale = new RequestLocalizationOptions()
{
    DefaultRequestCulture = new Microsoft.AspNetCore.Localization.RequestCulture(USA),
    SupportedCultures = new List<System.Globalization.CultureInfo> { USA },
    SupportedUICultures = new List<System.Globalization.CultureInfo> { USA },
   

};
app.UseRequestLocalization(Locale);


app.Run();
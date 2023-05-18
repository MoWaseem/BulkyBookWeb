using System.Diagnostics;
using System.Text;
using BulkyBook.Utilities;
using BullyBook.DataAccess.Data;
using BullyBook.DataAccess.Repository;
using BullyBook.DataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Stripe;


// Connection string through Env
DotNetEnv.Env.Load();
var connectionString = Environment.GetEnvironmentVariable("encodedConnectionString");
Debug.Assert(connectionString != null, nameof(connectionString) + " != null");
byte[] data = Convert.FromBase64String(connectionString);
var decodedConnectionString = Encoding.UTF8.GetString(data);

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddRazorPages();
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDbContext>(options
    => options.UseSqlServer(decodedConnectionString
    ));
builder.Services.Configure<StripSettings>(builder.Configuration.GetSection("Strip"));

builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddDefaultTokenProviders()
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddSingleton<IEmailSender, EmailSender >();
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = $"/Identity/Account/Login";
    options.LogoutPath = $"/Identity/Account/Logout";
    options.AccessDeniedPath = $"/Identity/Account/AccessDenied";
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
//These are Middleware and its sequence is really important
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
//Assigning golable Api Key inside Pipeline
StripeConfiguration.ApiKey = builder.Configuration.GetSection("Strip:SecretKey").Get<string>();
app.UseAuthentication();;


app.UseAuthorization();

app.MapRazorPages(); // this line will load razor pages for Register and Login
app.MapControllerRoute(
    name: "default",
    pattern: "/{area=Customer}/{controller=Home}/{action=Index}/{id?}");

app.Run();

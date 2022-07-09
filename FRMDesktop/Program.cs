using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Identity.Web;
using Microsoft.Identity.Web.UI;

var builder = WebApplication.CreateBuilder(args);

// Set up the environment
string keyString = builder.Configuration.GetValue<string>("sync-vars");
string[] keys = keyString.Split(';');
foreach (string key in keys)
{
    Environment.SetEnvironmentVariable(key, builder.Configuration.GetValue<string>(key));
}

// Add services to serer html pages
builder.Services.AddRazorPages();

// Add services to serer REST api
builder.Services.AddControllers();

// Build the swagger inforamtion
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// This lambda determines whether user consent for non-essential cookies is needed for a given request.
builder.Services.Configure<CookiePolicyOptions>(options =>
{
    options.CheckConsentNeeded = context => true;
    options.MinimumSameSitePolicy = SameSiteMode.Unspecified;
    options.HandleSameSiteCookieCompatibility();
});

// Sign-in users with the Microsoft identity platform
builder.Services.AddMicrosoftIdentityWebAppAuthentication(builder.Configuration);

builder.Services.AddControllersWithViews(options =>
{
    var policy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();
    options.Filters.Add(new AuthorizeFilter(policy));
}).AddMicrosoftIdentityUI();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();

app.Run();
using app.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using OutOfOfficeWebApp;
using OutOfOfficeWebApp.Models.Enums;
using OutOfOfficeWebApp.Utils;
using System;
using System.Reflection.Metadata;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(cookieOptions => {
    cookieOptions.ExpireTimeSpan = TimeSpan.FromMinutes(20);
    cookieOptions.LoginPath = "/Login";
    cookieOptions.AccessDeniedPath = "/Forbidden";
    cookieOptions.Cookie.SameSite = SameSiteMode.None;

    cookieOptions.Events.OnRedirectToLogin = ctx =>
    {
        ctx.Response.Redirect(cookieOptions.LoginPath);
        return Task.FromResult(0);
    };
});

// Add services to the container.
builder.Services.AddRazorPages()
        .AddRazorPagesOptions(options =>
        {
            options.RootDirectory = "/Lists";
        });


builder.Services.AddAuthorization(options =>
{
    
});




var connectionString = builder.Configuration.GetConnectionString("AppDb");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString)
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();


app.MapRazorPages();

app.Run();

using app.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using OutOfOfficeWebApp.Models.Enums;
using OutOfOfficeWebApp.Utils;
using System;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(cookieOptions => {
    cookieOptions.ExpireTimeSpan = TimeSpan.FromMinutes(20);
    cookieOptions.LoginPath = "/Login";
    cookieOptions.AccessDeniedPath = "/Forbidden";
    cookieOptions.Cookie.SameSite = SameSiteMode.None;

    // cookieOptions.Events.OnRedirectToAccessDenied = 
    cookieOptions.Events.OnRedirectToLogin = ctx =>
    {
        if (ctx.Request.Path.StartsWithSegments("/api"))
        {
            ctx.Response.StatusCode = StatusCodes.Status403Forbidden;
        }
        else
        {
            ctx.Response.Redirect(cookieOptions.LoginPath);
        }
        return Task.FromResult(0);
    };
});

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy =>
        policy.RequireRole(new string[] { "Admin" })
    );
    options.AddPolicy("OwnerOrAdmin", policy =>
    {
        policy.RequireAuthenticatedUser();
        // policy.Requirements.Add(new OwnerRequirement());
    });
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

/*using (var scope = app.Services.CreateScope())
{
    var service = scope.ServiceProvider;
    var dbContext = service.GetService<AppDbContext>();
    dbContext?.AbsenceReasons.SeedEnumValues<AbsenceReason, AbsenceReasonEnum>(@enum => @enum);
    dbContext?.ActiveStatuses.SeedEnumValues<ActiveStatus, ActiveStatusEnum>(@enum => @enum);
    dbContext?.Positions.SeedEnumValues<Position, PositionEnum>(@enum => @enum);
    dbContext?.ProjectTypes.SeedEnumValues<ProjectType, ProjectTypeEnum>(@enum => @enum);
    dbContext?.RequestStatuses.SeedEnumValues<RequestStatus, RequestStatusEnum>(@enum => @enum);
    dbContext?.Roles.SeedEnumValues<Role, RoleEnum>(@enum => @enum);
    dbContext?.Subdivisions.SeedEnumValues<Subdivision, SubdivisionEnum>(@enum => @enum);
}*/

app.Run();

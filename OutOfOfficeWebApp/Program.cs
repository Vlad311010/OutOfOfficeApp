using app.Authorization;
using app.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using OutOfOfficeWebApp.Interfaces;
using OutOfOfficeWebApp.Repositories;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(cookieOptions => {
    cookieOptions.ExpireTimeSpan = TimeSpan.FromMinutes(20);
    cookieOptions.LoginPath = "/";
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

            // Employees
            options.Conventions.AuthorizeFolder("/Employees", "Manager");
            options.Conventions.AuthorizePage("/Employees/Add", "HRManager");

            // Projects
            options.Conventions.AuthorizePage("/Projects/Add", "ProjectManager");
            options.Conventions.AuthorizePage("/Projects/Edit", "ProjectManager");

        });


builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Manager", policy =>
        policy.RequireRole(new string[] { "Administrator", "HRManager", "ProjectManager" })
    );

    options.AddPolicy("ProjectManager", policy =>
        policy.RequireRole(new string[] { "Administrator", "ProjectManager" })
    );

    options.AddPolicy("HRManager", policy =>
        policy.RequireRole(new string[] { "Administrator", "HRManager" })
    );

    options.AddPolicy("OwnerOrManager", policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.Requirements.Add(new OwnerRequirement());
    });
});

builder.Services.AddSingleton<IAuthorizationHandler, OwnerAuthorizationHandler>();

builder.Services.AddScoped<IEmployeesRepository, EmployeesRepository>();
builder.Services.AddScoped<IProjectsRepository, ProjectsRepository>();
builder.Services.AddScoped<IProjectEmployeesRepository, ProjectEmployeesRepository>();
builder.Services.AddScoped<ILeaveRequestsRepository, LeaveRequestsRepository>();
builder.Services.AddScoped<IApprovalRequestsRepository, ApprovalRequestsRepository>();



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

using Bookhive.Service.AccountService;
using Bookhive.Service.AuthorService;
using Bookhive.Service.CurrentUserService;
using Bookhive.Service.StudentService;
using BookHive.Dal.AccountRepository;
using BookHive.Dal.AuthorRepository;
using BookHive.Dal.DapperConfigure;
using BookHive.Dal.StudentRepository;
using BookHive.Data;
using BookHive.Hubs;
using BookHive.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
builder.Services.AddHttpClient();
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();


builder.Services.AddSignalR();

builder.Services.AddAutoMapper(typeof(Program).Assembly);

builder.Services.AddHttpContextAccessor();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddTransient<IDataAccessLayer, DataAccessLayer>();
builder.Services.AddScoped<BookHive.Helpers.Email>();
builder.Services.AddTransient<IAccountService, AccountService>();
builder.Services.AddTransient<IAccountRepository, AccountRepository>();
builder.Services.AddTransient<IStudentService, StudentService>();
builder.Services.AddTransient<IStudentRepository, StudentRepository>();
builder.Services.AddTransient<IAuthorService, AuthorService>();
builder.Services.AddTransient<IAuthorRepository, AuthorRepository>();
builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("connection")));

builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
})
.AddRoles<IdentityRole>()
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();



builder.Services.Configure<IdentityOptions>(options =>
{
    // Default Password settings.
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 1;

});

builder.Services.AddDistributedMemoryCache(); // Use a distributed cache provider
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Adjust as needed
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddHttpContextAccessor();


var app = builder.Build();

/*app.UseStaticFiles(new StaticFileOptions()
{
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @".\Image")),
    RequestPath = new PathString("/Image")
});*/

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
app.UseSession();
app.UseAuthentication();
app.UseAuthorization();
app.MapRazorPages();

/*app.UseAuthorization();*/

/**/

app.MapHub<ChatHubs>("/chatHub");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

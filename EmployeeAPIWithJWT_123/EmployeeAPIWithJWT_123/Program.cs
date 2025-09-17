using Azure.Identity;
using EmployeeAPIWithJWT_123;
using EmployeeAPIWithJWT_123.Identity;
using EmployeeAPIWithJWT_123.ServiceContract;
using EmployeeAPIWithJWT_123.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
string cs = builder.Configuration.GetConnectionString("constr");
builder.Services.AddEntityFrameworkSqlServer().AddDbContext<ApplicationDbContext>
 (option => option.UseSqlServer(cs, b =>
 b.MigrationsAssembly("EmployeeAPIWithJWT_123")));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//****
builder.Services.AddTransient<IRoleStore<ApplicationRole>, ApplicationRoleStore>();
builder.Services.AddTransient<UserManager<ApplicationUser>, ApplicationUserManager>();
builder.Services.AddTransient<SignInManager<ApplicationUser>, ApplicationSignInManager>();
builder.Services.AddTransient<RoleManager<ApplicationRole>, ApplicationRoleManager>();
builder.Services.AddTransient<IUserStore<ApplicationUser>, ApplicationUserStore>();
builder.Services.AddTransient<IUserService, UserServices>();
builder.Services.AddIdentity<ApplicationUser, ApplicationRole>()
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddUserStore<ApplicationUserStore>()
.AddUserManager<ApplicationUserManager>()
.AddRoleManager<ApplicationRoleManager>()
.AddSignInManager<ApplicationSignInManager>()
.AddRoleStore<ApplicationRoleStore>()
.AddDefaultTokenProviders();

builder.Services.AddScoped<ApplicationRoleStore>();
builder.Services.AddScoped<ApplicationUserStore>();
//****
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "MyPolicy",
        builder =>
        {
            builder.WithOrigins("http://localhost:4200/")
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();

        });

});
//jwt Authentication
var appSettingSection = builder.Configuration.GetSection("AppSettings");
builder.Services.Configure<AppSettings>(appSettingSection);
var appSetting = appSettingSection.Get<AppSettings>();
var key = Encoding.ASCII.GetBytes(appSetting.Secret);
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false,
    };

});
builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();




var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("MyPolicy");
app.UseAuthentication();

app.UseAuthorization();
//add users and roles
//IServiceScopeFactory serviceScopeFactory = app.Services.GetRequiredService<IServiceScopeFactory>();
//using (IServiceScope scope = serviceScopeFactory.CreateScope())
//{
//    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<ApplicationRole>>();
//    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
//    //Roles
//    if (!await roleManager.RoleExistsAsync("Admin"))
//    {
//        var role = new ApplicationRole();
//        role.Name = "Admin";
//        await roleManager.CreateAsync(role);
//    }
//    if (!await roleManager.RoleExistsAsync("employee"))
//    {
//        var role = new ApplicationRole();
//        role.Name = "Employee";
//        await roleManager.CreateAsync(role);
//    }
//    //Users
//    if (await userManager.FindByNameAsync("Vikas") == null)
//    {
//        var user = new ApplicationUser();
//        user.UserName = "vikas";
//        user.Email = "vikas@gmail.com";
//        var checkUser = await userManager.CreateAsync(user, "Vikas123#");
//        if (checkUser.Succeeded)
//        {
//            await userManager.AddToRoleAsync(user, "Admin");
//        }
//    }
//    if (await userManager.FindByNameAsync("Vishu") == null)
//    {
//        var user = new ApplicationUser();
//        user.UserName = "vishu";
//        user.Email = "vishu@gmail.com";
//        var checkUser = await userManager.CreateAsync(user, "Vishu123#");
//        if (checkUser.Succeeded)
//        {
//            await userManager.AddToRoleAsync(user, "Employee");
//        }
//    }
//}

app.MapControllers();

app.Run();

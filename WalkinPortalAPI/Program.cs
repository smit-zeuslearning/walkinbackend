using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WalkinPortalAPI.Models;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using WalkinPortalAPI.src.Mail;

var builder = WebApplication.CreateBuilder(args);

// Config variable(referencing apppsettings.json)
ConfigurationManager config = builder.Configuration;

// Injecting database depencency
builder.Services.AddDbContext<WalkinPortalContext>(options => options.UseMySql(config.GetConnectionString("DefaultConnection"), ServerVersion.AutoDetect(config.GetConnectionString("DefaultConnection"))));

// Injecting Identity dependency
builder.Services.AddIdentity<User, IdentityRole<int>>()
    .AddRoles<IdentityRole<int>>()
    .AddEntityFrameworkStores<WalkinPortalContext>()
    .AddDefaultTokenProviders();

// Configuration authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})

//JWT Bearer configutration
.AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters()
    {

        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidAudience = config["JWT:ValidAudience"],
        ValidIssuer = config["JWT:ValidIssuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JWT:Secret"]))
    };
});

// Adding CORS to allow frontend make request
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowOrigin", policy => policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
});

// Set forgot password token life span
builder.Services.Configure<DataProtectionTokenProviderOptions>(options =>
{
    options.TokenLifespan = TimeSpan.FromHours(1);
});

// Adding Mail Service dependency
builder.Services.AddScoped<IEmailService, EmailService>();

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = null;
    });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//app.UseCors("AllowFrontend");
app.UseCors(options => options.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

// Creating user role(seeding user role data)
using(var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<int>>>();

    string[] roles = { "admin", "employer", "employee" };

    foreach(var role in roles)
    {
        if(!await roleManager.RoleExistsAsync(role)){
            await roleManager.CreateAsync(new IdentityRole<int>(role));
        }
    }
}

app.Run();

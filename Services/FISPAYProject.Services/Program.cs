using FISPAYProject.Business;
using FISPAYProject.Business.Contracts;
using FISPAYProject.Data;
using FISPAYProject.Data.Context;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;

// Add services to the container.

// For Entity Framework
builder.Services.AddDbContext<FISPAYContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// For Identity
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<FISPAYContext>()
    .AddDefaultTokenProviders();

// Adding Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})

// Adding Jwt Bearer
.AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = false,
        ValidateIssuerSigningKey = true,
        //ValidAudience = configuration["JWT:ValidAudience"],
        ValidAudience = configuration["JWT:Issuer"],
        ValidIssuer = configuration["JWT:Issuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Key"]))
    };
});

builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "FIS PAY API", Version = "v1" });
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IVendorAccountRegistration, VendorAccountRegistrationRepository>();
builder.Services.AddScoped<IWallet, WalletRepository>();

//builder.Services.AddIdentity<ApplicationUser, IdentityRole>(opt =>
//{
//    opt.Password.RequiredLength = 7;
//    opt.Password.RequireDigit = false;
//    opt.Password.RequireUppercase = false;
//})
//.AddEntityFrameworkStores<FISPAYContext>();

//builder.Services.AddIdentity<User, IdentityRole>(opt =>
//{
//    opt.Password.RequiredLength = 7;
//    opt.Password.RequireDigit = false;
//    opt.Password.RequireUppercase = false;
//})
//.AddEntityFrameworkStores<ApplicationContext>();

var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
   
}

app.UseStaticFiles();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

// Authentication & Authorization
app.UseAuthorization();
app.UseAuthorization();

app.MapControllers();

app.Run();



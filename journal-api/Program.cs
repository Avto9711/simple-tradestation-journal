using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using journal.api.service;
using journal.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var  MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy  =>
                      {
                          policy.WithOrigins("http://localhost:8080");
                          policy.AllowAnyHeader();
                          policy.AllowAnyMethod();
                          policy.AllowCredentials();
                      });
});

builder
.Configuration
.AddJsonFile("appsettings.Local.Json",true);

IdentityModelEventSource.ShowPII = true;
IdentityModelEventSource.LogCompleteSecurityArtifact = true;

builder.Services
.AddAuthentication()
.AddJwtBearer((f)=>{   
    //Additional config snipped
     f.Events = new JwtBearerEvents
     {
         OnTokenValidated = async ctx =>
         {
             //Get the calling app client id that came from the token produced by Azure AD
             string clientId = ctx.Principal.FindFirstValue("http://tradestation.com/username");
             var claims = new List<Claim>
             {
                 new Claim("UserName", "joey")
             };
             var appIdentity = new ClaimsIdentity(claims);

             ctx.Principal.AddIdentity(appIdentity);
         }
     }; 
    f.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = false,
        //TODO: investigate how to validate the token signature
        SignatureValidator = delegate(string token, TokenValidationParameters parameters)
        {
            var jwt = new Microsoft.IdentityModel.JsonWebTokens.JsonWebToken(token);
            
            return jwt;
        },
        ValidIssuer = "https://signin.tradestation.com/",
        ValidAudiences = ["https://api.tradestation.com",
        "https://tradestation-prod.tslogin.auth0.com/userinfo"],
        };
});

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddTransient<IJournalService, JournalService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "Demo API", Version = "v1" });
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


builder.Services.Configure<TradeStationConfig>(
    builder.Configuration.GetSection(TradeStationConfig.Key));

builder.Services.AddHttpClient<TradeStationService>()
.AddHeaderPropagation();

builder.Services.AddHeaderPropagation(options =>
{
    options.Headers.Add("Authorization");
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(MyAllowSpecificOrigins);

app.UseAuthentication();
app.UseAuthorization();

app.UseHeaderPropagation();

app.MapControllers();

app.Run();

using JwtCommentsApp.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// 🔐 Реєстрація JWT-аутентифікації
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options => {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes("super_secret_key_123!")
            )
        };
    });

builder.Services.AddAuthorization();

// 🧩 Реєстрація контролерів і сервісу токенів
builder.Services.AddControllers();
builder.Services.AddSingleton<JwtService>();

var app = builder.Build();

// 📡 Middleware
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();

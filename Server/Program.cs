using AquaSolution.Data.Connection;
using AquaSolution.Data.Data;
using AquaSolution.Server;
using AquaSolution.Server.SignalR;
using AquaSolution.Shared.Permissions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// ----------------------- Services ----------------------- //
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddDbContext<AquaDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddControllers();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes("VerySecretKey12345"))
        };

        options.Events = new JwtBearerEvents
        {
            OnAuthenticationFailed = context =>
            {
                Console.WriteLine($"Authentication failed: {context.Exception.Message}");
                return Task.CompletedTask;
            }
        };
    });

builder.Services.AddAppServices();
builder.Services.AddAuthorization();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSignalR();

// ----------------------- Build app ----------------------- //
var app = builder.Build();

// ----------------------- Database migrate ----------------------- //
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AquaDbContext>();
    db.Database.Migrate();
    // DbSeeder.SeedData(db); // nếu có
}

// ----------------------- Middleware ----------------------- //
app.UsePathBase("/AquaSolution"); // base path
app.UseHttpsRedirection();

if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

// Swagger
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("v1/swagger.json", "AquaSolution API v1");
    c.RoutePrefix = "swagger";
});

app.UseAuthentication();
app.UseAuthorization();

// Blazor WebAssembly
app.UseBlazorFrameworkFiles("/AquaSolution"); // subfolder
app.UseStaticFiles();

app.UseRouting();

// Razor / API / SignalR
app.MapRazorPages();
app.MapControllers();
app.MapHub<SignalrHub>("/signalrhub");

// SPA fallback cho mọi route client-side
app.MapFallbackToFile("/AquaSolution/{*path:nonfile}", "index.html");

app.Run();

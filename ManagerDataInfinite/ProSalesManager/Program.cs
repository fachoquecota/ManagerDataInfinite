using ProSalesManager._01_Data.Modules.Login;
using ProSalesManager._01_Data.Modules.Login.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using ProSalesManager._02_Busnisess.Login.Interfaces;
using ProSalesManager._02_Busnisess.Login;
using ProSalesManager._04_Services.Login.Interfaces;
using ProSalesManager._04_Services.Login;
using ProSalesManager._01_Data.Modules.Users.Interfaces;
using ProSalesManager._01_Data.Modules.Users;
using ProSalesManager._02_Busnisess.Users.Interfaces;
using ProSalesManager._02_Busnisess.Users;
using ProSalesManager._01_Data.Modules.Products.Interfaces;
using ProSalesManager._01_Data.Modules.Products;
using ProSalesManager._02_Busnisess.Products.Interfaces;
using ProSalesManager._02_Busnisess.Products;
using ProSalesManager._02_Busnisess.Customers.Interfaces;
using ProSalesManager._02_Busnisess.Customers;
using ProSalesManager._01_Data.Modules.Customers.Interfaces;
using ProSalesManager._01_Data.Modules.Customers;
using ProSalesManager._01_Data.Modules.Supplier.Interfaces;
using ProSalesManager._01_Data.Modules.Supplier;
using ProSalesManager._02_Busnisess.Suppliers.Interfaces;
using ProSalesManager._02_Busnisess.Suppliers;
using ProSalesManager._01_Data.Modules.PublicProducts.Interfaces;
using ProSalesManager._02_Busnisess.PublicProducts.Interfaces;
using ProSalesManager._02_Busnisess.PublicProducts;
using ProSalesManager._01_Data.Modules.PublicProducts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ISP_Login, SP_Validation>();
builder.Services.AddScoped<IB_Login, B_Login>();

builder.Services.AddScoped<ITokenService, TokenService>();

builder.Services.AddScoped<ISP_Usuario, SP_GetUsuario>();
builder.Services.AddScoped<IB_Usuario, B_Usuario>();

builder.Services.AddScoped<ISP_Productos, SP_Productos>();
builder.Services.AddScoped<IB_Productos, B_Productos>();

builder.Services.AddScoped<ISP_Clientes, SP_Clientes>();
builder.Services.AddScoped<IB_Clientes, B_Clientes>();

builder.Services.AddScoped<ISP_Proveedores, SP_Proveedores>();
builder.Services.AddScoped<IB_Proveedores, B_Proveedores>();

builder.Services.AddScoped<ISP_PublicProducts, SP_PublicProducts>();
builder.Services.AddScoped<IB_PublicProducts, B_PublicProducts>();

//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("AllowAll",
//        builder =>
//        {
//            builder
//            .AllowAnyOrigin()
//            .AllowAnyMethod()
//            .AllowAnyHeader();
//        });
//});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"])),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

var app = builder.Build();

app.UseCors(options =>
{
    //options.WithOrigins("http://localhost:3030");
    //options.WithOrigins("file:///C:/Users/user/Documents/4%20Python/Josue/Project-main/index.html");

    options.AllowAnyMethod();

    options.AllowAnyHeader();
});
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseAuthentication();
app.UseAuthorization();
app.UseCors("AllowAll");  // Add this line

app.MapControllers();
app.MapGet("/", () => "API is running!");
app.Run();

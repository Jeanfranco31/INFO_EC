using BL.Auth.Login;
using BL.Cliente;
using BL.Empleado;
using BL.MenuOpciones;
using BL.Producto;
using INFO_EC_BACKEND.COMMON;
using INFO_EC_BACKEND.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<InfoEcDbContext>(opt =>
          opt.UseSqlServer(Common.connectionName)
);
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder => builder.AllowAnyOrigin()
                          .AllowAnyMethod());
});


builder.Services.AddScoped<ICliente, ClienteService>();
builder.Services.AddScoped<IEmpleado, EmpleadoService>();

builder.Services.AddScoped<ClienteService>();
builder.Services.AddScoped<EmpleadoService>();

builder.Services.AddScoped<LoginService>();
builder.Services.AddScoped<MenuOpcionService>();
builder.Services.AddScoped<IMenuOpcion, MenuOpcionService>();
builder.Services.AddScoped<IProducto, ProductService>();
builder.Services.AddScoped<ProductService>();

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

app.UseAuthorization();

app.UseCors("AllowSpecificOrigin");

app.MapControllers();

app.Run();

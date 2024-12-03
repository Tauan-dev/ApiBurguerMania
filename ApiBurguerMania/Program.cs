using Microsoft.EntityFrameworkCore;
using ApiBurguerMania.Data;
using ApiBurguerMania.Service;
using Microsoft.OpenApi.Models;
using ApiBurguerMania.Service.Interface;

var builder = WebApplication.CreateBuilder(args);

// Registra os serviços da API
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IStatusService, StatusService>();

// Configura o Swagger para documentação da API
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configura o banco de dados MySQL com ApplicationDbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion(new Version(8, 0, 25))  // Use a versão 8.0.28 do MySQL
    ));

// Adiciona o CORS (Cross-Origin Resource Sharing)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost4200", policy =>
    {
        policy.WithOrigins("http://localhost:4200")  // Permite apenas o frontend rodando em localhost:4200
              .AllowAnyMethod()  // Permite qualquer método HTTP
              .AllowAnyHeader(); // Permite qualquer cabeçalho
    });
});

builder.Services.AddControllers();

var app = builder.Build();

// Usa o Swagger
app.UseSwagger();
app.UseSwaggerUI();

// Redirecionamento para HTTPS
app.UseHttpsRedirection();

// Usa o CORS
app.UseCors("AllowLocalhost4200");

// Mapeia os controllers automaticamente
app.MapControllers();

app.Run();

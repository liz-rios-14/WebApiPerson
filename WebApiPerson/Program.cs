using System.Transactions;
using Microsoft.EntityFrameworkCore;
using WebApiPerson.Context;


var builder = WebApplication.CreateBuilder(args);

// Agrega servicios al contenedor (inyecci�n de dependencias)
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer(); // Para que Swagger reconozca los endpoints
builder.Services.AddSwaggerGen(); // Agrega Swagger para documentaci�n de la API

// Inyecta el contexto de base de datos con la cadena de conexi�n configurada
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Configuraci�n del pipeline de HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(); // Habilita Swagger UI en desarrollo
    app.UseSwaggerUI();
}

app.UseHttpsRedirection(); // Redirige HTTP a HTTPS

app.UseAuthorization(); // Middleware para autorizaci�n

app.MapControllers();  // Mapea los controladores con sus rutas

app.Run(); // Ejecuta la aplicaci�n

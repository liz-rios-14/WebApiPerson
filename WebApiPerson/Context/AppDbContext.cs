// Usamos Entity Framework Core y accedemos al modelo Person
using Microsoft.EntityFrameworkCore;
using WebApiPerson.Models;
namespace WebApiPerson.Context
{
    // Clase que define el contexto de la base de datos
    public class AppDbContext:DbContext
    {
        // Constructor que recibe las opciones del contexto (cadena de conexión, etc.)
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
        {

        }
        // DbSet representa una tabla en la base de datos; en este caso, la tabla "Persons"
        public DbSet<Person> Persons { get; set; }

    }
}

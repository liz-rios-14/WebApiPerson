using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiPerson.Context;
using WebApiPerson.Models;

// Controlador para manejar las peticiones HTTP relacionadas con "Person"
namespace WebApiPerson.Controllers
{
    // Ruta base: api/Person
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        // Inyección del contexto de base de datos
        private readonly AppDbContext _context;

        public PersonController(AppDbContext context)
        {
            _context = context;
        }

        // Método GET para obtener todas las personas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Person>>> GetPersons()
        {
            return await _context.Persons.ToListAsync();
        }

        // Método GET para obtener una persona por su id
        [HttpGet("{id}")]
        public async Task<ActionResult<Person>> GetPerson(int id)
        {
            var person = await _context.Persons.FindAsync(id);

            if (person == null)
            {
                return NotFound(); // Retorna 404 si no se encuentra
            }

            return person;
        }

        // Método PUT para actualizar una persona existente
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPerson(int id, Person person)
        {
            if (id != person.Id)
            {
                return BadRequest(); // Retorna 400 si los IDs no coinciden
            }
            // Marca la entidad como modificada
            _context.Entry(person).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync(); // Guarda los cambios
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonExists(id))
                {
                    return NotFound(); // Si no existe, retorna 404
                }
                else
                {
                    throw;
                }
            }

            return NoContent(); // Retorna 204 (éxito sin contenido)
        }

        // Método POST para crear una nueva persona
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Person>> PostPerson(Person person)
        {
            _context.Persons.Add(person); // Agrega la persona al contexto
            await _context.SaveChangesAsync(); // Guarda en la base de datos

            // Retorna 201 con la ruta para obtener la nueva persona
            return CreatedAtAction("GetPerson", new { id = person.Id }, person);
        }

        // Método DELETE para eliminar una persona por id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePerson(int id)
        {
            var person = await _context.Persons.FindAsync(id);
            if (person == null)
            {
                return NotFound(); // Si no existe, 404
            }

            _context.Persons.Remove(person); // Elimina la persona
            await _context.SaveChangesAsync(); // Guarda los cambios


            return NoContent(); // 204 (eliminación exitosa)
        }

        // Método auxiliar para verificar si una persona existe
        private bool PersonExists(int id)
        {
            return _context.Persons.Any(e => e.Id == id);
        }
    }
}

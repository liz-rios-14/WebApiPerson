namespace WebApiPerson.Models
{
    public class Person
    {
        // Clave primaria de la BD
        public int Id { get; set; }
        // Campos requeridos: Nombre y Edad
        public required string Name { get; set; }
        public required int Age { get; set; }
    }
}

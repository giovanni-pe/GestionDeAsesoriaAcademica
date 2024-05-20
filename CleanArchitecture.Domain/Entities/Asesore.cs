using System; // Importa el espacio de nombres System para utilizar tipos y clases básicas de .NET
using System.Collections.Generic; // Importa el espacio de nombres System.Collections.Generic para utilizar colecciones genéricas

namespace CleanArchitecture.Domain.Entities // Define el espacio de nombres CleanArchitecture.Domain.Entities donde se encuentra la clase
{
    public class Asesore : Entity // Declaración de la clase ResearchLine que hereda de la clase base Entity
    {
        public string Nombre { get; private set; }
        public string Apellido { get; private set; }
        // Constructor de la clase que inicializa las propiedades de la entidad
        
        public Asesore(Guid id, string nombre, string apellido) : base(id)
        {
            Nombre = nombre;
            Apellido = apellido;
        }

        public void SetNombre(string nombre)
        {
            Nombre = nombre;
        }

        public void SetApellido(string apellido)
        {
            Apellido = apellido;
        }
    }
}


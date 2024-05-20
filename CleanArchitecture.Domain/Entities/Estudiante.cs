using System; // Importa el espacio de nombres System para utilizar tipos y clases básicas de .NET
using System.Collections.Generic; // Importa el espacio de nombres System.Collections.Generic para utilizar colecciones genéricas

namespace CleanArchitecture.Domain.Entities // Define el espacio de nombres CleanArchitecture.Domain.Entities donde se encuentra la clase
{
    public class Estudiante : Entity // Declaración de la clase ResearchLine que hereda de la clase base Entity
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        // Constructor de la clase que inicializa las propiedades de la entidad
        
        public Estudiante(Guid id, string firstName, string lastName) : base(id)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        public void SetFirstName(string firstName)
        {
            FirstName = firstName;
        }

        public void SetLastName(string lastName)
        {
            LastName = lastName;
        }
    }
}


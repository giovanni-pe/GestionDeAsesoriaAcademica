using System; // Importa el espacio de nombres System para utilizar tipos y clases básicas de .NET
using System.Collections.Generic; // Importa el espacio de nombres System.Collections.Generic para utilizar colecciones genéricas

namespace CleanArchitecture.Domain.Entities // Define el espacio de nombres CleanArchitecture.Domain.Entities donde se encuentra la clase
{
    public class ResearchLine : Entity // Declaración de la clase ResearchLine que hereda de la clase base Entity
    {
        public string Name { get; private set; } // Propiedad Name que representa el nombre de la línea de investigación
        public Guid ResearchGroupId { get; private set; } // Propiedad ResearchGroupId que representa el identificador del grupo de investigación al que pertenece la línea de investigación
        public string Code { get; private set; } // Propiedad Code que representa el código de la línea de investigación
        public virtual ResearchGroup ResearchGroup { get; private set; } = null!; // Propiedad ResearchGroup que representa la relación de la línea de investigación con un grupo de investigación

        // Constructor de la clase que inicializa las propiedades de la entidad
        public ResearchLine(Guid id, string name, Guid researchGroupId, string code) : base(id)
        {
            Name = name; // Asigna el valor del parámetro name a la propiedad Name
            ResearchGroupId = researchGroupId; // Asigna el valor del parámetro researchGroupId a la propiedad ResearchGroupId
            Code = code; // Asigna el valor del parámetro code a la propiedad Code
        }

        // Método que permite establecer el nombre de la línea de investigación
        public void SetName(string name)
        {
            Name = name; // Asigna el valor del parámetro name a la propiedad Name
        }

        // Método que permite establecer el identificador del grupo de investigación al que pertenece la línea de investigación
        public void SetResearchGroupId(Guid researchGroupId)
        {
            ResearchGroupId = researchGroupId; // Asigna el valor del parámetro researchGroupId a la propiedad ResearchGroupId
        }

        // Método que permite establecer el código de la línea de investigación
        public void SetCode(string code)
        {
            Code = code; // Asigna el valor del parámetro code a la propiedad Code
        }
    }
}


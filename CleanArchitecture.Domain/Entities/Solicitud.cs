using System;
using System.Collections.Generic;

namespace CleanArchitecture.Domain.Entities
{
    public class Solicitud : Entity
    {
        public Solicitud(Guid id,Guid idEstudiante, Guid idAsesor, DateTime fechaSolicitud,
                         string estadoSolicitud, string descripcionSolicitud):base(id)
        {
            IdEstudiante = idEstudiante;
            IdAsesor = idAsesor;
            FechaSolicitud = fechaSolicitud;
            EstadoSolicitud = estadoSolicitud;
            DescripcionSolicitud = descripcionSolicitud;
        }

        public Guid IdSolicitud { get; set; }
        public Guid IdEstudiante { get; set; }
        public Guid IdAsesor { get; set; }
        public DateTime FechaSolicitud { get; set; }
        public string EstadoSolicitud { get; set; }
        public string DescripcionSolicitud { get; set; }

        public string Name { get; private set; } // Consider removing if not used

        public virtual ICollection<User> Users { get; private set; } = new HashSet<User>();
    }
}



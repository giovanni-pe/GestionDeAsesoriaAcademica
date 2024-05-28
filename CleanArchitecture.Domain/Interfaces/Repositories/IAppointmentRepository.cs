using CleanArchitecture.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace CleanArchitecture.Domain.Interfaces.Repositories
{
    public interface IAppointmentRepository : IRepository<Appointment>
    {
        Task<bool> ExistsAsync(Guid id);
        void Add(Appointment appointment);
        // Otros métodos según sea necesario
    }
}

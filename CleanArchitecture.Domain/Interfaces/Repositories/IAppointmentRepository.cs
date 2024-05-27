using System.Threading.Tasks;
using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Domain.Interfaces.Repositories
{
    public interface IAppointmentRepository : IRepository<Appointment>
    {
        // Add any additional methods specific to Appointment repository if needed
    }
}

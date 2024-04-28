using Application.Common;
using Domain.Entities;

namespace Application.Interfaces.Repository;

public interface IAttendanceRepository : IRepository<Domain.Entities.Attendance>
{
     public void Remove(Attendance attendance);
}
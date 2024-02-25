using Application.Interfaces.Repository;
using Domain.Entities;

namespace Infrastructure.Repositories;

public class AttendanceRepository : BaseRepository<Attendance>, IAttendanceRepository
{
    public AttendanceRepository(AttendanceDbContext context) : base(context)
    {
    }
}
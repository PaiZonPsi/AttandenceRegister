using Application.Interfaces.Repository;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class AttendanceRepository : BaseRepository<Attendance>, IAttendanceRepository
{
    private readonly AttendanceDbContext _context;
    public AttendanceRepository(AttendanceDbContext context) : base(context)
    {
        _context = context;
    }

    public override async Task<IEnumerable<Attendance>> GetAllAsync()
    {
        var entities = _context.Attendances.OrderBy(a => a.OccurrenceStartDate).Include(a => a.Employee).Include(a => a.Occurrence);

        return entities.AsEnumerable();
    }

    public override async Task<Attendance?> GetByIdAsync(int id)
    {
        var entity = await _context.Attendances
            .Where(a => a.Id == id)
            .Include(a => a.Employee)
            .Include(a => a.Occurrence)
            .FirstOrDefaultAsync();
        return entity;
    }

    public void Remove(Attendance attendance)
    {
        _context.Attendances.Remove(attendance);
    }
}
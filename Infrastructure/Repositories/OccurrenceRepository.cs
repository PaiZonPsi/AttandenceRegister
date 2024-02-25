using Application.Interfaces.Repository;
using Domain.Entities;

namespace Infrastructure.Repositories;

public class OccurrenceRepository : BaseRepository<Occurrence>, IOccurrenceRepository
{
    public OccurrenceRepository(AttendanceDbContext context) : base(context)
    {
    }
}
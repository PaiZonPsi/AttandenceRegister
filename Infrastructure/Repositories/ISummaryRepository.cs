
using Domain.Entities;

namespace Infrastructure.Repositories;

public interface ISummaryRepository
{
    public IEnumerable<EmployeeSummary> GetSummaryForEmployee(int employeeId);
}
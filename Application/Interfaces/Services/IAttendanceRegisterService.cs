using Application.Models.Attendances;

namespace Application.Interfaces.Services;

public interface IAttendanceRegisterService
{
    Task<IEnumerable<AttendanceModel>> GetAll();
    Task<AttendanceModel> Create(AttendanceModel model);
    Task<AttendanceModel> Update(AttendanceModel model);
    Task Delete(AttendanceModel model);
    Task<bool> Exists(int id);
}
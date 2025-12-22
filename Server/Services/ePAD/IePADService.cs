using AquaSolution.Shared.ePADDto;

namespace AquaSolution.Server.Services.ePAD
{
    public interface IePADService
    {
        Task<List<ePADDto>>GetUserByWorkDayId(string workDayId, string dateTime);
    }
}

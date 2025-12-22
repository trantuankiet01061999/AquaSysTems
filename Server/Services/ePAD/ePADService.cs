using AquaSolution.Data.Connection;
using AquaSolution.Shared.ePADDto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Globalization;

namespace AquaSolution.Server.Services.ePAD
{
    public class ePADService : IePADService
    {
        private readonly ePADContext _ePADContext;
        public ePADService(ePADContext ePADContext)
        {
            _ePADContext = ePADContext;
        }

        public async Task<List<ePADDto>> GetUserByWorkDayId(string workDayId, string dateTime)
        {
            var formats = new[]
            {
                "yyyy-MM-dd",
                "yyyy-MM-ddTHH:mm:ss",
                "yyyy-MM-ddTHH:mm:ss.fff",
                "yyyy-MM-dd HH:mm:ss",
                "yyyy-MM-dd HH:mm:ss.fff",
                "dd/MM/yyyy"
            };

            if (!DateTime.TryParseExact(
                    dateTime?.Trim(),
                    formats,
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.None,
                    out var date))
            {
                throw new ArgumentException(
                    $"Invalid dateTime value: '{dateTime}'. Expected format yyyy-MM-dd");
            }

            var data = await _ePADContext.ePAD
                .Where(x => x.EmployeeATID == workDayId
                         && x.CheckTime.Date == date.Date)
                .Select(x => new ePADDto
                {
                    EmployeeATID = x.EmployeeATID,
                    SerialNumber = x.SerialNumber,
                    CheckTime = x.CheckTime
                })
                .ToListAsync();

            return data;
        }

    }
}

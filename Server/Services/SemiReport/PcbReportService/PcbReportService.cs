using AquaSolution.Shared.SemiReport;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace AquaSolution.Server.Services.SemiReport.PcbReportService
{
    public class PcbReportService : IPcbReportService
    {
        private readonly IConfiguration _config;

        public PcbReportService(IConfiguration config)
        {
            _config = config;
        }

        public async Task<List<PcbReportDto>> GetAllAsync()
        {
            try
            {
                var result = new List<PcbReportDto>();
                var connStr = _config.GetConnectionString("SemiConnection");

                const string sql = @"
                    SELECT
                        PcbCode,
                        ScanTime,
                        IntermediateCode
                    FROM dbo.tbl_Pcb
                    ORDER BY ScanTime DESC;
                ";

                await using var con = new SqlConnection(connStr);
                await using var cmd = new SqlCommand(sql, con);

                await con.OpenAsync();
                await using var reader = await cmd.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    result.Add(new PcbReportDto
                    {
                        PcbCode = reader.IsDBNull(0) ? null : reader.GetString(0),
                        ScanTime = reader.IsDBNull(1) ? (DateTime?)null : reader.GetDateTime(1),
                        IntermediateCode = reader.IsDBNull(2) ? null : reader.GetString(2),
                    });
                }


                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }


}

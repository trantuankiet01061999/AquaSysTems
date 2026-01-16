
using AquaSolution.Shared.HRMSLOCAL;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using NPOI.HSSF.Record;
using NPOI.POIFS.Crypt.Dsig;
using NPOI.SS.Formula.Functions;
using System.Data;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AquaSolution.Server.Services.Hangfire
{
    public class DailyJobService : IDailyJobService
    {
        private readonly ILogger<DailyJobService> _logger;
        private readonly IConfiguration _configuration;

        public DailyJobService(ILogger<DailyJobService> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }
        #region Task RunDailyAsync
        public async Task RunDailyAsync()
        {
            int i = 1;
            var data = new List<DailyInOut>();
            do
            {
                var date = DateTime.Today.AddDays(-i);
                _logger.LogInformation(
                       "Hangfire job đọc attendance ngày {date} lúc {time}",
                       date,
                       DateTime.Now
                   );

                data = await GetQueryForYesterday(date);
                i++;
            }
            while (data.Count == 0);
            if (data.Count > 0)
            {
                await SaveDataToMes(data);
            }
            _logger.LogInformation(
                "Hangfire job hoàn tất, tổng {count} dòng",
                data.Count
            );
        }
        #endregion
        #region HandleDataForDate
        private async Task<List<DailyInOut>> GetQueryForYesterday(DateTime date)
        {
            try
            {
                var result = new List<DailyInOut>();
                using var connection = new SqlConnection(
                 _configuration.GetConnectionString("HRMSLOCAL"));
                using var cmd = new SqlCommand(
                "dbo.usp_GetDailyInOutTimes_ActiveOnly",
                connection);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CheckDate", date.Date);
                //cmd.Parameters.Add(new SqlParameter("@CheckDate", date.Date));
                await connection.OpenAsync();
                using var reader = await cmd.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    result.Add(new DailyInOut
                    {
                        EmployeeATID = reader.GetString(0),
                        EmployeeName = reader.GetString(1),
                        EmpFactory = reader.GetString(2),
                        InTime = reader.GetDateTime(3),
                        OutTime = reader.IsDBNull(4) ? null : reader.GetDateTime(4),
                        CheckDate = reader.GetDateTime(5)
                    });
                }

                return result;
            }
            catch (Exception ex)
            {
                throw ex;

            }

        }
        #endregion

        #region SaveDataToMes

        private static DateTime NowVietnam()
        {
            // Lấy giờ hiện tại theo VN (UTC+7) an toàn cho cả Windows và Linux
            var utcNow = DateTime.UtcNow;
            try
            {
                // Windows
                var tz = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");
                return TimeZoneInfo.ConvertTimeFromUtc(utcNow, tz);
            }
            catch (TimeZoneNotFoundException)
            {
                try
                {
                    // Linux (IANA). Ưu tiên Asia/Ho_Chi_Minh; fallback sang Asia/Bangkok nếu cần
                    var tz = TimeZoneInfo.FindSystemTimeZoneById("Asia/Ho_Chi_Minh");
                    return TimeZoneInfo.ConvertTimeFromUtc(utcNow, tz);
                }
                catch
                {
                    var tz = TimeZoneInfo.FindSystemTimeZoneById("Asia/Bangkok");
                    return TimeZoneInfo.ConvertTimeFromUtc(utcNow, tz);
                }
            }
            catch
            {
                // Fallback cuối cùng nếu timezone không tìm thấy
                return utcNow.AddHours(7);
            }
        }

        //private async Task SaveDataToMes(List<DailyInOut> dailyInOuts)
        //{
        //    var configuration = new ConfigurationBuilder()
        //             .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false)
        //             .Build();
        //    string mesConnection9A61 = configuration.GetSection("ConnectionStrings:MesConnection9A61").Value;
        //    string mesConnection9A62 = configuration.GetSection("ConnectionStrings:MesConnection9A62").Value;

        //    var group9A61 = dailyInOuts.Where(x => x.EmpFactory == "9A61").ToList();
        //    var group9A62 = dailyInOuts.Where(x => x.EmpFactory == "9A62").ToList();

        //      const string InsertAttendanceSql = @"
        //            INSERT INTO base_user_attendance_record (
        //                attendance_record_id,
        //                user_code,
        //                user_name,
        //                create_by,
        //                create_date,
        //                active,
        //                begin_time,
        //                end_time,
        //                attdate
        //            )
        //            VALUES (
        //                @attendance_record_id,
        //                @user_code,
        //                @user_name,
        //                @create_by,
        //                @create_date,
        //                @active,
        //                @begin_time,
        //                @end_time,
        //                @attdate
        //            );";

        //    const string CheckExistsSql = @"
        //                SELECT COUNT(*) 
        //                FROM base_user_attendance_record 
        //                WHERE user_code = @user_code AND attdate = @attdate;";

        //    async Task InsertGroupAsync(string connectionString, List<DailyInOut> group)
        //    {
        //        if (group.Count == 0 || string.IsNullOrWhiteSpace(connectionString))
        //            return;

        //        await using var conn = new MySqlConnection(connectionString);
        //        await conn.OpenAsync();
        //        await using var tx = await conn.BeginTransactionAsync();

        //        await using var cmdCheck = conn.CreateCommand();
        //        cmdCheck.Transaction = tx;
        //        cmdCheck.CommandType = CommandType.Text;
        //        cmdCheck.CommandText = CheckExistsSql;
        //        cmdCheck.Parameters.Add("@user_code", MySqlDbType.VarChar);
        //        cmdCheck.Parameters.Add("@attdate", MySqlDbType.Date);

        //        await using var cmdInsert = conn.CreateCommand();
        //        cmdInsert.Transaction = tx;
        //        cmdInsert.CommandType = CommandType.Text;
        //        cmdInsert.CommandText = InsertAttendanceSql;
        //        cmdInsert.Parameters.Add("@attendance_record_id", MySqlDbType.VarChar);
        //        cmdInsert.Parameters.Add("@user_code", MySqlDbType.VarChar);
        //        cmdInsert.Parameters.Add("@user_name", MySqlDbType.VarChar);
        //        cmdInsert.Parameters.Add("@create_by", MySqlDbType.VarChar);
        //        cmdInsert.Parameters.Add("@active", MySqlDbType.VarChar);
        //        cmdInsert.Parameters.Add("@begin_time", MySqlDbType.DateTime);
        //        cmdInsert.Parameters.Add("@end_time", MySqlDbType.DateTime);
        //        cmdInsert.Parameters.Add("@attdate", MySqlDbType.Date);

        //        foreach (var dto in group)
        //        {
        //            cmdCheck.Parameters["@user_code"].Value = dto.EmployeeATID ?? (object)DBNull.Value;
        //            cmdCheck.Parameters["@attdate"].Value = dto.CheckDate.Date;
        //            var exists = Convert.ToInt32(await cmdCheck.ExecuteScalarAsync()) > 0;
        //            if (exists)
        //                return; 
        //            cmdInsert.Parameters["@attendance_record_id"].Value = Guid.NewGuid().ToString();
        //            cmdInsert.Parameters["@user_code"].Value = dto.EmployeeATID ?? (object)DBNull.Value;
        //            cmdInsert.Parameters["@user_name"].Value = dto.EmployeeName ?? (object)DBNull.Value;
        //            cmdInsert.Parameters["@create_by"].Value = "Hangfire_job_Server14";
        //            cmdInsert.Parameters["@active"].Value = "1";
        //            cmdInsert.Parameters["@begin_time"].Value = dto.InTime;
        //            cmdInsert.Parameters["@end_time"].Value = (object?)dto.OutTime ?? DBNull.Value;
        //            cmdInsert.Parameters["@attdate"].Value = dto.CheckDate.Date;

        //            await cmdInsert.ExecuteNonQueryAsync();
        //        }
        //        await tx.CommitAsync();
        //    }
        //    await InsertGroupAsync(mesConnection9A61, group9A61);
        //    await InsertGroupAsync(mesConnection9A62, group9A62);
        //}

        private async Task SaveDataToMes(List<DailyInOut> dailyInOuts)
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false)
                .Build();

            string mesConnection9A61 = configuration.GetSection("ConnectionStrings:MesConnection9A61").Value;
            string mesConnection9A62 = configuration.GetSection("ConnectionStrings:MesConnection9A62").Value;

            var group9A61 = dailyInOuts.Where(x => x.EmpFactory == "9A61").ToList();
            var group9A62 = dailyInOuts.Where(x => x.EmpFactory == "9A62").ToList();

            const string InsertAttendanceSql = @"
                        INSERT INTO base_user_attendance_record (
                            attendance_record_id,
                            user_code,
                            user_name,
                            create_by,
                            create_date,
                            active,
                            begin_time,
                            end_time,
                            attdate
                        )
                        VALUES (
                            @attendance_record_id,
                            @user_code,
                            @user_name,
                            @create_by,
                            @create_date,
                            @active,
                            @begin_time,
                            @end_time,
                            @attdate
                        );";

            const string CheckExistsSql = @"
                SELECT COUNT(*) 
                FROM base_user_attendance_record 
                WHERE user_code = @user_code AND attdate = @attdate;";

            async Task InsertGroupAsync(string connectionString, List<DailyInOut> group)
            {
                if (group.Count == 0 || string.IsNullOrWhiteSpace(connectionString))
                    return;

                await using var conn = new MySqlConnection(connectionString);
                await conn.OpenAsync();

                await using var tx = await conn.BeginTransactionAsync();

                try
                {
                    // Command kiểm tra tồn tại
                    await using var cmdCheck = conn.CreateCommand();
                    cmdCheck.Transaction = tx;
                    cmdCheck.CommandType = CommandType.Text;
                    cmdCheck.CommandText = CheckExistsSql;
                    cmdCheck.CommandTimeout = 60;
                    cmdCheck.Parameters.Add("@user_code", MySqlDbType.VarChar);
                    cmdCheck.Parameters.Add("@attdate", MySqlDbType.Date);
                    cmdCheck.Prepare();

                    // Command insert
                    await using var cmdInsert = conn.CreateCommand();
                    cmdInsert.Transaction = tx;
                    cmdInsert.CommandType = CommandType.Text;
                    cmdInsert.CommandText = InsertAttendanceSql;
                    cmdInsert.CommandTimeout = 60;

                    cmdInsert.Parameters.Add("@attendance_record_id", MySqlDbType.VarChar);
                    cmdInsert.Parameters.Add("@user_code", MySqlDbType.VarChar);
                    cmdInsert.Parameters.Add("@user_name", MySqlDbType.VarChar);
                    cmdInsert.Parameters.Add("@create_by", MySqlDbType.VarChar);
                    cmdInsert.Parameters.Add("@create_date", MySqlDbType.DateTime); // ❗️bổ sung tham số
                    cmdInsert.Parameters.Add("@active", MySqlDbType.VarChar);
                    cmdInsert.Parameters.Add("@begin_time", MySqlDbType.DateTime);
                    cmdInsert.Parameters.Add("@end_time", MySqlDbType.DateTime);
                    cmdInsert.Parameters.Add("@attdate", MySqlDbType.Date);
                    cmdInsert.Prepare();

                    foreach (var dto in group)
                    {
                        // 1) Kiểm tra tồn tại (user_code + attdate)
                        cmdCheck.Parameters["@user_code"].Value = dto.EmployeeATID ?? (object)DBNull.Value;
                        cmdCheck.Parameters["@attdate"].Value = dto.CheckDate.Date;
                        var exists = Convert.ToInt32(await cmdCheck.ExecuteScalarAsync()) > 0;

                        if (exists)
                            return; // ❗️không return cả hàm, chỉ bỏ qua bản ghi này

                        // 2) Gán các tham số cho insert
                        cmdInsert.Parameters["@attendance_record_id"].Value = Guid.NewGuid().ToString();
                        cmdInsert.Parameters["@user_code"].Value = dto.EmployeeATID ?? (object)DBNull.Value;
                        cmdInsert.Parameters["@user_name"].Value = dto.EmployeeName ?? (object)DBNull.Value;
                        cmdInsert.Parameters["@create_by"].Value = "Hangfire_job_Server14";

                        // Lấy giờ hiện tại theo múi giờ VN
                        var nowVN = NowVietnam();
                        cmdInsert.Parameters["@create_date"].Value = nowVN;

                        cmdInsert.Parameters["@active"].Value = "1";
                        cmdInsert.Parameters["@begin_time"].Value =
                            dto.InTime is DateTime inTime ? inTime : (object)DBNull.Value;
                        cmdInsert.Parameters["@end_time"].Value =
                            dto.OutTime is DateTime outTime ? outTime : (object)DBNull.Value;
                        cmdInsert.Parameters["@attdate"].Value = dto.CheckDate.Date;

                        await cmdInsert.ExecuteNonQueryAsync();
                    }

                    await tx.CommitAsync();
                }
                catch
                {
                    await tx.RollbackAsync();
                    throw;
                }
            }

            await InsertGroupAsync(mesConnection9A61, group9A61);
            await InsertGroupAsync(mesConnection9A62, group9A62);
        }

        #endregion

    }
}
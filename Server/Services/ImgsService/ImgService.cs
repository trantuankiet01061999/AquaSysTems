using AquaSolution.Data.Connection;
using AquaSolution.Data.Data.Entities;
using AquaSolution.Data.Data.Entities.KPI;
using AquaSolution.Data.KPI.Entities;
using AquaSolution.Data.Repositories;
using AquaSolution.Shared.Enum;
using AquaSolution.Shared.Enum.KPIType;
using AquaSolution.Shared.Imgs;
using AquaSolution.Shared.KPI.IndexWeight;
using AquaSolution.Shared.KPI.KPIActual;
using AquaSolution.Shared.KPI.KPISubmit;
using AquaSolution.Shared.KPI.Result;
using AquaSolution.Shared.KPI.UserTask;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace AquaSolution.Server.Services.ImgsService
{
    public class ImgService : IImgService
    {

        public ImgService(IRepository<KPIIndexWeight> kpiIndexWeightRepo)
        {

        }

        public async Task<List<CloudinaryImageDto>> GetImagesFromCloudinary(string workId)
        {
            var cloudName = "dphblwavf";
            var apiKey = "576923445121493";
            var apiSecret = "LC-nxR3dP-U2tZpxy-N2wdccw7U";


            //var url =
            //    $"https://api.cloudinary.com/v1_1/{cloudName}/resources/image" +
            //    $"?type=upload&prefix=work/{workId}&max_results=50&direction=desc";
            var url =
                $"https://api.cloudinary.com/v1_1/{cloudName}/resources/image" +
                $"?type=upload&prefix=work/{workId}&max_results=50&direction=desc&context=true";
            using var http = new HttpClient();

            var auth = Convert.ToBase64String(
                Encoding.UTF8.GetBytes($"{apiKey}:{apiSecret}")
            );

            http.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Basic", auth);

            var res = await http.GetAsync(url);
            var json = await res.Content.ReadAsStringAsync();

            if (!res.IsSuccessStatusCode)
                throw new Exception(json);

            using var doc = JsonDocument.Parse(json);

            var images = new List<CloudinaryImageDto>();

            foreach (var item in doc.RootElement
                                     .GetProperty("resources")
                                     .EnumerateArray())
            {
                //---------------\
                string workIdFromContext = null;

                if (item.TryGetProperty("context", out var context))
                {
                    if (context.TryGetProperty("custom", out var custom) &&
                        custom.TryGetProperty("work_id", out var wid))
                    {
                        workIdFromContext = wid.GetString();
                    }
                }
                //---------------
                var sizeInKb = item.GetProperty("bytes").GetDecimal() / 1024m;
                images.Add(new CloudinaryImageDto
                {
                    PublicId = item.GetProperty("public_id").GetString(),
                    SecureUrl = item.GetProperty("secure_url").GetString(),

                    WorkId = workIdFromContext,
                    FileSize = sizeInKb >= 1024
                    ? Math.Round(sizeInKb / 1024m, 2)   // MB
                    : Math.Round(sizeInKb, 2),           // KB

                    FileSizeUnit = sizeInKb >= 1024 ? "MB" : "KB",


                    UpLoadDate = item
                        .GetProperty("created_at")
                        .GetDateTimeOffset()
                        .ToLocalTime()
                        .DateTime

                });
            }

            return images;
        }
    }
}

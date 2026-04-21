using AquaSolution.Shared.Enum;
using AquaSolution.Shared.Enum.KPIType;
using AquaSolution.Shared.Imgs;
using AquaSolution.Shared.KPI.IndexWeight;
using AquaSolution.Shared.KPI.KPIActual;
using AquaSolution.Shared.KPI.KPISubmit;
using AquaSolution.Shared.KPI.Result;
using AquaSolution.Shared.KPI.UserTask;

namespace AquaSolution.Server.Services.ImgsService
{
    public interface IImgService
    {
        Task<List<CloudinaryImageDto>> GetImagesFromCloudinary(string workId);
        Task<List<CloudinaryImageDto>> GetAllImagesFromCloudinary();
    }
}

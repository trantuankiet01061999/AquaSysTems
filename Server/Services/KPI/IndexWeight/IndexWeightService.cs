   using AquaSolution.Data.Connection;
using AquaSolution.Data.Data.Entities;
using AquaSolution.Data.Data.Entities.KPI;
using AquaSolution.Data.KPI.Entities;
using AquaSolution.Data.Repositories;
using AquaSolution.Shared.Enum;
using AquaSolution.Shared.Enum.KPIType;
using AquaSolution.Shared.KPI.IndexWeight;
using AquaSolution.Shared.KPI.KPIActual;
using AquaSolution.Shared.KPI.KPISubmit;
using AquaSolution.Shared.KPI.Result;
using AquaSolution.Shared.KPI.UserTask;
using Microsoft.EntityFrameworkCore;

namespace AquaSolution.Server.Services.KPI.IndexWeight
{
    public class IndexWeightService : IIndexWeightService
    {
        private readonly IRepository<KPIIndexWeight> _kpiIndexWeightRepo;


        private readonly AquaDbContext _context;
        public IndexWeightService(IRepository<KPIIndexWeight> kpiIndexWeightRepo)
        {
            _kpiIndexWeightRepo = kpiIndexWeightRepo;
        }

        public async Task<bool> CreatedAsync(IndexWeightDto indexWeightDto)
        {
          var kpiIndexWeight = new KPIIndexWeight
          {
              Id = Guid.NewGuid(),
              Name = indexWeightDto.Name,
              Weight = indexWeightDto.Weight,
              PeriodType = indexWeightDto.PeriodType,
              PositionType = indexWeightDto.PositionType,
              KPIIndexType = indexWeightDto.KPIIndexType
          };
            await _kpiIndexWeightRepo.InsertAsync(kpiIndexWeight);
            return await _kpiIndexWeightRepo.SaveChangesAsync() > 0;
        }

        public async Task<List<IndexWeightDto>> GetListAsync()
        {
          var list = await _kpiIndexWeightRepo.GetQueryableAsync();
            var query = from item in list
                        select new IndexWeightDto
                        {
                            Id = item.Id,
                            Name = item.Name,
                            Weight = item.Weight,
                            PeriodType = item.PeriodType,
                            PositionType = item.PositionType,
                            KPIIndexType = item.KPIIndexType
                        };
            return await query.ToListAsync();
        }
    }
}

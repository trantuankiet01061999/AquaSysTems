using AquaSolution.Data.Data.Entities;
using AquaSolution.Data.Data.Entities.KPI;
using AquaSolution.Data.Repositories;
using AquaSolution.Shared.KPI.KPITasks;
using AquaSolution.Shared.KPI.QuaterCalculated;
using Microsoft.EntityFrameworkCore;

namespace AquaSolution.Server.Services.KPi.QuarterCalculateds;

public class QuarterCalculatedService : IQuarterCalculatedService
{
    private readonly IRepository<QuarterCalculate> _quarterCalculatedRepo;




    public QuarterCalculatedService(IRepository<QuarterCalculate> quarterCalculatedRepo)
    {
        _quarterCalculatedRepo = quarterCalculatedRepo;

    }

    public async Task<bool> CreatedAsync(QuarterCalculatedDto calculatedDto)
    {
        var entity = new QuarterCalculate
        {
            Id = Guid.NewGuid(),
            Description = calculatedDto.Description,
            QuarterCalculated = calculatedDto.Calculated,
            QuarterCalculateType = calculatedDto.QuarterCalculateType

        };
        await _quarterCalculatedRepo.InsertAsync(entity);
        return await _quarterCalculatedRepo.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeletedAsync(Guid Id)
    {
        var entity = await _quarterCalculatedRepo.GetByIdAsync(Id);
        if (entity == null)
            return false;
        return await _quarterCalculatedRepo.DeleteAsync(entity);
    }

    public async Task<List<QuarterCalculatedDto>> LoadListAsync()
    {
        var getqueryable = await _quarterCalculatedRepo.GetQueryableAsync();
        var query = from calculated in getqueryable
                    select
                    new QuarterCalculatedDto
                    {
                        Id = calculated.Id,
                        Calculated = calculated.QuarterCalculated,
                        QuarterCalculateType = calculated.QuarterCalculateType,
                        Description = calculated.Description
                    };
        if (query == null)
            return new List<QuarterCalculatedDto>();
        return await query.ToListAsync();
    }

    public Task<bool> UpdateAsync(QuarterCalculatedDto calculated)
    {
        var entity = _quarterCalculatedRepo.GetByIdAsync(calculated.Id);
        if (entity == null)
            return Task.FromResult(false);
        if (entity.Result != null)
        {
            entity.Result.QuarterCalculated = calculated.Calculated;
            entity.Result.Description = calculated.Description;
            entity.Result.QuarterCalculateType = calculated.QuarterCalculateType;
        }
        return _quarterCalculatedRepo.UpdateAsync(entity.Result);
    }

}

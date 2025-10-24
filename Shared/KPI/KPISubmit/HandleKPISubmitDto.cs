using AquaSolution.Shared.Enum.KPIType;

namespace AquaSolution.Shared.KPI.KPISubmit
{
    public class HandleKPISubmitDto
    {
       public List<HandleActualDto> HandleActual { get; set; } = new List<HandleActualDto>();
        public List<KPITotalScoreDto> KPITotalScore { get; set; } = new List<KPITotalScoreDto>();
    }
}

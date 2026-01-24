using System.Text.Json.Serialization;
using AquaSolution.Shared.Enum.KPIType;

namespace AquaSolution.Shared.KPI.KPITasks
{
    public class HandleTaskDto
    {
        public Guid Id { get; set; }

        [JsonPropertyName("taskName")]
        public string TaskName { get; set; } = string.Empty;

        [JsonPropertyName("kpiCategory")]
        public KPICategoryType KPICategory { get; set; }

        [JsonPropertyName("taskDescription")]
        public string TaskDescription { get; set; }

        [JsonPropertyName("calculatedMdethod")]
        public string CalculatedMdethod { get; set; }

        [JsonPropertyName("dataSource")]
        public string DataSource { get; set; }

        [JsonPropertyName("pic")]
        public string PIC { get; set; }

        [JsonPropertyName("kpiIndexType")]
        public KPIIndexType KPIIndexType { get; set; }

        [JsonPropertyName("formulaId")]
        public Guid FormulaId { get; set; }

        [JsonPropertyName("calculatedId")]
        public Guid CalculatedId { get; set; }

        [JsonPropertyName("max")]
        public decimal Max { get; set; }

        [JsonPropertyName("bottom")]
        public decimal Bottom { get; set; }

        [JsonPropertyName("factoryId")]
        public Guid FactoryId { get; set; }

        [JsonPropertyName("unit")]
        public string Unit { get; set; }

        [JsonPropertyName("departmentId")]
        public Guid DepartmentId { get; set; }

        [JsonPropertyName("createdById")]
        public Guid CreatedById { get; set; }

        [JsonPropertyName("createdDate")]
        public DateTime CreatedDate { get; set; }
    }
}

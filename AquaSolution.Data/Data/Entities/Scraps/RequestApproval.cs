using AquaSolution.Shared.Enum.Scrap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AquaSolution.Data.Data.Entities.Scraps
{
    public class RequestApproval
    {
        public Guid Id { get; set; }
        public Guid HistoryScrapId { get; set; } 
        public StatusScrap Status { get; set; }
        public string? Comment { get; set; } = null;
        public Guid? ActionBy {  get; set; }
        public DateTime? ActionDate { get; set; }
        public string Title { get; set; }   
        public int Step { get; set; }
        public Guid DecisionMaker {  get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthTracker.Models
{
    public class DiseaseRequest
    {
        public string AnimalId { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string NameOfDisease { get; set; } = string.Empty;
        public string DiseaseDescription { get; set; } = string.Empty;
        public string Recommendations { get; set; } = string.Empty;
        public HealthRequest HealthRequest { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthTracker.Models
{
    public class HealthRequest
    {
        [CategoryAttribute("HealthTracker"), DescriptionAttribute("HeartRate")]
        public int HeartRate { get; set; }
        [CategoryAttribute("HealthTracker"), DescriptionAttribute("Temperature")]
        public double Temperature { get; set; }
        [CategoryAttribute("HealthTracker"), DescriptionAttribute("Pressure")]
        public int Pressure { get; set; }
        [CategoryAttribute("HealthTracker"), DescriptionAttribute("BloodOxygen")]
        public int BloodOxygen { get; set; }
    }
}

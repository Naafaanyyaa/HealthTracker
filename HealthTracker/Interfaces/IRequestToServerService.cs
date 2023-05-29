using HealthTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthTracker.Interfaces
{
    public interface IRequestToServerService
    {
        HealthRequest GenerateData();
    }
}

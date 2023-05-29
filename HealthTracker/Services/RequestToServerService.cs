using System.ComponentModel;
using HealthTracker.Interfaces;
using HealthTracker.Models;
using System.Text;
using System.Text.Json;
using HealthTracker.Exctentions;
using Microsoft.Extensions.Configuration;

namespace HealthTracker.Services
{
    public class RequestToServerService : IRequestToServerService
    {
        private JsonSerializerOptions options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
        };

        public HealthRequest GenerateData()
        {
            var healthData = new HealthRequest()
            {
                HeartRate = new Random().Next(55, 140),
                Temperature = new Random().NextDouble(35.00, 40.00),
                Pressure = new Random().Next(100, 180),
                BloodOxygen = new Random().Next(90, 99),
            };

            return healthData;
        }
    }
}

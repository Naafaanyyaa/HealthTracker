
using System.Text;
using HealthTracker.Interfaces;
using HealthTracker.Models;
using System.Xml;

namespace HealthTracker.Services
{
    public class MessageService : IMessageService
    {
        public DiseaseRequest GenerateRecommendations(string animalId, string userName, HealthRequest healthRequest)
        {
            XmlDocument config = new XmlDocument(), disease = new XmlDocument();
            config.Load("../../../App.config");
            disease.Load("../../../Diseases.xml");

            StringBuilder diseases = new StringBuilder();
            StringBuilder recommendations = new StringBuilder();

            var language = config.SelectSingleNode("//Language").InnerText;

            string nameOfDisease = language == "en" ? "Disease from IoT. Date" : "Звіт з IoT пристрою. Дата";

            if (healthRequest.Temperature > Convert.ToDouble(config.SelectSingleNode("//Temperature/Upper")?.InnerText))
            {
                foreach (XmlNode node in disease
                             .SelectNodes($"//Disease[@value='Temperature' and @language='{language!}' and @border='max']"))
                {
                    diseases.Append($"{node.SelectSingleNode("Description")?.InnerText}\n");
                    recommendations.Append($"{node.SelectSingleNode("Recommendations")?.InnerText}\n");
                }
            }
            else if (healthRequest.Temperature < Convert.ToDouble(config.SelectSingleNode("//Temperature/Lower").InnerText))
            {
                foreach (XmlNode node in disease
                             .SelectNodes($"//Disease[@value='Temperature' and @language='{language!}' and @border='min']"))
                {
                    diseases.Append($"{node.SelectSingleNode("Description")?.InnerText}\n");
                    recommendations.Append($"{node.SelectSingleNode("Recommendations")?.InnerText}\n");
                }
            }


            if (healthRequest.HeartRate > Convert.ToDouble(config.SelectSingleNode("//Bpm/Upper").InnerText))
            {
                foreach (XmlNode node in disease
                             .SelectNodes($"//Disease[@value='BPM' and @language='{language!}' and @border='max']"))
                {
                    diseases.Append($"{node.SelectSingleNode("Description")?.InnerText}\n");
                    recommendations.Append($"{node.SelectSingleNode("Recommendations")?.InnerText}\n");
                }
            }
            else if (healthRequest.HeartRate < Convert.ToDouble(config.SelectSingleNode("//Bpm/Lower").InnerText))
            {
                foreach (XmlNode node in disease
                             .SelectNodes($"//Disease[@value='BPM' and @language='{language!}' and @border='min']"))
                {
                    diseases.Append($"{node.SelectSingleNode("Description")?.InnerText}\n");
                    recommendations.Append($"{node.SelectSingleNode("Recommendations")?.InnerText}\n");
                }
            }


            if (healthRequest.HeartRate > Convert.ToDouble(config.SelectSingleNode("//Pressure/Upper").InnerText))
            {
                foreach (XmlNode node in disease
                             .SelectNodes($"//Disease[@value='Pressure' and @language='{language!}' and @border='max']"))
                {
                    diseases.Append($"{node.SelectSingleNode("Description")?.InnerText}\n");
                    recommendations.Append($"{node.SelectSingleNode("Recommendations")?.InnerText}\n");
                }
            }
            else if (healthRequest.HeartRate < Convert.ToDouble(config.SelectSingleNode("//Pressure/Lower").InnerText))
            {
                foreach (XmlNode node in disease
                             .SelectNodes($"//Disease[@value='Pressure' and @language='{language!}' and @border='min']"))
                {
                    diseases.Append($"{node.SelectSingleNode("Description")?.InnerText}\n");
                    recommendations.Append($"{node.SelectSingleNode("Recommendations")?.InnerText}\n");
                }
            }


            if (healthRequest.HeartRate > Convert.ToDouble(config.SelectSingleNode("//BloodOxygen/Upper").InnerText))
            {
                foreach (XmlNode node in disease
                             .SelectNodes($"//BloodOxygen[@value='Pressure' and @language='{language!}' and @border='max']"))
                {
                    diseases.Append($"{node.SelectSingleNode("Description")?.InnerText}\n");
                    recommendations.Append($"{node.SelectSingleNode("Recommendations")?.InnerText}\n");
                }
            }
            else if (healthRequest.HeartRate < Convert.ToDouble(config.SelectSingleNode("//BloodOxygen/Lower").InnerText))
            {
                foreach (XmlNode node in disease
                             .SelectNodes($"//Disease[@value='BloodOxygen' and @language='{language!}' and @border='min']"))
                {
                    diseases.Append($"{node.SelectSingleNode("Description")?.InnerText}\n");
                    recommendations.Append($"{node.SelectSingleNode("Recommendations")?.InnerText}\n");
                }
            }

            return new DiseaseRequest
            {
                AnimalId = animalId,
                UserName = userName,
                NameOfDisease = $"{nameOfDisease}: {DateTime.Now.ToString()}",
                HealthRequest = healthRequest,
                DiseaseDescription = $"{diseases.ToString()}",
                Recommendations = recommendations.ToString(),
                
            };
        }
    }
}

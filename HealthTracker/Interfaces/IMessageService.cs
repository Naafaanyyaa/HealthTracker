
using HealthTracker.Models;

namespace HealthTracker.Interfaces
{
    public interface IMessageService
    {
        DiseaseRequest GenerateRecommendations(string animalId, string userName, HealthRequest healthRequest);
    }
}

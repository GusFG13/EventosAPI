using EventosAPI.Entities;

namespace EventosAPI.Services
{
    public interface ICityEventService
    {
        Task<bool> CreateEvent(CityEvent cityEvent);
        Task<List<CityEvent>> GetEventList();
        Task<CityEvent> GetEvent(int id);
        Task<CityEvent> UpdateEvent(CityEvent cityEvent);
        Task<bool> DeleteEvent(int key);

        Task<List<CityEvent>> GetEventListByPartialName(string partialName);

        Task<List<CityEvent>> GetEventListByPriceRangeDate(double precoMin, double precoMax, DateTime date);

        Task<List<CityEvent>> GetEventListByLocalAndDate(string local, DateTime date);
    }
}

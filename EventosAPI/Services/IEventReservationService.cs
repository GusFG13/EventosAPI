using EventosAPI.Entities;

namespace EventosAPI.Services
{
    public interface IEventReservationService
    {
        Task<bool> CreateReservation(EventReservation eventReservation);
        Task<List<EventReservation>> GetReservationtList();
        Task<EventReservation> GetReservation(int id);
        Task<EventReservation> UpdateReservation(EventReservation eventReservation);
        Task<bool> DeleteReservation(int id);
        Task<List<EventReservation>> GetReservByPersonNamePartialTitle(string personName, string partialEventTitle);
        Task<List<EventReservation>> GetReservationByEvent(int idEvent);
    }
}

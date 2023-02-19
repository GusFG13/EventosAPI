using EventosAPI.Entities;

namespace EventosAPI.Services
{
    public class EventReservationService : IEventReservationService
    {
        private readonly IDbService _dbService;

        public EventReservationService(IDbService dbService)
        {
            _dbService = dbService;
        }

        public async Task<bool> CreateReservation(EventReservation eventReservation)
        {
            var result =
                await _dbService.EditData(
                    "INSERT INTO EventReservation (idReservation, idEvent, personName, quantity) VALUES (@IdReservation, @IdEvent, @PersonName, @Quantity)",
                    eventReservation);
            if (result == 1)
            {
                return true;
            }
            return false;
        }

        public async Task<List<EventReservation>> GetReservationtList()
        {
            var reservationList = await _dbService.GetAll<EventReservation>("SELECT * FROM EventReservation", new { });
            return reservationList;
        }

        public async Task<EventReservation> GetReservation(int id)
        {
            var reservationById = await _dbService.GetAsync<EventReservation>("SELECT * FROM EventReservation where idReservation=@id", new { id });
            return reservationById;
        }

        public async Task<EventReservation> UpdateReservation(EventReservation eventReservation)
        {
            var updateReservation =
                await _dbService.EditData(
                    "Update EventReservation SET quantity=@Quantity WHERE idReservation=@IdReservation", eventReservation);
            if (updateReservation == 1)
            {
                return eventReservation;
            }
            return null;
        }

        public async Task<bool> DeleteReservation(int id)
        {
            var deleteReservation = await _dbService.EditData("DELETE FROM EventReservation WHERE idReservation=@Id", new { id });
            if (deleteReservation == 1)
            {
                return true;
            }
            return false;
        }
    }
}

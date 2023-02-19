using EventosAPI.Entities;

namespace EventosAPI.Services
{
    public class CityEventService : ICityEventService
    {

        private readonly IDbService _dbService;

        public CityEventService(IDbService dbService)
        {
            _dbService = dbService;
        }

        public async Task<bool> CreateEvent(CityEvent cityEvent)
        {
            var result =
                await _dbService.EditData(
                    "INSERT INTO CityEvent (idEvent, title, description, dateHourEvent, local, address, price, status) VALUES (@IdEvent, @Title, @Description, @DateHourEvent, @Local, @Address, @Price, @Status)",
                    cityEvent);
            if (result == 1)
            {
                return true;
            }
            return false;
        }

        public async Task<List<CityEvent>> GetEventList()
        {
            var eventList = await _dbService.GetAll<CityEvent>("SELECT * FROM CityEvent", new { });
            return eventList;
        }

        public async Task<List<CityEvent>> GetEventListByPartialName(string partialName)
        {
            var eventList = await _dbService.GetAll<CityEvent>("SELECT * FROM CityEvent WHERE title LIKE '%" + partialName + "%'", new { });
            return eventList;
        }
       
        public async Task<List<CityEvent>> GetEventListByPriceRangeDate(double precoMin, double precoMax, DateTime date)
        {
            var eventList = await _dbService.GetAll<CityEvent>("SELECT * FROM CityEvent WHERE price >= @precoMin and price <= @precoMax and dateHourEvent = @date", new { precoMin, precoMax, date });
            return eventList;
        }
        /*********************************************/

        //GetEventListByLocalAndDate

        public async Task<List<CityEvent>> GetEventListByLocalAndDate(string local, DateTime date)
        {
            var eventList = await _dbService.GetAll<CityEvent>("SELECT * FROM CityEvent WHERE local >= @local and dateHourEvent = @date", new { local, date });
            return eventList;
        }

        /*********************************************/
        public async Task<CityEvent> GetEvent(int id)
        {
            var eventById = await _dbService.GetAsync<CityEvent>("SELECT * FROM CityEvent WHERE idEvent=@id", new { id });
            return eventById;
        }


        public async Task<CityEvent> UpdateEvent(CityEvent cityEvent)
        {
            var updateEvent =
                await _dbService.EditData(
                    "Update CityEvent SET title=@Title, description=@Description, dateHourEvent=@DateHourEvent, local=@Local, address=@Address, price=@Price, status=@Status  WHERE idEvent=@IdEvent",
                    cityEvent);
            if (updateEvent == 1)
            {
                return cityEvent;
            }
            return null;
        }

        public async Task<bool> DeleteEvent(int id)
        {
            var deleteEvent = await _dbService.EditData("DELETE FROM CityEvent WHERE idEvent=@Id", new { id });
            if (deleteEvent == 1)
            {
                return true;
            }
            return false;
        }
    }
}

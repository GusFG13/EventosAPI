using EventosAPI.Entities;
using EventosAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace EventosAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CityEventController : ControllerBase
    {
        
        private readonly ICityEventService _cityEventService;
        private readonly IEventReservationService _eventReservationService;

        public CityEventController(ICityEventService cityEventService, IEventReservationService eventReservationService)
        {
            _cityEventService = cityEventService;
            _eventReservationService = eventReservationService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _cityEventService.GetEventList();

            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetEvent(int id)
        {
            var result = await _cityEventService.GetEvent(id);

            return Ok(result);
        }


        // Consulta por título, utilizando similaridades, por exemplo, caso pesquise Show, traga todos os eventos que possuem a palavra Show no título;
        [HttpGet("{partialName}")]
        public async Task<IActionResult> GetEventByName(string partialName)
        {
            var result = await _cityEventService.GetEventListByPartialName(partialName);

            return Ok(result);
        }


        // Consulta por range de preço e a data;
        [HttpGet("{precoMin:double}/{precoMax:double}/{date:DateTime}")]
        public async Task<IActionResult> GetEventByName(double precoMin, double precoMax, DateTime date)
        {
            var result = await _cityEventService.GetEventListByPriceRangeDate(precoMin, precoMax, date);

            return Ok(result);
        }


        // Consulta por local e data;

        [HttpGet("{local}/{date:DateTime}")]
        public async Task<IActionResult> GetEventByName(string local, DateTime date)
        {
            var result = await _cityEventService.GetEventListByLocalAndDate(local, date);

            return Ok(result);
        }


        [HttpPost] // Inclusão de um novo evento; *Autenticação e Autorização admin
        public async Task<IActionResult> AddEvent([FromBody] CityEvent cityEvent)
        {
            var result = await  _cityEventService.CreateEvent(cityEvent);
            if (result)
            {
                return Ok(cityEvent);
            }
            return BadRequest($"Não foi possível inserir o evento {cityEvent.Title}");
        }

        [HttpPut] // Edição de um evento existente, filtrando por id; *Autenticação e Autorização admin
        public async Task<IActionResult> UpdateEvent([FromBody] CityEvent cityEvent)
        {
            var result = await _cityEventService.UpdateEvent(cityEvent);

            if (result != null)
            {
                return Ok(result);
            }
            return NotFound(cityEvent.IdEvent);
        }

        [HttpDelete("{id:int}")] // Remoção de um evento, caso o mesmo não possua reservas em andamento, caso possua inative-o; *Autenticação e Autorização admin
        public async Task<IActionResult> DeleteEvent(int id)
        {
            var reservationForEventExist = _eventReservationService.GetReservation(id);
            var result = await _cityEventService.DeleteEvent(id);

            if (result)
            {
                return Ok(result);
            }
            return NotFound(id);
        }
    }
}
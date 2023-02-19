using EventosAPI.Entities;
using EventosAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace EventosAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EventReservationController : ControllerBase
    {
        private readonly IEventReservationService _eventReservationService;
        private readonly ICityEventService _cityEventService;

        public EventReservationController(IEventReservationService eventReservationService, ICityEventService cityEventService)
        {
            _eventReservationService = eventReservationService;
            _cityEventService = cityEventService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _eventReservationService.GetReservationtList();

            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetEvent(int id)
        {
            var result = await _eventReservationService.GetReservation(id);

            return Ok(result);
        }
        /**************************************************/
        //incluir método na service e interface
        [HttpGet("{personName}/{partialEventTitle}")] // Consulta de reserva pelo PersonName e Title do evento, utilizando similaridade para o title; *Autenticação
        public async Task<IActionResult> GetEventByPersonNamePartialTitle(int id)
        {
            var result = await _eventReservationService.GetReservation(id);

            return Ok(result);
        }
        /**************************************************/
        [HttpPost] // Inclusão de uma nova reserva; *Autenticação
        public async Task<IActionResult> AddEvent([FromBody] EventReservation eventReservation)
        {

            var eventExist = _cityEventService.GetEvent(eventReservation.IdEvent);
            if (eventExist != null)
            {
                var result = await _eventReservationService.CreateReservation(eventReservation);
                if (result)
                {
                    return Ok(eventReservation);
                }
                return BadRequest($"Não foi possível inserir a reserva para o evento id {eventReservation.IdEvent}");
            }
            return NotFound(eventReservation.IdEvent);



        }

        [HttpPut] // Edição da quantidade de uma reserva; *Autenticação e Autorização admin
        public async Task<IActionResult> UpdateEvent([FromBody] EventReservation eventReservation)
        {
            var result = await _eventReservationService.UpdateReservation(eventReservation);

            if (result != null)
            {
                return Ok(result);
            }
            return NotFound(eventReservation.IdReservation);
        }

        [HttpDelete("{id:int}")] // Remoção de uma reserva; *Autenticação e Autorização admin
        public async Task<IActionResult> DeleteEvent(int id)
        {
            var result = await _eventReservationService.DeleteReservation(id);

            if (result)
            {
                return Ok(result);
            }
            return NotFound(id);
        }
    }
}

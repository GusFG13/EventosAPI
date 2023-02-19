namespace EventosAPI.Entities
{
    public class EventReservation
    {
        public int IdReservation { get; set; }
        public int IdEvent { get; set; }
        public string PersonName { get; set; }
        public int Quantity { get; set; }
    }
}

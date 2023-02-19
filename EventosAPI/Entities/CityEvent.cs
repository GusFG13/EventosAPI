namespace EventosAPI.Entities
{
    public class CityEvent
    {
        public int IdEvent { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DateHourEvent { get; set; }
        public string Local { get; set; }
        public string Address { get; set; }
        public double Price { get; set; }
        public bool Status { get; set; }
    }
}

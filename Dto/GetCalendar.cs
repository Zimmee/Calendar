namespace Calendar.Dto
{
    public class GetCalendar
    {
        public int? UserId { get; set; }

        public int? Id { get; set; }


        public virtual List<EventDto> Events { get; set; } = new List<EventDto>();
    }
}

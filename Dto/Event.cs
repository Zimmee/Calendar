using System;
using System.Collections.Generic;

namespace Calendar.Dto;

public partial class EventDto
{
    public int? Id { get; set; }
    public string? Name { get; set; }

    public string? Description { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public DateTime? Time { get; set; }

    public int? CalendarId { get; set; }

}

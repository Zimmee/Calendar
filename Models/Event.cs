using System;
using System.Collections.Generic;

namespace Calendar.Models;

public partial class Event
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public DateTime? Time { get; set; }

    public int? CalendarId { get; set; }

    public virtual Calendar? Calendar { get; set; }

    public virtual ICollection<DaysOfWeek> Days { get; } = new List<DaysOfWeek>();
}

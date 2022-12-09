using System;
using System.Collections.Generic;

namespace Calendar.Models;

public partial class DaysOfWeek
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Event> Events { get; } = new List<Event>();
}

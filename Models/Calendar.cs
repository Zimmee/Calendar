using System;
using System.Collections.Generic;

namespace Calendar.Models;

public partial class Calendar
{
    public int Id { get; set; }

    public int? UserId { get; set; }

    public virtual ICollection<Event> Events { get; } = new List<Event>();

    public virtual User? User { get; set; }
}

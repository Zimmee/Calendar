using System;
using System.Collections.Generic;

namespace Calendar.Dto;

public partial class UserDto
{
    public string? Name { get; set; }

    public GetCalendar calendar { get; set; }
}

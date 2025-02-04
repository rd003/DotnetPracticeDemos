using System;
using System.Collections.Generic;

namespace DbFirstDemo.Models;

public partial class Person
{
    public int PersonId { get; set; }

    public string PersonName { get; set; } = null!;

    public string PersonEmail { get; set; } = null!;

    public int? Age { get; set; }

    public string? ContactNumber { get; set; } = null!;
}

using System;
using System.Collections.Generic;

namespace HotelsApi.Entities;

public partial class City
{
    public int CityId { get; set; }

    public string? CityCode { get; set; }

    public string CityName { get; set; } = null!;

    public int StateId { get; set; }

    public virtual ICollection<Barangay> Barangays { get; set; } = new List<Barangay>();

    public virtual State State { get; set; } = null!;
}

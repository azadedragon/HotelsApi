using System;
using System.Collections.Generic;

namespace HotelsApi.Entities;

public partial class Barangay
{
    public int BarangayId { get; set; }

    public string BarangayName { get; set; } = null!;

    public string PostalCode { get; set; } = null!;

    public int CityId { get; set; }

    public virtual City City { get; set; } = null!;

    public virtual ICollection<Hotel> Hotels { get; set; } = new List<Hotel>();
}

using System;
using System.Collections.Generic;

namespace HotelsApi.Entities;

public partial class Hotel
{
    public int HotelId { get; set; }

    public string HotelCode { get; set; } = null!;

    public string HotelName { get; set; } = null!;

    public string HotelDescription { get; set; } = null!;
}

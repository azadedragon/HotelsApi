using System;
using System.Collections.Generic;

namespace HotelsApi.Entities;

public partial class State
{
    public int StateId { get; set; }

    public string? StateCode { get; set; }

    public string StateName { get; set; } = null!;
}

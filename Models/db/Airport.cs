using System;
using System.Collections.Generic;

namespace Final_2252.Models.db;

public partial class Airport
{
    public int AirportId { get; set; }

    public string AirportName { get; set; } = null!;

    public string AirportCode { get; set; } = null!;

    public string Address { get; set; } = null!;

    public virtual ICollection<Flight> FlightAirportDestinationNavigations { get; set; } = new List<Flight>();

    public virtual ICollection<Flight> FlightAirportSourceNavigations { get; set; } = new List<Flight>();
}

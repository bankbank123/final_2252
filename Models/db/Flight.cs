using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Final_2252.Models.db;

public partial class Flight
{
    public int FlightId { get; set; }
    [DisplayName("Flight No.")]
    public string FlightNo { get; set; } = null!;

    public int AirportSource { get; set; }

    public int AirportDestination { get; set; }

    public string FirstName { get; set; } = null!;

    public string? MiddleName { get; set; }

    public string LastName { get; set; } = null!;

    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}", ApplyFormatInEditMode = true)]
    public DateTime DepartDate { get; set; }

    [DataType(DataType.DateTime)]
    [DisplayFormat(DataFormatString = "{0:dd MMM yyyy HH:mm}", ApplyFormatInEditMode = true)]
    public DateTime BoardingTime { get; set; }

    public string? Gate { get; set; }

    public string Zone { get; set; } = null!;

    public string Seat { get; set; } = null!;

    public string Seq { get; set; } = null!;

    public virtual Airport AirportDestinationNavigation { get; set; } = null!;

    public virtual Airport AirportSourceNavigation { get; set; } = null!;
}

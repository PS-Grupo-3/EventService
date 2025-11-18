using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;

public class EventSeat
{
    public Guid EventSeatId { get; set; }
    public Guid EventId { get; set; }
    public Guid EventSectorId { get; set; }

    public int Row { get; set; }
    public int Column { get; set; }
    public int PosX { get; set; }
    public int PosY { get; set; }

    public decimal Price { get; set; } = 100;
    public bool Available { get; set; } = true;

    public EventSector EventSector { get; set; } = null!;
    public Event Event { get; set; } = null!;
}


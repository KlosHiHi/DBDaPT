using System;
using System.Collections.Generic;

namespace Lections1013;

public partial class CinemaHall
{
    public byte HallId { get; set; }

    public string CinemaName { get; set; } = null!;

    public byte RowsCount { get; set; }

    public byte SeatsCount { get; set; }

    public bool IsVip { get; set; }

    public virtual ICollection<Session> Sessions { get; set; } = new List<Session>();
}

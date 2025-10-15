using System;
using System.Collections.Generic;

namespace LabWork11.Model;

public partial class GamePrice
{
    public int GameId { get; set; }

    public decimal OldPrice { get; set; }

    public DateTime ChangingDate { get; set; }

    public virtual Game Game { get; set; } = null!;
}

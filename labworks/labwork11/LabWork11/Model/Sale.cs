using System;
using System.Collections.Generic;

namespace LabWork11.Model;

public partial class Sale
{
    public int SaleId { get; set; }

    public int GameId { get; set; }

    public short KeysAmount { get; set; }

    public DateTime SaleDate { get; set; }

    public virtual Game Game { get; set; } = null!;
}

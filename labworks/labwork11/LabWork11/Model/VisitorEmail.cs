using System;
using System.Collections.Generic;

namespace LabWork11.Model;

public partial class VisitorEmail
{
    public int VisitorId { get; set; }

    public string OldEmail { get; set; } = null!;

    public DateTime ChangeDate { get; set; }

    public virtual Visitor Visitor { get; set; } = null!;
}

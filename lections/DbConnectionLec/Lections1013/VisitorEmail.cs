using System;
using System.Collections.Generic;

namespace Lections1013;

public partial class VisitorEmail
{
    public int VisitorId { get; set; }

    public string OldEmail { get; set; } = null!;

    public DateTime ChangeDate { get; set; }

    public virtual Visitor Visitor { get; set; } = null!;
}

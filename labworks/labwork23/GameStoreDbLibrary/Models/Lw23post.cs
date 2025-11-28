using System;
using System.Collections.Generic;

namespace GameStoreDbLibrary.Models;

public partial class Lw23post
{
    public int PostId { get; set; }

    public int UserId { get; set; }

    public string Title { get; set; } = null!;

    public virtual ICollection<Lw23comment> Lw23comments { get; set; } = new List<Lw23comment>();
}

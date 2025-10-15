﻿using System;
using System.Collections.Generic;

namespace LabWork11.Model;

public partial class Film
{
    public int FilmId { get; set; }

    public string Name { get; set; } = null!;

    public short Duration { get; set; }

    public short ReleaseYear { get; set; }

    public string? Description { get; set; }

    public byte[]? Poster { get; set; }

    public string? AgeLimit { get; set; }

    public DateOnly? RentalStart { get; set; }

    public DateOnly? RentalFinish { get; set; }

    public bool IsDeleted { get; set; }

    public virtual ICollection<Session> Sessions { get; set; } = new List<Session>();

    public virtual ICollection<Genre> Genres { get; set; } = new List<Genre>();
}

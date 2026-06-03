using System;
using System.Collections.Generic;

namespace MyLearning.Data.Sqlite.Movieland;

public partial class Movie
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string? PosterUrl { get; set; }

    public DateOnly? ReleaseDate { get; set; }

    public int? Revenue { get; set; }

    public int? Runtime { get; set; }

    public string? Overview { get; set; }
}

using System;
using System.Collections.Generic;

namespace MyLearning.Data.Sqlite.Movieland;

public partial class UserRating
{
    public int UserId { get; set; }

    public int? MovieId { get; set; }

    public int Rating { get; set; }

    public string? AddedAt { get; set; }
}

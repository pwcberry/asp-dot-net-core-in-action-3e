using System;
using System.Collections.Generic;

namespace MyLearning.Data.Sqlite.Movieland;

public partial class UserFavourite
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int MovieId { get; set; }

    public string? AddedAt { get; set; }
}

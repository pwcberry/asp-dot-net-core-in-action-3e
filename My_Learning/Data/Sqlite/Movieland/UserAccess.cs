using System;
using System.Collections.Generic;

namespace MyLearning.Data.Sqlite.Movieland;

public partial class UserAccess
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int SessionStartedAt { get; set; }

    public int? SessionEndedAt { get; set; }
}

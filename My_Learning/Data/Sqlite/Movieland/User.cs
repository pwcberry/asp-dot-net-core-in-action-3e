using System;
using System.Collections.Generic;

namespace MyLearning.Data.Sqlite.Movieland;

public partial class User
{
    public int Id { get; set; }

    public string? Email { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? Handle { get; set; }

    public int? Disabled { get; set; }

    public string? AddedAt { get; set; }

    public string? UpdatedAt { get; set; }
}

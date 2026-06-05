/// <summary>
/// This class and associated entities were generated using the Entity Framework Core scaffolding tool, based on the Movieland SQLite database generated from this project's scripts.
/// 
/// The command to run the tool was:
/// `dotnet ef dbcontext scaffold "Data Source=<fullpath>\Movieland.db" Microsoft.EntityFrameworkCore.Sqlite --no-pluralize -c MovielandContext --namespace MyLearning.Data.Sqlite.Movieland --context-dir "$(pwd)\Sqlite"  --output-dir "$(pwd)\Sqlite\Carsales"`
/// 
/// The context class' namespace is then changed to MyLearning.Data.Sqlite.
/// </summary>
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MyLearning.Data.Sqlite.Movieland;

namespace MyLearning.Data.Sqlite;

public partial class MovielandContext : DbContext
{
    private readonly string connectionString;

    public MovielandContext(IConfiguration configuration)
    {
        connectionString = configuration.GetSqliteConnection("MovielandConnection") ?? throw new InvalidOperationException("Connection string not found.");
    }

    public MovielandContext(DbContextOptions<MovielandContext> options, IConfiguration configuration)
        : base(options)
    {
        connectionString = configuration.GetSqliteConnection("MovielandConnection") ?? throw new InvalidOperationException("Connection string not found.");
    }

    public virtual DbSet<Genre> Genre { get; set; }

    public virtual DbSet<Movie> Movie { get; set; }

    public virtual DbSet<MovieGenre> MovieGenre { get; set; }

    public virtual DbSet<User> User { get; set; }

    public virtual DbSet<UserAccess> UserAccess { get; set; }

    public virtual DbSet<UserFavourite> UserFavourite { get; set; }

    public virtual DbSet<UserRating> UserRating { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseSqlite(connectionString);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Genre>(entity =>
        {
            entity.ToTable("genre");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Name).HasColumnName("name");
        });

        modelBuilder.Entity<Movie>(entity =>
        {
            entity.ToTable("movie");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Overview).HasColumnName("overview");
            entity.Property(e => e.PosterUrl).HasColumnName("poster_url");
            entity.Property(e => e.ReleaseDate).HasColumnName("release_date");
            entity.Property(e => e.Revenue).HasColumnName("revenue");
            entity.Property(e => e.Runtime).HasColumnName("runtime");
            entity.Property(e => e.Title).HasColumnName("title");
        });

        modelBuilder.Entity<MovieGenre>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("movie_genre");

            entity.Property(e => e.GenreId).HasColumnName("genre_id");
            entity.Property(e => e.MovieId).HasColumnName("movie_id");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("user");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.AddedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("added_at");
            entity.Property(e => e.Disabled).HasColumnName("disabled");
            entity.Property(e => e.Email).HasColumnName("email");
            entity.Property(e => e.FirstName).HasColumnName("first_name");
            entity.Property(e => e.Handle).HasColumnName("handle");
            entity.Property(e => e.LastName).HasColumnName("last_name");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("updated_at");
        });

        modelBuilder.Entity<UserAccess>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.ToTable("user_access");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.SessionEndedAt).HasColumnName("session_ended_at");
            entity.Property(e => e.SessionStartedAt).HasColumnName("session_started_at");
            entity.Property(e => e.UserId).HasColumnName("user_id");
        });

        modelBuilder.Entity<UserFavourite>(entity =>
        {
            entity.HasKey(e => e.UserId);

            entity.ToTable("user_favourite");

            entity.Property(e => e.Id)
                .HasColumnName("id");
            entity.Property(e => e.UserId)
                .ValueGeneratedNever()
                .HasColumnName("user_id");
            entity.Property(e => e.AddedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("added_at");
            entity.Property(e => e.MovieId).HasColumnName("movie_id");
        });

        modelBuilder.Entity<UserRating>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.ToTable("user_rating");

            entity.Property(e => e.Id)
                .HasColumnName("id");
            entity.Property(e => e.AddedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("added_at");
            entity.Property(e => e.MovieId).HasColumnName("movie_id");
            entity.Property(e => e.Rating).HasColumnName("rating");
            entity.Property(e => e.UserId).HasColumnName("user_id");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

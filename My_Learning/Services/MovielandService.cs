using MyLearning.Data.Sqlite;
using MyLearning.Data.Sqlite.Movieland;

namespace MyLearning.Services
{
    public class MovielandService(MovielandContext context)
    {
        public bool AddFavourite(int userId, int movieId)
        {
            context.UserFavourite.Add(new UserFavourite { UserId = userId, MovieId = movieId });
            context.SaveChanges();

            // TODO: What happens when there is an error? We should handle that, but for now we will just return true.
            return true;
        }

        public bool AddRating(int userId, int movieId, int rating)
        {
            context.UserRating.Add(new UserRating
            {
                UserId = userId,
                MovieId = movieId,
                Rating = rating
            });
            context.SaveChanges();

            // TODO: What happens when there is an error? We should handle that, but for now we will just return true.
            return true;
        }

        public Movie? GetMovie(int movieId)
        {
            return context.Movie.FirstOrDefault(m => m.Id == movieId);
        }
    }

    namespace Inputs
    {
        public record MovieRating(int MovieId, int Rating);
    }
}

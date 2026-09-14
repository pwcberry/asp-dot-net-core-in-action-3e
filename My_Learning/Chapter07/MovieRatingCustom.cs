namespace MyLearning.Chapter07
{
    public record MovieRatingCustom(int UserId, int MovieId, int Rating)
    {
        public static async ValueTask<MovieRatingCustom?> BindAsync(HttpContext context)
        {
            using var reader = new StreamReader(context.Request.Body);

            var line1 = await reader.ReadLineAsync(context.RequestAborted);
            if (line1 is null || !line1.StartsWith("user:"))
                return null;

            var line2 = await reader.ReadLineAsync(context.RequestAborted);
            if (line2 is null || !line2.StartsWith("movie:"))
                return null;

            var line3 = await reader.ReadLineAsync(context.RequestAborted);
            if (line3 is null || !line3.StartsWith("rating:"))
                return null;

            if (!int.TryParse(line1.AsSpan(5), out var userIdValue))
                return null;

            if (!int.TryParse(line2.AsSpan(6), out var movieIdValue))
                return null;

            return int.TryParse(line3.AsSpan(7), out var ratingValue) ?
                new MovieRatingCustom(userIdValue, movieIdValue, ratingValue) : null;
        }
    }
}
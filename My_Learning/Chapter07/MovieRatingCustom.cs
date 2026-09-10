namespace MyLearning.Chapter07
{
    public record MovieRatingCustom(int UserId, int MovieId, int Rating)
    {
        public static async ValueTask<MovieRatingCustom?> BindAsync(HttpContext context)
        {
            using var reader = new StreamReader(context.Request.Body);

            string? line1 = await reader.ReadLineAsync(context.RequestAborted);
            if (line1 is null || !line1.StartsWith("user:"))
                return null;

            string? line2 = await reader.ReadLineAsync(context.RequestAborted);
            if (line2 is null || !line2.StartsWith("movie:"))
                return null;

            string? line3 = await reader.ReadLineAsync(context.RequestAborted);
            if (line3 is null || !line3.StartsWith("rating:"))
                return null;

            if (!int.TryParse(line1.Substring("user:".Length), out var userIdValue))
                return null;

            if (!int.TryParse(line2.Substring("movie:".Length), out var movieIdValue))
                return null;

            if (!int.TryParse(line3.Substring("rating:".Length), out var ratingValue))
                return null;

            return new MovieRatingCustom(userIdValue, movieIdValue, ratingValue);
        }
    }
}

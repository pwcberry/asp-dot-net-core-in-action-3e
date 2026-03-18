namespace MyLearning.Chapter05
{
    public static class Routes
    {
        public static void MapTrackRoutesSimply(this WebApplication app, Service service)
        {
            app.MapGet("/track/{id}", (int id) =>
            {
                var data = service.GetTrack(id);
                return data is not null ? Results.Ok(data) : Results.NotFound();
            });

            app.MapPost("/track", (Track track) =>
            {
                var id = service.AddTrack(track);
                return Results.Created($"/track/{id}", track);
            });

            app.MapPut("/track/{id}", (int id, Track track) =>
            {
                bool result = service.UpdateTrack(id, track);
                return result ? Results.NoContent() : Results.NotFound();
            });

            app.MapDelete("/track/{id}", (int id) =>
            {
                bool result = service.DeleteTrack(id);
                return result ? Results.NoContent() : Results.NotFound();
            });
        }

        public static void MapTrackRouteGroup(this WebApplication app, Service service)
        {
            RouteGroupBuilder trackApi = app.MapGroup("/track");

            trackApi.MapGet("/{id}", (int id) =>
            {
                var data = service.GetTrack(id);
                return data is not null ? Results.Ok(data) : Results.NotFound();
            });

            trackApi.MapPost("/", (Track track) =>
            {
                var id = service.AddTrack(track);
                return Results.Created($"/track/{id}", track);
            });

            trackApi.MapPut("/{id}", (int id, Track track) =>
            {
                bool result = service.UpdateTrack(id, track);
                return result ? Results.NoContent() : Results.NotFound();
            });

            trackApi.MapDelete("/{id}", (int id) =>
            {
                bool result = service.DeleteTrack(id);
                return result ? Results.NoContent() : Results.NotFound();
            });
        }
    }
}
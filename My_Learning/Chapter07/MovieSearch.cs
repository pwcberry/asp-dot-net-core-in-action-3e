using Microsoft.AspNetCore.Mvc;
        
namespace MyLearning.Chapter07;

public record MovieSearch([FromQuery(Name = "q")] string Query, [FromQuery(Name = "page")] int Page = 1);

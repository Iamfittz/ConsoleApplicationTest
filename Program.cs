using System.Text.Json;
using ConsoleApplicationTest.Models;

HttpClient httpClient = new HttpClient();

var usersJsonData = await httpClient.GetStringAsync("https://jsonplaceholder.typicode.com/users");
var postsJsonData = await httpClient.GetStringAsync("https://jsonplaceholder.typicode.com/posts");

var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

var usersRAM = JsonSerializer.Deserialize<List<User>>(usersJsonData, options)!;
var postsRAM = JsonSerializer.Deserialize<List<Post>>(postsJsonData, options)!;

var filteredUsersByS = usersRAM
    .Where(u => u.Address.City.StartsWith("S", StringComparison.OrdinalIgnoreCase))
    .ToList();
var sortedUsersByPost = usersRAM
    .OrderByDescending(u => postsRAM.Count(m => m.UserId == u.Id))
    .ToList();
var topUserData = usersRAM
    .Select(u => new {
        User = u,
        PostCount = postsRAM.Count(p => p.UserId == u.Id)
    })
    .OrderByDescending(x => x.PostCount)
    .First();
var topThreeUsers = usersRAM
    .Select(u => new {
        Name = u.Name,
        PostCount = postsRAM.Count(p => p.UserId == u.Id)
    })
    .OrderByDescending(x => x.PostCount)
    .Take(3)
    .ToList();


Console.WriteLine("Extract people who's city Starting with S");
foreach (var user in filteredUsersByS) {
    var userPosts = postsRAM
        .Where(p => p.UserId == user.Id)
        .ToList();

    Console.WriteLine($"Name: {user.Name}");
    Console.WriteLine($"City: {user.Address.City}");
    
    Console.WriteLine($"Posts count: {userPosts.Count}");
    Console.WriteLine(new string('-', 22));
}
Console.WriteLine("Extract people sorted by Post Count");
foreach (var user in sortedUsersByPost) {
    Console.WriteLine($"{user.Name}");
    Console.WriteLine($"City: {user.Address.City}");
    Console.WriteLine(new string('-', 22));
}
Console.WriteLine(new string('-', 22));
//Extract 1 person who has the most posts
Console.WriteLine($"Top user: {topUserData.User.Name} with {topUserData.PostCount} posts");

Console.WriteLine(new string('-', 22));
Console.WriteLine("Extract top 3 users by post cout");
foreach (var user in topThreeUsers) {
    Console.WriteLine($"{user.Name,-25} {user.PostCount} posts");
}

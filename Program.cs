using System.Text.Json;
using ConsoleApplicationTest.Models;

HttpClient httpClient = new HttpClient();

var usersJsonData = await httpClient.GetStringAsync("https://jsonplaceholder.typicode.com/users");
var postsJsonData = await httpClient.GetStringAsync("https://jsonplaceholder.typicode.com/posts");

var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

var usersRAM = JsonSerializer.Deserialize<List<User>>(usersJsonData, options)!;
var postsRAM = JsonSerializer.Deserialize<List<Post>>(postsJsonData, options)!;

var filteredUsers = usersRAM
    .Where(u => u.Address.City.StartsWith("S", StringComparison.OrdinalIgnoreCase))
    .ToList();
var sortedUsersByPost = usersRAM
    .OrderByDescending(u => postsRAM.Count(m => m.UserId == u.Id))
    .ToList();

foreach (var user in filteredUsers) {
    var userPosts = postsRAM
        .Where(p => p.UserId == user.Id)
        .ToList();

    Console.WriteLine($"Name: {user.Name}");
    Console.WriteLine($"City: {user.Address.City}");
    
    Console.WriteLine($"Posts count: {userPosts.Count}");
    Console.WriteLine(new string('-', 22));
}
foreach (var user in sortedUsersByPost) {
    Console.WriteLine($"{user.Name}");
    Console.WriteLine($"City: {user.Address.City}");
    Console.WriteLine(new string('-', 22));
}
var topUser = usersRAM
    .OrderByDescending(u => postsRAM.Count(p => p.UserId == u.Id))
    .First();
Console.WriteLine($"The top user by Post is : {topUser.Name}");
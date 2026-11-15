using System;
using System.Collections.Generic;

class Post
{
    public string Date { get; set; }
    public string Text { get; set; }
    public int Likes { get; set; }
    public List<string> Comments { get; set; } = new List<string>();
}

class User
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Nickname { get; set; }
    public List<Post> Posts { get; set; } = new List<Post>();

    public void GetLast5Posts(User otherUser)
    {
        Console.WriteLine("Останні пости користувача: {otherUser.Nickname}");
        
        if (otherUser.Posts.Count == 0)
        {
            Console.WriteLine("У цього користувача поки немає постів.");
            return;
        }

        int count = Math.Min(5, otherUser.Posts.Count);
        for (int i = otherUser.Posts.Count - count; i < otherUser.Posts.Count; i++)
        {
            Post p = otherUser.Posts[i];
            Console.WriteLine($"Дата: {p.Date}  Лайки: {p.Likes}  Коментарі: {p.Comments.Count}");
            Console.WriteLine($"Текст: {p.Text}");
            if (p.Comments.Count > 0)
            {
                Console.WriteLine("Коментарі:");
                foreach (var c in p.Comments)
                {
                    Console.WriteLine($"    - {c}");
                }
            }
            Console.WriteLine(new string('-', 30));
        }
    }

    public void LikePost(Post post)
    {
        post.Likes++;
        Console.WriteLine($"{Nickname} Лайків: {post.Likes}");
    }

    public void AddComment(Post post, string commentText)
    {
        post.Comments.Add(commentText);
        Console.WriteLine($"{Nickname} : \"{commentText}\"");
    }

    public void CreatePost(string text)
    {
        Post newPost = new Post
        {
            Date = DateTime.Now.ToString("dd.MM.yyyy HH:mm"),
            Text = text,
            Likes = 0
        };
        Posts.Add(newPost);
        Console.WriteLine($"{Nickname} створив новий пост!");
    }

    public void DeletePost(int index)
    {
        if (index >= 0 && index < Posts.Count)
        {
            Posts.RemoveAt(index);
            Console.WriteLine($"{Nickname} видалив свій пост під номером {index + 1}.");
        }
        else
        {
            Console.WriteLine("Пост з таким номером не знайдено!");
        }
    }
}

class Program
{
    static void Main()
    {
        List<User> users = new List<User>()
        {
            new User { Name = "Олександр", Email = "alex@gmail.com", Nickname = "@alex22" },
            new User { Name = "Марія", Email = "mariya@gmail.com", Nickname = "@mary33" }
        };

        users[1].Posts.Add(new Post { Date = "10.11.2026 12:00", Text = "Привіт, це мій перший пост!", Likes = 2 });
        users[1].Posts.Add(new Post { Date = "11.11.2026 15:30", Text = "Сьогодні чудова погода для прогулянки.", Likes = 5 });


        users[0].CreatePost("Вітаю всіх з новим роком!");

        users[0].GetLast5Posts(users[1]);

        users[0].LikePost(users[1].Posts[0]);
        users[0].AddComment(users[1].Posts[0], "Вітаю з новим роком!");

        users[1].DeletePost(0);

        users[0].GetLast5Posts(users[1]);
    }
}
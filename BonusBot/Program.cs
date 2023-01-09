using BonusBot.Abstractions;
using BonusBot.Core;
using BonusBot.Core.Data;

public class Program
{
    
    static async Task Main()
    {
        string token = File.ReadAllText("token.txt");
        IBot bot = new Bot(token);
        await bot.Start();
        Console.ReadLine();
    }
}
using BonusBot.Abstractions;
using BonusBot.Core;
using BonusBot.Core.Data;

public class Program
{
    
    static async Task Main(string[] args)
    {
        if (args.Length == 0)
        {
            Console.WriteLine("Enter token as argument, Example dotnet BonusBot.dll 123456:asdhsadsadhkjhwejquiahdak");
            return;
        }
        string token = args[0];
        IBot bot = new Bot(token);
        await bot.Start();
        Console.ReadLine();
    }
}
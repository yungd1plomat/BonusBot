using BonusBot.Abstractions;
using BonusBot.Core.Data;
using BonusBot.Entities;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace BonusBot.Core.Handlers;

public class AddNameHandler : IHandler
{
    public ITelegramBotClient Client { get; set; }

    public AddNameHandler(ITelegramBotClient client)
    {
        Client = client;
    }
    
    public bool IsMatch(Message message, TGUser user, AppDb db)
    {
        return user.LastCommand == Command.InitAddBonus && user.IsAdmin;
    }

    public async Task Handle(Message message, TGUser user, AppDb db)
    {
        var name = message.Text;
        var bonus = new Bonus()
        {
            Name = name,
        };
        await db.Bonuses.AddAsync(bonus);
        await db.SaveChangesAsync();
        user.Data = bonus.Id;
        await Client.SendTextMessageAsync(user.ChatId, "Введите описание бонуса");
    }
}
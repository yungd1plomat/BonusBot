using BonusBot.Abstractions;
using BonusBot.Core.Data;
using BonusBot.Entities;
using Microsoft.EntityFrameworkCore;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace BonusBot.Core.Handlers;

public class EditBmHandler : IHandler
{
    public ITelegramBotClient Client { get; set; }

    public EditBmHandler(ITelegramBotClient client)
    {
        Client = client;
    }
    
    public bool IsMatch(Message message, TGUser user, AppDb db)
    {
        return user.LastCommand == Command.SelectEditBm;
    }

    public async Task Handle(Message message, TGUser user, AppDb db)
    {
        var Id = user.Data;
        var newName = message.Text;
        var bookmaker = await db.Bookmakers.FirstOrDefaultAsync(x => x.Id == Id);
        await Client.SendTextMessageAsync(user.ChatId, $"Название измененно {bookmaker.Name} --> {newName}");
        bookmaker.Name = newName;
    }
}
using BonusBot.Abstractions;
using BonusBot.Core.Data;
using BonusBot.Entities;
using Microsoft.EntityFrameworkCore;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace BonusBot.Core.Handlers;

public class EditLinkHandler : IHandler
{
    public ITelegramBotClient Client { get; set; }

    public EditLinkHandler(ITelegramBotClient client)
    {
        Client = client;
    }

    public bool IsMatch(Message message, TGUser user, AppDb db)
    {
        return user.LastCommand == Command.EditDescription && user.IsAdmin;
    }

    public async Task Handle(Message message, TGUser user, AppDb db)
    {
        var newLink = message.Text;
        await Client.SendTextMessageAsync(user.ChatId, "Бонус успешно отредактирован");
        if (newLink == "none")
            return;
        var bonus = await db.Bonuses.FirstOrDefaultAsync(x => x.Id == user.Data);
        bonus.Link = newLink;
        user.Data = null;
    }
}
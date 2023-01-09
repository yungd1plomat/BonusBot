using BonusBot.Abstractions;
using BonusBot.Core.Data;
using BonusBot.Entities;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace BonusBot.Core.Handlers.Admin;

public class CancelHandler : IHandler
{
    public ITelegramBotClient Client { get; set; }

    public CancelHandler(ITelegramBotClient client)
    {
        Client = client;
    }
    
    public bool IsMatch(Message message, TGUser user, AppDb db)
    {
        return message.Text == "Отмена";
    }

    public async Task Handle(Message message, TGUser user, AppDb db)
    {
        user.Data = null;
        await Client.SendTextMessageAsync(user.ChatId, "Действие отменено");
    }
}
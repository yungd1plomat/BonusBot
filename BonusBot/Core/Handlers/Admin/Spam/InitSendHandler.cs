using BonusBot.Abstractions;
using BonusBot.Core.Data;
using BonusBot.Entities;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace BonusBot.Core.Handlers.Admin.Spam;

public class InitSendHandler : IHandler
{
    public ITelegramBotClient Client { get; set; }

    public InitSendHandler(ITelegramBotClient client)
    {
        Client = client;
    }
    
    public bool IsMatch(Message message, TGUser user, AppDb db)
    {
        return message.Text == "Рассылка" && user.IsAdmin;
    }

    public async Task Handle(Message message, TGUser user, AppDb db)
    {
        await Client.SendTextMessageAsync(user.ChatId, "Перешлите сообщение для рассылки");
    }
}
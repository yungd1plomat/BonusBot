using BonusBot.Abstractions;
using BonusBot.Core.Data;
using BonusBot.Entities;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace BonusBot.Core.Handlers.Admin.Spam;

public class SendHandler : IHandler
{
    public ITelegramBotClient Client { get; set; }

    public SendHandler(ITelegramBotClient client)
    {
        Client = client;
    }
    
    public bool IsMatch(Message message, TGUser user, AppDb db)
    {
        return user.LastCommand == Command.InitSend && user.IsAdmin;
    }

    private async Task SendMessage(IEnumerable<TGUser> users, long fromId, int messageId)
    {
        foreach (var tgUser in users)
        {
            await Client.ForwardMessageAsync(tgUser.ChatId, fromId, messageId, false, true);
            await Task.Delay(100);
        }
    }
    
    public async Task Handle(Message message, TGUser user, AppDb db)
    {
        var users = db.Users.ToList();
        await Task.Factory.StartNew(async () => await SendMessage(users, message.From.Id, message.MessageId), TaskCreationOptions.LongRunning);
        await Client.SendTextMessageAsync(user.ChatId, "Рассылка запущена!");
    }
}
using BonusBot.Abstractions;
using BonusBot.Core.Data;
using BonusBot.Core.Utils;
using BonusBot.Entities;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace BonusBot.Core.Handlers.Admin;

public class AdminHandler: IHandler
{
    public ITelegramBotClient Client { get; set; }

    public AdminHandler(ITelegramBotClient client)
    {
        Client = client;
    }
    
    public bool IsMatch(Message message, TGUser user, AppDb db)
    {
        return message.Text == "/admin" && user.IsAdmin;
    }

    public async Task Handle(Message message, TGUser user, AppDb db)
    {
        await Client.SendTextMessageAsync(user.ChatId, "Выдано админское меню", replyMarkup: KeyboardManager.AdminButtons);
    }
}
using BonusBot.Abstractions;
using BonusBot.Core.Data;
using BonusBot.Entities;
using Microsoft.EntityFrameworkCore;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace BonusBot.Core.Handlers.Admin;

public class AddAdminHandler : IHandler
{
    public ITelegramBotClient Client { get; set; }

    public AddAdminHandler(ITelegramBotClient client)
    {
        Client = client;
    }
    
    public bool IsMatch(Message message, TGUser user, AppDb db)
    {
        return (message.Text.StartsWith("/addadmin") ||
               message.Text.StartsWith("/removeadmin")) &&
               user.IsAdmin;
    }

    public async Task Handle(Message message, TGUser user, AppDb db)
    {
        bool isAddAdmin = message.Text.StartsWith("/addadmin");
        var rawId = message.Text.Replace("/addadmin", "")
                                      .Replace("/removeadmin", "");
        if (!long.TryParse(rawId, out long id))
        {
            await Client.SendTextMessageAsync(user.ChatId, "Некорректный Id");
            throw new InvalidOperationException("Invalid id");
        }
        var needUser = await db.Users.FirstOrDefaultAsync(x => x.ChatId == id);
        if (needUser == default)
        {
            await Client.SendTextMessageAsync(user.ChatId, "Пользователь не найден");
            throw new InvalidOperationException("User not found");
        }
        needUser.IsAdmin = isAddAdmin;
        await Client.SendTextMessageAsync(user.ChatId, $"Пользователь {rawId} админ: {isAddAdmin}");
        await Client.SendTextMessageAsync(needUser.ChatId, $"Права администратора: {isAddAdmin}");
    }
}
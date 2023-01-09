using BonusBot.Abstractions;
using BonusBot.Core.Data;
using BonusBot.Entities;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace BonusBot.Core.Handlers;

public class InitCountryHandler : IHandler
{
    public ITelegramBotClient Client { get; set; }

    public InitCountryHandler(ITelegramBotClient client)
    {
        Client = client;
    }
    
    public bool IsMatch(Message message, TGUser user, AppDb db)
    {
        return message.Text == "Добавить страну" && user.IsAdmin;
    }

    public async Task Handle(Message message, TGUser user, AppDb db)
    {
        await Client.SendTextMessageAsync(user.ChatId, "Введите название страны для добавления");
        user.Data = null;
    }
}
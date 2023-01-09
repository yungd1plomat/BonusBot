using System.Text;
using BonusBot.Abstractions;
using BonusBot.Core.Data;
using BonusBot.Entities;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace BonusBot.Core.Handlers;

public class InitRemoveCountryHandler : IHandler
{
    public ITelegramBotClient Client { get; set; }

    public InitRemoveCountryHandler(ITelegramBotClient client)
    {
        Client = client;
    }
    
    public bool IsMatch(Message message, TGUser user, AppDb db)
    {
        return message.Text == "Удалить страну";
    }

    public async Task Handle(Message message, TGUser user, AppDb db)
    {
        StringBuilder sb = new StringBuilder();
        var countries = db.Countries.ToList();
        sb.AppendLine("Выберите ID страны для удаления:");
        sb.AppendLine();
        foreach (var country in countries)
        {
            sb.AppendLine($"{country.Id} - {country.Name}");
        }
        await Client.SendTextMessageAsync(user.ChatId, sb.ToString());
    }
}
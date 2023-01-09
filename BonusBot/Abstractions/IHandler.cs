using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BonusBot.Core.Data;
using BonusBot.Entities;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace BonusBot.Abstractions
{
    /// <summary>
    /// Обработчик определенной команды
    /// </summary>
    public interface IHandler
    {
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        ITelegramBotClient Client { get; set; }

        /// <summary>
        /// Проверяем соответствует ли объект сообщения данной команде
        /// </summary>
        /// <param name="update"></param>
        bool IsMatch(Message message, TGUser user, AppDb db);

        /// <summary>
        /// Обрабатываем данное сообщение (команду)
        /// </summary>
        /// <param name="update"></param>
        /// <param name="user"></param>
        Task Handle(Message message, TGUser user, AppDb db);
    }
}

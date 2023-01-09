using BonusBot.Entities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace BonusBot.Abstractions
{
    public interface IBot
    {
        /// <summary>
        /// Обработчики событий
        /// </summary>
        IDictionary<Command, IHandler> Handlers { get; set; }

        /// <summary>
        /// Запуск "пуллинга" сервера телеграмма
        /// </summary>
        /// <returns></returns>
        Task Start();

        /// <summary>
        /// Обрабатывает все события
        /// </summary>
        /// <param name="botClient"></param>
        /// <param name="update"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken);

        /// <summary>
        /// Обрабатывает ошибки, возникающие при поллинге
        /// </summary>
        /// <param name="botClient"></param>
        /// <param name="exception"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken);

        /// <summary>
        /// Зарегистрировать пользователя (локально)
        /// </summary>
        /// <param name="chatId"></param>
        /// <returns></returns>
        Task<TGUser> Register(long chatId);
    }
}

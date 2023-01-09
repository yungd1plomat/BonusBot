using BonusBot.Abstractions;
using BonusBot.Core.Utils;
using BonusBot.Entities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BonusBot.Core.Data;
using BonusBot.Core.Handlers;
using BonusBot.Core.Handlers.Admin;
using BonusBot.Core.Handlers.Admin.Country;
using BonusBot.Core.Handlers.Admin.Spam;
using Microsoft.EntityFrameworkCore;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;

namespace BonusBot.Core
{
    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public class Bot : IBot
    {
        /// <inheritdoc/>
        public IDictionary<Command, IHandler> Handlers { get; set; }

        /// <inheritdoc/>
        private ITelegramBotClient _client { get; set; }
        
        private AppDb _database { get; }

        public Bot(string token)
        {
            _database = new AppDb();
            _client = new TelegramBotClient(token);
            Handlers = new Dictionary<Command, IHandler>()
            {
                { Command.Cancel, new CancelHandler(_client) },
                { Command.Admin, new AdminHandler(_client) },

                { Command.InitAddCountry, new InitCountryHandler(_client) },
                { Command.AddCountry, new AddCountryHandler(_client) },
                { Command.EditCountry, new EditCountryHandler(_client) },
                { Command.SelectCountry, new SelectCountryHandler(_client) },
                { Command.InitEditCountry, new InitEditCountryHandler(_client) },
                { Command.InitRemoveCountry, new InitRemoveCountryHandler(_client) },
                { Command.RemoveCountry, new RemoveCountryHandler(_client) },
                
                { Command.InitAddBookmaker, new InitAddBmHandler(_client) },
                { Command.InitEditBookmaker, new InitEditBmHandler(_client) },
                { Command.InitRemoveBookmaker, new InitRemoveBmHandler(_client) },
                { Command.SelectBmCountry, new SelectBmCountryHandler(_client) },
                { Command.SelectEditBm, new SelectEditBmHandler(_client) },
                { Command.EditBookmaker, new EditBmHandler(_client) },
                { Command.AddBookmaker, new AddBmHandler(_client) },
                { Command.RemoveBookmaker, new RemoveBmHandler(_client) },
                
                { Command.InitAddBonus, new InitAddBonusHandler(_client) },
                { Command.EditName, new EditNameHandler(_client) },
                { Command.InitEditBonus, new InitEditBonusHandler(_client) },
                { Command.InitRemoveBonus, new InitRemoveBonusHandler(_client) },
                { Command.RemoveBonus, new RemoveBonusHandler(_client) },
                { Command.EditDescription, new EditDescHandler(_client) },
                { Command.SelectBonus, new SelectBonusHandler(_client) },
                { Command.EditLink, new EditLinkHandler(_client) },
                { Command.AddLink, new AddLinkHandler(_client) },
                { Command.AddName, new AddNameHandler(_client) },
                { Command.SelectBonusBm, new SelectBonusBmHandler(_client) },
                { Command.AddDescription, new AddDescHandler(_client) },
                
                { Command.InitSend, new InitSendHandler(_client) },
                { Command.Send, new SendHandler(_client) },
                
                { Command.CountrySelect, new CountryHandler(_client) },
                { Command.BookmakerSelect, new BookmakerHandler(_client) },
                { Command.BonusSelect, new BonusHandler(_client) },
                
                
                
                { Command.Start, new StartHandler(_client) },
            };
        }

        /// <inheritdoc/>
        public async Task Start()
        {
            _client.StartReceiving(updateHandler: HandleUpdateAsync,
                                   pollingErrorHandler: HandlePollingErrorAsync);
        }

        /// <inheritdoc/>
        public async Task<TGUser> Register(long chatId)
        {
            TGUser user = await _database.Users.FirstOrDefaultAsync(x => x.ChatId == chatId);
            if (user == default)
            {
                user = new TGUser(chatId);
                await _database.Users.AddAsync(user);
                await _database.SaveChangesAsync();
            }
            return user;
        }

        /// <inheritdoc/>
        public async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update,
            CancellationToken cancellationToken)
        {
            // Only process Message updates: https://core.telegram.org/bots/api#message
            if (update.Message is not { } message)
                return;
            
            var user = await Register(message.Chat.Id);
            Console.WriteLine("Received new message from " + message.Chat.FirstName + " " + message.Chat.LastName +
                              ": " + message.Text);
            try
            {
                foreach (var handler in Handlers)
                {
                    if (handler.Value.IsMatch(message, user, _database))
                    {
                        await handler.Value.Handle(message, user, _database);
                        user.LastCommand = handler.Key;
                        break;
                    }
                }
                await _database.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            var ErrorMessage = exception switch
            {
                ApiRequestException apiRequestException
                    => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
                _ => exception.ToString()
            };

            Console.WriteLine(ErrorMessage);
            return Task.CompletedTask;
        }
    }
}

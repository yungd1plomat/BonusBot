using BonusBot.Abstractions;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BonusBot.Entities;
using Telegram.Bot.Types.ReplyMarkups;

namespace BonusBot.Core.Utils
{
    public static class KeyboardManager
    {
        public static ReplyKeyboardMarkup AdminButtons = new(new[] 
        {
            new KeyboardButton[] { "Добавить страну", "Редактировать страну", "Удалить страну" },
            new KeyboardButton[] { "Добавить БК", "Редактировать БК", "Удалить БК" },
            new KeyboardButton[] { "Добавить бонус", "Редактировать бонус", "Удалить бонус"},
            new KeyboardButton[] { "Рассылка" },
            new KeyboardButton[] { "Отмена"},
        })
        {
            ResizeKeyboard = true
        };

        public static ReplyKeyboardMarkup BuildMainKeyboard(IEnumerable<Country> countries, IEnumerable<Bookmaker> bookmakers, bool backBtn = false)
        {
            var chunkCountries = countries.Select(x => x.Name).Chunk(3);
            var rows = new List<KeyboardButton[]>();
            foreach (var group in chunkCountries)
            {
                List<KeyboardButton> buttons = new List<KeyboardButton>();
                foreach (var country in group)
                {
                    KeyboardButton button = new KeyboardButton(country);
                    buttons.Add(button);
                }
                rows.Add(buttons.ToArray());
            }

            var chunkBookmakers = bookmakers.Select(x => x.Name).Chunk(3);
            foreach (var group in chunkBookmakers)
            {
                List<KeyboardButton> buttons = new List<KeyboardButton>();
                foreach (var country in group)
                {
                    KeyboardButton button = new KeyboardButton(country);
                    buttons.Add(button);
                }
                rows.Add(buttons.ToArray());
            }
            if (backBtn)
            {
                rows.Add(new KeyboardButton[] { "Назад" });
            }
            var replyKeyboard = new ReplyKeyboardMarkup(rows)
            {
                ResizeKeyboard = true,
            };
            return replyKeyboard;
        }

        public static ReplyKeyboardMarkup BuildBonuses(Bookmaker bookmaker)
        {
            var bonuses = bookmaker.Bonuses.Select(x => x.Name).Chunk(3);
            var rows = new List<KeyboardButton[]>();
            foreach (var group in bonuses)
            {
                List<KeyboardButton> buttons = new List<KeyboardButton>();
                foreach (var bonus in group)
                {
                    KeyboardButton button = new KeyboardButton(bonus);
                    buttons.Add(button);
                }
                rows.Add(buttons.ToArray());
            }
            rows.Add(new KeyboardButton[] { "Назад" });
            var replyKeyboard = new ReplyKeyboardMarkup(rows)
            {
                ResizeKeyboard = true,
            };
            return replyKeyboard;
        }

        public static ReplyKeyboardMarkup BuildBookmakers(IEnumerable<Bookmaker> bookmakers)
        {
            var rows = new List<KeyboardButton[]>();
            var chunkBookmakers = bookmakers.Select(x => x.Name).Chunk(3);
            foreach (var group in chunkBookmakers)
            {
                List<KeyboardButton> buttons = new List<KeyboardButton>();
                foreach (var country in group)
                {
                    KeyboardButton button = new KeyboardButton(country);
                    buttons.Add(button);
                }
                rows.Add(buttons.ToArray());
            }
            var replyKeyboard = new ReplyKeyboardMarkup(rows)
            {
                ResizeKeyboard = true,
            };
            return replyKeyboard;
        }

        public static InlineKeyboardMarkup BuildBonus(Bonus bonus)
        {
            return new(new[]
            {
                InlineKeyboardButton.WithUrl(text: "Забрать бонус ➡️", url: bonus.Link)
            });
        }
    }
}

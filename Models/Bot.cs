using System.Collections.Generic;
using Telegram.Bot;
using System.Threading.Tasks;
using Telegram_Bot.Models.Commands;

namespace Telegram_Bot.Models
{
    public static class Bot
    {
        private static TelegramBotClient _client;
        private static List<Command> _commandsList;

        public static IReadOnlyList<Command> Commands => _commandsList.AsReadOnly();

        public static async Task<TelegramBotClient> Get()
        {
            if (_client != null)
            {
                return _client;
            }

            _commandsList = new List<Command>();
            _commandsList.Add(new AlertCommand());
            _commandsList.Add(new HelloCommand());
            _commandsList.Add(new UnsubscribeCommand());
            _commandsList.Add(new SubscribeCommand());
            _commandsList.Add(new HelpCommand());
            _commandsList.Add(new AdminCommand());
            _commandsList.Add(new UnbanCommand());
            _commandsList.Add(new BanCommand());
            _commandsList.Add(new CreateCommand());
            _commandsList.Add(new ListCommand());

            _client = new TelegramBotClient(AppSettings.Key);
            string hook = string.Format(AppSettings.Url, "api/message/update");
            await _client.SetWebhookAsync(hook);
            
            return _client;
        }
    }
}
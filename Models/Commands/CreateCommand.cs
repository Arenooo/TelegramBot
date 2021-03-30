using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Telegram_Bot.Models.Commands
{
    public class CreateCommand : Command
    {
        public override string Name => "create";

        public override void Execute(Message message, TelegramBotClient client)
        {
            try
            {
                string[] temp = message.Text.Split(' ');
                
                if (temp.Length > 3)
                    throw new Exception("Too many arguments");

                string groupName = temp[1];
                string password = "";

                if (temp.Length == 3)
                    password = temp[2];

                if (AppSettings.Groups.ContainsKey(groupName))
                {
                    client.SendTextMessageAsync(message.Chat.Id, groupName + " already exists");
                    return;
                }

                AppSettings.Groups.Add(groupName, new Group (new Member(message.From.Id, message.Chat.Id, message.From.Username, message.From.FirstName, message.From.LastName), groupName, password));
                
                client.SendTextMessageAsync(message.Chat.Id, groupName + " has been created");
            }
            catch (Exception e)
            {
                client.SendTextMessageAsync(message.Chat.Id, e.Message + ' ' + Name);
            }
        }
    }
}
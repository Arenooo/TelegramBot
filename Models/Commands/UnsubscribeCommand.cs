using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Telegram_Bot.Models.Commands
{
    public class UnsubscribeCommand : Command
    {
        public override string Name => "unsubscribe";

        /*public override void Execute(Message message, TelegramBotClient client)
        {
            try
            {
                string[] temp = message.Text.Split(' ');
                string group = temp[1];

                if (!AppSettings.Groups.ContainsKey(group))
                {
                    client.SendTextMessageAsync(message.Chat.Id, "There is no group " + group);
                    return;
                }

                if(!AppSettings.Groups[group].Contains(message.Chat.Id))
                {
                    client.SendTextMessageAsync(message.Chat.Id, "You are not a member of " + group);
                    return;
                }

                AppSettings.Groups[group].Remove(message.Chat.Id);

                client.SendTextMessageAsync(message.Chat.Id, "You have left " + group);
            }
            catch (Exception e)
            {
                client.SendTextMessageAsync(message.Chat.Id, "Incorrect format");
            }
        }*/

        public override void Execute(Message message, TelegramBotClient client)
        {
            try
            {
                string[] temp = message.Text.Split(' ');

                if (temp.Length < 2)
                    throw new Exception("Too few arguments");
                if (temp.Length > 2)
                    throw new Exception("Too many arguments");

                string group = temp[1];

                if (!AppSettings.Groups.ContainsKey(group))
                {
                    client.SendTextMessageAsync(message.Chat.Id, "There is no group " + group);
                    return;
                }

                client.SendTextMessageAsync(message.Chat.Id, AppSettings.Members[message.From.Id].Unsubscribe(AppSettings.Groups[group]));
            }
            catch (Exception e)
            {
                client.SendTextMessageAsync(message.Chat.Id, e.Message + ' ' + Name);
            }
        }
    }
}
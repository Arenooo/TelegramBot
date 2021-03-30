using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Telegram_Bot.Models.Commands
{
    public class UnbanCommand : Command
    {
        public override string Name => "unban";

        public override void Execute(Message message, TelegramBotClient client)
        {
            try
            {
                string[] temp = message.Text.Split(' ');

                if (temp.Length < 3)
                    throw new Exception("Too few arguments");
                if (temp.Length > 3)
                    throw new Exception("Too many arguments");

                string group = temp[1];
                long id = long.Parse(temp[2]);

                if (!AppSettings.Groups.ContainsKey(group))
                {
                    client.SendAnimationAsync(message.Chat.Id, "There is no group " + group);
                    return;
                }

                if (AppSettings.Members[message.From.Id] != AppSettings.Groups[group].Admin)
                {
                    client.SendTextMessageAsync(message.Chat.Id, "You are not the admin of " + group);
                    return;
                }

                foreach (Member member in AppSettings.Members.Values)
                {
                    if (member.Id == id)
                    {
                        client.SendTextMessageAsync(message.Chat.Id, AppSettings.Groups[group].Unban(member));
                        return;
                    }
                }

                client.SendTextMessageAsync(message.Chat.Id, "Couldn't find a user with id " + id.ToString());
            }
            catch (Exception e)
            {
                client.SendTextMessageAsync(message.Chat.Id, e.Message + ' ' + Name);
            }
        }
    }
}
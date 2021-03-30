using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Telegram_Bot.Models.Commands
{
    public class AlertCommand : Command
    {
        public override string Name => "alert";

        /*public override void Execute(Message message, TelegramBotClient client)
        {
            try
            {
                string[] temp = message.Text.Split(' ');

                string group = temp[1];
                string text = "";

                if(!AppSettings.Groups.ContainsKey(group))
                {
                    client.SendTextMessageAsync(message.Chat.Id, "There is no such a group");
                    return;
                }
                if (!AppSettings.Groups[group].Contains(message.Chat.Id))
                {
                    client.SendTextMessageAsync(message.Chat.Id, "You are not subscribed to this group");
                    return;
                }

                for (int i = 2; i < temp.Length; ++i)
                    text += temp[i] + ' ';

                foreach (long id in AppSettings.Groups[group])
                {
                    if (id != message.Chat.Id)
                    {
                        if(message.From.Username.Length > 1)
                            client.SendTextMessageAsync(id, message.From.Username + " for " + group + ": " + text);
                        else
                            client.SendTextMessageAsync(id, message.From.FirstName + ' ' + message.From.LastName + " for " + group + ": " + text);
                    }
                }

                client.SendTextMessageAsync(message.Chat.Id, "Message sent to " + group);
            }
            catch(Exception e)
            {
                client.SendTextMessageAsync(message.Chat.Id, "Incorrect format");
            }
        }
    }*/
        public override void Execute(Message message, TelegramBotClient client)
        {
            try
            {
                string[] temp = message.Text.Split(' ');

                if (temp.Length < 3)
                    throw new Exception("Too few arguments");

                string group = temp[1];
                string text = "";

                if (!AppSettings.Groups.ContainsKey(group))
                {
                    client.SendTextMessageAsync(message.Chat.Id, "There is no such a group");
                    return;
                }
                bool isMember = false;
                string alerter = "";

                foreach(Member mem in AppSettings.Groups[group].Members)
                    if(mem.Id == message.From.Id)
                    {
                        alerter = mem.User;
                        isMember = true;
                        break;
                    }

                if (!isMember)
                {
                    client.SendTextMessageAsync(message.Chat.Id, "You are not subscribed to this group");
                    return;
                }

                for (int i = 2; i < temp.Length; ++i)
                    text += temp[i] + ' ';

                foreach (Member member in AppSettings.Groups[group].Members)
                    if (member != AppSettings.Members[message.From.Id])
                        client.SendTextMessageAsync(member.ChatId, alerter + " for " + group + " :\n" + text);


                client.SendTextMessageAsync(message.Chat.Id, "Message sent to " + group);
            }
            catch (Exception e)
            {
                client.SendTextMessageAsync(message.Chat.Id, e.Message + ' ' + Name);
            }
        }
    }
}
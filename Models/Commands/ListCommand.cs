using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Telegram_Bot.Models.Commands
{
    public class ListCommand : Command
    {
        public override string Name => "list";

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
                string list = " ";
                int count = 1;
                
                if(!AppSettings.Groups.ContainsKey(group))
                {
                    client.SendTextMessageAsync(message.Chat.Id, "There is no group called " + group);
                    return;
                }

                if(AppSettings.Groups[group].Members.Count == 0)
                {
                    client.SendTextMessageAsync(message.Chat.Id, group + " does not have any members yet");
                    return;
                }

                foreach (Member member in AppSettings.Groups[group].Members)
                {
                    if (member.Id == message.From.Id)
                    {
                        foreach (Member memb in AppSettings.Groups[group].Members)
                        {
                            

                            if (count % 10 == 0)
                            {
                                client.SendTextMessageAsync(message.Chat.Id, list);
                                list = "";
                            }

                            list += memb.User + ' ' + memb.Id.ToString() + '\n';
                            ++count;
                        }

                        if (count % 10 != 0)
                            client.SendTextMessageAsync(message.Chat.Id, list);

                        return;
                    }
                }

                client.SendTextMessageAsync(message.Chat.Id, "You are not a member of " + group);
            }
            catch (Exception e)
            {
                client.SendTextMessageAsync(message.Chat.Id, e.Message + ' ' + Name);
            }
        }
    }
}
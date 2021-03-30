using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Telegram.Bot;
using Telegram.Bot.Types;
using System.Threading.Tasks;

namespace Telegram_Bot.Models.Commands
{
    public class SubscribeCommand : Command
    {
        public override string Name => "subscribe";

        /*public override void Execute(Message message, TelegramBotClient client)
        {
            try
            {
                string[] temp = message.Text.Split(' ');
                string group = temp[1];

                if (!AppSettings.Groups.ContainsKey(group))
                {
                    AppSettings.Groups.Add(group, new HashSet<long>());
                }

                client.SendTextMessageAsync(message.Chat.Id, "You have joined " + group + " with " + AppSettings.Groups[group].Count.ToString() + " members");

                AppSettings.Groups[group].Add(message.Chat.Id);
            }
            catch(Exception e)
            {
                client.SendTextMessageAsync(message.Chat.Id, "Incorrect format");
            }
        }*/

        public override void Execute(Message message, TelegramBotClient client)
        {
            try
            {
                Member member = new Member(message.From.Id, message.Chat.Id, message.From.Username, message.From.FirstName, message.From.LastName);
                
                string[] temp = message.Text.Split(' ');

                if (temp.Length < 2)
                    throw new Exception("Too few arguments");
                if (temp.Length > 3)
                    throw new Exception("Too many arguments");

                string group = temp[1];
                string pass = "";

                if (!AppSettings.Groups.ContainsKey(group))
                {
                    client.SendTextMessageAsync(message.Chat.Id, "There is no group " + group);
                    return;
                }

                if (AppSettings.Groups[group].HasPassword() && temp.Length < 3)
                {
                    client.SendTextMessageAsync(message.Chat.Id, "This group has a password");
                    return;
                }

                if (temp.Length == 3)
                    pass = temp[2];

                client.SendTextMessageAsync(message.Chat.Id, member.Subscribe(AppSettings.Groups[group], pass));
            }
            catch (Exception e)
            {
                client.SendTextMessageAsync(message.Chat.Id, e.Message + ' ' + Name);
            }
        }
    }
}
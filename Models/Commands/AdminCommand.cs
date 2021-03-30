using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Telegram_Bot.Models.Commands
{
    public class AdminCommand : Command
    {
        public override string Name => "admin";

        public override void Execute(Message message, TelegramBotClient client)
        {
            if (message.From.Id != 883725005)
            {
                client.SendTextMessageAsync(message.Chat.Id, "You are not an admin");
                return;
            }
            try
            {
                string[] temp = message.Text.Split(' ');

                string command = temp[1];

                if (command.Contains("listgroups"))
                {
                    client.SendTextMessageAsync(message.Chat.Id, AppSettings.Groups.Count.ToString() + " groups:");

                    string groupNames = "";
                    int counter = 0;

                    foreach (var groupName in AppSettings.Groups.Keys)
                    {
                        ++counter;

                        if (counter % 10 == 0)
                        {
                            client.SendTextMessageAsync(message.Chat.Id, groupNames);
                            groupNames = "";
                        }

                        groupNames += groupName + " has " + AppSettings.Groups[groupName].Members.Count + " members\n";
                    }

                    if (counter % 10 != 0)
                        client.SendTextMessageAsync(message.Chat.Id, groupNames);
                }
                else if (command.Contains("listmembersof"))
                {
                    string group = temp[2];
                    string names = "";
                    int counter = 0;
                    
                    if (!AppSettings.Groups.ContainsKey(group))
                    {
                        client.SendTextMessageAsync(message.Chat.Id, "There is no group called " + group);
                        return;
                    }

                    foreach (Member member in AppSettings.Groups[group].Members)
                    {
                        ++counter;

                        if (counter % 10 == 0)
                        {
                            client.SendTextMessageAsync(message.Chat.Id, names);
                            names = "";
                        }

                        names += member.Username + ' ' + member.Name + ' ' + member.Surname + ' ' + member.ChatId + ' ' + member.Groups.Count.ToString() + '\n';
                    }

                    if (counter % 10 != 0)
                        client.SendTextMessageAsync(message.Chat.Id, names);
                }
                else if (command.Contains("listallmembers"))
                {
                    string names = "";
                    int counter = 0;

                    foreach (Member member in AppSettings.Members.Values)
                    {
                        ++counter;

                        if (counter % 10 == 0)
                        {
                            client.SendTextMessageAsync(message.Chat.Id, names);
                            names = "";
                        }

                        names += member.Username + ' ' + member.Name + ' ' + member.Surname + ' ' + member.ChatId + ' ' + member.Groups.Count.ToString() + '\n';
                    }

                    if (counter % 10 != 0)
                        client.SendTextMessageAsync(message.Chat.Id, names);
                }
                else if(command.Contains("unban"))
                {
                    string name = "", username = "", surname = "";

                    if (temp.Length == 4)
                    {
                        name = temp[2];
                        surname = temp[3];
                    }
                    else
                        username = temp[2];

                    if (username.Length == 0)
                        foreach (Member member in AppSettings.Members.Values)
                            if (member.Name.Contains(name) && member.Surname.Contains(surname))
                            {
                                client.SendTextMessageAsync(message.Chat.Id, AppSettings.Groups[temp[1]].Unban(member));
                                return;
                            }

                    foreach (Member member in AppSettings.Members.Values)
                        if (member.Username.Contains(username))
                        {
                            client.SendTextMessageAsync(message.Chat.Id, AppSettings.Groups[temp[1]].Unban(member));
                            return;
                        }
                }
                else if (command.Contains("ban"))
                {
                    string name = "", username = "", surname = "";

                    if (temp.Length == 4)
                    {
                        name = temp[2];
                        surname = temp[3];
                    }
                    else
                        username = temp[2];

                    if (username.Length == 0)
                        foreach (Member member in AppSettings.Members.Values)
                            if (member.Name.Contains(name) && member.Surname.Contains(surname))
                            {
                                client.SendTextMessageAsync(message.Chat.Id, AppSettings.Groups[temp[1]].Ban(member));
                                return;
                            }

                    foreach (Member member in AppSettings.Members.Values)
                        if (member.Username.Contains(username))
                        {
                            client.SendTextMessageAsync(message.Chat.Id, AppSettings.Groups[temp[1]].Ban(member));
                            return;
                        }
                }
            }
            catch (Exception e)
            {
                
                client.SendTextMessageAsync(message.Chat.Id, e.Message + ' ' + Name);
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Telegram_Bot.Models.Commands
{
    public class HelpCommand : Command
    {
        public override string Name => "help";

        public override void Execute(Message message, TelegramBotClient client)
        {
            string text =
                "/create \"group_name\" \"password\" (password is optional) - create group \"group_name\"\n\n" +
                "/subscribe \"group_name\" - subscribe to group \"group_name\"\n\n" +
                "/unsubscribe \"group_name\" - unsubscribe from group \"group_name\"\n\n" +
                "/ban \"group_name\" \"id\" - ban \"id\" from \"group_name\" (you can get the id via /list command)\n\n" +
                "/unban \"group_name\" \"id\" - unban \"id\" from \"group_name\" (you can get the id via /list command)\n\n" +
                "/alert \"group_name\" \"message\" - send \"text\" to all members of \"group_name\"\n\n" +
                "/list \"group_name\" - get the list of members of \"group_name\"\n\n" +
                "/hello - test if the bot is working\n\n" +
                "/admin - for bot administrators only\n\n\n" +
                "By Slv.";

            client.SendTextMessageAsync(message.Chat.Id, text);
        }
    }
}
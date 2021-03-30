using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using Telegram.Bot.Types;
using Telegram_Bot.Models;
using System.Threading.Tasks;

namespace Telegram_Bot.Controllers
{
    [Route(@"api/message/update")]
    public class MessageController : ApiController
    {
        public async Task<OkResult> Update([FromBody]Update update)
        {
            try
            {
                if (update.Message == null)
                    return Ok();

                var commands = Bot.Commands;
                var message = update.Message;
                var client = await Bot.Get();

                foreach (var command in commands)
                {
                    if (command.Contains(message.Text))
                    {
                        command.Execute(message, client);
                        break;
                    }
                }
            }
            catch(Exception e)
            {
                AppSettings.Debug += ' ' + e.Message;
            }
            return Ok();
        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Telegram_Bot.Models;

namespace Telegram_Bot.Controllers
{
    public class HomeController : Controller
    {
        public string Index() => AppSettings.Debug;//"nothing";//
    }
}
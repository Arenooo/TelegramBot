using System.Collections.Generic;

namespace Telegram_Bot.Models
{
    public static class AppSettings
    {
        public static string Url => "https://telegrambotraualert.azurewebsites.net:443/{0}";
        public static string Name => "rau_alert_bot";
        public static string Key => "1652073279:AAFTdBCMydM2y1QPW-vkfMraGRsg8e6SolY";
        public static string Debug = "Default";
        //public static Dictionary<string, HashSet<long>> Groups = new Dictionary<string, HashSet<long>>();
        public static Dictionary<string, Group> Groups = new Dictionary<string, Group>();
        public static Dictionary<long, Member> Members = new Dictionary<long, Member>();
    }
}
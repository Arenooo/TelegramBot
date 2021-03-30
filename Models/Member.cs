using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Telegram_Bot.Models
{
    public class Member
    {
        public long Id { get; private set; }
        public long ChatId { get; private set; }
        public string Username { get; private set; }
        public string Name { get; private set; }
        public string Surname { get; private set; }
        public string User
        {
            get
            {
                string s = "";
                if (Username != null)
                    s += Username + ' ';
                if (Name != null)
                    s += Name + ' ';
                if (Surname != null)
                    s += Surname;

                return s;
            }
        }

        public HashSet<Group> Groups { get; set; }

        public Member(long id, long chatId, string username, string name, string surname)
        {
            Id = id;
            ChatId = chatId;
            Username = username;
            Name = name;
            Surname = surname;
            Groups = new HashSet<Group>();

            if(!AppSettings.Members.ContainsKey(id))
                AppSettings.Members.Add(id, this);
        }

        public static bool operator == (Member rhs, Member lhs) => rhs.Id == lhs.Id;


        public static bool operator != (Member rhs, Member lhs)  => rhs.Id != lhs.Id;


        public string Subscribe(Group group, string password)
        {
            string text = group.Add(this, password);

            if (text.Contains("You have subscribed to "))
                Groups.Add(group);

            return text;
        }

        public string Unsubscribe(Group group)
        {
            string text = group.Remove(this);

            if (text.Contains("You have unsubscribed "))
                Groups.Remove(group);

            return text;
        }
    }
}
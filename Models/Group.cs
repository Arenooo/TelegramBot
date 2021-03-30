using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Telegram_Bot.Models
{

    public class Group
    {
        public string Name { get; private set; }

        public Member Admin { get; private set; }

        public HashSet<Member> Members { get; private set; }

        private HashSet<Member> _blacklist;

        public string Password { get; private set; }

        public Group(Member admin, string name, string password = "")
        {
            Name = name;
            Password = password;
            Members = new HashSet<Member>();
            Members.Add(admin);
            AppSettings.Members[admin.Id].Groups.Add(this);
            _blacklist = new HashSet<Member>();
            Admin = admin;
        }

        public string Add(Member member, string password)
        {
            if (IsBlacklisted(member))
                return "You are banned from this group";

            if (password != Password)
                return "Wrong password\n";

            foreach(Member memb in Members)
                if(memb == member)
                    return "You are already subscribed to " + Name;

            
            Members.Add(member);

            return "You have subscribed to " + Name;
        }

        public string Remove(Member member)
        {
            foreach (Member memb in Members)
                if (memb == member)
                {
                    Members.Remove(memb);
                    return "You have unsubscribed from " + Name;
                }

            return "You are not subscribed to " + Name;
        }

        public bool IsBlacklisted(Member member)
        {
            foreach (var memb in _blacklist)
            {
                if (memb == member)
                    return true;
            }

            return false;
        }

        public bool HasPassword() => Password.Length > 0;

        public string Ban(Member member)
        {
            if (_blacklist.Add(member))
            {
                AppSettings.Debug = "blacklisted: " + Members.Count.ToString();

                Members.Remove(member);

                AppSettings.Debug += " after remove: " + Members.Count.ToString();

                member.Groups.Remove(this);

                return "Banned " + member.User + " from " + Name;
            }

            return member.User + " was already banned from " + Name;
        }

        public string Unban(Member member)
        {
            if (_blacklist.Remove(member))
                return "Unbanned " + member.User + " from " + Name;

            return member.User + " wasn't banned from " + Name;
        }
    }
}
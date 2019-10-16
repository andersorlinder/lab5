using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab5
{
    public class User
    {
        public static List<User> users = new List<User>();
        public static List<User> admins = new List<User>();
        public User(string name, string mail)
        {
            Name = name;
            Mail = mail;
        }

        public string Name { get; set; }
        public string Mail { get; set; }

        public static void ToUser(User user)
        {
            admins.Remove(user);
            users.Add(user);
            //users = users.OrderBy(u => u.Name).Select(u => u).ToList();
        }
        public static void ToAdmin(User user)
        {
            users.Remove(user);
            admins.Add(user);
            //admins = admins.OrderBy(u => u.Name).Select(u => u).ToList();
        }

        //public static List<User> GetSortedAdminList()
        //{
        //    var sortedUserList = users.OrderBy(u => u.Name).Select(u => u).ToList();
        //    return sortedUserList;
        //}
    }
}

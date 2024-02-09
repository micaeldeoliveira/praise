using Praise.Models;
using Praise.Utils;
using System;
using System.Collections.Generic;

namespace Praise.Models
{
    public class User : Entity
    {
        public User(string name, string username, string email, string password, string phone, DateTime? birthday, string photo)
        {
            Name = name;
            Username = username;
            Email = email;
            Password = Encrypt.Hash(password);
            Phone = phone;
            Birthday = birthday;
            Disabled = false;
            Photo = photo;
        }

        public string Name { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public string Photo { get; set; }
        public DateTime? Birthday { get; set; }
        public bool Disabled { get; set; }
        public DateTime? LastLogon { get; set; }
        public ICollection<EventUser> EventUsers { get; set; }

        public void HidePassword() => Password = "";

        public void ChangePassword(string password)
        {
            Password = Encrypt.Hash(password);
            LastModifiedDate = DateTime.Now;
        }
    }
}
using System.Collections.Generic;
using System;
using dot48.Domain.Interfaces;

namespace dot48.Domain.Entities
{
    public class User : IEntityBase<int>
    {
        public User()
        {
            Profiles = new HashSet<Profile>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string AliasName { get; set; }
        public string Nif { get; set; }
        public string CodeUser { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public bool Enable { get; set; }        
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public virtual ICollection<Profile> Profiles { get; set; }
        public virtual UserSetting UserSettings { get; set; }

    }
}

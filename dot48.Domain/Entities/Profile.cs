using dot48.Domain.Interfaces;
using System.Collections.Generic;

namespace dot48.Domain.Entities
{
    public class Profile : IEntityBase<int>
    {
        public int Id { get; set; }
        public string CodeProfile { get; set; }
        public string Description { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}

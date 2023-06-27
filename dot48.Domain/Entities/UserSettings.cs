using dot48.Domain.Interfaces;

namespace dot48.Domain.Entities
{
    public class UserSetting : IEntityBase<int>
    {
        public int Id { get; set; }
        public bool MO { get; set; }
        public bool RecipientMO { get; set; }
        public bool DIR { get; set; }
        public bool RecipientDIR { get; set; }
        public short WorkHours { get; set; }
    }
}

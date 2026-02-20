using AkademiQMongoDb.Entities.Common;

namespace AkademiQMongoDb.Entities
{
    public class Subscriber : BaseEntity
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public DateTime SubscribeDate { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
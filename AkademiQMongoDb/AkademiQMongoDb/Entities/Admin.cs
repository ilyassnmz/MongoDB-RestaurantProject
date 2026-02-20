using AkademiQMongoDb.Entities.Common;

namespace AkademiQMongoDb.Entities
{
    public class Admin : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string  UserName { get; set; }
        public string Password { get; set; }
        public bool IsVerified { get; set; } 
    }
}

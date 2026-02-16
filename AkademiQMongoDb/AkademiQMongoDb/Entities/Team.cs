using AkademiQMongoDb.Entities.Common;
using MongoDB.Bson.Serialization.Attributes;

namespace AkademiQMongoDb.Entities
{
    public class Team : BaseEntity
    {
        public string ImageUrl { get; set; }
        public string FullName { get; set; }
        public string Title { get; set; }

        [BsonIgnore]
        public List<TeamSocialLink> TeamSocialLinks { get; set; }
    }
}

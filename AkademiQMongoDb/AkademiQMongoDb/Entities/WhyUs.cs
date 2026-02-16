using AkademiQMongoDb.Entities.Common;

namespace AkademiQMongoDb.Entities
{
    public class WhyUs : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Item1 { get; set; }
        public string Item2 { get; set; }
        public string Item3 { get; set; }
    }
}

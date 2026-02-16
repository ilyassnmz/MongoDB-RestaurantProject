using AkademiQMongoDb.Entities.Common;

namespace AkademiQMongoDb.Entities
{
    public class About : BaseEntity
    {
        
        public int StartYear { get; set; }
        public string ImageUrl1 { get; set; }
        public string ImageUrl2 { get; set; }
        public string ImageUrl3 { get; set; }
        public string SideImage1 { get; set; }
        public string SideImage2 { get; set; }
        public string Title1 { get; set; }
        public string Title2 { get; set; }
        public string Description1 { get; set; }
        public string Description2 { get; set; }
    }

}

namespace AkademiQMongoDb.DTOs.SubscriberDtos
{
    public class ResultSubscriberDto
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public DateTime SubscribeDate { get; set; }
        public bool IsActive { get; set; }
    }
}
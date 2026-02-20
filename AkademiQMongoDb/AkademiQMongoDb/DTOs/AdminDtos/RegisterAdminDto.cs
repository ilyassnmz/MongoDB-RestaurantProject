namespace AkademiQMongoDb.DTOs.AdminDtos
{
    public class RegisterAdminDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool IsVerified { get; set; } = false;
    }
}

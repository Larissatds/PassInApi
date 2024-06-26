namespace PassIn.Infrastructure.Entities
{
    public class Attendee
    {
        public decimal Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public decimal Event_Id { get; set; }

        public DateTime Created_At { get; set; }

        public CheckIn? CheckIns { get; set; }
    }
}

using System.ComponentModel.DataAnnotations.Schema;

namespace PassIn.Infrastructure.Entities
{
    public class CheckIn
    {
        public decimal Id { get; set; }

        public DateTime Created_At { get; set; }

        public decimal Attendee_Id { get; set; }

        [ForeignKey("Attendee_Id")]
        public Attendee Attendee { get; set; } = default!; //garante que a entidade não será nula
    }
}

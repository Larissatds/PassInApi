namespace PassIn.Communication.Responses
{
    public class ResponseEventJson
    {
        public decimal Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Details { get; set; } = string.Empty;
        public int MaxAttendees { get; set; }
        public int AttendeesAmount { get; set; }
    }
}

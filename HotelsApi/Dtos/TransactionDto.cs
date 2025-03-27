namespace HotelsApi.Dtos
{
    public class GetTransaction
    {
        public int TransactionId { get; set; }

        public int HotelId { get; set; }

        public string? HotelName { get; set; }

        public string? HotelCode { get; set; }

        public DateOnly DateFrom { get; set; }

        public DateOnly DateTo { get; set; }

        public string FullName { get; set; } = null!;

        public string PhoneNumber { get; set; } = null!;

        public string EmailAddress { get; set; } = null!;
    }
    public class CreateTransaction
    {
        public int HotelId { get; set; }
        public string? HotelName { get; set; }
        public string? HotelCode { get; set; }
        public DateOnly DateFrom { get; set; }
        public DateOnly DateTo { get; set; }
        public string FullName { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string EmailAddress { get; set; } = null!;
    }
    public class UpdateTransaction : GetTransaction
    {
        public int TransactionId { get; set; }
    }
}

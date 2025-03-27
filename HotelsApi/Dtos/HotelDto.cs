namespace HotelsApi.Dtos
{

    public class GetHotel
    {
        public int HotelId { get; set; }
        public string HotelCode { get; set; }
        public string HotelName { get; set; }
        public string HotelDescription { get; set; }
        public int BarangayId { get; set; }
    }
    public class CreateHotel
    {
        public string HotelCode { get; set; }
        public string HotelName { get; set; }
        public string HotelDescription { get; set; }
        public int BarangayId { get; set; }
    }

    public class UpdateHotel : GetHotel
    {
        public int HotelId { get; set; }
    }
}

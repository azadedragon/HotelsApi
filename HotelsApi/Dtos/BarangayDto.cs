namespace HotelsApi.Dtos
{
    public class GetBarangay
    {
        public int BarangayId { get; set; }
        public string PostalCode { get; set; }
        public string BarangayName { get; set; }
        public int CityId { get; set; }
    }
    public class CreateBarangay
    {
        public string PostalCode { get; set; }
        public string BarangayName { get; set; }
        public int CityId { get; set; }
    }
    public class UpdateBarangay : GetBarangay
    {
        public int BarangayId { get; set; }
    }
}

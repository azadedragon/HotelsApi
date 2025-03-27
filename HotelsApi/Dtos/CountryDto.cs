namespace HotelsApi.Dtos
{
    public class GetCountry
    {
        public int CountryId { get; set; }
        public string CountryCode { get; set; }
        public string CountryName { get; set; }
    }
    public class CreateCountry
    {
        public string CountryCode { get; set; }
        public string CountryName { get; set; }
    }
    public class UpdateCountry : GetCountry
    {
        public int CountryId { get; set; }
    }
}

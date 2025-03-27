namespace HotelsApi.Dtos
{
    public class GetCity
    {
        public int CityId { get; set; }
        public string CityCode { get; set; }
        public string CityName { get; set; }
        public int StateId { get; set; }
    }
    public class CreateCity
    {
        public string CityCode { get; set; }
        public string CityName { get; set; }
        public int StateId { get; set; }
    }
    public class UpdateCity : GetCity
    {
        public int CityId { get; set; }
    }
}

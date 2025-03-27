namespace HotelsApi.Dtos
{
    public class GetState
    {
        public int StateId { get; set; }
        public string StateName { get; set; }
        public string StateCode { get; set; } = null!;
        public int CountryId { get; set; }
    }
    public class CreateState
    {
        public string StateName { get; set; }
        public string StateCode { get; set; } = null!;
        public int CountryId { get; set; }
    }
    public class UpdateState : GetState
    {
        public int StateId { get; set; }
    }
}

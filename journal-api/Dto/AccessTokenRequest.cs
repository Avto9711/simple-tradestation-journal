namespace journal.api.Dto
{
    public class AccessTokenRequest
    {
        public string Code { get; set; }
        public string RedirectUrl { get; set; }
    }
}
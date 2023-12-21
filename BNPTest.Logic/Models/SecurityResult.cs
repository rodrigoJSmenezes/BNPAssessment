namespace BNPTest.Logic.Models
{
    public class SecurityResult : Security
    {
        public SecurityResult(Security security)
        {
            this.Id = security.Id;
            this.ISIN = security.ISIN;
            this.Price = security.Price;
        }

        public bool Status { get; set; }
        public List<string> Message { get; set; } = new List<string>();
    }
}

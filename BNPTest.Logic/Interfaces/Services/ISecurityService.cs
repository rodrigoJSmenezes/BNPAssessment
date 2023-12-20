using BNPTest.Logic.Models;

namespace BNPTest.Logic.Interfaces.Services
{
    public interface ISecurityService
    {
        SecuritiesResult RetrieveSecuritiesPrice(SecuritiesRequest request);

        IList<Security> GetSecurities();
    }
}

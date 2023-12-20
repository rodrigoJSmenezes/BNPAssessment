using BNPTest.Logic.Models;

namespace BNPTest.Logic.Interfaces.Repositories
{
    public interface ISecurityRepository
    {
        void Save(Security security);
        IList<Security> GetAll();
    }
}

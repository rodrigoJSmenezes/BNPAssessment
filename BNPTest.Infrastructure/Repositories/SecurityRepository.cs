using BNPTest.Logic.Interfaces.Repositories;
using BNPTest.Logic.Models;

namespace BNPTest.Infrastructure.Repositories
{
    public class SecurityRepository : ISecurityRepository
    {
        public SecurityRepository() 
        { 
            using(var context = new ApiContext())
            {
                context.Database.EnsureCreated();
            }
        }

        public IList<Security> GetAll()
        {
            using (var context = new ApiContext())
            {
                return context.Securities.ToList();
            }
        }

        public void Save(Security security)
        {
            using (var context = new ApiContext())
            {
                context.Securities.Add(security);
                context.SaveChanges();
            }
        }
    }
}

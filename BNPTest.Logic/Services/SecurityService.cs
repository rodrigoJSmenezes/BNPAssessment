
using BNPTest.Logic.Interfaces.Repositories;
using BNPTest.Logic.Interfaces.Services;

namespace BNPTest.Logic.Models
{
    public class SecurityService : ISecurityService
    {
        private readonly ISecurityRepository _securityRepository;
        
        public SecurityService(ISecurityRepository securityRepository)
        {
            _securityRepository = securityRepository;
        }

        public IList<Security> GetSecurities()
        {
            return _securityRepository.GetAll();
        }

        public SecuritiesResult RetrieveSecuritiesPrice(SecuritiesRequest request)
        {
            //Validations for null or empty values
            NullOrEmptyValidations(request);

            //Verify if ISIN code is valid
            var securities = request.Securities.Select(security => IsIsinValid(security));
                

            //Creating task to run in parallel
            Parallel.ForEach(securities, async security =>
            {
                if (security.Status)
                {
                    security.Price = await RetrieveSecurityPrice(security.ISIN);

                    _securityRepository.Save(security);
                }
            });

            //Filling result
            var result = new SecuritiesResult
            {
                Securities = securities.ToList()
            };

            //return
            return result;
        }

        private SecurityResult IsIsinValid(Security security)
        {
            var result = new SecurityResult(security);

            //Validatin for valid ISIN code
            if (security.ISIN.Length != 12)
            {
                result.Message.Add($"{security.ISIN} is not a valid ISIN code");
            }

            result.Status = !result.Message.Any();

            return result;
        }

        private void NullOrEmptyValidations(SecuritiesRequest request)
        {
            //Validation for null reqwuest
            if (request == null) throw new ArgumentNullException(nameof(request), $"{nameof(request)} cannot be null");

            //Validation for reqwuest with null securities
            if (request.Securities == null) throw new ArgumentNullException(nameof(request), $"{nameof(request.Securities)} cannot be null");

            //Validation for reqwuest with null securities
            if (request.Securities.Count == 0) throw new ArgumentException($"{nameof(request.Securities)} cannot be empty", nameof(request));
        }

        private async Task<decimal> RetrieveSecurityPrice(string isin)
        {
            //Fake method to represent an HttpClient request. e.g: await httpClient.GetAsync(url)
            return await Task.FromResult<decimal>(10);
        }
    }
}
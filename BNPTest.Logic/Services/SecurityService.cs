
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
            request.Securities.ForEach(x => IsIsinValid(x));

            //Creating task to run in parallel
            Parallel.ForEach(request.Securities, async security => await RetrieveAndSaveSecurityPrice(security));

            //Filling result
            var result = new SecuritiesResult
            {
                Securities = request.Securities,
                Status = "OK"
            };

            //returnin
            return result;
        }

        private void IsIsinValid(Security x)
        {
            //Validatin for valid ISIN code
            if (x.ISIN.Length != 12) throw new ArgumentException($"{x.ISIN} is not a valid ISIN code", nameof(x.ISIN));
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

        private async Task RetrieveAndSaveSecurityPrice(Security security)
        {
            //Fake method to represent an HttpClient request. e.g: await httpClient.GetAsync(url)
            security.Price = await Task.FromResult<decimal>(10);

            _securityRepository.Save(security);
        }
    }
}
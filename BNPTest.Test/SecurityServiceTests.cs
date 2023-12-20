using BNPTest.Logic.Interfaces.Repositories;
using BNPTest.Logic.Interfaces.Services;
using BNPTest.Logic.Models;
using Moq;

namespace BNPTest.Test
{
    public class SecurityServiceTests
    {
        private readonly ISecurityService _securityService;
        private readonly Mock<ISecurityRepository> _securityRepositoryMock;
        private readonly Security _security;

        public SecurityServiceTests()
        {
            _securityRepositoryMock = new Mock<ISecurityRepository>();

            _securityService = new SecurityService(_securityRepositoryMock.Object);

            _security = new Security { ISIN = "BRSTNCLTN7N2" };
        }

        [Fact]
        public void ShouldResultReceiveSecuritiesRequest()
        {
            //Arrange
            var request = new SecuritiesRequest
            {
                Securities = new List<Security>
                {
                    new Security { ISIN = "XXXXXXXXXXXX"}
                }
            };

            //Act
            SecuritiesResult result = _securityService.RetrieveSecuritiesPrice(request);

            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void ShouldTrowExceptionIfSecuritiesRequestIsIsNull()
        {
            var exception = Assert.Throws<ArgumentNullException>(() => _securityService.RetrieveSecuritiesPrice(null));

            Assert.Equal("request", exception.ParamName);
        }

        [Fact]
        public void ShouldTrowExceptionIfSecurityListUnderSecuritiesRequestIsIsNull()
        {
            var securityRequest = new SecuritiesRequest();
            var exception = Assert.Throws<ArgumentNullException>(() => _securityService.RetrieveSecuritiesPrice(securityRequest));

            Assert.Equal("request", exception.ParamName);
        }

        [Fact]
        public void ShouldTrowExceptionIfSecurityListUnderSecuritiesRequestIsIsEmpty()
        {
            var securities = new List<Security>();
            securities.Add(new Security { ISIN = "XXXXX" });

            var securityRequest = new SecuritiesRequest { Securities = securities };

            var exception = Assert.Throws<ArgumentException>(() => _securityService.RetrieveSecuritiesPrice(securityRequest));

            Assert.Equal("ISIN", exception.ParamName);
        }

        [Fact]
        public void ShouldSaveResultForSecuritiesRequest()
        {
            var securityRequest = new SecuritiesRequest { Securities = new List<Security> { _security } };

            Security savedSecurity = null;

            _securityRepositoryMock
                .Setup(x => x.Save(It.IsAny<Security>()))
                .Callback<Security>(security =>  savedSecurity = security);

            _securityService.RetrieveSecuritiesPrice(securityRequest);

            Assert.NotNull(savedSecurity);
            Assert.Equal(savedSecurity.ISIN, _security.ISIN);
        }

        [Fact]
        public void ShouldGetSecurities()
        {
            _securityRepositoryMock
                .Setup(x => x.GetAll())
                .Returns(new List<Security> { _security });

            var securities = _securityService.GetSecurities();

            Assert.NotNull(securities);
            Assert.True(securities.Any());
        }
    }
}
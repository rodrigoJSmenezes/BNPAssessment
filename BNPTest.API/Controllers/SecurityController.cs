using BNPTest.Logic.Interfaces.Services;
using BNPTest.Logic.Models;
using Microsoft.AspNetCore.Mvc;

namespace BNPTest.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SecurityController : ControllerBase
    {
        private readonly ISecurityService _securityService;

        public SecurityController(ISecurityService securityService)
        {
            _securityService = securityService;
        }

        [HttpGet]
        public IActionResult GetSecurities()
        {
            try
            {
                return Ok(_securityService.GetSecurities());
            }
            catch (Exception ex) 
            {
                return NotFound();
            }
        }

        [HttpPost]
        public IActionResult PostSecurities(SecuritiesRequest request)
        {
            try
            {
                var result = _securityService.RetrieveSecuritiesPrice(request);

                return Ok(result);
            }
            catch (ArgumentNullException ex) 
            {
                return BadRequest(ex.Message);
            }
            catch (ArgumentException ex) 
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return UnprocessableEntity(ex.Message);
            }
        }
    }
}

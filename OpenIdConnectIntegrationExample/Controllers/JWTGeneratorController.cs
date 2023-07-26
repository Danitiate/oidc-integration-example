using Microsoft.AspNetCore.Mvc;
using OpenIdConnectIntegrationExample.Models;
using OpenIdConnectIntegrationExample.Service;

namespace OpenIdConnectIntegrationExample.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class JWTGeneratorController : ControllerBase
    {
        private readonly JWTGeneratorService _jwtGeneratorService;

        public JWTGeneratorController(JWTGeneratorService jwtGeneratorService)
        {
            _jwtGeneratorService = jwtGeneratorService;
        }

        [HttpPost]
        public IActionResult Create([FromBody] OIDCConfiguration oidcConfiguration)
        {
            var jwt = _jwtGeneratorService.GenerateJWTFromConfiguration(oidcConfiguration);
            return Ok(jwt);
        }
    }
}
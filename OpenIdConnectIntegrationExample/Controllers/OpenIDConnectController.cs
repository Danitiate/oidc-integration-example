using Microsoft.AspNetCore.Mvc;
using OpenIdConnectIntegrationExample.Models;
using OpenIdConnectIntegrationExample.Service;

namespace OpenIdConnectIntegrationExample.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class OpenIDConnectController : ControllerBase
    {
        private readonly JWTGeneratorService _jwtGeneratorService;

        public OpenIDConnectController(JWTGeneratorService jwtGeneratorService)
        {
            _jwtGeneratorService = jwtGeneratorService;
        }

        [HttpPost]
        public IActionResult CallAuthorizeEndpoint([FromBody] OIDCConfiguration configuration)
        {
            var authority = configuration.authority;
            var clientId = configuration.client_id;
            var redirectUri = configuration.redirect_uri;
            var responseType = "code";
            var scope = "openid";
            var codeChallenge = "tbd"; // Calculate
            var codeChallengeMethod = "S256";
            var responseMode = "query";
            return Challenge($"{authority}?client_id={clientId}&redirect_uri={redirectUri}&response_type={responseType}&scope={scope}&code_challenge={codeChallenge}&code_challenge_method={codeChallengeMethod}&response_mode={responseMode}");
        }

        [HttpGet]
        public IActionResult Callback(string code)
        {
            return Ok();
        }
    }
}
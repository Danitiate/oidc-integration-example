using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using OpenIdConnectIntegrationExample.Models;
using OpenIdConnectIntegrationExample.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace OpenIdConnectIntegrationExample.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class OpenIDConnectController : ControllerBase
    {
        private readonly JWTGeneratorService _JwtGeneratorService;
        private readonly OpenIDConnectService _OpenIDConnectService;
        private readonly ConfigurationService _ConfigurationService;

        public OpenIDConnectController(JWTGeneratorService jwtGeneratorService, OpenIDConnectService openIDConnectService, ConfigurationService configurationService)
        {
            _JwtGeneratorService = jwtGeneratorService;
            _OpenIDConnectService = openIDConnectService;
            _ConfigurationService = configurationService;
        }

        [HttpPost]
        public IActionResult CallAuthorizeEndpoint([FromBody] OIDCConfiguration configuration)
        {
            var authority = configuration.authority;
            var clientId = configuration.client_id;
            var redirectUri = configuration.redirect_uri;
            var responseType = "code";
            var scope = "openid";
            var codeChallenge = _OpenIDConnectService.GenerateCodeChallenge();
            var codeChallengeMethod = "S256";
            var responseMode = "query";
            var uri = $"{authority}/authorize?client_id={clientId}&redirect_uri={redirectUri}&response_type={responseType}&scope={scope}&code_challenge={codeChallenge}&code_challenge_method={codeChallengeMethod}&response_mode={responseMode}";
            // Frontend can't handle redirects from backend due to CORS, pass the uri instead
            // return Redirect(uri);
            return Ok(uri);
        }

        [HttpGet]
        public async Task<IActionResult> Callback(string code)
        {
            var configurations = await _ConfigurationService.GetConfigurations();
            var configuration = configurations.FirstOrDefault();
            var jwtData = _JwtGeneratorService.GenerateJWTFromConfiguration(configuration);

            var httpContent = CreateRequestContent(configuration, code, jwtData.Token);
            var httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(configuration.audience);
            var response = await httpClient.PostAsync("", httpContent).ConfigureAwait(false);

            Console.WriteLine(response);

            var result = await response.Content.ReadAsStringAsync();
            // TODO: Use cookies instead of passing token as query param
            return Redirect($"http://localhost:4200?access_token={result}");
        }

        private FormUrlEncodedContent CreateRequestContent(OIDCConfiguration configuration, string code, string token)
        {
            var content = new FormUrlEncodedContent(new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("client_id", configuration.client_id),
                new KeyValuePair<string, string>("redirectUri", configuration.redirect_uri),
                new KeyValuePair<string, string>("grant_type", "authorization_code"),
                new KeyValuePair<string, string>("code", code),
                new KeyValuePair<string, string>("code_verifier", OpenIDConnectService.CodeVerifier),
                new KeyValuePair<string, string>("client_assertion_type", "urn:ietf:params:oauth:client-assertion-type:jwt-bearer"),
                new KeyValuePair<string, string>("client_assertion", token),
            });

            content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");

            return content;
        }
    }
}
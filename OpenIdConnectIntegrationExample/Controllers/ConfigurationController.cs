using Microsoft.AspNetCore.Mvc;
using OpenIdConnectIntegrationExample.Models;
using OpenIdConnectIntegrationExample.Service;
using System.Threading.Tasks;

namespace OpenIdConnectIntegrationExample.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class ConfigurationController : ControllerBase
    {
        private readonly ConfigurationService _configurationService;

        public ConfigurationController(ConfigurationService configurationService)
        {
            _configurationService = configurationService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllConfigurations()
        {
            var configurations = await _configurationService.GetConfigurations();
            return Ok(configurations);
        }

        [HttpPost]
        public async Task<IActionResult> SaveConfiguration([FromBody] OIDCConfiguration configuration)
        {
            var savedEntity = await _configurationService.SaveOrUpdateConfiguration(configuration);
            return Ok(savedEntity);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteConfiguration([FromBody] OIDCConfiguration configuration)
        {
            var deletedEntity = await _configurationService.DeleteConfiguration(configuration);
            return Ok(deletedEntity);
        }
    }
}
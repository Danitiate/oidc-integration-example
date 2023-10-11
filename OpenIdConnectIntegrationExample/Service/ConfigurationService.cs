using OpenIdConnectIntegrationExample.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OpenIdConnectIntegrationExample.Service
{
    public class ConfigurationService
    {
        private readonly ConfigurationRepository _configurationRepository;
        private readonly JWTGeneratorService _jwtGeneratorService;

        public ConfigurationService(ConfigurationRepository configurationRepository, JWTGeneratorService jwtGeneratorService) 
        { 
            _configurationRepository = configurationRepository;
            _jwtGeneratorService = jwtGeneratorService;
        }

        public async Task<List<OIDCConfiguration>> GetConfigurations()
        {
            return await _configurationRepository.GetConfigurations();
        }

        public async Task<OIDCConfiguration> SaveOrUpdateConfiguration(OIDCConfiguration configuration)
        {
            if (configuration.Id == null)
            {
                if (string.IsNullOrEmpty(configuration.certificateSerial))
                {
                    configuration.jsonWebKey = _jwtGeneratorService.CreateJWK();
                }

                return await _configurationRepository.AddConfiguration(configuration);
            }
            else
            {
                return await _configurationRepository.UpdateConfiguration(configuration);
            }
        }

        public async Task<OIDCConfiguration> DeleteConfiguration(OIDCConfiguration configuration)
        {
            return await _configurationRepository.DeleteConfiguration(configuration);
        }
    }
}

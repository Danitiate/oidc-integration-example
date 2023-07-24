using OpenIdConnectIntegrationExample.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OpenIdConnectIntegrationExample.Service
{
    public class ConfigurationService
    {
        private readonly ConfigurationRepository _configurationRepository;

        public ConfigurationService(ConfigurationRepository configurationRepository) 
        { 
            _configurationRepository = configurationRepository;
        }

        public async Task<List<OIDCConfiguration>> GetConfigurations()
        {
            return await _configurationRepository.GetConfigurations();
        }

        public async Task<OIDCConfiguration> SaveOrUpdateConfiguration(OIDCConfiguration configuration)
        {
            if (configuration.Id == null)
            {
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

using Microsoft.EntityFrameworkCore;
using OpenIdConnectIntegrationExample.Database;
using OpenIdConnectIntegrationExample.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OpenIdConnectIntegrationExample.Service
{
    public class ConfigurationRepository
    {
        private SampleDbContext _dbContext;

        public ConfigurationRepository(SampleDbContext dbContext) 
        {
            _dbContext = dbContext;
        }

        public async Task<List<OIDCConfiguration>> GetConfigurations()
        {
            return await _dbContext.OIDCConfigurations.ToListAsync();
        }

        public async Task<OIDCConfiguration> AddConfiguration(OIDCConfiguration configuration)
        {
            var newEntity = _dbContext.OIDCConfigurations.Add(configuration);
            await _dbContext.SaveChangesAsync();
            return newEntity.Entity;
        }

        public async Task<OIDCConfiguration> UpdateConfiguration(OIDCConfiguration configuration)
        {
            var updatedEntity = _dbContext.OIDCConfigurations.Update(configuration);
            await _dbContext.SaveChangesAsync();
            return updatedEntity.Entity;
        }

        public async Task<OIDCConfiguration> DeleteConfiguration(OIDCConfiguration configuration)
        {
            var deletedEntity = _dbContext.OIDCConfigurations.Remove(configuration);
            await _dbContext.SaveChangesAsync();
            return deletedEntity.Entity;
        }
    }
}

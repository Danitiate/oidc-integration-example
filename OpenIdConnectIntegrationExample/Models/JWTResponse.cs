using System.Collections.Generic;

namespace OpenIdConnectIntegrationExample.Models
{
    public class JWTResponse
    {
        public IDictionary<string, object> headers { get; set; }
        public IDictionary<string, object> payload { get; set; }
        public string token { get; set; }
    }
}
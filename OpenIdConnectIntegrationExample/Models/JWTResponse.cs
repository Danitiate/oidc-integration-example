using System.Collections.Generic;

namespace OpenIdConnectIntegrationExample.Models
{
    public class JWTResponse
    {
        public IDictionary<string, object> Headers { get; set; }
        public IDictionary<string, object> Payload { get; set; }
        public string Token { get; set; }
    }
}
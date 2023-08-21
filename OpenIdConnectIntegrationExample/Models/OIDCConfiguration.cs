using System;
using System.ComponentModel.DataAnnotations;

namespace OpenIdConnectIntegrationExample.Models
{
    public class OIDCConfiguration
    {
        [Key]
        public Guid? Id { get; set; }
        public string? authority { get; set; }
        public string? audience { get; set; }
        public string? callback_uri { get; set; }
        public string? certificatePassword { get; set; }
        public string? certificateSerial { get; set; }
        public string? client_id { get; set; }
        public string? client_secret { get; set; }
        public string? jsonWebKey { get; set; }
        public string? redirect_uri { get; set; }
        public string? response_type { get; set; }
        public string? scope { get; set; }
    }
}
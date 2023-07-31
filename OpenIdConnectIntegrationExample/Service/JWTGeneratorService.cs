using OpenIdConnectIntegrationExample.Models;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System;
using JWT.Builder;
using System.Linq;
using JWT;
using JWT.Algorithms;
using JWT.Serializers;
using System.IdentityModel.Tokens.Jwt;

namespace OpenIdConnectIntegrationExample.Service
{
    public class JWTGeneratorService
    {
        public JWTGeneratorService() 
        { 
        }

        public JWTResponse GenerateJWTFromConfiguration(OIDCConfiguration configuration)
        {
            var certificateCollection = new X509Certificate2Collection();
            certificateCollection.Import("Certificates/Certificate.p12", configuration.certificatePassword, X509KeyStorageFlags.PersistKeySet);
            var certificate = certificateCollection.FirstOrDefault(certificate => certificate.SerialNumber.Equals(configuration.certificateSerial));

            var jwtData = new JwtData();
            jwtData.Header.Add("x5c", new List<string>() { Convert.ToBase64String(certificate.Export(X509ContentType.Cert)) });
            jwtData.Payload.Add("aud", configuration.audience);
            jwtData.Payload.Add("iss", configuration.client_id);
            jwtData.Payload.Add("iat", UnixEpoch.GetSecondsSince(DateTime.UtcNow));
            jwtData.Payload.Add("exp", UnixEpoch.GetSecondsSince(DateTime.UtcNow.AddMinutes(2)));
            jwtData.Payload.Add("jti", Guid.NewGuid());

            var encoder = new JwtEncoder(
               new RS256Algorithm(certificate.GetRSAPublicKey(), certificate.GetRSAPrivateKey()),
               new JsonNetSerializer(),
               new JwtBase64UrlEncoder());

            var token = encoder.Encode(jwtData.Header, jwtData.Payload, "");

            var JWT = new JWTResponse();
            JWT.Headers = jwtData.Header;
            JWT.Payload = jwtData.Payload;
            JWT.Token = token;
            return JWT;
        }

        public JWTResponse DecodeJWT(string jwtToken)
        {       
            var tokenHandler = new JwtSecurityTokenHandler();
            var decodedToken = tokenHandler.ReadJwtToken(jwtToken);
            var response = new JWTResponse();
            response.Token = jwtToken;
            response.Headers = decodedToken.Header.ToDictionary(h => h.Key, h => (object)h.Value);
            response.Payload = decodedToken.Claims.ToDictionary(c => c.Type, c => (object)c.Value);
            return response;
        }
    }
}

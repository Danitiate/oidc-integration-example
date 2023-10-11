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
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;

namespace OpenIdConnectIntegrationExample.Service
{
    public class JWTGeneratorService
    {
        public JWTGeneratorService()
        {
        }

        public JWTResponse GenerateJWTFromConfiguration(OIDCConfiguration configuration)
        {
            var jwtData = new JwtData();
            jwtData.Payload.Add("aud", configuration.audience);
            jwtData.Payload.Add("iss", configuration.client_id);
            jwtData.Payload.Add("iat", UnixEpoch.GetSecondsSince(DateTime.UtcNow));
            jwtData.Payload.Add("exp", UnixEpoch.GetSecondsSince(DateTime.UtcNow.AddMinutes(2)));
            jwtData.Payload.Add("jti", Guid.NewGuid());
            JwtEncoder encoder;

            if (string.IsNullOrEmpty(configuration.certificateSerial))
            {
                JsonWebKey jwk = new JsonWebKey(configuration.jsonWebKey);
                jwtData.Header.Add("kid", jwk.Kid);

                RSAParameters rsaParametersPrivate = GetRsaParameters(jwk, isPrivateKey: true);
                var rsaPrivate = RSA.Create();
                rsaPrivate.ImportParameters(rsaParametersPrivate);
                RSAParameters rsaParametersPublic = GetRsaParameters(jwk);
                var rsaPublic = RSA.Create();
                rsaPublic.ImportParameters(rsaParametersPublic);

                encoder = new JwtEncoder(
                   new RS256Algorithm(rsaPublic, rsaPrivate),
                   new JsonNetSerializer(),
                   new JwtBase64UrlEncoder());
            }
            else
            {
                var certificateCollection = new X509Certificate2Collection();
                certificateCollection.Import("Certificates/Certificate.p12", configuration.certificatePassword, X509KeyStorageFlags.PersistKeySet);
                var certificate = certificateCollection.FirstOrDefault(certificate => certificate.SerialNumber.Equals(configuration.certificateSerial));

                jwtData.Header.Add("x5c", new List<string>() { Convert.ToBase64String(certificate.Export(X509ContentType.Cert)) });

                encoder = new JwtEncoder(
                   new RS256Algorithm(certificate.GetRSAPublicKey(), certificate.GetRSAPrivateKey()),
                   new JsonNetSerializer(),
                   new JwtBase64UrlEncoder());
            }
            try
            {
                var token = encoder.Encode(jwtData.Header, jwtData.Payload, "");

                var JWT = new JWTResponse();
                JWT.Headers = jwtData.Header;
                JWT.Payload = jwtData.Payload;
                JWT.Token = token;
                return JWT;
            }
            catch (Exception ex)
            {
                return new JWTResponse();
            }
        }

        private RSAParameters GetRsaParameters(JsonWebKey jwk, bool isPrivateKey = false)
        {
            if (isPrivateKey)
            {
                return new RSAParameters
                {
                    Modulus = Base64UrlDecode(jwk.N),
                    Exponent = Base64UrlDecode(jwk.E),
                    D = Base64UrlDecode(jwk.D),
                    P = Base64UrlDecode(jwk.P),
                    Q = Base64UrlDecode(jwk.Q),
                    DP = Base64UrlDecode(jwk.DP),
                    DQ = Base64UrlDecode(jwk.DQ),
                    InverseQ = Base64UrlDecode(jwk.QI)
                };
            }
            return new RSAParameters
            {
                Modulus = Base64UrlDecode(jwk.N),
                Exponent = Base64UrlDecode(jwk.E)
            };
        }

        private byte[] Base64UrlDecode(string base64Url)
        {
            string base64 = base64Url.Replace('-', '+').Replace('_', '/');
            while (base64.Length % 4 != 0)
            {
                base64 += '=';
            }
            return Convert.FromBase64String(base64);
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

        public string CreateJWK()
        {
            RSA rsa = RSA.Create(2048);
            RsaSecurityKey publicAndPrivateKey = new(rsa.ExportParameters(true))
            {
                KeyId = Guid.NewGuid().ToString()
            };

            JsonWebKey jwk = JsonWebKeyConverter.ConvertFromRSASecurityKey(publicAndPrivateKey);
            jwk.Alg = "RS256";

            return JsonExtensions.SerializeToJson(jwk);
        }
    }
}

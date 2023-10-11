using System.Text;
using System;
using System.Security.Cryptography;

namespace OpenIdConnectIntegrationExample.Service
{
    public class OpenIDConnectService
    {
        const string VALID_CHARS = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789-._~";
        const int CODE_VERIFIER_MAX_LENGTH = 128;
        public static string CodeVerifier = ""; // TODO: Proper caching / storage

        public OpenIDConnectService() 
        { 
        }

        public string GenerateCodeChallenge()
        {
            GenerateCodeVerifier();
            using (var sha256 = SHA256.Create())
            {
                byte[] challengeBytes = sha256.ComputeHash(Encoding.ASCII.GetBytes(CodeVerifier));
                string codeChallenge = Base64UrlEncode(challengeBytes);
                return codeChallenge;
            }
        }

        private void GenerateCodeVerifier()
        {
            var random = new Random();
            var codeVerifierBuilder = new StringBuilder(128);
            for (int i = 0; i < CODE_VERIFIER_MAX_LENGTH; i++)
            {
                codeVerifierBuilder.Append(VALID_CHARS[random.Next(VALID_CHARS.Length)]);
            }

            CodeVerifier = codeVerifierBuilder.ToString();
        }


        private string Base64UrlEncode(byte[] data)
        {
            string base64 = Convert.ToBase64String(data);
            base64 = base64
                .Replace("+", "-")
                .Replace("/", "_")
                .Replace("=", "");
            return base64;
        }
    }
}

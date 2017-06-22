using InvestNinja.Core.Domain;
using Microsoft.AspNetCore.Http;
using System.Text;

namespace InvestNinja.Web.Utils
{
    public static class HttpExtender
    {
        public static T DecodeJWT<T>(this HttpRequest request, string secretKey) => Jose.JWT.Decode<T>(request.Headers["Authorization"], Encoding.UTF8.GetBytes(secretKey));

        public static string DecodeUserNameJWT(this HttpRequest request, string secretKey) => request.DecodeJWT<AuthorizationToken>(secretKey).UserName;
    }
}

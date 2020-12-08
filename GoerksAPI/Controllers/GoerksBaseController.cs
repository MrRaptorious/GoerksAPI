using AnanasCore;
using AnanasCore.Criteria;
using GoerksAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Logging;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace GoerksAPI.Controllers
{
    public class GoerksBaseController : ControllerBase
    {
        protected readonly ObjectSpace os;

        public GoerksBaseController()
        {
            os = AnanasApplication.GetApplication().DefaultSubManager.CreateObjectSpace(true);
        }

        protected TokenData GetTokenData()
        {
            var tok = Request.Headers["Authorization"].ToString().Replace("Bearer ", string.Empty);

            var handler = new JwtSecurityTokenHandler();
            //var tokenS = handler.ReadToken(headerValue.Parameter) as JwtSecurityToken;
            var tokenS = new System.IdentityModel.Tokens.Jwt.JwtSecurityToken(tok);

            var email = tokenS.Claims.First(claim => claim.Type == JwtRegisteredClaimNames.Email).Value;
            var id = tokenS.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.UniqueName).Value;

            return new TokenData(email, new Guid(id));
        }

        protected User GetUserFromToken()
        {
            var data = GetTokenData();

            return os.GetObject<User>(data.ID);
        }

        protected DateTime DateTimeFromUnixTimeStamp(long unixTime)
        {
            // Unix timestamp is seconds past epoch
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTime).ToLocalTime();
            return dtDateTime;
        }
    }

    public struct TokenData
    {
        public string Email { get; }
        public Guid ID { get; }

        public TokenData(string email, Guid id)
        {
            Email = email;
            ID = id;
        }
    }
}

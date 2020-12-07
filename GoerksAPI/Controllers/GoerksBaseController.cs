using AnanasCore;
using AnanasCore.Criteria;
using GoerksAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace GoerksAPI.Controllers
{
    public class GoerksBaseController : ControllerBase
    {
        protected readonly ObjectSpace os;

        public GoerksBaseController()
        {
            os = AnanasApplication.GetApplication().GetDefaultSubManager().CreateObjectSpace(true);
        }

        protected TokenData GetTokenData()
        {
            var tok = Request.Headers[HeaderNames.Authorization];

            // Debug token
            var token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJlbWFpbCI6IkNvb2xlckBsb2xvLmRlIiwidW5pcXVlX25hbWUiOiJhZmI3NDlmYS0wYWY1LTQxOWUtYmZkYS04MWFiNThhM2IyYjIiLCJuYmYiOjE2MDcwMDI1NzQsImV4cCI6MTYwNzA4ODk3NCwiaWF0IjoxNjA3MDAyNTc0LCJpc3MiOiJzZWxmIiwiYXVkIjoic2VsZiJ9._sONgQHl02oeoi27Cpz3p3gI5Xn_90i-26lPIiBzxh4";

            var handler = new JwtSecurityTokenHandler();
            var tokenS = handler.ReadToken(token) as JwtSecurityToken;

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

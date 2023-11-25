using AngularWebApi.DB.GenericRepository;
using AngularWebApi.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AngularWebApi.Controllers
{
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IRepository<User> _userRepository;

        public AuthController(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }
        /// <summary>
        /// Get OAuth token
        /// </summary>
        /// <returns></returns>
        [Route("api/OAuth")]
        [HttpPost]
        public async Task<IActionResult> GetAuthTokenAsync()
        {
            try
            {
                var claims = await ValidateUserCredentialsAsync();
                if (claims == null || claims.Count == 0)
                {
                    return BadRequest("Invalid credentials");
                }
                var token = GenerateToken(claims);
                return Ok(token);
            }
            catch (Exception ex)
            {
                return BadRequest("Invalid credentials");
            }
        }

        /// <summary>
        /// Get OAuth token
        /// </summary>
        /// <returns></returns>
        [Route("api/OAuthRefreshToken")]
        [HttpPost]
        public async Task<IActionResult> OAuthRefreshToken(TokenRequest tokenRequest)
        {
            try
            {
                var claims = await ValidateUserByRefreshTokenAsync(tokenRequest.UserId, tokenRequest.RefreshToken);
                if (claims == null || claims.Count == 0)
                {
                    return BadRequest("Invalid credentials");
                }
                var token = GenerateToken(claims);
                return Ok(token);
            }
            catch (Exception ex)
            {
                return BadRequest("Invalid credentials");
            }
        }

        private async Task<IDictionary<string, object>> ValidateUserCredentialsAsync()
        {
            var authHeader = HttpContext.Request.Headers["Authorization"].ToString();
            IDictionary<string, Object> claims = new Dictionary<string, Object>();
            string username = string.Empty;
            string password = string.Empty;
            if (authHeader != null && authHeader.StartsWith("Basic"))
            {
                string encodedUsernamePassword = authHeader.Substring("Basic ".Length).Trim();
                Encoding encoding = Encoding.GetEncoding("iso-8859-1");
                string usernamePassword = encoding.GetString(Convert.FromBase64String(encodedUsernamePassword));
                var usercredcollection = usernamePassword.Split(":");
                username = usercredcollection[0];
                password = usercredcollection[1];
            }
            else
            {
                //Handle what happens if that isn't the case
                throw new Exception("The authorization header is either empty or isn't Basic.");
            }

            var userDetails = _userRepository.Get(x => x.UserName.Equals(username))?.ToList();
            if (userDetails != null)
            {
                foreach (var user in userDetails)
                {
                    if ((password.Equals(user.Password, StringComparison.CurrentCulture)))
                    {
                        var refreshtoken = Guid.NewGuid().ToString();
                        claims.Add("UserId", user.UserId);
                        claims.Add("UserName", user.UserName);
                        claims.Add("Role", "Admin");
                        claims.Add("RoleID", 1);
                        claims.Add("RefreshToken", refreshtoken);
                        //Update refresh token
                        user.RefreshToken = refreshtoken;
                        await _userRepository.UpdateAsync(user);
                        break;
                    }
                }
            }
            if (claims.Count() > 0)
                return claims;
            else
                throw new Exception("Invalid Login");
        }

        private async Task<IDictionary<string, object>> ValidateUserByRefreshTokenAsync(int userId, string refreshToken)
        {
            IDictionary<string, Object> claims = new Dictionary<string, Object>();
            var userDetails = _userRepository.Get(x => x.UserId.Equals(userId))?.ToList();
            if (userDetails != null)
            {
                foreach (var user in userDetails)
                {
                    if ((refreshToken.Equals(user.RefreshToken, StringComparison.CurrentCulture)))
                    {
                        var refreshtoken = Guid.NewGuid().ToString();
                        claims.Add("UserId", user.UserId);
                        claims.Add("UserName", user.UserName);
                        claims.Add("Role", "Admin");
                        claims.Add("RoleID", 1);
                        claims.Add("RefreshToken", refreshtoken);
                        //Update refresh token
                        user.RefreshToken = refreshtoken;
                        await _userRepository.UpdateAsync(user);
                        break;
                    }
                }
            }
            if (claims.Count() > 0)
                return claims;
            else
                throw new Exception("Invalid Login");
        }
        private JwtResponseModel GenerateToken(IDictionary<string, object> emplyeeClaims)
        {
            var key = "EWRWDSERkljouiojljhui767y767kjhkhk76767";
            var issuer = "TestAngular";
            var audience = "AngularProject";
            var expiryInSec = 36000;

            // 1. Create Security Token Handler
            var tokenHandler = new JwtSecurityTokenHandler();

            // 2. Create Private Key to Encrypted
            var tokenKey = Encoding.ASCII.GetBytes(key);

            //3. Create JETdescriptor            
            var expiry = DateTime.UtcNow.AddSeconds(Convert.ToInt64(expiryInSec));
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(
                    new Claim[]
                    {
                        new Claim("UserName", emplyeeClaims["UserName"].ToString()),
                        new Claim("RoleID", emplyeeClaims["RoleID"].ToString()),
                        new Claim("Role", emplyeeClaims["Role"].ToString())
                    }),
                Expires = expiry,
                Audience = audience,
                Issuer = issuer,
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };
            //4. Create Token
            var token = tokenHandler.CreateToken(tokenDescriptor);

            // 6. Return Token from method
            return new JwtResponseModel()
            {
                AccessToken = tokenHandler.WriteToken(token),
                UserID = Convert.ToInt16(emplyeeClaims["UserId"].ToString()),
                UserName = emplyeeClaims["UserName"].ToString(),
                Role = emplyeeClaims["Role"].ToString(),
                RoleID = Convert.ToInt16(emplyeeClaims["RoleID"].ToString()),
                RefreshToken = emplyeeClaims["RefreshToken"].ToString(),
                CreatedDate = DateTime.UtcNow,
                ExpiredDate = expiry,
            };
        }
    }
}

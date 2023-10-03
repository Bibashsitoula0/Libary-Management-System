using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Bookhive.Service.CurrentUserService
{
    public class CurrentUserService : ICurrentUserService
    {
        public string UserId { get; }
        public string Username { get; }
        public string Email { get; }
        public string RoleId { get; }
        public string Role { get; }
        public bool IsInternalUser { get; }
        public IEnumerable<string> Roles { get; }
        public List<Guid> EnterpriseGroups { get; }

        private IHttpContextAccessor _httpContextAccessor;

        public static string GetId(IHttpContextAccessor httpContextAccessor)
        {
            ClaimsIdentity claimsIdentity = httpContextAccessor.HttpContext.User.Identity as ClaimsIdentity;

            Claim claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            return claim.Value;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="httpContextAccessor"></param>
        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            if (httpContextAccessor.HttpContext.User == null) return;
            _httpContextAccessor = httpContextAccessor;
            //var s = GetId(httpContextAccessor);
            var userId = _httpContextAccessor.HttpContext.User.FindFirst("UserId")?.Value;
            if (!string.IsNullOrEmpty(userId))
                UserId = userId;

            var username = _httpContextAccessor.HttpContext.User.FindFirst("UserName")?.Value;
            if (!string.IsNullOrEmpty(username))
                Username = username;

            var email = _httpContextAccessor.HttpContext.User.FindFirst("Email")?.Value;
            if (!string.IsNullOrEmpty(email))
                Email = email;

            var role = _httpContextAccessor.HttpContext.User.FindFirst("Role")?.Value;
            if (!string.IsNullOrEmpty(role))
                Role = role;

            var roleId = _httpContextAccessor.HttpContext.User.FindFirst("RoleId")?.Value;
            if (!string.IsNullOrEmpty(roleId))
                RoleId = roleId;
            var roles = httpContextAccessor.HttpContext.User.FindAll("Role");
            Roles = roles.Select(x => x.Value).ToList();
        }


    }
}

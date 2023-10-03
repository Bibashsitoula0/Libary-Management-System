using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookhive.Service.CurrentUserService
{
    public interface ICurrentUserService
    {
        /// <summary>
        /// User id
        /// </summary>
        string UserId { get; }

        /// <summary>
        /// Username
        /// </summary>
        string Username { get; }

        /// <summary>
        /// Email
        /// </summary>
        string Email { get; }

        /// <summary>
        /// Internal user
        /// </summary>
        bool IsInternalUser { get; }

        /// <summary>
        /// User roles
        /// </summary>
        IEnumerable<string> Roles { get; }

        /// <summary>
        /// RoleId
        /// </summary>
        public string RoleId { get; }

        /// <summary>
        /// Role
        /// </summary>
        public string Role { get; }

        /// <summary>
        /// Enterprise groups the user is attached to
        /// </summary>
        List<Guid> EnterpriseGroups { get; }
    }
}

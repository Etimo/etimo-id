using Etimo.Id.Entities;
using System.Threading.Tasks;

namespace Etimo.Id.Abstractions
{
    public interface ICreateAuditLogService
    {
        Task CreateFailedLoginAuditLogAsync(User user);
        Task CreateAuthorizationCodeAbuseAuditLogAsync(AuthorizationCode code);
        Task CreateRefreshTokenAbuseAuditLogAsync(RefreshToken refreshToken);
    }
}

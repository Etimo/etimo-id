using Etimo.Id.Abstractions;
using Etimo.Id.Entities;
using Etimo.Id.Entities.Abstractions;
using System;
using System.Threading.Tasks;

namespace Etimo.Id.Service
{
    public class LockUserService : ILockUserService
    {
        private readonly IApplicationRepository _applicationRepository;
        private readonly ICreateAuditLogService _createAuditLogService;
        private readonly IRequestContext        _requestContext;
        private readonly IUserRepository        _userRepository;

        public LockUserService(
            IUserRepository userRepository,
            IApplicationRepository applicationRepository,
            ICreateAuditLogService createAuditLogService,
            IRequestContext requestContext)
        {
            _userRepository        = userRepository;
            _applicationRepository = applicationRepository;
            _createAuditLogService = createAuditLogService;
            _requestContext        = requestContext;
        }

        public async Task LockAsync(User user)
        {
            Application application = await _applicationRepository.FindByClientIdAsync(_requestContext.ClientId.Value);
            user.FailedLogins++;
            if (user.FailedLogins >= application.FailedLoginsBeforeLocked)
            {
                user.LockedUntilDateTime = DateTime.UtcNow.AddMinutes(application.FailedLoginsLockLifetimeMinutes);
                user.FailedLogins        = 0;

                await _createAuditLogService.CreateFailedLoginAuditLogAsync(user, application);
            }

            await _userRepository.SaveAsync();
        }
    }
}

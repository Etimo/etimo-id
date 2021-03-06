using Etimo.Id.Abstractions;
using Etimo.Id.Entities;
using Etimo.Id.Exceptions;
using System;
using System.Threading.Tasks;

namespace Etimo.Id.Service
{
    public class FindUserService : IFindUserService
    {
        private readonly IUserRepository _userRepository;

        public FindUserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> FindAsync(Guid userId)
        {
            User user = await _userRepository.FindAsync(userId);
            if (user == null) { throw new NotFoundException(); }

            return user;
        }
    }
}

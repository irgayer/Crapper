using AutoMapper;

using Crapper.Interfaces;
using Crapper.Models;

using MediatR;

namespace Crapper.Features.UserFeatures.Commands.AddUser
{
    public class AddUserCommandHandler : IRequestHandler<AddUserCommand, bool>
    {
        private readonly IRepository<User> _userRepository;
        private readonly IMapper _mapper;

        public AddUserCommandHandler(IRepository<User> userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<bool> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            var user = _mapper.Map<User>(request.Dto);

            var candidate = _userRepository.Find(u => u.Username == request.Dto.Username || u.Email == request.Dto.Email).SingleOrDefault();
            if (candidate != null)
                return false;

            await _userRepository.Add(user);
            await _userRepository.Save();

            return true;
        }
    }
}

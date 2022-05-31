using AutoMapper;

using Crapper.DTOs.User;
using Crapper.Interfaces;
using Crapper.Models;

using MediatR;

namespace Crapper.Features.UserFeatures.Queries.GetUserById
{
    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserDto>
    {
        private readonly IRepository<User> _userRepository;
        private readonly IMapper _mapper;

        public GetUserByIdQueryHandler(IRepository<User> userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<UserDto> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetById(request.Id);
            return _mapper.Map<UserDto>(user);
        }
    }
}

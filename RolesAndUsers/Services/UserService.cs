using AutoMapper;
using RolesAndUsers.Dtos;
using RolesAndUsers.Repositories;
using RolesAndUsers.Models;
using RolesAndUsers.Exceptions;

namespace RolesAndUsers.Services;

public class UserService : IUserService
{
    private readonly IMapper _mapper;
    private readonly IUserRepository _userRepository;


    public UserService(IMapper mapper, IUserRepository userRepository)
    {
        _mapper = mapper;
        _userRepository = userRepository;
    }

    public async Task<IEnumerable<UserDto>> GetUsers()
    {
        return _mapper.Map<List<UserDto>>(await _userRepository.GetUsers());
    }

    public async Task<UserDto> GetUser(Guid id)
    {
        var user = await _userRepository.GetUser(id);

        if (user == null)
        {
            throw new NotFoundException(id.ToString());
        }

        var userDto = _mapper.Map<UserDto>(user);

        return userDto;
    }

    public async Task<UserDto> AddUser(UserDto userDto)
    {
        if (userDto == null)
        {
            throw new BadRequestException("User is null");
        }

        var user = _mapper.Map<User>(userDto);

        return _mapper.Map<UserDto>(await _userRepository.AddUser(user)); ;
    }

    public async Task DeleteUser(Guid id)
    {
        var user = await _userRepository.GetUser(id);

        if (user == null)
        {
            throw new NotFoundException(id.ToString());
        }

        await _userRepository.DeleteUser(id);
    }

    public async Task UpdateUser(Guid id, UserDto userDto)
    {
        if (id != userDto.Id)
        {
            throw new BadRequestException("User ID mismatch");
        }

        var user = _mapper.Map<User>(userDto);

        var userToUpdate = await _userRepository.GetUser(id);

        if (userToUpdate == null)
        {
            throw new NotFoundException(id.ToString());
        }

        await _userRepository.UpdateUser(user);
    }
}

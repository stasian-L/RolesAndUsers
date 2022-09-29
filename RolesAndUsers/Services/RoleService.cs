using AutoMapper;
using RolesAndUsers.Dtos;
using RolesAndUsers.Exceptions;
using RolesAndUsers.Models;
using RolesAndUsers.Repositories;

namespace RolesAndUsers.Services;

public class RoleService : IRoleService
{
    private readonly IMapper _mapper;
    private readonly IRoleRepository _roleRepository;

    public RoleService(IMapper mapper, IRoleRepository roleRepository)
    {
        _mapper = mapper;
        _roleRepository = roleRepository;
    }

    public async Task<RoleDto> AddRole(RoleDto roleDto)
    {
        if (roleDto == null)
        {
            throw new BadRequestException("Role is null");
        }

        var role = _mapper.Map<Role>(roleDto);

        return _mapper.Map<RoleDto>(await _roleRepository.AddRole(role));
    }

    public async Task DeleterRole(Guid id)
    {
        var role = await _roleRepository.GetRole(id);

        if (role == null)
        {
            throw new NotFoundException(id.ToString());
        }

        await _roleRepository.DeleteRole(id);
    }

    public async Task<RoleDto> GetRole(Guid id)
    {
        var role = await _roleRepository.GetRole(id);

        if (role == null)
        {
            throw new NotFoundException(id.ToString());
        }

        var roleDto = _mapper.Map<RoleDto>(role);

        return roleDto; ;
    }

    public async Task<IEnumerable<RoleDto>> GetRoles()
    {
        return _mapper.Map<List<RoleDto>>(await _roleRepository.GetRoles());
    }

    public async Task UpdateRole(Guid id, RoleDto roleDto)
    {
        if (id != roleDto.Id)
        {
            throw new BadRequestException("Role ID mismatch");
        }

        var role = _mapper.Map<Role>(roleDto);

        var roleToUpdate = await _roleRepository.GetRole(id);

        if (roleToUpdate == null)
        {
            throw new NotFoundException(id.ToString());
        }

        await _roleRepository.UpdateRole(role);
    }
}

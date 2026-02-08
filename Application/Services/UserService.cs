using System.Net;
using Microsoft.Extensions.Logging;
using AutoMapper;
using WebApi.DTOs;

public class UserService(IMapper mapper,IUserRepository UserRepository) : IUserService
{
    private readonly IUserRepository _UserRepository = UserRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<Response<string>> AddUserAsync(UserInsertDto UserInsertDto)
    {
        var User = _mapper.Map<User>(UserInsertDto);
        return await _UserRepository.AddUserAsync(User);
    }

    public async Task<Response<string>> DeleteAsync(int UserId)
    {
        return await _UserRepository.DeleteAsync(UserId);
    }

    public async Task<List<UserGetAsWaiterDto>> GetAllOrdersOfWaiterByIdAsync(int WaiterId)
    {
        var Users = await _UserRepository.GetAllOrdersOfWaiterByIdAsync(WaiterId);
        return _mapper.Map<List<UserGetAsWaiterDto>>(Users);
    }

    public async Task<PagedResult<UserGetDto>> GetAllUsersAsync(UserFilter filter, PagedQuery query)
    {
        var Users = await _UserRepository.GetAllUsersAsync(filter,query);
        return _mapper.Map<PagedResult<UserGetDto>>(Users);
    }

    public async Task<Response<UserGetDto>> GetUserByIdAsync(int UserId)
    {
        var User = await _UserRepository.GetUserByIdAsync(UserId);
        return _mapper.Map<Response<UserGetDto>>(User);
    }

    public async Task<Response<string>> UpdateAsync(int UserId,UserUpdateDto UserUpdateDto)
    {
        var User = _mapper.Map<User>(UserUpdateDto); 
        return await _UserRepository.UpdateAsync(UserId,User);
    }
}
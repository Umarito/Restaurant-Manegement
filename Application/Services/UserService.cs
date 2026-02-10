using System.Net;
using Microsoft.Extensions.Logging;
using AutoMapper;
using WebApi.DTOs;

public class UserService(IMapper mapper,IUserRepository UserRepository,ILogger<UserService> logger) : IUserService
{
    private readonly IUserRepository _UserRepository = UserRepository;
    private readonly IMapper _mapper = mapper;
    private readonly ILogger<UserService> _logger = logger;

    public async Task<Response<string>> AddUserAsync(UserInsertDto UserInsertDto)
    {
        try
        {
            var User = _mapper.Map<User>(UserInsertDto);
            await _UserRepository.AddAsync(User);
            return new Response<string>(HttpStatusCode.OK, "User was added successfully");
        }
        catch(Exception ex)
        {
            _logger.LogWarning(ex.Message);
            return new Response<string>(HttpStatusCode.InternalServerError, "Internal Server Error");
        }
    }

    public async Task<Response<string>> DeleteAsync(int UserId)
    {
        try
        {
            await _UserRepository.DeleteAsync(UserId);
            return new Response<string>(HttpStatusCode.OK, "User was deleted successfully");
        }
        catch(Exception ex)
        {
            _logger.LogWarning(ex.Message);
            return new Response<string>(HttpStatusCode.InternalServerError, "Internal Server Error");
        }
    }

    public async Task<Response<UserGetDto>> GetUserByIdAsync(int UserId)
    {
        try
        {
            var User = await _UserRepository.GetByIdAsync(UserId);
            var res = _mapper.Map<UserGetDto>(User);
            return new Response<UserGetDto>(HttpStatusCode.OK,"The data that you were searching for:",res);
        }
        catch(Exception ex)
        {
            _logger.LogWarning(ex.Message);
            return new Response<UserGetDto>(HttpStatusCode.InternalServerError, "Internal Server Error");
        }
    }

    public async Task<Response<string>> UpdateAsync(int UserId, UserUpdateDto User)
    {
        try
        {
            var res = await _UserRepository.GetByIdAsync(UserId);

            if (res == null)
            {   
                return new Response<string>(HttpStatusCode.NotFound,"User not found");
            }
            else
            {
                _mapper.Map(User, res);
                await _UserRepository.UpdateAsync(res);
                return new Response<string>(HttpStatusCode.OK,"User updated successfully");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return new Response<string>(HttpStatusCode.InternalServerError,"Internal Server Error");
        }
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
}
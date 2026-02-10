using System.Net;
using Microsoft.Extensions.Logging;
using AutoMapper;
using WebApi.DTOs;

public class TableService(IMapper mapper,ITableRepository TableRepository,ILogger<TableService> logger) : ITableService
{
    private readonly ITableRepository _TableRepository = TableRepository;
    private readonly IMapper _mapper = mapper;
    private readonly ILogger<TableService> _logger = logger;

    public async Task<Response<string>> AddTableAsync(TableInsertDto TableInsertDto)
    {
        try
        {
            var Table = _mapper.Map<Table>(TableInsertDto);
            await _TableRepository.AddAsync(Table);
            return new Response<string>(HttpStatusCode.OK, "Table was added successfully");
        }
        catch(Exception ex)
        {
            _logger.LogWarning(ex.Message);
            return new Response<string>(HttpStatusCode.InternalServerError, "Internal Server Error");
        }
    }

    public async Task<Response<string>> DeleteAsync(int TableId)
    {
        try
        {
            await _TableRepository.DeleteAsync(TableId);
            return new Response<string>(HttpStatusCode.OK, "Table was deleted successfully");
        }
        catch(Exception ex)
        {
            _logger.LogWarning(ex.Message);
            return new Response<string>(HttpStatusCode.InternalServerError, "Internal Server Error");
        }
    }

    public async Task<Response<TableGetDto>> GetTableByIdAsync(int TableId)
    {
        try
        {
            var Table = await _TableRepository.GetByIdAsync(TableId);
            var res = _mapper.Map<TableGetDto>(Table);
            return new Response<TableGetDto>(HttpStatusCode.OK,"The data that you were searching for:",res);
        }
        catch(Exception ex)
        {
            _logger.LogWarning(ex.Message);
            return new Response<TableGetDto>(HttpStatusCode.InternalServerError, "Internal Server Error");
        }
    }

    public async Task<Response<string>> UpdateAsync(int TableId, TableUpdateDto Table)
    {
        try
        {
            var res = await _TableRepository.GetByIdAsync(TableId);

            if (res == null)
            {   
                return new Response<string>(HttpStatusCode.NotFound,"Table not found");
            }
            else
            {
                _mapper.Map(Table, res);
                await _TableRepository.UpdateAsync(res);
                return new Response<string>(HttpStatusCode.OK,"Table updated successfully");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return new Response<string>(HttpStatusCode.InternalServerError,"Internal Server Error");
        }
    }
    
    public async Task<PagedResult<TableGetDto>> GetAllTablesAsync(TableFilter filter, PagedQuery query)
    {
        var Tables = await _TableRepository.GetAllTablesAsync(filter,query);
        return _mapper.Map<PagedResult<TableGetDto>>(Tables);
    }
}
using System.Net;
using Microsoft.Extensions.Logging;
using AutoMapper;
using WebApi.DTOs;

public class TableService(IMapper mapper,ITableRepository TableRepository) : ITableService
{
    private readonly ITableRepository _TableRepository = TableRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<Response<string>> AddTableAsync(TableInsertDto TableInsertDto)
    {
        var Table = _mapper.Map<Table>(TableInsertDto);
        return await _TableRepository.AddTableAsync(Table);
    }

    public async Task<Response<string>> DeleteAsync(int TableId)
    {
        return await _TableRepository.DeleteAsync(TableId);
    }

    public async Task<PagedResult<TableGetDto>> GetAllTablesAsync(TableFilter filter, PagedQuery query)
    {
        var Tables = await _TableRepository.GetAllTablesAsync(filter,query);
        return _mapper.Map<PagedResult<TableGetDto>>(Tables);
    }

    public async Task<Response<TableGetDto>> GetTableByIdAsync(int TableId)
    {
        var Table = await _TableRepository.GetTableByIdAsync(TableId);
        return _mapper.Map<Response<TableGetDto>>(Table);
    }

    public async Task<Response<string>> UpdateAsync(int TableId,TableUpdateDto TableUpdateDto)
    {
        var Table = _mapper.Map<Table>(TableUpdateDto); 
        return await _TableRepository.UpdateAsync(TableId,Table);
    }
}
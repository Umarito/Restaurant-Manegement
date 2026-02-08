using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApi.DTOs;

[ApiController]
[Route("api/[controller]")]
public class TablesController(ITableService TableService) : ControllerBase
{
    [HttpPost]
    public async Task<Response<string>> AddTableAsync(TableInsertDto Table)
    {
        return await TableService.AddTableAsync(Table);
    }
    [HttpPut("{TableId}")]
    public async Task<Response<string>> UpdateAsync(int TableId,TableUpdateDto Table)
    {
        return await TableService.UpdateAsync(TableId,Table);
    }
    [HttpDelete("{TableId}")]
    public async Task<Response<string>> DeleteAsync(int TableId)
    {
        return await TableService.DeleteAsync(TableId);
    }
    [HttpGet]
    public async Task<PagedResult<TableGetDto>> GetAllTables([FromQuery] TableFilter filter, [FromQuery] PagedQuery pagedQuery)
    {
        return await TableService.GetAllTablesAsync(filter, pagedQuery);   
    }
    
    [HttpGet("{TableId}")]
    public async Task<Response<TableGetDto>> GetTableByIdAsync(int TableId)
    {
        return await TableService.GetTableByIdAsync(TableId);
    }
}
using RestApi.Core.DTOs;
using RestApi.Core.DTOs.Common;

namespace RestApi.Core.Services.Quote
{
    public interface IQuoteService
    {
        Task<ApiResult<IEnumerable<QuoteDto>>> GetAll();
        Task<ApiResult<QuoteDto>> GetById(int id);
        Task<ApiResult<QuoteDto>> GetQuoteOfTheDay();
        Task<ApiResult<QuoteDto>> Insert(EditQuoteDto quoteDto);
        Task<ApiResult<QuoteDto>> Update(int id, EditQuoteDto quoteDto);
        Task<ApiResult<string>> Delete(int id);
    }
}
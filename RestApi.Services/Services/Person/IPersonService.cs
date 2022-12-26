using RestApi.Core.DTOs;
using RestApi.Core.DTOs.Common;

namespace RestApi.Core.Services.Person
{
    public interface IPersonService
    {
        Task<ApiResult<IEnumerable<PersonDto>>> GetAll();
        Task<ApiResult<PersonDto>> GetById(int id);
        Task<ApiResult<PersonDto>> Insert(PersonDto personDto);
        Task<ApiResult<PersonDto>> Update(int id, PersonDto personDto);
        Task<ApiResult<string>> Delete(int id);
    }
}
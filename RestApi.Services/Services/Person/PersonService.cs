using AutoMapper;
using Microsoft.Extensions.Logging;
using RestApi.Core.DTOs;
using RestApi.Core.DTOs.Common;
using RestApi.Core.Interfaces.Repositories;

#nullable disable

namespace RestApi.Core.Services.Person
{
    public class PersonService : IPersonService
    {
        private readonly IPersonRepository _personRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<PersonService> _logger;

        public PersonService(IPersonRepository personRepository, IMapper mapper, ILogger<PersonService> logger)
        {
            _personRepository = personRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ApiResult<string>> Delete(int id)
        {
            try
            {
                var result = await _personRepository.GetById(id);
                if (result == null)
                    return new ApiResult<string>(null);

                await _personRepository.Delete(id);
                return new ApiResult<string>("Deleted.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }

            return new ApiResult<string>(false, "Error deleting person.");
        }

        public async Task<ApiResult<IEnumerable<PersonDto>>> GetAll()
        {
            try
            {
                return new ApiResult<IEnumerable<Entities.Person>, IEnumerable<PersonDto>>(_mapper, await _personRepository.GetAll());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return new ApiResult<IEnumerable<PersonDto>>(false, "Error retrieving person data from the database.");
            }
        }

        public async Task<ApiResult<PersonDto>> GetById(int id)
        {
            try
            {
                return new ApiResult<Entities.Person, PersonDto>(_mapper, await _personRepository.GetById(id));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return new ApiResult<PersonDto>(false, "Error retrieving person data from the database.");
            }
        }

        public async Task<ApiResult<PersonDto>> Insert(PersonDto personDto)
        {
            try
            {
                if (personDto.Id != 0)
                    return new ApiResult<PersonDto>(false, "Person Id is identity, can't be modified.");

                Entities.Person personToInsert = _mapper.Map<PersonDto, Entities.Person>(personDto);
                var result = await _personRepository.Insert(personToInsert);
                return new ApiResult<Entities.Person, PersonDto>(_mapper, await _personRepository.GetById(result.Id));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return new ApiResult<PersonDto>(false, "Error creating new person record.");
            }
        }

        public async Task<ApiResult<PersonDto>> Update(int id, PersonDto personDto)
        {
            try
            {
                if (id != personDto.Id)
                    return new ApiResult<PersonDto>(false, "Person Id mismatch.");

                Entities.Person personToUpdate = _mapper.Map<PersonDto, Entities.Person>(personDto);
                personToUpdate.Id = id;
                await _personRepository.Update(personToUpdate);
                return new ApiResult<Entities.Person, PersonDto>(_mapper, await _personRepository.GetById(id));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return new ApiResult<PersonDto>(false, "Error updating person.");
            }
        }
    }
}
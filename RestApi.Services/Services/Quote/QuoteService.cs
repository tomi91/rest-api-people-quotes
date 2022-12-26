using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using RestApi.Core.DTOs;
using RestApi.Core.DTOs.Common;
using RestApi.Core.Entities;
using RestApi.Core.Interfaces.Repositories;

#nullable disable

namespace RestApi.Core.Services.Quote
{
    public class QuoteService : IQuoteService
    {
        private readonly IQuoteRepository _quoteRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<QuoteService> _logger;
        private IMemoryCache _memoryCache;

        private const string QuoteOfTheDayCacheKey = "QuoteOfTheDayCacheKey";

        public QuoteService(IQuoteRepository quoteRepository, IMapper mapper, ILogger<QuoteService> logger, IMemoryCache memoryCache)
        {
            _quoteRepository = quoteRepository;
            _mapper = mapper;
            _logger = logger;
            _memoryCache = memoryCache;
        }

        public async Task<ApiResult<string>> Delete(int id)
        {
            try
            {
                var result = await _quoteRepository.GetById(id);
                if (result == null)
                    return new ApiResult<string>(null);

                await _quoteRepository.Delete(id);
                return new ApiResult<string>("Deleted.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }

            return new ApiResult<string>(false, "Error deleting Quote.");
        }

        public async Task<ApiResult<IEnumerable<QuoteDto>>> GetAll()
        {
            try
            {
                return new ApiResult<IEnumerable<Entities.Quote>, IEnumerable<QuoteDto>>(_mapper, await _quoteRepository.GetAll());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return new ApiResult<IEnumerable<QuoteDto>>(false, "Error retrieving Quote data from the database.");
            }
        }

        public async Task<ApiResult<QuoteDto>> GetById(int id)
        {
            try
            {
                return new ApiResult<Entities.Quote, QuoteDto>(_mapper, await _quoteRepository.GetById(id));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return new ApiResult<QuoteDto>(false, "Error retrieving Quote data from the database.");
            }
        }

        public async Task<ApiResult<QuoteDto>> GetQuoteOfTheDay()
        {
            try
            {
                int? id = _memoryCache.Get<int?>(QuoteOfTheDayCacheKey);
                if (!id.HasValue)
                {
                    id = await _quoteRepository.GetQuoteOfTheDay();
                    DateTime midnight = DateTime.Now.AddDays(1).Date;
                    _memoryCache.Set(QuoteOfTheDayCacheKey, id.Value, midnight);
                }

                return new ApiResult<Entities.Quote, QuoteDto>(_mapper, await _quoteRepository.GetById(id.Value));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return new ApiResult<QuoteDto>(false, "Error retrieving Quote data from the database.");
            }
        }

        public async Task<ApiResult<QuoteDto>> Insert(EditQuoteDto quoteDto)
        {
            try
            {
                if (quoteDto.Id != 0)
                    return new ApiResult<QuoteDto>(false, "Quote Id is identity, can't be modified.");

                Entities.Quote quoteToInsert = _mapper.Map<EditQuoteDto, Entities.Quote>(quoteDto);
                var result = await _quoteRepository.Insert(quoteToInsert);
                return new ApiResult<Entities.Quote, QuoteDto>(_mapper, await _quoteRepository.GetById(result.Id));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return new ApiResult<QuoteDto>(false, "Error creating new Quote record.");
            }
        }

        public async Task<ApiResult<QuoteDto>> Update(int id, EditQuoteDto quoteDto)
        {
            try
            {
                if (id != quoteDto.Id)
                    return new ApiResult<QuoteDto>(false, "Quote Id mismatch.");

                Entities.Quote quoteToUpdate = _mapper.Map<EditQuoteDto, Entities.Quote>(quoteDto);
                quoteToUpdate.Id = id;
                await _quoteRepository.Update(quoteToUpdate);
                return new ApiResult<Entities.Quote, QuoteDto>(_mapper, await _quoteRepository.GetById(id));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return new ApiResult<QuoteDto>(false, "Error updating Quote.");
            }
        }
    }
}
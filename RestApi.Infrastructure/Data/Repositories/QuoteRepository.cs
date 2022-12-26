using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RestApi.Core.Entities;
using RestApi.Core.Interfaces.Repositories;

#nullable disable

namespace RestApi.Infrastructure.Data.Repositories
{
    public class QuoteRepository : BaseRepository<Quote>, IQuoteRepository
    {
        public QuoteRepository(ApplicationDbContext dbContext) : base(dbContext)
        {

        }

        public async override Task<IEnumerable<Quote>> GetAll()
        {
            return await _entities.Include(x => x.Person).ToListAsync();
        }

        public async override Task<Quote> GetById(int id)
        {
            return await _entities.Include(x => x.Person).Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<int> GetQuoteOfTheDay()
        {
            List<int> resultIds = await _entities.Select(x => x.Id).ToListAsync();
            int rndIndex = new Random().Next(0, resultIds.Count);

            return resultIds.ElementAtOrDefault(rndIndex);
        }
    }
}

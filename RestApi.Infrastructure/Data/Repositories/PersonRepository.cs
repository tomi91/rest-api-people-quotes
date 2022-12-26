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
    public class PersonRepository : BaseRepository<Person>, IPersonRepository
    {
        public PersonRepository(ApplicationDbContext dbContext) : base(dbContext)
        {

        }

        public async override Task<IEnumerable<Person>> GetAll()
        {
            return await _entities.Include(x => x.Quotes).ToListAsync();
        }

        public async override Task<Person> GetById(int id)
        {
            return await _entities.Include(x => x.Quotes).Where(x => x.Id == id).FirstOrDefaultAsync();
        }

    }
}

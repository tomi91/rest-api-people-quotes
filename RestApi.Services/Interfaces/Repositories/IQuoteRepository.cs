using RestApi.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestApi.Core.Interfaces.Repositories
{
    public interface IQuoteRepository : IBaseRepository<Quote>
    {
        Task<int> GetQuoteOfTheDay();

    }
}

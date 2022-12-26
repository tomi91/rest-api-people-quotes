using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#nullable disable

namespace RestApi.Core.DTOs
{
    public class QuoteDto
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#nullable disable

namespace RestApi.Core.Entities
{
    public class Quote : BaseEntity
    {
        public string Description { get; set; }
        public int PersonId { get; set; }

        public virtual Person Person { get; set; }

    }
}

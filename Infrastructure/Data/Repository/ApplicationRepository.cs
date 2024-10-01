using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces.Data;
using Infrastructure.Data.DbContext;

namespace Infrastructure.Data.Repository
{
    public class ApplicationRepository<T> : GenericRepository<T>, IApplicationRepository<T>
        where T : class
    {
        public ApplicationRepository(ApplicationDbContext context)
            : base(context) { }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Data
{
    public interface IApplicationRepository<T> : IGenericRepository<T>
        where T : class { }
}

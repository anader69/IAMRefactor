using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IAMRefactor.Application.Interface
{
    public interface IGenericRepository<T> where T : class
    {
        Task<dynamic> Post(T model, string URL);
    }
}

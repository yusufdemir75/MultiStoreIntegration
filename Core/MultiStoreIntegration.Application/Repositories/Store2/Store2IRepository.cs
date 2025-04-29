using Microsoft.EntityFrameworkCore;
using MultiStoreIntegration.Domain.Entities.common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiStoreIntegration.Application.Repositories.Store2
{
    public interface Store2IRepository<T> where T : BaseEntity
    {
        DbSet<T> Table { get; }
    }
}

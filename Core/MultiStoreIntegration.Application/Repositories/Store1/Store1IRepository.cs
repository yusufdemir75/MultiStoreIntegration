using Microsoft.EntityFrameworkCore;
using MultiStoreIntegration.Domain.Entities.common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiStoreIntegration.Application.Repositories.Store1
{
    public interface Store1IRepository<T> where T : BaseEntity
    {
        DbSet<T> Table { get; }
    }
}

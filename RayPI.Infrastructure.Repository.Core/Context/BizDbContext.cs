using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace RayPI.Infrastructure.Repository.Core.Context
{
    public class BizDbContext : DbContext
    {
        public BizDbContext()
        {
        }

        public BizDbContext(DbContextOptions options)
            : base(options)
        {
        }
    }
}

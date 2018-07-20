using System;
using Microsoft.EntityFrameworkCore.Design;

namespace LMS.Data.Migrations
{
    public class DbContextDesignFactory : IDesignTimeDbContextFactory<LMSDbContext>
    {
        private static Lazy<LMSDbContext> dbContextLazy;

        public LMSDbContext CreateDbContext(string[] args)
        {
            return dbContextLazy.Value;
        }

        public static void RegisterDbContextFactory(Func<LMSDbContext> dbContextFactory)
        {
            dbContextLazy = new Lazy<LMSDbContext>(dbContextFactory);
        }
    }
}

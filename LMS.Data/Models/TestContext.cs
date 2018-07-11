using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LMS.Client.Web.Models
{
    public class TestContext : DbContext
    {
        public DbSet<Test> Test { get; set; }
        public DbSet<ProblemList> ProblemList { get; set; }
        public DbSet<Problem> Problem { get; set; }
        public DbSet<ChoicesList> ChoicesList { get; set; }
        public DbSet<Choice> Choice { get; set; }
    }
}

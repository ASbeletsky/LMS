using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LMS.Client.Web.Models
{
    public class TestContext : DbContext
    {
        public DbSet<Test> tests { get; set; }
        public DbSet<Problem_List> problem_Lists { get; set; }
        public DbSet<Problem> problems { get; set; }
        public DbSet<Choices_List> choices_Lists { get; set; }
        public DbSet<Choice> choices { get; set; }
    }
}

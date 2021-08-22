using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Workerservicereadjobs.Models
{
    class JobsContext : DbContext
    {
        public JobsContext(DbContextOptions<JobsContext> options) : base(options)
        {

        }
        public DbSet<JobsTable> JobsTable { get; set; }
        public DbSet<WrittenJobsTable> WrittenJobsTable { get; set; }
    }
}

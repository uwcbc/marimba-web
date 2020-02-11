using marimba_web.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace marimba_web.Data
{
    // reference for DbContext: https://www.youtube.com/watch?v=nN9jOORIFtc&list=PL6n9fhu94yhVkdrusLaQsfERmL_Jh4XmU&index=47
    // useful stuff: https://stackoverflow.com/questions/43767933/entity-framework-core-using-multiple-dbcontexts
    public class MarimbaDbContext : DbContext
    {
        public DbSet<Member> Members { get; set; }

        public MarimbaDbContext(DbContextOptions<MarimbaDbContext> options) : base(options)
        {
        }
    }
}

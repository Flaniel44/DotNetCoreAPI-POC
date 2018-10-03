using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Library3.Models
{
    //Local DB
    public class BookContext : DbContext
    {
        public BookContext(DbContextOptions<BookContext> options)
               : base(options)
        {
        }

        public DbSet<BookItem> BookItems { get; set; }
    }
}

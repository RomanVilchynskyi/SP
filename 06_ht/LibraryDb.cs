using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _06_ht
{
    public class LibraryDb : DbContext
    {
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source= DESKTOP-F5EBSVM\SQLEXPRESS;
                                Initial Catalog = LibraryApp2;
                                Integrated Security=True;
                                Connect Timeout=2;
                                Encrypt=False;
                                Trust Server Certificate=False;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var authors = new List<Author>();
            var books = new List<Book>();

            for (int i = 1; i <= 10; i++)
            {
                authors.Add(new Author { Id = i, Name = $"Author {i}" });
            }

            int bookId = 1;
            for (int i = 1; i <= 10; i++)
            {
                for (int j = 1; j <= 50; j++)
                {
                    books.Add(new Book
                    {
                        Id = bookId++,
                        Title = $"Book {j} - author {i}",
                        AuthorId = i
                    });
                }
            }

            modelBuilder.Entity<Author>().HasData(authors);
            modelBuilder.Entity<Book>().HasData(books);
        }
    }
}

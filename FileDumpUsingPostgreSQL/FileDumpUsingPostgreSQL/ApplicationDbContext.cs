using FileDumpUsingPostgreSQL.Models;
using Microsoft.EntityFrameworkCore;

namespace FileDumpUsingPostgreSQL
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<FileUploadModel> FileUpload { get; set; }
    }
}

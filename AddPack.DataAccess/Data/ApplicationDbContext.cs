using Microsoft.EntityFrameworkCore;

namespace AddPack.DataAccess.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {
        

    }
}

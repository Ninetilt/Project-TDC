using Microsoft.EntityFrameworkCore;

namespace TDC.Backend.Database
{
    public class TdcDbContext(DbContextOptions<TdcDbContext> options) : DbContext(options);
}

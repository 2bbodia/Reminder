namespace Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

public interface IApplicationDbContext
{
   

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
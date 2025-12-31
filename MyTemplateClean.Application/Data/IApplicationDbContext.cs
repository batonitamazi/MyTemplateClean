using Microsoft.EntityFrameworkCore;
using MyTemplateClean.Domain.Models;

namespace MyTemplateClean.Application.Data;

public interface IApplicationDbContext
{
    DbSet<Todo> Todos { get; }
    
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);

}
using Fiorello_PB101.Data;
using Fiorello_PB101.Models;
using Fiorello_PB101.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Fiorello_PB101.Services
{
    public class ExpertService : IExpertsService
    {
        private readonly AppDbContext _context;

        public ExpertService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Expert>> GetAllAsync()
        {
            return await _context.Experts.ToListAsync();
        }
    }
}

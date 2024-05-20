using Fiorello_PB101.Data;
using Fiorello_PB101.Models;
using Fiorello_PB101.Services.Interfaces;
using Fiorello_PB101.ViewModels.Categories;
using Microsoft.EntityFrameworkCore;

namespace Fiorello_PB101.Services
{
    public class CategoryService : ICategoryService
    {

        private readonly AppDbContext _context;

        public CategoryService(AppDbContext context)
        {
            _context = context;
        }

        public  async Task CreateAsync(Category category)
        {
            await _context.AddAsync(category);
            await _context.SaveChangesAsync();
        }

        public async  Task DeleteAsync(Category category)
        {
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();  
        }

        public async  Task<bool> ExistAsync(string name)
        {
            return await _context.Categories.AnyAsync(m => m.Name.Trim() == name.Trim());
        }

        public async Task<bool> ExistExceptByIdAsync(int? id, string name)
        {
            return await _context.Categories.AnyAsync(m => m.Name == name && m.Id != id);
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<IEnumerable<CategoryProductVM>> GetAllWithProductCountAsync()
        {
            IEnumerable<Category> categories = await _context.Categories.Include(m=>m.Products)
                                                                        .OrderByDescending(m=>m.Id)
                                                                        .ToListAsync(); 
            return categories.Select(m => new CategoryProductVM 
            {
                Id= m.Id,
                CategoryName = m.Name,
                CreatedDate = m.CreatedDate.ToString("dd-MM-yyyy"),
                ProductCount = m.Products.Count 
            });
        }

        public async Task<Category> GetByIdAsync(int? id)
        {
            return await _context.Categories.Include(m => m.Products).ThenInclude(m=> m.ProductImages).FirstOrDefaultAsync(m => m.Id == id);
        }
    }
}
 
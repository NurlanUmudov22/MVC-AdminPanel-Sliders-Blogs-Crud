using Fiorello_PB101.Models;
using Fiorello_PB101.ViewModels.Blog;

namespace Fiorello_PB101.Services.Interfaces
{
    public interface IExpertsService
    {
        Task<IEnumerable<Expert>> GetAllAsync();

    }
}

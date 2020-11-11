using EONET.BL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EONET.BL.Abstraction
{
    public interface ICategoryService
    {
        /// <summary>
        /// Gets the filtered categories asynchronously.
        /// </summary>
        /// <returns>List of Category models</returns>
        Task<List<CategoryModel>> GetCategoriesAsync();
    }
}
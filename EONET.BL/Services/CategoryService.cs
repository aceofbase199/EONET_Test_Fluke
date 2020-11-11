using AutoMapper;
using EONET.Api.Client;
using EONET.BL.Abstraction;
using EONET.BL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EONET.BL.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IHttpEonetClient _eonetClient;
        private readonly IMapper _mapper;

        public CategoryService(IHttpEonetClient eonetClient, IMapper mapper)
        {
            _eonetClient = eonetClient;
            _mapper = mapper;
        }

        public async Task<List<CategoryModel>> GetCategoriesAsync()
        {
            var categories = await _eonetClient.GetCategoriesAsync();

            return _mapper.Map<List<CategoryModel>>(categories);
        }
    }
}
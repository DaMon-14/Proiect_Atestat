using Prezenta_API.Models;

namespace Prezenta_API.Services
{
    public interface IMapper
    {
        Task<List<Mapper>> GetAllMappers();
        Task<Mapper> GetMapperByUserCode(uint userCode);
        Task<Mapper> AddMapper(UpdateMapper mapper);
        Task<Mapper> SetActiveMap(uint userCode, bool active);
        Task<bool> DeleteMapper(uint userCode);
    }
}

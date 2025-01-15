using Microsoft.EntityFrameworkCore;
using Prezenta_API.EF;
using Prezenta_API.Models;

namespace Prezenta_API.Services
{
    public class MapperService : IMapper
    {
        private readonly EF.Context _db;

        public MapperService(EF.Context db)
        {
            _db = db;
        }

        public async Task<List<Mapper>> GetAllMappers()
        {
            return await _db.Mappers.Where(x => x.UserId > 0).ToListAsync();
        }

        public async Task<Mapper> GetMapperByUserCode(uint userCode)
        {
            return await _db.Mappers.FirstOrDefaultAsync(entry => entry.UserCode == userCode);
        }

        public async Task<Mapper> AddMapper(UpdateMapper mapper)
        {
            var addMapper = new Mapper
            {
                UserId = mapper.UserId,
                isActive = mapper.isActive
            };

            _db.Add(addMapper);
            var result = await _db.SaveChangesAsync();
            return result >= 0 ? addMapper : null;
        }

        public async Task<Mapper> SetActiveMap(uint userCode, bool active)
        {
            var map = await _db.Mappers.FirstOrDefaultAsync(index => index.UserCode == userCode);
            if (map != null)
            {
                map.isActive = active;
                var result = await _db.SaveChangesAsync();
                return result >= 0 ? map : null;
            }
            return null;
        }

        public async Task<bool> DeleteMapper(uint userId)
        {
            var map = await _db.Mappers.FirstOrDefaultAsync(index => index.UserCode == userId);
            if (map != null)
            {
                _db.Mappers.Remove(map);
                var result = await _db.SaveChangesAsync();
                return result >= 0;
            }
            return false;
        }
    }
}

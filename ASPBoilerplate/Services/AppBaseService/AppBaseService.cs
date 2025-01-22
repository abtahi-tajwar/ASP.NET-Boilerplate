using Microsoft.Extensions.Caching.Distributed;

namespace ASPBoilerplate.Services {
    public class AppBaseService {
        protected CacheService _cache;
        protected AppDbContext _context;
        public AppBaseService(AppDbContext context, CacheService cache) {
            _cache = cache;
            _context = context;
        }
    }
}
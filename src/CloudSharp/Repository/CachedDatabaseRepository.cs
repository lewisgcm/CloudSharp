using System.Threading.Tasks;
using CloudSharp.Configuration;
using CloudSharp.Model;

namespace CloudSharp.Repository
{
    public class CachedDatabaseRepository<ID, Model> : DatabaseRepository<ID, Model>, IRepository<ID, Model> where Model : class, IEntity<ID>
    {
        private ICache<ID, Model> _cache;

        public CachedDatabaseRepository(IDbContext context, ICache<ID, Model> cache)
            : base( context )
        {
            _cache = cache;
        }

        public new async Task<Model> Get(ID id)
        {
            var existing = await _cache.Get(id);
            if (existing == null)
            {
                existing = await base.Get(id);
                await _cache.Save( id, existing );
            }
            return existing;
        }

        public new async Task<Model> Update(ID id, Model model )
        {
            model = await base.Update(id, model);
            await _cache.Save(id, model);
            return model;
        }

        public new async Task<Model> Create( Model model )
        {
            model = await base.Create( model );
            await _cache.Save( model.Id, model );
            return model;
        }

        public new async Task<bool> Delete( ID id )
        {
            var result = await base.Delete( id );
            await _cache.Delete( id );
            return result;
        }
    }
}
using System.Collections.Generic;
using System.Threading.Tasks;
using CloudSharp.Model;
using CloudSharp.Configuration;

namespace CloudSharp.Service {

    public class ServiceCacheableMicroservice<ID, Model> : ServiceMicroservice<ID, Model>, IService<ID, Model> 
        where Model : class, IEntity<ID>
    {
        private ICache<ID, Model> _cache;
        
        public ServiceCacheableMicroservice(
            IServiceRegistration<Model> serviceRegistration,
            ICache<ID, Model> cache
        )
        : base(serviceRegistration) {
            _cache = cache;
        }

        public new async Task<Model> Create(Model model)
        {
            model = await base.Create( model );
            await _cache.Save( model.Id, model );
            return model;
        }

        public new async Task<bool> Delete(ID id)
        {
            var result = await base.Delete(id);
            await _cache.Delete( id );
            return result;
        }

        public new async Task<Model> Get(ID id)
        {
            var result = await _cache.Get( id );
            if( result == null ) {
                result = await base.Get( id );
                await _cache.Save( id, result );
            }
            return result;
        }

        public new async Task<List<Model>> GetList()
        {
            return await base.GetList();
        }

        public new async Task<Model> Update(ID id, Model model)
        {
            var result = await base.Update(id, model);
            await _cache.Save( id, model );
            return result;
        }
    }
}
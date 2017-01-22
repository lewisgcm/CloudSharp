using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CloudSharp.Model;
using CloudSharp.Repository;

namespace CloudSharp.Service {

    public class Service<ID, Model> : IService<ID, Model>
        where Model : class, IEntity<ID>
    {
        private IRepository<ID, Model> _repository;

        public Service( IRepository<ID, Model> repository ) {
            _repository = repository;
        }
        
        public Task<Model> Create(Model model)
        {
            return _repository.Create( model );
        }

        public Task<bool> Delete(ID id)
        {
            return _repository.Delete( id );
        }

        public Task<Model> Get(ID id)
        {
            return _repository.Get( id );
        }

        public Task<List<Model>> GetList()
        {
            return _repository.GetList();
        }

        public Task<Model> Update(ID id, Model model)
        {
            return _repository.Update(id, model );
        }
    }
}
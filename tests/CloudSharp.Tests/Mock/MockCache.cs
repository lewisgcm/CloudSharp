using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CloudSharp.Configuration;

namespace CloudSharp.Tests.Mock
{
    public class MockCache : ICache<object, object>
    {
        Dictionary<object, object> _store;

        MockCache() {
            _store = new Dictionary<object,object>();
        }

        public Task Delete(object id)
        {
            return Task.Run( () => _store.Remove(id) );
        }

        public async Task<object> Get(object id)
        {
            return await Task.Run( () => _store[id] );
        }

        public Task Save(object id, object model)
        {
            return Task.Run( () => _store[id] = model );
        }
    }
}
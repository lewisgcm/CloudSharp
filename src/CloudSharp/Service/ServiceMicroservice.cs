using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using CloudSharp.Model;
using CloudSharp.Configuration;

namespace CloudSharp.Service {

    public class ServiceMicroservice<ID, Model> : IService<ID, Model> 
        where Model : class, IEntity<ID>
    {
        private IServiceRegistration<Model> _serviceRegistration;
        private HttpClient _client;

        public ServiceMicroservice(IServiceRegistration<Model> serviceRegistration) {
            _serviceRegistration = serviceRegistration;
            _client = new HttpClient( _serviceRegistration.Handler );
        }

        public async Task<Model> Create(Model model)
        {
            var request = new HttpRequestMessage(
                HttpMethod.Post,
                $"{_serviceRegistration.GetService}"
            );
            var result = await _client.SendAsync( request );
            return await result.Content.ReadAsAsync<Model>();
        }

        public async Task<bool> Delete(ID id)
        {
            var request = new HttpRequestMessage(
                HttpMethod.Delete,
                $"{_serviceRegistration.GetService}/{id}"
            );
            var result = await _client.SendAsync( request );
            return await result.Content.ReadAsAsync<bool>();
        }

        public async Task<Model> Get(ID id)
        {
            var request = new HttpRequestMessage(
                HttpMethod.Get,
                $"{_serviceRegistration.GetService}/{id}"
            );
            var result = await _client.SendAsync( request );
            return await result.Content.ReadAsAsync<Model>();
        }

        public async Task<List<Model>> GetList()
        {
            var request = new HttpRequestMessage(
                HttpMethod.Get,
                $"{_serviceRegistration.GetService}"
            );
            var result = await _client.SendAsync( request );
            return await result.Content.ReadAsAsync<List<Model>>();
        }

        public async Task<Model> Update(ID id, Model model)
        {
            var request = new HttpRequestMessage(
                HttpMethod.Put,
                $"{_serviceRegistration.GetService}/{id}"
            );
            var result = await _client.SendAsync( request );
            return await result.Content.ReadAsAsync<Model>();
        }
    }
}
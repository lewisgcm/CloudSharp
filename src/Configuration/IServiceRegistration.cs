
using System;
using System.Net.Http;

namespace CloudSharp.Microservices.Configuration {
    public interface IServiceRegistration<Model> {
        Uri GetService { get; }

        HttpMessageHandler Handler { get; }
    }
}
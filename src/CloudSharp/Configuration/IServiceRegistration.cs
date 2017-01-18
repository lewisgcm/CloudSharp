
using System;
using System.Net.Http;

namespace CloudSharp.Configuration {
    public interface IServiceRegistration<Model> {
        Uri GetService { get; }

        HttpMessageHandler Handler { get; }
    }
}
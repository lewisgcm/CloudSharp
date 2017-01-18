
using System;
using System.Net.Http;

namespace CloudSharp.Core.Configuration {
    public interface IServiceRegistration<Model> {
        Uri GetService { get; }

        HttpMessageHandler Handler { get; }
    }
}
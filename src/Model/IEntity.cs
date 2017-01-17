/**
 * (C) Lewis Maitland 2017
 * Base IEntity class for generic handling of cacheable and services.
*/
namespace CloudSharp.Core.Model {
    public interface IEntity<ID> {
        ID Id { get; set; }
    }
}
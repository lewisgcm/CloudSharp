using System.Collections.Generic;
using System.Threading.Tasks;

/**
 * (C) Lewis Maitland 2017
 * Generic Async Service interface. 
*/
namespace CloudSharp.Service
{
    public interface IService<ID, Model> where Model : class
    {
        Task<Model> Get(ID id);

        Task<List<Model>> GetList();

        Task<Model> Update(ID id, Model model);

        Task<Model> Create(Model model);

        Task<bool> Delete(ID id);
    }
}
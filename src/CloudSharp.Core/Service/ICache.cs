using System.Threading.Tasks;

namespace CloudSharp.Core.Service
{
    public interface ICache<ID, Model> where Model : class
    {
        Task<Model> Get(ID id);
        Task Save(ID id, Model model);
        Task Delete(ID id);
    }
}
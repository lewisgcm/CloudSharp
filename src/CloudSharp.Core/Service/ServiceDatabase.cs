using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CloudSharp.Core.Service
{
    public class ServiceDatabase<ID, Model> : IService<ID, Model> where Model : class
    {
        private readonly IDbContext _context;

        public ServiceDatabase( IDbContext context )
        {
            this._context = context;
        }

        public Task<Model> Get(ID id)
        {
            return this._context
                .Set<Model>()
                .FindAsync(id);
        }

        public Task<List<Model>> GetList()
        {
            return this._context
                .Set<Model>()
                .ToListAsync();
        }

        public async Task<Model> Update(ID id, Model model)
        {
            var tracking = await this._context
                .Set<Model>()
                .FindAsync(id);

            _context
                .Entry( tracking )
                .CurrentValues
                .SetValues( model );

            await this._context.SaveChangesAsync();
            return tracking;
        }

        public async Task<Model> Create(Model model)
        {
            var tracking = await this._context
                .Set<Model>()
                .AddAsync( model );
            await this._context.SaveChangesAsync();
            return tracking.Entity;
        }

        public async Task<bool> Delete(ID id)
        {
            var model = await Get(id);
            this._context
                .Set<Model>()
                .Remove( model );

            return await this._context.SaveChangesAsync() > 0;
        }
    }
}
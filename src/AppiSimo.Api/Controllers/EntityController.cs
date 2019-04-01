namespace AppiSimo.Api.Controllers
{
    using System;
    using System.Threading.Tasks;
    using Data;
    using Microsoft.AspNet.OData;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Shared.Abstract;

    [Authorize]
    public abstract class EntityController<TEntity> : ODataController
        where TEntity : class, IEntity, new()
    {
        protected readonly KingRogerContext Context;

        protected EntityController(KingRogerContext context)
        {
            Context = context;
        }
        
        [EnableQuery]
        public virtual IActionResult Get() => Ok(Context.Set<TEntity>());

        [HttpPost]
        public virtual async Task<IActionResult> Post([FromBody] TEntity entity)
        {
            var result = await Context.Set<TEntity>().AddAsync(entity);
            await Context.SaveChangesAsync();

            return Created("/", result.Entity);
        }

        [HttpPut]
        public virtual async Task<IActionResult> Put([FromBody] TEntity entity)
        {
            var result = Context.Set<TEntity>().Update(entity);
            await Context.SaveChangesAsync();

            return Ok(result.Entity);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(Guid key)
        {
            var entity = new TEntity
            {
                Id = key
            };

            var result = Context.Set<TEntity>().Remove(entity);
            await Context.SaveChangesAsync();

            return Ok(result.Entity);
        }
    }
}
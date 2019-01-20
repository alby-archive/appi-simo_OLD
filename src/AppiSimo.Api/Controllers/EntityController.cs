namespace AppiSimo.Api.Controllers
{
    using System;
    using System.Threading.Tasks;
    using Data;
    using Microsoft.AspNet.OData;
    using Microsoft.AspNetCore.Mvc;
    using Shared.Abstract;

    public abstract class EntityController<TEntity> : ODataController
        where TEntity : class, IEntity, new()
    {
        protected readonly KingRogerContext Context;

        protected EntityController(KingRogerContext context)
        {
            Context = context;
        }

        [EnableQuery]
        [HttpGet]
        public virtual IActionResult Get() => Ok(Context.Set<TEntity>());

        [HttpGet("{key}")]
        public virtual async Task<IActionResult> Get(Guid key) => Ok(await Context.Set<TEntity>().FindAsync(key));

        [HttpPost]   
        [Route("odata/[controller]/[action]")]
        public virtual async Task<IActionResult> Post([FromBody] TEntity entity)
        {
            var result = await Context.Set<TEntity>().AddAsync(entity);
            await Context.SaveChangesAsync();

            return Created("/", result.Entity);
        }

        [HttpPut]
        [Route("odata/[controller]/[action]")]
        public virtual async Task<IActionResult> Put([FromBody] TEntity entity)
        {
            var result = Context.Set<TEntity>().Update(entity);
            await Context.SaveChangesAsync();

            return Ok(result.Entity);
        }

        [HttpDelete]
        [Route("odata/[controller]/[action]/{key}")]
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
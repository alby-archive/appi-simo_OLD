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
        readonly KingRogerContext _context;

        protected EntityController(KingRogerContext context)
        {
            _context = context;
        }

        [EnableQuery]
        [HttpGet]
        public virtual IActionResult Get() => Ok(_context.Set<TEntity>());

        [HttpGet("{key}")]
        public virtual async Task<IActionResult> Get(Guid key) => Ok(await _context.Set<TEntity>().FindAsync(key));

        [HttpPost]   
        [Route("odata/[controller]/[action]")]
        public virtual async Task<IActionResult> Post([FromBody] TEntity entity)
        {
            var result = await _context.Set<TEntity>().AddAsync(entity);
            await _context.SaveChangesAsync();

            return Created("/", result.Entity);
        }

        [HttpPut]
        [Route("odata/[controller]/[action]")]
        public virtual async Task<IActionResult> Put([FromBody] TEntity entity)
        {
            var result = _context.Set<TEntity>().Update(entity);
            await _context.SaveChangesAsync();

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

            var result = _context.Set<TEntity>().Remove(entity);
            await _context.SaveChangesAsync();

            return Ok(result.Entity);
        }
    }
}
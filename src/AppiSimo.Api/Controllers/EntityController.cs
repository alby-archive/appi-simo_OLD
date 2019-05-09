namespace AppiSimo.Api.Controllers
{
    using System;
    using System.Threading.Tasks;
    using Data;
    using Microsoft.AspNet.OData;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore.ChangeTracking;
    using Shared.Abstract;

//    [Authorize]
    public abstract class EntityController<TEntity> : ODataController
        where TEntity : class, IEntity, new()
    {
        protected readonly KingRogerContext _context;

        protected EntityController(KingRogerContext context)
        {
            _context = context;
        }

        [EnableQuery]
        public virtual IActionResult Get() => Ok(_context.Set<TEntity>());

        [HttpPost]
        public virtual async Task<IActionResult> Post([FromBody] TEntity entity)
        {
            var result = await _context.Set<TEntity>().AddAsync(entity);
            await _context.SaveChangesAsync();

            return Created("/", result.Entity);
        }

        [HttpPut]
        public virtual async Task<IActionResult> Put([FromBody] TEntity entity)
        {
            var result = await UpdateAsync(entity);

            return Ok(result.Entity);
        }

        [HttpDelete]
        public virtual async Task<IActionResult> Delete(Guid key)
        {
            var entity = new TEntity
            {
                Id = key
            };

            var result = _context.Set<TEntity>().Remove(entity);
            await _context.SaveChangesAsync();

            return Ok(result.Entity);
        }

        protected async Task<EntityEntry<TEntity>> UpdateAsync(TEntity entity)
        {
            var result = _context.Set<TEntity>().Update(entity);
            await _context.SaveChangesAsync();

            return result;
        }
    }
}